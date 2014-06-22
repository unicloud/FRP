#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/19 15:46:48
// 文件名：UserBCTests
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/19 15:46:48
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using UniCloud.Application.BaseManagementBC.UserServices;
using UniCloud.Infrastructure.Utilities.Container;

#endregion

namespace UniCloud.Application.BaseManagementBC.Tests
{
    [TestClass]
    public class UserBCTests
    {
        [TestMethod]
        public void TestGetUsers()
        {
            var service = UniContainer.Resolve<IUserAppService>();
            var result = service.GetUsers().ToList();
            var md5 = new MD5CryptoServiceProvider();
            var inBytes = Encoding.GetEncoding("GB2312").GetBytes("123456a");
            var outBytes = md5.ComputeHash(inBytes);
            var outString = "";
            for (var i = 0; i < outBytes.Length; i++)
            {
                outString += outBytes[i].ToString("x2");
            }

            var first = result.FirstOrDefault(p => p.UserName == "010768" && p.Password == outString);
        }
    }
}