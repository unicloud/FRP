#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 10:25:49
// 文件名：AtaEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 10:25:49
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AtaAgg;

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Ata实体相关配置
    /// </summary>
    internal class AtaEntityConfiguration : EntityTypeConfiguration<Ata>
    {
        public AtaEntityConfiguration()
        {
            ToTable("Ata", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.ATA).HasColumnName("ATA");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.ParentId).HasColumnName("ParentId");
            Property(p => p.AircraftSeriesId).HasColumnName("AircraftSeriesId");

            HasRequired(o => o.ParentAta).WithMany().HasForeignKey(o => o.ParentId);
            HasRequired(o => o.AircraftSeries).WithMany().HasForeignKey(o => o.AircraftSeriesId);
            HasMany(o => o.ChildAtas).WithRequired().HasForeignKey(o => o.ParentId);

        }
    }
}
