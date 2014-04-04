#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/21，9:17
// 方案：FRP
// 项目：Domain.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.OilMonitorAgg
{
    /// <summary>
    ///     滑油监控聚合根
    /// </summary>
    public class OilMonitor : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal OilMonitor()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     日期
        /// </summary>
        public DateTime Date { get; internal set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN { get; internal set; }

        /// <summary>
        ///     TSR，自上一次修理以来使用小时数
        /// </summary>
        public decimal TSR { get; internal set; }

        /// <summary>
        ///     总滑油消耗率
        /// </summary>
        public decimal TotalRate { get; internal set; }

        /// <summary>
        ///     区间滑油消耗率
        /// </summary>
        public decimal IntervalRate { get; internal set; }

        /// <summary>
        ///     区间滑油消耗率变化量
        /// </summary>
        public decimal DeltaIntervalRate { get; internal set; }

        /// <summary>
        ///     3天移动平均
        /// </summary>
        public decimal AverageRate3 { get; internal set; }

        /// <summary>
        ///     7天移动平均
        /// </summary>
        public decimal AverageRate7 { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     滑油用户ID
        /// </summary>
        public int SnRegID { get; internal set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置发动机滑油用户
        /// </summary>
        /// <param name="snReg">发动机滑油用户</param>
        /// <returns>滑油用户</returns>
        public EngineReg SetEngineOil(EngineReg snReg)
        {
            if (snReg == null || snReg.IsTransient())
            {
                throw new ArgumentException("发动机滑油用户参数为空！");
            }

            SnRegID = snReg.Id;
            return snReg;
        }

        /// <summary>
        ///     设置APU滑油用户
        /// </summary>
        /// <param name="snReg">APU滑油用户</param>
        /// <returns>滑油用户</returns>
        public APUReg SetApuOil(APUReg snReg)
        {
            if (snReg == null || snReg.IsTransient())
            {
                throw new ArgumentException("APU滑油用户参数为空！");
            }

            SnRegID = snReg.Id;
            return snReg;
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