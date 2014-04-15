using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.MerchantDataService;

namespace UniCloud.MerchantSync.Tests
{
    [TestClass]
    public class MerchantSyncTest
    {
        [TestMethod]
        public void TestSync()
        {
            MerchantDataSync sync=new MerchantDataSync();
            sync.SyncLinkmanInfo();
            //sync.SyncMerchantInfo();
        }

    }
}
