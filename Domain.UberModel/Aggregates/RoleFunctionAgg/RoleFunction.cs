#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/27，09:03
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.FunctionItemAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.RoleFunctionAgg
{
    public class RoleFunction : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal RoleFunction()
        {
        }

        public RoleFunction(int roleId, int functionItemId)
        {
            RoleId = roleId;
            FunctionItemId = functionItemId;
        }

        #endregion

        #region 属性

        #endregion

        #region 外键

        public int FunctionItemId { get; internal set; }

        public int RoleId { get; internal set; }

        #endregion

        #region 导航

        #endregion

        #region 导航属性

        /// <summary>
        ///     功能项
        /// </summary>
        public virtual FunctionItem FunctionItem { get; set; }

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