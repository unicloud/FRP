#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，18:12
// 方案：FRP
// 项目：Domain.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;
using UniCloud.Domain.Common.Enums;

namespace UniCloud.Domain.UberModel.Aggregates.DocumentPathAgg
{
    /// <summary>
    ///     文档路径工厂
    /// </summary>
    public static class DocumentPathFactory
    {
        /// <summary>
        /// 创建文档路径
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="isLeaf">是否是叶子节点，叶子节点，表示具体的文档</param>
        /// <param name="extension">扩展名</param>
        /// <param name="documentId">叶子节点</param>
        /// <param name="parentId">父节点</param>
        /// <param name="pathSource">路径类型</param>
        /// <returns>文档路径</returns>
        public static DocumentPath CreateDocumentPath(string name, bool isLeaf, string extension,
                        Guid? documentId, int? parentId, PathSource pathSource)
        {
            return new DocumentPath
            {
                Name = name,
                IsLeaf = isLeaf,
                Extension = extension,
                DocumentGuid = documentId,
                ParentId = parentId,
                PathSource = pathSource,
            };
        }
    }
}