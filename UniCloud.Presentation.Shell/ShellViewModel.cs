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
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.ViewModel;
using Telerik.Windows;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.Data.DataForm;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;
using UniCloud.Presentation.Shell.Authentication;

#endregion

namespace UniCloud.Presentation.Shell
{
    [Export(typeof (ShellViewModel))]
    public class ShellViewModel : NotificationObject, IPartImportsSatisfiedNotification
    {
        #region 声明

        private readonly BaseManagementData _context;
        [Import] public IModuleManager moduleManager;
        [Import] public IRegionManager regionManager;

        [ImportingConstructor]
        public ShellViewModel(IBaseManagementService service)
        {
            _context = service.Context;
            LoginInfo = new LoginInfo();
        }

        #region IPartImportsSatisfiedNotification 成员

        public void OnImportsSatisfied()
        {
            HomeCommand = new DelegateCommand<object>(OnHome, CanHome);
            LoginOkCommand = new DelegateCommand<object>(OnLoginOk, CanLoginOk);
            LoginCancelCommand = new DelegateCommand<object>(OnLoginCancel, CanLoginCancel);

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
            //InitialSession("123456", "test用户");
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 登录实体

        private LoginInfo _loginInfo;

        /// <summary>
        ///     登录实体
        /// </summary>
        public LoginInfo LoginInfo
        {
            get { return _loginInfo; }
            set
            {
                if (_loginInfo == value) return;
                _loginInfo = value;
                RaisePropertyChanged(() => LoginInfo);
            }
        }

        #endregion

        #region 是否已登录

        private bool _isLogined;

        /// <summary>
        ///     是否已登录
        /// </summary>
        public bool IsLogined
        {
            get { return _isLogined; }
            set
            {
                if (_isLogined == value) return;
                _isLogined = value;
                RaisePropertyChanged(() => IsLogined);
            }
        }

        #endregion

        #endregion

        #region 应用模块

        /// <summary>
        ///     根据登录用户加载模块
        /// </summary>
        /// <param name="moduleItems">模块功能项</param>
        private void ModuleLoader(List<FunctionItemDTO> moduleItems)
        {
            moduleItems.ForEach(m => moduleManager.LoadModule(GetModuleName(m)));
        }

        private static string GetModuleName(FunctionItemDTO moduleItem)
        {
            var item = moduleItem.Name;
            switch (item)
            {
                case "文档库":
                    return ModuleNames.CommonService;
                case "基础管理":
                    return ModuleNames.BaseManagement;
                case "管理门户":
                    return ModuleNames.Portal;
                case "运力规划":
                    return ModuleNames.FleetPlan;
                case "采购合同":
                    return ModuleNames.Purchase;
                case "应付款":
                    return ModuleNames.Payment;
                case "项目管理":
                    return ModuleNames.Project;
                case "飞机构型":
                    return ModuleNames.AircraftConfig;
                case "附件管理":
                    return ModuleNames.Part;
                case "发动机管理":
                    return ModuleNames.Part;
                default:
                    throw new ArgumentException("没有匹配的模块名称！");
            }
        }

        /// <summary>
        ///     应用模块加载完成后的操作
        /// </summary>
        private void moduleManager_LoadModuleCompleted(object sender, LoadModuleCompletedEventArgs e)
        {
            switch (e.ModuleInfo.ModuleName)
            {
                case "CommonServiceModule":
                    var menuItemCommonService = Items.SingleOrDefault(m => m.Text == "文档库");
                    if (menuItemCommonService != null)
                        menuItemCommonService.IsEnabled = true;
                    break;
                case "BaseManagementModule":
                    var menuItemBaseManagement = Items.SingleOrDefault(m => m.Text == "基础管理");
                    if (menuItemBaseManagement != null)
                        menuItemBaseManagement.IsEnabled = true;
                    break;
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
                case "AircraftConfigModule":
                    var menuItemAircraftConfig = Items.SingleOrDefault(m => m.Text == "飞机构型");
                    if (menuItemAircraftConfig != null)
                        menuItemAircraftConfig.IsEnabled = true;
                    break;
                case "PartModule":
                    var menuItemPart = Items.SingleOrDefault(m => m.Text == "附件管理");
                    if (menuItemPart != null)
                        menuItemPart.IsEnabled = true;
                    var menuItemEngine = Items.SingleOrDefault(m => m.Text == "发动机管理");
                    if (menuItemEngine != null)
                        menuItemEngine.IsEnabled = true;
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
            set
            {
                if (_items == value) return;
                _items = value;
                RaisePropertyChanged(() => Items);
            }
        }

        #endregion

        #region 加载

        private void LoadMenuItems(List<FunctionItemDTO> functionItems)
        {
            var module = functionItems.Where(f => f.ParentItemId == null).ToList();
            module.ForEach(m =>
            {
                var menu = new MenuItem {Text = m.Name, IsEnabled = false, NavUri = m.NaviUrl};
                _items.Add(menu);
                functionItems.Where(f => f.ParentItemId == m.Id)
                    .ToList()
                    .ForEach(fi => GenerateMenu(functionItems, fi, menu));
            });
        }

        private static void GenerateMenu(List<FunctionItemDTO> functionItems, FunctionItemDTO functionItem,
            MenuItem menuItem)
        {
            var fis = functionItems.Where(fi => fi.ParentItemId == functionItem.Id).ToList();
            fis.ForEach(fi =>
            {
                var menu = new MenuItem {Text = fi.Name, NavUri = fi.NaviUrl};
                menuItem.Items.Add(menu);
                functionItems.Where(f => f.ParentItemId == fi.Id)
                    .ToList()
                    .ForEach(func => GenerateMenu(functionItems, func, menu));
            });
        }

        private void LoadMenuItems()
        {
            #region 基础管理

            var menu9 = new MenuItem
            {
                Text = "基础管理",
                IsEnabled = true,
            };

            var menu91 = new MenuItem
            {
                Text = "管理授权",
            };
            var menu911 = new MenuItem
            {
                Text = "管理用户",
                NavUri = "UniCloud.Presentation.BaseManagement.ManagePermission.ManageUser"
            };
            var menu912 = new MenuItem
            {
                Text = "管理权限",
                NavUri = "UniCloud.Presentation.BaseManagement.ManagePermission.ManageFunctionsInRole"
            };
            var menu913 = new MenuItem
            {
                Text = "管理用户角色",
                NavUri = "UniCloud.Presentation.BaseManagement.ManagePermission.ManageUserInRole"
            };
            var menu914 = new MenuItem
            {
                Text = "管理组织机构角色",
                NavUri = "UniCloud.Presentation.BaseManagement.ManagePermission.ManageOrganizationInRole"
            };

            menu91.Items.Add(menu911);
            menu91.Items.Add(menu912);
            menu91.Items.Add(menu913);
            menu91.Items.Add(menu914);
            menu9.Items.Add(menu91);

            var menu92 = new MenuItem
            {
                Text = "管理运营资质",
            };
            var menu921 = new MenuItem
            {
                Text = "维护证照种类",
                NavUri = "UniCloud.Presentation.AircraftConfig.ManagerAircraftData.ManagerLicenseType"
            };
            var menu922 = new MenuItem
            {
                Text = "维护经营证照",
                NavUri = "UniCloud.Presentation.BaseManagement.ManageOperationQualification.ManageBusinessLicense"
            };
            menu92.Items.Add(menu921);
            menu92.Items.Add(menu922);
            menu9.Items.Add(menu92);

            var menu93 = new MenuItem
            {
                Text = "维护基础配置",
            };
            var menu931 = new MenuItem
            {
                Text = "维护分支机构",
                NavUri = "UniCloud.Presentation.BaseManagement.ManageSubsidiary.BranchCompany"
            };
            var menu932 = new MenuItem
            {
                Text = "管理系统配置",
                NavUri = "UniCloud.Presentation.BaseManagement.MaintainBaseSettings.ManageSystemConfig",
            };
            var menu933 = new MenuItem
            {
                Text = "管理提醒策略",
            };
            var menu934 = new MenuItem
            {
                Text = "配置邮件账号",
                NavUri = "UniCloud.Presentation.BaseManagement.MaintainBaseSettings.ConfigMailAddress"
            };
            menu93.Items.Add(menu931);
            menu93.Items.Add(menu932);
            menu93.Items.Add(menu933);
            menu93.Items.Add(menu934);
            menu9.Items.Add(menu93);

            _items.Add(menu9);

            #endregion

            #region 飞行构型

            var menu1 = new MenuItem
            {
                Text = "飞机构型",
                IsEnabled = true
            };

            var menu11 = new MenuItem
            {
                Text = "管理飞机构型",
            };
            var menu111 = new MenuItem
            {
                Text = "维护飞机系列",
                NavUri = "UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig.ManagerAircraftSeries"
            };
            var menu112 = new MenuItem
            {
                Text = "维护飞机型号",
                NavUri = "UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig.ManagerAircraftType"
            };
            var menu113 = new MenuItem
            {
                Text = "维护飞机配置",
                NavUri = "UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig.ManagerAircraftConfiguration"
            };
            menu11.Items.Add(menu111);
            menu11.Items.Add(menu112);
            menu11.Items.Add(menu113);
            menu1.Items.Add(menu11);

            var menu12 = new MenuItem
            {
                Text = "管理飞机数据",
            };
            //var menu121 = new MenuItem
            //{
            //    Text = "维护证照种类",
            //    NavUri = "UniCloud.Presentation.AircraftConfig.ManagerAircraftData.ManagerLicenseType"
            //};
            var menu122 = new MenuItem
            {
                Text = "维护飞机证照",
                NavUri = "UniCloud.Presentation.AircraftConfig.ManagerAircraftData.ManagerAircraftLicense"
            };
            //menu12.Items.Add(menu121);
            menu12.Items.Add(menu122);
            menu1.Items.Add(menu12);

            _items.Add(menu1);

            #endregion

            #region 运力规划

            var menu2 = new MenuItem
            {
                Text = "运力规划",
                IsEnabled = true
            };
            _items.Add(menu2);

            var menu21 = new MenuItem
            {
                Text = "编制运力规划",
            };
            var menu211 = new MenuItem
            {
                Text = "民航机队规划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.CaacProgramming"
            };
            var menu212 = new MenuItem
            {
                Text = "川航机队规划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.AirProgramming"
            };
            var menu213 = new MenuItem
            {
                Text = "准备编制",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanPrepare"
            };
            var menu214 = new MenuItem
            {
                Text = "编制计划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanLay"
            };
            var menu215 = new MenuItem
            {
                Text = "报送计划",
                NavUri = "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanSend"
            };
            var menu216 = new MenuItem
            {
                Text = "发布计划",
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
                Text = "维护发改委指标",
                NavUri = "UniCloud.Presentation.FleetPlan.Requests.ManageIndexAircraftView"
            };
            var menu222 = new MenuItem
            {
                Text = "维护申请",
                NavUri = "UniCloud.Presentation.FleetPlan.Requests.Request"
            };
            var menu223 = new MenuItem
            {
                Text = "维护批文",
                NavUri = "UniCloud.Presentation.FleetPlan.Approvals.Approval"
            };
            var menu224 = new MenuItem
            {
                Text = "完成计划",
                NavUri = "UniCloud.Presentation.FleetPlan.PerformFleetPlan.FleetPlanDeliver"
            };
            menu22.Items.Add(menu221);
            menu22.Items.Add(menu222);
            menu22.Items.Add(menu223);
            menu22.Items.Add(menu224);
            menu2.Items.Add(menu22);

            var menu23 = new MenuItem
            {
                Text = "更新飞机数据",
            };
            var menu231 = new MenuItem
            {
                Text = "更新飞机数据",
                NavUri = "UniCloud.Presentation.FleetPlan.AircraftOwnerShips.AircraftOwnership"
            };
            menu23.Items.Add(menu231);
            menu2.Items.Add(menu23);

            var menu24 = new MenuItem
            {
                Text = "分析计划执行",
            };
            var menu241 = new MenuItem
            {
                Text = "查询计划",
                NavUri = "UniCloud.Presentation.FleetPlan.QueryPlans.QueryPlan"
            };
            var menu242 = new MenuItem
            {
                Text = "分析计划执行",
                NavUri = "UniCloud.Presentation.FleetPlan.PerformFleetPlan.QueryPerformPlan"
            };
            var menu243 = new MenuItem
            {
                Text = "查询申请",
                NavUri = "UniCloud.Presentation.FleetPlan.Requests.QueryRequest"
            };
            var menu244 = new MenuItem
            {
                Text = "查询批文",
                NavUri = "UniCloud.Presentation.FleetPlan.Approvals.QueryApproval"
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
            //var menu258 = new MenuItem
            //{
            //    Text = "分析发动机引进方式",
            //    NavUri = "UniCloud.Presentation.FleetPlan.QueryAnalyse.EngineImportType"
            //};

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
                IsEnabled = true
            };

            var menu36 = new MenuItem
            {
                Text = "管理物料",
                NavUri = "UniCloud.Presentation.Purchase.Supplier.ManageMaterial"
            };
            menu3.Items.Add(menu36);

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
            var menu334 = new MenuItem
            {
                Text = "管理机身维修合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.AirframeMaintain"
            };

            menu33.Items.Add(menu331);
            menu33.Items.Add(menu332);
            menu33.Items.Add(menu333);
            menu33.Items.Add(menu334);
            menu3.Items.Add(menu33);

            var menu34 = new MenuItem
            {
                Text = "管理接机",
            };
            var menu341 = new MenuItem
            {
                Text = "匹配计划飞机",
                NavUri = "UniCloud.Presentation.Purchase.Reception.MatchingPlanAircraftManager"
            };
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
            menu34.Items.Add(menu341);
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
                Text = "合同管理",
                NavUri = "UniCloud.Presentation.Purchase.Contract.ManageContracts.ManageContract"
            };
            var menu352 = new MenuItem
            {
                Text = "查询合同",
                NavUri = "UniCloud.Presentation.Purchase.Contract.QueryContracts.QueryContractMain"
            };
            var menu353 = new MenuItem
            {
                Text = "分析飞机价格",
                NavUri = "UniCloud.Presentation.Purchase.QueryAnalyse.AnalyseAircraftPrice"
            };
            var menu354 = new MenuItem
            {
                Text = "分析发动机价格",
            };
            menu35.Items.Add(menu351);
            menu35.Items.Add(menu352);
            menu35.Items.Add(menu353);
            menu35.Items.Add(menu354);
            menu3.Items.Add(menu35);

            _items.Add(menu3);

            #endregion

            #region 应付款

            var menu4 = new MenuItem
            {
                Text = "应付款",
                IsEnabled = true
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
                Text = "管理BFE付款计划",
                NavUri = "UniCloud.Presentation.Payment.PaymentSchedules.StandardPaymentSchedule"
            };
            var menu414 = new MenuItem
            {
                Text = "管理维修付款计划",
                NavUri = "UniCloud.Presentation.Payment.PaymentSchedules.MaintainPaymentSchedule"
            };
            menu41.Items.Add(menu411);
            menu41.Items.Add(menu412);
            menu41.Items.Add(menu413);
            menu41.Items.Add(menu414);
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
                Text = "维护采购预付款发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.PurchasePrepayInvoiceManager"
            };
            var menu423 = new MenuItem
            {
                Text = "维护维修预付款发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.MaintainPrepayInvoiceManager"
            };
            var menu424 = new MenuItem
            {
                Text = "维护租赁发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.LeaseInvoiceManager"
            };
            var menu425 = new MenuItem
            {
                Text = "维护采购贷项单",
                NavUri = "UniCloud.Presentation.Payment.Invoice.PurchaseCreditNoteManager"
            };
            var menu426 = new MenuItem
            {
                Text = "维护维修贷项单",
                NavUri = "UniCloud.Presentation.Payment.Invoice.MaintainCreditNoteManager"
            };
            var menu427 = new MenuItem
            {
                Text = "维护杂项发票",
                NavUri = "UniCloud.Presentation.Payment.Invoice.SundryInvoiceManager"
            };

            menu42.Items.Add(menu421);
            menu42.Items.Add(menu422);
            menu42.Items.Add(menu423);
            menu42.Items.Add(menu424);
            menu42.Items.Add(menu425);
            menu42.Items.Add(menu426);
            menu42.Items.Add(menu427);
            menu4.Items.Add(menu42);


            var menu43 = new MenuItem
            {
                Text = "管理维修发票",
            };
            var menu431 = new MenuItem
            {
                Text = "维护发动机维修发票",
                NavUri = "UniCloud.Presentation.Payment.MaintainInvoice.EngineMaintain"
            };
            var menu432 = new MenuItem
            {
                Text = "维护APU维修发票",
                NavUri = "UniCloud.Presentation.Payment.MaintainInvoice.APUMaintain"
            };
            var menu433 = new MenuItem
            {
                Text = "维护起落架维修发票",
                NavUri = "UniCloud.Presentation.Payment.MaintainInvoice.UndercartMaintain"
            };
            var menu434 = new MenuItem
            {
                Text = "维护机身维修发票",
                NavUri = "UniCloud.Presentation.Payment.MaintainInvoice.AirframeMaintain"
            };
            var menu435 = new MenuItem
            {
                Text = "维护特修改装发票",
                NavUri = "UniCloud.Presentation.Payment.MaintainInvoice.SpecialRefitInvoiceManager"
            };
            menu43.Items.Add(menu431);
            menu43.Items.Add(menu432);
            menu43.Items.Add(menu433);
            menu43.Items.Add(menu434);
            menu43.Items.Add(menu435);
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
                Text = "维护大修储备金",
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
                Text = "维修成本",
            };
            var menu471 = new MenuItem
            {
                Text = "定检",
                NavUri = "UniCloud.Presentation.Payment.MaintainCost.RegularCheckMaintainCostManage"
            };
            var menu472 = new MenuItem
            {
                Text = "非FHA.超包修",
                NavUri = "UniCloud.Presentation.Payment.MaintainCost.NonFhaMaintainCostManage"
            };
            var menu473 = new MenuItem
            {
                Text = "起落架",
                NavUri = "UniCloud.Presentation.Payment.MaintainCost.UndercartMaintainCostManage"
            };
            var menu474 = new MenuItem
            {
                Text = "特修改装",
                NavUri = "UniCloud.Presentation.Payment.MaintainCost.SpecialRefitMaintainCostManage"
            };
            var menu475 = new MenuItem
            {
                Text = "APU",
                NavUri = "UniCloud.Presentation.Payment.MaintainCost.ApuMaintainCostManage"
            };
            var menu476 = new MenuItem
            {
                Text = "FHA",
                NavUri = "UniCloud.Presentation.Payment.MaintainCost.FhaMaintainCostManage"
            };
            var menu477 = new MenuItem
            {
                Text = "图表",
                NavUri = "UniCloud.Presentation.Payment.MaintainCost.MaintainCostReport"
            };
            menu47.Items.Add(menu471);
            menu47.Items.Add(menu472);
            menu47.Items.Add(menu473);
            menu47.Items.Add(menu474);
            menu47.Items.Add(menu475);
            menu47.Items.Add(menu476);
            menu47.Items.Add(menu477);
            menu4.Items.Add(menu47);

