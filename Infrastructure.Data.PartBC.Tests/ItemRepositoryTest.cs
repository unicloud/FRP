#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/6/13 22:30:26
// 文件名：ItemRepositoryTest
// 版本：V1.0.0
//
// 修改者：  时间：2014/6/13 22:30:26
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class ItemRepositoryTest
    {
        [TestMethod]
        public void CreateItems()
        {
            // Arrange
            var service = UniContainer.Resolve<IItemRepository>();

            // Act
            var items = new List<Item>
            {
                ItemFactory.CreateItem("发动机", "7200", null, "ENGINE - GENERAL", false),
                ItemFactory.CreateItem("风扇", "7220", null, "FAN SECTION", false),
                ItemFactory.CreateItem("风扇和低压压气机装置", "7221", null, "FAN AND BOOSTER ASSEMBLY", false),
                ItemFactory.CreateItem("风扇框架", "7223", null, "FAN FRAME ASSEMBLY", false),
                ItemFactory.CreateItem("高压压气机", "7230", null, "HP COMPRESSOR SECTION", false),
                ItemFactory.CreateItem("燃烧室", "7240", null, "COMBUSTION SECTION", false),
                ItemFactory.CreateItem("涡轮", "7250", null, "TURBINE SECTION", false),
                ItemFactory.CreateItem("发动机燃油和控制", "7300", null, "ENGINE FUEL AND CONTROL - GENERAL", false),
                ItemFactory.CreateItem("点火系统", "7400", null, "IGNITION - GENERAL", false),
                ItemFactory.CreateItem("发动机引气", "7500", null, "AIR - GENERAL", false),
                ItemFactory.CreateItem("发动机防冰", "7510", null, "ENGINE ANTI-ICING", false),
                ItemFactory.CreateItem("冷却", "7520", null, "COOLING", false),
                ItemFactory.CreateItem("发动机控制", "7600", null, "ENGINE CONTROLS - GENERAL", false),
                ItemFactory.CreateItem("功率控制", "7610", null, "POWER CONTROL", false),
                ItemFactory.CreateItem("油门控制", "7611", null, "THROTTLE CONTROL", false),
                ItemFactory.CreateItem("发动机主控制", "7612", null, "ENGINE MASTER CONTROL", false),
                ItemFactory.CreateItem("发动机滑油", "7900", null, "OIL - GENERAL", false),
                ItemFactory.CreateItem("发动机滑油分配-总体", "7920", null, "DISTRIBUTION", false),
                ItemFactory.CreateItem("发动机滑油分配", "7921", null, "DISTRIBUTION", false),
                ItemFactory.CreateItem("回油泵", "7922", null, "PUMP - OIL SCAVENGE", false),
                ItemFactory.CreateItem("发动机滑油指示", "7930", null, "INDICATING", false),
            };

            items.ForEach(service.Add);
            service.UnitOfWork.Commit();
        }
    }
}