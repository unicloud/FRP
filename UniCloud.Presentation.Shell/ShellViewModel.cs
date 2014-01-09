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
using System.Linq;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Telerik.Windows;
using Telerik.Windows.Controls;
using UniCloud.Presentation.SessionExtension;

#endregion

namespace UniCloud.Presentation.Shell
{
    [Export(typeof (ShellViewModel))]
    public class ShellViewModel : NotificationObject, IPartImportsSatisfiedNotification
    {
        #region 声明

        [Import] public IModuleManager moduleManager;
        [Import] public IRegionManager regionManager;

        #region IPartImportsSatisfiedNotification 成员

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
            InitialSession("123456", "test用户");
        }

        #endregion

        #region 数据

        #region 应用模块

        /// <summary>
        ///     应用模块加载完成后的操作
        /// </summary>
        private void moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            switch (e.ModuleInfo.ModuleName)
            {
                case "PortalModule":
                    var menuItemPortal = Items.SingleOrDefault(m => m.Text == "管理门户");
                    if (menuItemPortal != null)
                        menuItemPortal.IsEnabled = true;
                OnHome(null);
                    break;
                case "FleetPlanModule":
                    var menuItemFleetPlan = Items.SingleOrDefault(m => m.Text == "运力规划");
                    if (menuItemFleetPlan != null)
                        menuItemFleetPlan.IsEnabled = true;
                    break;
                case "PurchaseModule":
                    var menuItemPurchase = Items.SingleOrDefault(m => m.Text == "采购合同");
                    if (menuItemPurchase != null)
                        menuItemPurchase.IsEnabled = true;
                    break;
                case "PaymentModule":
                    var menuItemPayment = Items.SingleOrDefault(m => m.Text == "应付款");
                    if (menuItemPayment != null)
                        menuItemPayment.IsEnabled = true;
                    break;
                case "ProjectModule":
                    var menuItemProject = Items.SingleOrDefault(m => m.Text == "项目管理");
                    if (menuItemProject != null)
                        menuItemProject.IsEnabled = true;
                    break;
                default:
                    throw new ArgumentException("没有匹配的模块名称！");
            }
        }

        /// <summary>
        ///     应用模块加载进程处理
        /// </summary>
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
                IsEnabled = false
            };
            _items.Add(menu1);

            #endregion

            #region 运力规划

            var menu2 = new MenuItem
            {
                Text = "运力规划",
                IsEnabled = false
            };
            _items.Add(menu2);

            var menu21 = new MenuItem
            {
                Text = "编制运力规划",
            };
            var menu211 = new MenuItem
            {
                Text = "民航五年规划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.CaacProgramming"
            };
            var menu212 = new MenuItem
            {
                Text = "管理五年规划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.AirProgramming"
            };
            var menu213 = new MenuItem
            {
                Text = "准备编制",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanPrepare"
            };
            var menu214 = new MenuItem
            {
                Text = "编制规划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanLay"
            };
            var menu215 = new MenuItem
            {
                Text = "报送规划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanSend"
            };
            var menu216 = new MenuItem
            {
                Text = "发布规划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanPublish"
            };
            var menu217 = new MenuItem
            {
                Text = "编制备发计划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.SpareEnginePlanLay"
            };
            menu21.Items.Add(menu211);
            menu21.Items.Add(menu212);
            menu21.Items.Add(menu213);
            menu21.Items.Add(menu214);
            menu21.Items.Add(menu215);
            menu21.Items.Add(menu216);
            menu21.Items.Add(menu217);
            menu2.Items.Add(menu21);

            var menu22 = new MenuItem
            {
                Text = "执行运力规划",
            };
            var menu221 = new MenuItem
            {
                Text = "维护申请",
                NavUri = "UniCloud.Presentation.FleetPlan.Requests.Request"
            };
            var menu222 = new MenuItem
            {
                Text = "维护批文",
                NavUri = "UniCloud.Presentation.FleetPlan.Approvals.Approval"
            };
            var menu223 = new MenuItem
            {
                Text = "完成规划",
                NavUri = "UniCloud.Presentation.FleetPlan.PerformFleetPlan.FleetPlanDeliver"
            };
            menu22.Items.Add(menu221);
            menu22.Items.Add(menu222);
            menu22.Items.Add(menu223);
            menu2.Items.Add(menu22);

            var menu23 = new MenuItem
            {
                Text = "更新飞机数据",
            };
            var menu231 = new MenuItem
            {
                Text = "变更所有权",
                NavUri = "UniCloud.Presentation.FleetPlan.AircraftOwnerShips.AircraftOwnership"
            };
            menu23.Items.Add(menu231);
            menu2.Items.Add(menu23);

            var menu24 = new MenuItem
            {
                Text = "分析规划执行",
            };
            var menu241 = new MenuItem
            {
                Text = "查询规划",
            };
            var menu242 = new MenuItem
            {
                Text = "分析规划执行",
            };
            var menu243 = new MenuItem
            {
                Text = "查询申请",
                NavUri = "UniCloud.Presentation.FleetPlan.Requests.QueryRequest"
            };
            var menu244 = new MenuItem
            {
                Text = "查询批文",
            };

            menu24.Items.Add(menu241);
            menu24.Items.Add(menu242);
            menu24.Items.Add(menu243);
            menu24.Items.Add(menu244);
            menu2.Items.Add(menu24);

            var menu25 = new MenuItem
            {
                Text = "查询分析",
            };
            var menu251 = new MenuItem
            {
                Text = "查询飞机档案",
            };
            var menu252 = new MenuItem
            {
                Text = "分析运力趋势",
                NavUri = "UniCloud.Presentation.FleetPlan.QueryAnalyse.FleetTrend"
            };
            var menu253 = new MenuItem
            {
                Text = "分析客机运力趋势",
                NavUri = "UniCloud.Presentation.FleetPlan.QueryAnalyse.PassengerAircraftTrend"
            };
            var menu254 = new MenuItem
            {
                Text = "统计在册飞机",
                NavUri = "UniCloud.Presentation.FleetPlan.QueryAnalyse.CountRegisteredFleet"
            };
            var menu255 = new MenuItem
            {
                Text = "分析飞机引进方式",
                NavUri = "UniCloud.Presentation.FleetPlan.QueryAnalyse.AircraftImportType"
            };
            var menu256 = new MenuItem
            {
                Text = "分析机队结构",
                NavUri = "UniCloud.Presentation.FleetPlan.QueryAnalyse.FleetStructure"
            };
            var menu257 = new MenuItem
            {
                Text = "分析飞机机龄",
                NavUri = "UniCloud.Presentation.FleetPlan.QueryAnalyse.FleetAge"
            };
            var menu258 = new MenuItem
            {
                Text = "分析发动机引进方式",
                NavUri = "UniCloud.Presentation.FleetPlan.QueryAnalyse.EngineImportType"
            };

            menu25.Items.Add(menu251);
            menu25.Items.Add(menu252);
            menu25.Items.Add(menu253);
            menu25.Items.Add(menu254);
            menu25.Items.Add(menu255);
            menu25.Items.Add(menu256);
            menu25.Items.Add(menu257);
            //menu25.Items.Add(menu258);
            menu2.Items.Add(menu25);

            #endregion

            #region 采购合同

            var menu3 = new MenuItem
            {
                Text = "采购合同",
                IsEnabled = false
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
                Text = "维护供应商可供物料",
                NavUri = "UniCloud.Presentation.Purchase.Supplier.SupplierMaterialManager"
            };
            var menu313 = new MenuItem
            {
                Text = "维护联系人",
                NavUri = "UniCloud.Presentation.Purchase.Supplier.LinkManManager"
            };
            var menu314 = new MenuItem
            {
                Text = "查询供应商",
                NavUri = "UniCloud.Presentation.Purchase.Supplier.QuerySupplier"
            };
            var menu315 = new MenuItem
            {
                Text = "维护BFE承运人",
                NavUri = "UniCloud.Presentation.Purchase.Forwarder.ForwarderManager"
            };
            menu31.Items.Add(menu311);
            menu31.Items.Add(menu312);
            menu31.Items.Add(menu313);
            menu31.Items.Add(menu314);
            menu31.Items.Add(menu315);
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
                NavUri = "UniCloud.Presentation.Purchase.Contract.AircraftLease"
            };
            var menu323 = new MenuItem
            {
                Text = "管理发动机购买合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.EnginePurchase"
            };
            var menu324 = new MenuItem
            {
                Text = "管理发动机租赁合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.EngineLease"
            };
            var menu325 = new MenuItem
            {
                Text = "管理BFE合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.BFEPurchase"
            };

            menu32.Items.Add(menu321);
            menu32.Items.Add(menu322);
            menu32.Items.Add(menu323);
            menu32.Items.Add(menu324);
            menu32.Items.Add(menu325);
            menu3.Items.Add(menu32);

            var menu33 = new MenuItem
            {
                Text = "管理维修合同",
            };
            var menu331 = new MenuItem
            {
                Text = "管理发动机维修合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.EngineMaintain"
            };
            var menu332 = new MenuItem
            {
                Text = "管理APU维修合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.ApuMaintain"
            };
            var menu333 = new MenuItem
            {
                Text = "管理起落架维修合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.UndercartMaintain"
            };
            menu33.Items.Add(menu331);
            menu33.Items.Add(menu332);
            menu33.Items.Add(menu333);
            menu3.Items.Add(menu33);

            var menu34 = new MenuItem
            {
                Text = "管理接机",
            };
            //var menu341 = new MenuItem
            //{
            //    Text = "匹配计划飞机",
            //    NavUri = "UniCloud.Presentation.Purchase.Reception.MatchingPlanAircraftManager"
            //};
            var menu342 = new MenuItem
            {
                Text = "维护租赁飞机交付项目",
                NavUri = "UniCloud.Presentation.Purchase.Reception.AircraftLeaseReceptionManager",
            };
            var menu343 = new MenuItem
            {
                Text = "维护采购飞机交付项目",
                NavUri = "UniCloud.Presentation.Purchase.Reception.AircraftPurchaseReceptionManager",
            };
            var menu344 = new MenuItem
            {
                Text = "维护租赁发动机交付项目",
                NavUri = "UniCloud.Presentation.Purchase.Reception.EngineLeaseReceptionManager",
            };
            var menu345 = new MenuItem
            {
                Text = "维护采购发动机交付项目",
                NavUri = "UniCloud.Presentation.Purchase.Reception.EnginePurchaseReceptionManager",
            };
            //menu34.Items.Add(menu341);
            menu34.Items.Add(menu342);
            menu34.Items.Add(menu343);
            menu34.Items.Add(menu344);
            menu34.Items.Add(menu345);
            menu3.Items.Add(menu34);

            var menu35 = new MenuItem
            {
                Text = "查询分析",
            };
            var menu351 = new MenuItem
            {
                Text = "查询合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.QueryContract"
            };
            var menu352 = new MenuItem
            {
                Text = "分析飞机价格",
                NavUri = "UniCloud.Presentation.Purchase.QueryAnalyse.AnalyseAircraftPrice"
            };
            var menu353 = new MenuItem
            {
                Text = "分析发动机价格",
            };
            menu35.Items.Add(menu351);
            menu35.Items.Add(menu352);
            menu35.Items.Add(menu353);
            menu3.Items.Add(menu35);

            _items.Add(menu3);

            #endregion

            #region 应付款

            var menu4 = new MenuItem
            {
                Text = "应付款",
                IsEnabled = false
            };
            var menu41 = new MenuItem
            {
                Text = "管理付款计划",
            };
            var menu411 = new MenuItem
            {
                Text = "管理飞机付款计划",
                NavUri = "UniCloud.Presentation.Payment.PaymentSchedules.AcPaymentSchedule"
            };
            var menu412 = new MenuItem
            {
                Text = "管理发动机付款计划",
                NavUri = "UniCloud.Presentation.Payment.PaymentSchedules.EnginePaymentSchedule"
            };
            var menu413 = new MenuItem
            {
                Text = "管理一般付款计划",
                NavUri = "UniCloud.Presentation.Payment.PaymentSchedules.StandardPaymentSchedule"
            };
            menu41.Items.Add(menu411);
            menu41.Items.Add(menu412);
            menu41.Items.Add(menu413);
            menu4.Items.Add(menu41);

            var menu42 = new MenuItem
            {
                Text = "管理采购发票",
            };
            var menu421 = new MenuItem
            {
                Text = "维护采购发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.PurchaseInvoiceManager"
            };
            var menu422 = new MenuItem
            {
                Text = "维护预付款发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.PrePayInvoiceManager"
            };
            var menu423 = new MenuItem
            {
                Text = "维护租赁发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.LeaseInvoiceManager"
            };
            var menu424 = new MenuItem
            {
                Text = "维护贷项单",
                NavUri = "UniCloud.Presentation.Payment.Invoice.CreditNoteManager"
            };
            menu42.Items.Add(menu421);
            menu42.Items.Add(menu422);
            menu42.Items.Add(menu423);
            menu42.Items.Add(menu424);
            menu4.Items.Add(menu42);


            var menu43 = new MenuItem
            {
                Text = "管理维修发票",
            };
            var menu431 = new MenuItem
            {
                Text = "维护发动机维修发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.EngineMaintain"
            };
            var menu432 = new MenuItem
            {
                Text = "维护APU维修发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.APUMaintain"
            };
            var menu433 = new MenuItem
            {
                Text = "维护起落架维修发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.UndercartMaintain"
            };
            var menu434 = new MenuItem
            {
                Text = "维护机身维修发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.AirframeMaintain"
            };
            menu43.Items.Add(menu431);
            menu43.Items.Add(menu432);
            menu43.Items.Add(menu433);
            menu43.Items.Add(menu434);
            menu4.Items.Add(menu43);


            var menu44 = new MenuItem
            {
                Text = "维护付款通知",
                NavUri = "UniCloud.Presentation.Payment.PaymentNotice.PaymentNotice"
            };
            menu4.Items.Add(menu44);

            var menu45 = new MenuItem
            {
                Text = "管理保函",
            };
            var menu451 = new MenuItem
            {
                Text = "维护租赁保证金",
                NavUri = "UniCloud.Presentation.Payment.Guarantees.LeaseGuarantee"
            };
            var menu452 = new MenuItem
            {
                Text = "维护大修保证金",
                NavUri = "UniCloud.Presentation.Payment.Guarantees.MaintainGuarantee"
            };
            menu45.Items.Add(menu451);
            menu45.Items.Add(menu452);
            menu4.Items.Add(menu45);

            var menu46 = new MenuItem
            {
                Text = "管理飞机价格",
            };
            var menu461 = new MenuItem
            {
                Text = "设置飞机价格公式",
            };
            var menu462 = new MenuItem
            {
                Text = "设置发动机价格公式",
            };
            var menu463 = new MenuItem
            {
                Text = "计算飞机价格",
            };
            menu46.Items.Add(menu461);
            menu46.Items.Add(menu462);
            menu46.Items.Add(menu463);
            menu4.Items.Add(menu46);

            var menu47 = new MenuItem
            {
                Text = "查询分析",
            };
            var menu471 = new MenuItem
            {
                Text = "查询付款计划",
                NavUri = "UniCloud.Presentation.Payment.PaymentSchedules.QueryPaymentSchedule"
            };
            var menu472 = new MenuItem
            {
                Text = "预测资金需求",
                NavUri = "UniCloud.Presentation.Payment.QueryAnalyse.FinancingDemandForecast"
            };
            var menu473 = new MenuItem
            {
                Text = "分析付款计划执行",
                NavUri = "UniCloud.Presentation.Payment.QueryAnalyse.PaymentScheduleExecute"
            };
            var menu474 = new MenuItem
            {
                Text = "分析维修成本",
                NavUri = "UniCloud.Presentation.Payment.QueryAnalyse.AnalyseMaintenanceCosts"
            };
            menu47.Items.Add(menu471);
            menu47.Items.Add(menu472);
            menu47.Items.Add(menu473);
            menu47.Items.Add(menu474);
            menu4.Items.Add(menu47);


            _items.Add(menu4);

            #endregion

            #region 项目管理

            var menu5 = new MenuItem
            {
                Text = "项目管理",
                IsEnabled = false,
            };

            var menu51 = new MenuItem
            {
                Text = "配置任务模板"
            };
            var menu52 = new MenuItem
            {
                Text = "配置项目模板"
            };
            var menu53 = new MenuItem
            {
                Text = "管理项目计划"
            };
            menu5.Items.Add(menu51);
            menu5.Items.Add(menu52);
            menu5.Items.Add(menu53);

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
            var uri = new Uri("UniCloud.Presentation.Portal.Manager.ManagerPortal", UriKind.Relative);
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

        #region 去掉默认右键菜单

        /// <summary>
        ///     去掉右击选择默认菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            e.Handled = true;
        }

        #endregion

        #region 设置用户Session

        /// <summary>
        ///     设置用户Session
        /// </summary>
        /// <param name="userId">用户Id</param>
        /// <param name="userName">用户名</param>
        private void InitialSession(string userId, string userName)
        {
            SessionHelper.SetSession("userId", userId);
            SessionHelper.SetSession("userName", userName);
        }

        #endregion

        #endregion
    }
}
