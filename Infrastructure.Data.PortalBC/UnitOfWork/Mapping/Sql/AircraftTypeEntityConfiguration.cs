﻿#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/06/21，10:06
// 方案：FRP
// 项目：Infrastructure.Data.PortalBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PortalBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PortalBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftType实体相关配置
    /// </summary>
    internal class AircraftTypeEntityConfiguration : EntityTypeConfiguration<AircraftType>
    {
        public AircraftTypeEntityConfiguration()
        {
            ToTable("AircraftType", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");

            Property(p => p.CaacAircraftTypeId).HasColumnName("CaacAircraftTypeId");
            Property(p => p.AircraftSeriesId).HasColumnName("AircraftSeriesId");
            Property(p => p.AircraftCategoryId).HasColumnName("AircraftCategoryId");
            Property(p => p.ManufacturerId).HasColumnName("ManufacturerId");

            HasRequired(o => o.CaacAircraftType).WithMany().HasForeignKey(o => o.CaacAircraftTypeId);
            HasRequired(o => o.Manufacturer).WithMany().HasForeignKey(o => o.ManufacturerId);
            HasRequired(o => o.AircraftSeries).WithMany().HasForeignKey(o => o.AircraftSeriesId);
            HasRequired(o => o.AircraftCategory).WithMany().HasForeignKey(o => o.AircraftCategoryId);
        }
    }
}