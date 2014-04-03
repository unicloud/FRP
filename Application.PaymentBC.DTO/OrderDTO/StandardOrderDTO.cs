#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/18，16:12
// 文件名：StandardOrderDTO.cs
// 程序集：UniCloud.Application.PaymentBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     标准订单，目前只做BFE
    /// </summary>
    [DataServiceKey("StandardOrderId")]
    public class StandardOrderDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int StandardOrderId { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        ///     币种ID
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        ///     供应商Id
        /// </summary>
        public int SupplierId { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }
    }
}