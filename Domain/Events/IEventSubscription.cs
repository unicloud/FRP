#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:07
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

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     <see cref="T:UniCloud.Domain.Events.EventBase" />的事件订阅契约
    /// </summary>
    public interface IEventSubscription
    {
        /// <summary>
        ///     获取或设置<see cref="T:UniCloud.Domain.Events.SubscriptionToken" />，用以识别
        ///     <see cref="T:UniCloud.Domain.Events.IEventSubscription" />。
        /// </summary>
        /// <value>
        ///     <see cref="T:UniCloud.Domain.Events.IEventSubscription" />的凭据
        /// </value>
        SubscriptionToken SubscriptionToken { get; set; }

        /// <summary>
        ///     获取发布事件的策略
        /// </summary>
        /// <returns>
        ///     <see cref="T:System.Action`1" />执行策略, 如果<see cref="T:UniCloud.Domain.Events.IEventSubscription" />不再有效，则为
        ///     <see langword="null" />。
        /// </returns>
        Action<object[]> GetExecutionStrategy();
    }
}