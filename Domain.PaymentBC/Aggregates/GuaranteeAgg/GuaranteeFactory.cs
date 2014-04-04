#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/08，11:49
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PaymentBC.Aggregates.GuaranteeAgg
{
    /// <summary>
    ///     保函工厂
    /// </summary>
    public static class GuaranteeFactory
    {
        /// <summary>
        ///     新建租赁保证金
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="amount"></param>
        /// <param name="supplierName"></param>
        /// <param name="operatorName"></param>
        /// <param name="supplierId"></param>
        /// <param name="currencyId"></param>
        /// <param name="orderId"></param>
        /// <param name="status"></param>
        /// <param name="reviewer"></param>
        /// <returns></returns>
        public static LeaseGuarantee CreateLeaseGuarantee(DateTime startDate, DateTime endDate, decimal amount,
            string supplierName,
            string operatorName,
            int supplierId, int currencyId, int orderId, int status, string reviewer)
        {
            var newLeaseGuarantee = new LeaseGuarantee
            {
                StartDate = startDate,
                EndDate = endDate,
                Amount = amount,
                CreateDate = DateTime.Now,
            };
            newLeaseGuarantee.SetSupplier(supplierId, supplierName);
            newLeaseGuarantee.SetOperator(operatorName);
            newLeaseGuarantee.SetCurrency(currencyId);
            newLeaseGuarantee.SetGuaranteeStatus((GuaranteeStatus) status);
            newLeaseGuarantee.SetOrderId(orderId);
            if (!string.IsNullOrEmpty(reviewer))
            {
                newLeaseGuarantee.Review(reviewer);
            }
            return newLeaseGuarantee;
        }

        /// <summary>
        ///     新建维修保证金
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <param name="amount"></param>
        /// <param name="supplierName"></param>
        /// <param name="operatorName"></param>
        /// <param name="supplierId"></param>
        /// <param name="currencyId"></param>
        /// <param name="maintainContractId"></param>
        /// <param name="status"></param>
        /// <param name="reviewer"></param>
        /// <returns></returns>
        public static MaintainGuarantee CreateMaintainGuarantee(DateTime startDate, DateTime endDate, decimal amount,
            string supplierName,
            string operatorName,
            int supplierId, int currencyId, int maintainContractId, int status, string reviewer)
        {
            var newMaintainGuarantee = new MaintainGuarantee
            {
                StartDate = startDate,
                EndDate = endDate,
                Amount = amount,
                CreateDate = DateTime.Now,
            };
            newMaintainGuarantee.SetSupplier(supplierId, supplierName);
            newMaintainGuarantee.SetOperator(operatorName);
            newMaintainGuarantee.SetCurrency(currencyId);
            newMaintainGuarantee.SetGuaranteeStatus((GuaranteeStatus) status);
            newMaintainGuarantee.SetMaintainContractId(maintainContractId);
            if (!string.IsNullOrEmpty(reviewer))
            {
                newMaintainGuarantee.Review(reviewer);
            }
            return newMaintainGuarantee;
        }
    }
}