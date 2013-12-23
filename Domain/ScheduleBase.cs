#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/23，9:42
// 方案：FRP
// 项目：Domain
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;

#endregion

namespace UniCloud.Domain
{
    /// <summary>
    ///     任务的抽象基类
    /// </summary>
    public abstract class ScheduleBase : EntityInt
    {
        #region 属性

        /// <summary>
        ///     主题
        /// </summary>
        public string Subject { get; protected set; }

        /// <summary>
        ///     内容
        /// </summary>
        public string Body { get; protected set; }

        /// <summary>
        ///     重要性级别
        /// </summary>
        public string Importance { get; protected set; }

        /// <summary>
        ///     进度
        /// </summary>
        public string Tempo { get; protected set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        public DateTime Start { get; protected set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        public DateTime End { get; protected set; }

        /// <summary>
        ///     是否全天事件
        /// </summary>
        public bool IsAllDayEvent { get; protected set; }

        #endregion

        #region 操作

        /// <summary>
        ///     设置任务
        /// </summary>
        /// <param name="subject">主题</param>
        /// <param name="body">内容</param>
        /// <param name="importance">重要性级别</param>
        /// <param name="tempo">进度</param>
        /// <param name="start">开始时间</param>
        /// <param name="end">结束时间</param>
        /// <param name="isAllDay">是否全天事件</param>
        public void SetSchedule(string subject, string body, string importance, string tempo, DateTime start,
            DateTime end, bool isAllDay)
        {
            Subject = subject;
            Body = body;
            Importance = importance;
            Tempo = tempo;
            Start = start;
            End = end;
            IsAllDayEvent = isAllDay;
        }

        #endregion
    }
}