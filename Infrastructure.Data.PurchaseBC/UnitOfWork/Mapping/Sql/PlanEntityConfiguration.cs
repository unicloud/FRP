#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：1:02
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftPlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Plan实体相关配置
    /// </summary>
    internal class PlanEntityConfiguration : EntityTypeConfiguration<Plan>
    {
        public PlanEntityConfiguration()
        {
            ToTable("AircraftPlan", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Title).HasColumnName("Title");
            Property(p => p.VersionNumber).HasColumnName("VersionNumber");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.IsCurrentVersion).HasColumnName("IsCurrentVersion");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.SubmitDate).HasColumnName("SubmitDate").HasColumnType("datetime2");
            Property(p => p.IsFinished).HasColumnName("IsFinished");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.PublishStatus).HasColumnName("PublishStatus");

            Property(p => p.AnnualId).HasColumnName("AnnualId");

            HasRequired(o => o.Annual).WithMany().HasForeignKey(o => o.AnnualId);
        }
    }
}