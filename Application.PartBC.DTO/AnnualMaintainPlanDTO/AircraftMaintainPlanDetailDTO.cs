#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/9 9:36:18
// 文件名：AircraftMaintainPlanDetailDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/9 9:36:18
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///  AircraftMaintainPlanDetailDTO
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftMaintainPlanDetailDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 机号
        /// </summary>
        public string AircraftNumber { get; set; }
        /// <summary>
        /// 机型
        /// </summary>
        public string AircraftType { get; set; }
        /// <summary>
        /// 定检级别
        /// </summary>
        public string Level { get; set; }
        /// <summary>
        /// 预计进场时间
        /// </summary>
        public DateTime InDate { get; set; }
        /// <summary>
        /// 预计出场时间
        /// </summary>
        public DateTime OutDate { get; set; }
        #endregion
    }
}
