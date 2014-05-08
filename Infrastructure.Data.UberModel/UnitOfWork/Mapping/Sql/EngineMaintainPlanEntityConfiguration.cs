#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 11:00:48
// 文件名：EngineExceedMaintainPlanEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 11:00:48
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
    ///     EngineMaintainPlan实体相关配置
    /// </summary>
    internal class EngineMaintainPlanEntityConfiguration : EntityTypeConfiguration<EngineMaintainPlan>
    {
        public EngineMaintainPlanEntityConfiguration()
        {
            ToTable("EngineMaintainPlan", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.MaintainPlanType).HasColumnName("MaintainPlanType");
            Property(p => p.DollarRate).HasColumnName("DollarRate");
            Property(p => p.CompanyLeader).HasColumnName("CompanyLeader");
            Property(p => p.DepartmentLeader).HasColumnName("DepartmentLeader");
            Property(p => p.BudgetManager).HasColumnName("BudgetManager");
            Property(p => p.PhoneNumber).HasColumnName("PhoneNumber");
            HasMany(o => o.EngineMaintainPlanDetails).WithRequired().HasForeignKey(o => o.EngineMaintainPlanId);
        }
    }
}
