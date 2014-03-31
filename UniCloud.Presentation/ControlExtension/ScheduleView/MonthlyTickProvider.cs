#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/23，10:12
// 文件名：MonthlyTickProvider.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;
using Telerik.Windows.Controls.ScheduleView;

namespace UniCloud.Presentation.ControlExtension.ScheduleView
{
    public class MonthlyTickProvider : ITickProvider
    {
        public string GetFormatString(IFormatProvider formatInfo, string formatString, DateTime currentStart)
        {
            return string.Format(formatInfo, "{0:MMMM}", currentStart);
        }

        public DateTime GetNextStart(TimeSpan pixelLength, DateTime currentStart)
        {
            var currentDate = currentStart.Date;

            var monthStart = CalendarHelper.GetStartOfMonth(currentStart.Year, currentStart.Month);
            if (monthStart == currentDate)
            {
                return monthStart.AddMonths(1);
            }
            return monthStart;
        }
    }
}
