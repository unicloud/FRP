﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，17:12
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.PaymentScheduleAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     PaymentSchedule实体相关配置
    /// </summary>
    internal class PaymentScheduleEntityConfiguration : EntityTypeConfiguration<PaymentSchedule>
    {
        public PaymentScheduleEntityConfiguration()
        {
            ToTable("PaymentSchedule", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.SupplierName).HasColumnName("SupplierName");
            Property(p => p.IsCompleted).HasColumnName("IsCompleted");

            Property(p => p.CurrencyId).HasColumnName("CurrencyId");
            Property(p => p.SupplierId).HasColumnName("SupplierId");

            HasRequired(p => p.Currency).WithMany().HasForeignKey(p => p.CurrencyId);
            HasRequired(p => p.Supplier).WithMany().HasForeignKey(p => p.SupplierId);
            HasMany(p => p.PaymentScheduleLines).WithRequired().HasForeignKey(p => p.PaymentScheduleId);
        }
    }
}