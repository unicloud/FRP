#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/29 11:17:32
// 文件名：PlanHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AircraftPlanHistoryAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     PlanHistory实体相关配置
    /// </summary>
    internal class PlanHistoryEntityConfiguration : EntityTypeConfiguration<PlanHistory>
    {
        public PlanHistoryEntityConfiguration()
        {
            ToTable("PlanHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.SeatingCapacity).HasColumnName("SeatingCapacity");
            Property(p => p.CarryingCapacity).HasColumnName("CarryingCapacity");
            Property(p => p.PerformAnnualId).HasColumnName("PerformAnnualId");
            Property(p => p.PerformMonth).HasColumnName("PerformMonth");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.IsSubmit).HasColumnName("IsSubmit");
            Property(p => p.Note).HasColumnName("Note");
            Property(p => p.CanRequest).HasColumnName("CanRequest");
            Property(p => p.CanDeliver).HasColumnName("CanDeliver");

            Property(p => p.PlanAircraftId).HasColumnName("PlanAircraftId");
            Property(p => p.PlanId).HasColumnName("PlanId");
            Property(p => p.ActionCategoryId).HasColumnName("ActionCategoryId");
            Property(p => p.TargetCategoryId).HasColumnName("TargetCategoryId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.CaacAircraftTypeId).HasColumnName("CaacAircraftTypeId");
            Property(p => p.AirlinesId).HasColumnName("AirlinesId");
            Property(p => p.ApprovalHistoryId).HasColumnName("ApprovalHistoryId");

            HasOptional(o => o.PlanAircraft).WithMany().HasForeignKey(o => o.PlanAircraftId);
            HasRequired(o => o.ActionCategory).WithMany().HasForeignKey(o => o.ActionCategoryId);
            HasRequired(o => o.TargetCategory).WithMany().HasForeignKey(o => o.TargetCategoryId);
            HasRequired(o => o.AircraftType).WithMany().HasForeignKey(o => o.AircraftTypeId);
            HasRequired(o => o.CaacAircraftType).WithMany().HasForeignKey(o => o.CaacAircraftTypeId);
            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);
            HasRequired(o => o.PerformAnnual).WithMany().HasForeignKey(o => o.PerformAnnualId);

        }
    }
}
