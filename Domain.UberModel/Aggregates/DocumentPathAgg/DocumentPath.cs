#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，18:12
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
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.DocumentPathAgg
{
    /// <summary>
    ///     文档路径聚合根
    /// </summary>
    public class DocumentPath : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<DocumentPath> _documentPaths;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal DocumentPath()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        ///     是否叶子节点
        /// </summary>
        public bool IsLeaf { get; internal set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        public string Extension { get; internal set; }

        /// <summary>
        ///     文档ID
        /// </summary>
        public Guid? DocumentGuid { get; set; }

        /// <summary>
        ///     路径
        /// </summary>
        public string Path { get; internal set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     父节点ID
        /// </summary>
        public int? ParentId { get; internal set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     路径集合
        /// </summary>
        public virtual ICollection<DocumentPath> DocumentPaths
        {
            get { return _documentPaths ?? (_documentPaths = new HashSet<DocumentPath>()); }
            set { _documentPaths = new HashSet<DocumentPath>(value); }
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