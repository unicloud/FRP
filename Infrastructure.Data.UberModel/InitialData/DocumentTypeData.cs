#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 10:00:07
// 文件名：DocumentTypeData
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 10:00:07
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UniCloud.Domain.UberModel.Aggregates.AnnualAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentTypeAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    /// <summary>
    ///     文档类型相关数据
    /// </summary>
    public class DocumentTypeData : InitialDataBase
    {
        public DocumentTypeData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            var documentTypes = new List<DocumentType>
            {
                DocumentTypeFactory.CreateDocumentType(1,"报告",""),
                DocumentTypeFactory.CreateDocumentType(2,"规划",""),
                DocumentTypeFactory.CreateDocumentType(3,"合同",""),
            };
            documentTypes.ForEach(a => Context.DocumentTypes.AddOrUpdate(u => u.Id, a));
        }
    }
}
