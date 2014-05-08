#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 10:25:32
// 文件名：EngineNonFHAMaintainPlanDetail
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 10:25:32
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AnnualMaintainPlanAgg
{
    public class EngineMaintainPlanDetail : EntityInt, IValidatableObject
    {
           #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal EngineMaintainPlanDetail()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 发动机
        /// </summary>
        public string EngineNumber { get; internal set; }
        /// <summary>
        /// 送修日期
        /// </summary>
        public string InMaintainDate { get; internal set; }
        /// <summary>
        /// 返回日期
        /// </summary>
        public string OutMaintainDate { get; internal set; }
        /// <summary>
        /// 修理级别
        /// </summary>
        public string MaintainLevel { get; internal set; }
        /// <summary>
        /// 更换LLP个数
        /// </summary>
        public int ChangeLlpNumber { get; internal set; }
        /// <summary>
        /// TSN/CSN
        /// </summary>
        public string TsnCsn { get; internal set; }
        /// <summary>
        /// TSR/CSR
        /// </summary>
        public string TsrCsr { get; internal set; }
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
        /// 关增税
        /// </summary>
        public decimal CustomsTax { get; internal set; }
        /// <summary>
        /// 运费
        /// </summary>
        public decimal FreightFee { get; internal set; }
        /// <summary>
        /// 申报说明
        /// </summary>
        public string Note { get; internal set; }
        #endregion

        #region 外键属性
        public int EngineMaintainPlanId { get; internal set; }
        #endregion

        #region 导航属性

        #endregion

        #region 操作

        #endregion

         #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}
