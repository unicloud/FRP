#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 14:58:04
// 文件名：ApuMaintainCost
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 14:58:04
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.UberModel.Aggregates.MaintainCostAgg
{
    /// <summary>
    /// APU
    /// </summary>
    public class ApuMaintainCost : MaintainCost
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ApuMaintainCost()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 类别
        /// </summary>
        public string NameType { get; internal set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Type { get; internal set; }
        /// <summary>
        /// 预算费率(美元)
        /// </summary>
        public decimal YearBudgetRate { get; internal set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; internal set; }
        /// <summary>
        /// 预算轮档小时
        /// </summary>
        public decimal BudgetHour { get; internal set; }
        /// <summary>
        /// APU小时比例
        /// </summary>
        public decimal HourPercent { get; internal set; }
        /// <summary>
        /// APU 小时
        /// </summary>
        public decimal Hour { get; internal set; }
        /// <summary>
        /// APU包修费(美元)
        /// </summary>
        public decimal ContractRepairFeeUsd { get; internal set; }
        /// <summary>
        /// APU包修费(人民币)
        /// </summary>
        public decimal ContractRepairFeeRmb { get; internal set; }
        /// <summary>
        /// 关税率
        /// </summary>
        public decimal CustomRate { get; internal set; }
        /// <summary>
        /// 税费合计（人民元）
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
        #endregion

        #region 外键属性
        #endregion

        #region 导航属性

        #endregion

        #region 操作
        #endregion
    }
}
