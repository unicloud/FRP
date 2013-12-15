#region 版本信息

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

using System;
using System.Linq;
using UniCloud.Application.PurchaseBC.DTO;

#endregion

namespace UniCloud.Application.PurchaseBC.DocumentPathServices
{
    /// <summary>
    ///     表示用于处理文档相关信息服务接口。
    /// </summary>
    public interface IDocumentPathAppService
    {
     
        /// <summary>
        /// 获取文档路径
        /// </summary>
        /// <returns>文档路径</returns>
        IQueryable<DocumentPathDTO> GetDocumentPaths();

        /// <summary>
        ///     删除文档路径。
        /// </summary>
        /// <param name="documentPathId">文档路径DTO</param>
        void DelDocPath(int documentPathId);

        /// <summary>
        /// 添加文档
        /// </summary>
        /// <param name="name"></param>
        /// <param name="isLeaf"></param>
        /// <param name="documentId"></param>
        /// <param name="parentId"></param>
        /// <param name="pathSource"></param>
        void AddDocPath(string name, string isLeaf,string documentId, int parentId, int pathSource);

    }
}