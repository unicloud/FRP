#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/27，09:03
// 方案：FRP
// 项目：Domain.BaseManagementBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg
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

        /// <summary>
        ///     功能ID
        /// </summary>
        public int FunctionItemId { get; private set; }

        /// <summary>
        ///     角色ID
        /// </summary>
        public int RoleId { get; private set; }

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

        /// <summary>
        ///     更新角色功能
        /// </summary>
        /// <param name="roleId">角色ID</param>
        /// <param name="functionItemId">功能ID</param>
        public void UpdateRoleFunction(int roleId, int functionItemId)
        {
            RoleId = roleId;
            FunctionItemId = functionItemId;
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