#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/26 17:34:09
// 文件名：AirBusScnRepository
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/26 17:34:09
// 修改说明：
// ========================================================================*/
#endregion

using System;
using System.Linq;
using UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// AirBusScn仓储实现
    /// </summary>
    public class AirBusScnRepository : Repository<AirBusScn>, IAirBusScnRepository
    {
        public AirBusScnRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }
        public AirBusScn Get(string cscNumber, string scnNumber)
        {
            var currentUnitOfWork = UnitOfWork as PartBCUnitOfWork;
            if (currentUnitOfWork == null) return null;
            var set = currentUnitOfWork.CreateSet<AirBusScn>();
            return set.FirstOrDefault(p => p.CSCNumber.Equals(cscNumber, StringComparison.OrdinalIgnoreCase) && p.ScnNumber.Equals(scnNumber, StringComparison.OrdinalIgnoreCase));
        }

    }
}
