#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，20:45
// 方案：FRP
// 项目：Infrastructure.Data.CommonServiceBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Entity;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentPathAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;
using UniCloud.Domain.CommonServiceBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork
{
    public class CommonServiceBCUnitOfWork : UniContext<CommonServiceBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<DocumentPath> _documentPaths;
        private IDbSet<DocumentType> _documentTypes;
        private IDbSet<Document> _documents;
        private IDbSet<RelatedDoc> _relatedDocs;

        public IDbSet<Document> Documents
        {
            get { return _documents ?? (_documents = Set<Document>()); }
        }

        public IDbSet<DocumentPath> DocumentPaths
        {
            get { return _documentPaths ?? (_documentPaths = Set<DocumentPath>()); }
        }

        public IDbSet<RelatedDoc> RelatedDocs
        {
            get { return _relatedDocs ?? (_relatedDocs = Set<RelatedDoc>()); }
        }

        public IDbSet<DocumentType> DouDocumentTypes
        {
            get { return _documentTypes ?? (_documentTypes = Set<DocumentType>()); }
        }

        #endregion
    }
}