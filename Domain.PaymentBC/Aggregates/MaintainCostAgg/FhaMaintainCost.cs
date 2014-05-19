#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/19 8:50:04
// 文件名：FhaMaintainCost
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/19 8:50:04
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg
{
     /// <summary>
    /// FHA
    /// </summary>
    public class FhaMaintainCost : MaintainCost
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal FhaMaintainCost()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 发动机属性
        /// </summary>
        public string EngineProperty { get; internal set; }
        /// <summary>
        /// jx
        /// </summary>
        public string Jx { get; internal set; }
        /// <summary>
        /// 上一年实际费率（美元）
        /// </summary>
        public decimal LastYearRate { get; internal set; }
        /// <summary>
        /// 费率增幅
        /// </summary>
        public decimal YearAddedRate { get; internal set; }
        /// <summary>
        /// 预算费率(美元)
        /// </summary>
        public decimal YearBudgetRate { get; internal set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; internal set; }
        /// <summary>
        /// 空中小时数
        /// </summary>
        public decimal AirHour { get; internal set; }
        /// <summary>
        /// 发动机与空中小时比例
        /// </summary>
        public decimal HourPercent { get; internal set; }
        /// <summary>
        /// 发动机小时数
        /// </summary>
        public decimal Hour { get; internal set; }
        /// <summary>
        /// FHA费用(美元)
        /// </summary>
        public decimal FhaFeeUsd { get; internal set; }
        /// <summary>
        /// FHA费用(人民币)
        /// </summary>
        public decimal FhaFeeRmb { get; internal set; }
        /// <summary>
        /// 关税
        /// </summary>
        public decimal Custom { get; internal set; }
        /// <summary>
        /// 关增税(人民币)
        /// </summary>
        public decimal CustomAddedRmb { get; internal set; }
        /// <summary>
        /// 税费合计
        /// </summary>
        public decimal TotalTax { get; internal set; }
        /// <summary>
        /// 增值税率
        /// </summary>
        public decimal AddedValueRate { get; internal set; }
        /// <summary>
        /// 增值税
        /// </summary>
        public decimal AddedValue { get; internal set; }
        /// <summary>
        /// 含增值税
        /// </summary>
        public decimal IncludeAddedValue { get; internal set; }
        /// <summary>
        /// 关增税
        /// </summary>
        public decimal CustomAdded { get; internal set; }
        #endregion

        #region 外键属性
        public Guid AircraftTypeId { get; internal set; }
        #endregion

        #region 导航属性

        #endregion

        #region 操作
        #endregion
    }
}
