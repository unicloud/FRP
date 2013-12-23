#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/21，15:12
// 文件名：AppointmentConvertHelper.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ColorEditor;
using Telerik.Windows.Controls.ScheduleView;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules.PaymentScheduleExtension
{
    /// <summary>
    ///     Appointment点的转化
    /// </summary>
    public static class AppointmentConvertHelper
    {
        //实现Appointment转化Appointment
        public static PaymentAppointment ConvertToAppointPayment(PaymentScheduleLineDTO schedule)
        {
            var appointment = new PaymentAppointment
            {
                Subject = schedule.Subject,
                Body = schedule.Body,
                End = schedule.ScheduleDate,
                Start = schedule.ScheduleDate,
                Amount = schedule.Amount,
                IsAllDayEvent = true,
                UniqueId = Guid.NewGuid().ToString(),
                TimeMarker = GetTimeMarker(schedule.Importance),
                Category = GetCategory(schedule.ProcessStatus)
            };
            return appointment;
        }

        /// <summary>
        ///     把Appointment转化PaymentScheduleLineDTO
        /// </summary>
        /// <param name="appointment"></param>
        /// <returns></returns>
        public static PaymentScheduleLineDTO ConvertToPaymentScheduleLine(PaymentAppointment appointment)
        {
            var schedule = new PaymentScheduleLineDTO
            {
                Body = appointment.Body,
                Subject = appointment.Subject,
                ScheduleDate = appointment.Start,
                Amount = appointment.Amount
            };
            if (appointment.TimeMarker != null)
                schedule.Importance = appointment.TimeMarker.TimeMarkerName;
            if (appointment.Category != null)
                schedule.ProcessStatus = appointment.Category.CategoryName;
            if (appointment.RecurrenceRule!=null)
            {
                appointment.RecurrenceRule.Pattern.GetOccurrences(appointment.Start);
            }
            return schedule;
        }

        /// <summary>
        /// 获取循环的付款计划Appointment
        /// </summary>
        /// <returns></returns>
        public static List<PaymentAppointment> GetOccurrences(PaymentAppointment appointment, IEnumerable<DateTime> occurrenceDateTimes)
        {
            var occurrencePaymentAppointment = new List<PaymentAppointment>();
            occurrenceDateTimes.ToList().ForEach(p =>
            {
              
                var subPaymentAppointment = new PaymentAppointment
                {
                    Subject = appointment.Subject,
                    Body = appointment.Body,
                    End = p,
                    Start = p,
                    Amount = appointment.Amount,
                    IsAllDayEvent = true,
                    UniqueId = Guid.NewGuid().ToString(),
                    TimeMarker = appointment.TimeMarker,
                    Category = appointment.Category,
                };
                occurrencePaymentAppointment.Add(subPaymentAppointment);
            });
            return occurrencePaymentAppointment;
        }

        /// <summary>
        ///     获取进度
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        private static Category GetCategory(string categoryName)
        {
            switch (categoryName)
            {
                case "已完成":
                {
                    var category = new Category("已完成", new SolidColorBrush(Colors.Green));
                    return category;
                }
                case "正在进行中…":
                {
                    var category = new Category("正在进行中…", new SolidColorBrush(Colors.Brown));
                    return category;
                }
                case "未启动":
                {
                    var category = new Category("未启动", new SolidColorBrush(Colors.Gray));
                    return category;
                }
                default:
                    return null;
            }
        }

        /// <summary>
        ///     【日程类型】相关的标记类型
        /// </summary>
        /// <param name="timeMarkerName"></param>
        /// <returns></returns>
        private static TimeMarker GetTimeMarker(string timeMarkerName)
        {
            switch (timeMarkerName)
            {
                case "Free":
                {
                    var timeMarker = new TimeMarker("Free",
                        new SolidColorBrush(ColorConverter.ColorFromString("#FF309B46")));
                    return timeMarker;
                }
                case "Tentative":
                {
                    var timeMarker = new TimeMarker("Tentative",
                        new SolidColorBrush(ColorConverter.ColorFromString("#FF41229B")));
                    return timeMarker;
                }
                case "Busy":
                {
                    var timeMarker = new TimeMarker("Busy",
                        new SolidColorBrush(ColorConverter.ColorFromString("#FFE61E26")));
                    return timeMarker;
                }
                case "OutOfOffice":
                {
                    var timeMarker = new TimeMarker("OutOfOffice",
                        new SolidColorBrush(ColorConverter.ColorFromString("#FFF1C700")));
                    return timeMarker;
                }
                default:
                    return null;
            }
        }

        /// <summary>
        /// 获取任务状态集合
        /// </summary>
        /// <returns></returns>
        public static CategoryCollection GetCategoryCollection()
        {
            return new CategoryCollection
            {
                new Category("未启动", new SolidColorBrush(Colors.Gray)),
                new Category("正在进行中…", new SolidColorBrush(Colors.Brown)),
                new Category("已完成", new SolidColorBrush(Colors.Green)),
            };
        }
    }
}