#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 11:11:29
// 文件名：AircraftSeriesDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
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

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 飞机系列
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftSeriesDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     系列名称
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}
