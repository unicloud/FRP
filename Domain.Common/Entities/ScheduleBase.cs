#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/31，10:12
// 方案：FRP
// 项目：Domain.Common
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.Common.Entities
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
        public string Subject { get; private set; }

        /// <summary>
        ///     内容
        /// </summary>
        public string Body { get; private set; }

        /// <summary>
        ///     重要性级别
        /// </summary>
        public string Importance { get; private set; }

        /// <summary>
        ///     进度
        /// </summary>
        public string Tempo { get; private set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        public DateTime Start { get; private set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        public DateTime End { get; private set; }

        /// <summary>
        ///     是否全天事件
        /// </summary>
        public bool IsAllDayEvent { get; private set; }

        /// <summary>
        ///     是否完成
        /// </summary>
        public bool IsCompleted { get; private set; }

        /// <summary>
        ///     任务状态
        /// </summary>
        public TaskStatus Status { get; private set; }

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

        /// <summary>
        ///     设置任务状态
        /// </summary>
        /// <param name="status">任务状态</param>
        public void SetTaskStatus(TaskStatus status)
        {
            switch (status)
            {
                case TaskStatus.未开始:
                    Status = TaskStatus.未开始;
                    break;
                case TaskStatus.进行中:
                    Status = TaskStatus.进行中;
                    break;
                case TaskStatus.已完成:
                    Status = TaskStatus.已完成;
                    IsCompleted = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        #endregion
    }
}