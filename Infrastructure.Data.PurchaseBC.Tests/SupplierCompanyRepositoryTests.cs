#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/22，13:13
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class SupplierCompanyRepositoryTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
                .Register<IMaterialRepository, MaterialRepository>()
                .Register<ISupplierCompanyRepository, SupplierCompanyRepository>()
                .Register<ISupplierCompanyMaterialRepository, SupplierCompanyMaterialRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void AddMaterialWithSupplier()
        {
            // Arrange
            var acTypeRep = UniContainer.Resolve<IAircraftTypeRepository>();
            var supplierRep = UniContainer.Resolve<ISupplierCompanyRepository>();

            var acType = acTypeRep.GetAll().FirstOrDefault();
            if (acType != null)
            {
                var material = MaterialFactory.CreateAircraftMaterial("B737-800", null, acType.Id);
                material.SetAircraftType(acType);
                var supplierCompany = supplierRep.GetAll().FirstOrDefault();
                if (supplierCompany != null) supplierCompany.AddMaterial(material);
            }

            // Act
            supplierRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void DeleteMaterialFromSupplier()
        {
            // Arrange
            var supRep = UniContainer.Resolve<ISupplierCompanyRepository>();
            var matRep = UniContainer.Resolve<IMaterialRepository>();
            var scmRep = UniContainer.Resolve<ISupplierCompanyMaterialRepository>();

            var supplier = supRep.GetAll().FirstOrDefault(s => s.SupplierCompanyMaterials.Any());
            if (supplier == null)
            {
                throw new ArgumentException("供应商为空！");
            }
            var material =
                matRep.GetAll()
                    .OfType<AircraftMaterial>()
                    .SelectMany(m => m.SupplierCompanyMaterials)
                    .Where(s => s.SupplierCompanyId == supplier.Id)
                    .Select(s => s.Material)
                    .FirstOrDefault();

            var scm =
                scmRep.GetFiltered(s => s.MaterialId == material.Id && s.SupplierCompanyId == supplier.Id)
                    .FirstOrDefault();

            // Act
            scmRep.Remove(scm);
            scmRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void GetMaterialWithSupplier()
        {
            // Arrange
            var supplierRep = UniContainer.Resolve<ISupplierCompanyRepository>();

            // Act
            var result = supplierRep.Get(3).SupplierCompanyMaterials.Select(s => s.Material);

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}