#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 15:05:34
// 文件名：AircraftLicenseRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 15:05:34
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories
{
   /// <summary>
    ///   飞机证照仓储实现
    /// </summary>
    public  class AircraftLicenseRepository: Repository<AircraftLicense>, IAircraftLicenseRepository
    {
        public AircraftLicenseRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
