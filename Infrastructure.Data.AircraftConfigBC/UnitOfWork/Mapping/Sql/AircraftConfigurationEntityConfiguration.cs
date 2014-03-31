#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 13:37:17
// 文件名：AircraftConfigurationEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 13:37:17
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg;

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftConfiguration实体相关配置
    /// </summary>
    internal class AircraftConfigurationEntityConfiguration : EntityTypeConfiguration<AircraftConfiguration>
    {
        public AircraftConfigurationEntityConfiguration()
        {
            ToTable("AircraftConfiguration", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ConfigCode).HasColumnName("ConfigCode");
            Property(p => p.AircraftSeriesId).HasColumnName("AircraftSeriesId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.BEW).HasColumnName("BEW");
            Property(p => p.BW).HasColumnName("BW");
            Property(p => p.BWF).HasColumnName("BWF");
            Property(p => p.BWI).HasColumnName("BWI");
            Property(p => p.MCC).HasColumnName("MCC");
            Property(p => p.MLW).HasColumnName("MLW");
            Property(p => p.MMFW).HasColumnName("MMFW");
            Property(p => p.MTOW).HasColumnName("MTOW");
            Property(p => p.MTW).HasColumnName("MTW");
            Property(p => p.MZFW).HasColumnName("MZFW");
            Property(p => p.FileName).HasColumnName("FileName");
            Property(p => p.FileContent).HasColumnName("FileContent");
            Property(p => p.Description).HasColumnName("Description");

            HasRequired(o => o.AircraftType).WithMany().HasForeignKey(o => o.AircraftTypeId);
            HasRequired(o => o.AircraftSeries).WithMany().HasForeignKey(o => o.AircraftSeriesId);
            HasMany(p => p.AircraftCabins).WithRequired().HasForeignKey(p => p.AircraftConfiguratonId);
        }
    }
}
