#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:44:10
// 文件名：EnginePlanHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     EnginePlanHistory实体相关配置
    /// </summary>
    internal class EnginePlanHistoryEntityConfiguration : EntityTypeConfiguration<EnginePlanHistory>
    {
        public EnginePlanHistoryEntityConfiguration()
        {
            ToTable("EnginePlanHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.PerformAnnualId).HasColumnName("PerformAnnualId");
            Property(p => p.PerformMonth).HasColumnName("PerformMonth");
            Property(p => p.MaxThrust).HasColumnName("MaxThrust");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.IsFinished).HasColumnName("IsFinished");

            Property(p => p.EngineTypeId).HasColumnName("EngineTypeId");
            Property(p => p.EnginePlanId).HasColumnName("EnginePlanId");
            Property(p => p.PlanEngineId).HasColumnName("PlanEngineId");
            Property(p => p.ActionCategoryId).HasColumnName("ActionCategoryId");

            HasRequired(o => o.EngineType).WithMany().HasForeignKey(o => o.EngineTypeId);
            HasRequired(o => o.PerformAnnual).WithMany().HasForeignKey(o => o.PerformAnnualId);
            HasOptional(o => o.PlanEngine).WithMany().HasForeignKey(o => o.PlanEngineId);
            HasRequired(o => o.ActionCategory).WithMany().HasForeignKey(o => o.ActionCategoryId);

        }
    }
}
