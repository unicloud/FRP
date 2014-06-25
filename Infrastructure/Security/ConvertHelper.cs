#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：15:27
// 方案：FRP
// 项目：Infrastructure
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Text;

#endregion

namespace UniCloud.Infrastructure.Security
{
    /// <summary>
    ///     summary description for strhelper.
    ///     命名缩写：
    ///     str: unicode string
    ///     arr: unicode array
    ///     hex: 二进制数据
    ///     hexbin: 二进制数据用ascii字符表示 例 字符'1'的hex是0x31表示为hexbin是 '3''1'
    ///     asc: ascii
    ///     uni: unicode
    /// </summary>
    public sealed class ConvertHelper
    {
        #region Hex与HexBin的转换

        /// <summary>
        /// </summary>
        /// <param name="bHexBin"></param>
        /// <param name="bHex"></param>
        /// <param name="nLen"></param>
        public static void HexBin2Hex(byte[] bHexBin, byte[] bHex, int nLen)
        {
            for (var i = 0; i < nLen/2; i++)
            {
                if (bHexBin[2*i] < 0x41)
                {
                    bHex[i] = Convert.ToByte(((bHexBin[2*i] - 0x30) << 4) & 0xf0);
                }
                else
                {
                    bHex[i] = Convert.ToByte(((bHexBin[2*i] - 0x37) << 4) & 0xf0);
                }

                if (bHexBin[2*i + 1] < 0x41)
                {
                    bHex[i] |= Convert.ToByte((bHexBin[2*i + 1] - 0x30) & 0x0f);
                }
                else
                {
                    bHex[i] |= Convert.ToByte((bHexBin[2*i + 1] - 0x37) & 0x0f);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="bHexBin"></param>
        /// <param name="nLen"></param>
        /// <returns></returns>
        public static byte[] HexBin2Hex(byte[] bHexBin, int nLen)
        {
            if (nLen%2 != 0)
                return null;
            var bhex = new byte[nLen/2];
            HexBin2Hex(bHexBin, bhex, nLen);
            return bhex;
        }

        /// <summary>
        /// </summary>
        /// <param name="bHex"></param>
        /// <param name="bHexBin"></param>
        /// <param name="nLen"></param>
        public static void Hex2HexBin(byte[] bHex, byte[] bHexBin, int nLen)
        {
            for (var i = 0; i < nLen; i++)
            {
                var c = Convert.ToByte((bHex[i] >> 4) & 0x0f);
                if (c < 0x0a)
                {
                    bHexBin[2*i] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bHexBin[2*i] = Convert.ToByte(c + 0x37);
                }
                c = Convert.ToByte(bHex[i] & 0x0f);
                if (c < 0x0a)
                {
                    bHexBin[2*i + 1] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bHexBin[2*i + 1] = Convert.ToByte(c + 0x37);
                }
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="bHex"></param>
        /// <param name="nLen"></param>
        /// <returns></returns>
        public static byte[] Hex2HexBin(byte[] bHex, int nLen)
        {
            var bhexbin = new byte[nLen*2];
            Hex2HexBin(bHex, bhexbin, nLen);
            return bhexbin;
        }

        #endregion

        #region 数组和字符串之间的转化

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] Str2Arr(string s)
        {
            return (new UnicodeEncoding()).GetBytes(s);
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string Arr2Str(byte[] buffer)
        {
            return (new UnicodeEncoding()).GetString(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] Str2AscArr(string s)
        {
            return Encoding.Convert(Encoding.Unicode,
                Encoding.ASCII,
                Str2Arr(s));
        }

        /// <summary>
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static byte[] Str2HexAscArr(string s)
        {
            var hex = Str2AscArr(s);
            var hexbin = HexBin2Hex(hex, hex.Length);
            return hexbin;
        }

        /// <summary>
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        public static string AscArr2Str(byte[] b)
        {
            return Encoding.Unicode.GetString(
                Encoding.Convert(Encoding.ASCII,
                    Encoding.Unicode,
                    b)
                );
        }

        /// <summary>
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        public static string HexAscArr2Str(byte[] buffer)
        {
            var b = Hex2HexBin(buffer, buffer.Length);
            return AscArr2Str(b);
        }

        #endregion
    }
}