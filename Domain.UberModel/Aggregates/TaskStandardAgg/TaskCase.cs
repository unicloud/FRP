#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，17:01
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.TaskStandardAgg
{
    /// <summary>
    ///     任务标准聚合根
    ///     任务案例
    /// </summary>
    public class TaskCase : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal TaskCase()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     任务标准ID
        /// </summary>
        public int TaskStandardId { get; internal set; }

        /// <summary>
        ///     关联业务ID
        ///     <remarks>
        ///         用于查找相关实体
        ///         订单任务需要关联，但文档类任务无需关联
        ///     </remarks>
        /// </summary>
        public int? RelatedId { get; private set; }

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置关联业务ID
        /// </summary>
        /// <param name="id">关联业务ID</param>
        public void SetRelateId(int id)
        {
            if (id == 0)
            {
                throw new ArgumentException("关联业务参数为空！");
            }

            RelatedId = id;
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