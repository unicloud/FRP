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
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.DocumentQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.DocumentPathAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.DocumentPathServices
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class DocumentPathAppService : ContextBoundObject, IDocumentPathAppService
    {
        private readonly IDocumentPathQuery _documentPathQuery;
        private readonly IDocumentPathRepository _documentPathRepository; //文档路径仓储

        public DocumentPathAppService(IDocumentPathQuery documentPathQuery,
            IDocumentPathRepository documentPathRepository)
        {
            _documentPathQuery = documentPathQuery;
            _documentPathRepository = documentPathRepository;
        }

        public IQueryable<DocumentPathDTO> GetDocumentPaths()
        {
            var queryBuilder =
                new QueryBuilder<DocumentPath>();
            return _documentPathQuery.DocumentPathsQuery(queryBuilder);
        }

        public IEnumerable<DocumentPathDTO> SearchDocumentPath(int documentPathId, string name)
        {
            return _documentPathQuery.SearchDocumentPath(documentPathId, name);
        }
        /// <summary>
        ///     删除文档路径
        /// </summary>
        /// <param name="documentPathId"></param>
        public void DelDocPath(int documentPathId)
        {
            var docPath = _documentPathRepository.Get(documentPathId);
            DelSubDocumentPath(docPath);
        }

        public void AddDocPath(string name, string isLeaf, string documentId, int parentId)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("文件名称不能为空");
            }
            var docPathIsLeaf = bool.Parse(isLeaf); //是否是文件夹
            string extension = null;
            if (docPathIsLeaf)
            {
                extension = name.Substring(name.LastIndexOf('.') + 1); //如果是文件，获取文件夹名称
            }
            Guid? docPathId = null;
            if (!string.IsNullOrEmpty(documentId))
            {
                docPathId = Guid.Parse(documentId);
            }
            var parent = _documentPathRepository.Get(parentId);

            var newDocumentPath = DocumentPathFactory.CreateDocumentPath(name, docPathIsLeaf, extension,
                docPathId, parentId, parent.Path + "\\" + name);
            _documentPathRepository.Add(newDocumentPath);
            _documentPathRepository.UnitOfWork.Commit();
        }

        public void ModifyDocPath(int documentPathId, string name)
        {
            var documentPath = _documentPathRepository.Get(documentPathId);
            DocumentPathFactory.ModifyDocumentPath(documentPath, name);
            _documentPathRepository.Modify(documentPath);
            _documentPathRepository.UnitOfWork.Commit();
        }

        /// <summary>
        ///     删除子项文档
        /// </summary>
        private void DelSubDocumentPath(DocumentPath documentPath)
        {
            _documentPathRepository.Remove(documentPath);
            _documentPathRepository.UnitOfWork.Commit();
        }
    }
}