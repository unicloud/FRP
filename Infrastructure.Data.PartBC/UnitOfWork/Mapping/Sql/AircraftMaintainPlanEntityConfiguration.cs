#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/9 9:22:20
// 文件名：AircraftMaintainPlanEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/9 9:22:20
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftMaintainPlan实体相关配置
    /// </summary>
    internal class AircraftMaintainPlanEntityConfiguration : EntityTypeConfiguration<AircraftMaintainPlan>
    {
        public AircraftMaintainPlanEntityConfiguration()
        {
            ToTable("AircraftMaintainPlan", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.FirstHalfYear).HasColumnName("FirstHalfYear");
            Property(p => p.SecondHalfYear).HasColumnName("SecondHalfYear");
            Property(p => p.Note).HasColumnName("Note");
            HasMany(o => o.AircraftMaintainPlanDetails).WithRequired().HasForeignKey(o => o.AircraftMaintainPlanId);
        }
    }
}
