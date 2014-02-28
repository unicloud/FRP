#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/17 10:03:55
// 文件名：StringConverter
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/17 10:03:55
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Globalization;
using System.Windows.Data;
using UniCloud.Presentation.Service.AircraftConfig.AircraftConfig.Enum;
using UniCloud.Presentation.Service.Part.Part.Enums;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    /// <summary>
    ///     根据枚举的整型值获取模型批注中的列名
    /// </summary>
    public class EnumToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var type = parameter.ToString();
            object result;
            switch (type)
            {
                case "InvoiceStatus":
                    result = Enum.GetName(typeof (InvoiceStatus), (InvoiceStatus) value);
                    break;
                case "PaymentNoticeStatus":
                    result = Enum.GetName(typeof (PaymentNoticeStatus), (PaymentNoticeStatus) value);
                    break;
                case "GuaranteeStatus":
                    result = Enum.GetName(typeof (GuaranteeStatus), (GuaranteeStatus) value);
                    break;
                case "LicenseStatus":
                    result = Enum.GetName(typeof (LicenseStatus), (LicenseStatus) value);
                    break;
                case "InvoiceType":
                    result = Enum.GetName(typeof (InvoiceType), (InvoiceType) value);
                    break;
                case "ScnStatus":
                    result = Enum.GetName(typeof (ScnStatus), (ScnStatus) value);
                    break;
                case "OilMonitorStatus":
                    result = Enum.GetName(typeof (OilMonitorStatus), (OilMonitorStatus) value);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            return result;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //return EnumUtility.GetValue(targetType, value.ToString());
            return null;
        }

        #endregion
    }
}