#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/19 9:16:17
// 文件名：FhaMaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/19 9:16:17
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// FHA维修成本
    /// </summary>
    [DataServiceKey("Id")]
    public class FhaMaintainCostDTO : MaintainCostDTO
    {
        #region 属性

        /// <summary>
        ///  主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 发动机属性
        /// </summary>
        public string EngineProperty { get; set; }
        /// <summary>
        /// jx
        /// </summary>
        public string Jx { get; set; }
        /// <summary>
        /// 上一年实际费率（美元）
        /// </summary>
        public decimal LastYearRate { get; set; }
        /// <summary>
        /// 费率增幅
        /// </summary>
        public decimal YearAddedRate { get; set; }
        /// <summary>
        /// 预算费率(美元)
        /// </summary>
        public decimal YearBudgetRate { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 空中小时数
        /// </summary>
        public decimal AirHour { get; set; }
        /// <summary>
        /// 发动机与空中小时比例
        /// </summary>
        public decimal HourPercent { get; set; }
        /// <summary>
        /// 发动机小时数
        /// </summary>
        public decimal Hour { get; set; }
        /// <summary>
        /// FHA费用(美元)
        /// </summary>
        public decimal FhaFeeUsd { get; set; }
        /// <summary>
        /// FHA费用(人民币)
        /// </summary>
        public decimal FhaFeeRmb { get; set; }
        /// <summary>
        /// 关税
        /// </summary>
        public decimal Custom { get; set; }
        /// <summary>
        /// 关增税(人民币)
        /// </summary>
        public decimal CustomAddedRmb { get; set; }
        /// <summary>
        /// 税费合计
        /// </summary>
        public decimal TotalTax { get; set; }
        /// <summary>
        /// 增值税率
        /// </summary>
        public decimal AddedValueRate { get; set; }
        /// <summary>
        /// 增值税
        /// </summary>
        public decimal AddedValue { get; set; }
        /// <summary>
        /// 含增值税
        /// </summary>
        public decimal IncludeAddedValue { get; set; }
        /// <summary>
        /// 关增税
        /// </summary>
        public decimal CustomAdded { get; set; }
        #endregion

        #region 外键属性
        public Guid AircraftTypeId { get; set; }
        #endregion
    }
}
