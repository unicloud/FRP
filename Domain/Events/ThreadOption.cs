#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:10
// 方案：FRP
// 项目：Domain
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

namespace UniCloud.Domain.Events
{
    /// <summary>
    ///     <see cref="T:UniCloud.Domain.Events.CompositeEvent`1" />订阅的线程类型。
    /// </summary>
    public enum ThreadOption
    {
        PublisherThread,
        BackgroundThread,
    }
}