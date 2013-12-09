#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/09，22:11
// 文件名：ShellViewModel.cs
// 程序集：UniCloud.Presentation.Shell
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Telerik.Windows;
using Telerik.Windows.Controls;

#endregion

namespace UniCloud.Presentation.Shell
{
    [Export(typeof (ShellViewModel))]
    public class ShellViewModel : NotificationObject, IPartImportsSatisfiedNotification
    {
        #region 声明

        private bool _firstLoad = true;
        [Import] public IModuleManager moduleManager;
        [Import] public IRegionManager regionManager;

        #region IPartImportsSatisfiedNotification 成员

        /// <summary>
        ///     装配完成后执行的操作
        ///     在此处注册命令、事件处理程序
        /// </summary>
        public void OnImportsSatisfied()
        {
            HomeCommand = new DelegateCommand<object>(OnHome, CanHome);

            moduleManager.LoadModuleCompleted += moduleManager_LoadModuleCompleted;
            moduleManager.ModuleDownloadProgressChanged += moduleManager_ModuleDownloadProgressChanged;

            InitializeVM();
        }

        #endregion

        /// <summary>
        ///     初始化ViewModel
        /// </summary>
        private void InitializeVM()
        {
            LoadMenuItems();
        }

        #endregion

        #region 数据

        #region 应用模块

        /// <summary>
        ///     应用模块加载完成后的操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            if (_firstLoad)
            {
                _firstLoad = false;
                OnHome(null);
            }
        }

