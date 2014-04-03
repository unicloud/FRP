#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/2 20:27:25
// 文件名：SubContractDocumentDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/2 20:27:25
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     合同文档
    /// </summary>
    [DataServiceKey("Id")]
    public class SubContractDocumentDTO
    {
        /// <summary>
        ///     ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }
    }
}
