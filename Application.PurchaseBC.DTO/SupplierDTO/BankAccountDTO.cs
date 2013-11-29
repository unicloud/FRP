#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，19:11
// 文件名：BankAccountDTO.cs
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
    ///     银行账户相关信息。
    /// </summary>
    [DataServiceKey("BankAccountId")]
    public class BankAccountDTO
    {
        public int BankAccountId { get; set; }

        /// <summary>
        ///     账号
        /// </summary>
        public string Account { get; set; }

        /// <summary>
        ///     开户人
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     开户行
        /// </summary>
        public string Bank { get; set; }

        /// <summary>
        ///     开户行分支
        /// </summary>
        public string Branch { get; set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        ///     开户地址（中文）
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        ///     是否当前默认账号
        /// </summary>
        public bool IsCurrent { get; set; }

        /// <summary>
        ///     供应商外键
        /// </summary>
        public int SupplierId { get; set; }
    }
}