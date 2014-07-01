#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/01，21:40
// 方案：FRP
// 项目：Infrastructure.Data.PartBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PartBC.Aggregates.ThrustAgg;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests
{
    [TestClass]
    public class ThrustRepositoryTests
    {
        [TestMethod]
        public void CreateThrustTest()
        {
            // Arrange
            var service = UniContainer.Resolve<IThrustRepository>();

            // Act
            var thrusts = new List<Thrust>
            {
                ThrustFactory.CreateThrust("24K", "推力24K"),
                ThrustFactory.CreateThrust("25K", "推力25K"),
                ThrustFactory.CreateThrust("27K", "推力27K"),
            };
            thrusts.ForEach(service.Add);
            service.UnitOfWork.Commit();
        }
    }
}