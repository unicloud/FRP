#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/28 9:58:43
// 文件名：MaintainInvoiceLineEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/28 9:58:43
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.MaintainInvoiceAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     MaintainInvoiceLine实体相关配置
    /// </summary>
    internal class MaintainInvoiceLineEntityConfiguration: EntityTypeConfiguration<MaintainInvoiceLine>
    {
        public MaintainInvoiceLineEntityConfiguration()
        {
            ToTable("MaintainInvoiceLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.MaintainItem).HasColumnName("MaintainItem");
            Property(p => p.ItemName).HasColumnName("ItemName");
            Property(p => p.UnitPrice).HasColumnName("UnitPrice");
            Property(p => p.InvoiceId).HasColumnName("InvoiceId");

        }
    }
}
