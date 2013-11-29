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

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.MaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

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
            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IAircraftTypeRepository, AircraftTypeRepository>()
                .Register<IMaterialRepository, MaterialRepository>()
                .Register<ISupplierCompanyRepository, SupplierCompanyRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void AddMaterialWithSupplier()
        {
            //// Arrange
            //var acTypeRep = DefaultContainer.Resolve<IAircraftTypeRepository>();
            //var supplierRep = DefaultContainer.Resolve<ISupplierCompanyRepository>();

            //var acType = acTypeRep.GetAll().FirstOrDefault();
            //var material = MaterialFactory.CreateAircraftMaterial();
            //material.SetAircraftType(acType);
            //var supplierCompany = SupplierCompanyFactory.CreateSupplieCompany("0003");

            //// Act
            //supplierCompany.Materials.Add(material);
            //supplierRep.Add(supplierCompany);
            //supplierRep.UnitOfWork.Commit();
        }

        [TestMethod]
        public void GetMaterialWithSupplier()
        {
            // Arrange
            var supplierRep = DefaultContainer.Resolve<ISupplierCompanyRepository>();

            // Act
            var result = supplierRep.Get(3).Materials;

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}