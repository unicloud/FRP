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

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.DocumentPathAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.DocumentQueries
{
    public class DocumentPathQuery : IDocumentPathQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public DocumentPathQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
                    Path = p.Path,
                    SubDocumentPaths = p.DocumentPaths.Select(c => new SubDocumentPathDTO
                        {
                            SubDocumentPathId = c.Id,
                            DocumentGuid = c.DocumentGuid,
                            Name = c.Name,
                            Extension = c.Extension,
                            IsLeaf = c.IsLeaf,
                            ParentId = c.ParentId,
                            Path = c.Path
                        }).ToList(),
                });
        }

        public IEnumerable<DocumentPathDTO> SearchDocumentPath(string name)
        {
           return _unitOfWork.CreateSet<DocumentPath>().Where(p => p.Name.Contains(name)).Select(p => new DocumentPathDTO
                                                                                                {
                                                                                                    DocumentPathId =
                                                                                                        p.Id,
                                                                                                    DocumentGuid =
                                                                                                        p.DocumentGuid,
                                                                                                    Name = p.Name,
                                                                                                    Extension =
                                                                                                        p.Extension,
                                                                                                    IsLeaf = p.IsLeaf,
                                                                                                    ParentId =
                                                                                                        p.ParentId,
                                                                                                    Path = p.Path
                                                                                                });
        }
    }
}