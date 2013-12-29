#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:46:46
// 文件名：ApprovalHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     ApprovalHistory实体相关配置
    /// </summary>
    internal class ApprovalHistoryEntityConfiguration : EntityTypeConfiguration<ApprovalHistory>
    {
        public ApprovalHistoryEntityConfiguration()
        {
            ToTable("ApprovalHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.SeatingCapacity).HasColumnName("SeatingCapacity");
            Property(p => p.CarryingCapacity).HasColumnName("CarryingCapacity");
            Property(p => p.RequestDeliverAnnualId).HasColumnName("RequestDeliverAnnualId");
            Property(p => p.RequestDeliverMonth).HasColumnName("RequestDeliverMonth");
            Property(p => p.IsApproved).HasColumnName("IsApproved");
            Property(p => p.Note).HasColumnName("Note");

            Property(p => p.RequestId).HasColumnName("RequestId");
            Property(p => p.PlanAircraftId).HasColumnName("PlanAircraftId");
            Property(p => p.ImportCategoryId).HasColumnName("ImportCategoryId");
            Property(p => p.AirlinesId).HasColumnName("AirlinesId");

            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);
            HasRequired(o => o.PlanAircraft).WithMany().HasForeignKey(o => o.PlanAircraftId);
            HasRequired(o => o.ImportCategory).WithMany().HasForeignKey(o => o.ImportCategoryId);
            HasRequired(o => o.RequestDeliverAnnual).WithMany().HasForeignKey(o => o.RequestDeliverAnnualId);

        }
    }
}
