#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:01:23
// 文件名：ContractEngineDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    ///     合同发动机基类
    /// </summary>
    [DataServiceKey("ContractEngineId")]
    public class ContractEngineDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int ContractEngineId { get; set; }

        /// <summary>
        ///     发动机生产序列号
        /// </summary>
        public string SerialNumber { get; set; }

        /// <summary>
        ///     合同号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        ///     Rank号
        /// </summary>
        public string RankNumber { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public string ImportType { get; set; }

        /// <summary>
        ///     供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int? SupplierId { get; set; }
    }
}