            var menu48 = new MenuItem
            {
                Text = "查询分析",
            };
            var menu481 = new MenuItem
            {
                Text = "查询付款计划",
                NavUri = "UniCloud.Presentation.Payment.PaymentSchedules.QueryPaymentSchedule"
            };
            var menu482 = new MenuItem
            {
                Text = "预测资金需求",
                NavUri = "UniCloud.Presentation.Payment.QueryAnalyse.FinancingDemandForecast"
            };
            var menu483 = new MenuItem
            {
                Text = "分析付款计划执行",
                NavUri = "UniCloud.Presentation.Payment.QueryAnalyse.PaymentScheduleExecute"
            };
            //var menu484 = new MenuItem
            //{
            //    Text = "分析维修成本",
            //    NavUri = "UniCloud.Presentation.Payment.QueryAnalyse.AnalyseMaintenanceCosts"
            //};
            menu48.Items.Add(menu481);
            menu48.Items.Add(menu482);
            menu48.Items.Add(menu483);
            //menu48.Items.Add(menu484);
            menu4.Items.Add(menu48);


            _items.Add(menu4);

            #endregion

            #region 项目管理

            //var menu5 = new MenuItem
            //{
            //    Text = "项目管理",
            //    IsEnabled = false,
            //};

            //var menu51 = new MenuItem
            //{
            //    Text = "配置工作组",
            //    NavUri = "UniCloud.Presentation.Project.Template.WorkGroup"
            //};
            //var menu52 = new MenuItem
            //{
            //    Text = "配置任务模板"
            //};
            //var menu53 = new MenuItem
            //{
            //    Text = "配置项目模板"
            //};
            //var menu54 = new MenuItem
            //{
            //    Text = "管理项目计划"
            //};
            //menu5.Items.Add(menu51);
            //menu5.Items.Add(menu52);
            //menu5.Items.Add(menu53);
            //menu5.Items.Add(menu54);

