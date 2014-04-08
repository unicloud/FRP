#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/17 9:32:20
// 文件名：SupplierDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/17 9:32:20
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     供应商
    /// </summary>
    [DataServiceKey("SupplierId")]
    public class SupplierDTO
    {
        #region 属性

        /// <summary>
        ///     供应商主键
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     组织机构代码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     银行账户
        /// </summary>
        public virtual List<BankAccountDTO> BankAccounts { get; set; }

        #endregion
    }
}