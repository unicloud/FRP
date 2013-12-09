#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/07，11:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftBFEAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     发动机购买订单
    /// </summary>
    public class BFEPurchaseOrder : Order
    {
        #region 私有字段

        private HashSet<ContractAircraftBFE> _contractAircraftBfes;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BFEPurchaseOrder()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     承运人联系人
        /// </summary>
        public string ForwarderLinkman { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     承运人ID
        /// </summary>
        public int? ForwarderId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     承运人
        /// </summary>
        public virtual Forwarder Forwarder { get; private set; }

        /// <summary>
        ///     合同飞机BFE
        /// </summary>
        public virtual ICollection<ContractAircraftBFE> ContractAircraftBfes
        {
            get { return _contractAircraftBfes ?? (_contractAircraftBfes = new HashSet<ContractAircraftBFE>()); }
            set { _contractAircraftBfes = new HashSet<ContractAircraftBFE>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     添加BFE采购订单行
        /// </summary>
        /// <param name="price">单价</param>
        /// <param name="amount">数量</param>
        /// <param name="discount">折扣</param>
        /// <param name="delivery">预计交付日期</param>
        /// <returns>BFE采购订单行</returns>
        public BFEPurchaseOrderLine AddNewBFEPurchaseOrderLine(decimal price, int amount, decimal discount,
            DateTime delivery)
        {
            var bfePurchaseOrderLine = new BFEPurchaseOrderLine();
            bfePurchaseOrderLine.GenerateNewIdentity();

            bfePurchaseOrderLine.OrderId = Id;
            bfePurchaseOrderLine.UnitPrice = price;
            bfePurchaseOrderLine.Amount = amount;
            bfePurchaseOrderLine.Discount = discount;
            bfePurchaseOrderLine.EstimateDeliveryDate = delivery;

            OrderLines.Add(bfePurchaseOrderLine);

            return bfePurchaseOrderLine;
        }

        /// <summary>
        ///     添加合同飞机BFE
        /// </summary>
        /// <param name="contractAircraft">合同飞机</param>
        /// <returns></returns>
        public ContractAircraftBFE AddNewContractAircraft(ContractAircraft contractAircraft)
        {
            if (contractAircraft == null || contractAircraft.IsTransient())
            {
                throw new ArgumentException("合同飞机参数为空！");
            }

            var contractAircraftBFE = new ContractAircraftBFE
            {
                BFEPurchaseOrderId = Id,
                BFEPurchaseOrder = this,
                ContractAircraftId = contractAircraft.Id,
                ContractAircraft = contractAircraft
            };

            ContractAircraftBfes.Add(contractAircraftBFE);

            return contractAircraftBFE;
        }

        /// <summary>
        ///     设置承运人
        /// </summary>
        /// <param name="forwarder">承运人</param>
        public void SetForwarder(Forwarder forwarder)
        {
            if (forwarder == null || forwarder.IsTransient())
            {
                throw new ArgumentException("交易合同参数为空！");
            }

            Forwarder = forwarder;
            ForwarderId = forwarder.Id;
            ForwarderLinkman = forwarder.Attn;
        }

        #endregion
    }
}