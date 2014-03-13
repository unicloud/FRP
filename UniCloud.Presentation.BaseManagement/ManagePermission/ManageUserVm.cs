#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/11 14:27:53
// 文件名：ManageUserVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/11 14:27:53
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageUserVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageUserVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IRegionManager _regionManager;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageUserVm(IRegionManager regionManager, IBaseManagementService service)
            : base(service)
        {
            _regionManager = regionManager;
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
            Users = _service.CreateCollection(_context.Users);
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
            if (!Users.AutoLoad)
                Users.AutoLoad = true;
        }

        #region User

        /// <summary>
        ///     User集合
        /// </summary>
        public QueryableDataServiceCollectionView<UserDTO> Users { get; set; }

        #endregion



        #endregion

        #endregion

        #region 操作


        #region 重载操作

        #endregion

        #endregion
    }
}
