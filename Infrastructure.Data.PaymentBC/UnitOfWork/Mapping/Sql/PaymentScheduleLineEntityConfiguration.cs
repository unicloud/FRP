#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，16:50
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
    ///     PaymentScheduleLine实体相关配置
    /// </summary>
    internal class PaymentScheduleLineEntityConfiguration : EntityTypeConfiguration<PaymentScheduleLine>
    {
        public PaymentScheduleLineEntityConfiguration()
        {
            ToTable("PaymentScheduleLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ScheduleDate).HasColumnName("ScheduleDate").HasColumnType("datetime2");
            Property(p => p.Amount).HasColumnName("Amount");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.Note).HasColumnName("Note");

            Property(p => p.PaymentScheduleId).HasColumnName("PaymentScheduleId");
            Property(p => p.InvoiceId).HasColumnName("InvoiceId");

            HasOptional(p => p.Invoice).WithMany().HasForeignKey(p => p.InvoiceId);
        }
    }
}