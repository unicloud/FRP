#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 22:26:56
// 文件名：BasicConfigHistoryDTO
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

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    ///     BasicConfigHistory
    /// </summary>
    [DataServiceKey("Id")]
    public class BasicConfigHistoryDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     开始时间
        /// </summary>
        public DateTime StartDate { get; set; }

        /// <summary>
        ///     结束时间
        /// </summary>
        public DateTime? EndDate { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     合同飞机外键
        /// </summary>
        public int ContractAircraftId { get; set; }

        /// <summary>
        ///     基本构型组外键
        /// </summary>
        public int BasicConfigGroupId { get; set; }

        #endregion
    }
}