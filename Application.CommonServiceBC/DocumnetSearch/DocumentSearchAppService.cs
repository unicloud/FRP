#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/21 10:49:10
// 文件名：DocumentSearchAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/21 10:49:10
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Application.LuceneSearch;

namespace UniCloud.Application.CommonServiceBC.DocumnetSearch
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理文档搜索相关信息的服务，供Distributed Services调用。
    /// </summary>
  [LogAOP]
    public class DocumentSearchAppService : ContextBoundObject, IDocumentSearchAppService
    {
        public List<DocumentDTO> Search(string keyword, string documentType)
        {
            var aa = LuceneSearch.LuceneSearch.PanguQuery(keyword, documentType);
            if (aa == null) return new List<DocumentDTO>();
            var multiSearcher = IndexManager.GenerateMultiSearcher(documentType);
            var documents = aa.scoreDocs.Select(a => multiSearcher.Doc(a.doc)).Select(result => new DocumentDTO
                                                                                                  {
                                                                                                      DocumentId = Guid.Parse(result.Get("ID")),
                                                                                                      Extension = result.Get("extendType"),
                                                                                                      Abstract = ResultHighlighter.HighlightContent(keyword, result.Get("fileContent")),
                                                                                                      Name = result.Get("fileName"),
                                                                                                  }).ToList();
            return documents;
        }
    }
}
