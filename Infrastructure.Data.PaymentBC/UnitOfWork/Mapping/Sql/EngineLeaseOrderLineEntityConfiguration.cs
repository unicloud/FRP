#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，14:12
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     EngineLeaseOrderLine实体相关配置
    /// </summary>
    internal class EngineLeaseOrderLineEntityConfiguration : EntityTypeConfiguration<EngineLeaseOrderLine>
    {
        public EngineLeaseOrderLineEntityConfiguration()
        {
            ToTable("EngineLeaseOrderLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.EngineMaterialId).HasColumnName("EngineMaterialId");
            Property(p => p.ContractEngineId).HasColumnName("ContractEngineId");

            HasRequired(e => e.LeaseContractEngine)
                .WithMany(e => e.EngineLeaseOrderLines)
                .HasForeignKey(e => e.ContractEngineId);
        }
    }
}