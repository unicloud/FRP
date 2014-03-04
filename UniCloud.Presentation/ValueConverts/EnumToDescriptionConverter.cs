#region �汾��Ϣ

// ========================================================================
// ��Ȩ���� (C) 2014 UniCloud 
//�����๦�ܸ�����
// 
// ���ߣ���־�� ʱ�䣺2014/03/04��12:03
// ������FRP
// ��Ŀ��Presentation
// �汾��V1.0.0
//
// �޸��ߣ� ʱ�䣺 
// �޸�˵����
// ========================================================================

#endregion

#region �����ռ�

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
        #region IValueConverter ��Ա

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