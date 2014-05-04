#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/30 9:57:43
// 文件名：MaintainCreditNoteInvoiceEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/30 9:57:43
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     CreditNoteInvoice实体相关配置
    /// </summary>
    internal class MaintainCreditNoteInvoiceEntityConfiguration : EntityTypeConfiguration<MaintainCreditNoteInvoice>
    {
        public MaintainCreditNoteInvoiceEntityConfiguration()
        {
            ToTable("MaintainCreditNoteInvoice", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
