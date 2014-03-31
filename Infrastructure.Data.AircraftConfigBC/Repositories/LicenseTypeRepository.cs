#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 15:04:27
// 文件名：LicenseTypeRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 15:04:27
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories
{
    /// <summary>
    ///     证照类型仓储实现
    /// </summary>
    public class LicenseTypeRepository : Repository<LicenseType>, ILicenseTypeRepository
    {
        public LicenseTypeRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
