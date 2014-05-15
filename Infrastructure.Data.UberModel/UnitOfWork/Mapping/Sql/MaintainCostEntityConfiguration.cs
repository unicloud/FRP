#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 11:42:57
// 文件名：MaintainCostEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 11:42:57
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
    ///     MaintainCost实体相关配置
    /// </summary>
    internal class MaintainCostEntityConfiguration : EntityTypeConfiguration<MaintainCost>
    {
        public MaintainCostEntityConfiguration()
        {
            ToTable("MaintainCost", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.InMaintainTime).HasColumnName("InMaintainTime").HasColumnType("datetime2");
            Property(p => p.OutMaintainTime).HasColumnName("OutMaintainTime").HasColumnType("datetime2");
            Property(p => p.TotalDays).HasColumnName("TotalDays");
            Property(p => p.DepartmentDeclareAmount).HasColumnName("DepartmentDeclareAmount");
            Property(p => p.FinancialApprovalAmount).HasColumnName("FinancialApprovalAmount");
            Property(p => p.FinancialApprovalAmountNonTax).HasColumnName("FinancialApprovalAmountNonTax");
            Property(p => p.MaintainInvoiceId).HasColumnName("MaintainInvoiceId");

            HasRequired(o => o.MaintainInvoice).WithMany().HasForeignKey(o => o.MaintainInvoiceId);
        }
    }
}
