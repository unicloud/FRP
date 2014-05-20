#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 15:51:12
// 文件名：ThresholdRepository
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 15:51:12
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.PartBC.Aggregates.ThresholdAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// Threshold仓储实现
    /// </summary>
    public class ThresholdRepository : Repository<Threshold>, IThresholdRepository
    {
        public ThresholdRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion
    }
}
