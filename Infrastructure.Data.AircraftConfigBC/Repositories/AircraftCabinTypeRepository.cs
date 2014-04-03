#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 21:40:11
// 文件名：AircraftCabinTypeRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 21:40:11
// 修改说明：
// ========================================================================*/
#endregion

using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCabinTypeAgg;

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories
{
    /// <summary>
    ///     飞机舱位类型仓储实现
    /// </summary>
    public class AircraftCabinTypeRepository : Repository<AircraftCabinType>, IAircraftCabinTypeRepository
    {
        public AircraftCabinTypeRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
