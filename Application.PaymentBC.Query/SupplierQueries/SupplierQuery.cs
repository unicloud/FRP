#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/18，10:11
// 文件名：SupplierQuery.cs
// 程序集：UniCloud.Application.PaymentBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Domain.PaymentBC.Aggregates.SupplierAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PaymentBC.Query.SupplierQueries
{
    public class SupplierQuery : ISupplierQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public SupplierQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<SupplierDTO> SuppliersQuery(
            QueryBuilder<Supplier> query)
        {
            var dbSupplierRole = _unitOfWork.CreateSet<SupplierRole>();

            return query.ApplyTo(_unitOfWork.CreateSet<Supplier>()).Select(p => new SupplierDTO
                {
                    SupplierId = p.Id,
                    Name = p.CnName,
                    IsValid = p.IsValid,
                    Code = p.Code,
                    Note = p.Note,
                    SuppierCompanyId = p.SupplierCompanyId,
                    AircraftLeaseSupplier =
                        dbSupplierRole.OfType<AircraftLeaseSupplier>()
                            .Any(c => c.SupplierCompanyId == p.Id),
                    AircraftPurchaseSupplier =
                        dbSupplierRole.OfType<AircraftPurchaseSupplier>()
                            .Any(
                                c =>
                                    c.SupplierCompanyId == p.Id),
                    EngineLeaseSupplier =
                        dbSupplierRole.OfType<EngineLeaseSupplier>()
                            .Any(
                                c =>
                                    c.SupplierCompanyId == p.Id),
                    EnginePurchaseSupplier =
                        dbSupplierRole.OfType<EnginePurchaseSupplier>()
                            .Any(
                                c =>
                                    c.SupplierCompanyId == p.Id),
                    BFEPurchaseSupplier =
                        dbSupplierRole.OfType<BFEPurchaseSupplier>()
                            .Any(
                                c =>
                                    c.SupplierCompanyId == p.Id),
                    MaintainSupplier = dbSupplierRole.OfType<MaintainSupplier>()
                        .Any(
                            c =>
                                c.SupplierCompanyId == p.Id),
                    OtherSupplier = dbSupplierRole.OfType<OtherSupplier>()
                        .Any(c => c.SupplierCompanyId == p.Id),
                    BankAccounts = p.BankAccounts.Select(c => new BankAccountDTO
                        {
                            Account = c.Account,
                            Address = c.Address,
                            Bank = c.Bank,
                            BankAccountId = c.Id,
                            Branch = c.Branch,
                            Country = c.Country,
                            IsCurrent = c.IsCurrent,
                            Name = c.Name,
                            SupplierId = c.SupplierId
                        }).ToList(),
                });
        }
    }
}