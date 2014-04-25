﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 17:35:32
// 文件名：SnHistoryRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    ///     序号件装机历史仓储实现
    /// </summary>
    public class SnHistoryRepository : Repository<SnHistory>, ISnHistoryRepository
    {
        public SnHistoryRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}