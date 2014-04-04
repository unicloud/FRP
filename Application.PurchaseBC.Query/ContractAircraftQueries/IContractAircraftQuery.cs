#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/4 10:41:22
// 文件名：IContractAircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries
{
    public interface IContractAircraftQuery
    {

        /// <summary>
        ///    所有合同飞机查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>合同飞机DTO集合。</returns>
        IQueryable<ContractAircraftDTO> ContractAircraftDTOQuery(
            QueryBuilder<ContractAircraft> query);

    }
}
