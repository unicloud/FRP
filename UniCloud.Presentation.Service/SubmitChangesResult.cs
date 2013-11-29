#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/15，10:11
// 文件名：SubmitChangesResult.cs
// 程序集：UniCloud.Presentation.Service
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;

#endregion

namespace UniCloud.Presentation.Service
{
    public class SubmitChangesResult
    {
        public IDictionary<string, string> Headers { get; set; }

        /// <summary>
        ///     错误消息
        /// </summary>
        public Exception Error { get; set; }

        public int StatusCode { get; set; }
    }
}