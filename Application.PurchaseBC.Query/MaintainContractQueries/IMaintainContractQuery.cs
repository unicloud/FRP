#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/15 17:24:35
// 文件名：IMaintainContractQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.MaintainContractQueries
{
    public interface IMaintainContractQuery
    {
        /// <summary>
        ///     发动机维修合同查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>发动机维修合同DTO集合</returns>
        IQueryable<EngineMaintainContractDTO> EngineMaintainContractDTOQuery(
            QueryBuilder<MaintainContract> query);

        /// <summary>
        ///     APU维修合同查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>APU维修合同DTO集合</returns>
        IQueryable<APUMaintainContractDTO> APUMaintainContractDTOQuery(
            QueryBuilder<MaintainContract> query);

        /// <summary>
        ///     起落架维修合同查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>起落架维修合同DTO集合</returns>
        IQueryable<UndercartMaintainContractDTO> UndercartMaintainContractDTOQuery(
            QueryBuilder<MaintainContract> query);

        /// <summary>
        ///     机身维修合同查询
        /// </summary>
        /// <param name="query">查询表达式</param>
        /// <returns>机身维修合同DTO集合</returns>
        IQueryable<AirframeMaintainContractDTO> AirframeMaintainContractDTOQuery(
            QueryBuilder<MaintainContract> query);
    }
}
