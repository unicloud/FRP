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
            decimal financialApprovalAmountNonTax, int? maintainInvoiceId)
        {
            maintainCost.AircraftId = aircraftId;
            maintainCost.ActionCategoryId = actionCategoryId;
            maintainCost.AircraftTypeId = aircraftTypeId;
            maintainCost.RegularCheckType =(RegularCheckType) regularCheckType;
            maintainCost.RegularCheckLevel = regularCheckLevel;
            maintainCost.InMaintainTime = inMaintainTime;
            maintainCost.OutMaintainTime = outMaintainTime;
            maintainCost.TotalDays = totalDays;
            maintainCost.DepartmentDeclareAmount = departmentDeclareAmount;
            maintainCost.FinancialApprovalAmount = financialApprovalAmount;
            maintainCost.FinancialApprovalAmountNonTax = financialApprovalAmountNonTax;
            maintainCost.MaintainInvoiceId = maintainInvoiceId;
        }
    }
}
