#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/26，10:12
// 文件名：SessionHelper.cs
// 程序集：UniCloud.Presentation
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.IO.IsolatedStorage;

#endregion

namespace UniCloud.Presentation.SessionExtension
{
    /// <summary>
    ///     Session缓存
    /// </summary>
    public static class SessionHelper
    {
        /// <summary>
        ///     基于全局网站的独立存储
        /// </summary>
        private static readonly IsolatedStorageSettings AppSetting = IsolatedStorageSettings.SiteSettings;

        /// <summary>
        ///     设置Session的值
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">key所对应值</param>
        /// <param name="focus">当key值存在，是否强制替换</param>
        public static void SetSession(string key, object value, bool focus = false)
        {
            if (!AppSetting.Contains(key))
            {
                AppSetting.Add(key, value);
                return;
            }
            if (focus)
            {
                AppSetting[key] = value;
            }
        }

        /// <summary>
        ///     获取Session值
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public static object GetSession(string key)
        {
            object value;
            if (!AppSetting.TryGetValue(key, out value))
            {
                value = null;
            }
            return value;
        }
    }
}