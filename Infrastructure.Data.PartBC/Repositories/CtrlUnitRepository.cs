#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:58

// 文件名：CtrlUnitRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// CtrlUnit仓储实现
    /// </summary>
    public class CtrlUnitRepository : Repository<CtrlUnit>, ICtrlUnitRepository
    {
        public CtrlUnitRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion
    }
}
