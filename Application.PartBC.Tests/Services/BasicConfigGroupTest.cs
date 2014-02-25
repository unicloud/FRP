﻿#region 版本信息
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
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PartBC.BasicConfigGroupServices;
using UniCloud.Application.PartBC.Query.BasicConfigGroupQueries;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
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
            #endregion

;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        [TestMethod]
        public void GetBasicConfigGroups()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IBasicConfigGroupAppService>();

            // Act
            var result = service.GetBasicConfigGroups().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddBasicConfigGroups()
        {
            var context = DefaultContainer.Resolve<IBasicConfigGroupRepository>();
            var aircraftTypeContext = DefaultContainer.Resolve<IAircraftTypeRepository>();
            var tsContext = DefaultContainer.Resolve<ITechnicalSolutionRepository>();
            //获取
            var aircraftType = aircraftTypeContext.GetAll().ToList().First();

            var newBasicConfigGroup = BasicConfigGroupFactory.CreateBasicConfigGroup();
            if (newBasicConfigGroup != null)
            {
                newBasicConfigGroup.SetAircraftType(aircraftType);
                newBasicConfigGroup.SetDescription("adfd");
                newBasicConfigGroup.SetGroupNo("Engine0001");
                newBasicConfigGroup.SetStartDate(DateTime.Now);

                var ts = tsContext.GetAll().ToList().First();
                // 添加基本构型
                var newBasicConfig = newBasicConfigGroup.AddNewBasicConfig();
                newBasicConfig.SetDescription("adg");
                newBasicConfig.SetItemNo("asdg");
                newBasicConfig.SetTechnicalSolution(ts);
            }
            context.Add(newBasicConfigGroup);
            context.UnitOfWork.Commit();
        }
        #endregion
    }
}
