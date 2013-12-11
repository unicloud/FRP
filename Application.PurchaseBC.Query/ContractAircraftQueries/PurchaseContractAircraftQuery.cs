#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/12/3 14:40:26
// 文件名：PurchaseContractAircraftQuery
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
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.TradeAgg;
using UniCloud.Infrastructure.Data;
#endregion

namespace UniCloud.Application.PurchaseBC.Query.ContractAircraftQueries
{
    /// <summary>
    /// 采购合同飞机查询
    /// </summary>
    public class PurchaseContractAircraftQuery : IPurchaseContractAircraftQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;
        public PurchaseContractAircraftQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        ///    采购合同飞机查询
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>采购合同飞机DTO集合。</returns>
        public IQueryable<PurchaseContractAircraftDTO> PurchaseContractAircraftDTOQuery(
            QueryBuilder<PurchaseContractAircraft> query)
        {
            var suppliers = _unitOfWork.CreateSet<Supplier>();
            var orders = _unitOfWork.CreateSet<Order>().OfType<AircraftPurchaseOrder>();
            var trades = _unitOfWork.CreateSet<Trade>();
            return
            query.ApplyTo(_unitOfWork.CreateSet<ContractAircraft>().OfType<PurchaseContractAircraft>())
                     .Select(p => new PurchaseContractAircraftDTO
                     {
                         PurchaseContractAircraftId = p.Id,
                         ContractName = p.ContractName,
                         ContractNumber = p.ContractNumber,
                         RankNumber = p.RankNumber,
                         CSCNumber = p.CSCNumber,
                         SerialNumber = p.SerialNumber,
                         IsValid = p.IsValid,
                         ReceivedAmount = p.ReceivedAmount,
                         AcceptedAmount = p.AcceptedAmount,
                         AircraftTypeId = p.AircraftTypeId,
                         AircraftTypeName = p.AircraftType.Name,
                         PlanAircraftID = p.PlanAircraftID,
                         PlanAircraft = p.PlanAircraft.RegNumber,
                         ImportCategoryId = p.ImportCategoryId,
                         ImportType = p.ImportCategory.ActionType,
                         ImportActionName = p.ImportCategory.ActionName,
                         SupplierId = suppliers.FirstOrDefault(q => q.Id == trades.FirstOrDefault(l => l.Id == orders.FirstOrDefault(r => r.ContractNumber == p.ContractNumber).TradeId).SupplierId).Id,
                     });
        }
    }
}
