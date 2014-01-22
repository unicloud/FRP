#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/22 16:13:57
// 文件名：StringToXamlConverter
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/22 16:13:57
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Globalization;
using System.IO;
using System.Windows.Data;
using System.Windows.Markup;
using System.Xml;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    /// <summary>
    /// Converts a string containing valid XAML into WPF objects.
    /// </summary>
    public  class StringToXamlConverter : IValueConverter
    {
        /// <summary>
        /// Converts a string containing valid XAML into WPF objects.
        /// </summary>
        /// <param name="value">The string to convert.</param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>A WPF object.</returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var input = value as string;
            if (input != null)
            {
                string escapedXml = input;//SecurityElement.Escape(input);
                string withTags = escapedXml.Replace("|~S~|", "<Run Foreground=\"Red\">");
                withTags = withTags.Replace("|~E~|", "</Run>");

                string wrappedInput = string.Format("<TextBlock xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" TextWrapping=\"Wrap\">{0}</TextBlock>", withTags);

                using (var stringReader = new StringReader(wrappedInput))
                {
                    using (XmlReader xmlReader = XmlReader.Create(stringReader))
                    {

                        return XamlReader.Load(wrappedInput);
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Converts WPF framework objects into a XAML string.
        /// </summary>
        /// <param name="value">The WPF Famework object to convert.</param>
        /// <param name="targetType">This parameter is not used.</param>
        /// <param name="parameter">This parameter is not used.</param>
        /// <param name="culture">This parameter is not used.</param>
        /// <returns>A string containg XAML.</returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("This converter cannot be used in two-way binding.");
        }
    }
}
