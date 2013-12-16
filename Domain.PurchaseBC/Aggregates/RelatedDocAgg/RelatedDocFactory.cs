#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/10 17:12:36
// 文件名：RelatedDocFactory
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.RelatedDocAgg
{
    /// <summary>
    ///     关联文档工厂
    /// </summary>
    public static class RelatedDocFactory
    {
        /// <summary>
        ///     创建关联文档
        /// </summary>
        /// <param name="sourceId">关联外键</param>
        /// <param name="documentId">关联文档外键</param>
        /// <param name="documentName">文档名称</param>
        /// <returns>创建的关联文档</returns>
        public static RelatedDoc CreateRelatedDoc(Guid sourceId, Guid documentId, string documentName)
        {
            var relatedDoc = new RelatedDoc
            {
                SourceId = sourceId,
                DocumentId = documentId,
                DocumentName = documentName,
            };
            return relatedDoc;
        }
    }
}
