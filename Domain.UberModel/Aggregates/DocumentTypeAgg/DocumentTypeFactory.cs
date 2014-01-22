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

namespace UniCloud.Domain.UberModel.Aggregates.DocumentTypeAgg
{
    /// <summary>
    ///     文档类型工厂
    /// </summary>
    public static class DocumentTypeFactory
    {
        /// <summary>
        /// 创建文档类型
        /// </summary>
        /// <param name="id">主键Id</param>
        /// <param name="name">名字</param>
        /// <param name="description">描述</param>
        /// <returns></returns>
        public static DocumentType CreateDocumentType(int id, string name, string description)
        {
            var documentType = new DocumentType
                               {
                                   Name = name,
                                   Description = description
                               };
            documentType.ChangeCurrentIdentity(id);
            return documentType;
        }
    }
}
