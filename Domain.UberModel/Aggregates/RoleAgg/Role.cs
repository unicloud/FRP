#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.RoleAgg
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