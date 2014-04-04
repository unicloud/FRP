#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 9:25:34
// 文件名：CAACAircraftTypeRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 9:25:34
// 修改说明：
// ========================================================================*/
#endregion

using UniCloud.Domain.AircraftConfigBC.Aggregates.CAACAircraftTypeAgg;

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.Repositories
{
   /// <summary>
    ///     民航机型仓储实现
    /// </summary>
    public  class CAACAircraftTypeRepository: Repository<CAACAircraftType>, ICAACAircraftTypeRepository
    {
        public CAACAircraftTypeRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
