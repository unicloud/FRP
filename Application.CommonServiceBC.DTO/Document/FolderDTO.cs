#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/06，09:12
// 文件名：FolderDTO.cs
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

namespace UniCloud.Application.CommonServiceBC.DTO
{
    /// <summary>
    ///     文件夹管理
    /// </summary>
    [DataServiceKey("FolderId")]
    public class FolderDTO
    {
        /// <summary>
        ///     文件夹主键
        /// </summary>
        public Guid FolderId { get; set; }

        /// <summary>
        ///     名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     修改日期
        /// </summary>
        public DateTime UpdteDateTime { get; set; }

        /// <summary>
        ///     创建者
        /// </summary>
        public string Creator { get; set; }

        /// <summary>
        ///     主项文件夹
        /// </summary>
        public Guid? ParentFolderId { get; set; }

        /// <summary>
        ///     子项文件夹
        /// </summary>
        public List<FolderDTO> SubFolders { get; set; }
    }
}