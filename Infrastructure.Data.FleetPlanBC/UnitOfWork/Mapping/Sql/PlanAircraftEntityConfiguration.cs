#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:45:27
// 文件名：PlanAircraftEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     PlanAircraft实体相关配置
    /// </summary>
    internal class PlanAircraftEntityConfiguration : EntityTypeConfiguration<PlanAircraft>
    {
        public PlanAircraftEntityConfiguration()
        {
            ToTable("PlanAircraft", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.IsLock).HasColumnName("IsLock");
            Property(p => p.IsOwn).HasColumnName("IsOwn");
            Property(p => p.Status).HasColumnName("Status");

            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.AirlinesId).HasColumnName("AirlinesId");

            HasOptional(o => o.Aircraft).WithMany().HasForeignKey(o => o.AircraftId);
            HasRequired(o => o.AircraftType).WithMany().HasForeignKey(o => o.AircraftTypeId);
            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);
        }
    }
}
