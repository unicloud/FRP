#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/25 13:57:26
// 文件名：CAACAircraftTypeRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using UniCloud.Domain.FleetPlanBC.Aggregates.CAACAircraftTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     民航机型仓储实现
    /// </summary>
    public class CAACAircraftTypeRepository : Repository<CAACAircraftType>, ICAACAircraftTypeRepository
    {
        public CAACAircraftTypeRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}