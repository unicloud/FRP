﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/03/01，21:11
// 方案：FRP
// 项目：Infrastructure.Data.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.ThrustAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Thrust实体相关配置
    /// </summary>
    internal class ThrustEntityConfiguration : EntityTypeConfiguration<Thrust>
    {
        public ThrustEntityConfiguration()
        {
            ToTable("Thrust", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");
        }
    }
}