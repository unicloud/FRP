#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:33:14
// 文件名：ProgrammingDTO
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

namespace UniCloud.Application.FleetPlanBC.DTO.ProgrammingDTO
{
    /// <summary>
    /// 五年规划期间
    /// </summary>
    [DataServiceKey("Id")]
    public class ProgrammingDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     规划期间
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     规划开始时间
        /// </summary>
        public DateTime StartDate { get; set; }


        /// <summary>
        ///     规划结束时间
        /// </summary>
        public DateTime EndDate { get; set; }
        #endregion
    }
}
