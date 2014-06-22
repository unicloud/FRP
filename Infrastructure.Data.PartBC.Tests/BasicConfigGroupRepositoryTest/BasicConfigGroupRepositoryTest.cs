#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/4 10:51:55
// 文件名：BasicConfigGroupRepositoryTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.Tests.BasicConfigGroupRepositoryTest
{
    [TestClass]
    public class BasicConfigGroupRepositoryTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            UniContainer.Create()
                .Register<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())
                .Register<IModelConfiguration, SqlConfigurations>("Sql")
                .Register<IBasicConfigGroupRepository, BasicConfigGroupRepository>()
                .Register<IBasicConfigRepository, BasicConfigRepository>()
                .Register<IAircraftTypeRepository, AircraftTypeRepository>();
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void CreateBasicConfigGroupTest()
        {
            // Arrange
            //var bcGroupRep = UniContainer.Resolve<IBasicConfigGroupRepository>();
            //var bcRep = UniContainer.Resolve<IBasicConfigRepository>();
            //var tsRep = UniContainer.Resolve<ITechnicalSolutionRepository>();
            //var acTypeRep = UniContainer.Resolve<IAircraftTypeRepository>();
            //var ts = tsRep.GetAll().FirstOrDefault();
            //var acType = acTypeRep.GetAll().FirstOrDefault();
            //// Act
            //var bcGroup = BasicConfigGroupFactory.CreateBasicConfigGroup(acType, "基本构型组0001", "EngineBc0001", new DateTime(2014, 1, 1));
            //var bcGroup2 = BasicConfigGroupFactory.CreateBasicConfigGroup(acType, "基本构型组0003", "EngineBc0003", new DateTime(2014, 1, 1));
            //bcGroupRep.Add(bcGroup2);
            //var bc = BasicConfigFactory.CreateBasicConfig("", "2345", null, null, ts,bcGroup.Id);
            //bcGroup.BasicConfigs.Add(bc);
            //bcGroupRep.Add(bcGroup);
            //bcGroupRep.UnitOfWork.Commit();
            //bcRep.UnitOfWork.Commit();
        }
    }
}