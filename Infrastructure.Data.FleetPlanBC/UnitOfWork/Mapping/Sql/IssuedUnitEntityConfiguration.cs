#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/22 17:04:42
// 文件名：IssuedUnitEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.IssuedUnitAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     IssuedUnit实体相关配置
    /// </summary>
    internal class IssuedUnitEntityConfiguration : EntityTypeConfiguration<IssuedUnit>
    {
        public IssuedUnitEntityConfiguration()
        {
            ToTable("IssuedUnit", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.CnName).HasColumnName("CnName");
            Property(p => p.CnShortName).HasColumnName("CnShortName");
            Property(p => p.IsInner).HasColumnName("IsInner");
        }
    }
}