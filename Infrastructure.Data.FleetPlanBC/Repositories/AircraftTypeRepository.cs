#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 9:47:54
// 文件名：AircraftTypeRepository
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.Repositories
{
    /// <summary>
    ///     机型仓储实现
    /// </summary>
    public class AircraftTypeRepository : Repository<AircraftType>, IAircraftTypeRepository
    {
        public AircraftTypeRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        #region 方法重载

        #endregion
    }
}
