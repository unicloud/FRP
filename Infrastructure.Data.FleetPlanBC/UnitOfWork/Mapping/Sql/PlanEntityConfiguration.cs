#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:34:29
// 文件名：PlanEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Plan实体相关配置
    /// </summary>
    internal class PlanEntityConfiguration : EntityTypeConfiguration<Plan>
    {
        public PlanEntityConfiguration()
        {
            ToTable("Plan", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Title).HasColumnName("Title");
            Property(p => p.VersionNumber).HasColumnName("VersionNumber");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.IsCurrentVersion).HasColumnName("IsCurrentVersion");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.SubmitDate).HasColumnName("SubmitDate").HasColumnType("datetime2");
            Property(p => p.DocNumber).HasColumnName("DocNumber");
            Property(p => p.IsFinished).HasColumnName("IsFinished");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.PublishStatus).HasColumnName("PublishStatus");

            Property(p => p.AirlinesId).HasColumnName("AirlinesId");
            Property(p => p.AnnualId).HasColumnName("AnnualId");
            Property(p => p.DocumentId).HasColumnName("DocumentId");

            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);
            HasRequired(o => o.Annual).WithMany().HasForeignKey(o => o.AnnualId);
            HasMany(o => o.PlanHistories).WithRequired().HasForeignKey(o => o.PlanId);

        }
    }
}
