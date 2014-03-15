#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/11 14:52:18
// 文件名：ManageUserInRoleVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/11 14:52:18
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageUserInRoleVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageUserInRoleVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IRegionManager _regionManager;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageUserInRoleVm(IRegionManager regionManager, IBaseManagementService service)
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
            FunctionItems = _service.CreateCollection(_context.FunctionItems);
            FunctionItems.LoadedData += (o, e) =>
            {
                FunctionItems.ToList().ForEach(GenerateFunctionItemStructure);
            };
            Roles = _service.CreateCollection(_context.Roles, o => o.RoleFunctions);
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
            if (!FunctionItems.AutoLoad)
                FunctionItems.AutoLoad = true;
            if (!Roles.AutoLoad)
                Roles.AutoLoad = true;
        }

        #region User
        public QueryableDataServiceCollectionView<UserDTO> Users { get; set; }

        private UserDTO _user;
        public UserDTO User
        {
            get { return _user; }
            set
            {
                _user = value;
                RaisePropertyChanged(() => User);
            }
        }
        #endregion

        #region 功能菜单
        public QueryableDataServiceCollectionView<FunctionItemDTO> FunctionItems { get; set; }

        private ObservableCollection<FunctionItemDTO> _functionItemStructures = new ObservableCollection<FunctionItemDTO>();
        public ObservableCollection<FunctionItemDTO> FunctionItemStructures
        {
            get { return _functionItemStructures; }
            set
            {
                _functionItemStructures = value;
                RaisePropertyChanged(() => FunctionItemStructures);
            }
        }
        #endregion

        #region 应用集合
        private List<FunctionItemDTO> _applications;

        private List<FunctionItemDTO> _displayFunctionItems;
        public List<FunctionItemDTO> DisplayFunctionItems
        {
            get { return _displayFunctionItems; }
            set
            {
                _displayFunctionItems = value;
                RaisePropertyChanged(() => DisplayFunctionItems);
            }
        }
        #endregion

        #region 角色
        public QueryableDataServiceCollectionView<RoleDTO> Roles { get; set; }

        /// <summary>
        /// 选中的角色
        /// </summary>
        private RoleDTO _role;
        public RoleDTO Role
        {
            get { return _role; }
            set
            {
                _role = value;
                _applications = FunctionItems.Where(p => p.ParentItemId == null).ToList();
                SelectFunctionItems(_applications);
                DisplayFunctionItems = _applications;
                RaisePropertyChanged(() => Role);
            }
        }
        #endregion
        #endregion

        #endregion

        #region 操作
        #region 重组成有层次结构的菜单
        private void GenerateFunctionItemStructure(FunctionItemDTO functionItem)
        {
            var temp = FunctionItems.Where(p => p.ParentItemId == functionItem.Id).ToList().OrderBy(p => p.Sort);
            functionItem.SubFunctionItems.Load(temp);
            foreach (var subItem in functionItem.SubFunctionItems)
            {
                GenerateFunctionItemStructure(subItem);
            }
        }
        #endregion

        #region 筛选角色功能
        private void SelectFunctionItems(List<FunctionItemDTO> functionItems)
        {
            if (Role != null)
            {
                for (int i = functionItems.Count - 1; i >= 0; i--)
                {
                    var temp = functionItems[i];
                    if (Role.RoleFunctions.All(p => p.FunctionItemId != temp.Id))
                    {
                        functionItems.Remove(temp);
                        continue;
                    }
                    SelectFunctionItems(temp.SubFunctionItems.ToList());
                }
            }
        }
        #endregion
        #endregion
    }
}
