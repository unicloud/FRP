#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:32:23
// 文件名：ManufacturerDTO
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
    /// 制造商
    /// </summary>
    [DataServiceKey("Id")]
    public class ManufacturerDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     制造商中文名称
        /// </summary>
        public string CnName { get; set; }

        /// <summary>
        ///     制造商英文名称
        /// </summary>
        public string EnName { get; set; }

        /// <summary>
        ///     制造商中文简称
        /// </summary>
        public string CnShortName { get; set; }

        /// <summary>
        ///     制造商英文简称
        /// </summary>
        public string EnShortName { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        /// 制造商类型 1表示飞机制造商，2表示发动机制造商
        /// </summary>
        public int Type { get; set; }
        #endregion
    }
}
