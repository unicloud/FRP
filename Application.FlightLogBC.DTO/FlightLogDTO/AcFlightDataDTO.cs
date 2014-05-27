#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/26 14:01:25
// 文件名：AcFlightDataDTO
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/26 14:01:25
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

namespace UniCloud.Application.FlightLogBC.DTO
{
    /// <summary>
    /// 飞机每日飞行数据统计
    /// </summary>
    [DataServiceKey("Id")]
    public class AcFlightDataDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     机号
        /// </summary>
        public string RegNumber { get; set; }

        /// <summary>
        ///     日期
        /// </summary>
        public DateTime FlightDate { get; set; }

        /// <summary>
        ///     飞行小时
        /// </summary>
        public decimal FlightHour { get; set; }

        /// <summary>
        ///     飞行循环
        /// </summary>
        public decimal FlightCycle { get; set; }
        #endregion
    }
}
