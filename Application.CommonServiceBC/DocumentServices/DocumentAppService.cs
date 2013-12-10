#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/06，10:12
// 文件名：MaterialAppService.cs
// 程序集：UniCloud.Application.CommonServiceBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.CommonServiceBC.DTO;

#endregion

namespace UniCloud.Application.CommonServiceBC.DocumentServices
{
    /// <summary>
    ///     实现部件接口。
    ///     用于处理部件相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class DocumentAppService : IDocumentAppService
    {
        /// <summary>
        ///     获取文件夹
        /// </summary>
        /// <returns></returns>
        public IQueryable<FolderDTO> GetFolders()
        {
            var folders = new List<FolderDTO>
                {
                    new FolderDTO
                        {
                            Creator = "陈春勇",
                            FolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                            Name = "文件夹管理"
                        }
                };
            return folders.AsQueryable();
        }

        /// <summary>
        ///     获取文件夹下的文档与子文件夹
        /// </summary>
        /// <returns></returns>
        public IQueryable<FolderDocumentDTO> GetFolderDocuments()
        {
            var folders = new List<FolderDocumentDTO>
                {
                    new FolderDocumentDTO
                        {
                            Creator = "陈春勇",
                            FolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                            Name = "文件管理",
                            UpdteDateTime = DateTime.Now,
                            ParentFolderId = null,
                            SubFolders = new List<FolderDocumentDTO>
                                {
                                    new FolderDocumentDTO
                                        {
                                            Creator = "陈春勇",
                                            ParentFolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                                            Name = "飞机引进",
                                            UpdteDateTime = DateTime.Now,
                                            FolderId = Guid.Parse("B5A7D565-5A7E-4A4D-A962-9637056DE1C4"),
                                        },
                                    new FolderDocumentDTO
                                        {
                                            Creator = "陈春勇",
                                            ParentFolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                                            Name = "飞机退出",
                                            UpdteDateTime = DateTime.Now,
                                            FolderId = Guid.Parse("B5A7D565-5A7E-4A4D-A962-4537056DE1C5"),
                                        }
                                }
                        },
                    new FolderDocumentDTO
                        {
                            Creator = "陈春勇",
                            FolderId = Guid.Parse("F3107DB3-20BA-4D5C-ABDE-D330CA14D113"),
                            Name = "飞机引进",
                            UpdteDateTime = DateTime.Now,
                            ParentFolderId = Guid.Parse("B5A7D565-5A7E-4A4D-A962-9637056DE1C4"),
                            Documents = new List<DocumentDTO>
                                {
                                    new DocumentDTO
                                        {
                                            Creator = "陈春勇",
                                            DocumentId = Guid.Parse("1A28CB6A-FBD5-43FB-9BCF-B83AE25FA435"),
                                            ExtendType = "docx",
                                            FolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                                            Name = "引进2013年飞机计划1.doc"
                                        },
                                    new DocumentDTO
                                        {
                                            Creator = "陈春勇",
                                            DocumentId = Guid.Parse("0024F3CC-52C8-4FC0-9A03-7089F027A5CA"),
                                            ExtendType = "docx",
                                            FolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                                            Name = "引进2013年飞机计划2.doc"
                                        },
                                    new DocumentDTO
                                        {
                                            Creator = "陈春勇",
                                            DocumentId = Guid.Parse("8338E0C6-7C61-4785-9E14-D25C7507B782"),
                                            ExtendType = "xlsx",
                                            FolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                                            Name = "引进2013年飞机计划3.doc"
                                        }
                                },
                        },
                    new FolderDocumentDTO
                        {
                            Creator = "陈春勇",
                            FolderId = Guid.Parse("F3107DB3-20BA-4D5C-ABDE-D330CA14D144"),
                            Name = "飞机退出",
                            UpdteDateTime = DateTime.Now,
                            ParentFolderId = Guid.Parse("B5A7D565-5A7E-4A4D-A962-4537056DE1C5"),
                            Documents = new List<DocumentDTO>
                                {
                                    new DocumentDTO
                                        {
                                            Creator = "陈春勇",
                                            DocumentId = Guid.Parse("1A28CB6A-FBD5-43FB-9BCF-B83AE25FA536"),
                                            ExtendType = "docx",
                                            FolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                                            Name = "引进2013年飞机计划1.doc"
                                        },
                                    new DocumentDTO
                                        {
                                            Creator = "陈春勇",
                                            DocumentId = Guid.Parse("1124F3CC-52C8-4FC0-9A03-7088F027A5CA"),
                                            ExtendType = "docx",
                                            FolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                                            Name = "引进2013年飞机计划2.doc"
                                        },
                                    new DocumentDTO
                                        {
                                            Creator = "陈春勇",
                                            DocumentId = Guid.Parse("9338E0C6-7C61-4785-9E14-D25C7507B780"),
                                            ExtendType = "xlsx",
                                            FolderId = Guid.Parse("F4107DB3-20BA-4D5C-ABDE-D330CA14D111"),
                                            Name = "引进2013年飞机计划3.doc"
                                        }
                                },
                        },
                };
            return folders.AsQueryable();
        }
    }
}