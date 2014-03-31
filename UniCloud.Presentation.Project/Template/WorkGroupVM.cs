#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/09，15:13
// 方案：FRP
// 项目：Project
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
using UniCloud.Presentation.Service.Project;
using UniCloud.Presentation.Service.Project.Project;

#endregion

namespace UniCloud.Presentation.Project.Template
{
    [Export(typeof (WorkGroupVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class WorkGroupVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly ProjectData _context;
        private readonly IProjectService _service;

        [ImportingConstructor]
        public WorkGroupVM(IProjectService service) : base(service)
        {
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
            InitializeViewUserDTO();
            InitializeViewWorkGroupDTO();
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
            if (!ViewUserDTO.AutoLoad) ViewUserDTO.AutoLoad = true;
            else ViewUserDTO.Load(true);
            if (!ViewWorkGroupDTO.AutoLoad) ViewWorkGroupDTO.AutoLoad = true;
            else ViewWorkGroupDTO.Load(true);
        }

        #region 工作组

        private WorkGroupDTO _selWorkGroupDTO;

        /// <summary>
        ///     工作组集合
        /// </summary>
        public QueryableDataServiceCollectionView<WorkGroupDTO> ViewWorkGroupDTO { get; set; }

        /// <summary>
        ///     选中的工作组
        /// </summary>
        public WorkGroupDTO SelWorkGroupDTO
        {
            get { return _selWorkGroupDTO; }
            private set
            {
                if (_selWorkGroupDTO != value)
                {
                    _selWorkGroupDTO = value;
                    RaisePropertyChanged(() => SelWorkGroupDTO);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     初始化工作组集合
        /// </summary>
        private void InitializeViewWorkGroupDTO()
        {
            ViewWorkGroupDTO = _service.CreateCollection(_context.WorkGroups);
            _service.RegisterCollectionView(ViewWorkGroupDTO);
        }

        #endregion

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
            private set
            {
                if (_selUserDTO != value)
                {
                    _selUserDTO = value;
                    RaisePropertyChanged(() => SelUserDTO);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     初始化用户集合
        /// </summary>
        private void InitializeViewUserDTO()
        {
            ViewUserDTO = _service.CreateCollection(_context.Users);
            _service.RegisterCollectionView(ViewUserDTO);
        }

        #endregion

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