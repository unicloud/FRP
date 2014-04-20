#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，19:11
// 文件名：LinkmanDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     联系人相关信息。
    /// </summary>
    [DataServiceKey("LinkmanId")]
    public class LinkmanDTO
    {
        /// <summary>
        ///     主键。
        /// </summary>
        public int LinkmanId { get; set; }

        /// <summary>
        ///     姓名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     是否默认联系人
        /// </summary>
        public bool IsDefault { get; set; }

        /// <summary>
        ///     电话
        /// </summary>
        public string TelePhone { get; set; }

        /// <summary>
        ///     手机
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        ///     传真
        /// </summary>
        public string Fax { get; set; }

        /// <summary>
        ///     邮件账号
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///     公司部门
        /// </summary>
        public string Department { get; set; }

        /// <summary>
        ///     地址
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     源ID
        /// </summary>
        public Guid SourceId { get; set; }

        public string CustCode { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        ///     更改日期
        /// </summary>
        public DateTime UpdateDate { get; set; }
    }
}