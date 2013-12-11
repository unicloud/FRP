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
using System.Collections.Generic;
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
            InitialDocumentPath(); //初始化文档路径

            InitialCommad(); //初始化命令
        }

        #region 加载FolderDocument相关信息

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
            DocumentPathsView.FilterDescriptors.Add(_pathFilterDes);
            DocumentPathsView.LoadedData += (sender, e) =>
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

        private DocumentPathDTO _currentPathItem;

        private DocumentPathDTO _rootPath;

        private SubDocumentPathDTO _selDocumentPath;

        /// <summary>
        ///     RadBreadcrumb控件中当前选中ListBox中的当前路径
        /// </summary>
        public DocumentPathDTO CurrentPathItem
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
        public DocumentPathDTO RootPath
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
        public SubDocumentPathDTO SelDocumentPath
        {
            get { return _selDocumentPath; }
            set
            {
                if (_selDocumentPath != value)
                {
                    _selDocumentPath = value;
                    RaisePropertyChanged(() => SelDocumentPath);
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
            if (DocumentPathsView.Any(p => p.ParentId == null))
            {
                //获取顶层文件夹文档          
                RootPath = DocumentPathsView
                    .FirstOrDefault(p => p.ParentId == null);
                return;
            }
            //双击打开文件夹 
            if (_loadType == "DoubleClick")
            {
                if (SelDocumentPath != null)
                {
                    //var childDocuments = DocumentPathsView
                    //    .Where(p => p.ParentId == SelDocumentPath.DocumentPathId);
                    //ListBoxItemHelper.TransformToSubListBoxItem(SelDocumentPath, childDocuments);
                    //CurrentPathItem = SelDocumentPath;
                }
            }
            else //通过搜索框打开文件夹
            {
                if (CurrentPathItem != null)
                {
                    //var childDocuments = DocumentPathsView
                    //  .Where(p => p.ParentId == _currentPathItem.DocumentPathId);
                    //ListBoxItemHelper.TransformToSubListBoxItem(CurrentPathItem, childDocuments);
                    //RaisePropertyChanged(()=>CurrentPathItem);
                }
            }
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
                //LoadSubFolderDocuemnt(SelDocumentPath);
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
        private void LoadSubFolderDocuemnt(DocumentPathDTO currentDocumentPath)
        {
            if (_pathFilterDes.Value != null && currentDocumentPath != null &&
                int.Parse(_pathFilterDes.Value.ToString()) == currentDocumentPath.DocumentPathId)
            {
                GetListBoxDocumentItem(); //加载子项
                return;
            }
            if (currentDocumentPath != null && currentDocumentPath.ParentId != null)
            {
                _pathFilterDes.Value = currentDocumentPath.DocumentPathId;
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
            //SelDocumentPath = selItem;
            //if (!SelDocumentPath.IsLeaf)
            //{
            //    _loadType = "DoubleClick"; //双击打开文件夹形式
            //    LoadSubFolderDocuemnt(SelDocumentPath);
            //    return;
            //}
            OpenDocument();
        }

        private bool CanOpen(object sender)
        {
            return true;
        }

        private void InitialCommad()
        {
            OpenCommand = new DelegateCommand<object>(OnOPen, CanOpen);
        }

        #endregion

        #region 重载基类服务

        /// <summary>
        ///     加载合作公司数据。
        /// </summary>
        public override void LoadData()
        {
            DocumentPathsView.AutoLoad = true; //加载数据。
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