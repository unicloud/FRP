#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/11 15:23:19
// 文件名：ManageFunctionsInRoleVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/11 15:23:19
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;

#endregion

namespace UniCloud.Presentation.BaseManagement.ManagePermission
{
    [Export(typeof(ManageFunctionsInRoleVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageFunctionsInRoleVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly BaseManagementData _context;
        private readonly IRegionManager _regionManager;
        private readonly IBaseManagementService _service;
        private bool enableTreeViewCheck = true;
        [Import]
        public ManageFunctionsInRole CurrentManageFunctionsInRole;
        [ImportingConstructor]
        public ManageFunctionsInRoleVm(IRegionManager regionManager, IBaseManagementService service)
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
            AddRoleCommand = new DelegateCommand<object>(OnAddRole, CanAddRole);
            RemoveRoleCommand = new DelegateCommand<object>(OnRemoveRole, CanRemoveRole);
            CheckedCommand = new DelegateCommand<object>(FunctionItemChecked);
            UncheckedCommand = new DelegateCommand<object>(FunctionItemUnchecked);
            //创建并注册CollectionView
            FunctionItems = _service.CreateCollection(_context.FunctionItems);
            FunctionItems.LoadedData += (o, e) =>
                                        {
                                            FunctionItems.ToList().ForEach(GenerateFunctionItemStructure);
                                            Applications = FunctionItems.Where(p => p.ParentItemId == null).ToList();
                                        };
            Roles = _service.CreateCollection(_context.Roles, o => o.RoleFunctions);
            _service.RegisterCollectionView(Roles);
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
            if (!FunctionItems.AutoLoad)
            {
                FunctionItems.AutoLoad = true;
            }
            if (!Roles.AutoLoad)
            {
                Roles.AutoLoad = true;
            }
            Roles.Load(true);
        }
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
                SetCheckedState();
                RaisePropertyChanged(() => Role);
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
        /// <summary>
        /// 应用集合
        /// </summary>
        private List<FunctionItemDTO> _applications;
        public List<FunctionItemDTO> Applications
        {
            get { return _applications; }
            set
            {
                _applications = value;
                RaisePropertyChanged(() => Applications);
            }
        }

        /// <summary>
        /// 选中的应用
        /// </summary>
        private FunctionItemDTO _application;
        public FunctionItemDTO Application
        {
            get { return _application; }
            set
            {
                _application = value;
                if (_application != null)
                {
                    FunctionItemStructures.Clear();
                    FunctionItemStructures.Add(_application);
                    SetCheckedState();
                }
                RaisePropertyChanged(() => Application);
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

        #region 新增角色
        public DelegateCommand<object> AddRoleCommand { get; set; }

        protected void OnAddRole(object obj)
        {
            Role = new RoleDTO
            {
                Id = RandomHelper.Next(),
            };
            Roles.AddNew(Role);
        }

        protected bool CanAddRole(object obj)
        {
            return true;
        }
        #endregion

        #region 删除角色
        public DelegateCommand<object> RemoveRoleCommand { get; set; }

        protected void OnRemoveRole(object obj)
        {
            if (Role == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                Roles.Remove(Role);
            });
        }

        protected bool CanRemoveRole(object obj)
        {
            return true;
        }
        #endregion

        #region 功能选中

        public DelegateCommand<object> CheckedCommand { get; set; }
        private void FunctionItemChecked(object e)
        {
            if (!enableTreeViewCheck)
            {
                return;
            }
            //var checkedItems = GenerateCheckedItems(FunctionItemStructures);
            //if (Role != null)
            //{
            //    foreach (var checkedItem in checkedItems)
            //    {
            //        var functionItem = checkedItem;
            //        if (functionItem != null && Role.RoleFunctions.All(p => p.FunctionItemId != functionItem.Id))
            //        {
            //            //if (functionItem.ParentItemId != null)
            //            //{
            //            //    var parent = FunctionItems.FirstOrDefault(p => p.Id == functionItem.ParentItemId);
            //            //    FindParentCheckedItem(parent);
            //            //}
            //            var roleFunction = new RoleFunctionDTO
            //                               {
            //                                   Id = RandomHelper.Next(),
            //                                   RoleId = Role.Id,
            //                                   FunctionItemId = functionItem.Id
            //                               };
            //            Role.RoleFunctions.Add(roleFunction);
            //        }
            //    }
            //}
            if (Role != null)
                CurrentManageFunctionsInRole.FunctionRole.CheckedItems.ToList().ForEach(p =>
                                                                                        {
                                                                                            var functionItem = p as FunctionItemDTO;
                                                                                            if (functionItem != null && Role.RoleFunctions.All(t => t.FunctionItemId != functionItem.Id))
                                                                                            {
                                                                                                var roleFunction = new RoleFunctionDTO
                                                                                                                   {
                                                                                                                       Id = RandomHelper.Next(),
                                                                                                                       RoleId = Role.Id,
                                                                                                                       FunctionItemId = functionItem.Id
                                                                                                                   };
                                                                                                Role.RoleFunctions.Add(roleFunction);
                                                                                            }
                                                                                        });
        }

        #endregion

        #region 功能反选处理
        public DelegateCommand<object> UncheckedCommand { get; set; }
        private void FunctionItemUnchecked(object e)
        {
            if (!enableTreeViewCheck)
            {
                return;
            }
            IEnumerable<FunctionItemDTO> tempFunctionItems = GenerateUncheckedItems(FunctionItemStructures.FirstOrDefault(), FunctionItemStructures.FirstOrDefault().SubFunctionItems);

            if (Role != null)
            {
                tempFunctionItems.ToList().ForEach(p =>
                {
                    if (Role.RoleFunctions.Any(t => t.FunctionItemId == p.Id))
                    {
                        var temp = Role.RoleFunctions.FirstOrDefault(t => t.FunctionItemId == p.Id);
                        Role.RoleFunctions.Remove(temp);
                    }
                });
            }
        }

        private IEnumerable<FunctionItemDTO> GenerateUncheckedItems(FunctionItemDTO parentItem, IEnumerable<FunctionItemDTO> subItems)
        {
            var items = new List<FunctionItemDTO>();
            foreach (var sourceItem in subItems)
            {
                if (!sourceItem.IsChecked)
                {
                    items.Add(sourceItem);
                    items.Add(parentItem);
                }
                GenerateUncheckedItems(sourceItem, sourceItem.SubFunctionItems.AsEnumerable()).ToList().ForEach(items.Add);
            }
            return items;
        }
        #endregion

        #region 设置TreeView选中状态
        private void SetCheckedState()
        {
            enableTreeViewCheck = false;
            if (Role == null || Role.RoleFunctions.Count == 0)
            {
                FunctionItemStructures.ToList().ForEach(ClearCheckedState);
            }
            else
            {
                FunctionItemStructures.ToList().ForEach(ClearCheckedState);
                FunctionItemStructures.ToList().ForEach(p => SetCheckedState(Role.RoleFunctions, p));
            }
            enableTreeViewCheck = true;
        }

        private void SetCheckedState(IEnumerable<RoleFunctionDTO> sourceItems, FunctionItemDTO item)
        {
            item.IsChecked = sourceItems.Any(p => p.FunctionItemId == item.Id);
            item.SubFunctionItems.ToList().ForEach(p => SetCheckedState(Role.RoleFunctions, p));
        }

        private void ClearCheckedState(FunctionItemDTO item)
        {
            item.IsChecked = false;
            item.SubFunctionItems.ToList().ForEach(ClearCheckedState);
        }
        #endregion
        #endregion
    }
}
