#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 13:54:52

// 文件名：MScnRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using UniCloud.Domain.PartBC.Aggregates.MScnAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// MScn仓储实现
    /// </summary>
    public class MScnRepository : Repository<MScn>, IMScnRepository
    {
        public MScnRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion
    }
}
