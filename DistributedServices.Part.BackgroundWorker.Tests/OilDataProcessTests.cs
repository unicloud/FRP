#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:13
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
using UniCloud.DataService.DataProcess;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.Part.BackgroundWorker.Tests
{
    [TestClass]
    public class OilDataProcessTests
    {
        [TestMethod]
        public void PorcessOilDataTest()
        {
            // Arrange
            var oilProcess = UniContainer.Resolve<OilDataProcess>();

            // Act
            oilProcess.ProcessEngine();
            oilProcess.ProcessAPU();

            // Assert
            Assert.IsTrue(true);
        }
    }
}