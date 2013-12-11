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
using UniCloud.Domain.CommonServiceBC.Enums;

#endregion

namespace UniCloud.Application.CommonServiceBC.DocumentServices
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class DocumentAppService : IDocumentAppService
    {
        private readonly IDocumentQuery _documentQuery;
        private readonly IDocumentPathRepository _documentPathRepository;//文档路径仓储
        private readonly IDocumentRepository _documentRepository;//文档仓储
        public DocumentAppService(IDocumentQuery documentQuery, IDocumentPathRepository documentPathRepository, IDocumentRepository documentRepository)
        {
            _documentQuery = documentQuery;
            _documentPathRepository = documentPathRepository;
            _documentRepository = documentRepository;
        }

        public IQueryable<DocumentDTO> GetDocuments()
        {
            var queryBuilder =
                new QueryBuilder<Document>();
            return _documentQuery.DocumentsQuery(queryBuilder);
        }

        [Insert(typeof(DocumentDTO))]
        public void InsertLinkman(DocumentDTO document)
        {
            if (document == null)
            {
                throw new Exception("文档不能为空");
            }
            //新建文档
            var newDocument = DocumentFactory.CreateStandardDocument(document.DocumentId, document.Name, document.Extension,
                                                                     document.Abstract, document.Note, document.Uploader,
                                                                     true, document.FileStorage);
            _documentRepository.Add(newDocument);
        }


        public IQueryable<DocumentPathDTO> GetDocumentPaths()
        {
            var queryBuilder =
                new QueryBuilder<DocumentPath>();
            return _documentQuery.DocumentPathsQuery(queryBuilder);
        }
        [Insert(typeof(DocumentPathDTO))]
        public void InsertDocumentPath(DocumentPathDTO documentPath)
        {
            if (documentPath == null)
            {
                throw new Exception("文档路径不能为空");
            }
            var newDocumentPath = DocumentPathFactory.CreateDocumentPath(documentPath.Name, documentPath.IsLeaf, documentPath.Extension,
                                                     documentPath.DocumentGuid, documentPath.ParentId,
                                                     (PathSource)documentPath.PathSource);
            _documentPathRepository.Add(newDocumentPath);
        }
        [Update(typeof(DocumentPathDTO))]
        public void ModifyLinkman(DocumentPathDTO documentPath)
        {
            if (documentPath == null)
            {
                throw new Exception("文档路径不能为空");
            }
            var pesistDocumentPath = _documentPathRepository.Get(documentPath.DocumentPathId);
            if (pesistDocumentPath == null)
            {
                throw new Exception("未找到文档路径");
            }
            pesistDocumentPath.Update(documentPath.Name, documentPath.IsLeaf, documentPath.Extension, documentPath.DocumentGuid, documentPath.ParentId, (PathSource)documentPath.PathSource);
            _documentPathRepository.Modify(pesistDocumentPath);
        }
        [Delete(typeof(DocumentPathDTO))]
        public void DeleteLinkman(DocumentPathDTO documentPath)
        {
            if (documentPath == null)
            {
                throw new Exception("文档路径不能为空");
            }
            var pesistDocumentPath = _documentPathRepository.Get(documentPath.DocumentPathId);
            if (pesistDocumentPath == null)
            {
                throw new Exception("未找到文档路径");
            }
            _documentPathRepository.Remove(pesistDocumentPath);
        }
    }
}