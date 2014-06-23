#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.UberModel.Aggregates.RoleFunctionAgg;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.RoleAgg
{
    public class Role : EntityInt, IValidatableObject
    {
        private ICollection<RoleFunction> _roleFunctions;

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Role()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     角色
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; internal set; }

        /// <summary>
        ///     层级代码
        /// </summary>
        public string LevelCode { get; internal set; }

        /// <summary>
        ///     角色编码
        /// </summary>
        public string Code { get; internal set; }

        /// <summary>
        ///     是否系统角色
        /// </summary>
        public bool IsSystemRole { get;internal set; }

        #endregion

        #region 外键

        #endregion

        #region 导航

        public virtual ICollection<RoleFunction> RoleFunctions
        {
            get { return _roleFunctions ?? (_roleFunctions = new HashSet<RoleFunction>()); }
            set { _roleFunctions = new HashSet<RoleFunction>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     新增角色功能
        /// </summary>
        /// <returns>角色功能</returns>
        public RoleFunction AddNewRoleFunction()
        {
            var roleFunction = new RoleFunction
            {
                RoleId = Id,
            };
            roleFunction.GenerateNewIdentity();
            RoleFunctions.Add(roleFunction);

            return roleFunction;
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