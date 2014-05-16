#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 10:36:39
// 文件名：NonFhaMaintainCost
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 10:36:39
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.MaintainCostAgg
{
    /// <summary>
    /// 非FHA.超包修
    /// </summary>
    public class NonFhaMaintainCost: MaintainCost
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal NonFhaMaintainCost()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 发动机
        /// </summary>
        public string EngineNumber { get; internal set; }
        /// <summary>
        /// 类别
        /// </summary>
        public MaintainCostType Type { get; internal set; }
        /// <summary>
        /// 是否包修
        /// </summary>
        public ContractRepairtType ContractRepairt { get; internal set; }
        /// <summary>
        /// 修理级别
        /// </summary>
        public int MaintainLevel { get; internal set; }
        /// <summary>
        /// 更换LLP个数
        /// </summary>
        public int ChangeLlpNumber { get; internal set; }
        public decimal Tsr { get; internal set; }
        public decimal Csr { get; internal set; }
        /// <summary>
        /// 非FHA大修费
        /// </summary>
        public decimal NonFhaFee { get; internal set; }
        /// <summary>
        /// 附件修理费
        /// </summary>
        public decimal PartFee { get; internal set; }
        /// <summary>
        /// 更换LLP件费
        /// </summary>
        public decimal ChangeLlpFee { get; internal set; }
        /// <summary>
        /// 修理费小计
        /// </summary>
        public decimal FeeLittleSum { get; internal set; }
        /// <summary>
        /// 汇率
        /// </summary>
        public decimal Rate { get; internal set; }
        /// <summary>
        /// 修理费合计
        /// </summary>
        public decimal FeeTotalSum { get; internal set; }
        /// <summary>
        /// 关税税率
        /// </summary>
        public decimal CustomRate { get; internal set; }
        /// <summary>
        /// 关税
        /// </summary>
        public string Custom { get; internal set; }
        /// <summary>
        /// 增值税税率
        /// </summary>
        public decimal AddedValueRate { get; internal set; }
        /// <summary>
        /// 增值税
        /// </summary>
        public string AddedValue { get; internal set; }
        /// <summary>
        /// 关增税
        /// </summary>
        public decimal CustomsTax { get; internal set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal FreightFee { get; internal set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; internal set; }
        /// <summary>
        /// 修理级别
        /// </summary>
        public int ActualMaintainLevel { get; internal set; }
        /// <summary>
        /// 更换LLP个数
        /// </summary>
        public int ActualChangeLlpNumber { get; internal set; }
        public decimal ActualTsr { get; internal set; }
        public decimal ActualCsr { get; internal set; }
        #endregion

        #region 外键属性
        public Guid AircraftId { get; internal set; }
        public Guid ActionCategoryId { get; internal set; }
        public Guid AircraftTypeId { get; internal set; }
        public int SupplierId { get; internal set; }
        #endregion

        #region 导航属性

        #endregion

        #region 操作
        #endregion
    }
}

