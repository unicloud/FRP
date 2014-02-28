#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/28，9:44
// 方案：FRP
// 项目：Infrastructure.Data.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using UniCloud.Domain.PartBC.Aggregates.OilUserAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    ///     滑油用户仓储实现
    /// </summary>
    public class OilUserRepository : Repository<OilUser>, IOilUserRepository
    {
        public OilUserRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}