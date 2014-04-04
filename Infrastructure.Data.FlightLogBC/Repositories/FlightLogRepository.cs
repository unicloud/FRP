#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/23 16:12:25
// 文件名：FlightLogRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.FlightLogBC.Aggregates.FlightLogAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FlightLogBC.Repositories
{
    /// <summary>
    ///     飞行日志仓储实现
    /// </summary>
    public class FlightLogRepository : Repository<FlightLog>, IFlightLogRepository
    {
        public FlightLogRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
