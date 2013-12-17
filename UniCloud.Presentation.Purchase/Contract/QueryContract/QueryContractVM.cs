#region 版本信息

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
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.DocumentExtension;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof (QueryContractVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryContractVM : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        [Import] public DocumentViewer DocumentView;
        private PurchaseData _context; //域上下文
        private string _loadType; //加载子项文件夹方式方式，1、DoubleClick 双击,2、SearchText 搜索框

        [ImportingConstructor]
        public QueryContractVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitialDocumentPath(); //初始化文档路径
            InitialCommad(); //初始化命令
            InitialAddFolder();
            InitialOrderDocument();
        }

        #region 加载DocumentPathDTO相关信息

        private FilterDescriptor _pathFilterDes;

        /// <summary>
        ///     文档路劲View
        /// </summary>
        public QueryableDataServiceCollectionView<DocumentPathDTO> DocumentPathsView { get; set; }

        /// <summary>
        ///     初始化文档路径
        /// </summary>
        private void InitialDocumentPath()
        {
            DocumentPathsView = Service.CreateCollection(_context.DocumentPaths.Expand(p => p.SubDocumentPaths));
            _pathFilterDes = new FilterDescriptor("ParentId", FilterOperator.IsEqualTo, null);
            var pathType = new FilterDescriptor("PathSource", FilterOperator.IsEqualTo, 0); //路径类型
            DocumentPathsView.FilterDescriptors.Add(_pathFilterDes);
            DocumentPathsView.FilterDescriptors.Add(pathType);
            DocumentPathsView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
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

        #endregion

        #region 属性

        private readonly ObservableCollection<ListBoxDocumentItem> _listBoxDocumentItems =
            new ObservableCollection<ListBoxDocumentItem>();

        private ListBoxDocumentItem _currentPathItem;

        private ListBoxDocumentItem _rootPath;

        private ListBoxDocumentItem _selDocumentPath;

        /// <summary>
        ///     RadBreadcrumb控件中当前选中ListBox中的当前路径
        /// </summary>
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
                        _loadType = "SearchText";
                        //加载子项文件夹文档，即模仿打开文件夹，加载文件夹里的子文件夹与文件
                        LoadSubFolderDocuemnt(value);
                    }
                }
            }
        }

        /// <summary>
        ///     根路径
        /// </summary>
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

        /// <summary>
        ///     Lisbox控件中，选择的当前项
        /// </summary>
        public ListBoxDocumentItem SelDocumentPath
        {
            get { return _selDocumentPath; }
            set
            {
                if (_selDocumentPath != value)
                {
                    _selDocumentPath = value;
                    DelDocumentPathToolBarCommand.RaiseCanExecuteChanged();
                    OpenDocPathToolBarCommand.RaiseCanExecuteChanged();
                    RaisePropertyChanged(() => SelDocumentPath);
                }
            }
        }

        /// <summary>
        ///     TreeView绑定集合
        /// </summary>
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
            if (DocumentPathsView.Any(p => p.ParentId == null))
            {
                //获取顶层文件夹文档          
                RootPath = ListBoxItemHelper.TransformToRootListBoxItem(DocumentPathsView
                                                                            .FirstOrDefault(p => p.ParentId == null));
                //为Treeview集合
                _listBoxDocumentItems.Clear();
                _listBoxDocumentItems.Add(RootPath);
            }
            else //双击打开文件夹 
                if (_loadType == "DoubleClick")
                {
                    if (SelDocumentPath != null && !SelDocumentPath.IsLeaf)
                    {
                        //子项文档集合
                        var childDocuments = DocumentPathsView
                            .Where(p => p.ParentId == SelDocumentPath.DocumentPathId);
                        //设置当前选中项
                        _currentPathItem =
                            CurrentPathItem.SubDocumentPaths.FirstOrDefault(
                                p => p.DocumentPathId == SelDocumentPath.DocumentPathId);
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
            if (SelDocumentPath == null)
                return;
            if (!SelDocumentPath.IsLeaf)
            {
                _loadType = "DoubleClick"; //双击打开文件夹形式
                LoadSubFolderDocuemnt(SelDocumentPath);
                return;
            }
            OpenDocument(SelDocumentPath.DocumentGuid);
        }

        /// <summary>
        ///     打开文件
        /// </summary>
        private void OpenDocument(Guid? documentId)
        {
            DocumentView.Tag = null;
            if (documentId != null) DocumentView.ViewModel.InitData(true, documentId.Value, null);
            DocumentView.ShowDialog();
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
                //根据选中的父亲节点，查询子项文件
                if (currentDocumentPath.ParentId != null)
                {
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
        /// <param name="pathSource">路径类型</param>
        /// <returns>Uri实例</returns>
        private Uri CreateDocumentPathQuery(string name, bool isLeaf, string documentId, int parentId, int pathSource)
        {
            return new Uri(string.Format("AddDocPath?name='{0}'&isLeaf='{1}'" +
                                         "&documentId='{2}'&parentId={3}" +
                                         "&pathSource={4}", name, isLeaf, documentId, parentId, pathSource),
                           UriKind.Relative);
        }

        /// <summary>
        ///     删除文档路径
        /// </summary>
        /// <param name="docPathId"></param>
        /// <returns></returns>
        private Uri DelDocumentPathQuery(int docPathId)
        {
            return new Uri(string.Format("DelDocPath?docPathId={0}", docPathId),
                           UriKind.Relative);
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
        private void OnOPen(object sender)
        {
            var selItem = sender as ListBoxDocumentItem;
            if (selItem == null)
                return;
            SelDocumentPath = selItem;
            if (!SelDocumentPath.IsLeaf)
            {
                _loadType = "DoubleClick"; //双击打开文件夹形式
                LoadSubFolderDocuemnt(SelDocumentPath);
                return;
            }
            OpenDocument(SelDocumentPath.DocumentGuid);
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
            return SelDocumentPath != null;
        }

        #endregion

        #region 新建文档

        public DelegateCommand<object> CreateDocumentPathCommand { get; private set; }

        private void CreateDocumentPath(object sender)
        {
            if (sender.ToString().Equals("文件夹"))
            {
                DocumentName = string.Empty;
                AddDocumentPathChildView.ShowDialog();
            }
            else
            {
                if (!OrderDocumentView.AutoLoad)
                {
                    OrderDocumentView.AutoLoad = true;
                }
                else
                {
                    OrderDocumentView.Load(true);
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
        public DelegateCommand<object> DelDocumentPathListBoxCommand { get; private set; }

        /// <summary>
        ///     删除文档通过右击ToolBar的删除文件按钮触发的事件
        /// </summary>
        public DelegateCommand<object> DelDocumentPathToolBarCommand { get; private set; }
        /// <summary>
        ///     删除文档通过单击ToolBar的删除文件按钮触发的事件
        /// </summary>
        private void DelDocumentPathToolBar(object sender)
        {
            if (SelDocumentPath == null)
            {
                MessageAlert("提示", "请选择需要删除文件");
                return;
            }
            MessageConfirm("提示", "确定删除此文件", (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        DelDocPath();
                    }
                });
        }
        /// <summary>
        /// 删除文档通过右击ListBox的删除文件按钮触发的事件
        /// </summary>
        /// <param name="sender"></param>
        private void DelDocumentPathListBox(object sender)
        {
            if (sender == null)
            {
                MessageAlert("提示", "请选择需要删除文件");
                return;
            }
            SelDocumentPath = sender as ListBoxDocumentItem;
            MessageConfirm("提示", "确定删除此文件", (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    DelDocPath();
                }
            });
        }


        /// <summary>
        ///     删除文档路径
        /// </summary>
        private void DelDocPath()
        {
            var delDocPath = DelDocumentPathQuery(SelDocumentPath.DocumentPathId);
            _context.BeginExecute<string>(delDocPath,
                                          p =>
                                          Deployment.Current.Dispatcher.BeginInvoke(() => DocumentPathsView.Load(true)),
                                          null);
        }

        /// <summary>
        ///     删除文档通过右击ListBoxItem，选择的RadContextMenu触发的事件按钮是否可用
        /// </summary>
        private bool CanDelDocumentPathListBox(object sender)
        {
            return true;
        }

        /// <summary>
        ///     删除文档通过单击ToolBar的删除文件按钮触发的事件按钮是否可用
        /// </summary>
        private bool CanDelDocumentPathToolBar(object sender)
        {
            return SelDocumentPath != null;
        }

        #endregion

        private void InitialCommad()
        {
            OpenDocPathListBoxCommand = new DelegateCommand<object>(OnOPen, CanOpenListBox);
            OpenDocPathToolBarCommand = new DelegateCommand<object>(OnOPen, CanOpenToolBar);
            CreateDocumentPathCommand = new DelegateCommand<object>(CreateDocumentPath, CanCreateDocumentPath);
            DelDocumentPathToolBarCommand = new DelegateCommand<object>(DelDocumentPathToolBar, CanDelDocumentPathToolBar);
            DelDocumentPathListBoxCommand = new DelegateCommand<object>(DelDocumentPathListBox, CanDelDocumentPathListBox);
        }

        #endregion

        #region 子窗体相关

        private string _documentName;
        private OrderDocumentDTO _selOrderDocument; //选中的订单文档

        public AddDocumentPathChild AddDocumentPathChildView
        {
            get
            {
                return ServiceLocator.Current.
                                      GetInstance<AddDocumentPathChild>();
            }
        }

        /// <summary>
        ///     初始化添加文档子窗体
        /// </summary>
        public AddDocumetChild AddDocumetChildView
        {
            get
            {
                return ServiceLocator.Current.
                                      GetInstance<AddDocumetChild>();
            }
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
      /// 选中订单文档
      /// </summary>
        public OrderDocumentDTO SelOrderDocument
        {
            get { return _selOrderDocument; }
            set
            {
                if (_selOrderDocument != value)
                {
                    _selOrderDocument = value;
                    RaisePropertyChanged(() => SelOrderDocument);
                }
            }
        }

        private bool _documentChildIsBusy;
        /// <summary>
        /// 子窗体是否正在处理中
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
            if (sender.ToString().Equals("文件夹", StringComparison.CurrentCultureIgnoreCase))
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
            DocumentChildIsBusy = true;
            if (sender.ToString().Equals("文件夹", StringComparison.CurrentCultureIgnoreCase))
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
            if (string.IsNullOrEmpty(DocumentName))
            {
                MessageAlert("提示", "文件夹名称不能为空");
                return;
            }
            if (CurrentPathItem.SubFolders.Any(p => p.Name.Contains(DocumentName.Trim())))
            {
                MessageAlert("提示", "已存在同名文件夹");
                return;
            }
            var newDocPath = CreateDocumentPathQuery(DocumentName, false, null,
                                                     CurrentPathItem.DocumentPathId, 0);

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

        /// <summary>
        ///     添加文档
        /// </summary>
        private void AddDocument()
        {
            if (SelOrderDocument == null)
            {
                MessageAlert("提示", "请选择需要添加的合同");
                return;
            }
            if (
                CurrentPathItem.SubDocumentPaths.Any(
                    p => p.Name.Contains(SelOrderDocument.ContractName.Trim()) && p.IsLeaf))
            {
                MessageAlert("提示", "已存在同名文件");
                return;
            }
            var newDocPath = CreateDocumentPathQuery(SelOrderDocument.ContractName, true,
                                                     SelOrderDocument.ContractDocGuid.ToString(),
                                                     CurrentPathItem.DocumentPathId, 0);
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

        #region 加载OrderDocumentDTO相关信息

        /// <summary>
        ///     订单文档View
        /// </summary>
        public QueryableDataServiceCollectionView<OrderDocumentDTO> OrderDocumentView { get; set; }

        /// <summary>
        ///     初始化订单文档
        /// </summary>
        private void InitialOrderDocument()
        {
            OrderDocumentView = Service.CreateCollection(_context.OrderDocuments);
            OrderDocumentView.PageSize = 20;
            OrderDocumentView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    if (e.Entities.Cast<OrderDocumentDTO>().FirstOrDefault() != null)
                    {
                        SelOrderDocument = (e.Entities.Cast<OrderDocumentDTO>().FirstOrDefault());
                    }
                };
        }

        #endregion

        /// <summary>
        ///     初始化文件夹信息
        /// </summary>
        private void InitialAddFolder()
        {
            CommitCommand = new DelegateCommand<object>(Commit, CanCommit);
            CancelCommand = new DelegateCommand<object>(Cancel, CanCancel);
        }

        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载合作公司数据。
        /// </summary>
        public override void LoadData()
        {
            if (!DocumentPathsView.AutoLoad)
            {
                DocumentPathsView.AutoLoad = true; //加载数据。
            }
            else
            {
                DocumentPathsView.Load(true);
            }
        }

        /// <summary>
        ///     创建服务。
        /// </summary>
        /// <returns></returns>
        protected override IService CreateService()
        {
            _context = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(_context);
        }

        #endregion
    }
}