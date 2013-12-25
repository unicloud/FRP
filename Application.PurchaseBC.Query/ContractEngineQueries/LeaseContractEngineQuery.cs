#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:03:11
// 文件名：LeaseContractEngineQuery
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
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ContractEngineQueries
{
    /// <summary>
    /// 租赁合同发动机查询实现
    /// </summary>
    public class LeaseContractEngineQuery : ILeaseContractEngineQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public LeaseContractEngineQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    租赁合同发动机查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>租赁合同发动机DTO集合。</returns>
        public IQueryable<LeaseContractEngineDTO> LeaseContractEngineDTOQuery(
            QueryBuilder<LeaseContractEngine> query)
        {
            return
                query.ApplyTo(_unitOfWork.CreateSet<ContractEngine>().OfType<LeaseContractEngine>())
                     .Select(p => new LeaseContractEngineDTO
                     {
                         LeaseContractEngineId = p.Id,
                         ContractName = p.ContractName,
                         ContractNumber = p.ContractNumber,
                         RankNumber = p.RankNumber,
                         SerialNumber = p.SerialNumber,
                         IsValid = p.IsValid,
                         ReceivedAmount = p.ReceivedAmount,
                         AcceptedAmount = p.AcceptedAmount,
                         ImportCategoryId = p.ImportCategoryId,
                         ImportType = p.ImportCategory.ActionType,
                         ImportActionName = p.ImportCategory.ActionName,
                         SupplierId = p.SupplierId,
                     });
        }

    }
}