            //_items.Add(menu5);

            #endregion

            #region 附件管理

            var menu8 = new MenuItem
            {
                Text = "附件管理",
                IsEnabled = true,
            };

            var menu81 = new MenuItem
            {
                Text = "管理SCN/MSCN",
            };

            var menu811 = new MenuItem
            {
                Text = "维护SCN/MSCN",
                NavUri = "UniCloud.Presentation.Part.ManageSCN.MaintainScn"
            };

            var menu812 = new MenuItem
            {
                Text = "对比SCN/MSCN",
                NavUri = "UniCloud.Presentation.Part.ManageSCN.CompareScn"
            };

            menu81.Items.Add(menu811);
            menu81.Items.Add(menu812);


            var menu82 = new MenuItem
            {
                Text = "维护结构损伤",
                NavUri = "UniCloud.Presentation.Part.ManageAirStructureDamage.MaintainAirStructureDamage"
            };

            var menu83 = new MenuItem
            {
                Text = "管理年度送修计划",
            };

            var menu831 = new MenuItem
            {
                Text = "管理发动机年度送修计划",
                NavUri = "UniCloud.Presentation.Part.ManageAnnualMaintainPlan.ManageEngineMaintainPlan"
            };

            var menu832 = new MenuItem
            {
                Text = "管理机身、起落架年度送修计划",
                NavUri = "UniCloud.Presentation.Part.ManageAnnualMaintainPlan.ManageAircraftMaintainPlan"
            };

