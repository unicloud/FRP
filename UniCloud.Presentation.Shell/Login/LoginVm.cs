#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/4 14:04:40
// 文件名：LoginVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/4 14:04:40
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.ServiceLocation;
using Telerik.Windows.Data;
using UniCloud.Presentation.Cryptography;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.BaseManagement;
using UniCloud.Presentation.Service.BaseManagement.BaseManagement;
#endregion

namespace UniCloud.Presentation.Shell.Login
{
    [Export(typeof(LoginVm))]
    public class LoginVm : ViewModelBase
    {
        #region 声明、初始化
        private readonly BaseManagementData _context;
        private readonly IBaseManagementService _service;
        private readonly FilterDescriptor _workNumberFilter;
        private readonly FilterDescriptor _pwdNumberFilter;
        private TextBox _userNameTextBox;
        private bool _isDfFocused;

        [ImportingConstructor]
        public LoginVm(IBaseManagementService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            LoginOkCommand = new DelegateCommand<object>(OnLoginOk);
            LoginCancelCommand = new DelegateCommand<object>(OnLoginCancel);

            Users = _service.CreateCollection(_context.Users);
            _workNumberFilter = new FilterDescriptor("EmployeeCode", FilterOperator.IsEqualTo, string.Empty);
            _pwdNumberFilter = new FilterDescriptor("Password", FilterOperator.IsEqualTo, string.Empty);
            Users.FilterDescriptors.Add(_workNumberFilter);
            Users.FilterDescriptors.Add(_pwdNumberFilter);
            Users.LoadedData += (sender, o) =>
                                {
                                    var user = Users.FirstOrDefault();
                                    if (user == null)
                                    {
                                        MessageAlert("登录失败，请检查工号和密码。");
                                        return;
                                    }
                                    GetFunctionItemsByUser(user.Id);
                                    LoginShellView();
                                };
        }

        #endregion

        #region 登录操作

        #region ViewModel 命令 LoginOkCommand -- 确认登录

        public DelegateCommand<object> LoginOkCommand { get; set; }
        private void OnLoginOk(object obj)
        {
            var loginForm = obj as DataForm;

            // 由于未使用 DataForm 中的标准“确定”按钮，因此需要强制进行验证。
            // 如果未确保窗体有效，则在实体无效时调用该操作会导致异常。
            if (loginForm != null && loginForm.ValidateItem())
            {
                _workNumberFilter.Value = LogInfo.UserName;
                _pwdNumberFilter.Value =  MD5CryptoServiceProvider.GetMd5String(LogInfo.Password);
                Users.Load(true);
            }
            else
            {
                _userNameTextBox.Focus();
            }

        }

        private void LoginShellView()
        {
            var pageSwitcher = Application.Current.RootVisual as PageSwitcher;
            if (pageSwitcher != null) pageSwitcher.SwitchPage(ServiceLocator.Current.GetInstance<ShellView>());
        }

        #endregion

        #region ViewModel 命令 LoginCancelCommand -- 取消登录

        public DelegateCommand<object> LoginCancelCommand { get; set; }
        private void OnLoginCancel(object obj)
        {
            MessageConfirm("是否退出！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                else
                {
                    HtmlPage.Window.Eval("window.open('','_self');window.close();");
                }
            });
        }

        #endregion

        #endregion

        #region 获取用户功能
        private void GetFunctionItemsByUser(int userId)
        {
            var validateUserUri = GetFunctionItemsByUserUri(userId.ToString(CultureInfo.InvariantCulture));
            _context.BeginExecute<FunctionItemDTO>(validateUserUri,
             result => Deployment.Current.Dispatcher.BeginInvoke(() =>
             {
                 var context = result.AsyncState as BaseManagementData;
                 try
                 {
                     if (context != null)
                     {
                         //context.MergeOption = MergeOption.OverwriteChanges;
                         var functionItems = context.EndExecute<FunctionItemDTO>(result).ToList();
                         if (functionItems == null)
                         {

                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageAlert(ex.ToString());
                 }
                 IsBusy = false;
             }), _context);
        }

        /// <summary>
        ///  获取用户功能  
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        private Uri GetFunctionItemsByUserUri(string userId)
        {
            return new Uri(string.Format("GetFunctionItemsByUser?userId='{0}'", userId),
                UriKind.Relative);
        }
        #endregion

        #region ViewModel 属性 --登录实体
        public QueryableDataServiceCollectionView<UserDTO> Users { get; set; }
        private LoginInfo _logInfo = new LoginInfo();
        /// <summary>
        /// 登录实体
        /// </summary>
        public LoginInfo LogInfo
        {
            get { return _logInfo; }
            set
            {
                if (_logInfo != value)
                {
                    _logInfo = value;
                    RaisePropertyChanged(() => LogInfo);
                }
            }
        }

        #endregion

        #region Method

        /// <summary>
        /// 自动生成登录框DataForm
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoginFormAutoGeneratingField(object sender, DataFormAutoGeneratingFieldEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "UserName":
                    _userNameTextBox = (TextBox)e.Field.Content;
                    break;
                case "Password":
                    {
                        var passwordBox = new PasswordBox();
                        e.Field.ReplaceTextBox(passwordBox, PasswordBox.PasswordProperty);
                    }
                    break;
            }
        }

        /// <summary>
        /// 把用户输入框设置为焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoginFormContentLoaded(object sender, DataFormContentLoadEventArgs e)
        {
            if (!_isDfFocused && _userNameTextBox != null)
            {
                _userNameTextBox.Focus();
                _isDfFocused = true;
            }
        }

        /// <summary>
        /// 将 Esc 映射到取消按钮，将 Enter 映射到确定按钮。
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void LoginFormKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                OnLoginCancel(sender);
            }
            else if (e.Key == Key.Enter && !LogInfo.IsLoggingIn)
            {
                OnLoginOk(sender);
            }
        }

        #endregion

        public override void LoadData()
        {
        }
    }
}
