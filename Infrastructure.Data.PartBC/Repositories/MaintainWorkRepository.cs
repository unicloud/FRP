#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：MaintainWorkRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// MaintainWork仓储实现
    /// </summary>
    public class MaintainWorkRepository : Repository<MaintainWork>, IMaintainWorkRepository
    {
        public MaintainWorkRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion
    }
}
