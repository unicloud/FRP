#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
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
using UniCloud.Domain.UberModel.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftPurchaseOrderLine实体相关配置
    /// </summary>
    internal class AircraftPurchaseOrderLineEntityConfiguration : EntityTypeConfiguration<AircraftPurchaseOrderLine>
    {
        public AircraftPurchaseOrderLineEntityConfiguration()
        {
            ToTable("AircraftPurchaseOrderLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.AirframePrice).HasColumnName("AirframePrice");
            Property(p => p.RefitCost).HasColumnName("RefitCost");
            Property(p => p.EnginePrice).HasColumnName("EnginePrice");

            Property(p => p.ContractAircraftId).HasColumnName("ContractAircraftId");

            HasRequired(a => a.PurchaseContractAircraft)
                .WithMany(p => p.AircraftPurchaseOrderLines)
                .HasForeignKey(a => a.ContractAircraftId);
        }
    }
}