            menu83.Items.Add(menu831);
            menu83.Items.Add(menu832);

            menu8.Items.Add(menu81);
            menu8.Items.Add(menu82);
            menu8.Items.Add(menu83);
            _items.Add(menu8);

            #endregion

            #region 发动机管理

            var menu7 = new MenuItem
            {
                Text = "发动机管理",
                IsEnabled = true,
            };

            var menu71 = new MenuItem
            {
                Text = "管理基础配置",
                NavUri = "UniCloud.Presentation.Part.BaseConfigurations.BaseConfiguration",
            };
            menu7.Items.Add(menu71);

            var menu72 = new MenuItem
            {
                Text = "管理附件项",
            };
            var menu721 = new MenuItem
            {
                Text = "维护附件项",
                NavUri = "UniCloud.Presentation.Part.ManageItem.ItemView",
            };
            var menu722 = new MenuItem
            {
                Text = "维护装机控制信息",
                NavUri = "UniCloud.Presentation.Part.ManageItem.ItemControlView",
            };
            menu72.Items.Add(menu721);
            menu72.Items.Add(menu722);
            menu7.Items.Add(menu72);

            var menu73 = new MenuItem
            {
                Text = "管理件序号",
            };
            var menu731 = new MenuItem
            {
                Text = "维护件号",
                NavUri = "UniCloud.Presentation.Part.PnRegAndSnReg.PnRegView",
            };
            var menu732 = new MenuItem
            {
                Text = "维护序号",
                NavUri = "UniCloud.Presentation.Part.PnRegAndSnReg.SnRegView",
            };
            menu73.Items.Add(menu731);
            menu73.Items.Add(menu732);
            menu7.Items.Add(menu73);

