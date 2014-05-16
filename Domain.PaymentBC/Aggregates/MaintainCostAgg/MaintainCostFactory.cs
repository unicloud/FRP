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
            decimal financialApprovalAmountNonTax, int? maintainInvoiceId, Guid annualId)
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
            maintainCost.AnnualId = annualId;
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
            decimal replaceFee, decimal customRate, string custom, decimal addedValueRate, string addedValue, Guid annualId)
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
            maintainCost.AnnualId = annualId;
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
            decimal financialApprovalAmountNonTax, int? maintainInvoiceId, Guid annualId)
        {
            maintainCost.Project = project;
            maintainCost.Info = info;
            maintainCost.DepartmentDeclareAmount = departmentDeclareAmount;
            maintainCost.Note = note;
            maintainCost.FinancialApprovalAmount = financialApprovalAmount;
            maintainCost.FinancialApprovalAmountNonTax = financialApprovalAmountNonTax;
            maintainCost.MaintainInvoiceId = maintainInvoiceId;
            maintainCost.AnnualId = annualId;
        }
    }
}
