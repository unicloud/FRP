#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:33
// 方案：FRP
// 项目：DistributedServices.Part.BackgroundWorker.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.DataService.DataSync;

#endregion

namespace UniCloud.DistributedServices.Part.BackgroundWorker.Tests
{
    [TestClass]
    public class DataSyncTest
    {
        [TestMethod]
        public void FlightLogSyncTest()
        {
            // Arrange
            var dataSync = new FlightLogSync();

            // Act
            dataSync.DataSynchronous();

            // Assert
            Assert.IsTrue(true);
        }
    }
}