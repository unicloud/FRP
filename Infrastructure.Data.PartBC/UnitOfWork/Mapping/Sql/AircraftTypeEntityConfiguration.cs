#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AircraftTypeEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// AircraftType实体相关配置
    /// </summary>
    internal class AircraftTypeEntityConfiguration : EntityTypeConfiguration<AircraftType>
    {
        public AircraftTypeEntityConfiguration()
        {
            ToTable("AircraftType", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(p => p.Name).HasColumnName("Name").HasMaxLength(100);
            Property(p => p.AircraftSeriesId).HasColumnName("AircraftSeriesId");

            HasRequired(o => o.AircraftSeries).WithMany().HasForeignKey(o => o.AircraftSeriesId);
        }

    }
}
