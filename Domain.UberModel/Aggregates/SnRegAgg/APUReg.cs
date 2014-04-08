#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/22，18:29
// 方案：FRP
// 项目：Domain.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.UberModel.Aggregates.SnRegAgg
{
    /// <summary>
    ///     滑油用户聚合根
    ///     APU滑油
    /// </summary>
    public class APUReg : SnReg
    {
        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal APUReg()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     是否需要监控
        /// </summary>
        public bool NeedMonitor { get; internal set; }

        /// <summary>
        ///     监控状态
        /// </summary>
        public OilMonitorStatus MonitorStatus { get; private set; }

        #endregion

        #region 外键属性

        #endregion

        #region 导航属性

        #endregion

        #region 操作

        /// <summary>
        ///     设置滑油监控状态
        /// </summary>
        /// <param name="status">滑油监控状态</param>
        public void SetMonitorStatus(OilMonitorStatus status)
        {
            switch (status)
            {
                case OilMonitorStatus.正常:
                    MonitorStatus = OilMonitorStatus.正常;
                    break;
                case OilMonitorStatus.关注:
                    MonitorStatus = OilMonitorStatus.关注;
                    break;
                case OilMonitorStatus.警告:
                    MonitorStatus = OilMonitorStatus.警告;
                    break;
                default:
                    throw new ArgumentOutOfRangeException("status");
            }
        }

        #endregion
    }
}