#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/11 17:03:23
// 文件名：PurchaseContractEngineQuery
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
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.ContractEngineQueries
{
    /// <summary>
    /// 采购合同发动机查询实现
    /// </summary>
    public class PurchaseContractEngineQuery : IPurchaseContractEngineQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public PurchaseContractEngineQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    采购合同发动机查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>采购合同发动机DTO集合。</returns>
        public IQueryable<PurchaseContractEngineDTO> PurchaseContractEngineDTOQuery(
            QueryBuilder<PurchaseContractEngine> query)
        {
            var suppliers = _unitOfWork.CreateSet<Supplier>();
            var orders = _unitOfWork.CreateSet<Order>().OfType<EnginePurchaseOrder>();
            var trades = _unitOfWork.CreateSet<Trade>();
            return
                query.ApplyTo(_unitOfWork.CreateSet<PurchaseContractEngine>())
            .Select(p => new PurchaseContractEngineDTO
            {
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
                SupplierId = suppliers.FirstOrDefault(q => q.Id == trades.FirstOrDefault(l => l.Id == orders.FirstOrDefault(r => r.ContractNumber == p.ContractNumber).TradeId).SupplierId).Id,
            });
        }

    }
}
