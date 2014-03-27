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
using System.IO;
using System.Linq;
using Lucene.Net.Analysis.PanGu;
using PanGu;
using PanGu.HighLight;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.CommonServiceBC.DocumnetSearch.LuceneSearch;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;

namespace UniCloud.Application.CommonServiceBC.DocumnetSearch
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理文档搜索相关信息的服务，供Distributed Services调用。
    /// </summary>
  [LogAOP]
    public class DocumentSearchAppService : ContextBoundObject, IDocumentSearchAppService
    {
        private const String Start = "|~S~|";
        private const String End = "|~E~|";

        public List<DocumentDTO> Search(string keyword, string documentType)
        {
            //DocumentIndexService service = new DocumentIndexService();
            //List<string> paths = new List<string>
            //                     {
            //                         @"C:\Users\Linxw\Desktop\读书.txt",
            //                         @"C:\Users\Linxw\Desktop\新建文本文档.txt",
            //                         @"C:\Users\Linxw\Desktop\111.txt",

            //                     };
            //Document document = DocumentFactory.CreateStandardDocument(Guid.NewGuid(), "读书.txt", ".txt", "", "", "",
            //    true, null, File.ReadAllText(@"C:\Users\Linxw\Desktop\读书.txt"), 1);
            //service.AddDocumentSearchIndex(document);
            //document = DocumentFactory.CreateStandardDocument(Guid.NewGuid(), "新建文本文档.txt", ".txt", "", "", "",
            //    true, null, File.ReadAllText(@"C:\Users\Linxw\Desktop\新建文本文档.txt"), 3);
            //service.AddDocumentSearchIndex(document);
            //document = DocumentFactory.CreateStandardDocument(Guid.NewGuid(), "111.txt", ".txt", "", "", "",
            //    true, null, File.ReadAllText(@"C:\Users\Linxw\Desktop\111.txt"), 3);
            //service.AddDocumen.tSearchIndex(document);
            //var simpleHTMLFormatter = new SimpleHTMLFormatter(Start, End);
            //var highlighter = new Highlighter(simpleHTMLFormatter, new Segment()) { FragmentSize = 20 };
            var aa = LuceneSearch.LuceneSearch.PanguQuery(keyword, documentType);
            if (aa == null) return new List<DocumentDTO>();
            var multiSearcher = IndexManager.GenerateMultiSearcher(documentType);
            var documents = aa.scoreDocs.Select(a => multiSearcher.Doc(a.doc)).Select(result => new DocumentDTO
                                                                                                  {
                                                                                                      DocumentId = Guid.Parse(result.Get("ID")),
                                                                                                      Extension = result.Get("extendType"),
                                                                                                      Abstract = ResultHighlighter.HighlightContent(keyword, result.Get("fileContent")),
                                                                                                      //FileContent = result.Get("fileContent").Replace(keyword, Start + keyword + End),
                                                                                                      Name = result.Get("fileName"),
                                                                                                  }).ToList();

            return documents;
        }
    }
}
