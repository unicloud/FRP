#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/21 13:55:03
// 文件名：ConnectionStringCryptography
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/21 13:55:03
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

#endregion

namespace UniCloud.Cryptography
{
    public static class ConnectionStringCryptography
    {
        public static string DecryptConnectionString(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString)) return null;
            //分解连接字符串
            var splitConnection = connectionString.Split(';');
            //获取含加密密码字符串
            var encryptedPwd = splitConnection.FirstOrDefault(p => p.StartsWith("Password="));
            //解密后密码
            var decryptPwd = DESCryptography.DecryptString(encryptedPwd.Substring(encryptedPwd.LastIndexOf('=') + 1));
            //含解密后密码的字符串
            var cryptographyPwd = "Password=" + decryptPwd;
            var connection = splitConnection.Where(p => !p.Contains("Password="));
            //重新组合解密后的连接字符串
            string decryptConnectionString = null;
            if (connection.Count() != 0)
            {
                foreach (var item in connection)
                {
                    decryptConnectionString += item + ";";
                }
            }
            decryptConnectionString += cryptographyPwd;
            return decryptConnectionString;
        }
    }
}
