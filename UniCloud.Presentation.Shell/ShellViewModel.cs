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
using System.ServiceModel;
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
using UniCloud.Presentation.Service;
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
            moduleManager.LoadModule(ModuleNames.Portal);
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
                case "适航管理":
                    return ModuleNames.Part;
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
                    var menuItemAirworthiness = Items.SingleOrDefault(m => m.Text == "适航管理");
                    if (menuItemAirworthiness != null)
                        menuItemAirworthiness.IsEnabled = true;
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
                var subItems = functionItems.Where(func => func.ParentItemId == m.Id).ToList();
                subItems.ForEach(fi => GenerateMenu(functionItems, fi, menu));
            });
        }

        private static void GenerateMenu(IEnumerable<FunctionItemDTO> functionItems, FunctionItemDTO functionItem,
            MenuItem menuItem)
        {
            var menu = new MenuItem {Text = functionItem.Name, NavUri = functionItem.NaviUrl};
            menuItem.Items.Add(menu);
            var functionItemDtos = functionItems as IList<FunctionItemDTO> ?? functionItems.ToList();
            var fis = functionItemDtos.Where(fi => fi.ParentItemId == functionItem.Id).ToList();
            fis.ForEach(func => GenerateMenu(functionItemDtos, func, menu));
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
            var client = GetAuthenticationServiceClient();
            var loginForm = obj as RadDataForm;
            // 由于未使用 DataForm 中的标准“确定”按钮，因此需要强制进行验证。
            // 如果未确保窗体有效，则在实体无效时调用该操作会导致异常。
            if (loginForm == null || !loginForm.ValidateItem()) return;
            client.LoginCompleted += client_LoginCompleted;
            client.LoginAsync(LoginInfo.UserName, LoginInfo.Password, null, true);
        }

        /// <summary>
        ///     动态获取身份验证服务客户端
        /// </summary>
        /// <returns>身份验证服务客户端</returns>
        private static AuthenticationServiceClient GetAuthenticationServiceClient()
        {
            var binding = new BasicHttpBinding(
                Application.Current.Host.Source.Scheme.Equals("https", StringComparison.InvariantCultureIgnoreCase)
                    ? BasicHttpSecurityMode.Transport
                    : BasicHttpSecurityMode.None)
            {
                MaxReceivedMessageSize = int.MaxValue,
                MaxBufferSize = int.MaxValue
            };
            var svcUri = Application.Current.Resources["AuthenticationService"].ToString();
            return new AuthenticationServiceClient(binding, new EndpointAddress(new Uri(svcUri)));
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
            StatusData.curUser = LoginInfo.UserName;
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