#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：15:35
// 方案：FRP
// 项目：BaseManagement
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.Composition;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof (ManageUserVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageUserVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageUserVM(IBaseManagementService service)
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
            InitializeViewUserDTO();
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
        }

        #region 用户

        private UserDTO _selUserDTO;

        /// <summary>
        ///     用户集合
        /// </summary>
        public QueryableDataServiceCollectionView<UserDTO> ViewUserDTO { get; set; }

        /// <summary>
        ///     选中的用户
        /// </summary>
        public UserDTO SelUserDTO
        {
            get { return _selUserDTO; }
            set
            {
                if (_selUserDTO == value) return;
                _selUserDTO = value;
                RaisePropertyChanged(() => SelUserDTO);
                // 刷新按钮状态
                RefreshCommandState();
            }
        }

        /// <summary>
        ///     初始化用户集合
        /// </summary>
        private void InitializeViewUserDTO()
        {
            ViewUserDTO = _service.CreateCollection(_context.Users);
            ViewUserDTO.PageSize = 20;
            _service.RegisterCollectionView(ViewUserDTO);
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #region 保存成功后执行

        protected override void OnSaveSuccess(object sender)
        {
        }

        #endregion

        #region 撤销成功后执行

        protected override void OnAbortExecuted(object sender)
        {
        }

        #endregion

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
        }

        #endregion

        #endregion

        #endregion
    }
}