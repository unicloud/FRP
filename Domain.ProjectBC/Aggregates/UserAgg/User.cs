#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，21:12
// 方案：FRP
// 项目：Domain.ProjectBC
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

namespace UniCloud.Domain.ProjectBC.Aggregates.UserAgg
{
    /// <summary>
    ///     用户聚合根
    /// </summary>
    public class User : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal User()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     员工号
        /// </summary>
        public string EmployeeCode { get; protected set; }

        /// <summary>
        ///     名
        /// </summary>
        public string FirstName { get; protected set; }

        /// <summary>
        ///     姓
        /// </summary>
        public string LaseName { get; protected set; }

        /// <summary>
        ///     显示名称
        /// </summary>
        public string DisplayName { get; protected set; }

        #endregion

        #region 外键属性

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