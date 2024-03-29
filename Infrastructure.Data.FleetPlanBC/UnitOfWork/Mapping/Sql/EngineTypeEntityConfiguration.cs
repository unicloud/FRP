﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:44:22
// 文件名：EngineTypeEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     EngineType实体相关配置
    /// </summary>
    internal class EngineTypeEntityConfiguration : EntityTypeConfiguration<EngineType>
    {
        public EngineTypeEntityConfiguration()
        {
            ToTable("EngineType", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Name).HasColumnName("Name");

            Property(p => p.ManufacturerId).HasColumnName("ManufacturerId");

            HasRequired(o => o.Manufacturer).WithMany().HasForeignKey(o => o.ManufacturerId);

        }
    }
}
