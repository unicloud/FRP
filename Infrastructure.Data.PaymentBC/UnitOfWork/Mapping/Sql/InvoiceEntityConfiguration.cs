﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，14:58
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Invoice实体相关配置
    /// </summary>
    internal class InvoiceEntityConfiguration : EntityTypeConfiguration<Invoice>
    {
        public InvoiceEntityConfiguration()
        {
            ToTable("Invoice", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.InvoiceType).HasColumnName("InvoiceType");
            Property(p => p.InvoiceNumber).HasColumnName("InvoiceNumber");
            Property(p => p.InvoideCode).HasColumnName("InvoideCode");
            Property(p => p.InvoiceDate).HasColumnName("InvoiceDate").HasColumnType("datetime2");
            Property(p => p.SupplierName).HasColumnName("SupplierName");
            Property(p => p.InvoiceValue).HasColumnName("InvoiceValue");
            Property(p => p.PaidAmount).HasColumnName("PaidAmount");
            Property(p => p.OperatorName).HasColumnName("OperatorName");
            Property(p => p.Reviewer).HasColumnName("Reviewer");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.ReviewDate).HasColumnName("ReviewDate").HasColumnType("datetime2");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.IsCompleted).HasColumnName("IsCompleted");
            Property(p => p.Status).HasColumnName("Status");

            Property(p => p.OrderId).HasColumnName("OrderId");
            Property(p => p.SupplierId).HasColumnName("SupplierId");
            Property(p => p.CurrencyId).HasColumnName("CurrencyId");
            Property(p => p.PaymentScheduleLineId).HasColumnName("PaymentScheduleLineId");

            HasRequired(i => i.Supplier).WithMany().HasForeignKey(i => i.SupplierId);
            HasRequired(i => i.Currency).WithMany().HasForeignKey(i => i.CurrencyId);
            HasOptional(i => i.PaymentScheduleLine).WithMany().HasForeignKey(i => i.PaymentScheduleLineId);
        }
    }
}