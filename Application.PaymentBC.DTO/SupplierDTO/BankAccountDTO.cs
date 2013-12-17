#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/17 9:35:24
// 文件名：BankAccountDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/17 9:35:24
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///  银行账户
    /// </summary>
    [DataServiceKey("BankAccountId")]
    public class BankAccountDTO
    {
        #region 属性
        /// <summary>
        /// 银行账户主键
        /// </summary>
        public int BankAccountId { get; set; }
        /// <summary>
        ///     账号
        /// </summary>
        public string Account { get;  set; }

        /// <summary>
        ///     开户人
        /// </summary>
        public string Name { get;  set; }

        /// <summary>
        ///     开户行
        /// </summary>
        public string Bank { get;  set; }

        /// <summary>
        ///     开户行分支
        /// </summary>
        public string Branch { get;  set; }

        /// <summary>
        ///     国家
        /// </summary>
        public string Country { get;  set; }

        /// <summary>
        ///     开户地址（中文）
        /// </summary>
        public string Address { get;  set; }

        /// <summary>
        ///     是否当前默认账号
        /// </summary>
        public bool IsCurrent { get;  set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int SupplierId { get;  set; }

        #endregion
    }
}
