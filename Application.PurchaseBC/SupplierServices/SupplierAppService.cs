#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/18，10:11
// 文件名：SupplierAppService.cs
// 程序集：UniCloud.Application.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Domain.PurchaseBC.ValueObjects;

#endregion

namespace UniCloud.Application.PurchaseBC.SupplierServices
{
    /// <summary>
    ///     实现供应商服务接口。
    ///     用于处于供应商相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class SupplierAppService : ISupplierAppService
    {
        private readonly ILinkmanRepository _linkmanRepository;
        private readonly IStaticLoad _staticLoad;
        private readonly ISupplierCompanyMaterialRepository _supplierCompanyMaterialRepository;
        private readonly ISupplierCompanyRepository _supplierCompanyRepository;
        private readonly ISupplierQuery _supplierQuery;
        private readonly ISupplierRoleRepository _supplierRoleRepository;

        public SupplierAppService(ISupplierQuery supplierQuery, ISupplierRoleRepository supplierRoleRepository,
            ISupplierCompanyRepository supplierCompanyRepository, ILinkmanRepository linkmanRepository,
            ISupplierCompanyMaterialRepository supplierCompanyMaterialRepository, IStaticLoad staticLoad)
        {
            _supplierQuery = supplierQuery;
            _supplierRoleRepository = supplierRoleRepository;
            _supplierCompanyRepository = supplierCompanyRepository;
            _linkmanRepository = linkmanRepository;
            _supplierCompanyMaterialRepository = supplierCompanyMaterialRepository;
            _staticLoad = staticLoad;
        }

        #region 合作公司相关操作

        /// <summary>
        ///     获取所有供应商公司。
        /// </summary>
        /// <returns>所有供应商公司。</returns>
        public IQueryable<SupplierCompanyDTO> GetSupplierCompanys()
        {
            var queryBuilder =
                new QueryBuilder<SupplierCompany>();
            return _supplierQuery.SupplierCompanysQuery(queryBuilder);
        }

        /// <summary>
        ///     更新合作公司。
        /// </summary>
        /// <param name="supplierCompany">合作公司DTO。</param>
        [Update(typeof (SupplierCompanyDTO))]
        public void ModifySupplierCompany(SupplierCompanyDTO supplierCompany)
        {
            if (supplierCompany.IsNull()) throw new ArgumentNullException("supplierCompany");
            AddSupplierCompanyRole(supplierCompany);
            RemoveSupplierCompanyRole(supplierCompany);
        }

        /// <summary>
        ///     增加供应商角色。
        /// </summary>
        /// <param name="supplierCompany"></param>
        private void AddSupplierCompanyRole(SupplierCompanyDTO supplierCompany)
        {
            var supplierCmy = _supplierCompanyRepository.Get(supplierCompany.SupplierCompanyId);
            if (supplierCmy == null) throw new Exception("找不到合作公司。");
            supplierCompany
                //新增飞机租赁供应商角色
                .If(p => p.AircraftLeaseSupplier, p =>
                    {
                        var aircraftLeaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (AircraftLeaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (aircraftLeaseSupplier != null) return;
                        var newAircraftLeaseSupplier = SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCmy);
                        _supplierRoleRepository.Add(newAircraftLeaseSupplier);
                    })
                //新增飞机购买供应商角色
                .If(p => p.AircraftPurchaseSupplier, p =>
                    {
                        var aircraftPurchaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (AircraftPurchaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (aircraftPurchaseSupplier != null) return;
                        var newAircraftPurchaseSupplier = SupplierRoleFactory.CreateAircraftPurchaseSupplier(supplierCmy);
                        _supplierRoleRepository.Add(newAircraftPurchaseSupplier);
                    })
                //新增发动机租赁供应商角色
                .If(p => p.EngineLeaseSupplier, p =>
                    {
                        var engineLeaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (EngineLeaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (engineLeaseSupplier != null) return;
                        var newEngineLeaseSupplier = SupplierRoleFactory.CreateEngineLeaseSupplier(supplierCmy);
                        _supplierRoleRepository.Add(newEngineLeaseSupplier);
                    })
                //新增发动机购买供应商角色
                .If(p => p.EnginePurchaseSupplier, p =>
                    {
                        var enginePurchaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (EnginePurchaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (enginePurchaseSupplier != null) return;
                        var newEnginePurchaseSupplier = SupplierRoleFactory.CreateEnginePurchaseSupplier(supplierCmy);
                        _supplierRoleRepository.Add(newEnginePurchaseSupplier);
                    })
                //新增BFE供应商角色
                .If(p => p.BFEPurchaseSupplier, p =>
                    {
                        var bfePurchaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (BFEPurchaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (bfePurchaseSupplier != null) return;
                        var newBFEPurchaseSupplier = SupplierRoleFactory.CreateBFEPurchaseSupplier(supplierCmy);
                        _supplierRoleRepository.Add(newBFEPurchaseSupplier);
                    })
                //新增维修物料角色
                .If(p => p.MaintainSupplier, p =>
                    {
                        var maintainPurchaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (MaintainSupplier),
                                                            p.SupplierCompanyId);
                        if (maintainPurchaseSupplier != null) return;
                        var newMaintainPurchaseSupplier = SupplierRoleFactory.CreateBFEPurchaseSupplier(supplierCmy);
                        _supplierRoleRepository.Add(newMaintainPurchaseSupplier);
                    });
        }

        /// <summary>
        ///     删除供应商角色。
        /// </summary>
        /// <param name="supplierCompany"></param>
        private void RemoveSupplierCompanyRole(SupplierCompanyDTO supplierCompany)
        {
            var supplierCmy = _supplierCompanyRepository.Get(supplierCompany.SupplierCompanyId);
            if (supplierCmy == null) throw new Exception("找不到合作公司。");
            supplierCompany
                //删除飞机租赁供应商角色
                .If(p => !p.AircraftLeaseSupplier, p =>
                    {
                        var aircraftLeaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (AircraftLeaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (aircraftLeaseSupplier != null)
                        {
                            _supplierRoleRepository.Remove(aircraftLeaseSupplier);
                        }
                    })
                //删除飞机购买供应商角色
                .If(p => !p.AircraftPurchaseSupplier, p =>
                    {
                        var aircraftPurchaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (AircraftPurchaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (aircraftPurchaseSupplier != null)
                        {
                            _supplierRoleRepository.Remove(aircraftPurchaseSupplier);
                        }
                    })
                //删除发动机租赁供应商角色
                .If(p => !p.EngineLeaseSupplier, p =>
                    {
                        var engineLeaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (EngineLeaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (engineLeaseSupplier != null)
                        {
                            _supplierRoleRepository.Remove(engineLeaseSupplier);
                        }
                    })
                //删除发动机购买供应商角色
                .If(p => !p.EnginePurchaseSupplier, p =>
                    {
                        var enginePurchaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (EnginePurchaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (enginePurchaseSupplier != null)
                        {
                            _supplierRoleRepository.Remove(enginePurchaseSupplier);
                        }
                    })
                //删除BFE供应商角色
                .If(p => !p.BFEPurchaseSupplier, p =>
                    {
                        var bfePurchaseSupplier =
                            _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (BFEPurchaseSupplier),
                                                                                    p.SupplierCompanyId);
                        if (bfePurchaseSupplier != null)
                        {
                            _supplierRoleRepository.Remove(bfePurchaseSupplier);
                        }
                    })
                //删除维修供应商角色
                .If(p => !p.MaintainSupplier, p =>
                {
                    var maintainSupplierPurchaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (MaintainSupplier),
                                                                                p.SupplierCompanyId);
                    if (maintainSupplierPurchaseSupplier != null)
                    {
                        _supplierRoleRepository.Remove(maintainSupplierPurchaseSupplier);
                    }
                });
        }

        #endregion

        #region 供应商相关操作

        /// <summary>
        ///     获取所有供应商信息，包括银行账户，联系人。
        /// </summary>
        /// <returns>所有供应商。</returns>
        public IQueryable<SupplierDTO> GetSuppliers()
        {
            var queryBuilder =
                new QueryBuilder<Supplier>();
            return _supplierQuery.SuppliersQuery(queryBuilder);
        }

        /// <summary>
        ///     获取所有联系人信息。
        /// </summary>
        /// <returns>所有联系人。</returns>
        public IQueryable<LinkmanDTO> GetLinkmans()
        {
            var queryBuilder =
                new QueryBuilder<Linkman>();
            return _supplierQuery.LinkmansQuery(queryBuilder);
        }

        #endregion

        #region 联系人相关操作

        /// <summary>
        ///     新增联系人。
        /// </summary>
        /// <param name="linkman">联系人DTO。</param>
        [Insert(typeof (LinkmanDTO))]
        public void InsertLinkman(LinkmanDTO linkman)
        {
            var newLinkman = LinkmanFactory.CreateLinkman(linkman.Name, linkman.TelePhone, linkman.Mobile,
                linkman.Fax, linkman.Email, new Address(null, null, linkman.Address, null), linkman.SourceId);
            _linkmanRepository.Add(newLinkman);
        }

        /// <summary>
        ///     更新联系人。
        /// </summary>
        /// <param name="linkman">联系人DTO。</param>
        [Update(typeof (LinkmanDTO))]
        public void ModifyLinkman(LinkmanDTO linkman)
        {
            var updateLinkman = _linkmanRepository.Get(linkman.LinkmanId); //获取需要更新的对象。

            //更新。
            updateLinkman.Name = linkman.Name;
            updateLinkman.TelePhone = linkman.TelePhone;
            updateLinkman.Mobile = linkman.Mobile;
            updateLinkman.Fax = linkman.Fax;
            updateLinkman.Email = linkman.Email;
            updateLinkman.SetSourceId(linkman.SourceId);
            updateLinkman.Address = new Address(null, null, linkman.Address, null);
            _linkmanRepository.Modify(updateLinkman);
        }


        /// <summary>
        ///     删除联系人。
        /// </summary>
        /// <param name="linkman">联系人DTO。</param>
        [Delete(typeof (LinkmanDTO))]
        public void DeleteLinkman(LinkmanDTO linkman)
        {
            var deletedLinkman = _linkmanRepository.Get(linkman.LinkmanId); //获取需要更新的对象。
            _linkmanRepository.Remove(deletedLinkman); //删除联系人。
        }

        #endregion

        #region 供应商物料相关操作

        /// <summary>
        ///     获取合作公司的飞机物料。
        /// </summary>
        /// <returns>合作公司的飞机物料。</returns>
        public IQueryable<SupplierCompanyAcMaterialDTO> GetSupplierCompanyAcMaterials()
        {
            var queryBuilder =
                     new QueryBuilder<SupplierCompanyMaterial>();
            return _supplierQuery.SupplierCompanyAcMaterialsQuery(queryBuilder);
        }

        /// <summary>
        ///     获取合作公司的发动机物料。
        /// </summary>
        /// <returns>合作公司的发动机物料。</returns>
        public IQueryable<SupplierCompanyEngineMaterialDTO> GetSupplierCompanyEngineMaterials()
        {
            var queryBuilder =
                     new QueryBuilder<SupplierCompanyMaterial>();
            return _supplierQuery.SupplierCompanyEngineMaterialsQuery(queryBuilder);
        }

        /// <summary>
        ///     获取合作公司的BFE物料。
        /// </summary>
        /// <returns>合作公司的BFE物料。</returns>
        public IQueryable<SupplierCompanyBFEMaterialDTO> GetSupplierCompanyBFEMaterials()
        {
            var queryBuilder =
                      new QueryBuilder<SupplierCompanyMaterial>();
            return _supplierQuery.SupplierCompanyBFEMaterialsQuery(queryBuilder);
        }

        /// <summary>
        ///     新增合作公司飞机物料。
        /// </summary>
        /// <param name="supplierCompanyAcMaterial">合作公司飞机物料DTO。</param>
        [Insert(typeof (SupplierCompanyAcMaterialDTO))]
        public void InsertSupplierCompanyAcMaterial(SupplierCompanyAcMaterialDTO supplierCompanyAcMaterial)
        {
            _staticLoad.RefreshSupplierMaterial();
            //判断增加的物料是否存在
            var supplierCompanyMaterial = _supplierCompanyMaterialRepository.GetAll()
                                                      .FirstOrDefault(
                                                          p => p.MaterialId == supplierCompanyAcMaterial.MaterialId
                                                               &&
                                                               p.SupplierCompanyId ==
                                                               supplierCompanyAcMaterial.SupplierCompanyId);
            if (supplierCompanyMaterial != null)
                throw new Exception("飞机物料已存在");
            var supplier = _supplierCompanyRepository.Get(supplierCompanyAcMaterial.SupplierCompanyId);
            if (supplier != null)
            {
                supplier.AddMaterial(supplierCompanyAcMaterial.MaterialId); //添加物料
            }
        }

        /// <summary>
        ///     删除合作公司飞机物料。
        /// </summary>
        /// <param name="supplierCompanyAcMaterial">合作公司飞机物料DTO。</param>
        [Delete(typeof (SupplierCompanyAcMaterialDTO))]
        public void DeleteSupplierCompanyAcMaterial(SupplierCompanyAcMaterialDTO supplierCompanyAcMaterial)
        {
            _staticLoad.RefreshSupplierMaterial();
            var supplierMaterial =
                _supplierCompanyMaterialRepository.Get(supplierCompanyAcMaterial.SupplierCompanyMaterialId);
            DelSupplierCompanyMaterial(supplierMaterial);
        }

        /// <summary>
        ///     新增合作公司发动机物料。
        /// </summary>
        /// <param name="supplierCompanyEngineMaterial">合作公司发动机物料DTO。</param>
        [Insert(typeof (SupplierCompanyEngineMaterialDTO))]
        public void InsertSupplierCompanyEngineMaterial(SupplierCompanyEngineMaterialDTO supplierCompanyEngineMaterial)
        {
            _staticLoad.RefreshSupplierMaterial();
            //判断增加的物料是否存在
            var supplierCompanyMaterial = _supplierCompanyMaterialRepository.GetAll()
                                                      .FirstOrDefault(
                                                          p => p.MaterialId == supplierCompanyEngineMaterial.MaterialId
                                                               &&
                                                               p.SupplierCompanyId ==
                                                               supplierCompanyEngineMaterial.SupplierCompanyId);
            if (supplierCompanyMaterial != null)
                throw new Exception("发动机物料已存在");

            var supplier = _supplierCompanyRepository.Get(supplierCompanyEngineMaterial.SupplierCompanyId);
            if (supplier != null)
            {
                supplier.AddMaterial(supplierCompanyEngineMaterial.MaterialId); //添加物料
            }
        }

        /// <summary>
        ///     删除合作公司发动机物料。
        /// </summary>
        /// <param name="supplierCompanyEngineMaterial">合作公司发动机物料DTO。</param>
        [Delete(typeof (SupplierCompanyEngineMaterialDTO))]
        public void DeleteSupplierCompanyEngineMaterial(SupplierCompanyEngineMaterialDTO supplierCompanyEngineMaterial)
        {
            _staticLoad.RefreshSupplierMaterial();
            var supplierMaterial =
                _supplierCompanyMaterialRepository.Get(supplierCompanyEngineMaterial.SupplierCompanyMaterialId);
            DelSupplierCompanyMaterial(supplierMaterial);
        }

        /// <summary>
        ///     新增合作公司BFE物料。
        /// </summary>
        /// <param name="supplierCompanyBFEMaterial">合作公司BFE物料DTO。</param>
        [Insert(typeof (SupplierCompanyBFEMaterialDTO))]
        public void InsertSupplierCompanyBFEMaterial(SupplierCompanyBFEMaterialDTO supplierCompanyBFEMaterial)
        {
            _staticLoad.RefreshSupplierMaterial();
            //判断增加的物料是否存在
            var supplierCompanyMaterial = _supplierCompanyMaterialRepository.GetAll()
                                                      .FirstOrDefault(
                                                          p => p.MaterialId == supplierCompanyBFEMaterial.MaterialId
                                                               &&
                                                               p.SupplierCompanyId ==
                                                               supplierCompanyBFEMaterial.SupplierCompanyId);
            if (supplierCompanyMaterial != null)
                throw new Exception("BFE物料已存在");

            var supplier = _supplierCompanyRepository.Get(supplierCompanyBFEMaterial.SupplierCompanyId);
            if (supplier != null)
            {    
                supplier.AddMaterial(supplierCompanyBFEMaterial.MaterialId); //添加物料
            }
        }

        /// <summary>
        ///     删除合作公司BFE物料。
        /// </summary>
        /// <param name="supplierCompanyBFEMaterial">合作公司BFE物料DTO。</param>
        [Delete(typeof (SupplierCompanyBFEMaterialDTO))]
        public void DeleteSupplierCompanyBFEMaterial(SupplierCompanyBFEMaterialDTO supplierCompanyBFEMaterial)
        {
            _staticLoad.RefreshSupplierMaterial();
            var supplierMaterial =
                _supplierCompanyMaterialRepository.Get(supplierCompanyBFEMaterial.SupplierCompanyMaterialId);
            DelSupplierCompanyMaterial(supplierMaterial);
        }

        /// <summary>
        ///     删除合作公司物料
        /// </summary>
        /// <param name="supplierCompanyBFEMaterial"></param>
        private void DelSupplierCompanyMaterial(SupplierCompanyMaterial supplierCompanyBFEMaterial)
        {
            _staticLoad.RefreshSupplierMaterial();
            _supplierCompanyMaterialRepository.Remove(supplierCompanyBFEMaterial);
        }

        #endregion
    }
}