#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/04，12:03
// 方案：FRP
// 项目：Presentation
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Globalization;
using System.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    public class EnumToDescriptionConverter : IValueConverter
    {
        #region IValueConverter 成员

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = parameter.ToString();
            object result;
            switch (type)
            {
                case "OilMonitorStatus":
                    result = ((OilMonitorStatus) value).GetDescription();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
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