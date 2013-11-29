#region

using System.Text.RegularExpressions;

#endregion

namespace System
{
    /// <summary>
    ///     通用扩展。
    /// </summary>
    public static class CommonExtension
    {
        #region If扩展

        /// <summary>
        ///     If扩展。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="t">对象。</param>
        /// <param name="predicate">判断条件。</param>
        /// <param name="action">操作。</param>
        /// <returns>对象。</returns>
        public static T If<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
        {
            if (t == null) throw new ArgumentNullException();
            if (predicate(t)) action(t);
            return t;
        }

        /// <summary>
        ///     If扩展。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="t">对象。</param>
        /// <param name="predicate">判断条件。</param>
        /// <param name="func">操作。</param>
        /// <returns>对象。</returns>
        public static T If<T>(this T t, Predicate<T> predicate, Func<T, T> func) where T : class
        {
            return predicate(t) ? func(t) : t;
        }

        /// <summary>
        ///     If扩展。
        /// </summary>
        /// <param name="s">string类型。</param>
        /// <param name="predicate">表达式。</param>
        /// <param name="func">操作。</param>
        /// <returns>string类型对象。</returns>
        public static string If(this string s, Predicate<string> predicate, Func<string, string> func)
        {
            return predicate(s) ? func(s) : s;
        }

        #endregion

        #region While 扩展

        /// <summary>
        ///     While扩展。
        /// </summary>
        /// <typeparam name="T">类。</typeparam>
        /// <param name="t">对象。</param>
        /// <param name="predicate">表达式。</param>
        /// <param name="action">操作。</param>
        public static void While<T>(this T t, Predicate<T> predicate, Action<T> action) where T : class
        {
            while (predicate(t)) action(t);
        }

        /// <summary>
        ///     While扩展。
        /// </summary>
        /// <typeparam name="T">类。</typeparam>
        /// <param name="t">对象。</param>
        /// <param name="predicate">表达式。</param>
        /// <param name="actions">操作集合。</param>
        public static void While<T>(this T t, Predicate<T> predicate, params Action<T>[] actions) where T : class
        {
            while (predicate(t))
            {
                foreach (var action in actions)
                    action(t);
            }
        }

        #endregion

        #region String 扩展

        /// <summary>
        ///     string扩展。
        /// </summary>
        /// <param name="s"></param>
        /// <returns>String 是否为空。</returns>
        public static bool IsNullOrEmpty(this string s)
        {
            return string.IsNullOrEmpty(s);
        }

        /// <summary>
        ///     format扩展。
        /// </summary>
        /// <param name="format">string类型。</param>
        /// <param name="args">参数。</param>
        /// <returns>string对象。</returns>
        public static string FormatWith(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        /// <summary>
        ///     正则表达式扩展。
        /// </summary>
        /// <param name="s">string类型。</param>
        /// <param name="pattern">表达式。</param>
        /// <returns>是否匹配。</returns>
        public static bool IsMatch(this string s, string pattern)
        {
            return s != null && Regex.IsMatch(s, pattern);
        }


        /// <summary>
        ///     验证字符串是否由正负号（+-）、数字、小数点构成，并且最多只有一个小数点
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string str)
        {
            var regex = new Regex(@"^[+-]?\d+[.]?\d*$");

            return regex.IsMatch(str);
        }


        /// <summary>
        ///     验证字符串是否仅由[0-9]构成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumericOnly(this string str)
        {
            var regex = new Regex("[0-9]");

            return regex.IsMatch(str);
        }


        /// <summary>
        ///     验证字符串是否由字母和数字构成
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumericOrLetters(this string str)
        {
            var regex = new Regex("[a-zA-Z0-9]");

            return regex.IsMatch(str);
        }


        /// <summary>
        ///     验证是否为空字符串。若无需裁切两端空格，建议直接使用 String.IsNullOrEmpty(string)
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        /// <remarks>
        ///     不同于String.IsNullOrEmpty(string)，此方法会增加一步Trim操作。如 IsNullOrEmptyStr(" ") 将返回 true。
        /// </remarks>
        public static bool IsNullOrEmptyStr(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return true;
            }

            return str.Trim().Length == 0;
        }


        /// <summary>
        ///     裁切字符串（中文按照两个字符计算）
        /// </summary>
        /// <param name="str">旧字符串</param>
        /// <param name="len">新字符串长度</param>
        public static string CutStr(this string str, int len)
        {
            if (string.IsNullOrEmpty(str) || len <= 0)
            {
                return string.Empty;
            }

            var l = str.Length;

            #region 计算长度

            var clen = 0; //当前长度 

            while (clen < len && clen < l)
            {
                //每遇到一个中文，则将目标长度减一。

                if (str[clen] > 128)
                {
                    len--;
                }

                clen++;
            }

            #endregion

            if (clen < l)
            {
                return str.Substring(0, clen) + "...";
            }

            return str;
        }

        /// <summary>
        ///     获取字符串长度。与string.Length不同的是，该方法将中文作 2 个字符计算。
        /// </summary>
        /// <param name="str">目标字符串</param>
        /// <returns></returns>
        public static int GetLength(this string str)
        {
            if (string.IsNullOrEmpty(str))
            {
                return 0;
            }


            var l = str.Length;

            var realLen = l;

            #region 计算长度

            var clen = 0; //当前长度 

            while (clen < l)
            {
                //每遇到一个中文，则将实际长度加一。

                if (str[clen] > 128)
                {
                    realLen++;
                }

                clen++;
            }

            #endregion

            return realLen;
        }


        /// <summary>
        ///     将形如 10.1MB 格式对用户友好的文件大小字符串还原成真实的文件大小，单位为字节。
        /// </summary>
        /// <param name="formatedSize">形如 10.1MB 格式的文件大小字符串</param>
        /// <remarks>
        ///     参见：
        ///     <see>
        ///         <cref>uoLib.Common.Functions.FormatFileSize(long)</cref>
        ///     </see>
        /// </remarks>
        /// <returns></returns>
        public static long GetFileSizeFromString(this string formatedSize)
        {
            if (IsNullOrEmptyStr(formatedSize)) throw new ArgumentNullException("formatedSize");


            long size;

            if (long.TryParse(formatedSize, out size)) return size;


            //去掉数字分隔符

            formatedSize = formatedSize.Replace(",", "");


            var re = new Regex(@"^([\d\.]+)((?:TB|GB|MB|KB|Bytes))$");

            if (re.IsMatch(formatedSize))
            {
                var mc = re.Matches(formatedSize);

                var m = mc[0];

                var s = double.Parse(m.Groups[1].Value);


                switch (m.Groups[2].Value)
                {
                    case "TB":

                        s *= 1099511627776;

                        break;

                    case "GB":

                        s *= 1073741824;

                        break;

                    case "MB":

                        s *= 1048576;

                        break;

                    case "KB":

                        s *= 1024;

                        break;
                }


                size = (long) s;

                return size;
            }


            throw new ArgumentException("formatedSize");
        }

        public static string GetJsSafeStr(this string str)
        {
            if (string.IsNullOrEmpty(str))

                return string.Empty;


            return str.Replace("\\", "\\\\").Replace("\"", "\\\"");
        }

        #endregion

        #region IsNull扩展

        /// <summary>
        ///    IsNull。
        /// </summary>
        /// <typeparam name="T">对象类型。</typeparam>
        /// <param name="t">对象。</param>
        /// <returns>对象。</returns>
        public static bool IsNull<T>(this T t) where T : class
        {
            return t == null;
        }

        #endregion

    }
}