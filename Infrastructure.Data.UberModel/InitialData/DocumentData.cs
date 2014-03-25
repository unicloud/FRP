#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/10，13:12
// 文件名：DocumentData.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：VVersionNumber
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.UberModel.Aggregates.DocumentAgg;
using UniCloud.Domain.UberModel.Aggregates.DocumentPathAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    /// <summary>
    ///     文档相关数据
    /// </summary>
    public class DocumentData : InitialDataBase
    {
        public DocumentData(UberModelUnitOfWork context)
            : base(context)
        {
        }

        public override void InitialData()
        {
            //根目录
            var rootDocumentPath = DocumentPathFactory
                .CreateDocumentPath("合同管理", false, null, null, null, "\\合同管理");
            rootDocumentPath.GenerateNewIdentity();
            Context.DocumentPaths.Add(rootDocumentPath);
            var importPath = DocumentPathFactory
                .CreateDocumentPath("飞机引进", false, null, null, rootDocumentPath.Id, "\\合同管理\\飞机引进");
            importPath.GenerateNewIdentity();
            Context.DocumentPaths.Add(importPath);
            var exportPath = DocumentPathFactory
                .CreateDocumentPath("飞机退出", false, null, null, rootDocumentPath.Id, "\\合同管理\\飞机退出");
            exportPath.GenerateNewIdentity();
            Context.DocumentPaths.Add(exportPath);
            var documents = new List<Document>();
            var document1 = DocumentFactory.CreateStandardDocument(Guid.Parse("8C58622E-01E3-4F61-B34D-619D3FB43211"), "计划引进飞机10架飞机.docx", "docx", null, null, "XXX",
                                               true, null, null,3);
            var document2 = DocumentFactory.CreateStandardDocument(Guid.Parse("8C58622E-01E3-4F61-B34D-619D3FB43222"), "计划引进飞机11架飞机.docx", "docx", null, null, "XXX",
                                               true, null, null, 3);
            var document3 = DocumentFactory.CreateStandardDocument(Guid.Parse("8C58622E-01E3-4F61-B34D-619D3FB43233"), "计划引进飞机11架飞机.xlsx", "xlsx", null, null, "XXX",
                                               true, null, null, 3);

            var document4 = DocumentFactory.CreateStandardDocument(Guid.Parse("8C58622E-01E3-4F61-B34D-619D3FB43244"), "计划退出飞机10架飞机.docx", "docx", null, null, "XXX",
                                   true, null, null, 3);
            var document5 = DocumentFactory.CreateStandardDocument(Guid.Parse("8C58622E-01E3-4F61-B34D-619D3FB43255"), "计划退出飞机11架飞机.docx", "docx", null, null, "XXX",
                                               true, null, null, 3);
            var document6 = DocumentFactory.CreateStandardDocument(Guid.Parse("8C58622E-01E3-4F61-B34D-619D3FB43266"), "计划退出飞机11架飞机.xlsx", "xlsx", null, null, "XXX",
                                               true, null, null, 3);

            documents.Add(document1);
            documents.Add(document2);
            documents.Add(document3);
            documents.Add(document4);
            documents.Add(document5);
            documents.Add(document6);

            var documentPath1 = DocumentPathFactory
                  .CreateDocumentPath(document1.FileName, true, document1.Extension,
                  document1.Id, importPath.Id, "\\合同管理\\飞机引进\\" + document1.FileName);
            var documentPath2 = DocumentPathFactory
                  .CreateDocumentPath(document2.FileName, true, document2.Extension,
                  document2.Id, importPath.Id, "\\合同管理\\飞机引进\\" + document2.FileName);
            var documentPath3 = DocumentPathFactory
                  .CreateDocumentPath(document3.FileName, true, document3.Extension,
                  document3.Id, importPath.Id, "\\合同管理\\飞机引进\\" + document3.FileName);

            var documentPath4 = DocumentPathFactory
             .CreateDocumentPath(document4.FileName, true, document4.Extension,
             document4.Id, exportPath.Id, "\\合同管理\\飞机退出\\" + document4.FileName);
            var documentPath5 = DocumentPathFactory
                  .CreateDocumentPath(document5.FileName, true, document5.Extension,
                  document5.Id, exportPath.Id, "\\合同管理\\飞机退出\\" + document5.FileName);
            var documentPath6 = DocumentPathFactory
                  .CreateDocumentPath(document6.FileName, true, document6.Extension,
                  document6.Id, exportPath.Id, "\\合同管理\\飞机退出\\" + document6.FileName);

            documentPath1.GenerateNewIdentity();
            documentPath2.GenerateNewIdentity();
            documentPath3.GenerateNewIdentity();
            documentPath4.GenerateNewIdentity();
            documentPath5.GenerateNewIdentity();
            documentPath6.GenerateNewIdentity();
            Context.DocumentPaths.Add(documentPath1);
            Context.DocumentPaths.Add(documentPath2);
            Context.DocumentPaths.Add(documentPath3);
            Context.DocumentPaths.Add(documentPath4);
            Context.DocumentPaths.Add(documentPath5);
            Context.DocumentPaths.Add(documentPath6);

            rootDocumentPath.DocumentPaths.Add(importPath);
            rootDocumentPath.DocumentPaths.Add(exportPath);
            documents.ForEach(p => Context.Documents.Add(p));
            Context.DocumentPaths.Add(rootDocumentPath);

        }
    }
}