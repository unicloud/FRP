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
using System.Linq;
using System.Text;
using System.Threading.Tasks;


#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg
{
    /// <summary>
    ///     关联文档聚合根
    /// </summary>
    public class RelatedDoc : EntityInt, IValidatableObject
    {

        #region 属性

        /// <summary>
        ///    业务外键
        /// </summary>
        public Guid SourceId { get; set; }

        /// <summary>
        ///     文档外键
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        ///  文档名称
        /// </summary>
        public string DocumentName { get; set; }
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
