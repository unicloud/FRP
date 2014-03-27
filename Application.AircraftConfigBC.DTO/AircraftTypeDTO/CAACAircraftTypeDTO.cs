#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 9:42:05
// 文件名：CAACAircraftTypeDTO
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 9:42:05
// 修改说明：
// ========================================================================*/
#endregion
using System;
using System.Data.Services.Common;

namespace UniCloud.Application.AircraftConfigBC.DTO
{
    /// <summary>
    /// 机型
    /// </summary>
    [DataServiceKey("CAACAircraftTypeId")]
    public class CAACAircraftTypeDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid CAACAircraftTypeId { get; set; }

        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 所属座级
        /// </summary>
        public Guid AircraftCategoryId { get; set; }

        /// <summary>
        /// 系列
        /// </summary>
        public Guid AircraftSeriesId { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public Guid ManufacturerId { get; set; }
        #endregion
    }
}