            var menu74 = new MenuItem
            {
                Text = "管理发动机构型",
            };
            var menu741 = new MenuItem
            {
                Text = "维护基本构型",
                NavUri = "UniCloud.Presentation.Part.EngineConfig.BasicConfigGroupView",
            };
            var menu742 = new MenuItem
            {
                Text = "维护发动机选型",
                NavUri = "UniCloud.Presentation.Part.EngineConfig.AircraftConfigView",
            };
            var menu743 = new MenuItem
            {
                Text = "比较发动机构型差异",
                NavUri = "UniCloud.Presentation.Part.EngineConfig.ConfigCompareView",
            };
            menu74.Items.Add(menu741);
            menu74.Items.Add(menu742);
            menu74.Items.Add(menu743);
            menu7.Items.Add(menu74);

            var menu75 = new MenuItem
            {
                Text = "滑油监控",
            };
            var menu751 = new MenuItem
            {
                Text = "管理发动机滑油",
                NavUri = "UniCloud.Presentation.Part.OilMonitor.EngineOil"
            };
            var menu752 = new MenuItem
            {
                Text = "管理APU滑油",
            };
            menu75.Items.Add(menu751);
            menu75.Items.Add(menu752);
            menu7.Items.Add(menu75);

            var menu76 = new MenuItem
            {
                Text = "修正飞机利用率",
                NavUri = "UniCloud.Presentation.Part.MaintainAcDailyUtilization.CorrectAcDailyUtilization"
            };
            menu7.Items.Add(menu76);

