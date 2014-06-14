#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，16:11
// 文件名：SupplierData.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.Common.ValueObjects;
using UniCloud.Domain.UberModel.Aggregates.BankAccountAgg;
using UniCloud.Domain.UberModel.Aggregates.LinkmanAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierRoleAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class SupplierData : InitialDataBase
    {
        public SupplierData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        /// <summary>
        ///     初始化供应商相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var supplier = SupplierFactory.CreateSupplier(SupplierType.国外, "V0001", "空客", null);
            supplier.GenerateNewIdentity();

            var supplier2 = SupplierFactory.CreateSupplier(SupplierType.国外, "V0002", "波音", null);
            supplier.GenerateNewIdentity();

            var supplier3 = SupplierFactory.CreateSupplier(SupplierType.国外, "V0003", "Genesis China Leasing 1 Limited", null);
            supplier.GenerateNewIdentity();

            var supplier4 = SupplierFactory.CreateSupplier(SupplierType.国外, "V0004", "AerDragon Aviation Partners Limited", null);
            supplier.GenerateNewIdentity();

            var supplier5 = SupplierFactory.CreateSupplier(SupplierType.国外, "V0005", "ILFC Aircraft 32A-3116 Limited", null);
            supplier.GenerateNewIdentity();

            var supplier6 = SupplierFactory.CreateSupplier(SupplierType.国外, "V0005", "ILFC aircraft 32A-550 Limited", null);
            supplier.GenerateNewIdentity();

            var supplier7 = SupplierFactory.CreateSupplier(SupplierType.国外, "V0007", "罗罗", null);
            supplier.GenerateNewIdentity();

            var supplierCompany = SupplierCompanyFactory.CreateSupplieCompany(supplier.Code);
            supplierCompany.GenerateNewIdentity();
            supplier.SetSupplierCompany(supplierCompany);

            var supplierCompany2 = SupplierCompanyFactory.CreateSupplieCompany(supplier2.Code);
            supplierCompany2.GenerateNewIdentity();
            supplier2.SetSupplierCompany(supplierCompany2);

            var supplierCompany3 = SupplierCompanyFactory.CreateSupplieCompany(supplier3.Code);
            supplierCompany3.GenerateNewIdentity();
            supplier3.SetSupplierCompany(supplierCompany3);

            var supplierCompany4 = SupplierCompanyFactory.CreateSupplieCompany(supplier4.Code);
            supplierCompany4.GenerateNewIdentity();
            supplier4.SetSupplierCompany(supplierCompany4);

            var supplierCompany5 = SupplierCompanyFactory.CreateSupplieCompany(supplier5.Code);
            supplierCompany5.GenerateNewIdentity();
            supplier5.SetSupplierCompany(supplierCompany5);
            supplier6.SetSupplierCompany(supplierCompany5);

            var supplierCompany7 = SupplierCompanyFactory.CreateSupplieCompany(supplier7.Code);
            supplierCompany7.GenerateNewIdentity();
            supplier7.SetSupplierCompany(supplierCompany7);

            Context.Suppliers.Add(supplier);
            Context.Suppliers.Add(supplier2);
            Context.Suppliers.Add(supplier3);
            Context.Suppliers.Add(supplier4);
            Context.Suppliers.Add(supplier5);
            Context.Suppliers.Add(supplier6);
            Context.Suppliers.Add(supplier7); 
            Context.SupplierCompanies.Add(supplierCompany);
            Context.SupplierCompanies.Add(supplierCompany2);
            Context.SupplierCompanies.Add(supplierCompany3);
            Context.SupplierCompanies.Add(supplierCompany4);
            Context.SupplierCompanies.Add(supplierCompany5);
            Context.SupplierCompanies.Add(supplierCompany7);

            var acLeaseSupplier = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCompany);
            var acLeaseSupplier2 = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCompany2);
            var acLeaseSupplier3 = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCompany3);
            var acLeaseSupplier4 = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCompany4);
            var acLeaseSupplier5 = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCompany5);
            var acPurchaseSupplier = SupplierRoleFactory.CreateAircraftPurchaseSupplier(supplierCompany);
            var acPurchaseSupplier2 = SupplierRoleFactory.CreateAircraftPurchaseSupplier(supplierCompany2);
            var engLeaseSupplier = SupplierRoleFactory.CreateEngineLeaseSupplier(supplierCompany7);
            var engPurchaseSupplier = SupplierRoleFactory.CreateEnginePurchaseSupplier(supplierCompany7);
            var maintainSupplier = SupplierRoleFactory.CreateMaintainSupplier(supplierCompany);
            Context.SupplierRoles.Add(acLeaseSupplier);
            Context.SupplierRoles.Add(acLeaseSupplier2);
            Context.SupplierRoles.Add(acLeaseSupplier3);
            Context.SupplierRoles.Add(acLeaseSupplier4);
            Context.SupplierRoles.Add(acLeaseSupplier5);
            Context.SupplierRoles.Add(acPurchaseSupplier);
            Context.SupplierRoles.Add(acPurchaseSupplier2);
            Context.SupplierRoles.Add(engLeaseSupplier);
            Context.SupplierRoles.Add(engPurchaseSupplier);
            Context.SupplierRoles.Add(maintainSupplier);

            var banck = new BankAccount
            {
                Account = "432222283746262",
                Address = "四川成都",
                Bank = "中国工商银行",
                Branch = "成都支行",
                Country = "中国",
                IsCurrent = true,
                Name = "XXXBY",
                SupplierId = supplier.Id,
            };
            banck.GenerateNewIdentity();
            Context.BankAccounts.Add(banck);
            var banck2 = new BankAccount
            {
                Account = "6225222283746262",
                Address = "四川成都",
                Bank = "招商银行",
                Branch = "成都支行",
                Country = "中国",
                IsCurrent = true,
                Name = "XXXKK",
                SupplierId = supplier.Id,
            };
            banck2.GenerateNewIdentity();
            Context.BankAccounts.Add(banck2);
            var banck3 = new BankAccount
            {
                Account = "6182222283746262",
                Address = "四川成都",
                Bank = "中国银行",
                Branch = "成都支行",
                Country = "中国",
                IsCurrent = true,
                Name = "XXXGE",
                SupplierId = supplier.Id,
            };
            banck3.GenerateNewIdentity();
            Context.BankAccounts.Add(banck3);
            var banck4 = new BankAccount
            {
                Account = "4113222283746262",
                Address = "四川成都",
                Bank = "中国工商银行",
                Branch = "成都支行",
                Country = "中国",
                IsCurrent = true,
                Name = "XXXAD",
                SupplierId = supplier.Id,
            };
            banck4.GenerateNewIdentity();
            Context.BankAccounts.Add(banck4);
            var linkman = new Linkman
            {
                Name = "XXXAL",
                IsDefault = true,
                Address = new Address(null, null, "四川成都", null),
                Department = "售后服务部",
                Email = "1234567@163.com",
                Fax = "1234567",
                Mobile = "1234567",
                Note = "备注信息",
            };
            linkman.SetSourceId(supplier.SupplierCompany.LinkmanId);
            Context.Linkmen.Add(linkman);
            Context.SaveChanges();
        }
    }
}