#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/24 15:21:29
// 文件名：BasicConfigGroupTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PartBC.BasicConfigGroupServices;
using UniCloud.Application.PartBC.BasicConfigServices;
using UniCloud.Application.PartBC.ConfigGroupServices;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.BasicConfigGroupQueries;
using UniCloud.Application.PartBC.Query.BasicConfigQueries;
using UniCloud.Application.PartBC.Query.ConfigGroupQueries;
using UniCloud.Domain.PartBC.Aggregates;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PartBC.Tests.Services
{
    [TestClass]
    public class BasicConfigGroupTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 基本构型组相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IBasicConfigGroupQuery, BasicConfigGroupQuery>()
                .RegisterType<IBasicConfigGroupAppService, BasicConfigGroupAppService>()
                .RegisterType<IBasicConfigGroupRepository, BasicConfigGroupRepository>()
                .RegisterType<IAircraftTypeRepository, AircraftTypeRepository>()
                .RegisterType<ITechnicalSolutionRepository, TechnicalSolutionRepository>()
                .RegisterType<IBasicConfigQuery, BasicConfigQuery>()
                .RegisterType<IBasicConfigAppService, BasicConfigAppService>()
                .RegisterType<IBasicConfigRepository, BasicConfigRepository>()
                #endregion

                .RegisterType<IConfigGroupQuery, ConfigGroupQuery>()
                .RegisterType<IConfigGroupAppService, ConfigGroupAppService>()
                .RegisterType<IContractAircraftRepository, ContractAircraftRepository>()
                .RegisterType<ISpecialConfigRepository, SpecialConfigRepository>()
                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetBasicConfigGroups()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IConfigGroupAppService>();

            // Act
            List<ConfigGroupDTO> result = service.GetConfigGroups().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddBasicConfigGroups()
        {
            var context = DefaultContainer.Resolve<IBasicConfigGroupRepository>();
            var bcContext = DefaultContainer.Resolve<IBasicConfigRepository>();
            var aircraftTypeContext = DefaultContainer.Resolve<IAircraftTypeRepository>();
            var tsContext = DefaultContainer.Resolve<ITechnicalSolutionRepository>();
            //获取
            AircraftType aircraftType = aircraftTypeContext.GetAll().ToList().First();

            BasicConfigGroup newBasicConfigGroup = BasicConfigGroupFactory.CreateBasicConfigGroup();
            if (newBasicConfigGroup != null)
            {
                newBasicConfigGroup.SetAircraftType(aircraftType);
                newBasicConfigGroup.SetDescription("adfd");
                newBasicConfigGroup.SetGroupNo("Engine0001");
                newBasicConfigGroup.SetStartDate(DateTime.Now);
            }
            context.Add(newBasicConfigGroup); 
            
            TechnicalSolution ts = tsContext.GetAll().ToList().First();
            BasicConfig bc = bcContext.Get(1);
            // 添加基本构型
            BasicConfig newBasicConfig = BasicConfigFactory.CreateBasicConfig();
            if (newBasicConfig != null)
            {
                newBasicConfig.SetDescription("adg");
                newBasicConfig.SetItemNo("asdg");
                newBasicConfig.SetTechnicalSolution(ts);
                newBasicConfig.SetRootId(bc);
            }
            bcContext.Add(newBasicConfig);
            context.UnitOfWork.Commit();
        }
    }
}