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

namespace UniCloud.Domain.UberModel.Aggregates.DocumentAgg
{
    /// <summary>
    ///     文档工厂
    /// </summary>
    public static class DocumentFactory
    {
        /// <summary>
        /// 新增标准文档
        /// </summary>
        /// <param name="docId">文档主键</param>
        /// <param name="fileName">名称</param>
        /// <param name="extension">扩展名</param>
        /// <param name="abstractInfo">摘要</param>
        /// <param name="note">备注</param>
        /// <param name="uploader">上传者</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="stream">字节数组</param>
        /// <param name="content">文本内容</param>
        /// <param name="documentTypeId">文档类型</param>
        /// <returns>标准文档</returns>
        public static StandardDocument CreateStandardDocument(Guid docId, string fileName, string extension,
                                        string abstractInfo, string note, string uploader, bool isValid,
                                        byte[] stream, string content, int documentTypeId)
        {
            var doc = new StandardDocument
            {
                FileName = fileName,
                Extension = extension,
                Abstract = abstractInfo,
                Note = note,
                Uploader = uploader,
                IsValid = isValid,
                CreateTime = DateTime.Now,
                FileStorage = stream,
                FileContent = content,
                DocumentTypeId = 1
            };
            doc.ChangeCurrentIdentity(docId);
            doc.SetIndexStatus(IndexStatus.未建);
            return doc;
        }
    }
}