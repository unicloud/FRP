#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/9/17 21:24:37
// 文件名：DocIndexService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;

#endregion

namespace UniCloud.Application.CommonServiceBC.DocumnetSearch.LuceneSearch
{
    public class DocumentIndexService
    {
        /// <summary>
        /// 创建文档搜索索引
        /// </summary>
        /// <returns></returns>
        public void AddDocumentSearchIndex(Document document)
        {
            IndexManager.SetIndexStorePath("E:\\Indexs\\" + document.DocumentTypeId);
            IndexManager.GenerateWriter();
            IndexManager.MaxMergeFactor = 301;
            IndexManager.MinMergeDocs = 301;

            LuceneIndex.CreateDocumentIndex(document);

            LuceneIndex.CloseWithOptimize();

        }

        /// <summary>
        /// 删除文档搜索索引
        /// </summary>
        /// <returns></returns>
        public void DeleteDocumentSearchIndex(Document document)
        {
            IndexManager.SetIndexStorePath("E:\\Indexs\\" + document.DocumentTypeId);
            IndexManager.GenerateWriter();
            LuceneIndex.DeleteIndex(document.Id.ToString());
            LuceneIndex.CloseWithOptimize();
        }

        /// <summary>
        /// 更新文档搜索索引
        /// </summary>
        /// <returns></returns>
        public void UpdateDocumentSearchIndex(Document document)
        {
            DeleteDocumentSearchIndex(document);
            AddDocumentSearchIndex(document);
        }
    }
}
