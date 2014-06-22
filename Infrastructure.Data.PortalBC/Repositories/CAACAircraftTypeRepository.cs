#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/06/21，10:06
// 方案：FRP
// 项目：Infrastructure.Data.PortalBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.PortalBC.Aggregates.CAACAircraftTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PortalBC.Repositories
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