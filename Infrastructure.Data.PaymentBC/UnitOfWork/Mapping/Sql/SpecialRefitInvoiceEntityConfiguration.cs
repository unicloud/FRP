#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 9:48:23
// 文件名：SpecialRefitInvoiceEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 9:48:23
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SpecialRefitInvoice实体相关配置
    /// </summary>
    internal class SpecialRefitInvoiceEntityConfiguration : EntityTypeConfiguration<SpecialRefitInvoice>
    {
        public SpecialRefitInvoiceEntityConfiguration()
        {
            ToTable("SpecialRefitInvoice", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
