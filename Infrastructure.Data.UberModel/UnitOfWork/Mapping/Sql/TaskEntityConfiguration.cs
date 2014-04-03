#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，18:01
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.ProjectAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Task实体相关配置
    /// </summary>
    internal class TaskEntityConfiguration : EntityTypeConfiguration<Task>
    {
        public TaskEntityConfiguration()
        {
            ToTable("Task", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Subject).HasColumnName("Subject");
            Property(p => p.Body).HasColumnName("Body");
            Property(p => p.Importance).HasColumnName("Importance");
            Property(p => p.Tempo).HasColumnName("Tempo");
            Property(p => p.Start).HasColumnName("Start").HasColumnType("datetime2");
            Property(p => p.End).HasColumnName("End").HasColumnType("datetime2");
            Property(p => p.IsAllDayEvent).HasColumnName("IsAllDayEvent");
            Property(p => p.IsCompleted).HasColumnName("IsCompleted");
            Property(p => p.TaskStatus).HasColumnName("TaskStatus");
            Property(p => p.Duration).HasColumnName("Duration");
            Property(p => p.DeadLine).HasColumnName("DeadLine").HasColumnType("datetime2");
            Property(p => p.IsMileStone).HasColumnName("IsMileStone");
            Property(p => p.IsSummary).HasColumnName("IsSummary");
            Property(p => p.HasRisk).HasColumnName("HasRisk");
            Property(p => p.TimeZoneId).HasColumnName("TimeZoneId");
            Property(p => p.Note).HasColumnName("Note");

            Property(p => p.ProjectId).HasColumnName("ProjectId");
            Property(p => p.TaskStandardId).HasColumnName("TaskStandardId");
            Property(p => p.RelatedId).HasColumnName("RelatedId");
            Property(p => p.ParentId).HasColumnName("ParentId");

            HasOptional(t => t.Parent).WithMany(t => t.Children).HasForeignKey(t => t.ParentId);
        }
    }
}