#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/17，15:11
// 文件名：SupplierBCTests.cs
// 程序集：UniCloud.Application.PurchaseBC.Tests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PurchaseBC.Query.AircraftTypeQueries;
using UniCloud.Application.PurchaseBC.Query.CurrencyQueries;
using UniCloud.Application.PurchaseBC.Query.SupplierQueries;
using UniCloud.Application.PurchaseBC.SupplierServices;
using UniCloud.Domain.PurchaseBC.Aggregates.CurrencyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.LinkmanAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierCompanyMaterialAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierRoleAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;
using Microsoft.Practices.Unity;
#endregion

namespace UniCloud.Application.PurchaseBC.Tests.Services
{
    [TestClass]
    public class SupplierBCTests
    {
        [TestInitialize]
        public void Initialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PurchaseBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 供应商相关配置，包括查询，应用服务，仓储注册

                .RegisterType<ISupplierQuery, SupplierQuery>()
                .RegisterType<ISupplierAppService, SupplierAppService>()
                .RegisterType<ISupplierCompanyRepository, SupplierCompanyRepository>()
                .RegisterType<ISupplierRepository, SupplierRepository>()
                .RegisterType<ILinkmanRepository, LinkmanRepository>()
                .RegisterType<ISupplierRoleRepository, SupplierRoleRepository>()
                .RegisterType<ISupplierCompanyMaterialRepository, SupplierCompanyMaterialRepository>()
                .RegisterType<IAircraftTypeQuery, AircraftTypeQuery>()
                .RegisterType<ICurrencyRepository,CurrencyRepository>()
                .RegisterType<ICurrencyQuery,CurrencyQuery>()

                #endregion

                ;
        }

        [TestCleanup]
        public void Cleanup()
        {
        }

        /// <summary>
        ///     获取合作公司
        /// </summary>
        [TestMethod]
        public void GetSupplierCompany()
        {
            var service = DefaultContainer.Resolve<ISupplierAppService>();
            var result = service.GetSupplierCompanys().ToList();
            Assert.IsNotNull(result);
        }

        /// <summary>
        ///     修改合作公司
        /// </summary>
        [TestMethod]
        public void ModifySupplierCompany()
        {
            var service = DefaultContainer.Resolve<ISupplierAppService>();
            var supplierCompany = service.GetSupplierCompanys().FirstOrDefault();
            if (supplierCompany != null)
            {
                supplierCompany.AircraftLeaseSupplier = false;
                service.ModifySupplierCompany(supplierCompany);
            }
        }

        /// <summary>
        ///     获取供应商
        /// </summary>
        [TestMethod]
        public void GetSupplier()
        {
            var service = DefaultContainer.Resolve<ISupplierAppService>();
            var result = service.GetSuppliers().ToList();
            Assert.IsNotNull(result);
        }

        /// <summary>
        ///     获取联系人信息
        /// </summary>
        [TestMethod]
        public void GetLinkmans()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ISupplierAppService>();

            // Act
            var result = service.GetLinkmans().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        /// <summary>
        ///     获取合作公司飞机物料
        /// </summary>
        [TestMethod]
        public void GetSupplierCompanyMaterials()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ISupplierAppService>();
            // Act
            var result = service.GetSupplierCompanyAcMaterials().ToList();
            // Assert
            Assert.IsTrue(result.Any());
        }

        /// <summary>
        ///     获取合作公司发动机物料
        /// </summary>
        [TestMethod]
        public void GetSupplierCompanyEngineMaterials()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ISupplierAppService>();
            // Act
            var result = service.GetSupplierCompanyEngineMaterials().ToList();
            // Assert
            Assert.IsTrue(result.Any());
        }

        /// <summary>
        ///     获取合作公司发动机物料
        /// </summary>
        [TestMethod]
        public void GetSupplierCompanyBFEMaterials()
        {
            // Arrange
            var service = DefaultContainer.Resolve<ISupplierAppService>();
            // Act
            var result = service.GetSupplierCompanyBFEMaterials().ToList();
            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}