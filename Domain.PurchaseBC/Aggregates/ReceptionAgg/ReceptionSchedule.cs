#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/20 13:39:01
// 文件名：ReceptionSchedule
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Entities;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg
{
    /// <summary>
    ///     接收聚合根
    ///     交付日程
    /// </summary>
    public class ReceptionSchedule : ScheduleBase, IValidatableObject
    {
        #region 属性

        /// <summary>
        ///     分组信息
        /// </summary>
        public string Group { get; set; }

        #endregion

        #region 外键

        /// <summary>
        ///     接收项目外键
        /// </summary>
        public int ReceptionId { get; set; }

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