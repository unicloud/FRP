﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/15 22:46:13
// 文件名：SnRemInstRecordEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.SnRemInstRecordAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SnRemInstRecord实体相关配置
    /// </summary>
    internal class SnRemInstRecordEntityConfiguration : EntityTypeConfiguration<SnRemInstRecord>
    {
        public SnRemInstRecordEntityConfiguration()
        {
            ToTable("SnRemInstRecord", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.ActionNo).HasColumnName("ActionNo").HasMaxLength(100);
            Property(p => p.ActionDate).HasColumnName("ActionDate").HasColumnType("datetime2");
            Property(p => p.ActionType).HasColumnName("ActionType");
            Property(p => p.Reason).HasColumnName("Reason").HasMaxLength(100);
            Property(p => p.AircraftId).HasColumnName("AircraftId");
        }
    }
}