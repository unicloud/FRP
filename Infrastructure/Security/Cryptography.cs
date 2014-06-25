#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：11:52
// 方案：FRP
// 项目：Infrastructure
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace UniCloud.Infrastructure.Security
{
    /// <summary>
    ///     密码算法。
    /// </summary>
    public static class Cryptography
    {
        /// <summary>
        ///     Key
        /// </summary>
        public static string desKey = "qwertyui";

        /// <summary>
        ///     IV
        /// </summary>
        public static string desIV = "qwertyui";

        /// <summary>
        ///     加密数组。
        /// </summary>
        /// <param name="inStreamBuffer">待加密的数组。</param>
        /// <returns>加密后的数组。</returns>
        public static byte[] EncryptByte(byte[] inStreamBuffer)
        {
            var ms = new MemoryStream();
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(desKey),
                IV = Encoding.UTF8.GetBytes(desIV)
            };
            var encStream = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
            encStream.Write(inStreamBuffer, 0, inStreamBuffer.Length);
            encStream.FlushFinalBlock();
            return ms.ToArray();
        }

        /// <summary>
        ///     加密流。
        /// </summary>
        /// <param name="ms">待加密的内存流。</param>
        /// <returns>加密后的内存流。</returns>
        public static Stream EncryptStream(ref MemoryStream ms)
        {
            var buffer = EncryptByte(ms.ToArray());
            if (buffer == null) return ms;
            ms.SetLength(buffer.Length);
            ms.Write(buffer, 0, buffer.Length);
            ms.Position = 0L;
            return ms;
        }

        /// <summary>
        ///     加密字符串。
        /// </summary>
        /// <param name="str">待加密的字符串。</param>
        /// <returns>加密后的字符串。</returns>
        public static string EncryptString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            try
            {
                var bt = EncryptByte(ConvertHelper.Str2AscArr(str));
                return bt != null ? ConvertHelper.HexAscArr2Str(bt) : str;
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        ///     解密数组。
        /// </summary>
        /// <param name="inStreamBuffer">待解密的数组。</param>
        /// <returns>解密后的数组。</returns>
        public static byte[] DecryptByte(byte[] inStreamBuffer)
        {
            var ms = new MemoryStream();
            var des = new DESCryptoServiceProvider
            {
                Key = Encoding.UTF8.GetBytes(desKey),
                IV = Encoding.UTF8.GetBytes(desIV)
            };
            var encStream = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
            encStream.Write(inStreamBuffer, 0, inStreamBuffer.Length);
            encStream.FlushFinalBlock();
            return ms.ToArray();
        }

        /// <summary>
        ///     解密字符串。
        /// </summary>
        /// <param name="str">待解密的字符串。</param>
        /// <returns>解密后的字符串。</returns>
        public static string DecryptString(string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return string.Empty;
            try
            {
                var bt = DecryptByte(ConvertHelper.Str2HexAscArr(str));
                return bt != null ? ConvertHelper.AscArr2Str(bt) : str;
            }
            catch
            {
                return str;
            }
        }

        /// <summary>
        ///     获取数据库连接字符串
        /// </summary>
        /// <param name="conn">连接字符串</param>
        /// <returns>连接字符串</returns>
        public static string GetConnString(string conn)
        {
            var mPassWord = Regex.Match(conn, @"Password=(?<PassWord>[^;]*)", RegexOptions.IgnoreCase);
            var passWord = mPassWord.Groups["PassWord"].Value;
            var realPassWord = "Password=" + DecryptString(passWord);
            var result = Regex.Replace(conn, @"Password=(?<PassWord>[^;]*)", realPassWord);
            return result;
        }
    }
}