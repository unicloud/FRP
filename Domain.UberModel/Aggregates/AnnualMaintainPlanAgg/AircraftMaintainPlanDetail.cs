#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/9 9:09:03
// 文件名：AircraftMaintainPlanDetail
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/9 9:09:03
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AnnualMaintainPlanAgg
{
    public class AircraftMaintainPlanDetail: EntityInt, IValidatableObject
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftMaintainPlanDetail()
        {
        }

        #endregion

        #region 属性
        /// <summary>
        /// 机号
        /// </summary>
        public string AircraftNumber { get; internal set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string AircraftType { get; internal set; }
        /// <summary>
        /// 定检级别
        /// </summary>
        public string Level { get; internal set; }
        /// <summary>
        /// 预计进场时间
        /// </summary>
        public DateTime InDate { get; internal set; }
        /// <summary>
        /// 预计出场时间
        /// </summary>
        public DateTime OutDate { get; internal set; }
        #endregion

        #region 外键属性
        public int AircraftMaintainPlanId { get; internal set; }
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
