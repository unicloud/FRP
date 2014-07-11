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
using System.Linq.Expressions;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.Common.ValueObjects;
using UniCloud.Domain.PurchaseBC.Aggregates.BankAccountAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.SupplierServices
{
    /// <summary>
    ///     实现供应商服务接口。
    ///     用于处于供应商相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class SupplierAppService : ContextBoundObject, ISupplierAppService
    {
        private readonly IBankAccountRepository _bankAccountRepository;
        private readonly ILinkmanRepository _linkmanRepository;
        private readonly ISupplierCompanyMaterialRepository _supplierCompanyMaterialRepository;
        private readonly ISupplierCompanyRepository _supplierCompanyRepository;
        private readonly ISupplierQuery _supplierQuery;
        private readonly ISupplierRepository _supplierRepository;
        private readonly ISupplierRoleRepository _supplierRoleRepository;

        public SupplierAppService(ISupplierQuery supplierQuery, ISupplierRepository supplierRepository,
            ISupplierRoleRepository supplierRoleRepository,
            ISupplierCompanyRepository supplierCompanyRepository, ILinkmanRepository linkmanRepository,
            IBankAccountRepository bankAccountRepository,
            ISupplierCompanyMaterialRepository supplierCompanyMaterialRepository)
        {
            _supplierQuery = supplierQuery;
            _supplierRepository = supplierRepository;
            _supplierRoleRepository = supplierRoleRepository;
            _supplierCompanyRepository = supplierCompanyRepository;
            _linkmanRepository = linkmanRepository;
            _bankAccountRepository = bankAccountRepository;
            _supplierCompanyMaterialRepository = supplierCompanyMaterialRepository;
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
            SupplierCompany supplierCmy = _supplierCompanyRepository.Get(supplierCompany.SupplierCompanyId);
            if (supplierCmy == null) throw new Exception("找不到合作公司。");
            supplierCompany
                //新增飞机租赁供应商角色
                .If(p => p.AircraftLeaseSupplier, p =>
                {
                    SupplierRole aircraftLeaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (AircraftLeaseSupplier),
                            p.SupplierCompanyId);
                    if (aircraftLeaseSupplier != null) return;
                    AircraftLeaseSupplier newAircraftLeaseSupplier =
                        SupplierRoleFactory.CreateAircraftLeaseSupplier(supplierCmy);
                    _supplierRoleRepository.Add(newAircraftLeaseSupplier);
                })
                //新增飞机购买供应商角色
                .If(p => p.AircraftPurchaseSupplier, p =>
                {
                    SupplierRole aircraftPurchaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (AircraftPurchaseSupplier),
                            p.SupplierCompanyId);
                    if (aircraftPurchaseSupplier != null) return;
                    AircraftPurchaseSupplier newAircraftPurchaseSupplier =
                        SupplierRoleFactory.CreateAircraftPurchaseSupplier(supplierCmy);
                    _supplierRoleRepository.Add(newAircraftPurchaseSupplier);
                })
                //新增发动机租赁供应商角色
                .If(p => p.EngineLeaseSupplier, p =>
                {
                    SupplierRole engineLeaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (EngineLeaseSupplier),
                            p.SupplierCompanyId);
                    if (engineLeaseSupplier != null) return;
                    EngineLeaseSupplier newEngineLeaseSupplier =
                        SupplierRoleFactory.CreateEngineLeaseSupplier(supplierCmy);
                    _supplierRoleRepository.Add(newEngineLeaseSupplier);
                })
                //新增发动机购买供应商角色
                .If(p => p.EnginePurchaseSupplier, p =>
                {
                    SupplierRole enginePurchaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (EnginePurchaseSupplier),
                            p.SupplierCompanyId);
                    if (enginePurchaseSupplier != null) return;
                    EnginePurchaseSupplier newEnginePurchaseSupplier =
                        SupplierRoleFactory.CreateEnginePurchaseSupplier(supplierCmy);
                    _supplierRoleRepository.Add(newEnginePurchaseSupplier);
                })
                //新增BFE供应商角色
                .If(p => p.BFEPurchaseSupplier, p =>
                {
                    SupplierRole bfePurchaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (BFEPurchaseSupplier),
                            p.SupplierCompanyId);
                    if (bfePurchaseSupplier != null) return;
                    BFEPurchaseSupplier newBfePurchaseSupplier =
                        SupplierRoleFactory.CreateBFEPurchaseSupplier(supplierCmy);
                    _supplierRoleRepository.Add(newBfePurchaseSupplier);
                })
                //新增维修物料角色
                .If(p => p.MaintainSupplier, p =>
                {
                    SupplierRole maintainPurchaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (MaintainSupplier),
                            p.SupplierCompanyId);
                    if (maintainPurchaseSupplier != null) return;
                    MaintainSupplier newMaintainPurchaseSupplier =
                        SupplierRoleFactory.CreateMaintainSupplier(supplierCmy);
                    _supplierRoleRepository.Add(newMaintainPurchaseSupplier);
                })
                //新增其他角色
                .If(p => p.OtherSupplier, p =>
                {
                    SupplierRole otherSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (OtherSupplier),
                            p.SupplierCompanyId);
                    if (otherSupplier != null) return;
                    OtherSupplier newOtherSupplier = SupplierRoleFactory.CreateOtherSupplier(supplierCmy);
                    _supplierRoleRepository.Add(newOtherSupplier);
                });
        }

        /// <summary>
        ///     删除供应商角色。
        /// </summary>
        /// <param name="supplierCompany"></param>
        private void RemoveSupplierCompanyRole(SupplierCompanyDTO supplierCompany)
        {
            SupplierCompany supplierCmy = _supplierCompanyRepository.Get(supplierCompany.SupplierCompanyId);
            if (supplierCmy == null) throw new Exception("找不到合作公司。");
            supplierCompany
                //删除飞机租赁供应商角色
                .If(p => !p.AircraftLeaseSupplier, p =>
                {
                    SupplierRole aircraftLeaseSupplier =
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
                    SupplierRole aircraftPurchaseSupplier =
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
                    SupplierRole engineLeaseSupplier =
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
                    SupplierRole enginePurchaseSupplier =
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
                    SupplierRole bfePurchaseSupplier =
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
                    SupplierRole maintainSupplierPurchaseSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (MaintainSupplier),
                            p.SupplierCompanyId);
                    if (maintainSupplierPurchaseSupplier != null)
                    {
                        _supplierRoleRepository.Remove(maintainSupplierPurchaseSupplier);
                    }
                })
                //删除其他供应商角色
                .If(p => !p.OtherSupplier, p =>
                {
                    SupplierRole otherSupplier =
                        _supplierRoleRepository.GetSupplierRoleBySupplierCmpyId(typeof (OtherSupplier),
                            p.SupplierCompanyId);
                    if (otherSupplier != null)
                    {
                        _supplierRoleRepository.Remove(otherSupplier);
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
            Linkman newLinkman = LinkmanFactory.CreateLinkman(linkman.Name, linkman.IsDefault, linkman.TelePhone,
                linkman.Mobile,
                linkman.Fax, linkman.Email, linkman.Department, new Address(null, null, linkman.Address, null),
                linkman.SourceId, linkman.CustCode, DateTime.Now);
            _linkmanRepository.Add(newLinkman);
        }

        /// <summary>
        ///     更新联系人。
        /// </summary>
        /// <param name="linkman">联系人DTO。</param>
        [Update(typeof (LinkmanDTO))]
        public void ModifyLinkman(LinkmanDTO linkman)
        {
            Linkman updateLinkman = _linkmanRepository.Get(linkman.LinkmanId); //获取需要更新的对象。

            //更新。
            updateLinkman.Name = linkman.Name;
            updateLinkman.IsDefault = linkman.IsDefault;
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
            Linkman deletedLinkman = _linkmanRepository.Get(linkman.LinkmanId); //获取需要更新的对象。
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
            //判断增加的物料是否存在
            SupplierCompanyMaterial supplierCompanyMaterial = _supplierCompanyMaterialRepository.GetAll()
                .FirstOrDefault(
                    p => p.MaterialId == supplierCompanyAcMaterial.MaterialId
                         &&
                         p.SupplierCompanyId ==
                         supplierCompanyAcMaterial.SupplierCompanyId);
            if (supplierCompanyMaterial != null)
                throw new Exception("飞机物料已存在");
            SupplierCompany supplier = _supplierCompanyRepository.Get(supplierCompanyAcMaterial.SupplierCompanyId);
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
            SupplierCompanyMaterial supplierMaterial =
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
            //判断增加的物料是否存在
            SupplierCompanyMaterial supplierCompanyMaterial = _supplierCompanyMaterialRepository.GetAll()
                .FirstOrDefault(
                    p => p.MaterialId == supplierCompanyEngineMaterial.MaterialId
                         &&
                         p.SupplierCompanyId ==
                         supplierCompanyEngineMaterial.SupplierCompanyId);
            if (supplierCompanyMaterial != null)
                throw new Exception("发动机物料已存在");

            SupplierCompany supplier = _supplierCompanyRepository.Get(supplierCompanyEngineMaterial.SupplierCompanyId);
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
            SupplierCompanyMaterial supplierMaterial =
                _supplierCompanyMaterialRepository.Get(supplierCompanyEngineMaterial.SupplierCompanyMaterialId);
            DelSupplierCompanyMaterial(supplierMaterial);
        }

        /// <summary>
        ///     新增合作公司BFE物料。
        /// </summary>
        /// <param name="supplierCompanyBfeMaterial">合作公司BFE物料DTO。</param>
        [Insert(typeof (SupplierCompanyBFEMaterialDTO))]
        public void InsertSupplierCompanyBFEMaterial(SupplierCompanyBFEMaterialDTO supplierCompanyBfeMaterial)
        {
            //判断增加的物料是否存在
            SupplierCompanyMaterial supplierCompanyMaterial = _supplierCompanyMaterialRepository.GetAll()
                .FirstOrDefault(
                    p => p.MaterialId == supplierCompanyBfeMaterial.MaterialId
                         &&
                         p.SupplierCompanyId ==
                         supplierCompanyBfeMaterial.SupplierCompanyId);
            if (supplierCompanyMaterial != null)
                throw new Exception("BFE物料已存在");

            SupplierCompany supplier = _supplierCompanyRepository.Get(supplierCompanyBfeMaterial.SupplierCompanyId);
            if (supplier != null)
            {
                supplier.AddMaterial(supplierCompanyBfeMaterial.MaterialId); //添加物料
            }
        }

        /// <summary>
        ///     删除合作公司BFE物料。
        /// </summary>
        /// <param name="supplierCompanyBfeMaterial">合作公司BFE物料DTO。</param>
        [Delete(typeof (SupplierCompanyBFEMaterialDTO))]
        public void DeleteSupplierCompanyBFEMaterial(SupplierCompanyBFEMaterialDTO supplierCompanyBfeMaterial)
        {
            SupplierCompanyMaterial supplierMaterial =
                _supplierCompanyMaterialRepository.Get(supplierCompanyBfeMaterial.SupplierCompanyMaterialId);
            DelSupplierCompanyMaterial(supplierMaterial);
        }

        /// <summary>
        ///     删除合作公司物料
        /// </summary>
        /// <param name="supplierCompanyBfeMaterial"></param>
        private void DelSupplierCompanyMaterial(SupplierCompanyMaterial supplierCompanyBfeMaterial)
        {
            _supplierCompanyMaterialRepository.Remove(supplierCompanyBfeMaterial);
        }

        #endregion

        public void SyncLinkmanInfo(List<LinkmanDTO> linkmen)
        {
            foreach (LinkmanDTO linkman in linkmen)
            {
                LinkmanDTO linkman1 = linkman;
                Expression<Func<Linkman, bool>> condition = p => p.CustCode == linkman1.CustCode;

                Linkman tempLinkman = _linkmanRepository.GetLinkman(condition);
                if (tempLinkman != null)
                {
                    if (linkman.UpdateDate.CompareTo(tempLinkman.UpdateDate) > 0)
                    {
                        LinkmanFactory.SetLinkman(tempLinkman, linkman.Name, linkman.Department, linkman.UpdateDate);
                        _linkmanRepository.Modify(tempLinkman);
                    }
                }
                else
                {
                    tempLinkman = LinkmanFactory.CreateLinkman(linkman.Name, true, linkman.TelePhone, linkman.Mobile,
                        linkman.Fax, linkman.Email, linkman.Department, new Address(null, null, linkman.Address, null),
                        Guid.NewGuid(), linkman.CustCode, linkman.UpdateDate);
                    _linkmanRepository.Add(tempLinkman);
                }
            }
            _linkmanRepository.UnitOfWork.CommitAndRefreshChanges();
        }

        public void SyncSupplierInfo(List<SupplierDTO> suppliers)
        {
            foreach (SupplierDTO supplierCompany in suppliers)
            {
                SupplierDTO company = supplierCompany;
                Expression<Func<Linkman, bool>> conditionLinkman = p => p.CustCode == company.Code;
                Linkman tempLinkman = _linkmanRepository.GetLinkman(conditionLinkman);

                SupplierDTO company1 = supplierCompany;
                Expression<Func<SupplierCompany, bool>> conditionSupplierCompany = p => p.Code == company1.Code;

                SupplierCompany tempSupplierCompany =
                    _supplierCompanyRepository.GetSupplierCompany(conditionSupplierCompany);
                if (tempSupplierCompany != null)
                {
                    if (supplierCompany.UpdateDate.CompareTo(tempSupplierCompany.UpdateDate) > 0)
                    {
                        tempSupplierCompany.Code = supplierCompany.Code;
                        tempSupplierCompany.UpdateDate = supplierCompany.UpdateDate;
                        if (tempLinkman != null)
                            tempSupplierCompany.LinkmanId = tempLinkman.SourceId;
                        _supplierCompanyRepository.Modify(tempSupplierCompany);
                    }
                }
                else
                {
                    tempSupplierCompany = SupplierCompanyFactory.CreateSupplieCompany(supplierCompany.Code);
                    tempSupplierCompany.UpdateDate = supplierCompany.UpdateDate;
                    if (tempLinkman != null)
                    {
                        tempSupplierCompany.LinkmanId = tempLinkman.SourceId;
                    }
                    _supplierCompanyRepository.Add(tempSupplierCompany);
                }

                SupplierDTO supplier2 = supplierCompany;
                Expression<Func<Supplier, bool>> conditionSupplier = p => p.Code == supplier2.Code;

                Supplier tempSupplier = _supplierRepository.GetSupplier(conditionSupplier);
                if (tempSupplier != null)
                {
                    if (supplierCompany.UpdateDate.CompareTo(tempSupplier.UpdateDate) > 0)
                    {
                        SupplierFactory.SetSupplier(tempSupplier,
                            supplierCompany.SupplierType.Contains("国内") ? SupplierType.国内 : SupplierType.国外,
                            supplierCompany.Code, supplierCompany.Name, supplierCompany.ShortName, true, null,
                            supplierCompany.UpdateDate, tempSupplierCompany.Id);
                        _supplierRepository.Modify(tempSupplier);
                    }
                }
                else
                {
                    tempSupplier = SupplierFactory.CreateSupplier();
                    SupplierFactory.SetSupplier(tempSupplier,
                        supplierCompany.SupplierType.Contains("国内") ? SupplierType.国内 : SupplierType.国外,
                        supplierCompany.Code, supplierCompany.Name, supplierCompany.ShortName, true, null,
                        supplierCompany.UpdateDate, tempSupplierCompany.Id);
                    _supplierRepository.Add(tempSupplier);
                }
            }
            _supplierCompanyRepository.UnitOfWork.CommitAndRefreshChanges();

            //foreach (var supplier in suppliers)
            //{

            //}
            //_supplierRepository.UnitOfWork.CommitAndRefreshChanges();
        }

        public void SyncBankAccountInfo(List<BankAccountDTO> bankAccounts)
        {
            foreach (BankAccountDTO bankAccount in bankAccounts)
            {
                BankAccountDTO account = bankAccount;
                Expression<Func<Supplier, bool>> conditionSupplier = p => p.Code == account.CustCode;
                Supplier tempSupplier = _supplierRepository.GetSupplier(conditionSupplier);
                if (tempSupplier == null)
                {
                    continue;
                }
                BankAccountDTO account1 = bankAccount;
                Expression<Func<BankAccount, bool>> condition = p => p.CustCode == account1.CustCode;

                BankAccount tempBankAccount = _bankAccountRepository.GetBankAccount(condition);
                if (tempBankAccount != null)
                {
                    if (bankAccount.UpdateDate.CompareTo(tempBankAccount.UpdateDate) > 0)
                    {
                        BankAccountFactory.SetBankAccount(tempBankAccount, bankAccount.CustCode,
                            tempBankAccount.IsCurrent,
                            bankAccount.Account, bankAccount.Name, bankAccount.Bank, bankAccount.Branch,
                            bankAccount.Country,
                            bankAccount.Address, bankAccount.UpdateDate, tempSupplier.Id);
                        _bankAccountRepository.Modify(tempBankAccount);
                    }
                }
                else
                {
                    tempBankAccount = BankAccountFactory.CreateBankAccount();
                    BankAccountFactory.SetBankAccount(tempBankAccount, bankAccount.CustCode, true, bankAccount.Account,
                        bankAccount.Name, bankAccount.Bank, bankAccount.Branch, bankAccount.Country,
                        bankAccount.Address, bankAccount.UpdateDate, tempSupplier.Id);
                    _bankAccountRepository.Add(tempBankAccount);
                }
            }
            _bankAccountRepository.UnitOfWork.CommitAndRefreshChanges();
        }

        #region 获取供应商信息

        /// <summary>
        /// 获取所有的飞机供应商（飞机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetAircraftSuppliers()
        {
            return _supplierQuery.GetAircraftSuppliers();
        }

        /// <summary>
        /// 获取所有的发动机供应商（发动机采购和租赁供应商）
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetEngineSuppliers()
        {
            return _supplierQuery.GetEngineSuppliers();
        }

        /// <summary>
        /// 获取所有的飞机采购供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetAircraftPurchaseSuppliers()
        {
            return _supplierQuery.GetAircraftPurchaseSuppliers();
        }

        /// <summary>
        /// 获取所有的飞机租赁供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetAircraftLeaseSuppliers()
        {
            return _supplierQuery.GetAircraftLeaseSuppliers();
        }

        /// <summary>
        /// 获取所有的发动机采购供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetEnginePurchaseSuppliers()
        {
            return _supplierQuery.GetEnginePurchaseSuppliers();
        }

        /// <summary>
        /// 获取所有的发动机租赁供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetEngineLeaseSuppliers()
        {
            return _supplierQuery.GetEngineLeaseSuppliers();
        }

        /// <summary>
        /// 获取所有的BFE供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetBfeSuppliers()
        {
            return _supplierQuery.GetBfeSuppliers();
        }

        /// <summary>
        /// 获取所有的维修供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetMaintainSuppliers()
        {
            return _supplierQuery.GetMaintainSuppliers();
        }

        /// <summary>
        /// 获取所有的"其他"供应商
        /// </summary>
        /// <returns></returns>
        public List<SupplierDTO> GetOtherSuppliers()
        {
            return _supplierQuery.GetOtherSuppliers();
        }
        #endregion
    }
}