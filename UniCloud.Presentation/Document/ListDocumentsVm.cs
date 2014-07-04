﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/21 9:32:48
// 文件名：ListDocumentsVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/21 9:32:48
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof (ListDocumentsVm))]
    public class ListDocumentsVm : ViewModelBase
    {
        #region 声明、初始化

        private readonly CommonServiceData _context;
        private readonly ICommonService _service;

        [ImportingConstructor]
        public ListDocumentsVm(ICommonService service)
            : base(service)
        {
            _service = service;
            _context = service.Context;

            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处访问创建并注册CollectionView集合的方法。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            Documents = _service.CreateCollection(_context.Documents);
            Documents.PageSize = 20;
            DocumentTypes = _service.CreateCollection(_context.DocumentTypes);
        }

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     文档集合
        /// </summary>
        public QueryableDataServiceCollectionView<DocumentDTO> Documents { get; set; }

        /// <summary>
        ///     文档类型
        /// </summary>
        public QueryableDataServiceCollectionView<DocumentTypeDTO> DocumentTypes { get; set; }

        #endregion

        #region 加载数据

        public override void LoadData()
        {
            if (!Documents.AutoLoad) Documents.AutoLoad = true;
            else Documents.Load(true);
            if (!DocumentTypes.AutoLoad) DocumentTypes.AutoLoad = true;
            else DocumentTypes.Load(true);
        }

        public void InitData(Action<DocumentDTO> callback)
        {
            windowClosed = callback;
            DocumentDoubleClickHelper.windowClosed = windowClosed;
            DocumentTypes.Load(true);
            Documents.Load(true);
        }

        #endregion

        #endregion

        #region 操作

        #endregion
    }
}