#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/03，15:11
// 方案：FRP
// 项目：Infrastructure.Crosscutting.Tests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using Microsoft.Practices.EnterpriseLibrary.SemanticLogging.Utility;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Infrastructure.Crosscutting.Logging;
using UniCloud.Infrastructure.Crosscutting.NetFramework.Logging;

#endregion

namespace UniCloud.Infrastructure.Crosscutting.Tests
{
    [TestClass]
    public class UniCloudLogTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            // 初始化缺省的日志工厂
            LoggerFactory.SetCurrent(new UniCloudLogFactory());
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void ShouldValidateEventSource()
        {
            EventSourceAnalyzer.InspectAll(UniCloudLog.Log);
        }
    }
}