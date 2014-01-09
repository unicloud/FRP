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
using UniCloud.Domain.UberModel.Aggregates.TaskStandardAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     TaskStandard实体相关配置
    /// </summary>
    internal class TaskStandardEntityConfiguration : EntityTypeConfiguration<TaskStandard>
    {
        public TaskStandardEntityConfiguration()
        {
            ToTable("TaskStandard", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.OptimisticTime).HasColumnName("OptimisticTime");
            Property(p => p.PessimisticTime).HasColumnName("PessimisticTime");
            Property(p => p.NormalTime).HasColumnName("NormalTime");
            Property(p => p.SourceGuid).HasColumnName("SourceGuid");
            Property(p => p.IsCustom).HasColumnName("IsCustom");
            Property(p => p.TaskType).HasColumnName("TaskType");

            Property(p => p.WorkGroupId).HasColumnName("WorkGroupId");

            HasRequired(t => t.WorkGroup).WithMany().HasForeignKey(t => t.WorkGroupId);
            HasMany(t => t.TaskCases).WithRequired().HasForeignKey(t => t.TaskStandardId);
        }
    }
}