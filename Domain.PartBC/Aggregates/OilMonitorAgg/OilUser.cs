﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/27，13:55
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
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg
{
    /// <summary>
    ///     滑油监控聚合根
    ///     滑油用户
    /// </summary>
    public abstract class OilUser : EntityInt, IValidatableObject
    {
        #region 私有字段

        private HashSet<OilMonitor> _oilMonitors;

        #endregion

        #region 构造函数

        /// <summary>
        ///     内部构造函数
        ///     限制只能从内部创建新实例
        /// </summary>
        internal OilUser()
        {
        }

        #endregion

        #region 属性

        /// <summary>
        ///     序列号
        /// </summary>
        public string Sn { get; internal set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN { get; internal set; }

        /// <summary>
        ///     TSR，自上一次修理以来使用小时数
        /// </summary>
        public decimal TSR { get; internal set; }

        /// <summary>
        ///     CSN，自装机以来使用循环
        /// </summary>
        public decimal CSN { get; internal set; }

        /// <summary>
        ///     CSR，自上一次修理以来使用循环
        /// </summary>
        public decimal CSR { get; internal set; }

        /// <summary>
        ///     是否需要监控
        /// </summary>
        public bool NeedMonitor { get; private set; }

        /// <summary>
        ///     监控状态
        /// </summary>
        public OilMonitorStatus MonitorStatus { get; private set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     序号件ID
        /// </summary>
        public int SnRegID { get; private set; }

        #endregion

        #region 导航属性

        /// <summary>
        ///     滑油消耗数据
        /// </summary>
        public virtual ICollection<OilMonitor> OilMonitors
        {
            get { return _oilMonitors ?? (_oilMonitors = new HashSet<OilMonitor>()); }
            set { _oilMonitors = new HashSet<OilMonitor>(value); }
        }

        #endregion

        #region 操作

        /// <summary>
        ///     添加滑油消耗数据
        /// </summary>
        /// <param name="date">日期</param>
        /// <param name="tsn">TSN</param>
        /// <param name="tsr">TSR</param>
        /// <param name="totalRate">总消耗率</param>
        /// <param name="intervalRate">区间消耗率</param>
        /// <param name="deltaIntervalRate">区间消耗率增量</param>
        /// <param name="averageRate3">总消耗率3天移动平均</param>
        /// <param name="averageRate7">总消耗率7天移动平均</param>
        /// <returns>滑油消耗数据</returns>
        public OilMonitor AddOilMonitor(
            DateTime date,
            decimal tsn,
            decimal tsr,
            decimal totalRate,
            decimal intervalRate,
            decimal deltaIntervalRate,
            decimal averageRate3,
            decimal averageRate7)
        {
            var oilMonitor = new OilMonitor
            {
                Date = date,
                TSN = tsn,
                TSR = tsr,
                TotalRate = totalRate,
                IntervalRate = intervalRate,
                DeltaIntervalRate = deltaIntervalRate,
                AverageRate3 = averageRate3,
                AverageRate7 = averageRate7,
                OilUserID = Id
            };
            oilMonitor.GenerateNewIdentity();
            if (!NeedMonitor)
                NeedMonitor = true;

            OilMonitors.Add(oilMonitor);
            return oilMonitor;
        }

        /// <summary>
        ///     设置序号件
        /// </summary>
        /// <param name="snReg">序号件</param>
        public void SetSnReg(SnReg snReg)
        {
            if (snReg == null || snReg.IsTransient())
            {
                throw new ArgumentException("序号件参数为空！");
            }

            Sn = snReg.Sn;
            SnRegID = snReg.Id;
        }

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

        #region IValidatableObject 成员

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            #region 验证逻辑

            #endregion

            return validationResults;
        }

        #endregion
    }
}