#region 命名空间

using System;
using System.Globalization;
using System.IO;
using Lucene.Net.Documents;
using Lucene.Net.Index;

#endregion

namespace UniCloud.Application.CommonServiceBC.DocumnetSearch.LuceneSearch
{
    public static class LuceneIndex
    {
        /// <summary>
        /// 创建Document索引
        /// </summary>
        public static void CreateDocumentIndex(UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg.Document document)
        {
            try
            {
                var doc = new Lucene.Net.Documents.Document();//Document为索引文档，可以理解成数据库里的记录
                doc.Add(new Field("pKID", document.Id.ToString(), Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                doc.Add(new Field("ID", document.Id.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));//存储且索引
                if (!string.IsNullOrEmpty(document.FileName))
                {
                    doc.Add(new Field("fileName", document.FileName, Field.Store.YES, Field.Index.ANALYZED)); //存储且索引
                }
                if (!string.IsNullOrEmpty(document.Extension))
                {
                    doc.Add(new Field("extendType", document.Extension, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                }
                doc.Add(new Field("fileType", document.DocumentTypeId.ToString(CultureInfo.InvariantCulture), Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                if (!string.IsNullOrEmpty(document.Uploader))
                {
                    doc.Add(new Field("uploader", document.Uploader, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                }
                if (!string.IsNullOrEmpty(document.FileContent))
                {
                    doc.Add(new Field("fileContent", document.FileContent, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                }
                if (!string.IsNullOrEmpty(document.Abstract))
                {
                    doc.Add(new Field("abstract", document.Abstract, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
                }
                IndexManager.IndexWriter.AddDocument(doc);
            }
            catch (FileNotFoundException fnfe)
            {
                throw fnfe;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 创建索引
        /// </summary>
        /// <param name="pKID">文档ID</param>
        public static void DeleteIndex(string pKID)
        {
            var term = new Term("ID", pKID);
            IndexManager.IndexWriter.DeleteDocuments(term);
            IndexManager.IndexWriter.Commit();
        }

        /// <summary>
        /// 关闭索引但不优化
        /// </summary>
        public static void CloseWithoutOptimize()
        {
            IndexManager.IndexWriter.Close();
        }

        /// <summary>
        /// 关闭索引且优化
        /// </summary>
        public static void CloseWithOptimize()
        {
            IndexManager.IndexWriter.Optimize();
            IndexManager.IndexWriter.Close();
        }
    }
}
