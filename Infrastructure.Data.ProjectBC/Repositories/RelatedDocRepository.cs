#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，16:35
// 方案：FRP
// 项目：Infrastructure.Data.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.ProjectBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.Repositories
{
    /// <summary>
    ///     关联文档仓储接口
    /// </summary>
    public class RelatedDocRepository : Repository<RelatedDoc>, IRelatedDocRepository
    {
        public RelatedDocRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}