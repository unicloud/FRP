#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/26，10:12
// 文件名：SessionUser.cs
// 程序集：UniCloud.Presentation
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间



#endregion

namespace UniCloud.Presentation.SessionExtension
{
    /// <summary>
    ///     Session用户存储
    /// </summary>
    public static class SessionUser
    {
        /// <summary>
        ///     主键
        /// </summary>
        public static string UserId
        {
            get
            {
                return SessionHelper.GetSession("userId") == null
                    ? null
                    : SessionHelper.GetSession("userId").ToString();
            }
        }

        /// <summary>
        ///     名称
        /// </summary>
        public static string UserName
        {
            get
            {
                return SessionHelper.GetSession("userName") == null
                    ? null
                    : SessionHelper.GetSession("userName").ToString();
            }
        }
    }
}