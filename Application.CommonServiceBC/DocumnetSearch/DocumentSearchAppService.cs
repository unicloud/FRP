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
using UniCloud.Application.CommonServiceBC.DocumnetSearch.LuceneSearch;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;

namespace UniCloud.Application.CommonServiceBC.DocumnetSearch
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理文档搜索相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class DocumentSearchAppService : IDocumentSearchAppService
    {
        private const String Start = "|~S~|";
        private const String End = "|~E~|";

        public List<DocumentDTO> Search(string keyword)
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
            //    true, null, File.ReadAllText(@"C:\Users\Linxw\Desktop\新建文本文档.txt"), 1);
            //service.AddDocumentSearchIndex(document);
            //document = DocumentFactory.CreateStandardDocument(Guid.NewGuid(), "111.txt", ".txt", "", "", "",
            //    true, null, File.ReadAllText(@"C:\Users\Linxw\Desktop\111.txt"), 1);
            //service.AddDocumentSearchIndex(document);
           // var simpleHTMLFormatter = new SimpleHTMLFormatter(Start, End);
           // var highlighter = new Highlighter(simpleHTMLFormatter, new Segment()) { FragmentSize = 20 };
            //var aa = LuceneSearch.LuceneSearch.Query(keyword);
            IndexManager.GenerateSearcher();
            var docCount = IndexManager.IndexSearcher.MaxDoc();
            //var asdasd= aa.scoreDocs.Select(a => IndexManager.IndexSearcher.Doc(a.doc)).Select(result => new DocumentDTO
            //                                                                                      {
            //                                                                                          DocumentId = Guid.Parse(result.Get("ID")),
            //                                                                                          Extension = result.Get("extendType"),
            //                                                                                          //Abstract = highlighter.GetBestFragment(keyword, result.Get("fileContent")),
            //                                                                                          FileContent = result.Get("fileContent"),
            //                                                                                          Name = result.Get("fileName"),
            //                                                                                      }).ToList();
            var documents = new List<DocumentDTO>();
            for (int i = 0; i < docCount; i++)
            {
                var result = IndexManager.IndexSearcher.Doc(i);
                if (!string.IsNullOrEmpty(ResultHighlighter.HighlightContent(keyword, result.Get("fileContent"))))
                {
                    var temp = new DocumentDTO
                               {
                                   DocumentId = Guid.Parse(result.Get("ID")),
                                   Extension = result.Get("extendType"),
                                   Abstract = ResultHighlighter.HighlightContent(keyword, result.Get("fileContent")),
                                   FileContent = result.Get("fileContent"),
                                   Name = result.Get("fileName"),
                               };
                    documents.Add(temp);
                }
            }
            return documents;
            //return null;
        }
    }
}
