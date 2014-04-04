#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 17:15:37
// 文件名：AdSbRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 17:15:37
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using UniCloud.Domain.PartBC.Aggregates.AdSbAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// AdSb仓储实现
    /// </summary>
    public class AdSbRepository: Repository<AdSb>, IAdSbRepository
    {
        public AdSbRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
    }
}
