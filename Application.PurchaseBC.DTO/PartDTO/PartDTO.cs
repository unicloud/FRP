#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，13:11
// 文件名：PartDTO.cs
// 程序集：UniCloud.Application.PurchaseBC.DTO
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///     部件信息
    /// </summary>
    [DataServiceKey("PartId")]
    public class PartDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int PartId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     型号
        /// </summary>
        public string Pn { get; set; }
    }
}