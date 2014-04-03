#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 13:56:59
// 文件名：AircraftCabinDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 13:56:59
// 修改说明：
// ========================================================================*/
#endregion

using System.Data.Services.Common;

namespace UniCloud.Application.AircraftConfigBC.DTO
{
    /// <summary>
    /// 舱位
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftCabinDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 舱位类型
        /// </summary>
        public int AircraftCabinTypeId { get; set; }

        /// <summary>
        /// 座位数
        /// </summary>
        public int SeatNumber { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
        #endregion
    }
}
