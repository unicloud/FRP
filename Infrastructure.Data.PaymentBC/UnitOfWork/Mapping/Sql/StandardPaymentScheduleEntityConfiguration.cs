﻿#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，16:59
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
using UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     StandardPaymentSchedule实体相关配置
    /// </summary>
    internal class StandardPaymentScheduleEntityConfiguration : EntityTypeConfiguration<StandardPaymentSchedule>
    {
        public StandardPaymentScheduleEntityConfiguration()
        {
            ToTable("StandardPaymentSchedule", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.OrderId).HasColumnName("OrderId");
        }
    }
}