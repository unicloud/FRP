#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/6 9:42:58
// 文件名：BoolConverter
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/6 9:42:58
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Windows.Data;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        #region "IValueConverter Members"

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string type = value.ToString();
            object result = null;
            switch (type)
            {
                case "所有机型":
                    result = true; break;
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
