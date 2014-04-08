#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 21:55:15
// 文件名：AircraftCabinTypeDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 21:55:15
// 修改说明：
// ========================================================================*/
#endregion

using System.Data.Services.Common;

namespace UniCloud.Application.BaseManagementBC.DTO
{
    /// <summary>
    /// 舱位类型
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftCabinTypeDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Note { get; set; }
        #endregion
    }
}
