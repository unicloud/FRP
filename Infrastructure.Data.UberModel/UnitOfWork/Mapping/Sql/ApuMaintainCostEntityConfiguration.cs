#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 15:21:22
// 文件名：ApuMaintainCostEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 15:21:22
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
    ///     ApuMaintainCost实体相关配置
    /// </summary>
    internal class ApuMaintainCostEntityConfiguration : EntityTypeConfiguration<ApuMaintainCost>
    {
        public ApuMaintainCostEntityConfiguration()
        {
            ToTable("ApuMaintainCost", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.AddedValue).HasColumnName("AddedValue");
            Property(p => p.AddedValueRate).HasColumnName("AddedValueRate");
            Property(p => p.BudgetHour).HasColumnName("BudgetHour");
            Property(p => p.ContractRepairFeeRmb).HasColumnName("ContractRepairFeeRmb");
            Property(p => p.ContractRepairFeeUsd).HasColumnName("ContractRepairFeeUsd");
            Property(p => p.CustomRate).HasColumnName("CustomRate");
            Property(p => p.Hour).HasColumnName("Hour");
            Property(p => p.HourPercent).HasColumnName("HourPercent");
            Property(p => p.IncludeAddedValue).HasColumnName("IncludeAddedValue");
            Property(p => p.NameType).HasColumnName("NameType");
            Property(p => p.Rate).HasColumnName("Rate");
            Property(p => p.TotalTax).HasColumnName("TotalTax");
            Property(p => p.Type).HasColumnName("Type");
            Property(p => p.YearBudgetRate).HasColumnName("YearBudgetRate");
        }
    }
}
