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
    ///     表示对<see cref="T:System.Delegate" />的引用
    /// </summary>
    public interface IDelegateReference
    {
        /// <summary>
        ///     获取对<see cref="T:System.Delegate" />对象的引用
        /// </summary>
        /// <value>
        ///     如果目标有效，为<see cref="T:System.Delegate" />实例; 否则<see langword="null" />
        /// </value>
        Delegate Target { get; }
    }
}