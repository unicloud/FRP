#region 命名空间

using System;
using System.ComponentModel.DataAnnotations;
using System.ServiceModel.DomainServices.Client;
using System.ServiceModel.DomainServices.Client.ApplicationServices;

#endregion

namespace UniCloud.Presentation.Shell.Login
{
    /// <summary>
    /// 此内部实体用于简化 UI 控件(DataForm 和显示验证错误的标签)与用户输入的登录凭据之间的绑定。
    /// </summary>
    public class LoginInfo : ComplexObject
    {
        private string userName;
        private string password;
        private bool rememberMe;
        private LoginOperation currentLoginOperation;

        /// <summary>
        /// 获取和设置用户名。
        /// </summary>
        [Display(Name = "工号")]
        [Required]
        public string UserName
        {
            get
            {
                return userName;
            }
            set
            {
                if (userName != value)
                {
                    ValidateProperty("UserName", value);
                    userName = value;
                    RaisePropertyChanged("UserName");
                }
            }
        }

        /// <summary>
        /// 获取或设置返回密码的函数。
        /// </summary>
        internal Func<string> PasswordAccessor { get; set; }

        /// <summary>
        /// 获取和设置密码。
        /// </summary>
        [Display(Name = "密码")]
        [Required]
        public string Password
        {
            get
            {
                return password;
            }
            set
            {
                ValidateProperty("Password", value);
                password = value;
                RaisePropertyChanged("Password");
            }
        }

        /// <summary>
        /// 获取和设置一个值，该值指示是否应记录用户的身份验证信息以用于将来的登录。
        /// </summary>
        //[Display(Name = "RememberMeLabel", ResourceType = typeof(ApplicationStrings))]
        [Display(AutoGenerateField = false)]
        public bool RememberMe
        {
            get
            {
                return rememberMe;
            }

            set
            {
                if (rememberMe != value)
                {
                    ValidateProperty("RememberMe", value);
                    rememberMe = value;
                    RaisePropertyChanged("RememberMe");
                }
            }
        }

        /// <summary>
        /// 获取或设置当前登录操作。
        /// </summary>
        internal LoginOperation CurrentLoginOperation
        {
            get
            {
                return currentLoginOperation;
            }
            set
            {
                if (currentLoginOperation != value)
                {
                    if (currentLoginOperation != null)
                    {
                        currentLoginOperation.Completed -= (s, e) => CurrentLoginOperationChanged();
                    }

                    currentLoginOperation = value;

                    if (currentLoginOperation != null)
                    {
                        currentLoginOperation.Completed += (s, e) => CurrentLoginOperationChanged();
                    }

                    CurrentLoginOperationChanged();
                }
            }
        }

        /// <summary>
        /// 获取一个值，该值指示用户当前是否正在登录。
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool IsLoggingIn
        {
            get
            {
                return CurrentLoginOperation != null && !CurrentLoginOperation.IsComplete;
            }
        }

        /// <summary>
        /// 获取一个值，该值指示用户当前是否可以登录。
        /// </summary>
        [Display(AutoGenerateField = false)]
        public bool CanLogIn
        {
            get
            {
                return !IsLoggingIn;
            }
        }

        /// <summary>
        /// 在当前登录操作更改时引发与操作相关的属性更改通知。
        /// </summary>
        private void CurrentLoginOperationChanged()
        {
            RaisePropertyChanged("IsLoggingIn");
            RaisePropertyChanged("CanLogIn");
        }

        /// <summary>
        /// 使用此实体中存储的数据创建新 <see cref="LoginParameters"/> 实例。
        /// </summary>
        public LoginParameters ToLoginParameters()
        {
            return new LoginParameters(UserName, Password, RememberMe, null);
        }
    }
}
