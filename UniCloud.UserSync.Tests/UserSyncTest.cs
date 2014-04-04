using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.BaseManagementBC.Query.UserQueries;
using UniCloud.Infrastructure.Data.BaseManagementBC.Repositories;
using UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork;
using UniCloud.UserDataService;

namespace UniCloud.UserSync.Tests
{
    [TestClass]
    public class UserSyncTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var userDataSync = new UserDataSync(new UserQuery(new BaseManagementBCUnitOfWork()), new UserRepository(new BaseManagementBCUnitOfWork()));
            userDataSync.SyncUserInfo();
        }
    }
}
