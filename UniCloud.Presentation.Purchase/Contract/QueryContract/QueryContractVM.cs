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
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.CommonService.DocumentExtension;
using UniCloud.Presentation.Service.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Contract
{
    [Export(typeof (QueryContractVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryContractVM : ViewModelBase
    {
        private readonly IRegionManager _regionManager;
        private CommonServiceData _context;
        private string _loadType; //加载子项文件夹方式方式，1、DoubleClick 双击,2、SearchText 搜索框

        [ImportingConstructor]
        public QueryContractVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitialFolderDocument();//初始化文件夹文档
            InitialCommad(); //初始化命令
        }

        #region 加载FolderDocument相关信息

        private FilterDescriptor _folderFilterDes;

        /// <summary>
        ///     文件夹下文件及文档View
        /// </summary>
        public QueryableDataServiceCollectionView<FolderDocumentDTO> FolderDocumentsView { get; set; }

        /// <summary>
        ///     初始化文件夹下文件及文档
        /// </summary>
        private void InitialFolderDocument()
        {
            FolderDocumentsView = Service.CreateCollection(_context.FolderDocuments.Expand(p => p.SubFolders));
            _folderFilterDes = new FilterDescriptor("ParentFolderId", FilterOperator.IsEqualTo, null);
            FolderDocumentsView.FilterDescriptors.Add(_folderFilterDes);
            FolderDocumentsView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    GetListBoxDocumentItem();
                };
        }

        #endregion

        #region 属性

        private ListBoxDocumentItem _currentPathItem;

        private ListBoxDocumentItem _rootListBoxDocumentItem;

        private ListBoxDocumentItem _selListBoxDocumentItem;

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
                    if (value != null)
                    {
                        _loadType = "SearchText";
                        //加载子项文件夹文档，即模仿打开文件夹，加载文件夹里的子文件夹与文件
                        LoadSubFolderDocuemnt(value);
                    }
                    RaisePropertyChanged(() => CurrentPathItem);
                }
            }
        }

        /// <summary>
        ///     根选中项
        /// </summary>
        public ListBoxDocumentItem RootListBoxDocumentItem
        {
            get { return _rootListBoxDocumentItem; }
            set
            {
                if (_rootListBoxDocumentItem != value)
                {
                    _rootListBoxDocumentItem = value;
                    RaisePropertyChanged(() => RootListBoxDocumentItem);
                }
            }
        }

        /// <summary>
        ///     Lisbox控件中，选择的当前项
        /// </summary>
        public ListBoxDocumentItem SelListBoxDocumentItem
        {
            get { return _selListBoxDocumentItem; }
            set
            {
                if (_selListBoxDocumentItem != value)
                {
                    _selListBoxDocumentItem = value;
                    RaisePropertyChanged(() => RootListBoxDocumentItem);
                }
            }
        }

        #endregion

        #region 方法

        /// <summary>
        ///     获取ListBoxItem
        /// </summary>
        private void GetListBoxDocumentItem()
        {
            if (FolderDocumentsView.Any(p => p.ParentFolderId == null))
            {
                //获取顶层文件夹文档
                var rootFolderDocument = FolderDocumentsView
                    .FirstOrDefault(p => p.ParentFolderId == null);
                RootListBoxDocumentItem = ListBoxItemHelper.TransformToListBoxItem(null, rootFolderDocument);
                return;
            }
            //双击打开文件夹
            if (_loadType == "DoubleClick")
            {
                if (SelListBoxDocumentItem != null)
                {
                    var currentFolderDocument = FolderDocumentsView
                        .FirstOrDefault(p => p.ParentFolderId == SelListBoxDocumentItem.ListBoxItemId);
                    _currentPathItem = ListBoxItemHelper.TransformToListBoxItem(SelListBoxDocumentItem,
                                                                                currentFolderDocument);
                    RaisePropertyChanged(() => CurrentPathItem);
                }
            }
            else //通过搜索框打开文件夹
            {   
                if (CurrentPathItem != null)
                {
                    var currentFolderDocument = FolderDocumentsView
                        .FirstOrDefault(p => p.ParentFolderId == CurrentPathItem.ListBoxItemId);
                    ListBoxItemHelper.TransformToListBoxItem(CurrentPathItem,
                                                             currentFolderDocument);
                    RaisePropertyChanged(() => CurrentPathItem);
                }
            }
        }

        /// <summary>
        ///     双击ListBoxItem
        /// </summary>
        public void ListBoxDoubleClick()
        {
            if (SelListBoxDocumentItem == null)
                return;
            if (SelListBoxDocumentItem.IsFolder)
            {
                _loadType = "DoubleClick"; //双击打开文件夹形式
                LoadSubFolderDocuemnt(SelListBoxDocumentItem);
                return;
            }
            OpenDocument();
        }

        /// <summary>
        ///     打开文件
        /// </summary>
        private void OpenDocument()
        {
        }

        /// <summary>
        ///     加载子项文件与文件夹
        /// </summary>
        private void LoadSubFolderDocuemnt(ListBoxDocumentItem listBoxDocument)
        {
            if (_folderFilterDes.Value != null && listBoxDocument != null &&
                Guid.Parse(_folderFilterDes.Value.ToString()) == listBoxDocument.ListBoxItemId)
            {
                GetListBoxDocumentItem(); //加载子项
                return;
            }
            if (listBoxDocument != null && listBoxDocument.ParentFolderId != null)
            {
                _folderFilterDes.Value = listBoxDocument.ListBoxItemId;
            }
        }

        #endregion

        #region 命令

        public DelegateCommand<object> OpenCommand { get; private set; }

        private void OnOPen(object sender)
        {
            var selItem = sender as ListBoxDocumentItem;
            if (selItem == null)
                return;
            SelListBoxDocumentItem = selItem;
            if (SelListBoxDocumentItem.IsFolder)
            {
                _loadType = "DoubleClick"; //双击打开文件夹形式
                LoadSubFolderDocuemnt(SelListBoxDocumentItem);
                return;
            }
            OpenDocument();
        }

        private bool CanOpen(object sender)
        {
            return true;
        }

        private void InitialCommad()
        {
            OpenCommand=new DelegateCommand<object>(OnOPen,CanOpen);
        }
        
        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载合作公司数据。
        /// </summary>
        public override void LoadData()
        {
            FolderDocumentsView.AutoLoad = true; //加载数据。
        }

        /// <summary>
        ///     创建服务。
        /// </summary>
        /// <returns></returns>
        protected override IService CreateService()
        {
            _context = new CommonServiceData(AgentHelper.CommonServiceUri);
            return new PurchaseService(_context);
        }

        #endregion
    }
}