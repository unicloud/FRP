using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
     [TestClass]
   public  class ForwardRepositoryTest
    {
        [TestInitialize]
        public void TestInitialize()
        {
            
            Configuration.Create()
                         .UseAutofac()
                         .CreateLog()
                         .Register<IQueryableUnitOfWork, PurchaseBCUnitOfWork>
                         ().Register<IForwarderRepository, ForwarderRepository>();
        }
        [TestMethod]
        public void TestRemoveForward()
        {

            var bc = DefaultContainer.Resolve<IForwarderRepository>();
           var result=bc.GetAll().FirstOrDefault();
           bc.Remove(result);
           bc.UnitOfWork.Commit();
        }
        [TestCleanup]
        public void TestCleanup()
        {
        }
    }
}