        /// <summary>
        ///     应用模块加载进程处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void moduleManager_ModuleDownloadProgressChanged(object sender,
            ModuleDownloadProgressChangedEventArgs e)
        {
        }

        #endregion

        #region 功能菜单

        #region 集合

        private MenuItemsCollection _items = new MenuItemsCollection();

        /// <summary>
        ///     功能集合
        /// </summary>
        public MenuItemsCollection Items
        {
            get { return _items; }
            private set
            {
                if (_items != value)
                {
                    _items = value;
                    RaisePropertyChanged(() => Items);
                }
            }
        }

        #endregion

        #region 加载

        private void LoadMenuItems()
        {
            #region 飞行日志

            var menu1 = new MenuItem
            {
                Text = "飞行日志",
                NavUri = "UniCloud.Presentation.Portal.Plan.PlanPortal"
            };
            _items.Add(menu1);

            #endregion

            #region 运力规划

            var menu2 = new MenuItem
            {
                Text = "运力规划",
                NavUri = "UniCloud.Presentation.Portal.Manager.ManagerPortal"
            };
            _items.Add(menu2);

            #endregion

            #region 采购合同

            var menu3 = new MenuItem
            {
                Text = "采购合同",
            };

            var menu31 = new MenuItem
            {
                Text = "管理合作公司",
            };
            var menu311 = new MenuItem
            {
                Text = "维护供应商类别",
                NavUri = "UniCloud.Presentation.Purchase.Supplier.SupplierRoleManager"
            };
            var menu312 = new MenuItem
            {
                Text = "查询供应商",
                NavUri = "UniCloud.Presentation.Purchase.Supplier.QuerySupplier"
            };
            var menu313 = new MenuItem
            {
                Text = "维护供应商可供物料",
                NavUri = "UniCloud.Presentation.Purchase.Supplier.SupplierMaterialManager"
            };
            var menu314 = new MenuItem
            {
                Text = "维护BFE承运人",
                NavUri = "UniCloud.Presentation.Purchase.Forwarder.ForwarderManager"
            };
            var menu315 = new MenuItem
            {
                Text = "维护联系人",
                NavUri = "UniCloud.Presentation.Purchase.Supplier.LinkManManager"
            };
            menu31.Items.Add(menu311);
            menu31.Items.Add(menu313);
            menu31.Items.Add(menu314);
            menu31.Items.Add(menu315);
            menu31.Items.Add(menu312);
            menu3.Items.Add(menu31);

            var menu32 = new MenuItem
            {
                Text = "管理采购合同",
            };
            var menu321 = new MenuItem
            {
                Text = "管理飞机购买合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.AircraftPurchase"
            };
            var menu322 = new MenuItem
            {
                Text = "管理飞机租赁合同",
            };
            var menu323 = new MenuItem
            {
                Text = "管理发动机购买合同",
            };
            var menu324 = new MenuItem
            {
                Text = "管理发动机租赁合同",
            };
            var menu325 = new MenuItem
            {
                Text = "管理BFE合同",
            };
            var menu326 = new MenuItem
            {
                Text = "管理发动机维修合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.EngineMaintain"
            };
            var menu327 = new MenuItem
            {
                Text = "管理APU维修合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.ApuMaintain"
            };
            var menu328 = new MenuItem
            {
                Text = "管理起落架维修合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.UndercartMaintain"
            };
            menu32.Items.Add(menu321);
            menu32.Items.Add(menu322);
            menu32.Items.Add(menu323);
            menu32.Items.Add(menu324);
            menu32.Items.Add(menu325);
            menu32.Items.Add(menu326);
            menu32.Items.Add(menu327);
            menu32.Items.Add(menu328);
            menu3.Items.Add(menu32);

            var menu33 = new MenuItem
            {
                Text = "管理接机",
            };
            var menu331 = new MenuItem
            {
                Text = "匹配计划飞机",
                NavUri = "UniCloud.Presentation.Purchase.Reception.MatchingPlanAircraftManager"
            };
            var menu332 = new MenuItem
            {
                Text = "维护租赁飞机交付项目",
                NavUri = "UniCloud.Presentation.Purchase.Reception.AircraftLeaseReceptionManager",
            };
            var menu333 = new MenuItem
            {
                Text = "维护采购飞机交付项目",
                NavUri = "UniCloud.Presentation.Purchase.Reception.AircraftPurchaseReceptionManager",
            };
            var menu334 = new MenuItem
            {
                Text = "维护租赁发动机交付项目",
                NavUri = "UniCloud.Presentation.Purchase.Reception.EngineLeaseReceptionManager",
            };
            var menu335 = new MenuItem
            {
                Text = "维护采购发动机交付项目",
                NavUri = "UniCloud.Presentation.Purchase.Reception.EnginePurchaseReceptionManager",
            };
            menu33.Items.Add(menu331);
            menu33.Items.Add(menu332);
            menu33.Items.Add(menu333);
            menu33.Items.Add(menu334);
            menu33.Items.Add(menu335);
            menu3.Items.Add(menu33);

            var menu34 = new MenuItem
            {
                Text = "查询分析",
            };
            var menu341 = new MenuItem
            {
                Text = "查询合同",
            };
            var menu342 = new MenuItem
            {
                Text = "分析飞机价格",
                NavUri = "UniCloud.Presentation.Purchase.QueryAnalyse.AnalyseAircraftPrice"
            };
            var menu343 = new MenuItem
            {
                Text = "分析发动机价格",
            };
            menu34.Items.Add(menu341);
            menu34.Items.Add(menu342);
            menu34.Items.Add(menu343);
            menu3.Items.Add(menu34);

            _items.Add(menu3);

            #endregion

            #region 应付款

            var menu4 = new MenuItem
            {
                Text = "应付款",
                IsEnabled = false,
            };
            _items.Add(menu4);

            #endregion

            #region 项目管理

            var menu5 = new MenuItem
            {
                Text = "项目管理",
                IsEnabled = false,
            };
            _items.Add(menu5);

            #endregion

            #region 系统管理

            var menu6 = new MenuItem
            {
                Text = "系统管理",
                IsEnabled = false,
            };
            _items.Add(menu6);

            #endregion
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 主页导航

        /// <summary>
        ///     主页导航
        /// </summary>
        public DelegateCommand<object> HomeCommand { get; private set; }

        private void OnHome(object obj)
        {
            // TODO：根据角色获取门户并导航
            var uri = new Uri("UniCloud.Presentation.Portal.Plan.PlanPortal", UriKind.Relative);
            regionManager.RequestNavigate(RegionNames.MainRegion, uri);
        }

        private bool CanHome(object obj)
        {
            return true;
        }

        #endregion

        #region 菜单导航

        public void RadMenu_ItemClick(object sender, RadRoutedEventArgs e)
        {
            var menuItem = (e.OriginalSource as RadMenuItem);
            if (menuItem != null && menuItem.CommandParameter != null)
            {
                var uri = new Uri(menuItem.CommandParameter.ToString(), UriKind.Relative);
                regionManager.RequestNavigate(RegionNames.MainRegion, uri);
            }
        }

        #endregion

        #endregion
    }
}