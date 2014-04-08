#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 13:53:09
// 文件名：AircraftConfigurationRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 13:53:09
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg;
using UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork;
using System.Data.Entity;
namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories
{
    /// <summary>
    ///     飞机配置仓储实现
    /// </summary>
    public class AircraftConfigurationRepository : Repository<AircraftConfiguration>, IAircraftConfigurationRepository
    {
        public AircraftConfigurationRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载
        public override AircraftConfiguration Get(object id)
        {
            var currentUnitOfWork = UnitOfWork as AircraftConfigBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<AircraftConfiguration>();
            return set.Include(t => t.AircraftCabins).FirstOrDefault(p => p.Id == (int)id);
        }
        #endregion

        /// <summary>
        /// 删除飞机配置
        /// </summary>
        /// <param name="aircraftConfiguration"></param>
        public void DeleteAircraftConfiguration(AircraftConfiguration aircraftConfiguration)
        {
            var currentUnitOfWork = UnitOfWork as AircraftConfigBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var aircraftConfigurations = currentUnitOfWork.CreateSet<AircraftConfiguration>();
            aircraftConfiguration.AircraftCabins.ToList().ForEach(DeleteAircraftCabin);
            aircraftConfigurations.Remove(aircraftConfiguration);
        }

        /// <summary>
        /// 删除舱位
        /// </summary>
        /// <param name="aircraftCabin"></param>
        public void DeleteAircraftCabin(AircraftCabin aircraftCabin)
        {
            var currentUnitOfWork = UnitOfWork as AircraftConfigBCUnitOfWork;
            if (currentUnitOfWork == null) return;
            var aircraftCabins = currentUnitOfWork.CreateSet<AircraftCabin>();
            aircraftCabins.Remove(aircraftCabin);
        }

    }
}
