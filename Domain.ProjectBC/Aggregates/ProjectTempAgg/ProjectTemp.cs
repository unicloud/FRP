#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/02，22:13
// 方案：FRP
// 项目：Domain.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.ProjectTempAgg
{
    /// <summary>
    ///     项目模板聚合根
    /// </summary>
    public class ProjectTemp : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<TaskTemp> _taskTemps;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal ProjectTemp()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     项目模板状态
        /// </summary>
        public ProjectTempStatus Status { get; set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        /// <summary>
        ///     任务模板集合
        /// </summary>
        public virtual ICollection<TaskTemp> TaskTemps
        {
            get { return _taskTemps ?? (_taskTemps = new HashSet<TaskTemp>()); }
            set { _taskTemps = new HashSet<TaskTemp>(value); }
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