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
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.PurchaseBC.Enums;
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
                    Code = p.Code,
                    CreateDate = p.Suppliers.FirstOrDefault(c => c.Code.Equals(p.Code)).CreateDate,
                    SupplierType =
                        p.Suppliers.FirstOrDefault(c => c.Code.Equals(p.Code)).SupplierType == 0 ? "国内" : "国外",
                    Name = p.Suppliers.FirstOrDefault(c => c.Code.Equals(p.Code)).Name,
                    Note = p.Suppliers.FirstOrDefault(c => c.Code.Equals(p.Code)).Note,
                });


            return suppliers;
        }

        public IQueryable<SupplierDTO> SuppliersQuery(
            QueryBuilder<Supplier> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Supplier>()).Select(p => new SupplierDTO
                {
                    SupplierId = p.Id,
                    Name = p.Name,
                    SupplierType = p.SupplierType == SupplierType.Foreign ? "国外" : "国内",
                    CreateDate = p.CreateDate,
                    UpdateDate = p.UpdateDate,
                    Code = p.Code,
                    Note = p.Note,
                SuppierCompanyId = p.SupplierCompanyId,
                BankAccounts = p.BankAccounts.Select(c => new BankAccountDTO
                        {
                            Account = c.Account,
                            Address = c.Address,
                            Bank = c.Bank,
                            BankAccountId = c.Id,
                            Branch = c.Bank,
                            Country = c.Country,
                            IsCurrent = c.IsCurrent,
                            Name = c.Name,
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
                    Email = p.Email,
                    IsDefault = p.IsDefault,
                    LinkmanId = p.Id,
                    Fax = p.Fax,
                    Mobile = p.Mobile,
                    Name = p.Name,
                    Note = p.Note,
                    SourceId = p.SourceId,
                });
        }

        /// <summary>
        ///     合作公司相关物料。
        /// </summary>
        /// <param name="query">查询条件。</param>
        /// <returns>合作公司相关物料。</returns>
        public IQueryable<SupplierCompanyMaterialDTO> SupplierCompanyMaterialsQuery(
            QueryBuilder<SupplierCompany> query)
        {
            var dbSupplierRole = _unitOfWork.CreateSet<SupplierRole>();
            return _unitOfWork.CreateSet<SupplierCompany>().Select(p => new SupplierCompanyMaterialDTO
                {
                    SupplierCompanyId = p.Id,
                    AircraftSupplier =
                        dbSupplierRole.OfType<AircraftLeaseSupplier>()
                                      .Any(c => c.SupplierCompanyId == p.Id)
                        || dbSupplierRole.OfType<AircraftPurchaseSupplier>()
                                         .Any(c =>
                                              c.SupplierCompanyId == p.Id),
                    EngineSupplier =
                        dbSupplierRole.OfType<EngineLeaseSupplier>()
                                      .Any(c =>
                                           c.SupplierCompanyId == p.Id)
                        || dbSupplierRole.OfType<EnginePurchaseSupplier>()
                                         .Any(c =>
                                              c.SupplierCompanyId == p.Id),
                    BFESupplier =
                        dbSupplierRole.OfType<BFEPurchaseSupplier>()
                                      .Any(c =>
                                           c.SupplierCompanyId == p.Id),
                    Code = p.Code,
                    SupplierType =
                        p.Suppliers.FirstOrDefault(c => c.Code.Equals(p.Code)).SupplierType == 0 ? "国内" : "国外",
                    Name = p.Suppliers.FirstOrDefault(c => c.Code.Equals(p.Code)).Name,
                    AircraftMaterials = p.Materials.OfType<AircraftMaterial>().Select(c => new AircraftMaterialDTO
                        {
                            AcMaterialId = c.Id,
                            Name = c.Name,
                            Description = c.Description,
                            AircraftTypeId = c.AircraftTypeId,
                        }).ToList(),
                    BFEMaterials = p.Materials.OfType<BFEMaterial>().Select(c => new BFEMaterialDTO
                        {
                            BFEMaterialId = c.Id,
                            Name = c.Name,
                            Description = c.Description,
                            PartId = c.PartID,
                        }).ToList(),
                    EngineMaterials = p.Materials.OfType<EngineMaterial>().Select(c => new EngineMaterialDTO
                        {
                            EngineMaterialId = c.Id,
                            Name = c.Name,
                            Description = c.Description,
                            PartId = c.PartID,
                        }).ToList(),
                });
        }
    }
}