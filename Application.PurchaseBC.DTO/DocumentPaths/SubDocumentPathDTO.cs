#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/11，10:12
// 文件名：SubDocumentPathDTO.cs
// 程序集：UniCloud.Application.CommonServiceBC.DTO
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
    /// <summary>
    ///     子项文档路径
    /// </summary>
    [DataServiceKey("SubDocumentPathId")]
    public class SubDocumentPathDTO
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int SubDocumentPathId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     是否叶子节点
        /// </summary>
        public bool IsLeaf { get; set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     文档ID
        /// </summary>
        public Guid? DocumentGuid { get; set; }

        /// <summary>
        ///     路径源
        /// </summary>
        public int PathSource { get; set; }

        /// <summary>
        ///     父节点ID
        /// </summary>
        public int? ParentId { get; set; }
    }
}