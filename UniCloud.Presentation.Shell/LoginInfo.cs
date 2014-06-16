#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：9:46
// 方案：FRP
// 项目：Shell
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Presentation.Shell
{
    public class LoginInfo
    {
        private string _userName = string.Empty;

        public string UserName
        {
            get { return _userName; }

            set
            {
                if (_userName != null && _userName != value)
                {
                    _userName = value;
                }
            }
        }

        /// <summary>
        ///     获取或设置返回密码的函数。
        /// </summary>
        internal Func<string> PasswordAccessor { get; set; }

        public string Password
        {
            get { return (PasswordAccessor == null) ? string.Empty : PasswordAccessor(); }
            set
            {
                // 请不要将密码存储在私有字段中，因为不应以纯文本形式在内存中存储密码。
                // 而应将提供的 PasswordAccessor 用作该值的后备存储。
            }
        }
    }
}