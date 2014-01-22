#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 9:49:28
// 文件名：DocumentTypeRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 9:49:28
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.CommonServiceBC.Repositories
{
   /// <summary>
    ///     文档类型仓储实现
    /// </summary>
    public class DocumentTypeRepository: Repository<DocumentType>, IDocumentTypeRepository
    {
        public DocumentTypeRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
