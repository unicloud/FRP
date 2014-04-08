#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:26:03
// 文件名：AnnualDTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.FleetPlanBC.DTO
{
    /// <summary>
    /// 计划年度
    /// </summary>
    [DataServiceKey("Id")]
    public class AnnualDTO
    {

        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     年度
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        ///     是否打开年度
        /// </summary>
        public bool IsOpen { get; set; }

        /// <summary>
        /// 规划期间
        /// </summary>
        public string ProgrammingName { get;set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///     五年规划期间ID
        /// </summary>
        public Guid ProgrammingId { get; set; }

        #endregion

    }
}
