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

using System.Collections.Generic;
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
        private readonly ISupplierCompanyRepository _supplierCompanyRepository;

        public SupplierQuery(IQueryableUnitOfWork unitOfWork,            
            ISupplierCompanyRepository supplierCompanyRepository)
        {
            _unitOfWork = unitOfWork;
            _supplierCompanyRepository = supplierCompanyRepository;
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

        #region 获取供应商信息
        /// <summary>
        /// 获取所有的飞机供应商（飞机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetAircraftSuppliers()
        {
            var results = new List<Supplier>();
            var dbAcPurSuppliers = _unitOfWork.CreateSet<AircraftPurchaseSupplier>().ToList();
            var dbAcLeaseSuppliers = _unitOfWork.CreateSet<AircraftLeaseSupplier>().ToList();
            if (dbAcPurSuppliers.ToList().Count != 0)
            {
                dbAcPurSuppliers.ToList().ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            if (dbAcLeaseSuppliers.ToList().Count != 0)
            {
                dbAcLeaseSuppliers.ToList().ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }

        /// <summary>
        /// 获取所有的发动机供应商（发动机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetEngineSuppliers()
        {
            var results = new List<Supplier>();
            var dbEnginePurSuppliers = _unitOfWork.CreateSet<SupplierRole>().OfType<EnginePurchaseSupplier>().ToList();
            var dbEngineLeaseSuppliers = _unitOfWork.CreateSet<SupplierRole>().OfType<EngineLeaseSupplier>().ToList();
            if (dbEnginePurSuppliers.Count != 0)
            {
                dbEnginePurSuppliers.ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            if (dbEngineLeaseSuppliers.Count != 0)
            {
                dbEngineLeaseSuppliers.ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }

        /// <summary>
        /// 获取所有的飞机采购供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetAircraftPurchaseSuppliers()
        {
            var results = new List<Supplier>();
            var dbAcPurSuppliers = _unitOfWork.CreateSet<AircraftPurchaseSupplier>().ToList();
            if (dbAcPurSuppliers.ToList().Count != 0)
            {
                dbAcPurSuppliers.ToList().ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }

        /// <summary>
        /// 获取所有的飞机租赁供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetAircraftLeaseSuppliers()
        {
            var results = new List<Supplier>();
            var dbAcLeaseSuppliers = _unitOfWork.CreateSet<AircraftLeaseSupplier>().ToList();
            if (dbAcLeaseSuppliers.ToList().Count != 0)
            {
                dbAcLeaseSuppliers.ToList().ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }

        /// <summary>
        /// 获取所有的发动机采购供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetEnginePurchaseSuppliers()
        {
            var results = new List<Supplier>();
            var dbEnginePurSuppliers = _unitOfWork.CreateSet<SupplierRole>().OfType<EnginePurchaseSupplier>().ToList();
            if (dbEnginePurSuppliers.Count != 0)
            {
                dbEnginePurSuppliers.ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }

        /// <summary>
        /// 获取所有的发动机租赁供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetEngineLeaseSuppliers()
        {
            var results = new List<Supplier>();
            var dbEngineLeaseSuppliers = _unitOfWork.CreateSet<SupplierRole>().OfType<EngineLeaseSupplier>().ToList();
            if (dbEngineLeaseSuppliers.Count != 0)
            {
                dbEngineLeaseSuppliers.ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }

        /// <summary>
        /// 获取所有的BFE供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetBfeSuppliers()
        {
            var results = new List<Supplier>();
            var dbBfeSuppliers = _unitOfWork.CreateSet<SupplierRole>().OfType<BFEPurchaseSupplier>().ToList();
            if (dbBfeSuppliers.Count != 0)
            {
                dbBfeSuppliers.ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }


        /// <summary>
        /// 获取所有的维修供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetMaintainSuppliers()
        {
            var results = new List<Supplier>();
            var dbBfeSuppliers = _unitOfWork.CreateSet<SupplierRole>().OfType<MaintainSupplier>().ToList();
            if (dbBfeSuppliers.Count != 0)
            {
                dbBfeSuppliers.ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }

        /// <summary>
        /// 获取所有的"其他"供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetOtherSuppliers()
        {
            var results = new List<Supplier>();
            var dbBfeSuppliers = _unitOfWork.CreateSet<SupplierRole>().OfType<OtherSupplier>().ToList();
            if (dbBfeSuppliers.Count != 0)
            {
                dbBfeSuppliers.ForEach(p =>
                {
                    var supplierCompany = _supplierCompanyRepository.Get(p.SupplierCompanyId);
                    results.AddRange(supplierCompany.Suppliers);
                });
            }
            results = results.Distinct().ToList();
            return results.Select(p => new SupplierDTO
            {
                SupplierId = p.Id,
                Name = p.CnName,
                ShortName = p.CnShortName,
                Code = p.Code,
                Note = p.Note,
            }).ToList();
        }
        #endregion
    }
}