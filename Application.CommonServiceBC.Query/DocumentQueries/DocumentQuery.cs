#region 版本信息

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

using System;
using System.Linq;
using System.Linq.Expressions;
using UniCloud.Application.CommonServiceBC.DTO;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;
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
                    DocumentTypeId = p.DocumentTypeId,
                    Uploader = p.Uploader,
                    UpdateTime = p.UpdateTime
                    //FileStorage = p.FileStorage
                });
        }

        public IQueryable<DocumentDTO> DocumentsQueryWithContent(QueryBuilder<Document> query)
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
                DocumentTypeId = p.DocumentTypeId,
                Uploader = p.Uploader,
                UpdateTime = p.UpdateTime,
                FileStorage = p.FileStorage
            });
        }

        public DocumentDTO GetSingleDocumentQuery(Expression<Func<Document, bool>> source)
        {
            var document = _unitOfWork.CreateSet<Document>().FirstOrDefault(source);
            if (document == null) return null;

            return new DocumentDTO
                {
                    DocumentId = document.Id,
                    Abstract = document.Abstract,
                    CreateTime = document.CreateTime,
                    Extension = document.Extension,
                    IsValid = document.IsValid,
                    Name = document.FileName,
                    Note = document.Note,
                    DocumentTypeId = document.DocumentTypeId,
                    Uploader = document.Uploader,
                    UpdateTime = document.UpdateTime,
                    FileStorage = document.FileStorage
                };
        }
        public IQueryable<DocumentTypeDTO> DocumentTypesQuery(QueryBuilder<DocumentType> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<DocumentType>()).Select(p => new DocumentTypeDTO
            {
                DocumentTypeId = p.Id,
                Name = p.Name,
                Description = p.Description
            });
        }
    }
}