﻿
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;

namespace UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries
{
    /// <summary>
    /// 租赁合同飞机查询接口
    /// </summary>
    public interface ILeaseContractAircraftQuery
    {
        /// <summary>
        ///    租赁合同飞机查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>租赁合同飞机DTO集合。</returns>
        IQueryable<LeaseContractAircraftDTO> LeaseContractAircraftDTOQuery(
            QueryBuilder<LeaseContractAircraft> query);
    }
}
