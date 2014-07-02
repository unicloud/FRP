#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，11:12
// 文件名：SupplierQuery.cs
// 程序集：UniCloud.Application.FleetPlanBC.Query
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierRoleAgg;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Application.FleetPlanBC.Query.SupplierQueries
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

        /// <summary>
        ///     所有权人（供应商）查询。
        /// </summary>
        /// <param name="query">查询表达式。</param>
        /// <returns>所有权人（供应商）DTO集合。</returns>
        public IQueryable<SupplierDTO> SupplierDTOQuery(
            QueryBuilder<Supplier> query)
        {
            return query.ApplyTo(_unitOfWork.CreateSet<Supplier>()).Select(p => new SupplierDTO
            {
                Id = p.Id,
                AirlineGuid = p.AirlineGuid,
                CnName = p.CnName,
                CnShortName = p.CnShortName,
                EnName = p.EnName,
                EnShortName = p.EnShortName,
                Code = p.Code,
                IsValid = p.IsValid,
                Note = p.Note,
                SupplierType = (int)p.SupplierType,
            });
        }

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
                Id = p.Id,
                AirlineGuid = p.AirlineGuid,
                CnName = p.CnName,
                CnShortName = p.CnShortName,
                EnName = p.EnName,
                EnShortName = p.EnShortName,
                Code = p.Code,
                IsValid = p.IsValid,
                Note = p.Note,
                SupplierType = (int)p.SupplierType,
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
                Id = p.Id,
                AirlineGuid = p.AirlineGuid,
                CnName = p.CnName,
                CnShortName = p.CnShortName,
                EnName = p.EnName,
                EnShortName = p.EnShortName,
                Code = p.Code,
                IsValid = p.IsValid,
                Note = p.Note,
                SupplierType = (int)p.SupplierType,
            }).ToList();
        }
    }
}