            var menu77 = new MenuItem
            {
                Text = "维护拆换记录",
                NavUri = "UniCloud.Presentation.Part.MaintainControl.ManageRemovalAndInstallationView"
            };
            menu7.Items.Add(menu77);

            var menu78 = new MenuItem
            {
                Text = "维护在位信息",
                NavUri = "UniCloud.Presentation.Part.ManageOnBoardSn.ManageOnBoardSn"
            };
            menu7.Items.Add(menu78);

            //var menu79 = new MenuItem
            //{
            //    Text = "查询拆装历史",
            //    NavUri = "UniCloud.Presentation.Part.SnHistories.QuerySnHistory"
            //};
            //menu7.Items.Add(menu79);

            var menu710 = new MenuItem
            {
                Text = "控制维修",
            };
            var menu7101 = new MenuItem
            {
                Text = "查看控制方案",
                NavUri = "UniCloud.Presentation.Part.MaintainControl.QueryMaintainCtrlView"
            };
            var menu7102 = new MenuItem
            {
                Text = "查询到寿日期",
                NavUri = "UniCloud.Presentation.Part.MaintainControl.QueryLifeMonitorView"
            };
            menu710.Items.Add(menu7101);
            menu710.Items.Add(menu7102);
            menu7.Items.Add(menu710);

