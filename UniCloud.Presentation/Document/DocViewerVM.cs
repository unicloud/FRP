#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/10，13:59
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService;
using UniCloud.Presentation.Service.CommonService.Common;

#endregion

namespace UniCloud.Presentation.Document
{
    [Export(typeof (DocViewerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class DocViewerVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly CommonServiceData _context;
        private readonly IRegionManager _regionManager;
        private readonly ICommonService _service;

        [ImportingConstructor]
        public DocViewerVM(IRegionManager regionManager, ICommonService service) : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;

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
            // 将CollectionView的AutoLoad属性设为True，例如：
            // Orders.AutoLoad = true;
        }

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
        }

        #endregion

        #endregion

        #endregion
    }
}