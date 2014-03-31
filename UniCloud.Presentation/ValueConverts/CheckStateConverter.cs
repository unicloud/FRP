#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/11 14:58:35
// 文件名：CheckStateConverter
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/11 14:58:35
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Windows.Automation;
using System.Windows.Data;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    public class CheckStateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var isChecked = (bool)value;

            if (isChecked)
            {
                return ToggleState.On;
            }
            else
            {
                return ToggleState.Off;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var checkState = (ToggleState)value;

            if (checkState == ToggleState.On)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
