#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:32:58
// 文件名：PlanEngineDTO
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
    /// 计划发动机
    /// </summary>
    [DataServiceKey("Id")]
    public class PlanEngineDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     实际发动机ID
        /// </summary>
        public Guid? EngineId { get; set; }

        /// <summary>
        ///     发动机型号外键
        /// </summary>
        public Guid EngineTypeId { get; set; }

        /// <summary>
        ///     航空公司外键
        /// </summary>
        public Guid AirlinesId { get; set; }
        #endregion
    }
}
