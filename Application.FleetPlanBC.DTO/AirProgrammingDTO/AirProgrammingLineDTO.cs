#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 14:52:07
// 文件名：AirProgrammingLineDTO
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
    /// 航空公司五年规划行
    /// </summary>
    [DataServiceKey("Id")]
    public class AirProgrammingLineDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     年份
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        ///     购买数量
        /// </summary>
        public int BuyNum { get; set; }

        /// <summary>
        ///     退出数量
        /// </summary>
        public int ExportNum { get; set; }

        /// <summary>
        ///     租赁数量
        /// </summary>
        public int LeaseNum { get; set; }


        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机系列
        /// </summary>
        public Guid AircraftSeriesId { get; set; }

        /// <summary>
        ///     航空公司规划
        /// </summary>
        public Guid AirProgrammingId { get; set; }

        #endregion
    }
}
