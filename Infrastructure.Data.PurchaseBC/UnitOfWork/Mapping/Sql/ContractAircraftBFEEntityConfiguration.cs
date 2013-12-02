#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/02，17:22
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftBFEAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     ContractAircraftBFE实体相关配置
    /// </summary>
    internal class ContractAircraftBFEEntityConfiguration : EntityTypeConfiguration<ContractAircraftBFE>
    {
        public ContractAircraftBFEEntityConfiguration()
        {
            ToTable("ContractAircraftBFE", DbConfig.Schema);

            HasKey(p => new {p.ContractAircraftId, p.BFEPurchaseOrderId});

            Property(p => p.ContractAircraftId).HasColumnName("ContractAircraftId");
            Property(p => p.BFEPurchaseOrderId).HasColumnName("BFEPurchaseOrderId");

            HasRequired(c => c.ContractAircraft)
                .WithMany(c => c.ContractAircraftBfes)
                .HasForeignKey(c => c.ContractAircraftId);
            HasRequired(c => c.BFEPurchaseOrder)
                .WithMany(b => b.ContractAircraftBfes)
                .HasForeignKey(c => c.BFEPurchaseOrderId);
        }
    }
}