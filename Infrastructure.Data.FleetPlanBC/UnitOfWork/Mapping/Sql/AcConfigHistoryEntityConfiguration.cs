#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 13:55:20
// 文件名：AcConfigHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AcConfigHistory实体相关配置
    /// </summary>
    internal class AcConfigHistoryEntityConfiguration : EntityTypeConfiguration<AcConfigHistory>
    {
        public AcConfigHistoryEntityConfiguration()
        {
            ToTable("AcConfigHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");

            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.AircraftConfigurationId).HasColumnName("AircraftConfigurationId");
        }
    }
}