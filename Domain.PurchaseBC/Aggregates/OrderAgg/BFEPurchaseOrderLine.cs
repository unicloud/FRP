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
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Enums;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg
{
    /// <summary>
    ///     订单聚合根
    ///     发动机购买订单行
    /// </summary>
    public class BFEPurchaseOrderLine : OrderLine
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal BFEPurchaseOrderLine()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     交易状态
        /// </summary>
        public BFEStatus Status { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     BFE物料ID
        /// </summary>
        public int BFEMaterialId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     BFE物料
        /// </summary>
        public virtual BFEMaterial BFEMaterial { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置BFE物料
        /// </summary>
        /// <param name="bfeMaterial">BFE物料</param>
        public void SetBFEMaterial(BFEMaterial bfeMaterial)
        {
            if (bfeMaterial == null || bfeMaterial.IsTransient())
            {
                throw new ArgumentException("BFE物料参数为空！");
            }

            BFEMaterial = bfeMaterial;
            BFEMaterialId = bfeMaterial.Id;
        }

        /// <summary>
        ///     设置BFE物料
        /// </summary>
        /// <param name="bfeMaterialId">BFE物料ID</param>
        public void SetBFEMaterial(int bfeMaterialId)
        {
            if (bfeMaterialId == 0)
            {
                throw new ArgumentException("BFE物料ID参数为空！");
            }

            BFEMaterialId = bfeMaterialId;
        }


        /// <summary>
        ///     设置交易状态
        /// </summary>
        /// <param name="status">交易状态</param>
        public void SetStatus(BFEStatus status)
        {
            switch (status)
            {
                case BFEStatus.签约:
                    Status = BFEStatus.签约;
                    break;
                case BFEStatus.制造:
                    Status = BFEStatus.制造;
                    break;
                case BFEStatus.交付制造商:
                    Status = BFEStatus.交付制造商;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        #endregion
    }
}