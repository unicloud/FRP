#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/9 9:24:07
// 文件名：AircraftMaintainPlanDetailEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/9 9:24:07
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftMaintainPlanDetail实体相关配置
    /// </summary>
    internal class AircraftMaintainPlanDetailEntityConfiguration : EntityTypeConfiguration<AircraftMaintainPlanDetail>
    {
        public AircraftMaintainPlanDetailEntityConfiguration()
        {
            ToTable("AircraftMaintainPlanDetail", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.AircraftNumber).HasColumnName("AircraftNumber");
            Property(p => p.AircraftType).HasColumnName("AircraftType");
            Property(p => p.Level).HasColumnName("Level");
            Property(p => p.InDate).HasColumnName("InDate").HasColumnType("datetime2");
            Property(p => p.OutDate).HasColumnName("OutDate").HasColumnType("datetime2");
        }
    }
}
