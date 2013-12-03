#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，22:11
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.MaterialAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Material实体相关配置
    /// </summary>
    internal class MaterialEntityConfiguration : EntityTypeConfiguration<Material>
    {
        public MaterialEntityConfiguration()
        {
            ToTable("Material", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");

            Property(p => p.ManufacturerID).HasColumnName("ManufacturerID");

            HasOptional(m => m.Manufacturer).WithMany().HasForeignKey(m => m.ManufacturerID);
        }
    }
}