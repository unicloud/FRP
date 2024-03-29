﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/25 13:58:37
// 文件名：CAACAircraftTypeEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.FleetPlanBC.Aggregates.CAACAircraftTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     CAACAircraftType实体相关配置
    /// </summary>
    internal class CAACAircraftTypeEntityConfiguration : EntityTypeConfiguration<CAACAircraftType>
    {
        public CAACAircraftTypeEntityConfiguration()
        {
            ToTable("CAACAircraftType", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.AircraftCategoryId).HasColumnName("AircraftCategoryId");
            Property(p => p.ManufacturerId).HasColumnName("ManufacturerId");

            HasRequired(o => o.Manufacturer).WithMany().HasForeignKey(o => o.ManufacturerId);
            HasRequired(o => o.AircraftCategory).WithMany().HasForeignKey(o => o.AircraftCategoryId);
        }
    }
}
