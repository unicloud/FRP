#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:28:54
// 文件名：EngineBusinessHistoryDTO
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
    /// 发动机商业数据历史
    /// </summary>
    [DataServiceKey("Id")]
    public class EngineBusinessHistoryDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     开始日期
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        ///     结束日期
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        ///     最大推力
        /// </summary>
        public decimal MaxThrust { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     实际发动机外键
        /// </summary>
        public Guid EngineId { get; set; }

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeId { get; set; }

        /// <summary>
        ///     引进方式
        /// </summary>
        public Guid ImportCategoryId { get; set; }

        #endregion
    }
}
