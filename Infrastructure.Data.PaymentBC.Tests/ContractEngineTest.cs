using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PaymentBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.Infrastructure.Data.PaymentBC.Tests
{
    [TestClass]
  public   class ContractEngineTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            Configuration.Create()
                .UseAutofac()
                .CreateLog()
                .Register<IQueryableUnitOfWork, PaymentBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IContractEngineRepository, ContractEngineRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetAllContractEngine()
        {
            // Arrange
            var contractEngineRepository = DefaultContainer.Resolve<IContractEngineRepository>();
          var retsult= contractEngineRepository.GetAll().ToList();
          Assert.IsTrue(retsult.Any());
        }

     

    }
}
