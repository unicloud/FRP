#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PaymentBC.Aggregates.ContractEngineAgg;
using UniCloud.Infrastructure.Data.PaymentBC.Repositories;
using UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.Tests
{
    [TestClass]
    public class ContractEngineTest
    {
        [TestMethod]
        public void GetAllContractEngine()
        {
            // Arrange
            var contractEngineRepository = UniContainer.Resolve<IContractEngineRepository>();
            var retsult = contractEngineRepository.GetAll().ToList();
            Assert.IsTrue(retsult.Any());
        }
    }
}