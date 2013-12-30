#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/27 14:51:49
// 文件名：CaacProgrammingLineDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO.CaacProgrammingDTO
{
    /// <summary>
    /// 民航局五年规划行
    /// </summary>
    [DataServiceKey("Id")]
    public class CaacProgrammingLineDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     数量
        /// </summary>
        public int Number { get; set; }

        /// <summary>
        ///     年份
        /// </summary>
        public int Year { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机类别（座机）
        /// </summary>
        public Guid AircraftCategoryId { get; set; }

        /// <summary>
        ///     民航局下发规划
        /// </summary>
        public Guid CaacProgrammingId { get; set; }

        #endregion
    }
}
