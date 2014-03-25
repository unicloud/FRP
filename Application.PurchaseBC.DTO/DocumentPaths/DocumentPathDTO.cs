#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/06，09:12
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
using System.Collections.Generic;
using System.Data.Services.Common;

#endregion

namespace UniCloud.Application.PurchaseBC.DTO
{
    /// <summary>
    ///    文档路径
    /// </summary>
    [DataServiceKey("DocumentPathId")]
    public class DocumentPathDTO
    {
        public DocumentPathDTO()
        {
            SubDocumentPaths=new List<SubDocumentPathDTO>();
        }
        /// <summary>
        /// 主键
        /// </summary>
        public int DocumentPathId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     是否叶子节点
        /// </summary>
        public bool IsLeaf { get;  set; }

        /// <summary>
        ///     扩展名
        /// </summary>
        public string Extension { get;  set; }

        /// <summary>
        ///     文档ID
        /// </summary>
        public Guid? DocumentGuid { get; set; }

        /// <summary>
        ///     路径
        /// </summary>
        public string Path { get;  set; }

        /// <summary>
        ///     父节点ID
        /// </summary>
        public int? ParentId { get;  set; }

        /// <summary>
        /// 子项文档集合
        /// </summary>
        public  List<SubDocumentPathDTO> SubDocumentPaths { get; set; }

    }
}