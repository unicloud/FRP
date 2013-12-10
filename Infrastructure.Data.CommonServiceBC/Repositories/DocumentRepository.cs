#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，20:38
// 方案：FRP
// 项目：Infrastructure.Data.CommonServiceBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentAgg;

#endregion

namespace UniCloud.Infrastructure.Data.CommonServiceBC.Repositories
{
    /// <summary>
    ///     文档仓储实现
    /// </summary>
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}