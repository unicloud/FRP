#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/06，10:12
// 文件名：MaterialAppService.cs
// 程序集：UniCloud.Application.CommonServiceBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Application.CommonServiceBC.Query.DocumentQueries;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentPathAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;

#endregion

namespace UniCloud.Application.CommonServiceBC.DocumentServices
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class DocumentAppService : IDocumentAppService
    {
        private readonly IDocumentPathRepository _documentPathRepository; //文档路径仓储
        private readonly IDocumentQuery _documentQuery;
        private readonly IDocumentRepository _documentRepository; //文档仓储
        private readonly IDocumentTypeRepository _documentTypeRepository;//文档类型仓储
        public DocumentAppService(IDocumentQuery documentQuery, IDocumentPathRepository documentPathRepository,
                                  IDocumentRepository documentRepository, IDocumentTypeRepository documentTypeRepository)
        {
            _documentQuery = documentQuery;
            _documentPathRepository = documentPathRepository;
            _documentRepository = documentRepository;
            _documentTypeRepository = documentTypeRepository;
        }

        #region DocumnetDTO
        public IQueryable<DocumentDTO> GetDocuments()
        {
            var queryBuilder =
                new QueryBuilder<Document>();
            return _documentQuery.DocumentsQuery(queryBuilder);
        }

        [Insert(typeof(DocumentDTO))]
        public void InsertDocument(DocumentDTO document)
        {
            if (document == null)
            {
                throw new Exception("文档不能为空");
            }
            //新建文档
            var newDocument = DocumentFactory.CreateStandardDocument(document.DocumentId, document.Name,
                                                                     document.Extension,
                                                                     document.Abstract, document.Note, document.Uploader,
                                                                     document.IsValid, document.FileStorage);
            _documentRepository.Add(newDocument);
        }

        /// <summary>
        ///     更新文档。
        /// </summary>
        /// <param name="document">文档DTO。</param>
        [Update(typeof(DocumentDTO))]
        public void ModifyDocument(DocumentDTO document)
        {
            if (document == null)
            {
                throw new Exception("文档不能为空");
            }
            var updateDocument = _documentRepository.Get(document.DocumentId);
            DocumentFactory.UpdateDocument(updateDocument, document.Name, document.Extension, document.Abstract,
                document.Note, document.Uploader, document.IsValid, document.FileStorage);
            _documentRepository.Modify(updateDocument);
        }

        /// <summary>
        ///     删除文档。
        /// </summary>
        /// <param name="document">文档DTO。</param>
        [Delete(typeof(DocumentDTO))]
        public void DeleteDocument(DocumentDTO document)
        {
            if (document == null)
            {
                throw new Exception("文档不能为空");
            }
            var deleteDocument = _documentRepository.Get(document.DocumentId);
            _documentRepository.Remove(deleteDocument);
        }
        #endregion

        #region DocumnetDTO
        public IQueryable<DocumentTypeDTO> GetDocumentTypes()
        {
            var queryBuilder =
                new QueryBuilder<DocumentType>();
            return _documentQuery.DocumentTypesQuery(queryBuilder);
        }

        [Insert(typeof(DocumentTypeDTO))]
        public void InsertDocumentType(DocumentTypeDTO documentType)
        {
            if (documentType == null)
            {
                throw new Exception("文档不能为空");
            }
            //新建文档
            var newDocumentType = DocumentTypeFactory.CreateDocumentType();
            DocumentTypeFactory.SetDocumentType(newDocumentType, documentType.Name, documentType.Description);
            _documentTypeRepository.Add(newDocumentType);
        }

        /// <summary>
        ///     更新文档。
        /// </summary>
        /// <param name="documentType">文档DTO。</param>
        [Update(typeof(DocumentTypeDTO))]
        public void ModifyDocumentType(DocumentTypeDTO documentType)
        {
            if (documentType == null)
            {
                throw new Exception("文档不能为空");
            }
            var updateDocumentType = _documentTypeRepository.Get(documentType.DocumentTypeId);
            DocumentTypeFactory.SetDocumentType(updateDocumentType, documentType.Name, documentType.Description);
            _documentTypeRepository.Modify(updateDocumentType);
        }

        /// <summary>
        ///     删除文档。
        /// </summary>
        /// <param name="documentType">文档DTO。</param>
        [Delete(typeof(DocumentTypeDTO))]
        public void DeleteDocumentType(DocumentTypeDTO documentType)
        {
            if (documentType == null)
            {
                throw new Exception("文档不能为空");
            }
            var deleteDocumentType = _documentTypeRepository.Get(documentType.DocumentTypeId);
            _documentTypeRepository.Remove(deleteDocumentType);
        }
        #endregion
    }
}