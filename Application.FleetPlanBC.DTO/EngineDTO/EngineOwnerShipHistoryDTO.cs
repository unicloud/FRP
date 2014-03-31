#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:29:07
// 文件名：EngineOwnershipHistoryDTO
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
    /// 发动机所有权历史
    /// </summary>
    [DataServiceKey("Id")]
    public class EngineOwnershipHistoryDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

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
        ///     实际发动机外键
        /// </summary>
        public Guid EngineId { get; set; }

        /// <summary>
        ///     所有权人
        /// </summary>
        public int SupplierId { get; set; }

        #endregion
    }
}
