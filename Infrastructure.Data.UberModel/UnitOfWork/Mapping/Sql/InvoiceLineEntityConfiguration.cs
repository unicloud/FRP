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
using UniCloud.Domain.UberModel.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     InvoiceLine实体相关配置
    /// </summary>
    internal class InvoiceLineEntityConfiguration : EntityTypeConfiguration<InvoiceLine>
    {
        public InvoiceLineEntityConfiguration()
        {
            ToTable("InvoiceLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            
            Property(p => p.ItemName).HasColumnName("ItemName");
            Property(p => p.Amount).HasColumnName("Amount");
            Property(p => p.Note).HasColumnName("Note");
        }
    }
}