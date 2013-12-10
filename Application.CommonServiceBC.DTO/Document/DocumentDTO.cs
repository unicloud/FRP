#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/06，10:12
// 文件名：DocumentDTO.cs
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

namespace UniCloud.Application.CommonServiceBC.DTO
{
    /// <summary>
    /// 文档
    /// </summary>
    [DataServiceKey("DocumentId")]
    public class DocumentDTO
    {
        /// <summary>
        ///     主键
        /// </summary>
        public Guid DocumentId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     文档创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        public string ExtendType { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime UpdateDateTime { get; set; }

        /// <summary>
        ///     文档外键
        /// </summary>
        public Guid FolderId { get; set; }
    }
}