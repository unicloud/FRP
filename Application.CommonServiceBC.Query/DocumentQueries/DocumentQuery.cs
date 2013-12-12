﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/10，10:12
// 文件名：DocumentQuery.cs
// 程序集：UniCloud.Application.CommonServiceBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentPathAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.CommonServiceBC.Query.DocumentQueries
{
    public class DocumentQuery : IDocumentQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public DocumentQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<DocumentDTO> DocumentsQuery(QueryBuilder<Document> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Document>()).Select(p => new DocumentDTO
                {
                    DocumentId = p.Id,
                    Abstract = p.Abstract,
                    CreateTime = p.CreateTime,
                    Extension = p.Extension,
                    IsValid = p.IsValid,
                    Name = p.FileName,
                    Note = p.Note,
                    Uploader = p.Uploader,
                    FileStorage = p.FileStorage
                });
        }
        public IQueryable<DocumentPathDTO> DocumentPathsQuery(QueryBuilder<DocumentPath> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<DocumentPath>()).Select(p => new DocumentPathDTO
                {
                    DocumentPathId = p.Id,
                    DocumentGuid = p.DocumentGuid,
                    Name = p.Name,
                    Extension = p.Extension,
                    IsLeaf = p.IsLeaf,
                    ParentId = p.ParentId,
                    PathSource = (int) p.PathSource,
                    SubDocumentPaths = p.DocumentPaths.Select(c => new SubDocumentPathDTO
                        {
                            SubDocumentPathId = c.Id,
                            DocumentGuid = c.DocumentGuid,
                            Name = c.Name,
                            Extension = c.Extension,
                            IsLeaf = c.IsLeaf,
                            ParentId = c.ParentId,
                            PathSource = (int)c.PathSource
                        }).ToList(),
                });
        }
    }
}