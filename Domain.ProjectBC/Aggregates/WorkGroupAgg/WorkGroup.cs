#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，21:05
// 方案：FRP
// 项目：Domain.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg
{
    /// <summary>
    ///     工作组聚合根
    /// </summary>
    public class WorkGroup : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<Member> _members;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal WorkGroup()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     工作组名称
        /// </summary>
        public string Name { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     工作组管理者ID
        /// </summary>
        public int ManagerId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     工作组管理者
        /// </summary>
        public virtual Member Manager { get; private set; }

        /// <summary>
        ///     工作组成员集合
        ///     包含工作组管理者
        /// </summary>
        public virtual ICollection<Member> Members
        {
            get { return _members ?? (_members = new HashSet<Member>()); }
            set { _members = new HashSet<Member>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     设置工作组管理者
        /// </summary>
        /// <param name="member">工作组管理者</param>
        public void SetManager(Member member)
        {
            if (member == null || member.IsTransient())
            {
                throw new ArgumentException("工作组管理者参数为空！");
            }

            var manager = Members.FirstOrDefault(m => m.Equals(Manager));
            if (manager != null)
                manager.IsManager = false;
            Manager = member;
            ManagerId = member.Id;
            member.IsManager = true;
        }

        /// <summary>
        ///     添加工作组成员
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述</param>
        /// <param name="userId">用户ID</param>
        /// <returns>添加的工作组成员</returns>
        public Member AddMember(string name, string description, int userId)
        {
            var member = WorkGroupFactory.CreateMember(name, description, userId);
            member.WorkGroupId = Id;
            Members.Add(member);

            return member;
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