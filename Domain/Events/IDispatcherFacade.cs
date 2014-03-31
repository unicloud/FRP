#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/03，21:08
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
    ///     分配外观接口
    /// </summary>
    public interface IDispatcherFacade
    {
        /// <summary>
        ///     分配一个方法调用
        /// </summary>
        /// <param name="method">调用的方法</param>
        /// <param name="arg">调用方法的参数</param>
        void BeginInvoke(Delegate method, object arg);
    }
}