#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/16 15:09:25
// 文件名：CtrlUnitTest
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/16 15:09:25
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class CtrlUnitTest
    {
        [TestMethod]
        public void CreateCtrlUnits()
        {
            // Arrange
            var service = UniContainer.Resolve<ICtrlUnitRepository>();

            // Act
            var ctrlUnits = new List<CtrlUnit>
            {
                CtrlUnitFactory.CreateCtrlUnit("FH", "飞行小时"),
                CtrlUnitFactory.CreateCtrlUnit("FC", "飞行循环"),
                CtrlUnitFactory.CreateCtrlUnit("Day", "日历天"),
                CtrlUnitFactory.CreateCtrlUnit("Month", "日历月"),
            };
            ctrlUnits.ForEach(service.Add);
            service.UnitOfWork.Commit();
        }
    }
}