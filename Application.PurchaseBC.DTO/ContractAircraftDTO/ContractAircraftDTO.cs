#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     合同飞机基类
    /// </summary>
    [DataServiceKey("ContractNumber", "RankNumber")]
    public class ContractAircraftDTO
    {
        private List<BFEPurchaseOrderDTO> _bfePurchaseOrders;

        #region 属性

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
        public string AircraftTypeName { get; set; }

        /// <summary>
        ///     计划飞机
        /// </summary>
        public string PlanAircraft { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public string ImportType { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public string ImportActionName { get; set; }

        #endregion

        #region 外键属性

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

        /// <summary>
        ///     供应商ID
        /// </summary>
        public int? SupplierId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     合同飞机BFE集合
        /// </summary>
        public virtual List<BFEPurchaseOrderDTO> BFEPurchaseOrders
        {
            get { return _bfePurchaseOrders ?? (_bfePurchaseOrders = new List<BFEPurchaseOrderDTO>()); }
            set { _bfePurchaseOrders = value; }
        }

        #endregion
    }
}