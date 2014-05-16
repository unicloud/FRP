#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 11:34:10
// 文件名：NonFhaMaintainCostEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 11:34:10
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     NonFhaMaintainCost实体相关配置
    /// </summary>
    internal class NonFhaMaintainCostEntityConfiguration : EntityTypeConfiguration<NonFhaMaintainCost>
    {
        public NonFhaMaintainCostEntityConfiguration()
        {
            ToTable("NonFhaMaintainCost", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ActionCategoryId).HasColumnName("ActionCategoryId");
            Property(p => p.ActualChangeLlpNumber).HasColumnName("ActualChangeLlpNumber");
            Property(p => p.ActualCsr).HasColumnName("ActualCsr");
            Property(p => p.ActualMaintainLevel).HasColumnName("ActualMaintainLevel");
            Property(p => p.ActualTsr).HasColumnName("ActualTsr");
            Property(p => p.AddedValue).HasColumnName("AddedValue");
            Property(p => p.AddedValueRate).HasColumnName("AddedValueRate");
            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.ChangeLlpFee).HasColumnName("ChangeLlpFee");
            Property(p => p.ChangeLlpNumber).HasColumnName("ChangeLlpNumber");
            Property(p => p.ContractRepairt).HasColumnName("ContractRepairt");
            Property(p => p.Csr).HasColumnName("Csr");
            Property(p => p.Custom).HasColumnName("Custom");
            Property(p => p.CustomRate).HasColumnName("CustomRate");
            Property(p => p.CustomsTax).HasColumnName("CustomsTax");
            Property(p => p.EngineNumber).HasColumnName("EngineNumber");
            Property(p => p.FeeLittleSum).HasColumnName("FeeLittleSum");
            Property(p => p.FeeTotalSum).HasColumnName("FeeTotalSum");
            Property(p => p.FreightFee).HasColumnName("FreightFee");
            Property(p => p.MaintainLevel).HasColumnName("MaintainLevel");
            Property(p => p.NonFhaFee).HasColumnName("NonFhaFee");
            Property(p => p.Note).HasColumnName("Note");
            Property(p => p.PartFee).HasColumnName("PartFee");
            Property(p => p.Rate).HasColumnName("Rate");
            Property(p => p.SupplierId).HasColumnName("SupplierId");
            Property(p => p.Tsr).HasColumnName("Tsr");
            Property(p => p.Type).HasColumnName("Type");
        }
    }
}
