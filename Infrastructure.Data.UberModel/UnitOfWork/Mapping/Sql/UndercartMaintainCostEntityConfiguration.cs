#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 16:38:04
// 文件名：UndercartMaintainCostEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 16:38:04
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.MaintainCostAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     UndercartMaintainCost实体相关配置
    /// </summary>
    internal class UndercartMaintainCostEntityConfiguration : EntityTypeConfiguration<UndercartMaintainCost>
    {
        public UndercartMaintainCostEntityConfiguration()
        {
            ToTable("UndercartMaintainCost", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.ActionCategoryId).HasColumnName("ActionCategoryId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.AddedValue).HasColumnName("AddedValue");
            Property(p => p.AddedValueRate).HasColumnName("AddedValueRate");
            Property(p => p.Custom).HasColumnName("Custom");
            Property(p => p.CustomRate).HasColumnName("CustomRate");
            Property(p => p.FreightFee).HasColumnName("FreightFee");
            Property(p => p.MaintainFeeEur).HasColumnName("MaintainFeeEur");
            Property(p => p.MaintainFeeRmb).HasColumnName("MaintainFeeRmb");
            Property(p => p.Part).HasColumnName("Part");
            Property(p => p.Rate).HasColumnName("Rate");
            Property(p => p.ReplaceFee).HasColumnName("ReplaceFee");
            Property(p => p.Type).HasColumnName("Type");
        }
    }
}
