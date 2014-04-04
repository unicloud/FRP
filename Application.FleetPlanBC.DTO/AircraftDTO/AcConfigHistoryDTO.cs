#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 14:22:12
// 文件名：AcConfigHistoryDTO
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
    /// 飞机配置历史
    /// </summary>
    [DataServiceKey("Id")]
    public class AcConfigHistoryDTO
    {
        #region 属性
        /// <summary>
        ///  主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机外键
        /// </summary>
        public Guid AircraftId { get; set; }


        /// <summary>
        ///     飞机配置外键
        /// </summary>
        public int AircraftConfigurationId { get; set; }

        #endregion
    }
}
