﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/06，10:12
// 文件名：IMaterialAppService.cs
// 程序集：UniCloud.Application.CommonServiceBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion


#region

using System.Linq;
using UniCloud.Application.CommonServiceBC.DTO;

#endregion

namespace UniCloud.Application.CommonServiceBC.DocumentServices
{
    /// <summary>
    ///     表示用于处理文档相关信息服务接口。
    /// </summary>
    public interface IDocumentAppService
    {
       
        /// <summary>
        ///   获取文件夹
        /// </summary>
        /// <returns>文件夹DTO集合</returns>
        IQueryable<FolderDTO> GetFolders();

        /// <summary>
        /// 获取文件夹下的子文件夹与文档
        /// </summary>
        /// <returns></returns>
        IQueryable<FolderDocumentDTO> GetFolderDocuments();
    }
}