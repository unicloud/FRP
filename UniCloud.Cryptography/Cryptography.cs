using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using UniCloud.ConvertHelper;

namespace UniCloud.Cryptography
{

    /// <summary>
    /// 加密、解密类
    /// </summary>
    public static class DESCryptography
    {

        #region DESCryptoServiceProvider 加密、解密
        public static string UnionKey = "qwertyuiop";

        // 创建Key
        public static string GenerateKey()
        {
            DESCryptoServiceProvider key = new DESCryptoServiceProvider();
            DESCryptoServiceProvider desCrypto = (DESCryptoServiceProvider)DESCryptoServiceProvider.Create();
            return ASCIIEncoding.ASCII.GetString(desCrypto.Key);
        }

        // 加密
        public static byte[] EncryptByte(byte[] inStreamBuffer)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.UTF8.GetBytes(UnionKey.Substring(0, 8));
            DES.IV = Encoding.UTF8.GetBytes(UnionKey.Substring(0, 8));
            ICryptoTransform desencrypt = DES.CreateEncryptor();
            byte[] result = desencrypt.TransformFinalBlock(inStreamBuffer, 0, inStreamBuffer.Length);
            return result;
        }

        // 加密
        public static Stream EncryptStream(ref MemoryStream sm)
        {

            byte[] bt = EncryptByte(sm.ToArray());
            if (bt != null)
            {
                sm.SetLength(bt.Length);
                sm.Write(bt, 0, bt.Length);
                sm.Position = 0;
            }
            return sm;
        }

        // 加密
        public static string EncryptString(string str)
        {
            if (str == null || str.Trim() == "")
            {
                return "";
            }
            try
            {
                byte[] bt = EncryptByte(ConvertHelper.ConvertHelper.Str2AscArr(str));
                if (bt != null)
                {
                    return ConvertHelper.ConvertHelper.HexAscArr2Str(bt);
                }
                else
                {
                    return str;
                }
            }
            catch
            {
                return str;
            }
        }

        // 解密
        public static byte[] DecryptByte(byte[] inStreamBuffer)
        {
            DESCryptoServiceProvider DES = new DESCryptoServiceProvider();
            DES.Key = Encoding.UTF8.GetBytes(UnionKey.Substring(0, 8));
            DES.IV = Encoding.UTF8.GetBytes(UnionKey.Substring(0, 8));
            ICryptoTransform desencrypt = DES.CreateDecryptor();
            byte[] result = desencrypt.TransformFinalBlock(inStreamBuffer, 0, inStreamBuffer.Length);
            return result;
        }

        // 解密
        public static string DecryptString(string str)
        {
            if (str == null || str.Trim() == "")
            {
                return "";
            }
            try
            {
                byte[] bt = DecryptByte(ConvertHelper.ConvertHelper.Str2HexAscArr(str));
                if (bt != null)
                {
                    return ConvertHelper.ConvertHelper.AscArr2Str(bt);
                }
                else
                {
                    return str;
                }
            }
            catch
            {
                return str;
            }
        }

        #endregion

    }


    public class RijndaelCryptography
    {

        #region RijndaelManaged 加密、解密
        public static byte[] key = { 24, 55, 102, 24, 98, 26, 67, 29, 84, 19, 37, 118, 104, 85, 121, 27, 93, 86, 24, 55, 102, 24, 98, 26, 67, 29, 9, 2, 49, 69, 73, 92 };
        public static byte[] IV = { 22, 56, 82, 77, 84, 31, 74, 24, 55, 102, 24, 98, 26, 67, 29, 99 };


        /// <summary>
        /// 加密
        /// </summary>
        public static byte[] CryToByte(byte[] inStreamBuffer)
        {

            try
            {
                RijndaelManaged myRijndael = new RijndaelManaged();
                myRijndael.Key = key;
                myRijndael.IV = IV;
                ICryptoTransform desencrypt = myRijndael.CreateEncryptor();
                byte[] result = desencrypt.TransformFinalBlock(inStreamBuffer, 0, inStreamBuffer.Length);
                return result;


            }
            catch (Exception ex)
            {

            }
            return null;
        }

        /// <summary>
        /// 解密
        /// </summary>
        public static byte[] DeCryToByte(byte[] inStreamBuffer)
        {

            try
            {
                RijndaelManaged myRijndael = new RijndaelManaged();
                myRijndael.Key = key;
                myRijndael.IV = IV;
                ICryptoTransform desencrypt = myRijndael.CreateDecryptor();
                byte[] result = desencrypt.TransformFinalBlock(inStreamBuffer, 0, inStreamBuffer.Length);
                return result;

            }
            catch (Exception ex)
            {

            }
            return null;
        }

        #endregion
    }
}
