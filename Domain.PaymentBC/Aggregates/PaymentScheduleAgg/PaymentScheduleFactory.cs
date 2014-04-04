#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/08，11:57
// 方案：FRP
// 项目：Domain.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

using System;

namespace UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg
{
    /// <summary>
    ///     付款计划工厂
    /// </summary>
    public static class PaymentScheduleFactory
    {
        /// <summary>
        ///  创建飞机付款计划
        /// </summary>
        /// <param name="supplierName"></param>
        /// <param name="supplierId"></param>
        /// <param name="currencyId"></param>
        /// <param name="contractAcId"></param>
        /// <returns></returns>
        public static PaymentSchedule CreateAcPaymentSchedule(string supplierName,int supplierId,int currencyId,int contractAcId)
        {
            var acPaymentSchedule=new AircraftPaymentSchedule
               {
                 CreateDate   = DateTime.Now
               };
            acPaymentSchedule.SetSupplier(supplierId,supplierName);
            acPaymentSchedule.SetCurrency(currencyId);
            acPaymentSchedule.SetContractAircraft(contractAcId);
            return acPaymentSchedule;
        }

        /// <summary>
        /// 创建发动机付款计划
        /// </summary>
        /// <param name="supplierName"></param>
        /// <param name="supplierId"></param>
        /// <param name="currencyId"></param>
        /// <param name="contractEngineId"></param>
        /// <returns></returns>
        public static PaymentSchedule CreateEnginePaymentSchedule(string supplierName, int supplierId, int currencyId,
                                                              int contractEngineId)
        {
            var enginePaymentSchedule = new EnginePaymentSchedule
            {
                CreateDate = DateTime.Now
            };
            enginePaymentSchedule.SetSupplier(supplierId, supplierName);
            enginePaymentSchedule.SetCurrency(currencyId);
            enginePaymentSchedule.SetContractEngine(contractEngineId);
            return enginePaymentSchedule;
        }

        /// <summary>
        /// 创建标准付款计划
        /// </summary>
        /// <param name="supplierName"></param>
        /// <param name="supplierId"></param>
        /// <param name="currencyId"></param>
        /// <param name="orderId"></param>
        /// <returns></returns>
        public static PaymentSchedule CreateStandardPaymentSchedule(string supplierName, int supplierId, int currencyId,
                                                              int orderId)
        {
            var standardPaymentSchedule = new StandardPaymentSchedule
            {
                CreateDate = DateTime.Now
            };
            standardPaymentSchedule.SetSupplier(supplierId, supplierName);
            standardPaymentSchedule.SetCurrency(currencyId);
            standardPaymentSchedule.SetOrderId(orderId);
            return standardPaymentSchedule;
        }
    }
}