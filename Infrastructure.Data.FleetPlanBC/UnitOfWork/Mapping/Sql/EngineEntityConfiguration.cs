#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:41:38
// 文件名：EngineEntityConfiguration
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
    ///     Engine实体相关配置
    /// </summary>
    internal class EngineEntityConfiguration : EntityTypeConfiguration<Engine>
    {
        public EngineEntityConfiguration()
        {
            ToTable("Engine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.SerialNumber).HasColumnName("SerialNumber");
            Property(p => p.MaxThrust).HasColumnName("MaxThrust");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.FactoryDate).HasColumnName("FactoryDate").HasColumnType("datetime2");
            Property(p => p.ImportDate).HasColumnName("ImportDate").HasColumnType("datetime2");
            Property(p => p.ExportDate).HasColumnName("ExportDate").HasColumnType("datetime2");

            Property(p => p.EngineTypeId).HasColumnName("EngineTypeId");
            Property(p => p.SupplierId).HasColumnName("SupplierId");
            Property(p => p.AirlinesId).HasColumnName("AirlinesId");
            Property(p => p.ImportCategoryId).HasColumnName("ImportCategoryId");

            HasRequired(o => o.EngineType).WithMany().HasForeignKey(o => o.EngineTypeId);
            HasOptional(o => o.Supplier).WithMany().HasForeignKey(o => o.SupplierId);
            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);
            HasRequired(o => o.ImportCategory).WithMany().HasForeignKey(o => o.ImportCategoryId);
            HasMany(o => o.EngineOwnerShipHistories).WithRequired().HasForeignKey(o => o.EngineId);
            HasMany(o => o.EngineBusinessHistories).WithRequired().HasForeignKey(o => o.EngineId);

        }
    }
}
