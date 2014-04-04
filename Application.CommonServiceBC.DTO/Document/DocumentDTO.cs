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
        public Guid DocumentId { get; set; }

        /// <summary>
        ///     文件名
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        ///     文档
        /// </summary>
        public byte[] FileStorage { get; set; }

        /// <summary>
        /// 文档文本内容
        /// </summary>
        public string FileContent { get; set; }

        /// <summary>
        /// 文档类型Id
        /// </summary>
        public int DocumentTypeId { get; set; }

        /// <summary>
        ///     摘要
        /// </summary>
        public string Abstract { get; set; }

        /// <summary>
        ///     备注
        /// </summary>
        public string Note { get; set; }

        /// <summary>
        ///     上传者
        /// </summary>
        public string Uploader { get; set; }

        /// <summary>
        ///     是否有效
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        ///     创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }
    }
}