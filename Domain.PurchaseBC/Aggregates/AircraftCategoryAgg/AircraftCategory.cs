#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：18:46
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.AircraftCategoryAgg
{
    /// <summary>
    ///     AircraftCategory聚合根
    /// </summary>
    public class AircraftCategory : EntityGuid, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal AircraftCategory()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     类型
        /// </summary>
        public string Category { get; protected set; }

        /// <summary>
        ///     座级
        /// </summary>
        public string Regional { get; protected set; }

        #endregion

        #region 外键

        #endregion

        #region 导航

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