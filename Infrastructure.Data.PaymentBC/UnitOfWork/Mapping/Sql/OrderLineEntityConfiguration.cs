﻿#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，14:12
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     OrderLine实体相关配置
    /// </summary>
    internal class OrderLineEntityConfiguration : EntityTypeConfiguration<OrderLine>
    {
        public OrderLineEntityConfiguration()
        {
            ToTable("OrderLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.UnitPrice).HasColumnName("UnitPrice");
            Property(p => p.Amount).HasColumnName("Amount");
            Property(p => p.Discount).HasColumnName("Discount");
            Property(p => p.EstimateDeliveryDate).HasColumnName("EstimateDeliveryDate").HasColumnType("datetime2");
            Property(p => p.IsCompleted).HasColumnName("IsCompleted");
            Property(p => p.Note).HasColumnName("Note");
            Ignore(p => p.TotalLine);

            Property(p => p.OrderId).HasColumnName("OrderId");
        }
    }
}