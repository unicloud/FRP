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
            set;
        }

        /// <summary>
        /// 计算日利用率
        /// </summary>
        public decimal CalculatedValue
        {
            get;
            set;
        }

        /// <summary>
        /// 修正日利用率
        /// </summary>
        public decimal AmendValue
        {
            get;
            set;
        }

        /// <summary>
        /// 年度
        /// </summary>
        public int Year
        {
            get;
            set;
        }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month
        {
            get;
            set;
        }

        /// <summary>
        /// 是否当前
        /// </summary>
        public bool IsCurrent
        {
            get;
            set;
        }
        #endregion

        #region 外键属性

        /// <summary>
        /// 飞机ID
        /// </summary>
        public Guid AircraftId
        {
            get;
            set;
        }
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
