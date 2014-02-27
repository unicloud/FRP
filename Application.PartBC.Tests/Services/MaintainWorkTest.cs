#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 22:19:46
// 文件名：MaintainWorkTest
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.MaintainWorkServices;
using UniCloud.Application.PartBC.Query.MaintainWorkQueries;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PartBC.Tests.Services
{
    [TestClass]
    public class MaintainWorkTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 附件相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IMaintainWorkQuery, MaintainWorkQuery>()
                .RegisterType<IMaintainWorkAppService, MaintainWorkAppService>()
                .RegisterType<IMaintainWorkRepository, MaintainWorkRepository>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetMaintainWorks()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IMaintainWorkAppService>();

            // Act
            List<MaintainWorkDTO> result = service.GetMaintainWorks().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddMaintainWorks()
        {
            var context = DefaultContainer.Resolve<IMaintainWorkRepository>();
            //获取

            const string description = "描述信息";
            const string workCode = "Work002394";
            MaintainWork newMaintainWork = MaintainWorkFactory.CreateMaintainWork(description, workCode);
            context.Add(newMaintainWork);
            context.UnitOfWork.Commit();
        }
    }
}