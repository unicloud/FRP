#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，21:41
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
using UniCloud.Domain.ProjectBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg
{
    /// <summary>
    ///     工作组聚合根
    ///     成员
    /// </summary>
    public class Member : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Member()
        {
        }

        #endregion

        #region 属性

        #endregion

        #region 外键属性

        /// <summary>
        ///     工作组ID
        /// </summary>
        public int WorkGroupId { get; internal set; }

        /// <summary>
        ///     工作组成员ID
        /// </summary>
        public int MemberUserId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     工作组成员
        /// </summary>
        public User MemberUser { get; private set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置工作组成员
        /// </summary>
        /// <param name="user">工作组成员</param>
        public void SetMember(User user)
        {
            if (user == null || user.IsTransient())
            {
                throw new ArgumentException("工作组成员参数为空！");
            }

            MemberUser = user;
            MemberUserId = user.Id;
        }

        /// <summary>
        ///     设置工作组成员
        /// </summary>
        /// <param name="id">工作组成员ID</param>
        public void SetMember(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("工作组成员参数为空！");
            }

            MemberUserId = id;
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