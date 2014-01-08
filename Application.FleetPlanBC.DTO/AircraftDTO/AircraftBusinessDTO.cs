#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:21:26
// 文件名：AircraftBusinessDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 飞机商业数据历史
    /// </summary>
    [DataServiceKey("AircraftBusinessId")]
    public class AircraftBusinessDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid AircraftBusinessId { get; set; }

        /// <summary>
        ///     座位数
        /// </summary>
        public int SeatingCapacity { get; set; }

        /// <summary>
        ///     商载量
        /// </summary>
        public decimal CarryingCapacity { get; set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     处理状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        ///     机型
        /// </summary>
        public string AircraftTypeName { get; set; }

        /// <summary>
        ///     飞机座级
        /// </summary>
        public string Regional { get;set; }

        /// <summary>
        ///  飞机座级类型
        /// </summary>
        public string Category { get; set; }
        #endregion

        #region 外键属性

        /// <summary>
        ///     飞机外键
        /// </summary>
        public Guid AircraftId { get; set; }

        /// <summary>
        ///     机型外键
        /// </summary>
        public Guid AircraftTypeId { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; set; }


        #endregion
    }
}
