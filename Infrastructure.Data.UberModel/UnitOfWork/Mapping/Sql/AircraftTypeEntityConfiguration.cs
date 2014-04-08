#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
// 文件名：AircraftTypeEntityConfiguration.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
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

            Property(p => p.AircraftSeriesId).HasColumnName("AircraftSeriesId");
            Property(p => p.AircraftCategoryId).HasColumnName("AircraftCategoryId");
            Property(p => p.ManufacturerId).HasColumnName("ManufacturerId");
            Property(p => p.CaacAircraftTypeId).HasColumnName("CaacAircraftTypeId");

            HasRequired(o => o.CaacAircraftType).WithMany().HasForeignKey(o => o.CaacAircraftTypeId);
            HasRequired(o => o.Manufacturer).WithMany().HasForeignKey(o => o.ManufacturerId);
            HasRequired(o => o.AircraftSeries).WithMany().HasForeignKey(o => o.AircraftSeriesId);
            HasRequired(o => o.AircraftCategory).WithMany().HasForeignKey(o => o.AircraftCategoryId);

        }
    }
}