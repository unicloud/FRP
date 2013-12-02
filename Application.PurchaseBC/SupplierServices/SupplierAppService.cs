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
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
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
        private readonly ISupplierCompanyRepository _supplierCompanyRepository;
        private readonly ISupplierQuery _supplierQuery;
        private readonly ISupplierRoleRepository _supplierRoleRepository;
        private readonly ILinkmanRepository _linkmanRepository;
        private readonly IMaterialRepository _materialRepository;
        public SupplierAppService(ISupplierQuery supplierQuery, ISupplierRoleRepository supplierRoleRepository,
                                  ISupplierCompanyRepository supplierCompanyRepository, ILinkmanRepository linkmanRepository, IMaterialRepository materialRepository)
        {
            _supplierQuery = supplierQuery;
            _supplierRoleRepository = supplierRoleRepository;
            _supplierCompanyRepository = supplierCompanyRepository;
            _linkmanRepository = linkmanRepository;
            _materialRepository = materialRepository;
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
    linkman.Fax, linkman.Email, new Address(null, null, linkman.Address, null),linkman.SourceId);
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
        ///     获取供应商物料。
        /// </summary>
        /// <returns>供应商物料。</returns>
        public IQueryable<SupplierCompanyMaterialDTO> GetSupplierCompanyMaterials()
        {
                        var queryBuilder =
                new QueryBuilder<SupplierCompany>();
            return _supplierQuery.SupplierCompanyMaterialsQuery(queryBuilder);
        }


        /// <summary>
        ///     更新合作公司物料。
        /// </summary>
        /// <param name="supplierCompanyMaterial">合作公司物料DTO。</param>
        [Update(typeof(SupplierCompanyMaterialDTO))]
        public void ModifySupplierCompanyMaterial(SupplierCompanyMaterialDTO supplierCompanyMaterial)
        {
            //var persistSupplierCompanyMaterial =
            //    _supplierCompanyRepository.Get(supplierCompanyMaterial.SupplierCompanyId);
            //var persistAcMaterial = persistSupplierCompanyMaterial
            //                                     .Materials.OfType<AircraftMaterial>().ToList();
            //var persistBFEMaterial = persistSupplierCompanyMaterial
            //                                                  .Materials.OfType<BFEMaterial>().ToList();
            //var persistEngineMaterial = persistSupplierCompanyMaterial
            //                                                  .Materials.OfType<EngineMaterial>().ToList();
            ////更新飞机物料
            //UpdateAcMaterial(persistSupplierCompanyMaterial, supplierCompanyMaterial.AircraftMaterials, persistAcMaterial);
            ////更新发动机物料
            //UpdateEngineMaterial(persistSupplierCompanyMaterial, supplierCompanyMaterial.EngineMaterials, persistEngineMaterial);
            ////更新BFE物料
            //UpdateBfeMaterial(persistSupplierCompanyMaterial, supplierCompanyMaterial.BFEMaterials, persistBFEMaterial);

        }

        ///// <summary>
        ///// 更新飞机物料信息
        ///// </summary>
        ///// <param name="supplierCompany">合作公司</param>
        ///// <param name="actMaterials">飞机物料信息</param>
        ///// <param name="persistAcMaterials">已持久化飞机物料信息</param>
        //private void UpdateAcMaterial(SupplierCompany supplierCompany, List<AircraftMaterialDTO> actMaterials,
        //    List<AircraftMaterial> persistAcMaterials)
        //{

        //    //遍历从前台传过来的飞机物料，如果数据库中没有存在，则此物料为新增。
        //    actMaterials.ForEach(p =>
        //    {
        //        if (persistAcMaterials.All(c => c.Id != p.AcMaterialId))
        //        {
        //            var newAcMaterial = _materialRepository.Get(p.AcMaterialId);
        //            if (newAcMaterial!=null)
        //            {
        //                supplierCompany.Materials.Add(newAcMaterial);
        //            }
        //        }
        //    });
        //    //与前台传过来的飞机物料相比，如果前台的物料没有，数据库有，则该物料为删除。
        //    persistAcMaterials.ForEach(p =>
        //    {
        //        if (actMaterials.All(c=>c.AcMaterialId!=p.Id))
        //        {
        //            supplierCompany.Materials.Remove(p);
        //        }
        //    });

        //}

        ///// <summary>
        ///// 更新发动机物料信息
        ///// </summary>
        ///// <param name="supplierCompany">合作公司</param>
        ///// <param name="engineMaterials">发动机物料信息</param>
        ///// <param name="persistEngineMaterials">已持久化发动机物料信息</param>
        //private void UpdateEngineMaterial(SupplierCompany supplierCompany, List<EngineMaterialDTO> engineMaterials,
        //    List<EngineMaterial> persistEngineMaterials)
        //{

        //    //遍历从前台传过来的发动机物料，如果数据库中没有存在，则此物料为新增。
        //    engineMaterials.ForEach(p =>
        //    {
        //        if (persistEngineMaterials.All(c => c.Id != p.PartId))
        //        {
        //            var newAcMaterial = _materialRepository.Get(p.PartId);
        //            if (newAcMaterial != null)
        //            {
        //                supplierCompany.Materials.Add(newAcMaterial);
        //            }
        //        }
        //    });
        //    //与前台传过来的发动机物料相比，如果前台的物料没有，数据库有，则该物料为删除。
        //    persistEngineMaterials.ForEach(p =>
        //    {
        //        if (engineMaterials.All(c => c.PartId != p.Id))
        //        {
        //            supplierCompany.Materials.Remove(p);
        //        }
        //    });

        //}

        ///// <summary>
        ///// 更新BFE物料信息
        ///// </summary>
        ///// <param name="supplierCompany">合作公司</param>
        ///// <param name="bfeMaterials">BFE物料信息</param>
        ///// <param name="persistBfeMaterials">已持久化BFE物料信息</param>
        //private void UpdateBfeMaterial(SupplierCompany supplierCompany, List<BFEMaterialDTO> bfeMaterials,
        //    List<BFEMaterial> persistBfeMaterials)
        //{

        //    //遍历从前台传过来的BFE物料，如果数据库中没有存在，则此物料为新增。
        //    bfeMaterials.ForEach(p =>
        //    {
        //        if (persistBfeMaterials.All(c => c.Id != p.PartId))
        //        {
        //            var newAcMaterial = _materialRepository.Get(p.PartId);
        //            if (newAcMaterial != null)
        //            {
        //                supplierCompany.Materials.Add(newAcMaterial);
        //            }
        //        }
        //    });
        //    //与前台传过来的BFE物料相比，如果前台的物料没有，数据库有，则该物料为删除。
        //    persistBfeMaterials.ForEach(p =>
        //    {
        //        if (bfeMaterials.All(c => c.PartId != p.Id))
        //        {
        //            supplierCompany.Materials.Remove(p);
        //        }
        //    });

        //}
        #endregion
    }
}