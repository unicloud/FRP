#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 16:18:56
// 文件名：UndercartMaintainCost
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 16:18:56
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg
{
    /// <summary>
    /// 起落架
    /// </summary>
    public class UndercartMaintainCost : MaintainCost
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal UndercartMaintainCost()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 类别
        /// </summary>
        public MaintainCostType Type { get; internal set; }
        /// <summary>
        /// 部件
        /// </summary>
        public UndercartPart Part { get; internal set; }
        /// <summary>
        /// 修理费（欧元）
        /// </summary>
        public decimal MaintainFeeEur { get; internal set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; internal set; }
        /// <summary>
        /// 修理费  （人民币）
        /// </summary>
        public decimal MaintainFeeRmb { get; internal set; }
        /// <summary>
        /// 运费（人民币）
        /// </summary>
        public decimal FreightFee { get; internal set; }
        /// <summary>
        /// 更换费（人民币）
        /// </summary>
        public decimal ReplaceFee { get; internal set; }
        /// <summary>
        /// 关税税率
        /// </summary>
        public decimal CustomRate { get; internal set; }
        /// <summary>
        /// 关税
        /// </summary>
        public decimal Custom { get; internal set; }
        /// <summary>
        /// 增值税税率
        /// </summary>
        public decimal AddedValueRate { get; internal set; }
        /// <summary>
        /// 增值税
        /// </summary>
        public decimal AddedValue { get; internal set; }
        #endregion

        #region 外键属性
        public Guid AircraftId { get; internal set; }
        public Guid ActionCategoryId { get; internal set; }
        public Guid AircraftTypeId { get; internal set; }
        #endregion

        #region 导航属性

        #endregion

        #region 操作
        #endregion
    }
}
