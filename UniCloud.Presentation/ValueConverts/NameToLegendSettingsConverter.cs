#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/27 15:39:03
// 文件名：NameToLegendSettingsConverter
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/27 15:39:03
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Windows.Data;
using Telerik.Windows.Controls.ChartView;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    public class NameToLegendSettingsConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return new SeriesLegendSettings { Title = value.ToString() };
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
