#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 15:28:07
// 文件名：ApuMaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 15:28:07
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PaymentBC.DTO
{
    /// <summary>
    /// APU维修成本
    /// </summary>
    [DataServiceKey("Id")]
    public class ApuMaintainCostDTO : MaintainCostDTO
    {
        #region 属性

        /// <summary>
        ///  主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string NameType { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// 预算费率(美元)
        /// </summary>
        public decimal YearBudgetRate { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 预算轮档小时
        /// </summary>
        public decimal BudgetHour { get; set; }
        /// <summary>
        /// APU小时比例
        /// </summary>
        public decimal HourPercent { get; set; }
        /// <summary>
        /// APU 小时
        /// </summary>
        public decimal Hour { get; set; }
        /// <summary>
        /// APU包修费(美元)
        /// </summary>
        public decimal ContractRepairFeeUsd { get; set; }
        /// <summary>
        /// APU包修费(人民币)
        /// </summary>
        public decimal ContractRepairFeeRmb { get; set; }
        /// <summary>
        /// 关税率
        /// </summary>
        public decimal CustomRate { get; set; }
        /// <summary>
        /// 税费合计（人民元）
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
        #endregion

        #region 外键属性
        #endregion
    }
}
