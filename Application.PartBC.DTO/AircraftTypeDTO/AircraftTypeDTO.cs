#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AircraftTypeDTO
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

namespace UniCloud.Application.PartBC.DTO
{
    /// <summary>
    /// AircraftType
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftTypeDTO
    {
        #region 属性
        /// <summary>
        /// 主键
        /// </summary>
        public Guid Id { get; set; }
        
        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; set; }
        #endregion

    }
}
