#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 10:55:11
// 文件名：MaintainCostFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 10:55:11
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg
{
    public static class MaintainCostFactory
    {
        /// <summary>
        ///     创建定检年度维修成本
        /// </summary>
        /// <returns></returns>
        public static RegularCheckMaintainCost CreateRegularCheckMaintainCost()
        {
            var maintainCost = new RegularCheckMaintainCost();
            maintainCost.GenerateNewIdentity();

            return maintainCost;
        }


        public static void SetRegularCheckMaintainCost(RegularCheckMaintainCost maintainCost, Guid aircraftId, Guid actionCategoryId, Guid aircraftTypeId, int regularCheckType,
            string regularCheckLevel, DateTime inMaintainTime, DateTime outMaintainTime, int totalDays, decimal departmentDeclareAmount, decimal financialApprovalAmount,
            decimal financialApprovalAmountNonTax, int? maintainInvoiceId, int year)
        {
            maintainCost.AircraftId = aircraftId;
            maintainCost.ActionCategoryId = actionCategoryId;
            maintainCost.AircraftTypeId = aircraftTypeId;
            maintainCost.RegularCheckType = (RegularCheckType)regularCheckType;
            maintainCost.RegularCheckLevel = regularCheckLevel;
            maintainCost.InMaintainTime = inMaintainTime;
            maintainCost.OutMaintainTime = outMaintainTime;
            maintainCost.TotalDays = totalDays;
            maintainCost.DepartmentDeclareAmount = departmentDeclareAmount;
            maintainCost.FinancialApprovalAmount = financialApprovalAmount;
            maintainCost.FinancialApprovalAmountNonTax = financialApprovalAmountNonTax;
            maintainCost.MaintainInvoiceId = maintainInvoiceId;
            maintainCost.Year = year;
        }

        /// <summary>
        ///     创建起落架维修成本
        /// </summary>
        /// <returns></returns>
        public static UndercartMaintainCost CreateUndercartMaintainCost()
        {
            var maintainCost = new UndercartMaintainCost();
            maintainCost.GenerateNewIdentity();

            return maintainCost;
        }


        public static void SetUndercartMaintainCost(UndercartMaintainCost maintainCost, Guid aircraftId, Guid actionCategoryId, Guid aircraftTypeId, int type,
            int part, DateTime inMaintainTime, DateTime outMaintainTime, int totalDays, decimal departmentDeclareAmount, decimal financialApprovalAmount,
            decimal financialApprovalAmountNonTax, int? maintainInvoiceId, decimal maintainFeeEur, decimal rate, decimal maintainFeeRmb, decimal freightFee,
            decimal replaceFee, decimal customRate, decimal custom, decimal addedValueRate, decimal addedValue, int year)
        {
            maintainCost.AircraftId = aircraftId;
            maintainCost.ActionCategoryId = actionCategoryId;
            maintainCost.AircraftTypeId = aircraftTypeId;
            maintainCost.Type = (MaintainCostType)type;
            maintainCost.Part = (UndercartPart)part;
            maintainCost.InMaintainTime = inMaintainTime;
            maintainCost.OutMaintainTime = outMaintainTime;
            maintainCost.TotalDays = totalDays;
            maintainCost.DepartmentDeclareAmount = departmentDeclareAmount;
            maintainCost.FinancialApprovalAmount = financialApprovalAmount;
            maintainCost.FinancialApprovalAmountNonTax = financialApprovalAmountNonTax;
            maintainCost.MaintainInvoiceId = maintainInvoiceId;
            maintainCost.MaintainFeeEur = maintainFeeEur;
            maintainCost.Rate = rate;
            maintainCost.MaintainFeeRmb = maintainFeeRmb;
            maintainCost.FreightFee = freightFee;
            maintainCost.ReplaceFee = replaceFee;
            maintainCost.CustomRate = customRate;
            maintainCost.Custom = custom;
            maintainCost.AddedValueRate = addedValueRate;
            maintainCost.AddedValue = addedValue;
            maintainCost.Year = year;
        }


        /// <summary>
        ///     创建特修改装维修成本
        /// </summary>
        /// <returns></returns>
        public static SpecialRefitMaintainCost CreateSpecialRefitMaintainCost()
        {
            var maintainCost = new SpecialRefitMaintainCost();
            maintainCost.GenerateNewIdentity();

            return maintainCost;
        }

        public static void SetSpecialRefitMaintainCost(SpecialRefitMaintainCost maintainCost, string project, string info, decimal departmentDeclareAmount, string note, decimal financialApprovalAmount,
            decimal financialApprovalAmountNonTax, int? maintainInvoiceId, int year)
        {
            maintainCost.Project = project;
            maintainCost.Info = info;
            maintainCost.DepartmentDeclareAmount = departmentDeclareAmount;
            maintainCost.Note = note;
            maintainCost.FinancialApprovalAmount = financialApprovalAmount;
            maintainCost.FinancialApprovalAmountNonTax = financialApprovalAmountNonTax;
            maintainCost.MaintainInvoiceId = maintainInvoiceId;
            maintainCost.Year = year;
        }

        /// <summary>
        ///     创建非FHA.超包修维修成本
        /// </summary>
        /// <returns></returns>
        public static NonFhaMaintainCost CreateNonFhaMaintainCost()
        {
            var maintainCost = new NonFhaMaintainCost();
            maintainCost.GenerateNewIdentity();

            return maintainCost;
        }


        public static void SetNonFhaMaintainCost(NonFhaMaintainCost maintainCost, string engineNumber, int contractRepairt, int type, Guid aircraftId, Guid actionCategoryId,
            Guid aircraftTypeId, int supplierId, DateTime inMaintainTime, DateTime outMaintainTime, int maintainLevel, int changeLlpNumber, decimal tsr, decimal csr, decimal nonFhaFee,
            decimal partFee, decimal changeLlpFee, decimal feeLittleSum, decimal rate, decimal feeTotalSum, decimal customRate, decimal custom, decimal addedValueRate, decimal addedValue,
        decimal customsTax, decimal freightFee, decimal departmentDeclareAmount, decimal financialApprovalAmount, decimal financialApprovalAmountNonTax, string note, int actualMaintainLevel,
            int actualChangeLlpNumber, decimal actualTsr, decimal actualCsr, int? maintainInvoiceId, int year)
        {
            maintainCost.EngineNumber = engineNumber;
            maintainCost.ContractRepairt = (ContractRepairtType)contractRepairt;
            maintainCost.Type = (MaintainCostType)type;
            maintainCost.AircraftId = aircraftId;
            maintainCost.ActionCategoryId = actionCategoryId;
            maintainCost.AircraftTypeId = aircraftTypeId;
            maintainCost.SupplierId = supplierId;
            maintainCost.InMaintainTime = inMaintainTime;
            maintainCost.OutMaintainTime = outMaintainTime;
            maintainCost.MaintainLevel = maintainLevel;
            maintainCost.ChangeLlpNumber = changeLlpNumber;
            maintainCost.Tsr = tsr;
            maintainCost.Csr = csr;
            maintainCost.NonFhaFee = nonFhaFee;
            maintainCost.PartFee = partFee;
            maintainCost.ChangeLlpFee = changeLlpFee;
            maintainCost.FeeLittleSum = feeLittleSum;
            maintainCost.Rate = rate;
            maintainCost.FeeTotalSum = feeTotalSum;
            maintainCost.CustomRate = customRate;
            maintainCost.Custom = custom;
            maintainCost.AddedValueRate = addedValueRate;
            maintainCost.AddedValue = addedValue;
            maintainCost.CustomsTax = customsTax;
            maintainCost.FreightFee = freightFee;
            maintainCost.DepartmentDeclareAmount = departmentDeclareAmount;
            maintainCost.FinancialApprovalAmount = financialApprovalAmount;
            maintainCost.FinancialApprovalAmountNonTax = financialApprovalAmountNonTax;
            maintainCost.Note = note;
            maintainCost.ActualMaintainLevel = actualMaintainLevel;
            maintainCost.ActualChangeLlpNumber = actualChangeLlpNumber;
            maintainCost.ActualCsr = actualCsr;
            maintainCost.ActualTsr = actualTsr;
            maintainCost.MaintainInvoiceId = maintainInvoiceId;
            maintainCost.Year = year;
        }

        /// <summary>
        ///     创建APU维修成本
        /// </summary>
        /// <returns></returns>
        public static ApuMaintainCost CreateApuMaintainCost()
        {
            var maintainCost = new ApuMaintainCost();
            maintainCost.GenerateNewIdentity();

            return maintainCost;
        }

        public static void SetApuMaintainCost(ApuMaintainCost maintainCost, string nameType, string type, decimal yearBudgetRate,
            decimal rate, decimal budgetHour, decimal hourPercent, decimal hour, decimal contractRepairFeeUsd, decimal contractRepairFeeRmb, decimal customRate, decimal totalTax,
            decimal addedValueRate, decimal addedValue, decimal includeAddedValue, int? maintainInvoiceId, int year)
        {
            maintainCost.NameType = nameType;
            maintainCost.Type = type;
            maintainCost.YearBudgetRate = yearBudgetRate;
            maintainCost.Rate = rate;
            maintainCost.BudgetHour = budgetHour;
            maintainCost.HourPercent = hourPercent;
            maintainCost.Hour = hour;
            maintainCost.ContractRepairFeeUsd = contractRepairFeeUsd;
            maintainCost.ContractRepairFeeRmb = contractRepairFeeRmb;
            maintainCost.CustomRate = customRate;
            maintainCost.TotalTax = totalTax;
            maintainCost.AddedValueRate = addedValueRate;
            maintainCost.AddedValue = addedValue;
            maintainCost.IncludeAddedValue = includeAddedValue;
            maintainCost.MaintainInvoiceId = maintainInvoiceId;
            maintainCost.Year = year;
        }

        /// <summary>
        ///     创建Fha维修成本
        /// </summary>
        /// <returns></returns>
        public static FhaMaintainCost CreateFhaMaintainCost()
        {
            var maintainCost = new FhaMaintainCost();
            maintainCost.GenerateNewIdentity();

            return maintainCost;
        }

        public static void SetFhaMaintainCost(FhaMaintainCost maintainCost,Guid aircraftTypeId, string engineProperty, string jx,  decimal yearBudgetRate,
            decimal rate, decimal airHour, decimal hourPercent, decimal hour, decimal fhaFeeUsd, decimal fhaFeeRmb, decimal custom, decimal customAddedRmb, decimal totalTax,
            decimal addedValueRate, decimal addedValue, decimal includeAddedValue, decimal customAdded, int? maintainInvoiceId, int year)
        {
            maintainCost.AircraftTypeId = aircraftTypeId;
            maintainCost.EngineProperty = engineProperty;
            maintainCost.Jx = jx;
            maintainCost.YearBudgetRate = yearBudgetRate;
            maintainCost.Rate = rate;
            maintainCost.AirHour = airHour;
            maintainCost.HourPercent = hourPercent;
            maintainCost.Hour = hour;
            maintainCost.FhaFeeUsd = fhaFeeUsd;
            maintainCost.FhaFeeRmb = fhaFeeRmb;
            maintainCost.Custom = custom;
            maintainCost.CustomAddedRmb = customAddedRmb;
            maintainCost.TotalTax = totalTax;
            maintainCost.AddedValueRate = addedValueRate;
            maintainCost.AddedValue = addedValue;
            maintainCost.IncludeAddedValue = includeAddedValue;
            maintainCost.CustomAdded = customAdded;
            maintainCost.MaintainInvoiceId = maintainInvoiceId;
            maintainCost.Year = year;
        }
    }
}
