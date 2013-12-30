#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/26 18:30:27
// 文件名：EngineTypeDTO
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
    /// 发动机型号
    /// </summary>
    [DataServiceKey("Id")]
    public class EngineTypeDTO
    {
        #region 属性

        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }


        /// <summary>
        ///     发动机型号名称
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region 外键属性

        /// <summary>
        ///    制造商
        /// </summary>
        public Guid ManufacturerId { get; set; }

        #endregion
    }
}
