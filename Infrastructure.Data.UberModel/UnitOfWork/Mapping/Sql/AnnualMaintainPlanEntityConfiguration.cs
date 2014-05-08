#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 10:58:50
// 文件名：AnnualMaintainPlanEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 10:58:50
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
    ///     AnnualMaintainPlan实体相关配置
    /// </summary>
    internal class AnnualMaintainPlanEntityConfiguration : EntityTypeConfiguration<AnnualMaintainPlan>
    {
        public AnnualMaintainPlanEntityConfiguration()
        {
            ToTable("AnnualMaintainPlan", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.AnnualId).HasColumnName("AnnualId");
        }
    }
}
