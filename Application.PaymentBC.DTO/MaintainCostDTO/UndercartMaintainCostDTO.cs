#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 16:44:04
// 文件名：UndercartMaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 16:44:04
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
    /// 起落架维修成本
    /// </summary>
    [DataServiceKey("Id")]
    public class UndercartMaintainCostDTO : MaintainCostDTO
    {
        #region 属性
        /// <summary>
        ///  主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 部件
        /// </summary>
        public int Part { get; set; }
        /// <summary>
        /// 修理费（欧元）
        /// </summary>
        public decimal MaintainFeeEur { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 修理费  （人民币）
        /// </summary>
        public decimal MaintainFeeRmb { get; set; }
        /// <summary>
        /// 运费（人民币）
        /// </summary>
        public decimal FreightFee { get; set; }
        /// <summary>
        /// 更换费（人民币）
        /// </summary>
        public decimal ReplaceFee { get; set; }
        /// <summary>
        /// 关税税率
        /// </summary>
        public decimal CustomRate { get; set; }
        /// <summary>
        /// 关税
        /// </summary>
        public string Custom { get; set; }
        /// <summary>
        /// 增值税税率
        /// </summary>
        public decimal AddedValueRate { get; set; }
        /// <summary>
        /// 增值税
        /// </summary>
        public string AddedValue { get; set; }
        #endregion

        #region 外键属性
        public Guid AircraftId { get; set; }
        public Guid ActionCategoryId { get; set; }
        public Guid AircraftTypeId { get; set; }
        #endregion

    }
}
