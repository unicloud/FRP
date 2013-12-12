#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:01:45
// 文件名：LeaseContractEngineDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
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
    ///  租赁合同发动机
    /// </summary>
    [DataServiceKey("LeaseContractEngineId")]
    public partial class LeaseContractEngineDTO
    {

        #region 属性

        //主键
        public int LeaseContractEngineId { get; set; }
        //发动机生产序列号
        public string SerialNumber { get; set; }
        //合同号
        public string ContractNumber { get; set; }
        //合同名称
        public string ContractName { get; set; }
        //Rank号
        public string RankNumber { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     接收数量
        /// </summary>
        public int ReceivedAmount { get; set; }

        /// <summary>
        ///     接受数量
        /// </summary>
        public int AcceptedAmount { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual string ImportType { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public virtual string ImportActionName { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     引进方式ID
        /// </summary>
        public Guid ImportCategoryId { get; set; }

        /// <summary>
        ///    供应商ID
        /// </summary>
        public int? SupplierId { get; set; }

        #endregion
    }
}
