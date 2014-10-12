#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:34:47
// 文件名：AircraftSeriesRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories
{
    /// <summary>
    ///     飞机系列仓储实现
    /// </summary>
    public class AircraftSeriesRepository : Repository<AircraftSeries>, IAircraftSeriesRepository
    {
        public AircraftSeriesRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
