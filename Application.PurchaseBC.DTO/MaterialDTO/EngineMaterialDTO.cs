﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，10:11
// 文件名：EngineMaterialDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     发动机物料
    /// </summary>
    [DataServiceKey("EngineMaterialId")]
    public class EngineMaterialDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int EngineMaterialId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     附件件号
        /// </summary>
        public string Pn { get; set; }

        /// <summary>
        /// 制造商Id
        /// </summary>
        public Guid? ManufacturerId { get; set; }

        /// <summary>
        /// 制造商名称
        /// </summary>
        public string Manufacturer { get; set; }

        /// <summary>
        /// 发动机目录价
        /// </summary>
        public decimal ListPrice { get; set; }

    }
}