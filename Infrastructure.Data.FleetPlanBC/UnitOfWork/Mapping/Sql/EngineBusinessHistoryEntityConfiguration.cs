#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:42:19
// 文件名：EngineBusinessHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     EngineBusinessHistory实体相关配置
    /// </summary>
    internal class EngineBusinessHistoryEntityConfiguration : EntityTypeConfiguration<EngineBusinessHistory>
    {
        public EngineBusinessHistoryEntityConfiguration()
        {
            ToTable("EngineBusinessHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");
            Property(p => p.MaxThrust).HasColumnName("MaxThrust");

            Property(p => p.EngineId).HasColumnName("EngineId");
            Property(p => p.EngineTypeId).HasColumnName("EngineTypeId");
            Property(p => p.ImportCategoryId).HasColumnName("ImportCategoryId");

            HasRequired(o => o.EngineType).WithMany().HasForeignKey(o => o.EngineTypeId);
            HasRequired(o => o.ImportCategory).WithMany().HasForeignKey(o => o.ImportCategoryId);

        }
    }
}
