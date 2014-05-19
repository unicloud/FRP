#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/19 9:06:11
// 文件名：FhaMaintainCostEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/19 9:06:11
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
    ///     FhaMaintainCost实体相关配置
    /// </summary>
    internal class FhaMaintainCostEntityConfiguration : EntityTypeConfiguration<FhaMaintainCost>
    {
        public FhaMaintainCostEntityConfiguration()
        {
            ToTable("FhaMaintainCost", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.AddedValue).HasColumnName("AddedValue");
            Property(p => p.AddedValueRate).HasColumnName("AddedValueRate");
            Property(p => p.AirHour).HasColumnName("AirHour");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.Custom).HasColumnName("Custom");
            Property(p => p.CustomAddedRmb).HasColumnName("CustomAddedRmb");
            Property(p => p.Hour).HasColumnName("Hour");
            Property(p => p.HourPercent).HasColumnName("HourPercent");
            Property(p => p.IncludeAddedValue).HasColumnName("IncludeAddedValue");
            Property(p => p.LastYearRate).HasColumnName("LastYearRate");
            Property(p => p.EngineProperty).HasColumnName("EngineProperty");
            Property(p => p.Rate).HasColumnName("Rate");
            Property(p => p.TotalTax).HasColumnName("TotalTax");
            Property(p => p.Jx).HasColumnName("Jx");
            Property(p => p.YearAddedRate).HasColumnName("YearAddedRate");
            Property(p => p.YearBudgetRate).HasColumnName("YearBudgetRate");
            Property(p => p.FhaFeeRmb).HasColumnName("FhaFeeRmb");
            Property(p => p.FhaFeeUsd).HasColumnName("FhaFeeUsd");
            Property(p => p.CustomAdded).HasColumnName("CustomAdded");
        }
    }
}
