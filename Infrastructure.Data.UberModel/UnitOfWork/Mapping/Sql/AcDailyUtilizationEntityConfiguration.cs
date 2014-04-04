#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:57

// 文件名：AcDailyUtilizationEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AcDailyUtilizationAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AcDailyUtilization实体相关配置
    /// </summary>
    internal class AcDailyUtilizationEntityConfiguration : EntityTypeConfiguration<AcDailyUtilization>
    {
        public AcDailyUtilizationEntityConfiguration()
        {
            ToTable("AcDailyUtilization", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.RegNumber).HasColumnName("RegNumber").HasMaxLength(100);
            Property(p => p.CalculatedHour).HasColumnName("CalculatedHour").HasPrecision(16, 4);
            Property(p => p.CalculatedCycle).HasColumnName("CalculatedCycle").HasPrecision(16, 4);
            Property(p => p.AmendHour).HasColumnName("AmendHour").HasPrecision(16, 4);
            Property(p => p.AmendCycle).HasColumnName("AmendCycle").HasPrecision(16, 4);
            Property(p => p.Year).HasColumnName("Year");
            Property(p => p.Month).HasColumnName("Month");
            Property(p => p.IsCurrent).HasColumnName("IsCurrent");
            Property(p => p.AircraftId).HasColumnName("AircraftId");
        }
    }
}