#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/21 10:50:58
// 文件名：DocumentSearchBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/21 10:50:58
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using Microsoft.Practices.Unity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.CommonServiceBC.DocumnetSearch;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.CommonServiceBC.Tests
{
    [TestClass]
    public class DocumentSearchBCTests
    {
        #region 基础配置

        [TestInitialize]
        public void TestInitialize()
        {
            DefaultContainer.CreateContainer()
                         .RegisterType<IQueryableUnitOfWork, CommonServiceBCUnitOfWork>(new WcfPerRequestLifetimeManager())
            #region 文档相关配置，包括查询，应用服务，仓储注册

.RegisterType<IDocumentSearchAppService, DocumentSearchAppService>()
            #endregion

;
        }

        [TestCleanup]
        public void TestCleanup()
        {
            
        }

        [TestMethod]
        public void TestSearch()
        {
            var service = DefaultContainer.Resolve<IDocumentSearchAppService>();
            var result = service.Search("item");
        }

        #endregion
    }
}
