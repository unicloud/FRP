#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/11/22，16:11
// 文件名：ActionCategoryData.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using UniCloud.Domain.UberModel.Aggregates.ActionCategoryAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class ActionCategoryData : InitialDataBase
    {
        public ActionCategoryData(UberModelUnitOfWork context) : base(context)
        {
        }

        public override void InitialData()
        {
            var actionCategories = new List<ActionCategory>
            {
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("8C58622E-01E3-4F61-B34D-619D3FB432AF"),
                    "引进", "购买",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("7228CC67-572F-472B-9F0A-B7BBDCA7BEE1"),
                    "引进", "融资租赁",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("4B69B41A-D528-4591-9409-7BBEBC283A0C"),
                    "引进", "经营租赁",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("297BD0FE-229B-41FE-95F6-2E8FDD4FB591"),
                    "引进", "湿租",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("EBDF8AC3-7C5C-42C4-9913-D11FDA4128AD"),
                    "引进", "经营租赁续租",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("CAA1009D-93BA-458C-AD9E-39B13884FDF0"),
                    "引进", "湿租续租",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("A02C29D9-5B94-4FEF-BE75-022EA65BFAE5"),
                    "退出", "退役",false),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("5340C3A7-E17C-4683-99C4-45AE9D931EA0"),
                    "退出", "出售",false),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("BC429864-AFA3-43C4-9ADD-C5CA1BD9E409"),
                    "退出", "退租",false),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("3005491F-F9AB-47A8-8305-267B42C69930"),
                    "变更", "一般改装",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("8A7C3384-C6C1-47B8-A719-05BBB05523BB"),
                    "变更", "客改货",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("BEDC8356-BFF7-46E4-9753-5C66F580D325"),
                    "变更", "货改客",true),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("D7C68663-7F77-4DF3-B953-76DE32035F5D"),
                    "变更", "售后融资租赁",false),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("3260D2F8-5160-494B-99E3-0E4F2EDD9672"),
                    "变更", "售后经营租赁",false),
                ActionCategoryFactory.CreateActionCategory(Guid.Parse("F8FC06A0-75B4-4FCC-A7F5-1592315E6794"),
                    "变更", "租转购",false),
            };
            actionCategories.ForEach(a => Context.ActionCategories.AddOrUpdate(u => u.Id, a));
        }
    }
}