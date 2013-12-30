#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:49:18
// 文件名：EnginePlanRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     备发计划仓储实现
    /// </summary>
    public class EnginePlanRepository : Repository<EnginePlan>, IEnginePlanRepository
    {
        public EnginePlanRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
