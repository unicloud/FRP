#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/11/12 13:48:18
// 文件名：PropertiesHelper
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region

using System;
using System.Linq;
using System.Reflection;

#endregion

namespace UniCloud.Application
{
    public class PropertiesHelper
    {
        /// <summary>
        ///     把src属性值赋值给dst
        /// </summary>
        /// <param name="src"></param>
        /// <param name="dst"></param>
        public static void UpdateProperty(object src, object dst)
        {
            if (src == null || dst == null)
                throw new ArgumentNullException("src");

            var srcProperties = src.GetType()
                                   .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            var dstProperties = src.GetType()
                                   .GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.GetProperty);

            foreach (var dstP in dstProperties)
            {
                if (dstP.PropertyType.Attributes.ToString().Contains("ClassSemanticsMask")) continue;
                var srcP = srcProperties.FirstOrDefault(p => p.Name.Equals(dstP.Name));
                if (srcP == null) continue;
                dstP.SetValue(dst, srcP.GetValue(src));
            }
        }
    }
}