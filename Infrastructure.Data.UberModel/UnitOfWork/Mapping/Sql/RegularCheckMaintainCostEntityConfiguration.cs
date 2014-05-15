#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 11:48:01
// 文件名：RegularCheckMaintainCostEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 11:48:01
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
    ///     RegularCheckMaintainCost实体相关配置
    /// </summary>
    internal class RegularCheckMaintainCostEntityConfiguration : EntityTypeConfiguration<RegularCheckMaintainCost>
    {
        public RegularCheckMaintainCostEntityConfiguration()
        {
            ToTable("RegularCheckMaintainCost", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.ActionCategoryId).HasColumnName("ActionCategoryId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.RegularCheckType).HasColumnName("RegularCheckType");
            Property(p => p.RegularCheckLevel).HasColumnName("RegularCheckLevel");

        }
    }
}
