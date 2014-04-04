#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/25，16:50
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Globalization;
using System.Windows.Data;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    public class DayAxisLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date;
            if (value is DateTime)
                date = (DateTime) value;
            else
                date = DateTime.Parse((string) value);
            if (date.Month == 1 && date.Day == 1)
                return String.Format("{0:MM/dd}" + Environment.NewLine + "{0:yyyy}", date);
            return String.Format("{0:MM/dd}" + Environment.NewLine, date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    public class MonthAxisLabelConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DateTime date;
            if (value is DateTime)
                date = (DateTime) value;
            else
                date = DateTime.Parse((string) value);
            if (date.Month == 1 && date.Day == 1)
                return String.Format("{0:MM}" + Environment.NewLine + "{0:yyyy}", date);
            return String.Format("{0:MM}" + Environment.NewLine, date);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}