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
    ///     Project实体相关配置
    /// </summary>
    internal class ProjectEntityConfiguration : EntityTypeConfiguration<Project>
    {
        public ProjectEntityConfiguration()
        {
            ToTable("Project", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.PlannedStart).HasColumnName("PlannedStart").HasColumnType("datetime2");
            Property(p => p.PlannedEnd).HasColumnName("PlannedEnd").HasColumnType("datetime2");
            Property(p => p.Status).HasColumnName("Status");

            HasMany(p => p.Tasks).WithRequired().HasForeignKey(p => p.ProjectId);
        }
    }
}