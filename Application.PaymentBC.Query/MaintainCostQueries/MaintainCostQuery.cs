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
        ///     定检维修成本查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>定检维修成本DTO集合。</returns>
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
                        AcutalTotalDays = p.MaintainInvoice.TotalDays,
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
                        MaintainInvoiceId = p.MaintainInvoiceId,
                        AnnualId = p.AnnualId
                    });
        }

        /// <summary>
        ///     起落架维修成本查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>起落架维修成本DTO集合。</returns>
        public IQueryable<UndercartMaintainCostDTO> UndercartMaintainCostDTOQuery(
           QueryBuilder<UndercartMaintainCost> query)
        {
            return
                query.ApplyTo(_maintainCostRepository.GetAll().OfType<UndercartMaintainCost>())
                    .Select(p => new UndercartMaintainCostDTO
                    {
                        Id = p.Id,
                        ActionCategoryId = p.ActionCategoryId,
                        AcutalAmount = p.MaintainInvoice.PaidAmount,
                        AcutalBudgetAmount = p.MaintainInvoice.InvoiceValue,
                        AcutalInMaintainTime = p.MaintainInvoice.InMaintainTime,
                        AcutalOutMaintainTime = p.MaintainInvoice.OutMaintainTime,
                        AcutalTotalDays = p.MaintainInvoice.TotalDays,
                        AircraftId = p.AircraftId,
                        AircraftTypeId = p.AircraftTypeId,
                        DepartmentDeclareAmount = p.DepartmentDeclareAmount,
                        FinancialApprovalAmount = p.FinancialApprovalAmount,
                        FinancialApprovalAmountNonTax = p.FinancialApprovalAmountNonTax,
                        InMaintainTime = p.InMaintainTime,
                        OutMaintainTime = p.OutMaintainTime,
                        TotalDays = p.TotalDays,
                        MaintainInvoiceId = p.MaintainInvoiceId,
                        Type = (int)p.Type,
                        Part = (int)p.Part,
                        AddedValue = p.AddedValue,
                        AddedValueRate = p.AddedValueRate,
                        Custom = p.Custom,
                        CustomRate = p.CustomRate,
                        FreightFee = p.FreightFee,
                        MaintainFeeEur = p.MaintainFeeEur,
                        MaintainFeeRmb = p.MaintainFeeRmb,
                        Rate = p.Rate,
                        ReplaceFee = p.ReplaceFee,
                        AnnualId = p.AnnualId
                    });
        }

        /// <summary>
        ///      特修改装维修成本查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns> 特修改装维修成本DTO集合。</returns>
        public IQueryable<SpecialRefitMaintainCostDTO> SpecialRefitMaintainCostDTOQuery(
           QueryBuilder<SpecialRefitMaintainCost> query)
        {
            return
                query.ApplyTo(_maintainCostRepository.GetAll().OfType<SpecialRefitMaintainCost>())
                    .Select(p => new SpecialRefitMaintainCostDTO
                    {
                        Id = p.Id,
                        Project = p.Project,
                        Info = p.Info,
                        DepartmentDeclareAmount = p.DepartmentDeclareAmount,
                        Note = p.Note,
                        FinancialApprovalAmount = p.FinancialApprovalAmount,
                        FinancialApprovalAmountNonTax = p.FinancialApprovalAmountNonTax,
                        AcutalBudgetAmount = p.MaintainInvoice.InvoiceValue,
                        AcutalAmount = p.MaintainInvoice.PaidAmount,
                        MaintainInvoiceId = p.MaintainInvoiceId,
                        AnnualId = p.AnnualId
                    });
        }

        /// <summary>
        ///      非FHA.超包修维修成本查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns> 非FHA.超包修维修成本DTO集合。</returns>
        public IQueryable<NonFhaMaintainCostDTO> NonFhaMaintainCostDTOQuery(
           QueryBuilder<NonFhaMaintainCost> query)
        {
            return
                query.ApplyTo(_maintainCostRepository.GetAll().OfType<NonFhaMaintainCost>())
                    .Select(p => new NonFhaMaintainCostDTO
                    {
                        Id = p.Id,
                        EngineNumber = p.EngineNumber,
                        ContractRepairt = (int)p.ContractRepairt,
                        Type = (int)p.Type,
                        AircraftId = p.AircraftId,
                        ActionCategoryId = p.ActionCategoryId,
                        AircraftTypeId = p.AircraftTypeId,
                        SupplierId = p.SupplierId,
                        InMaintainTime = p.InMaintainTime,
                        OutMaintainTime = p.OutMaintainTime,
                        MaintainLevel = p.MaintainLevel,
                        ChangeLlpNumber = p.ChangeLlpNumber,
                        Tsr = p.Tsr,
                        Csr = p.Csr,
                        NonFhaFee = p.NonFhaFee,
                        PartFee = p.PartFee,
                        ChangeLlpFee = p.ChangeLlpFee,
                        FeeLittleSum = p.FeeLittleSum,
                        Rate = p.Rate,
                        FeeTotalSum = p.FeeTotalSum,
                        CustomRate = p.CustomRate,
                        Custom = p.Custom,
                        AddedValueRate = p.AddedValueRate,
                        AddedValue = p.AddedValue,
                        CustomsTax = p.CustomsTax,
                        FreightFee = p.FreightFee,
                        DepartmentDeclareAmount = p.DepartmentDeclareAmount,
                        Note = p.Note,
                        FinancialApprovalAmount = p.FinancialApprovalAmount,
                        FinancialApprovalAmountNonTax = p.FinancialApprovalAmountNonTax,
                        ActualMaintainLevel = p.ActualMaintainLevel,
                        ActualChangeLlpNumber = p.ActualChangeLlpNumber,
                        ActualCsr = p.ActualCsr,
                        ActualTsr = p.ActualTsr,
                        AcutalInMaintainTime = p.MaintainInvoice.InMaintainTime,
                        AcutalOutMaintainTime = p.MaintainInvoice.OutMaintainTime,
                        AcutalTotalDays = p.MaintainInvoice.TotalDays,
                        AcutalBudgetAmount = p.MaintainInvoice.InvoiceValue,
                        AcutalAmount = p.MaintainInvoice.PaidAmount,
                        MaintainInvoiceId = p.MaintainInvoiceId,
                        AnnualId = p.AnnualId
                    });
        }

        /// <summary>
        ///   APU维修成本查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns> APU维修成本DTO集合。</returns>
        public IQueryable<ApuMaintainCostDTO> ApuMaintainCostDTOQuery(
           QueryBuilder<ApuMaintainCost> query)
        {
            return
                query.ApplyTo(_maintainCostRepository.GetAll().OfType<ApuMaintainCost>())
                    .Select(p => new ApuMaintainCostDTO
                    {
                        Id = p.Id,
                        NameType = p.NameType,
                        Type = p.Type,
                        LastYearRate = p.LastYearRate,
                        YearAddedRate = p.YearAddedRate,
                        YearBudgetRate = p.YearBudgetRate,
                        Rate = p.Rate,
                        BudgetHour = p.BudgetHour,
                        HourPercent = p.HourPercent,
                        Hour = p.Hour,
                        ContractRepairFeeUsd = p.ContractRepairFeeUsd,
                        ContractRepairFeeRmb = p.ContractRepairFeeRmb,
                        CustomRate = p.CustomRate,
                        TotalTax = p.TotalTax,
                        AddedValueRate = p.AddedValueRate,
                        AddedValue = p.AddedValue,
                        IncludeAddedValue = p.IncludeAddedValue,
                        MaintainInvoiceId = p.MaintainInvoiceId,
                        AnnualId = p.AnnualId,
                        AcutalBudgetAmount = p.MaintainInvoice.InvoiceValue,
                        AcutalAmount = p.MaintainInvoice.PaidAmount
                    });
        }

        /// <summary>
        ///   FHA维修成本查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns> FHA维修成本DTO集合。</returns>
        public IQueryable<FhaMaintainCostDTO> FhaMaintainCostDTOQuery(
           QueryBuilder<FhaMaintainCost> query)
        {
            return
                query.ApplyTo(_maintainCostRepository.GetAll().OfType<FhaMaintainCost>())
                    .Select(p => new FhaMaintainCostDTO
                    {
                        Id = p.Id,
                        EngineProperty = p.EngineProperty,
                        Jx = p.Jx,
                        LastYearRate = p.LastYearRate,
                        YearAddedRate = p.YearAddedRate,
                        YearBudgetRate = p.YearBudgetRate,
                        Rate = p.Rate,
                        AirHour = p.AirHour,
                        HourPercent = p.HourPercent,
                        Hour = p.Hour,
                        FhaFeeUsd = p.FhaFeeUsd,
                        FhaFeeRmb = p.FhaFeeRmb,
                        Custom = p.Custom,
                        CustomAddedRmb = p.CustomAddedRmb,
                        TotalTax = p.TotalTax,
                        AddedValueRate = p.AddedValueRate,
                        AddedValue = p.AddedValue,
                        IncludeAddedValue = p.IncludeAddedValue,
                        CustomAdded = p.CustomAdded,
                        AircraftTypeId = p.AircraftTypeId,
                        MaintainInvoiceId = p.MaintainInvoiceId,
                        AnnualId = p.AnnualId,
                        AcutalBudgetAmount = p.MaintainInvoice.InvoiceValue,
                        AcutalAmount = p.MaintainInvoice.PaidAmount
                    });
        }
    }
}
