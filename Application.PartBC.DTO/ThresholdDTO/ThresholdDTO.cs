#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 16:04:52
// 文件名：ThresholdDTO
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 16:04:52
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// SpecialConfig
    /// </summary>
    [DataServiceKey("Id")]
    public class ThresholdDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     总滑油消耗率阀值
        /// </summary>
        public decimal TotalThreshold { get; set; }

        /// <summary>
        ///     区间滑油消耗率阀值
        /// </summary>
        public decimal IntervalThreshold { get; set; }

        /// <summary>
        ///     区间滑油消耗率变化量阀值
        /// </summary>
        public decimal DeltaIntervalThreshold { get; set; }

        /// <summary>
        ///     3天移动平均阀值
        /// </summary>
        public decimal Average3Threshold { get; set; }

        /// <summary>
        ///     7天移动平均阀值
        /// </summary>
        public decimal Average7Threshold { get; set; }

        /// <summary>
        ///    附件件号
        /// </summary>
        public string Pn { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     阀值适用件号外键
        /// </summary>
        public int PnRegId { get; set; }

        #endregion
    }
}
