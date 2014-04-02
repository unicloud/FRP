#region 命名空间

using System;
using Lucene.Net.Documents;
using Lucene.Net.Index;

#endregion

namespace UniCloud.Application.LuceneSearch
{
    public static class LuceneIndex
    {
        /// <summary>
        /// 创建Document索引
        /// </summary>
        public static void CreateDocumentIndex(Guid documentId, string fileName, string fileContent)
        {
            var doc = new Document();//Document为索引文档，可以理解成数据库里的记录
            doc.Add(new Field("pKID", documentId.ToString(), Field.Store.YES, Field.Index.ANALYZED));//存储且索引
            doc.Add(new Field("ID", documentId.ToString(), Field.Store.YES, Field.Index.NOT_ANALYZED));//存储且索引
            if (!string.IsNullOrEmpty(fileName))
            {
                doc.Add(new Field("fileName", fileName, Field.Store.YES, Field.Index.ANALYZED)); //存储且索引
            }
            if (!string.IsNullOrEmpty(fileContent))
            {
                doc.Add(new Field("fileContent", fileContent, Field.Store.YES, Field.Index.ANALYZED));//存储且索引
            }
               
            IndexManager.IndexWriter.AddDocument(doc);
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
