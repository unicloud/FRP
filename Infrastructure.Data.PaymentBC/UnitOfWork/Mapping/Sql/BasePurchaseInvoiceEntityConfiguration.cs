#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/28 15:07:29
// 文件名：BasePurchaseInvoiceEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/28 15:07:29
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
    ///     BasePurchaseInvoice实体相关配置
    /// </summary>
    internal class BasePurchaseInvoiceEntityConfiguration : EntityTypeConfiguration<BasePurchaseInvoice>
    {
        public BasePurchaseInvoiceEntityConfiguration()
        {
            ToTable("BasePurchaseInvoice", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            HasMany(i => i.InvoiceLines).WithRequired().HasForeignKey(i => i.InvoiceId);
        }
    }
}
