#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 11:04:12
// 文件名：EngineMaintainPlanDetailEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 11:04:12
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AnnualMaintainPlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     EngineMaintainPlanDetail实体相关配置
    /// </summary>
    internal class EngineMaintainPlanDetailEntityConfiguration : EntityTypeConfiguration<EngineMaintainPlanDetail>
    {
        public EngineMaintainPlanDetailEntityConfiguration()
        {
            ToTable("EngineMaintainPlanDetail", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.EngineNumber).HasColumnName("EngineNumber");
            Property(p => p.InMaintainDate).HasColumnName("InMaintainDate");
            Property(p => p.OutMaintainDate).HasColumnName("OutMaintainDate");
            Property(p => p.MaintainLevel).HasColumnName("MaintainLevel");
            Property(p => p.ChangeLlpNumber).HasColumnName("ChangeLlpNumber");
            Property(p => p.TsnCsn).HasColumnName("TsnCsn");
            Property(p => p.TsrCsr).HasColumnName("TsrCsr");
            Property(p => p.NonFhaFee).HasColumnName("NonFhaFee");
            Property(p => p.PartFee).HasColumnName("PartFee");
            Property(p => p.ChangeLlpFee).HasColumnName("ChangeLlpFee");
            Property(p => p.CustomsTax).HasColumnName("CustomsTax");
            Property(p => p.FreightFee).HasColumnName("FreightFee");
            Property(p => p.Note).HasColumnName("Note");
            Property(p => p.FeeLittleSum).HasColumnName("FeeLittleSum");
            Property(p => p.FeeTotalSum).HasColumnName("FeeTotalSum");
            Property(p => p.BudgetToalSum).HasColumnName("BudgetToalSum");
            Property(p => p.EngineMaintainPlanId).HasColumnName("EngineMaintainPlanId");
        }
    }
}
