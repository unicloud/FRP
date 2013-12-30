#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/29 11:17:11
// 文件名：PlanEngineEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.PlanEngineAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     PlanEngine实体相关配置
    /// </summary>
    internal class PlanEngineEntityConfiguration : EntityTypeConfiguration<PlanEngine>
    {
        public PlanEngineEntityConfiguration()
        {
            ToTable("PlanEngine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.EngineId).HasColumnName("EngineId");
            Property(p => p.EngineTypeId).HasColumnName("EngineTypeId");
            Property(p => p.AirlinesId).HasColumnName("AirlinesId");

            HasOptional(o => o.Engine).WithMany().HasForeignKey(o => o.EngineId);
            HasRequired(o => o.EngineType).WithMany().HasForeignKey(o => o.EngineTypeId);
            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);

        }
    }
}
