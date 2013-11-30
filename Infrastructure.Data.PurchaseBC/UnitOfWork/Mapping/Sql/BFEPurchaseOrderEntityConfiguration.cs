#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，20:11
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     BFEPurchaseOrder实体相关配置
    /// </summary>
    internal class BFEPurchaseOrderEntityConfiguration : EntityTypeConfiguration<BFEPurchaseOrder>
    {
        public BFEPurchaseOrderEntityConfiguration()
        {
            ToTable("BFEPurchaseOrder", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ForwarderLinkman).HasColumnName("ForwarderLinkman");

            Property(p => p.ForwarderId).HasColumnName("ForwarderId");

            HasOptional(b => b.Forwarder).WithMany().HasForeignKey(b => b.ForwarderId);
            HasMany(b => b.ContractAircrafts)
                .WithMany(c => c.BFEPurchaseOrders)
                .Map(m => m.ToTable("ContractAircraftBFE", DbConfig.Schema));
        }
    }
}