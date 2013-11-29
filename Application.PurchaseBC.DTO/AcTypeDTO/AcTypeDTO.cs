#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/21，16:11
// 文件名：AcTypeDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    [DataServiceKey("AcTypeId")]
    public class AcTypeDTO
    {
        /// <summary>
        ///     机型名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     获取持久化对象的Id
        /// </summary>
        public Guid AcTypeId { get; set; }
    }
}