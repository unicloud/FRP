#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/26 21:44:19
// 文件名：PnRegTest
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
using UniCloud.Application.PartBC.PnRegServices;
using UniCloud.Application.PartBC.Query.PnRegQueries;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.PartBC.Repositories;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.PartBC.Tests.Services
{
    [TestClass]
    public class PnRegTest
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                .RegisterType<IQueryableUnitOfWork, PartBCUnitOfWork>(new WcfPerRequestLifetimeManager())

                #region 附件相关配置，包括查询，应用服务，仓储注册

                .RegisterType<IPnRegQuery, PnRegQuery>()
                .RegisterType<IPnRegAppService, PnRegAppService>()
                .RegisterType<IPnRegRepository, PnRegRepository>()
                #endregion

                ;
        }

        [TestCleanup]
        public void TestCleanup()
        {
        }

        #endregion

        [TestMethod]
        public void GetPnRegs()
        {
            // Arrange
            var service = DefaultContainer.Resolve<IPnRegAppService>();

            // Act
            List<PnRegDTO> result = service.GetPnRegs().ToList();

            // Assert
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void AddPnRegs()
        {
            var context = DefaultContainer.Resolve<IPnRegRepository>();
            //获取

            const string pn = "EN33423423";
            PnReg newPnReg = PnRegFactory.CreatePnReg(true, pn);
            context.Add(newPnReg);
            context.UnitOfWork.Commit();
        }
    }
}