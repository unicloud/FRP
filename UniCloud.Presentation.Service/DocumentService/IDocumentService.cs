#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/26 10:37:58
// 文件名：IDocumentService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

using System;
using UniCloud.Presentation.Service.Document;

namespace UniCloud.Presentation.Service.DocumentService
{
    /// <summary>
    /// 文档接口
    /// </summary>
    public interface IDocumentService
    {
        /// <summary>
        /// 根据ID获取文档
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <param name="key"></param>
        void GetDocumentDataObjectByDocId(Guid key, EventHandler<GetDocumentDataObjectByDocIDCompletedEventArgs> callback);

        /// <summary>
        /// 提交要更改的文档
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <param name="resultData"></param>
        void CommitDocument(ResultDataStandardDocumentDataObject resultData, EventHandler<CommitStandardDocumentDTOCompletedEventArgs> callback);

        /// <summary>
        /// 获取文档流
        /// </summary>
        /// <param name="callback">回调函数</param>
        /// <param name="docId">文档ID</param>
        void GetDocumentFileStream(Guid docId, EventHandler<GetDocumentFileStreamCompletedEventArgs> callback);

    }
}
