﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，17:37
// 方案：FRP
// 项目：Infrastructure.Data.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.ProjectBC.Aggregates.TaskStandardAgg;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     TaskCase实体相关配置
    /// </summary>
    internal class TaskCaseEntityConfiguration : EntityTypeConfiguration<TaskCase>
    {
        public TaskCaseEntityConfiguration()
        {
            ToTable("TaskCase", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Description).HasColumnName("Description");

            Property(p => p.TaskStandardId).HasColumnName("TaskStandardId");
            Property(p => p.RelatedId).HasColumnName("RelatedId");
        }
    }
}