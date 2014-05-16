#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 11:06:43
// 文件名：NonFhaMaintainCostDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 11:06:43
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
    /// 非FHA.超包修维修成本
    /// </summary>
    [DataServiceKey("Id")]
    public class NonFhaMaintainCostDTO : MaintainCostDTO
    {
        #region 属性

        /// <summary>
        ///  主键
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 发动机
        /// </summary>
        public string EngineNumber { get; set; }
        /// <summary>
        /// 类别
        /// </summary>
        public int Type { get; set; }
        /// <summary>
        /// 是否包修
        /// </summary>
        public int ContractRepairt { get; set; }
        /// <summary>
        /// 修理级别
        /// </summary>
        public int MaintainLevel { get; set; }
        /// <summary>
        /// 更换LLP个数
        /// </summary>
        public int ChangeLlpNumber { get; set; }
        public decimal Tsr { get; set; }
        public decimal Csr { get; set; }
        /// <summary>
        /// 非FHA大修费
        /// </summary>
        public decimal NonFhaFee { get; set; }
        /// <summary>
        /// 附件修理费
        /// </summary>
        public decimal PartFee { get; set; }
        /// <summary>
        /// 更换LLP件费
        /// </summary>
        public decimal ChangeLlpFee { get; set; }
        /// <summary>
        /// 修理费小计
        /// </summary>
        public decimal FeeLittleSum { get; set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; set; }
        /// <summary>
        /// 修理费合计
        /// </summary>
        public decimal FeeTotalSum { get; set; }
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
        /// <summary>
        /// 关增税
        /// </summary>
        public decimal CustomsTax { get; set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal FreightFee { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
        /// <summary>
        /// 修理级别
        /// </summary>
        public int ActualMaintainLevel { get; set; }
        /// <summary>
        /// 更换LLP个数
        /// </summary>
        public int ActualChangeLlpNumber { get; set; }
        public decimal ActualTsr { get; set; }
        public decimal ActualCsr { get; set; }
        #endregion

        #region 外键属性
        public Guid AircraftId { get; set; }
        public Guid ActionCategoryId { get; set; }
        public Guid AircraftTypeId { get; set; }
        public int SupplierId { get; set; }
        #endregion

    }
}
