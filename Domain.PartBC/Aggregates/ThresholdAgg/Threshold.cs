#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 15:12:20
// 文件名：Threshold
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 15:12:20
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.ThresholdAgg
{
    /// <summary>
    ///     （滑油监控）阀值聚合根
    /// </summary>
    public class Threshold : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Threshold()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     总滑油消耗率阀值
        /// </summary>
        public decimal TotalThreshold { get; internal set; }

        /// <summary>
        ///     区间滑油消耗率阀值
        /// </summary>
        public decimal IntervalThreshold { get; internal set; }

        /// <summary>
        ///     区间滑油消耗率变化量阀值
        /// </summary>
        public decimal DeltaIntervalThreshold { get; internal set; }

        /// <summary>
        ///     3天移动平均阀值
        /// </summary>
        public decimal Average3Threshold { get; internal set; }

        /// <summary>
        ///     7天移动平均阀值
        /// </summary>
        public decimal Average7Threshold { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     阀值适用件号外键
        /// </summary>
        public int PnRegId { get; internal set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置阀值适用件号
        /// </summary>
        /// <param name="pnReg">阀值适用附件</param>
        public void SetPnReg(PnReg pnReg)
        {
            if (pnReg == null || pnReg.IsTransient())
            {
                throw new ArgumentException("阀值适用附件参数为空！");
            }

            PnRegId = pnReg.Id;
        }


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