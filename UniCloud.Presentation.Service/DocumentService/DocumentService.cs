#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/26 10:38:15
// 文件名：DocumentService
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
    public class DocumentClient : IDocumentService
    {
        #region Private Fields

        private static DocumentClient _instance;
        private readonly DocumentServiceClient _documentService;

        #endregion

        #region Ctor

        public DocumentClient()
        {
            _documentService = new DocumentServiceClient();
        }

        #endregion

        #region Properities

        /// <summary>
        ///     Contract服务实例
        /// </summary>
        public static DocumentClient Instance
        {
            get { return _instance ?? (_instance = new DocumentClient()); }
        }

        #endregion


        public void GetDocumentDataObjectByDocId(Guid key, EventHandler<GetDocumentDataObjectByDocIDCompletedEventArgs> callback)
        {
            _documentService.GetDocumentDataObjectByDocIDAsync(key);
            _documentService.GetDocumentDataObjectByDocIDCompleted -= callback;
            _documentService.GetDocumentDataObjectByDocIDCompleted += callback;
        }

        public void CommitDocument(ResultDataStandardDocumentDataObject resultData, EventHandler<CommitStandardDocumentDTOCompletedEventArgs> callback)
        {
            _documentService.CommitStandardDocumentDTOAsync(resultData);
            _documentService.CommitStandardDocumentDTOCompleted -= callback;
            _documentService.CommitStandardDocumentDTOCompleted += callback;
        }


        public void GetDocumentFileStream(Guid docId,
                                          EventHandler<GetDocumentFileStreamCompletedEventArgs> callback)
        {
            _documentService.GetDocumentFileStreamAsync(docId);
            _documentService.GetDocumentFileStreamCompleted -= callback;
            _documentService.GetDocumentFileStreamCompleted += callback;
        }
    }
}
