#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:04
// 方案：FRP
// 项目：Infrastructure.Data.BaseManagementBC.Tests
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Security.Cryptography;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Domain.BaseManagementBC.Aggregates.UserAgg;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.Tests
{
    [TestClass]
    public class AuthTests
    {
        [TestMethod]
        public void CreateUserTest()
        {
            // Arrange
            var usRep = UniContainer.Resolve<IUserRepository>();
            const string password = "1";
            const string validationKey = "C50B3C89CB21F4F1422FF158A5B42D0E8DB8CB5CDA1742572A487";
            var hash = new HMACSHA1 {Key = HexToByte(validationKey + "C50B3C89")};
            var encodedPassword = Convert.ToBase64String(hash.ComputeHash(Encoding.Unicode.GetBytes(password)));
            var user = UserFactory.CreateUser("admin1", encodedPassword, null, 1, "First User", "3U", DateTime.Now);

            // Act
            usRep.Add(user);
            usRep.UnitOfWork.Commit();

            // Assert
            Assert.IsTrue(true);
        }


        /// <summary>
        ///     十六进制转换为字节数组。
        ///     用于转换加密的key。
        /// </summary>
        /// <param name="hexString">十六进制字符串。</param>
        /// <returns>字节数组</returns>
        private static byte[] HexToByte(string hexString)
        {
            var returnBytes = new byte[hexString.Length/2];
            for (var i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i*2, 2), 16);
            return returnBytes;
        }
    }
}