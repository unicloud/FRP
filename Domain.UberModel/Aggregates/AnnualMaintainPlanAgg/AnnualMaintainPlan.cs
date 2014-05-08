#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 9:38:12
// 文件名：AnnualMaintainPlan
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 9:38:12
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
    /// <summary>
    /// AnnualMaintainPlan聚合根。
    /// </summary>
    public class AnnualMaintainPlan : EntityInt, IValidatableObject
    {
        #region 私有字段

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AnnualMaintainPlan()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性
        /// <summary>
        /// 年度ID
        /// </summary>
        public Guid AnnualId
        {
            get;
            internal set;
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
