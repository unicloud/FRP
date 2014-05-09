#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/9 9:34:54
// 文件名：AircraftMaintainPlanDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/9 9:34:54
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// AircraftMaintainPlanDTO
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftMaintainPlanDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 上半年计划（总计架次）
        /// </summary>
        public int FirstHalfYear { get; set; }

        /// <summary>
        /// 下半年计划（总计架次）
        /// </summary>
        public int SecondHalfYear { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 年度ID
        /// </summary>
        public Guid AnnualId { get; set; }

        private List<AircraftMaintainPlanDetailDTO> _aircraftMaintainPlanDetails;
        public List<AircraftMaintainPlanDetailDTO> AircraftMaintainPlanDetails
        {
            get { return _aircraftMaintainPlanDetails ?? (_aircraftMaintainPlanDetails = new List<AircraftMaintainPlanDetailDTO>()); }
            set { _aircraftMaintainPlanDetails = value; }
        }
        #endregion
    }
}
