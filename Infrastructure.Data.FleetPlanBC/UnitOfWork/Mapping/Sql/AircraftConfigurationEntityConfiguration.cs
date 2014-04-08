#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 15:39:45
// 文件名：AircraftConfigurationEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftConfigurationAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
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
            Property(p => p.Description).HasColumnName("Description");
        }
    }
}
