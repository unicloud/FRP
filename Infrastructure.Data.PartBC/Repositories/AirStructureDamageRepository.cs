#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 13:42:42
// 文件名：AirStructureDamageRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 13:42:42
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{

    /// <summary>
    /// AirStructureDamage仓储实现
    /// </summary>
    public class AirStructureDamageRepository : Repository<AirStructureDamage>, IAirStructureDamageRepository
    {
        public AirStructureDamageRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion
    }
}