            var menu711 = new MenuItem
            {
                Text = "租赁月度报表",
                NavUri = "UniCloud.Presentation.Part.Report.MonthlyUtilizationReport"
            };
            menu7.Items.Add(menu711);
            _items.Add(menu7);

            #endregion

            #region 适航管理

            var menu6 = new MenuItem
            {
                Text = "适航管理",
                IsEnabled = true,
            };
            var menu61 = new MenuItem
            {
                Text = "查询AD/SB",
                NavUri = "UniCloud.Presentation.Part.ManageAdSb.QueryAdSb"
            };
            menu6.Items.Add(menu61);
            _items.Add(menu6);

            #endregion

            #region 文档库

            var menu10 = new MenuItem
            {
                Text = "文档库",
                IsEnabled = true,
            };
            var menu101 = new MenuItem
            {
                Text = "维护文档类型",
                NavUri = "UniCloud.Presentation.CommonService.DocumentTypeManager.ManagerDocumentType"
            };
            var menu102 = new MenuItem
            {
                Text = "搜索文档",
                NavUri = "UniCloud.Presentation.CommonService.SearchDocument.SearchDocumentMain"
            };
            menu10.Items.Add(menu101);
            menu10.Items.Add(menu102);
            _items.Add(menu10);

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
            if (menuItem == null || menuItem.CommandParameter == null) return;
            var uri = new Uri(menuItem.CommandParameter.ToString(), UriKind.Relative);
            regionManager.RequestNavigate(RegionNames.MainRegion, uri);
        }

