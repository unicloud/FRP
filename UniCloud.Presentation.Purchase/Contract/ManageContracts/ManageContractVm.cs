﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/05，17:12
// 文件名：QueryContractVM.cs
// 程序集：UniCloud.Presentation.Purchase
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.DocumentExtension;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Contract.ManageContracts
{
    [Export(typeof (ManageContractVm))]
    public class ManageContractVm : ViewModelBase
    {
        #region 初始化

        private readonly PurchaseData _context; //域上下文
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;
        private string _loadType; //加载子项文件夹方式方式，1、DoubleClick 双击,2、SearchText 搜索框
        private FilterDescriptor _pathFilterDes;

        [ImportingConstructor]
        public ManageContractVm(IRegionManager regionManager, IPurchaseService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
            InitialDocumentPath(); //初始化文档路径
            InitialCommad(); //初始化命令
            InitialDocument();
        }

        /// <summary>
        ///     初始化文档路径
        /// </summary>
        private void InitialDocumentPath()
        {
            DocumentPathsView = _service.CreateCollection(_context.DocumentPaths.Expand(p => p.SubDocumentPaths));
            _pathFilterDes = new FilterDescriptor("ParentId", FilterOperator.IsEqualTo, null);
            DocumentPathsView.FilterDescriptors.Add(_pathFilterDes);
            DocumentPathsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (_openResult)
                    GetOpenFolderSearchResults();
                else
                    GetListBoxDocumentItem();
            };
            DocumentPathsView.SubmittedChanges += (o, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    MessageAlert("提示", "保存失败，请检查");
                    return;
                }
                AddDocumentPathChildView.Close();
            };
        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialCommad()
        {
            OpenDocPathListBoxCommand = new DelegateCommand<object>(OnOpen, CanOpenListBox);
            OpenDocPathToolBarCommand = new DelegateCommand<object>(OnOpen, CanOpenToolBar);
            CreateDocumentPathCommand = new DelegateCommand<object>(CreateDocumentPath, CanCreateDocumentPath);
            DeleteDocumentPathToolBarCommand = new DelegateCommand<object>(DeleteDocumentPathToolBar,
                CanDeleteDocumentPathToolBar);
            DeleteDocumentPathListBoxCommand = new DelegateCommand<object>(DeleteDocumentPathListBox,
                CanDeleteDocumentPathListBox);
            BackToParentFolderCommand = new DelegateCommand<object>(OnBackToParentFolder);

            CommitCommand = new DelegateCommand<object>(Commit, CanCommit);
            CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
        }

        /// <summary>
        ///     初始化文档
        /// </summary>
        private void InitialDocument()
        {
            ContractDocuments = _service.CreateCollection(_context.ContractDocuments);
            ContractDocuments.PageSize = 20;
            ContractDocuments.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (e.Entities.Cast<ContractDocumentDTO>().FirstOrDefault() != null)
                {
                    ContractDocument = (e.Entities.Cast<ContractDocumentDTO>().FirstOrDefault());
                }
            };
        }

        #endregion

        #region 属性

        /// <summary>
        ///     TreeView绑定集合
        /// </summary>
        private readonly ObservableCollection<ListBoxDocumentItem> _listBoxDocumentItems =
            new ObservableCollection<ListBoxDocumentItem>();

        /// <summary>
        ///     RadBreadcrumb控件中当前选中ListBox中的当前路径
        /// </summary>
        private ListBoxDocumentItem _currentPathItem;

        /// <summary>
        ///     根路径
        /// </summary>
        private ListBoxDocumentItem _rootPath;

        /// <summary>
        ///     Lisbox控件中，选择的当前项
        /// </summary>
        private ListBoxDocumentItem _selectDocumentPath;

        /// <summary>
        ///     文档路径View
        /// </summary>
        public QueryableDataServiceCollectionView<DocumentPathDTO> DocumentPathsView { get; set; }

        /// <summary>
        ///     合同文档View
        /// </summary>
        public QueryableDataServiceCollectionView<ContractDocumentDTO> ContractDocuments { get; set; }

        public ListBoxDocumentItem CurrentPathItem
        {
            get { return _currentPathItem; }
            set
            {
                if (_currentPathItem != value)
                {
                    _currentPathItem = value;
                    RaisePropertyChanged(() => CurrentPathItem);
                    if (value != null)
                    {
                        WatermarkText = "搜索 " + CurrentPathItem.Name;
                        _loadType = "SearchText";
                        //加载子项文件夹文档，即模仿打开文件夹，加载文件夹里的子文件夹与文件
                        LoadSubFolderDocuemnt(value);
                    }
                }
            }
        }

        public ListBoxDocumentItem RootPath
        {
            get { return _rootPath; }
            set
            {
                if (_rootPath != value)
                {
                    _rootPath = value;
                    RaisePropertyChanged(() => RootPath);
                }
            }
        }

        public ListBoxDocumentItem SelectDocumentPath
        {
            get { return _selectDocumentPath; }
            set
            {
                if (_selectDocumentPath != value)
                {
                    _selectDocumentPath = value;
                    DeleteDocumentPathToolBarCommand.RaiseCanExecuteChanged();
                    OpenDocPathToolBarCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelectDocumentPath);
                }
            }
        }

        public ObservableCollection<ListBoxDocumentItem> ListBoxDocumentItems
        {
            get { return _listBoxDocumentItems; }
        }

        /// <summary>
        ///     添加文件夹路径图标
        /// </summary>
        public Image AddFolderImage
        {
            get { return ImagePathHelper.GetImageSourceByName("SmallAddFolder"); }
        }

        /// <summary>
        ///     添加文件路径图标
        /// </summary>
        public Image AddDocumentImage
        {
            get { return ImagePathHelper.GetImageSourceByName("SmallAddDocument"); }
        }

        /// <summary>
        ///     打开文件路径图标
        /// </summary>
        public Image OpenDocumentImage
        {
            get { return ImagePathHelper.GetImageSourceByName("SmallOpenDocument"); }
        }

        /// <summary>
        ///     删除文件路径图标
        /// </summary>
        public Image DelDocumentImage
        {
            get { return ImagePathHelper.GetImageSourceByName("SmallDelDocument"); }
        }

        #endregion

        #region 方法

        /// <summary>
        ///     获取ListBoxItem
        /// </summary>
        private void GetListBoxDocumentItem()
        {
            IsBusy = false;
            if (DocumentPathsView.Any(p => p.ParentId == null))
            {
                //获取顶层文件夹文档          
                RootPath =
                    ListBoxItemHelper.TransformToRootListBoxItem(
                        DocumentPathsView.FirstOrDefault(p => p.ParentId == null));
                //为Treeview集合
                _listBoxDocumentItems.Clear();
                _listBoxDocumentItems.Add(RootPath);
                CurrentPathItem = RootPath;
            }
            else //双击打开文件夹 
                if (_loadType.Equals("DoubleClick", StringComparison.OrdinalIgnoreCase))
                {
                    if (SelectDocumentPath != null && !SelectDocumentPath.IsLeaf)
                    {
                        //子项文档集合
                        var childDocuments =
                            DocumentPathsView.Where(p => p.ParentId == SelectDocumentPath.DocumentPathId);
                        //设置当前选中项
                        _currentPathItem = CurrentPathItem.SubDocumentPaths.FirstOrDefault(
                            p => p.DocumentPathId == SelectDocumentPath.DocumentPathId);
                        //子项文档转化为ListBoxItem
                        ListBoxItemHelper.TransformToSubListBoxItem(_currentPathItem, childDocuments);
                        RaisePropertyChanged(() => CurrentPathItem);
                    }
                }
                else //通过搜索框打开文件夹
                {
                    if (CurrentPathItem != null)
                    {
                        //子项文档集合
                        var childDocuments = DocumentPathsView
                            .Where(p => p.ParentId == _currentPathItem.DocumentPathId);
                        //子项文档转化为ListBoxItem
                        ListBoxItemHelper.TransformToSubListBoxItem(_currentPathItem, childDocuments);
                        RaisePropertyChanged(() => CurrentPathItem);
                    }
                }
            RaisePropertyChanged(() => ListBoxDocumentItems);
        }

        /// <summary>
        ///     双击ListBoxItem
        /// </summary>
        public void ListBoxDoubleClick()
        {
            if (SelectDocumentPath == null)
                return;
            if (!SelectDocumentPath.IsLeaf)
            {
                _loadType = "DoubleClick"; //双击打开文件夹形式
                LoadSubFolderDocuemnt(SelectDocumentPath);
                return;
            }
            OpenDocument(SelectDocumentPath.DocumentGuid);
        }

        /// <summary>
        ///     打开文件
        /// </summary>
        public void OpenDocument(Guid? documentId)
        {
            if (documentId != null) OnViewAttach(documentId);
        }

        /// <summary>
        ///     加载子项文件与文件夹
        /// </summary>
        private void LoadSubFolderDocuemnt(ListBoxDocumentItem currentDocumentPath)
        {
            if (_pathFilterDes.Value != null && currentDocumentPath != null &&
                int.Parse(_pathFilterDes.Value.ToString()) == currentDocumentPath.DocumentPathId)
            {
                GetListBoxDocumentItem(); //加载子项
                return;
            }
            if (currentDocumentPath != null)
            {
                _openResult = false;
                //根据选中的父亲节点，查询子项文件
                if (currentDocumentPath.ParentId != null)
                {
                    IsBusy = true;
                    _pathFilterDes.Value = currentDocumentPath.DocumentPathId;
                    return;
                }
                //选中的项为根目录，其父亲节点为空
                _pathFilterDes.Value = null;
            }
        }

        /// <summary>
        ///     创建文件路劲
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isLeaf">是否是叶子节点</param>
        /// <param name="documentId">文档Guid</param>
        /// <param name="parentId">父亲文档</param>
        /// <returns>Uri实例</returns>
        private Uri CreateDocumentPathQuery(string name, bool isLeaf, string documentId, int parentId)
        {
            return new Uri(string.Format("AddDocPath?name='{0}'&isLeaf='{1}'" +
                                         "&documentId='{2}'&parentId={3}", name, isLeaf, documentId, parentId),
                UriKind.Relative);
        }

        private Uri ModifyDocumentPath(int documentPathId, string name, int? parentId)
        {
            return
                new Uri(
                    string.Format("ModifyDocPath?docPathId={0}&name='{1}'&parentId={2}", documentPathId, name, parentId),
                    UriKind.Relative);
        }

        /// <summary>
        ///     删除文档路径
        /// </summary>
        /// <param name="docPathId"></param>
        /// <returns></returns>
        private Uri DeleteDocumentPathQuery(int docPathId)
        {
            return new Uri(string.Format("DelDocPath?docPathId={0}", docPathId), UriKind.Relative);
        }

        #endregion

        #region 命令

        #region 打开文档

        /// <summary>
        ///     打开文档通过右击ListBoxItem，选择的RadContextMenu触发的事件
        /// </summary>
        public DelegateCommand<object> OpenDocPathListBoxCommand { get; private set; }

        /// <summary>
        ///     打开文档通过单击ToolBar的打开按钮触发的事件
        /// </summary>
        public DelegateCommand<object> OpenDocPathToolBarCommand { get; private set; }

        /// <summary>
        ///     打开文档
        /// </summary>
        private void OnOpen(object sender)
        {
            var selectItem = sender as ListBoxDocumentItem;
            if (selectItem == null)
                return;
            SelectDocumentPath = selectItem;
            if (!SelectDocumentPath.IsLeaf)
            {
                _loadType = "DoubleClick"; //双击打开文件夹形式
                LoadSubFolderDocuemnt(SelectDocumentPath);
                return;
            }
            OpenDocument(SelectDocumentPath.DocumentGuid);
        }

        /// <summary>
        ///     打开文档通过右击ListBoxItem，选择的RadContextMenu触发的事件按钮是否可用
        /// </summary>
        private bool CanOpenListBox(object sender)
        {
            return true;
        }

        /// <summary>
        ///     打开文档通过单击ToolBar的打开按钮触发的事件按钮是否可用
        /// </summary>
        private bool CanOpenToolBar(object sender)
        {
            return SelectDocumentPath != null;
        }

        #endregion

        #region 新建文档

        private bool _isRenameFolder;
        public DelegateCommand<object> CreateDocumentPathCommand { get; private set; }

        private void CreateDocumentPath(object sender)
        {
            if (sender.ToString().Equals("文件夹", StringComparison.OrdinalIgnoreCase))
            {
                _isRenameFolder = false;
                DocumentName = string.Empty;
                AddDocumentPathChildView.Header = "新建文件夹";
                AddDocumentPathChildView.ShowDialog();
            }
            else if (sender.ToString().Equals("重命名文件夹", StringComparison.OrdinalIgnoreCase))
            {
                if (CurrentPathItem == null)
                {
                    MessageAlert("请选择相应的文件夹！");
                    return;
                }
                if (CurrentPathItem.ParentId == null)
                {
                    MessageAlert("根文件夹不能重命名！");
                    return;
                }
                _isRenameFolder = true;
                DocumentName = CurrentPathItem.Name;
                AddDocumentPathChildView.Header = "重命名文件夹";
                AddDocumentPathChildView.ShowDialog();
            }
            else
            {
                if (!ContractDocuments.AutoLoad)
                {
                    ContractDocuments.AutoLoad = true;
                }
                else
                {
                    ContractDocuments.Load(true);
                }
                AddDocumetChildView.ShowDialog();
            }
        }

        private bool CanCreateDocumentPath(object sender)
        {
            return true;
        }

        #endregion

        #region 删除文档

        /// <summary>
        ///     删除文档通过右击ListBoxItem，选择的RadContextMenu触发的事件
        /// </summary>
        public DelegateCommand<object> DeleteDocumentPathListBoxCommand { get; private set; }

        /// <summary>
        ///     删除文档通过右击ToolBar的删除文件按钮触发的事件
        /// </summary>
        public DelegateCommand<object> DeleteDocumentPathToolBarCommand { get; private set; }

        /// <summary>
        ///     删除文档通过单击ToolBar的删除文件按钮触发的事件
        /// </summary>
        private void DeleteDocumentPathToolBar(object sender)
        {
            if (SelectDocumentPath == null)
            {
                MessageAlert("提示", "请选择需要删除文件或文件夹");
                return;
            }
            var message = SelectDocumentPath.IsLeaf ? "确定删除此文件 " : "确定删除此文件夹 ";
            message += SelectDocumentPath.Name;
            MessageConfirm("提示", message, (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    DeleteDocPath();
                }
            });
        }

        /// <summary>
        ///     删除文档通过右击ListBox的删除文件按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        private void DeleteDocumentPathListBox(object sender)
        {
            if (sender == null)
            {
                MessageAlert("提示", "请选择需要删除文件");
                return;
            }
            SelectDocumentPath = sender as ListBoxDocumentItem;
            if (SelectDocumentPath == null)
            {
                MessageAlert("提示", "请选择需要删除文件");
                return;
            }
            var message = SelectDocumentPath.IsLeaf ? "确定删除此文件 " : "确定删除此文件夹 ";
            message += SelectDocumentPath.Name;
            MessageConfirm("提示", message, (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    DeleteDocPath();
                }
            });
        }

        /// <summary>
        ///     删除文档路径
        /// </summary>
        private void DeleteDocPath()
        {
            var delDocPath = DeleteDocumentPathQuery(SelectDocumentPath.DocumentPathId);
            _context.BeginExecute<string>(delDocPath,
                p => Deployment.Current.Dispatcher.BeginInvoke(() => DocumentPathsView.Load(true)),
                null);
        }

        /// <summary>
        ///     删除文档通过右击ListBoxItem，选择的RadContextMenu触发的事件按钮是否可用
        /// </summary>
        private bool CanDeleteDocumentPathListBox(object sender)
        {
            return true;
        }

        /// <summary>
        ///     删除文档通过单击ToolBar的删除文件按钮触发的事件按钮是否可用
        /// </summary>
        private bool CanDeleteDocumentPathToolBar(object sender)
        {
            return SelectDocumentPath != null;
        }

        #endregion

        #endregion

        #region 子窗体相关

        private ContractDocumentDTO _contractDocument; //选中的文档
        private bool _documentChildIsBusy;
        private string _documentName;

        /// <summary>
        ///     初始化添加文件夹子窗体
        /// </summary>
        public AddDocumentPathChild AddDocumentPathChildView
        {
            get { return ServiceLocator.Current.GetInstance<AddDocumentPathChild>(); }
        }

        /// <summary>
        ///     初始化添加文档子窗体
        /// </summary>
        public AddDocumetChild AddDocumetChildView
        {
            get { return ServiceLocator.Current.GetInstance<AddDocumetChild>(); }
        }

        /// <summary>
        ///     新建文件夹路径名称
        /// </summary>
        public string DocumentName
        {
            get { return _documentName; }
            set
            {
                if (_documentName != value)
                {
                    _documentName = value;
                    RaisePropertyChanged(() => DocumentName);
                }
            }
        }

        /// <summary>
        ///     选中文档
        /// </summary>
        public ContractDocumentDTO ContractDocument
        {
            get { return _contractDocument; }
            set
            {
                if (_contractDocument != value)
                {
                    _contractDocument = value;
                    RaisePropertyChanged(() => ContractDocument);
                }
            }
        }

        /// <summary>
        ///     子窗体是否正在处理中
        /// </summary>
        public bool DocumentChildIsBusy
        {
            get { return _documentChildIsBusy; }
            set
            {
                if (_documentChildIsBusy != value)
                {
                    _documentChildIsBusy = value;
                    RaisePropertyChanged(() => DocumentChildIsBusy);
                }
            }
        }

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void Cancel(object sender)
        {
            if (sender.ToString().Equals("文件夹", StringComparison.OrdinalIgnoreCase))
            {
                AddDocumentPathChildView.Close();
            }
            else
            {
                AddDocumetChildView.Close();
            }
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancel(object sender)
        {
            if (DocumentPathsView.IsSubmittingChanges)
            {
                return false;
            }
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void Commit(object sender)
        {
            if (CurrentPathItem == null)
            {
                MessageAlert("提示", "请选择父文件夹");
                return;
            }
            _loadType = "SearchText";
            if (sender.ToString().Equals("文件夹", StringComparison.OrdinalIgnoreCase))
            {
                AddFolder(); //文件夹添加
            }
            else
            {
                AddDocument(); //文档添加
            }
        }

        /// <summary>
        ///     添加文件夹
        /// </summary>
        private void AddFolder()
        {
            if (string.IsNullOrEmpty(DocumentName.Trim()))
            {
                MessageAlert("提示", "文件夹名称不能为空");
                return;
            }
            Uri newDocPath;
            if (_isRenameFolder)
            {
                var sameLevelItems = FindSameLevelItems(_listBoxDocumentItems, CurrentPathItem.ParentId);
                if (sameLevelItems != null)
                {
                    if (sameLevelItems.Any(p => p.Name.Equals(DocumentName, StringComparison.OrdinalIgnoreCase)))
                    {
                        MessageAlert("提示", "已存在同名文件夹");
                        return;
                    }
                }
                CurrentPathItem.Name = DocumentName;
                newDocPath = ModifyDocumentPath(CurrentPathItem.DocumentPathId, DocumentName, CurrentPathItem.ParentId);
            }
            else
            {
                if (CurrentPathItem.SubFolders.Any(p => p.Name.Equals(DocumentName, StringComparison.OrdinalIgnoreCase)))
                {
                    MessageAlert("提示", "已存在同名文件夹");
                    return;
                }
                newDocPath = CreateDocumentPathQuery(DocumentName, false, null,
                    CurrentPathItem.DocumentPathId);
            }
            DocumentChildIsBusy = true;
            _context.BeginExecute<string>(newDocPath, p => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                DocumentPathsView.Load(true);
                if (AddDocumentPathChildView.IsOpen)
                {
                    AddDocumentPathChildView.Close();
                }
                DocumentChildIsBusy = false;
            }), null);
        }

        private IEnumerable<ListBoxDocumentItem> FindSameLevelItems(IEnumerable<ListBoxDocumentItem> subItems,
            int? parentId)
        {
            var listBoxDocumentItems = subItems as ListBoxDocumentItem[] ?? subItems.ToArray();
            foreach (var listBoxDocumentItem in listBoxDocumentItems)
            {
                if (listBoxDocumentItem.DocumentPathId == parentId)
                {
                    return listBoxDocumentItem.SubFolders;
                }
                return FindSameLevelItems(listBoxDocumentItem.SubFolders, parentId);
            }
            return null;
        }

        /// <summary>
        ///     添加文档
        /// </summary>
        private void AddDocument()
        {
            if (ContractDocument == null)
            {
                MessageAlert("提示", "请选择需要添加的合同");
                return;
            }
            if (
                CurrentPathItem.SubDocumentPaths.Any(
                    p => p.Name.Contains(ContractDocument.DocumentName.Trim()) && p.IsLeaf))
            {
                MessageAlert("提示", "已存在同名文件");
                return;
            }
            DocumentChildIsBusy = true;
            var newDocPath = CreateDocumentPathQuery(ContractDocument.DocumentName, true,
                ContractDocument.DocumentId.ToString(), CurrentPathItem.DocumentPathId);
            _context.BeginExecute<string>(newDocPath, p => Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                DocumentPathsView.Load(true);
                if (AddDocumetChildView.IsOpen)
                {
                    AddDocumetChildView.Close();
                }
                DocumentChildIsBusy = false;
            }), null);
        }

        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommit(object sender)
        {
            if (DocumentPathsView.IsSubmittingChanges)
            {
                return false;
            }
            return true;
        }

        #endregion

        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载合作公司数据。
        /// </summary>
        public override void LoadData()
        {
            IsBusy = true;
            if (!DocumentPathsView.AutoLoad)
            {
                DocumentPathsView.AutoLoad = true; //加载数据。
            }
            else
            {
                DocumentPathsView.Load(true);
            }
        }

        #endregion

        #region 搜索功能

        private readonly Dictionary<int, int> _searchKeys = new Dictionary<int, int>();
        private bool _isBusySearch;
        private bool _openResult;

        private ObservableCollection<ListBoxDocumentItem> _searchResults =
            new ObservableCollection<ListBoxDocumentItem>();

        private string _searchText;
        private int _tempPrarentId;

        private string _watermarkText;

        public string WatermarkText
        {
            get { return _watermarkText; }
            set
            {
                _watermarkText = value;
                RaisePropertyChanged("WatermarkText");
            }
        }

        public bool IsBusySearch
        {
            get { return _isBusySearch; }
            set
            {
                if (_isBusySearch != value)
                {
                    _isBusySearch = value;
                    RaisePropertyChanged(() => IsBusySearch);
                }
            }
        }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                RaisePropertyChanged("SearchText");
            }
        }

        public ObservableCollection<ListBoxDocumentItem> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                RaisePropertyChanged(() => SearchResults);
            }
        }

        /// <summary>
        ///     在搜索结果中返回上层文件夹
        /// </summary>
        public DelegateCommand<object> BackToParentFolderCommand { get; set; }

        public void RadWatermarkTextBoxKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var search = ServiceLocator.Current.GetInstance<SearchResultsWindow>();
                search.Header = "\"" + CurrentPathItem.Name + "\"中的搜索结果";
                search.ShowDialog();
                IsBusySearch = true;
                _openResult = true;
                _searchKeys.Clear();
                _tempPrarentId = CurrentPathItem.DocumentPathId;
                RealSearch();
            }
        }

        private void RealSearch()
        {
            var searchDocumentUri = SearchDocumentUri(CurrentPathItem.DocumentPathId, SearchText);
            _context.BeginExecute<DocumentPathDTO>(searchDocumentUri,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as PurchaseData;
                    try
                    {
                        if (context != null)
                        {
                            SearchResults.Clear();
                            SearchResults =
                                ListBoxItemHelper.TransformToListBoxItems(
                                    context.EndExecute<DocumentPathDTO>(result).ToList());
                        }
                    }
                    catch (DataServiceQueryException ex)
                    {
                        var response = ex.Response;
                        MessageAlert(response.Error.Message);
                    }
                    IsBusySearch = false;
                }), _context);
        }

        private Uri SearchDocumentUri(int documentPathId, string name)
        {
            return new Uri(string.Format("SearchDocumentPath?documentPathId={0}&name='{1}'", documentPathId, name),
                UriKind.Relative);
        }

        /// <summary>
        ///     在搜索结果中打开文件夹
        /// </summary>
        /// <param name="documentPathId"></param>
        public void OpenFolderInSearchResults(int documentPathId)
        {
            IsBusySearch = true;
            _searchKeys[documentPathId] = _tempPrarentId;
            _tempPrarentId = documentPathId;
            _pathFilterDes.Value = documentPathId;
            DocumentPathsView.Load(true);
        }

        private void GetOpenFolderSearchResults()
        {
            IsBusySearch = false;
            SearchResults.Clear();
            SearchResults = ListBoxItemHelper.TransformToListBoxItems(DocumentPathsView.ToList());
        }

        private void OnBackToParentFolder(object sender)
        {
            if (_searchKeys.ContainsKey((int) _pathFilterDes.Value))
            {
                IsBusySearch = true;
                _tempPrarentId = _searchKeys[(int) _pathFilterDes.Value];
                if (_tempPrarentId == CurrentPathItem.DocumentPathId)
                {
                    RealSearch();
                }
                else
                {
                    _pathFilterDes.Value = _tempPrarentId;
                    DocumentPathsView.Load(true);
                }
            }
        }

        #endregion

        #region 定位Listbox中的item

        private string _locateText;

        public string LoacteText
        {
            get { return _locateText; }
            set
            {
                _locateText = value;
                if (!string.IsNullOrEmpty(_locateText))
                {
                    SelectDocumentPath =
                        CurrentPathItem.SubDocumentPaths.FirstOrDefault(p => p.Name.Contains(LoacteText));
                }
                RaisePropertyChanged(() => LoacteText);
            }
        }

        #endregion
    }
}