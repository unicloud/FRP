﻿#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/06/21，10:06
// 方案：FRP
// 项目：Application.PortalBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PortalBC.DTO
{
    /// <summary>
    ///     飞机系列
    /// </summary>
    [DataServiceKey("Id")]
    public class AircraftSeriesDTO
    {
        #region 属性

        /// <summary>
        ///     主键
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///     系列名称
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}