#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/30，17:55
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

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.ProjectTempAgg
{
    /// <summary>
    ///     项目模板聚合根
    ///     任务模板
    /// </summary>
    public class TaskTemp : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<TaskTemp> _children;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal TaskTemp()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     主题
        /// </summary>
        public string Subject { get; private set; }

        /// <summary>
        ///     开始偏移量
        /// </summary>
        public TimeSpan Start { get; set; }

        /// <summary>
        ///     结束偏移量
        /// </summary>
        public TimeSpan End { get; set; }

        /// <summary>
        ///     是否里程碑
        /// </summary>
        public bool IsMileStone { get; set; }

        /// <summary>
        ///     是否摘要任务
        /// </summary>
        public bool IsSummary { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     任务标准ID
        /// </summary>
        public int? TaskStandardId { get; set; }

        /// <summary>
        ///     父项ID
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        ///     项目模板ID
        /// </summary>
        public int ProjectTempId { get; set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     父节点
        /// </summary>
        public virtual TaskTemp Parent { get; set; }

        /// <summary>
        ///     子集
        /// </summary>
        public virtual ICollection<TaskTemp> Children
        {
            get { return _children ?? (_children = new HashSet<TaskTemp>()); }
            set { _children = new HashSet<TaskTemp>(value); }
        }

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