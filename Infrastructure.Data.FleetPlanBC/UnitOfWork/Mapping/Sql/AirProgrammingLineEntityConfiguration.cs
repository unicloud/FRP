﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:36:12
// 文件名：AirProgrammingLineEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Infrastructure.Data;

#endregion

namespace UniCloud.Domain.FleetPlanBC.Aggregates.AirProgrammingAgg
{
    /// <summary>
    ///     AirProgrammingLine实体相关配置
    /// </summary>
    internal class AirProgrammingLineEntityConfiguration : EntityTypeConfiguration<AirProgrammingLine>
    {
        public AirProgrammingLineEntityConfiguration()
        {
            ToTable("AirProgrammingLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Year).HasColumnName("Year");
            Property(p => p.BuyNum).HasColumnName("BuyNum");
            Property(p => p.ExportNum).HasColumnName("ExportNum");
            Property(p => p.LeaseNum).HasColumnName("LeaseNum");

            Property(p => p.AircraftSeriesId).HasColumnName("AircraftSeriesId");
            Property(p => p.AircraftCategoryId).HasColumnName("AircraftCategoryId");
            Property(p => p.AirProgrammingId).HasColumnName("AirProgrammingId");

            HasRequired(o => o.AircraftSeries).WithMany().HasForeignKey(o => o.AircraftSeriesId);
            HasRequired(o => o.AircraftCategory).WithMany().HasForeignKey(o => o.AircraftCategoryId);

        }
    }
}
