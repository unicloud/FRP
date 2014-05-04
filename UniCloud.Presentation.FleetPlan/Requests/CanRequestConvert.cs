#region 命名空间

using System;
using System.Globalization;
using System.Windows.Data;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    public class CanRequestConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new Exception("值不能为空");
            return Enum.GetName(typeof(CanRequest), (CanRequest)value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}