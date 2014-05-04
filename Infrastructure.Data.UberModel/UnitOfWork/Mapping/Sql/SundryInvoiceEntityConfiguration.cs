#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/4 14:07:38
// 文件名：SundryInvoiceEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/4 14:07:38
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.InvoiceAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SundryInvoice实体相关配置
    /// </summary>
    internal class SundryInvoiceEntityConfiguration : EntityTypeConfiguration<SundryInvoice>
    {
        public SundryInvoiceEntityConfiguration()
        {
            ToTable("SundryInvoice", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
