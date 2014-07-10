#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：SupplierQuery.cs
// 程序集：UniCloud.Application.PurchaseBC.Query
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.PurchaseBC.Query.SupplierQueries
{
    public class SupplierQuery : ISupplierQuery
    {
        private readonly IQueryableUnitOfWork _unitOfWork;

        public SupplierQuery(IQueryableUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IQueryable<SupplierCompanyDTO> SupplierCompanysQuery(QueryBuilder<SupplierCompany> query)
        {
            var dbSupplierRole = _unitOfWork.CreateSet<SupplierRole>();
            var suppliers = _unitOfWork.CreateSet<SupplierCompany>().Select(p => new SupplierCompanyDTO
                                                                                 {
                                                                                     SupplierCompanyId = p.Id,
                                                                                     LinkManId = p.LinkmanId,
                                                                                     AircraftLeaseSupplier =
                                                                                         dbSupplierRole
                                                                                         .OfType<AircraftLeaseSupplier>()
                                                                                         .Any(
                                                                                             c =>
                                                                                         c.SupplierCompanyId == p.Id),
                                                                                     AircraftPurchaseSupplier =
                                                                                         dbSupplierRole
                                                                                         .OfType
                                                                                         <AircraftPurchaseSupplier>()
                                                                                         .Any(
                                                                                             c =>
                                                                                         c.SupplierCompanyId == p.Id),
                                                                                     EngineLeaseSupplier =
                                                                                         dbSupplierRole
                                                                                         .OfType<EngineLeaseSupplier>()
                                                                                         .Any(
                                                                                             c =>
                                                                                         c.SupplierCompanyId == p.Id),
                                                                                     EnginePurchaseSupplier =
                                                                                         dbSupplierRole
                                                                                         .OfType<EnginePurchaseSupplier>
                                                                                         ()
                                                                                         .Any(
                                                                                             c =>
                                                                                         c.SupplierCompanyId == p.Id),
                                                                                     BFEPurchaseSupplier =
                                                                                         dbSupplierRole
                                                                                         .OfType<BFEPurchaseSupplier>()
                                                                                         .Any(
                                                                                             c =>
                                                                                         c.SupplierCompanyId == p.Id),
                                                                                     MaintainSupplier =
                                                                                         dbSupplierRole
                                                                                         .OfType<MaintainSupplier>()
                                                                                         .Any(
                                                                                             c =>
                                                                                         c.SupplierCompanyId == p.Id),
                                                                                     OtherSupplier =
                                                                                         dbSupplierRole
                                                                                         .OfType<OtherSupplier>()
                                                                                         .Any(
                                                                                             c =>
                                                                                         c.SupplierCompanyId == p.Id),
                                                                                     Code = p.Code,
                                                                                     CreateDate = p.CreateDate,
                                                                                     UpdateDate = p.UpdateDate,
                                                                                     SupplierType =
                                                                                         p.Suppliers.FirstOrDefault(
                                                                                             c => c.Code.Equals(p.Code))
                                                                                         .SupplierType ==
                                                                                         SupplierType.国内
                                                                                             ? "国内"
                                                                                             : "国外",
                                                                                     Name =
                                                                                         p.Suppliers.FirstOrDefault(
                                                                                             c => c.Code.Equals(p.Code))
                                                                                         .CnName,
                                                                                     Note =
                                                                                         p.Suppliers.FirstOrDefault(
                                                                                             c => c.Code.Equals(p.Code))
                                                                                         .Note,
                                                                                 });


            return suppliers;
        }

        public IQueryable<SupplierDTO> SuppliersQuery(
            QueryBuilder<Supplier> query)
        {
            var dbSupplierRole = _unitOfWork.CreateSet<SupplierRole>();

            return query.ApplyTo(_unitOfWork.CreateSet<Supplier>()).Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                SupplierType = p.SupplierType == SupplierType.国外 ? "国外" : "国内",
                CreateDate = p.CreateDate,
                UpdateDate = p.UpdateDate,
                Code = p.Code,
                Note = p.Note,
                SuppierCompanyId = p.SupplierCompanyId,
                AircraftLeaseSupplier =
                    dbSupplierRole.OfType<AircraftLeaseSupplier>()
                        .Any(c => c.SupplierCompanyId == p.SupplierCompanyId),
                AircraftPurchaseSupplier =
                    dbSupplierRole.OfType<AircraftPurchaseSupplier>()
                        .Any(
                            c =>
                                c.SupplierCompanyId == p.SupplierCompanyId),
                EngineLeaseSupplier =
                    dbSupplierRole.OfType<EngineLeaseSupplier>()
                        .Any(
                            c =>
                                c.SupplierCompanyId == p.SupplierCompanyId),
                EnginePurchaseSupplier =
                    dbSupplierRole.OfType<EnginePurchaseSupplier>()
                        .Any(
                            c =>
                                c.SupplierCompanyId == p.SupplierCompanyId),
                BFEPurchaseSupplier =
                    dbSupplierRole.OfType<BFEPurchaseSupplier>()
                        .Any(
                            c =>
                                c.SupplierCompanyId == p.SupplierCompanyId),
                MaintainSupplier = dbSupplierRole.OfType<MaintainSupplier>()
                    .Any(
                        c =>
                            c.SupplierCompanyId == p.SupplierCompanyId),
                OtherSupplier = dbSupplierRole.OfType<OtherSupplier>()
                    .Any(c => c.SupplierCompanyId == p.SupplierCompanyId),
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
                    CustCode = c.CustCode,
                    CreateDate = p.CreateDate,
                    UpdateDate = p.UpdateDate,
                    SupplierId = c.SupplierId
                }).ToList(),
            });
        }

        public IQueryable<LinkmanDTO> LinkmansQuery(
            QueryBuilder<Linkman> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Linkman>()).Select(p => new LinkmanDTO
            {
                Address = p.Address.AddressLine1,
                Department = p.Department,
                Email = p.Email,
                IsDefault = p.IsDefault,
                LinkmanId = p.Id,
                Fax = p.Fax,
                TelePhone=p.TelePhone,
                Mobile = p.Mobile,
                Name = p.Name,
                Note = p.Note,
                SourceId = p.SourceId,
                CustCode = p.CustCode,
                CreateDate = p.CreateDate,
                UpdateDate = p.UpdateDate
            });
        }

        /// <summary>
        ///     飞机物料。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>飞机物料。</returns>
        public IQueryable<SupplierCompanyAcMaterialDTO> SupplierCompanyAcMaterialsQuery(
            QueryBuilder<SupplierCompanyMaterial> query)
        {
            var dbMaterial = _unitOfWork.CreateSet<Material>().OfType<AircraftMaterial>();
            var dbSupplierCompanyMaterial = _unitOfWork.CreateSet<SupplierCompanyMaterial>();
            return from t in dbMaterial
                   from c in dbSupplierCompanyMaterial
                   where t.Id == c.MaterialId
                   select new SupplierCompanyAcMaterialDTO
                   {
                       SupplierCompanyMaterialId = c.Id,
                       Name = t.Name,
                       Description = t.Description,
                       SupplierCompanyId = c.SupplierCompanyId,
                       MaterialId = c.MaterialId
                   };
        }

        /// <summary>
        ///     发动机物料。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>发动机物料。</returns>
        public IQueryable<SupplierCompanyEngineMaterialDTO> SupplierCompanyEngineMaterialsQuery(
            QueryBuilder<SupplierCompanyMaterial> query)
        {
            var dbMaterial = _unitOfWork.CreateSet<Material>().OfType<EngineMaterial>();
            var dbSupplierCompanyMaterial = _unitOfWork.CreateSet<SupplierCompanyMaterial>();
            return from t in dbMaterial
                   from c in dbSupplierCompanyMaterial
                   where t.Id == c.MaterialId
                   select new SupplierCompanyEngineMaterialDTO
                   {
                       SupplierCompanyMaterialId = c.Id,
                       Name = t.Name,
                       Description = t.Description,
                       SupplierCompanyId = c.SupplierCompanyId,
                       MaterialId = c.MaterialId
                   };
        }

        /// <summary>
        ///     BFE物料。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>BFE物料。</returns>
        public IQueryable<SupplierCompanyBFEMaterialDTO> SupplierCompanyBFEMaterialsQuery(
            QueryBuilder<SupplierCompanyMaterial> query)
        {
            var dbMaterial = _unitOfWork.CreateSet<Material>().OfType<BFEMaterial>();
            var dbSupplierCompanyMaterial = _unitOfWork.CreateSet<SupplierCompanyMaterial>();
            return from t in dbMaterial
                   from c in dbSupplierCompanyMaterial
                   where t.Id == c.MaterialId
                   select new SupplierCompanyBFEMaterialDTO
                   {
                       SupplierCompanyMaterialId = c.Id,
                       Name = t.Name,
                       Description = t.Description,
                       SupplierCompanyId = c.SupplierCompanyId,
                       MaterialId = c.MaterialId
                   };
        }
    }
}