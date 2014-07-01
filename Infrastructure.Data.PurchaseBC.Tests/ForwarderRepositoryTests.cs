#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/12，20:11
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC.Tests
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;
using UniCloud.Infrastructure.Data.PurchaseBC.Repositories;
using UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.Tests
{
    [TestClass]
    public class ForwarderRepositoryTests
    {
        /// <summary>
        ///     删除承运人
        /// </summary>
        [TestMethod]
        public void RemoveForwarder()
        {
            //Arrange
            var service = UniContainer.Resolve<IForwarderRepository>();

            //Act
            var result = service.GetAll().FirstOrDefault();
            service.Remove(result);
            service.UnitOfWork.Commit();

            //Assert
        }
    }
}