#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/02，16:12
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.SupplierCompanyMaterialAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SupplierCompanyMaterial实体相关配置
    /// </summary>
    internal class SupplierCompanyMaterialEntityConfiguration : EntityTypeConfiguration<SupplierCompanyMaterial>
    {
        public SupplierCompanyMaterialEntityConfiguration()
        {
            ToTable("SupplierCompanyMaterial", DbConfig.Schema);

            HasKey(p => new {p.MaterialId, p.SupplierCompanyId});

            Property(p => p.MaterialId).HasColumnName("MaterialId");
            Property(p => p.SupplierCompanyId).HasColumnName("SupplierCompanyId");

            HasRequired(s => s.SupplierCompany)
                .WithMany(s => s.SupplierCompanyMaterials)
                .HasForeignKey(s => s.SupplierCompanyId);
            HasRequired(s => s.Material).WithMany(m => m.SupplierCompanyMaterials).HasForeignKey(s => s.MaterialId);
        }
    }
}