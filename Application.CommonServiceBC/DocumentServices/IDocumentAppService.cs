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
        ///   获取文档
        /// </summary>
        /// <returns>文档DTO集合</returns>
        IQueryable<DocumentDTO> GetDocuments();

        /// <summary>
        ///     新增文档。
        /// </summary>
        /// <param name="document">文档DTO</param>
        void InsertDocument(DocumentDTO document);


        /// <summary>
        /// 获取文档路径
        /// </summary>
        /// <returns>文档路径</returns>
        IQueryable<DocumentPathDTO> GetDocumentPaths();

        /// <summary>
        ///     新增路径DTO。
        /// </summary>
        /// <param name="documentPath">文档路径DTO</param>
        void InsertDocumentPath(DocumentPathDTO documentPath);

        /// <summary>
        ///     修改文档路径。
        /// </summary>
        /// <param name="documentPath">文档路径DTO</param>
        void ModifyDocumentPath(DocumentPathDTO documentPath);


        /// <summary>
        ///     删除联系人。
        /// </summary>
        /// <param name="documentPath">文档路径DTO</param>
        void DeleteDocumentPath(DocumentPathDTO documentPath);

    }
}