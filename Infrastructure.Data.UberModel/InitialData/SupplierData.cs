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

using UniCloud.Domain.UberModel.Aggregates.BankAccountAgg;
using UniCloud.Domain.UberModel.Aggregates.LinkmanAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.UberModel.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.UberModel.Enums;
using UniCloud.Domain.UberModel.ValueObjects;
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
            var supplier = SupplierFactory.CreateSupplier(SupplierType.国外, "V0001", "波音", null);
            supplier.GenerateNewIdentity();

            var supplierCompany = SupplierCompanyFactory.CreateSupplieCompany(supplier.Code);
            supplierCompany.GenerateNewIdentity();
            supplier.SetSupplierCompany(supplierCompany);

            Context.Suppliers.Add(supplier);

            var acLeaseSupplier = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCompany);
            var acPurchaseSupplier = SupplierRoleFactory.CreateAircraftPurchaseSupplier(supplierCompany);
            var bfePurchaseSupplier = SupplierRoleFactory.CreateBFEPurchaseSupplier(supplierCompany);
            var engLeaseSupplier = SupplierRoleFactory.CreateEngineLeaseSupplier(supplierCompany);
            var engPurchaseSupplier = SupplierRoleFactory.CreateEnginePurchaseSupplier(supplierCompany);
            Context.SupplierRoles.Add(acLeaseSupplier);
            Context.SupplierRoles.Add(acPurchaseSupplier);
            Context.SupplierRoles.Add(bfePurchaseSupplier);
            Context.SupplierRoles.Add(engLeaseSupplier);
            Context.SupplierRoles.Add(engPurchaseSupplier);
            var banck = new BankAccount
                {
                    Account = "432222283746262",
                    Address = "四川成都",
                    Bank = "中国工商银行",
                    Branch = "成都支行",
                    Country = "中国",
                    IsCurrent = true,
                    Name = "XXX",
                    SupplierId = supplier.Id,
                };
            Context.BankAccounts.Add(banck);
            var linkman = new Linkman
                {
                    Name = "XXX",
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
        }
    }
}