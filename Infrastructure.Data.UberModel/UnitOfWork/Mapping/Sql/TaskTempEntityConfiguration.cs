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
using UniCloud.Domain.UberModel.Aggregates.ProjectTempAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     TaskTemp实体相关配置
    /// </summary>
    internal class TaskTempEntityConfiguration : EntityTypeConfiguration<TaskTemp>
    {
        public TaskTempEntityConfiguration()
        {
            ToTable("TaskTemp", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Subject).HasColumnName("Subject");
            Property(p => p.Start).HasColumnName("Start");
            Property(p => p.End).HasColumnName("End");
            Property(p => p.IsMileStone).HasColumnName("IsMileStone");
            Property(p => p.IsSummary).HasColumnName("IsSummary");

            Property(p => p.TaskStandardId).HasColumnName("TaskStandardId");
            Property(p => p.ParentId).HasColumnName("ParentId");
            Property(p => p.ProjectTempId).HasColumnName("ProjectTempId");

            HasOptional(t => t.Parent).WithMany(t => t.Children).HasForeignKey(t => t.ParentId);
        }
    }
}