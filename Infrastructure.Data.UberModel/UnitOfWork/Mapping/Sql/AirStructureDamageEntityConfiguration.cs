#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 13:44:12
// 文件名：AirStructureDamageEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 13:44:12
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AirStructureDamageAgg;

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// Scn实体相关配置
    /// </summary>
    internal class AirStructureDamageEntityConfiguration : EntityTypeConfiguration<AirStructureDamage>
    {
        public AirStructureDamageEntityConfiguration()
        {
            ToTable("AirStructureDamage", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.AircraftReg).HasColumnName("AircraftReg");
            Property(p => p.AircraftSeries).HasColumnName("AircraftSeries");
            Property(p => p.AircraftType).HasColumnName("AircraftType");
            Property(p => p.TotalCost).HasColumnName("TotalCost").HasColumnType("decimal").HasPrecision(16, 4);
            Property(p => p.CloseDate).HasColumnName("CloseDate").HasColumnType("datetime2");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.DocumentId).HasColumnName("DocumentId");
            Property(p => p.DocumentName).HasColumnName("DocumentName");
            Property(p => p.IsDefer).HasColumnName("IsDefer");
            Property(p => p.Level).HasColumnName("Level");
            Property(p => p.RepairDeadline).HasColumnName("RepairDeadline");
            Property(p => p.ReportDate).HasColumnName("AuditTime").HasColumnType("datetime2");
            Property(p => p.ReportNo).HasColumnName("ReportNo");
            Property(p => p.ReportType).HasColumnName("ReportType");
            Property(p => p.Source).HasColumnName("Source");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.TecAssess).HasColumnName("TecAssess");
            Property(p => p.TreatResult).HasColumnName("TreatResult");

            HasRequired(i => i.Aircraft).WithMany().HasForeignKey(i => i.AircraftId);
        }

    }
}
