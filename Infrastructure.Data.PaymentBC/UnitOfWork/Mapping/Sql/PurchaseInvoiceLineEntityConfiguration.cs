#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/28 9:56:52
// 文件名：PurchaseInvoiceLineEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/28 9:56:52
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
    ///     PurchaseInvoiceLine实体相关配置
    /// </summary>
    internal class PurchaseInvoiceLineEntityConfiguration: EntityTypeConfiguration<PurchaseInvoiceLine>
    {
        public PurchaseInvoiceLineEntityConfiguration()
        {
            ToTable("PurchaseInvoiceLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.OrderLineId).HasColumnName("OrderLineId");
        }
    }
}
