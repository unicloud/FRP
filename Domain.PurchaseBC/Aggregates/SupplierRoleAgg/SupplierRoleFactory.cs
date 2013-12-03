#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，14:11
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg
{
    /// <summary>
    ///     供应商角色工厂
    /// </summary>
    public static class SupplierRoleFactory
    {
        /// <summary>
        ///     创建飞机租赁供应商
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        /// <returns>创建的飞机租赁供应商</returns>
        public static AircraftLeaseSupplier CreateAircraftLeaseSupplier(SupplierCompany supplierCompany)
        {
            var aircraftLeaseSupplier = new AircraftLeaseSupplier();

            aircraftLeaseSupplier.SetSupplierCompany(supplierCompany);

            return aircraftLeaseSupplier;
        }

        /// <summary>
        ///     创建飞机购买供应商
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        /// <returns>创建的飞机购买供应商</returns>
        public static AircraftPurchaseSupplier CreateAircraftPurchaseSupplier(SupplierCompany supplierCompany)
        {
            var aircraftPurchaseSupplier = new AircraftPurchaseSupplier();

            aircraftPurchaseSupplier.SetSupplierCompany(supplierCompany);

            return aircraftPurchaseSupplier;
        }

        /// <summary>
        ///     创建BFE采购供应商
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        /// <returns>创建的BFE采购供应商</returns>
        public static BFEPurchaseSupplier CreateBFEPurchaseSupplier(SupplierCompany supplierCompany)
        {
            var bfePurchaseSupplier = new BFEPurchaseSupplier();

            bfePurchaseSupplier.SetSupplierCompany(supplierCompany);

            return bfePurchaseSupplier;
        }

        /// <summary>
        ///     创建发动机租赁供应商
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        /// <returns>创建的发动机租赁供应商</returns>
        public static EngineLeaseSupplier CreateEngineLeaseSupplier(SupplierCompany supplierCompany)
        {
            var engineLeaseSupplier = new EngineLeaseSupplier();

            engineLeaseSupplier.SetSupplierCompany(supplierCompany);

            return engineLeaseSupplier;
        }

        /// <summary>
        ///     创建飞机购买供应商
        /// </summary>
        /// <param name="supplierCompany">供应商公司</param>
        /// <returns>创建的飞机购买供应商</returns>
        public static EnginePurchaseSupplier CreateEnginePurchaseSupplier(SupplierCompany supplierCompany)
        {
            var enginePurchaseSupplier = new EnginePurchaseSupplier();

            enginePurchaseSupplier.SetSupplierCompany(supplierCompany);

            return enginePurchaseSupplier;
        }
    }
}