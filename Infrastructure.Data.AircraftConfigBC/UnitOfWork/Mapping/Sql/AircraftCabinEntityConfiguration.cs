#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 13:45:16
// 文件名：AircraftCabinEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 13:45:16
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg;

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftCabin实体相关配置
    /// </summary>
    internal class AircraftCabinEntityConfiguration : EntityTypeConfiguration<AircraftCabin>
    {
        public AircraftCabinEntityConfiguration()
        {
            ToTable("AircraftCabin", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.AircraftCabinTypeId).HasColumnName("AircraftCabinTypeId");
            Property(p => p.SeatNumber).HasColumnName("SeatNumber");
            Property(p => p.Note).HasColumnName("Note");

            HasRequired(o => o.AircraftCabinType).WithMany().HasForeignKey(o => o.AircraftCabinTypeId);
        }
    }
}
