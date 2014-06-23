#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:24
// 方案：FRP
// 项目：Application.BaseManagementBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Specialized;
using System.Configuration.Provider;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Security;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserRoleAgg;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.BaseManagementBC
{
    /// <summary>
    ///     自定义成员提供程序
    /// </summary>
    public class UniMembershipProvider : MembershipProvider
    {
        #region 声明、初始化

        private const int NewPasswordLength = 8;
        private readonly string _pName;
        private MachineKeySection _machineKey;
        private string _pApplicationName;
        private bool _pEnablePasswordReset;
        private bool _pEnablePasswordRetrieval;
        private int _pMaxInvalidPasswordAttempts;
        private int _pMinRequiredNonAlphanumericCharacters;
        private int _pMinRequiredPasswordLength;
        private int _pPasswordAttemptWindow;
        private MembershipPasswordFormat _pPasswordFormat;
        private string _pPasswordStrengthRegularExpression;
        private bool _pRequiresQuestionAndAnswer;
        private bool _pRequiresUniqueEmail;
        private bool _pWriteExceptionsToEventLog;

        #region DI

        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        #endregion

        public UniMembershipProvider()
        {
            _pApplicationName = "unicloud";
            _pName = "UniMembershipProvider";
            var cfg = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
            _machineKey = (MachineKeySection) cfg.GetSection("system.web/machineKey");
            _userRepository = UniContainer.Resolve<IUserRepository>();
            _userRoleRepository = UniContainer.Resolve<IUserRoleRepository>();
        }

        #endregion

        #region 属性

        public bool WriteExceptionsToEventLog
        {
            get { return _pWriteExceptionsToEventLog; }
            set { _pWriteExceptionsToEventLog = value; }
        }

        #region 重写MembershipProvider属性

        public override string ApplicationName
        {
            get { return _pApplicationName; }
            set { _pApplicationName = value; }
        }

        public override bool EnablePasswordReset
        {
            get { return _pEnablePasswordReset; }
        }

        public override bool EnablePasswordRetrieval
        {
            get { return _pEnablePasswordRetrieval; }
        }

        public override int MaxInvalidPasswordAttempts
        {
            get { return _pMaxInvalidPasswordAttempts; }
        }

        public override int MinRequiredNonAlphanumericCharacters
        {
            get { return _pMinRequiredNonAlphanumericCharacters; }
        }

        public override int MinRequiredPasswordLength
        {
            get { return _pMinRequiredPasswordLength; }
        }

        public override int PasswordAttemptWindow
        {
            get { return _pPasswordAttemptWindow; }
        }

        public override MembershipPasswordFormat PasswordFormat
        {
            get { return _pPasswordFormat; }
        }

        public override string PasswordStrengthRegularExpression
        {
            get { return _pPasswordStrengthRegularExpression; }
        }

        public override bool RequiresQuestionAndAnswer
        {
            get { return _pRequiresQuestionAndAnswer; }
        }

        public override bool RequiresUniqueEmail
        {
            get { return _pRequiresUniqueEmail; }
        }

        public override string Name
        {
            get { return _pName; }
        }

        #endregion

        #endregion

        #region 重写MembershipProvider方法

        public override void Initialize(string name, NameValueCollection config)
        {
            try
            {
                // 从web.config文件初始化相关值
                if (name.Length == 0) name = "UniMembershipProvider";
                if (String.IsNullOrEmpty(config["description"]))
                {
                    config.Remove("description");
                    config.Add("description", "UniCloud Membership provider");
                }
                base.Initialize(name, config);

                _pApplicationName = GetConfigValue(config["applicationName"], HostingEnvironment.ApplicationVirtualPath);
                _pMaxInvalidPasswordAttempts = Convert.ToInt32(GetConfigValue(config["maxInvalidPasswordAttempts"], "5"));
                _pPasswordAttemptWindow = Convert.ToInt32(GetConfigValue(config["passwordAttemptWindow"], "10"));
                _pMinRequiredNonAlphanumericCharacters =
                    Convert.ToInt32(GetConfigValue(config["minRequiredNonAlphanumericCharacters"], "1"));
                _pMinRequiredPasswordLength = Convert.ToInt32(GetConfigValue(config["minRequiredPasswordLength"], "7"));
                _pPasswordStrengthRegularExpression =
                    Convert.ToString(GetConfigValue(config["passwordStrengthRegularExpression"], ""));
                _pEnablePasswordReset = Convert.ToBoolean(GetConfigValue(config["enablePasswordReset"], "true"));
                _pEnablePasswordRetrieval = Convert.ToBoolean(GetConfigValue(config["enablePasswordRetrieval"], "true"));
                _pRequiresQuestionAndAnswer =
                    Convert.ToBoolean(GetConfigValue(config["requiresQuestionAndAnswer"], "false"));
                _pRequiresUniqueEmail = Convert.ToBoolean(GetConfigValue(config["requiresUniqueEmail"], "true"));
                _pWriteExceptionsToEventLog =
                    Convert.ToBoolean(GetConfigValue(config["writeExceptionsToEventLog"], "true"));

                var tempFormat = config["passwordFormat"] ?? "Hashed";

                switch (tempFormat)
                {
                    case "Hashed":
                        _pPasswordFormat = MembershipPasswordFormat.Hashed;
                        break;
                    case "Encrypted":
                        _pPasswordFormat = MembershipPasswordFormat.Encrypted;
                        break;
                    case "Clear":
                        _pPasswordFormat = MembershipPasswordFormat.Clear;
                        break;
                    default:
                        throw new ProviderException("未支持的密码格式！");
                }

                var cfg = WebConfigurationManager.OpenWebConfiguration(HostingEnvironment.ApplicationVirtualPath);
                _machineKey = (MachineKeySection) cfg.GetSection("system.web/machineKey");

                if (!_machineKey.ValidationKey.Contains("AutoGenerate")) return;
                if (PasswordFormat != MembershipPasswordFormat.Clear)
                    throw new ProviderException("自动生成的Keys不支持Hashed或者Encrypted密码！ ");
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("初始化出错", ex);
                throw;
            }
        }

        public override bool ChangePassword(string username, string oldPassword, string newPassword)
        {
            try
            {
                if (!ValidateUser(username, oldPassword)) return false;
                var args = new ValidatePasswordEventArgs(username, newPassword, true);
                OnValidatingPassword(args);
                if (args.Cancel)
                {
                    if (args.FailureInformation != null)
                        throw args.FailureInformation;
                    throw new MembershipPasswordException("新密码验证失败，因而取消修改密码。");
                }

                var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
                if (user == null) throw new Exception("未取到用户！");
                user.SetPassword(EncodePassword(newPassword));
                return true;
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("修改密码出错", ex);
                throw;
            }
        }

        public override bool ChangePasswordQuestionAndAnswer(string username, string password,
            string newPasswordQuestion, string newPasswordAnswer)
        {
            try
            {
                if (!ValidateUser(username, password))
                    return false;

                var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
                if (user == null) throw new Exception("未取到用户！");
                user.SetPasswordQuestionAndAnswer(newPasswordQuestion, EncodePassword(newPasswordAnswer));
                return true;
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("修改密码问题与答案出错", ex);
                throw;
            }
        }

        public override MembershipUser CreateUser(string username, string password, string email,
            string passwordQuestion, string passwordAnswer, bool isApproved, object providerUserKey,
            out MembershipCreateStatus status)
        {
            var args = new ValidatePasswordEventArgs(username, password, true);
            OnValidatingPassword(args);
            if (args.Cancel)
            {
                status = MembershipCreateStatus.InvalidPassword;
                return null;
            }
            if (RequiresUniqueEmail && GetUserNameByEmail(email) != "")
            {
                status = MembershipCreateStatus.DuplicateEmail;
                return null;
            }
            var u = GetUser(username, false);

            if (u == null)
            {
                var createDate = DateTime.Now;
                int passwordFormat;
                switch (_pPasswordFormat)
                {
                    case MembershipPasswordFormat.Clear:
                        passwordFormat = 0;
                        break;
                    case MembershipPasswordFormat.Encrypted:
                        passwordFormat = 2;
                        break;
                    case MembershipPasswordFormat.Hashed:
                        passwordFormat = 1;
                        break;
                    default:
                        passwordFormat = 0;
                        break;
                }

                try
                {
                    var user = UserFactory.CreateUser(username, EncodePassword(password), email, passwordFormat,
                        passwordQuestion, passwordAnswer, createDate);
                    _userRepository.Add(user);
                    status = MembershipCreateStatus.Success;
                }
                catch (Exception ex)
                {
                    if (WriteExceptionsToEventLog) WriteToEventLog("保存新增的用户出错", ex);
                    status = MembershipCreateStatus.UserRejected;
                }

                return GetUser(username, false);
            }
            status = MembershipCreateStatus.DuplicateUserName;

            return null;
        }

        public override bool DeleteUser(string username, bool deleteAllRelatedData)
        {
            try
            {
                var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
                if (user == null) throw new Exception("未取到用户！");
                _userRepository.Remove(user);
                if (deleteAllRelatedData)
                {
                    // 删除用户相关数据。
                    _userRoleRepository.GetFiltered(ur => ur.UserId == user.Id)
                        .ToList()
                        .ForEach(ur => _userRoleRepository.Remove(ur));
                }
                return true;
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("删除用户出错", ex);
                return false;
            }
        }

        public override MembershipUserCollection FindUsersByEmail(string emailToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            var users = new MembershipUserCollection();
            var selUsers =
                _userRepository.GetFiltered(
                    u => String.Equals(u.Email, emailToMatch, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(u => u.UserName);
            totalRecords = selUsers.Count();
            if (totalRecords == 0) return users;
            var startIndex = pageSize*pageIndex;
            selUsers.Skip(startIndex).Take(pageSize).Select(u => new MembershipUser(
                Name,
                u.UserName,
                u.Id,
                u.Email,
                u.PasswordQuestion,
                u.Comment,
                u.IsApproved,
                u.IsLockedOut,
                u.CreateDate,
                u.LastLoginDate,
                u.LastActivityDate,
                u.LastPasswordChangedDate,
                u.LastLockoutDate)).ToList().ForEach(users.Add);
            return users;
        }

        public override MembershipUserCollection FindUsersByName(string usernameToMatch, int pageIndex, int pageSize,
            out int totalRecords)
        {
            var users = new MembershipUserCollection();
            var selUsers =
                _userRepository.GetFiltered(
                    u => String.Equals(u.UserName, usernameToMatch, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(u => u.UserName);
            totalRecords = selUsers.Count();
            if (totalRecords == 0) return users;
            var startIndex = pageSize*pageIndex;
            selUsers.Skip(startIndex).Take(pageSize).Select(u => new MembershipUser(
                Name,
                u.UserName,
                u.Id,
                u.Email,
                u.PasswordQuestion,
                u.Comment,
                u.IsApproved,
                u.IsLockedOut,
                u.CreateDate,
                u.LastLoginDate,
                u.LastActivityDate,
                u.LastPasswordChangedDate,
                u.LastLockoutDate)).ToList().ForEach(users.Add);
            return users;
        }

        public override MembershipUserCollection GetAllUsers(int pageIndex, int pageSize, out int totalRecords)
        {
            var users = new MembershipUserCollection();
            var allUsers = _userRepository.GetAll().OrderBy(u => u.UserName);
            totalRecords = allUsers.Count();
            if (totalRecords == 0) return users;
            var startIndex = pageSize*pageIndex;
            allUsers.Skip(startIndex).Take(pageSize).Select(u => new MembershipUser(
                Name,
                u.UserName,
                u.Id,
                u.Email,
                u.PasswordQuestion,
                u.Comment,
                u.IsApproved,
                u.IsLockedOut,
                u.CreateDate,
                u.LastLoginDate,
                u.LastActivityDate,
                u.LastPasswordChangedDate,
                u.LastLockoutDate)).ToList().ForEach(users.Add);
            return users;
        }

        public override int GetNumberOfUsersOnline()
        {
            var onlineSpan = new TimeSpan(0, Membership.UserIsOnlineTimeWindow, 0);
            var compareTime = DateTime.Now.Subtract(onlineSpan);
            return _userRepository.GetFiltered(u => u.LastActivityDate > compareTime).Count();
        }

        public override string GetPassword(string username, string answer)
        {
            try
            {
                if (!EnablePasswordRetrieval)
                    throw new ProviderException("不能重新获取密码！");

                if (PasswordFormat == MembershipPasswordFormat.Hashed)
                    throw new ProviderException("哈希密码无法获取！");

                var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
                if (user == null) throw new Exception("未取到用户！");
                if (user.IsLockedOut)
                    throw new MembershipPasswordException("用户被锁定！");
                var password = user.Password;
                var passwordAnswer = user.PasswordAnswer;

                if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
                {
                    UpdateFailureCount(username, "passwordAnswer");
                    throw new MembershipPasswordException("密码问题答案不正确！");
                }

                if (PasswordFormat == MembershipPasswordFormat.Encrypted)
                    password = UnEncodePassword(password);

                return password;
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("获取密码出错", ex);
                throw;
            }
        }

        public override MembershipUser GetUser(string username, bool userIsOnline)
        {
            var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
            if (user == null) return null;
            var mu = new MembershipUser(
                Name,
                user.UserName,
                user.Id,
                user.Email,
                user.PasswordQuestion,
                user.Comment,
                user.IsApproved,
                user.IsLockedOut,
                user.CreateDate,
                user.LastLoginDate,
                user.LastActivityDate,
                user.LastPasswordChangedDate,
                user.LastLockoutDate);
            if (!userIsOnline) return mu;
            user.LastActivityDate = DateTime.Now;
            _userRepository.UnitOfWork.Commit();

            return mu;
        }

        public override MembershipUser GetUser(object providerUserKey, bool userIsOnline)
        {
            var user = _userRepository.GetFiltered(u => u.Id == (int) providerUserKey).FirstOrDefault();
            if (user == null) return null;
            var mu = new MembershipUser(
                Name,
                user.UserName,
                user.Id,
                user.Email,
                user.PasswordQuestion,
                user.Comment,
                user.IsApproved,
                user.IsLockedOut,
                user.CreateDate,
                user.LastLoginDate,
                user.LastActivityDate,
                user.LastPasswordChangedDate,
                user.LastLockoutDate);
            if (!userIsOnline) return mu;
            user.LastActivityDate = DateTime.Now;
            _userRepository.UnitOfWork.Commit();

            return mu;
        }

        public override string GetUserNameByEmail(string email)
        {
            var username =
                _userRepository.GetFiltered(u => u.Email == email).Select(u => u.UserName).FirstOrDefault() ??
                "";
            return username;
        }

        public override string ResetPassword(string username, string answer)
        {
            if (!EnablePasswordReset)
            {
                throw new NotSupportedException("密码重设不可用！");
            }

            if (answer == null && RequiresQuestionAndAnswer)
            {
                UpdateFailureCount(username, "passwordAnswer");
                throw new ProviderException("密码重设需要答出密码问题！");
            }

            var newPassword =
                Membership.GeneratePassword(NewPasswordLength, MinRequiredNonAlphanumericCharacters);

            var args =
                new ValidatePasswordEventArgs(username, newPassword, true);

            OnValidatingPassword(args);

            if (args.Cancel)
                if (args.FailureInformation != null)
                    throw args.FailureInformation;
                else
                    throw new MembershipPasswordException("密码验证失败从而取消密码重设！");

            var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
            if (user == null) throw new Exception("未取到用户！");
            var passwordAnswer = user.PasswordAnswer;
            if (RequiresQuestionAndAnswer && !CheckPassword(answer, passwordAnswer))
            {
                UpdateFailureCount(username, "passwordAnswer");
                throw new MembershipPasswordException("不正确的密码问题答案！");
            }

            try
            {
                user.SetPassword(newPassword);
                return newPassword;
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("密码重设出错", ex);
                throw new MembershipPasswordException("用户被锁定，因此密码未重设！");
            }
        }

        public override bool UnlockUser(string userName)
        {
            try
            {
                var user = _userRepository.GetFiltered(u => u.UserName == userName).FirstOrDefault();
                if (user == null) throw new Exception("未取到用户！");
                user.Unlock();
                return true;
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("解锁用户出错", ex);
                return false;
            }
        }

        public override void UpdateUser(MembershipUser user)
        {
            var mu = _userRepository.GetFiltered(u => u.UserName == user.UserName).FirstOrDefault();
            if (mu == null) throw new Exception("未取到用户！");
            mu.UpdateUser(user.Email, user.Comment, user.IsApproved);
        }

        public override bool ValidateUser(string username, string password)
        {
            var isValid = false;
            bool isApproved;
            string pwd;

            var user = _userRepository.GetFiltered(u => u.UserName == username && !u.IsLockedOut).FirstOrDefault();
            if (user != null)
            {
                pwd = user.Password;
                isApproved = user.IsApproved;
            }
            else
                return false;

            if (CheckPassword(password, pwd))
            {
                if (!isApproved) return false;
                isValid = true;
                user.LastLoginDate = DateTime.Now;
                user.FailedPasswordAttemptCount = 0;
                user.FailedPasswordAnswerAttemptCount = 0;
                _userRepository.UnitOfWork.Commit();
            }
            else
                UpdateFailureCount(username, "password");

            return isValid;
        }

        #endregion

        #region 私有方法

        /// <summary>
        ///     更新失败次数
        /// </summary>
        /// <param name="username">用户</param>
        /// <param name="failureType">失败类型</param>
        private void UpdateFailureCount(string username, string failureType)
        {
            try
            {
                var windowStart = new DateTime();
                var failureCount = 0;

                var user = _userRepository.GetFiltered(u => u.UserName == username).FirstOrDefault();
                if (user == null) throw new Exception("未取到用户！");
                if (failureType == "password")
                {
                    failureCount = user.FailedPasswordAttemptCount;
                    windowStart = user.FailedPasswordAttemptWindowStart;
                }

                if (failureType == "passwordAnswer")
                {
                    failureCount = user.FailedPasswordAnswerAttemptCount;
                    windowStart = user.FailedPasswordAnswerAttemptWindowStart;
                }

                var windowEnd = windowStart.AddMinutes(PasswordAttemptWindow);

                if (failureCount == 0 || DateTime.Now > windowEnd)
                {
                    if (failureType == "password")
                    {
                        user.FailedPasswordAttemptCount = 1;
                        user.FailedPasswordAttemptWindowStart = DateTime.Now;
                    }
                    if (failureType == "passwordAnswer")
                    {
                        user.FailedPasswordAnswerAttemptCount = 1;
                        user.FailedPasswordAnswerAttemptWindowStart = DateTime.Now;
                    }
                    _userRepository.UnitOfWork.Commit();
                }
                else
                {
                    if (failureCount++ >= MaxInvalidPasswordAttempts)
                    {
                        user.Lockout();
                        _userRepository.UnitOfWork.Commit();
                    }
                    else
                    {
                        if (failureType == "password")
                            user.FailedPasswordAttemptCount = failureCount;
                        if (failureType == "passwordAnswer")
                            user.FailedPasswordAnswerAttemptCount = failureCount;
                        _userRepository.UnitOfWork.Commit();
                    }
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("更新失败次数出错", ex);
                throw;
            }
        }

        /// <summary>
        ///     从Config配置文件获取值。
        /// </summary>
        /// <param name="configValue">配置值</param>
        /// <param name="defaultValue">缺省值</param>
        /// <returns>获取的值</returns>
        private static string GetConfigValue(string configValue, string defaultValue)
        {
            return String.IsNullOrEmpty(configValue) ? defaultValue : configValue;
        }

        /// <summary>
        ///     检查密码
        ///     基于密码格式比较密码值。
        /// </summary>
        /// <param name="password">输入的密码</param>
        /// <param name="dbpassword">存储的密码</param>
        /// <returns>检查是否成功，成功为True，失败为False。</returns>
        private bool CheckPassword(string password, string dbpassword)
        {
            var pass1 = password;
            var pass2 = dbpassword;

            switch (PasswordFormat)
            {
                case MembershipPasswordFormat.Encrypted:
                    pass2 = UnEncodePassword(dbpassword);
                    break;
                case MembershipPasswordFormat.Hashed:
                    pass1 = EncodePassword(password);
                    break;
            }

            return pass1 == pass2;
        }

        /// <summary>
        ///     加密密码。
        ///     根据密码格式（Encrypts, Hashes, clear）决定加密方法。
        /// </summary>
        /// <param name="password">密码</param>
        /// <returns>密码</returns>
        public string EncodePassword(string password)
        {
            var encodedPassword = password;

            try
            {
                switch (PasswordFormat)
                {
                    case MembershipPasswordFormat.Clear:
                        break;
                    case MembershipPasswordFormat.Encrypted:
                        encodedPassword =
                            Convert.ToBase64String(EncryptPassword(Encoding.Unicode.GetBytes(password)));
                        break;
                    case MembershipPasswordFormat.Hashed:
                        var hash = new HMACSHA1 {Key = HexToByte(_machineKey.ValidationKey + "C50B3C89")};
                        encodedPassword =
                            Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
                        break;
                    default:
                        throw new ProviderException("未支持的加密格式！");
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("加密出错", ex);
                throw;
            }
            return encodedPassword;
        }

        /// <summary>
        ///     解密密码
        ///     根据密码格式（Encrypts, Hashes, clear）决定解密方法。
        /// </summary>
        /// <param name="encodedPassword">加密的密码</param>
        /// <returns>密码</returns>
        private string UnEncodePassword(string encodedPassword)
        {
            var password = encodedPassword;

            try
            {
                switch (PasswordFormat)
                {
                    case MembershipPasswordFormat.Clear:
                        break;
                    case MembershipPasswordFormat.Encrypted:
                        password =
                            Encoding.Unicode.GetString(DecryptPassword(Convert.FromBase64String(password)));
                        break;
                    case MembershipPasswordFormat.Hashed:
                        throw new ProviderException("无法解密Hashed密码！");
                    default:
                        throw new ProviderException("未支持的密码格式！");
                }
            }
            catch (Exception ex)
            {
                if (WriteExceptionsToEventLog) WriteToEventLog("解密出错", ex);
                throw;
            }
            return password;
        }

        /// <summary>
        ///     十六进制转换为字节数组。
        ///     用于转换加密的key。
        /// </summary>
        /// <param name="hexString">十六进制字符串。</param>
        /// <returns>字节数组</returns>
        private static byte[] HexToByte(string hexString)
        {
            var returnBytes = new byte[hexString.Length/2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i*2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        ///     记录操作日志。
        /// </summary>
        /// <param name="action">操作</param>
        /// <param name="e">异常</param>
        private static void WriteToEventLog(string action, Exception e)
        {
            var log = new EventLog {Source = "UniMembershipProvider", Log = "Application"};

            var message = "发生异常。\n\n";
            message += "操作: " + action + "\n\n";
            message += "异常: " + e;
            log.WriteEntry(message);
        }

        #endregion
    }
}