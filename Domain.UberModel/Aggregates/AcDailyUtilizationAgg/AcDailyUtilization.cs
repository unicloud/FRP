#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:05:43

// 文件名：AcDailyUtilization
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.AircraftAgg;

namespace UniCloud.Domain.UberModel.Aggregates.AcDailyUtilizationAgg
{
    /// <summary>
    /// AcDailyUtilization聚合根。
    /// 飞机日利用率
    /// </summary>
    public class AcDailyUtilization : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AcDailyUtilization()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     飞机注册号
        /// </summary>
        public string RegNumber { get; private set; }

        /// <summary>
        ///     计算日利用小时
        /// </summary>
        public decimal CalculatedHour { get; internal set; }

        /// <summary>
        ///     计算日利用循环
        /// </summary>
        public decimal CalculatedCycle { get; internal set; }

        /// <summary>
        ///     修正日利用小时
        /// </summary>
        public decimal AmendHour { get; private set; }

        /// <summary>
        ///     修正日利用循环
        /// </summary>
        public decimal AmendCycle { get; private set; }

        /// <summary>
        ///     年度
        /// </summary>
        public int Year { get; internal set; }

        /// <summary>
        ///     月份
        /// </summary>
        public int Month { get; internal set; }

        /// <summary>
        ///     是否当前
        /// </summary>
        public bool IsCurrent { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机ID
        /// </summary>
        public Guid AircraftId { get; private set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置修正日利用小时
        /// </summary>
        /// <param name="amendHour">修正日利用小时</param>
        public void SetAmendHour(decimal amendHour)
        {
            AmendHour = amendHour;
        }

        /// <summary>
        ///     设置修正日利用循环
        /// </summary>
        /// <param name="amendCycle">修正日利用循环</param>
        public void SetAmendCycle(decimal amendCycle)
        {
            AmendCycle = amendCycle;
        }

        /// <summary>
        ///     设置是否当前
        /// </summary>
        /// <param name="isCurrent">是否当前</param>
        public void SetIsCurrent(bool isCurrent)
        {
            IsCurrent = isCurrent;
        }

        /// <summary>
        ///     设置飞机
        /// </summary>
        /// <param name="aircraft">飞机</param>
        public void SetAircraft(Aircraft aircraft)
        {
            if (aircraft == null || aircraft.IsTransient())
            {
                throw new ArgumentException("飞机参数为空！");
            }

            AircraftId = aircraft.Id;
            RegNumber = aircraft.RegNumber;
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
