#region 版本信息

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
    ///     PaymentScheduleLine实体相关配置
    /// </summary>
    internal class PaymentScheduleLineEntityConfiguration : EntityTypeConfiguration<PaymentScheduleLine>
    {
        public PaymentScheduleLineEntityConfiguration()
        {
            ToTable("PaymentScheduleLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Subject).HasColumnName("Subject");
            Property(p => p.Body).HasColumnName("Body");
            Property(p => p.Importance).HasColumnName("Importance");
            Property(p => p.Tempo).HasColumnName("Tempo");
            Property(p => p.Start).HasColumnName("Start").HasColumnType("datetime2");
            Property(p => p.End).HasColumnName("End").HasColumnType("datetime2");
            Property(p => p.IsAllDayEvent).HasColumnName("IsAllDayEvent");
            Property(p => p.ScheduleDate).HasColumnName("ScheduleDate").HasColumnType("datetime2");
            Property(p => p.Amount).HasColumnName("Amount");
            Property(p => p.TaskStatus).HasColumnName("TaskStatus");
            Property(p => p.Status).HasColumnName("Status");

            Property(p => p.PaymentScheduleId).HasColumnName("PaymentScheduleId");
            Property(p => p.InvoiceId).HasColumnName("InvoiceId");

            HasOptional(p => p.Invoice).WithMany().HasForeignKey(p => p.InvoiceId);
        }
    }
}