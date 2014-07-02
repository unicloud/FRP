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
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof (ManageUserInRoleVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManageUserInRoleVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IBaseManagementService _service;

        [ImportingConstructor]
        public ManageUserInRoleVm(IBaseManagementService service)
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
            RemoveRoleCommand = new DelegateCommand<object>(OnRemoveRole, CanRemoveRole);
            //创建并注册CollectionView
            Users = _service.CreateCollection(_context.Users, o => o.UserRoles);
            Users.PageSize = 8;
            _service.RegisterCollectionView(Users);
            FunctionItems = _service.CreateCollection(_context.FunctionItems);
            FunctionItems.LoadedData += (o, e) => FunctionItems.ToList().ForEach(GenerateFunctionItemStructure);
            Roles = _service.CreateCollection(_context.Roles, o => o.RoleFunctions);
            Roles.PageSize = 8;
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
            else
                Roles.Load(true);
        }

        #region 用户

        private UserDTO _user;
        public QueryableDataServiceCollectionView<UserDTO> Users { get; set; }

        public UserDTO User
        {
            get { return _user; }
            set
            {
                _user = value;
                if (_user != null)
                {
                    var tempUserRoles = new ObservableCollection<RoleDTO>();
                    _user.UserRoles.ToList()
                        .ForEach(p => tempUserRoles.Add(Roles.FirstOrDefault(t => t.Id == p.RoleId)));
                    UserRoles = tempUserRoles;
                }
                RaisePropertyChanged(() => User);
            }
        }

        #endregion

        #region 用户功能

        /// <summary>
        ///     选中的角色
        /// </summary>
        private RoleDTO _userRole;

        private ObservableCollection<RoleDTO> _userRoles;

        public ObservableCollection<RoleDTO> UserRoles
        {
            get { return _userRoles; }
            set
            {
                _userRoles = value;
                RaisePropertyChanged(() => UserRoles);
            }
        }

        public RoleDTO UserRole
        {
            get { return _userRole; }
            set
            {
                _userRole = value;
                if (_userRole != null)
                {
                    _applications = FunctionItems.Where(p => p.ParentItemId == null).ToList();
                    RoleCommonMethod.SelectFunctionItems(_userRole, _applications);
                    DisplayFunctionItems = _applications;
                }
                RaisePropertyChanged(() => UserRole);
            }
        }

        #endregion

        #region 功能菜单

        private ObservableCollection<FunctionItemDTO> _functionItemStructures =
            new ObservableCollection<FunctionItemDTO>();

        public QueryableDataServiceCollectionView<FunctionItemDTO> FunctionItems { get; set; }

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

        /// <summary>
        ///     选中的角色
        /// </summary>
        private RoleDTO _role;

        public QueryableDataServiceCollectionView<RoleDTO> Roles { get; set; }

        public RoleDTO Role
        {
            get { return _role; }
            set
            {
                _role = value;
                if (_role != null)
                {
                    _applications = FunctionItems.Where(p => p.ParentItemId == null).ToList();
                    RoleCommonMethod.SelectFunctionItems(_role, _applications);
                    DisplayFunctionItems = _applications;
                }
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

        #region 添加用户角色

        public void AddUserRole()
        {
            if (User != null && Role != null)
            {
                if (User.UserRoles.All(p => p.RoleId != Role.Id))
                {
                    var userRole = new UserRoleDTO
                    {
                        Id = RandomHelper.Next(),
                        UserId = User.Id,
                        RoleId = Role.Id
                    };
                    User.UserRoles.Add(userRole);
                    UserRole = new RoleDTO
                    {
                        Name = Role.Name,
                        RoleFunctions = Role.RoleFunctions
                    };
                    var tempUserRoles = UserRoles ?? new ObservableCollection<RoleDTO>();
                    tempUserRoles.Add(UserRole);
                    UserRoles = tempUserRoles;
                }
                else
                {
                    MessageAlert("当前用户已有该角色！");
                }
            }
        }

        #endregion

        #region 删除角色

        public DelegateCommand<object> RemoveRoleCommand { get; set; }

        protected void OnRemoveRole(object obj)
        {
            if (UserRole == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                var userRole = User.UserRoles.FirstOrDefault(p => p.RoleId == UserRole.Id);
                User.UserRoles.Remove(userRole);
                UserRoles.Remove(UserRole);
                UserRole = UserRoles.FirstOrDefault();
            });
        }

        protected bool CanRemoveRole(object obj)
        {
            return true;
        }

        #endregion

        //#region 筛选角色功能
        //private void SelectFunctionItems(RoleDTO role, List<FunctionItemDTO> functionItems)
        //{
        //    for (int i = functionItems.Count - 1; i >= 0; i--)
        //    {
        //        var temp = functionItems[i];
        //        if (role.RoleFunctions.All(p => p.FunctionItemId != temp.Id))
        //        {
        //            functionItems.Remove(temp);
        //            continue;
        //        }
        //        SelectFunctionItems(role, temp.SubFunctionItems);
        //    }
        //}

        //private void SelectFunctionItems(RoleDTO role, DataServiceCollection<FunctionItemDTO> functionItems)
        //{
        //    for (int i = functionItems.Count - 1; i >= 0; i--)
        //    {
        //        var temp = functionItems[i];
        //        if (role.RoleFunctions.All(p => p.FunctionItemId != temp.Id))
        //        {
        //            functionItems.Remove(temp);
        //            continue;
        //        }
        //        SelectFunctionItems(role, temp.SubFunctionItems);
        //    }
        //}
        //#endregion

        #endregion
    }
}