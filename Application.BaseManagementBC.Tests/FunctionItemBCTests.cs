#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 20:32:30
// 文件名：FunctionItemBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 20:32:30
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.BaseManagementBC.FunctionItemServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.BaseManagementBC.Tests
{
    [TestClass]
    public class FunctionItemBCTests
    {
        [TestMethod]
        public void TestGetFunctionItems()
        {
            // Arrange
            var svc = UniContainer.Resolve<IFunctionItemAppService>();

            // Act
            var result = svc.GetFunctionItemsWithHierarchy();

            // Assert
            Assert.IsTrue(result.Any());
        }
    }
}