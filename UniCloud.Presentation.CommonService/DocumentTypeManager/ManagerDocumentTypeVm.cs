#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 10:38:23
// 文件名：ManagerDocumentTypeVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 10:38:23
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.CommonService.DocumentTypeManager
{
    [Export(typeof (ManagerDocumentTypeVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManagerDocumentTypeVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly CommonServiceData _context;
        private readonly ICommonService _service;

        [ImportingConstructor]
        public ManagerDocumentTypeVm(ICommonService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            //创建并注册CollectionView
            DocumentTypes = _service.CreateCollection(_context.DocumentTypes);
            _service.RegisterCollectionView(DocumentTypes);
            DocumentTypes.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
                {
                    var newItem = DocumentTypes.CurrentAddItem as DocumentTypeDTO;
                    if (newItem != null)
                    {
                        newItem.DocumentTypeId = RandomHelper.Next();
                    }
                }
                else if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectDocumentType = !DocumentTypes.HasChanges;
                }
            };
        }

        #endregion

        #region 数据

        #region 公共属性

        #endregion

        #region 加载数据

        /// <summary>
        ///     加载数据方法
        ///     <remarks>
        ///         导航到此页面时调用。
        ///         可在此处将CollectionView的AutoLoad属性设为True，以实现数据的自动加载。
        ///     </remarks>
        /// </summary>
        public override void LoadData()
        {
            //// 将CollectionView的AutoLoad属性设为True
            if (!DocumentTypes.AutoLoad)
                DocumentTypes.AutoLoad = true;
            DocumentTypes.Load(true);
        }

        #region 文档类型

        private bool _canSelectDocumentType = true;
        private DocumentTypeDTO _documentType;

        /// <summary>
        ///     文档类型集合
        /// </summary>
        public QueryableDataServiceCollectionView<DocumentTypeDTO> DocumentTypes { get; set; }

        /// <summary>
        ///     选中的文档类型
        /// </summary>
        public DocumentTypeDTO DocumentType
        {
            get { return _documentType; }
            set
            {
                if (_documentType != value)
                {
                    _documentType = value;
                    RaisePropertyChanged(() => DocumentType);
                }
            }
        }

        //用户能否选择

        public bool CanSelectDocumentType
        {
            get { return _canSelectDocumentType; }
            set
            {
                if (_canSelectDocumentType != value)
                {
                    _canSelectDocumentType = value;
                    RaisePropertyChanged(() => CanSelectDocumentType);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #endregion

        #endregion
    }
}