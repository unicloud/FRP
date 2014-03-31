#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ContractAircraftRepository
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Repositories
{
    /// <summary>
    /// ContractAircraft仓储实现
    /// </summary>
    public class ContractAircraftRepository : Repository<ContractAircraft>, IContractAircraftRepository
    {
        public ContractAircraftRepository(IQueryableUnitOfWork unitOfWork)
            : base(unitOfWork)
        {

        }

        #region 方法重载
        #endregion
    }
}
