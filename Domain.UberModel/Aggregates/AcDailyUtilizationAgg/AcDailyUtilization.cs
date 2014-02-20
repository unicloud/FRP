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
        /// 内部构造函数
        /// 限制只能从内部创建新实例
        /// </summary>
        internal AcDailyUtilization()
        {
        }
        #endregion

        #region 属性

        /// <summary>
        /// 飞机注册号
        /// </summary>
        public string RegNumber
        {
            get;
            private set;
        }

        /// <summary>
        /// 计算日利用率
        /// </summary>
        public decimal CalculatedValue
        {
            get;
            private set;
        }

        /// <summary>
        /// 修正日利用率
        /// </summary>
        public decimal AmendValue
        {
            get;
            private set;
        }

        /// <summary>
        /// 年度
        /// </summary>
        public int Year
        {
            get;
            private set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month
        {
            get;
            private set;
        }

        /// <summary>
        /// 是否当前
        /// </summary>
        public bool IsCurrent
        {
            get;
            private set;
        }
        #endregion

        #region 外键属性

        /// <summary>
        /// 飞机ID
        /// </summary>
        public Guid AircraftId
        {
            get;
            private set;
        }
        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置飞机注册号
        /// </summary>
        /// <param name="regNumber">飞机注册号</param>
        public void SetRegNumber(string regNumber)
        {
            if (string.IsNullOrWhiteSpace(regNumber))
            {
                throw new ArgumentException("飞机注册号参数为空！");
            }

            RegNumber = regNumber;
        }

        /// <summary>
        ///     设置计算日利用率
        /// </summary>
        /// <param name="calculatedValue">计算日利用率</param>
        public void SetCalculatedValue(decimal calculatedValue)
        {
            CalculatedValue = calculatedValue;
        }

        /// <summary>
        ///     设置修正日利用率
        /// </summary>
        /// <param name="amendValue">修正日利用率</param>
        public void SetAmendValue(decimal amendValue)
        {
            AmendValue = amendValue;
        }

        /// <summary>
        ///     设置年度
        /// </summary>
        /// <param name="year">年度</param>
        public void SetYear(int year)
        {
            Year = year;
        }

        /// <summary>
        ///     设置月份
        /// </summary>
        /// <param name="month">月份</param>
        public void SetMonth(int month)
        {
            Month = month;
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
