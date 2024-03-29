﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/04，14:11
// 文件名：AircraftEntityConfiguration.cs
// 程序集：UniCloud.Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Aircraft实体相关配置
    /// </summary>
    internal class AircraftEntityConfiguration : EntityTypeConfiguration<Aircraft>
    {
        public AircraftEntityConfiguration()
        {
            ToTable("Aircraft", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.RegNumber).HasColumnName("RegNumber");

            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");

            HasRequired(a => a.AircraftType).WithMany(a => a.Aircrafts).HasForeignKey(a => a.AircraftTypeId);
        }
    }
}