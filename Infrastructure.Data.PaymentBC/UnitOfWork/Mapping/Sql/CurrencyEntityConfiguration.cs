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
using UniCloud.Domain.PaymentBC.Aggregates.CurrencyAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Currency实体相关配置
    /// </summary>
    internal class CurrencyEntityConfiguration : EntityTypeConfiguration<Currency>
    {
        public CurrencyEntityConfiguration()
        {
            ToTable("Currency", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.CnName).HasColumnName("CnName");
            Property(p => p.EnName).HasColumnName("EnName");
            Property(p => p.Symbol).HasColumnName("Symbol");
            Property(p => p.ExchangeRate).HasColumnName("ExchangeRate");
        }
    }
}