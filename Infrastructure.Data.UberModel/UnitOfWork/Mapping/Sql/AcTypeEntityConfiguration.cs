#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/4 10:36:49
// 文件名：AcTypeEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AcTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AcType实体相关配置
    /// </summary>
    internal class AcTypeEntityConfiguration : EntityTypeConfiguration<AcType>
    {
        public AcTypeEntityConfiguration()
        {
            ToTable("AcType", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");

            Property(p => p.ManufacturerId).HasColumnName("ManufacturerId");
            Property(p => p.AircraftCategoryId).HasColumnName("AircraftCategoryId");

            HasRequired(o => o.Manufacturer).WithMany().HasForeignKey(o => o.ManufacturerId);
            HasRequired(o => o.AircraftCategory).WithMany().HasForeignKey(o => o.AircraftCategoryId);
        }
    }
}
