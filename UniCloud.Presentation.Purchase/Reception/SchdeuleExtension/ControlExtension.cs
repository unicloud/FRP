﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/11/28 9:35:44
// 文件名：ControlExtension
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ColorEditor;
using Telerik.Windows.Controls.ScheduleView;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception.SchdeuleExtension
{
    public partial class ControlExtension
    {
        //实现后台ReceptionSchdeule和Appointment之间的装换
        public Appointment ConvertToAppointment(ReceptionScheduleDTO schedule)
        {
            var appointment = new Appointment
            {
                Subject = schedule.Subject,
                Body = schedule.Body,
                End = schedule.End,
                Start = schedule.Start,
                IsAllDayEvent = schedule.IsAllDayEvent,
                TimeMarker = GetTimeMarker(schedule.Importance),
                Category = GetCategory(schedule.Tempo),
                UniqueId = schedule.ReceptionScheduleId.ToString(CultureInfo.InvariantCulture),
            };
            appointment.Resources.Add(GetResource(schedule.Group));
            return appointment;
        }

        public ReceptionScheduleDTO ConvertToReceptionSchedule(Appointment appointment)
        {
            var schedule = new ReceptionScheduleDTO
            {
                Body = appointment.Body,
                Subject = appointment.Subject,
                Start = appointment.Start,
                End = appointment.End
            };
            if (appointment.TimeMarker != null)
                schedule.Importance = appointment.TimeMarker.TimeMarkerName;
            schedule.IsAllDayEvent = appointment.IsAllDayEvent;
            if (appointment.Category != null)
                schedule.Tempo = appointment.Category.CategoryName;
            ResourceCollection ar = appointment.Resources;
            schedule.Group = GetScheduleGroup(ar);//将对应的取过来
            return schedule;
        }

        /// <summary>
        /// 获取进度
        /// </summary>
        /// <param name="categoryName"></param>
        /// <returns></returns>
        private Category GetCategory(string categoryName)
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
        /// 
        /// </summary>
        /// <param name="importance"></param>
        /// <returns></returns>
        private Importance GetImportance(string importance)
        {
            switch (importance)
            {
                case "High":
                    {
                        return Importance.High;
                    }
                case "Low":
                    {
                        return Importance.Low;
                    }
                default:
                    return Importance.None;
            }
        }

        /// <summary>
        /// 【日程类型】相关的标记类型
        /// </summary>
        /// <param name="timeMarkerName"></param>
        /// <returns></returns>
        private TimeMarker GetTimeMarker(string timeMarkerName)
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

        Resource GetResource(string groupName)
        {
            switch (groupName)
            {
                case "机队管理组":
                    {
                        var resource = new Resource("机队管理组", "工作组");
                        return resource;
                    }
                case "机务组":
                    {
                        var resource = new Resource("机务组", "工作组");
                        return resource;
                    }
                case "后勤组":
                    {
                        var resource = new Resource("后勤组", "工作组");
                        return resource;
                    }
                case "其他":
                    {
                        var resource = new Resource("其他", "工作组");
                        return resource;
                    }
                default:
                    return null;
            }
        }

        private string GetScheduleGroup(ResourceCollection ar)
        {
            var r1 = new Resource("机队管理组", "工作组");
            var r2 = new Resource("机务组", "工作组");
            var r3 = new Resource("后勤组", "工作组");
            var r4 = new Resource("其他", "工作组");
            if (ar.Contains(r1))
                return r1.DisplayName;
            else if (ar.Contains(r2))
                return r2.DisplayName;
            else if (ar.Contains(r3))
                return r3.DisplayName;
            else if (ar.Contains(r4))
                return r4.DisplayName;
            else return "其他";
        }

    }
}