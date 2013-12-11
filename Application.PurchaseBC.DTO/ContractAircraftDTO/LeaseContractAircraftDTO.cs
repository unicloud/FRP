using System;
using System.Collections.Generic;
using System.Data.Services.Common;

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///  租赁合同飞机
    /// </summary>
    [DataServiceKey("LeaseContractAircraftId")]
    public partial class LeaseContractAircraftDTO
    {
        public LeaseContractAircraftDTO()
        {
            BFEPurchaseOrders = new List<BFEPurchaseOrderDTO>();
        }

        #region 属性
        /// <summary>
        /// 租赁合同飞机
        /// </summary>
        public int LeaseContractAircraftId { get; set; }

        /// <summary>
        ///     合同名称
        /// </summary>
        public string ContractName { get; set; }

        /// <summary>
        ///     合同编号
        /// </summary>
        public string ContractNumber { get; set; }

        /// <summary>
        ///     合同Rank号
        /// </summary>
        public string RankNumber { get; set; }

        /// <summary>
        ///     飞机批次号
        /// </summary>
        public string CSCNumber { get; set; }

        /// <summary>
        ///     飞机序列号
        /// </summary>
        public string SerialNumber { get; set; }

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
        ///     机型
        /// </summary>
        public virtual string AircraftTypeName { get; set; }

        /// <summary>
        ///     计划飞机
        /// </summary>
        public virtual string PlanAircraft { get; set; }

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
        ///    供应商ID
        /// </summary>
        public int? SupplierId { get; set; }

        /// <summary>
        ///     机型ID
        /// </summary>
        public Guid AircraftTypeId { get; set; }

        /// <summary>
        ///     计划飞机
        /// </summary>
        public Guid? PlanAircraftID { get; set; }

        /// <summary>
        ///     引进方式ID
        /// </summary>
        public Guid ImportCategoryId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     合同飞机BFE
        /// </summary>
        public List<BFEPurchaseOrderDTO> BFEPurchaseOrders { get; set; }
        #endregion
    }
}
