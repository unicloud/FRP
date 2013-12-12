#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:03:53
// 文件名：IPurchaseContractEngineQuery
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ContractEngineQueries
{
    /// <summary>
    /// 采购合同发动机查询接口
    /// </summary>
    public interface IPurchaseContractEngineQuery
    {

        /// <summary>
        ///    采购合同发动机查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>采购合同发动机DTO集合。</returns>
        IQueryable<PurchaseContractEngineDTO> PurchaseContractEngineDTOQuery(
            QueryBuilder<PurchaseContractEngine> query);

    }
}
