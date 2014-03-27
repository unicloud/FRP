#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:24:42
// 文件名：AircraftTypeDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.AircraftConfigBC.DTO
{
    /// <summary>
    /// 机型
    /// </summary>
    [DataServiceKey("AircraftTypeId")]
    public class AircraftTypeDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid AircraftTypeId { get; set; }

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
        /// 所属座级
        /// </summary>
        public string AircraftCategoryName { get; set; }

        /// <summary>
        /// 系列
        /// </summary>
        public Guid AircraftSeriesId { get; set; }

        /// <summary>
        /// 系列
        /// </summary>
        public string AircraftSeriesName { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public Guid ManufacturerId { get; set; }

        /// <summary>
        /// 制造商
        /// </summary>
        public string ManufacturerName { get; set; }

        /// <summary>
        /// 民航机型
        /// </summary>
        public Guid CaacAircraftTypeId { get; set; }

        /// <summary>
        /// 民航机型
        /// </summary>
        public string CaacAircraftTypeName { get; set; }
        #endregion
    }
}
