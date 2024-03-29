﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/3 10:37:56
// 文件名：AircraftTypeDTO
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

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    /// 机型
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
