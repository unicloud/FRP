#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：10:28
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
using System.Windows;
using System.Windows.Data;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            object result = null;
            if (parameter == null)
            {
                result = (bool) value ? Visibility.Visible : Visibility.Collapsed;
            }
            else if (parameter.ToString() == "Inverse")
            {
                result = !(bool) value ? Visibility.Visible : Visibility.Collapsed;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}