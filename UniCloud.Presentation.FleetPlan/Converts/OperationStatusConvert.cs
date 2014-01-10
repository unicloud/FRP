#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/07，14:01
// 文件名：ApprovalStatusConvert.cs
// 程序集：UniCloud.Presentation.FleetPlan
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
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;
using UniCloud.Presentation.ValueConverts;

#endregion

namespace UniCloud.Presentation.FleetPlan.Converts
{
    public class OperationStatusConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
                throw new Exception("值不能为空");
            return EnumUtility.GetName(typeof (OperationStatus), (OperationStatus) value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}