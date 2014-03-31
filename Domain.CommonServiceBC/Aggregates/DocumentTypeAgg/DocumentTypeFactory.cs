#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 9:42:59
// 文件名：DocumentTypeFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 9:42:59
// 修改说明：
// ========================================================================*/
#endregion

using System.Diagnostics.Contracts;

namespace UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg
{
    /// <summary>
    ///     文档类型工厂
    /// </summary>
    public static class DocumentTypeFactory
    {
        /// <summary>
        /// 创建文档类型
        /// </summary>
        /// <returns></returns>
        public static DocumentType CreateDocumentType()
        {
            var documentType = new DocumentType();
            documentType.GenerateNewIdentity();
            return documentType;
        }

        /// <summary>
        /// 设置文档类型属性
        /// </summary>
        /// <param name="documentType">当前文档类型</param>
        /// <param name="name">名字</param>
        /// <param name="description">描述</param>
        public static void SetDocumentType(DocumentType documentType, string name, string description)
        {
            documentType.Name = name;
            documentType.Description = description;
        }
    }
}
