#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，19:11
// 文件名：ForwarderDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     承运人。
    /// </summary>
    [DataServiceKey("ForwarderId")]
    public class ForwarderDTO
    {
        /// <summary>
        ///     承运人主键
        /// </summary>
        public int ForwarderId { get; set; }

        /// <summary>
        ///     名称。
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     地址。
        /// </summary>
        public string Addr { get; set; }

        /// <summary>
        ///     联系电话。
        /// </summary>
        public string Tel { get; set; }

        /// <summary>
        ///     传真。
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        ///     联系人。
        /// </summary>
        public string Attn { get; set; }

        /// <summary>
        ///     邮件。
        /// </summary>
        public string Email { get; set; }
    }
}