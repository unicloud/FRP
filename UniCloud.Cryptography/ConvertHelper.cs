/*
* @author wuerping
* @version 1.0
* @date 2004/11/30
* @description: 
*/
using System;
using System.Text;

namespace UniCloud.ConvertHelper
{
    /// <summary>
    /// summary description for strhelper.
    /// 命名缩写：
    /// str: unicode string
    /// arr: unicode array
    /// hex: 二进制数据
    /// hexbin: 二进制数据用ascii字符表示 例 字符'1'的hex是0x31表示为hexbin是 '3''1' 
    /// asc: ascii
    /// uni: unicode
    /// </summary>
    public sealed class ConvertHelper
    {
        #region Hex与HexBin的转换

        public static void HexBin2Hex(byte[] bHexBin, byte[] bHex, int nLen)
        {
            for (int i = 0; i < nLen / 2; i++)
            {
                if (bHexBin[2 * i] < 0x41)
                {
                    bHex[i] = Convert.ToByte(((bHexBin[2 * i] - 0x30) << 4) & 0xf0);
                }
                else
                {
                    bHex[i] = Convert.ToByte(((bHexBin[2 * i] - 0x37) << 4) & 0xf0);
                }

                if (bHexBin[2 * i + 1] < 0x41)
                {
                    bHex[i] |= Convert.ToByte((bHexBin[2 * i + 1] - 0x30) & 0x0f);
                }
                else
                {
                    bHex[i] |= Convert.ToByte((bHexBin[2 * i + 1] - 0x37) & 0x0f);
                }
            }
        }

        public static byte[] HexBin2Hex(byte[] bHexBin, int nLen)
        {
            if (nLen % 2 != 0)
                return null;
            byte[] bhex = new byte[nLen / 2];
            HexBin2Hex(bHexBin, bhex, nLen);
            return bhex;
        }

        public static void Hex2HexBin(byte[] bHex, byte[] bHexBin, int nLen)
        {
            byte c;
            for (int i = 0; i < nLen; i++)
            {
                c = Convert.ToByte((bHex[i] >> 4) & 0x0f);
                if (c < 0x0a)
                {
                    bHexBin[2 * i] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bHexBin[2 * i] = Convert.ToByte(c + 0x37);
                }
                c = Convert.ToByte(bHex[i] & 0x0f);
                if (c < 0x0a)
                {
                    bHexBin[2 * i + 1] = Convert.ToByte(c + 0x30);
                }
                else
                {
                    bHexBin[2 * i + 1] = Convert.ToByte(c + 0x37);
                }
            }
        }

        public static byte[] Hex2HexBin(byte[] bHex, int nLen)
        {
            byte[] bhexbin = new byte[nLen * 2];
            Hex2HexBin(bHex, bhexbin, nLen);
            return bhexbin;
        }

        #endregion

        #region 数组和字符串之间的转化

        public static byte[] Str2Arr(string s)
        {
            return (new UnicodeEncoding()).GetBytes(s);
        }

        public static string Arr2Str(byte[] buffer)
        {
            return (new UnicodeEncoding()).GetString(buffer, 0, buffer.Length);
        }

        public static byte[] Str2AscArr(string s)
        {
            return System.Text.UnicodeEncoding.Convert(System.Text.Encoding.Unicode,
            System.Text.Encoding.ASCII,
            Str2Arr(s));
        }

        public static byte[] Str2HexAscArr(string s)
        {
            byte[] hex = Str2AscArr(s);
            byte[] hexbin = HexBin2Hex(hex, hex.Length);
            return hexbin;
        }
   
        public static string AscArr2Str(byte[] b)
        {
            return System.Text.UnicodeEncoding.Unicode.GetString(
            System.Text.ASCIIEncoding.Convert(System.Text.Encoding.ASCII,
            System.Text.Encoding.Unicode,
            b)
            );
        }

        public static string HexAscArr2Str(byte[] buffer)
        {
            byte[] b = Hex2HexBin(buffer, buffer.Length);
            return AscArr2Str(b);
        }

        #endregion
    }
}
