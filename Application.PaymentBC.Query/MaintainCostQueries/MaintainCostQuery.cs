#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 13:41:57
// 文件名：MaintainCostQuery
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 13:41:57
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg;

#endregion

namespace UniCloud.Application.PaymentBC.Query.MaintainCostQueries
{
    public class MaintainCostQuery : IMaintainCostQuery
    {
        private readonly IMaintainCostRepository _maintainCostRepository;

        public MaintainCostQuery(IMaintainCostRepository maintainCostRepository)
        {
            _maintainCostRepository = maintainCostRepository;
        }

        /// <summary>
        ///     发票查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>发票DTO集合。</returns>
        public IQueryable<RegularCheckMaintainCostDTO> RegularCheckMaintainCostDTOQuery(
           QueryBuilder<RegularCheckMaintainCost> query)
        {
            return
                query.ApplyTo(_maintainCostRepository.GetAll().OfType<RegularCheckMaintainCost>())
                    .Select(p => new RegularCheckMaintainCostDTO
                    {
                        Id = p.Id,
                        ActionCategoryId = p.ActionCategoryId,
                        AcutalAmount = p.MaintainInvoice.PaidAmount,
                        AcutalBudgetAmount = p.MaintainInvoice.InvoiceValue,
                        AcutalInMaintainTime = p.MaintainInvoice.InMaintainTime,
                        AcutalOutMaintainTime = p.MaintainInvoice.OutMaintainTime,
                        AircraftId = p.AircraftId,
                        AircraftTypeId = p.AircraftTypeId,
                        DepartmentDeclareAmount = p.DepartmentDeclareAmount,
                        FinancialApprovalAmount = p.FinancialApprovalAmount,
                        FinancialApprovalAmountNonTax = p.FinancialApprovalAmountNonTax,
                        InMaintainTime = p.InMaintainTime,
                        OutMaintainTime = p.OutMaintainTime,
                        TotalDays = p.TotalDays,
                        RegularCheckType = (int)p.RegularCheckType,
                        RegularCheckLevel = p.RegularCheckLevel,
                        MaintainInvoiceId = p.MaintainInvoiceId
                    });
        }
    }
}
