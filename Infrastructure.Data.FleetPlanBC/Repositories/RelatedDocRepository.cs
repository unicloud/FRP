#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/25 16:00:52
// 文件名：RelatedDocRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using UniCloud.Domain.FleetPlanBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     关联文档仓储实现
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