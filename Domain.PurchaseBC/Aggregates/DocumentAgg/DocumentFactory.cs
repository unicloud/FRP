#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/12，17:01
// 方案：FRP
// 项目：Domain.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using UniCloud.Domain.Common.Enums;

#endregion

namespace UniCloud.Domain.PurchaseBC.Aggregates.DocumentAgg
{
    /// <summary>
    ///     文档工厂
    /// </summary>
    public static class DocumentFactory
    {
        /// <summary>
        ///     新增标准文档
        /// </summary>
        /// <param name="docId">文档主键</param>
        /// <param name="fileName">名称</param>
        /// <param name="extension">扩展名</param>
        /// <param name="abstractInfo">摘要</param>
        /// <param name="note">备注</param>
        /// <param name="uploader">上传者</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="stream">字节数组</param>
        /// <returns>标准文档</returns>
        public static StandardDocument CreateStandardDocument(Guid docId, string fileName, string extension,
            string abstractInfo, string note, string uploader, bool isValid, byte[] stream)
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
                FileStorage = stream
            };
            doc.ChangeCurrentIdentity(docId);
            doc.SetIndexStatus(IndexStatus.未建);
            return doc;
        }

        /// <summary>
        ///     更新标准文档
        /// </summary>
        /// <param name="document">文档</param>
        /// <param name="fileName">名称</param>
        /// <param name="extension">扩展名</param>
        /// <param name="abstractInfo">摘要</param>
        /// <param name="note">备注</param>
        /// <param name="uploader">上传者</param>
        /// <param name="isValid">是否有效</param>
        /// <param name="stream">字节数组</param>
        public static void UpdateDocument(Document document, string fileName, string extension,
            string abstractInfo, string note, string uploader, bool isValid, byte[] stream)
        {
            document.FileName = fileName;
            document.Extension = extension;
            document.Abstract = abstractInfo;
            document.Note = note;
            document.Uploader = uploader;
            document.IsValid = isValid;
            document.FileStorage = stream;
        }
    }
}