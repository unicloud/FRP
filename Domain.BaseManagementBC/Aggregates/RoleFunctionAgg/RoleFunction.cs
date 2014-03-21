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

        public RoleFunction(int functionItemId,int roleId)
        {
            FunctionItemId = functionItemId;
            RoleId = roleId;
        }


        #endregion

        #region 属性

        public int FunctionItemId
        {
            get;
            internal set;
        }

        public int RoleId
        {
            get;
            internal set;
        }


        #endregion

        #region 导航属性

        /// <summary>
        /// 功能项
        /// </summary>
        public virtual FunctionItem FunctionItem { get; set; }
        #endregion

        #region 方法

        public void SetRoleId(int roleId)
        {
            RoleId = roleId;
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