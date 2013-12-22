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

using System.Windows.Data;
using UniCloud.Presentation.Service.Payment.Payment.Enums;

#endregion

namespace UniCloud.Presentation.ValueConverts
{
    /// <summary>
    /// 根据枚举的整型值获取模型批注中的列名
    /// </summary>
    public class EnumToStringConverter : IValueConverter
    {
        #region IValueConverter Members

        public object Convert(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string type = parameter.ToString();
            object result = null;
            switch (type)
            {
                case "InvoiceStatus":
                    result = EnumUtility.GetName(typeof(InvoiceStatus), (InvoiceStatus)value);
                    break;
                case "PaymentNoticeStatus":
                    result = EnumUtility.GetName(typeof(PaymentNoticeStatus), (PaymentNoticeStatus)value);
                    break;
                default:
                    break;
            }
            return result;
        }

        public object ConvertBack(object value, System.Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //return EnumUtility.GetValue(targetType, value.ToString());
            return null;
        }

        #endregion
    }
}
