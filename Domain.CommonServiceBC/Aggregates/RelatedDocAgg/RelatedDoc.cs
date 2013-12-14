#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/10 16:15:32
// 文件名：RelatedDoc
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#endregion

namespace UniCloud.Domain.CommonServiceBC.Aggregates.RelatedDocAgg
{
    /// <summary>
    ///     关联文档聚合根
    /// </summary>
    public class RelatedDoc : EntityInt, IValidatableObject
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal RelatedDoc()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     业务外键
        /// </summary>
        public Guid SourceId { get; internal set; }

        /// <summary>
        ///     文档外键
        /// </summary>
        public Guid DocumentId { get; internal set; }

        /// <summary>
        ///     文档名称
        /// </summary>
        public string DocumentName { get; internal set; }

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