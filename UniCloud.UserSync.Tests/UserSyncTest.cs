using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.UserDataService;

namespace UniCloud.UserSync.Tests
{
    [TestClass]
    public class UserSyncTest
    {
        [TestMethod]
        public void TestMethod()
        {
            var userDataSync = new UserDataSync();
            userDataSync.SyncOrganizationInfo();
        }
    }
}
