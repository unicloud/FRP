#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：IContractAircraftQuery
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
#endregion

namespace UniCloud.Application.PartBC.Query.ContractAircraftQueries
{
    /// <summary>
    /// ContractAircraft查询接口
    /// </summary>
    public interface IContractAircraftQuery
    {
        /// <summary>
        /// ContractAircraft查询。
        /// </summary>
        /// <param name="query">查询表达式</param>
        ///  <returns>ContractAircraftDTO集合</returns>
        IQueryable<ContractAircraftDTO> ContractAircraftDTOQuery(QueryBuilder<ContractAircraft> query);
    }
}
