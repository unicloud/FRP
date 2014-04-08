#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/30，19:20
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
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.ProjectBC.Aggregates.DependencyAgg
{
    /// <summary>
    ///     依赖项聚合根
    /// </summary>
    public class Dependency : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal Dependency()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     依赖类型
        /// </summary>
        public DependencyType DependencyType { get; private set; }

        #endregion

        #region 外键属性

        public int ScheduleId { get; internal set; }

        public int DependencyScheduleId { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     设置依赖类型
        /// </summary>
        /// <param name="dependency">依赖类型</param>
        public void SetDependencyType(DependencyType dependency)
        {
            switch (dependency)
            {
                case DependencyType.完成开始:
                    DependencyType = DependencyType.完成开始;
                    break;
                case DependencyType.开始开始:
                    DependencyType = DependencyType.开始开始;
                    break;
                case DependencyType.完成完成:
                    DependencyType = DependencyType.完成完成;
                    break;
                case DependencyType.开始完成:
                    DependencyType = DependencyType.开始完成;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("dependency");
            }
        }

        /// <summary>
        ///     设置依赖项
        /// </summary>
        /// <param name="dependencyId">依赖项ID</param>
        public void SetDependency(int dependencyId)
        {
            if (dependencyId == 0)
            {
                throw new ArgumentException("依赖项ID值无效！");
            }

            DependencyScheduleId = dependencyId;
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