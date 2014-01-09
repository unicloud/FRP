#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，17:39
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
using UniCloud.Domain.ProjectBC.Aggregates.WorkGroupAgg;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     WorkGroup实体相关配置
    /// </summary>
    internal class WorkGroupEntityConfiguration : EntityTypeConfiguration<WorkGroup>
    {
        public WorkGroupEntityConfiguration()
        {
            ToTable("WorkGroup", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("Name");

            Property(p => p.ManagerId).HasColumnName("ManagerId");

            HasRequired(w => w.Manager).WithMany().HasForeignKey(w => w.ManagerId);
            HasMany(w => w.Members).WithRequired().HasForeignKey(w => w.WorkGroupId);
        }
    }
}