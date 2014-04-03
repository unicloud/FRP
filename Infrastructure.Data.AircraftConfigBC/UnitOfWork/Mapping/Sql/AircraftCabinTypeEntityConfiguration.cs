#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 21:33:47
// 文件名：AircraftCabinTypeEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 21:33:47
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftCabinTypeAgg;

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftCabin实体相关配置
    /// </summary>
    internal class AircraftCabinTypeEntityConfiguration : EntityTypeConfiguration<AircraftCabinType>
    {
        public AircraftCabinTypeEntityConfiguration()
        {
            ToTable("AircraftCabinType", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Note).HasColumnName("Note");

        }
    }
}
