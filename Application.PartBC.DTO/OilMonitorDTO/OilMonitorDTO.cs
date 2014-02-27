#region 版本信息
// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/27，16:31
// 方案：FRP
// 项目：Application.PartBC.DTO
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================
#endregion

using System;
using System.Data.Services.Common;

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     滑油消耗DTO
    /// </summary>
    [DataServiceKey("Id")]
    public class OilMonitorDTO
    {
        /// <summary>
        ///     发动机滑油监控ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     日期
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        ///     TSN，自装机以来使用小时数
        /// </summary>
        public decimal TSN { get; set; }

        /// <summary>
        ///     TSR，自上一次修理以来使用小时数
        /// </summary>
        public decimal TSR { get; set; }

        /// <summary>
        ///     总滑油消耗率
        /// </summary>
        public decimal TotalRate { get; set; }

        /// <summary>
        ///     区间滑油消耗率
        /// </summary>
        public decimal IntervalRate { get; set; }

        /// <summary>
        ///     区间滑油消耗率变化量
        /// </summary>
        public decimal DeltaIntervalRate { get; set; }

        /// <summary>
        ///     3天移动平均
        /// </summary>
        public decimal AverageRate3 { get; set; }

        /// <summary>
        ///     7天移动平均
        /// </summary>
        public decimal AverageRate7 { get; set; }
    }
}