        #endregion

        #region 登录

        #region 确认登录

        /// <summary>
        ///     确认登录
        /// </summary>
        public DelegateCommand<object> LoginOkCommand { get; private set; }

        private void OnLoginOk(object obj)
        {
            var client = new AuthenticationServiceClient();
            var loginForm = obj as RadDataForm;
            // 由于未使用 DataForm 中的标准“确定”按钮，因此需要强制进行验证。
            // 如果未确保窗体有效，则在实体无效时调用该操作会导致异常。
            if (loginForm == null || !loginForm.ValidateItem()) return;
            client.LoginCompleted += client_LoginCompleted;
            client.LoginAsync(LoginInfo.UserName, LoginInfo.Password, null, true);
        }

        private void client_LoginCompleted(object sender, LoginCompletedEventArgs e)
        {
            if (!e.Result) return;
            var queryString = string.Format("GetFunctionItemsByUser?userName='{0}'", LoginInfo.UserName);
            _context.BeginExecute<FunctionItemDTO>(new Uri(queryString, UriKind.Relative),
                result =>
                    Deployment.Current.Dispatcher.BeginInvoke(
                        () =>
                        {
                            var context = result.AsyncState as BaseManagementData;
                            if (context == null) return;
                            var retMenu = context.EndExecute<FunctionItemDTO>(result).ToList();
                            LoadMenuItems(retMenu);
                            ModuleLoader(retMenu.Where(m => m.ParentItemId == null).ToList());
                        }), _context);

            IsLogined = true;
        }

        private bool CanLoginOk(object obj)
        {
            return true;
        }

        #endregion

        #region 取消登录

        /// <summary>
        ///     取消登录
        /// </summary>
        public DelegateCommand<object> LoginCancelCommand { get; private set; }

        private void OnLoginCancel(object obj)
        {
            //_userNameTextBox.Focus();
        }

        private bool CanLoginCancel(object obj)
        {
            return true;
        }

        #endregion

        /// <summary>
        ///     自动生成登录框DataForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoginForm_AutoGeneratingField(object sender, AutoGeneratingFieldEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Password":
                    var passwordBox = new PasswordBox();
                    //e.DataField.ReplaceTextBox(passwordBox, PasswordBox.PasswordProperty);
                    e.DataField.Content = passwordBox;
                    LoginInfo.PasswordAccessor = () => passwordBox.Password;
                    break;
            }
        }

        /// <summary>
        ///     将 Esc 映射到取消按钮，将 Enter 映射到确定按钮。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoginForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Escape:
                    OnLoginCancel(sender);
                    break;
                case Key.Enter:
                    OnLoginOk(sender);
                    break;
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

        ///// <summary>
        /////     设置用户Session
        ///// </summary>
        ///// <param name="userId">用户Id</param>
        ///// <param name="userName">用户名</param>
        //private void InitialSession(string userId, string userName)
        //{
        //    SessionHelper.SetSession("userId", userId);
        //    SessionHelper.SetSession("userName", userName);
        //}

        #endregion

        #endregion
    }
}