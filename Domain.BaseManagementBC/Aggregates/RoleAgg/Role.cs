﻿#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;

#endregion

namespace UniCloud.Domain.BaseManagementBC.Aggregates.RoleAgg
{
    public class Role : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Role()
        {
        }
        /// <summary>
        /// 初始化角色构造函数
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="decription">描述</param>
        public Role(string name, string decription = null)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentNullException("name");
            }
            Name = name;
            Description = decription;
            CreateDate = DateTime.Now;
            LevelCode = null;
            Code = null;
        }

        #endregion

        #region 属性

        /// <summary>
        ///     角色
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        ///     创建日期
        /// </summary>
        public DateTime CreateDate { get; private set; }

        /// <summary>
        ///     顺序
        /// </summary>
        public string LevelCode { get; private set; }

        /// <summary>
        ///     角色编码
        /// </summary>
        public string Code { get; private set; }

        /// <summary>
        /// 功能项集合
        /// </summary>
        private ICollection<RoleFunction> _roleFunctions;
        public ICollection<RoleFunction> RoleFunctions
        {
            get { return _roleFunctions ?? (_roleFunctions = new HashSet<RoleFunction>()); }
            set { _roleFunctions = new HashSet<RoleFunction>(value); }
        }
        #endregion

        #region 操作

        /// <summary>
        /// 设置属性
        /// </summary>
        /// <param name="name">名字</param>
        /// <param name="description">描述</param>
        public void SerRole(string name, string description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// 新增RoleFunction
        /// </summary>
        /// <returns></returns>
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