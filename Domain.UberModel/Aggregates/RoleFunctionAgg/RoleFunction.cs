#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
            private set;
        }

        public int RoleId
        {
            get;
            private set;
        }


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