#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:26:37
// 文件名：AircraftEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Aircraft实体相关配置
    /// </summary>
    internal class AircraftEntityConfiguration : EntityTypeConfiguration<Aircraft>
    {
        public AircraftEntityConfiguration()
        {
            ToTable("Aircraft", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.RegNumber).HasColumnName("RegNumber");
            Property(p => p.SerialNumber).HasColumnName("SerialNumber");
            Property(p => p.SeatingCapacity).HasColumnName("SeatingCapacity");
            Property(p => p.CarryingCapacity).HasColumnName("CarryingCapacity");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.FactoryDate).HasColumnName("FactoryDate").HasColumnType("datetime2");
            Property(p => p.ImportDate).HasColumnName("ImportDate").HasColumnType("datetime2");
            Property(p => p.ExportDate).HasColumnName("ExportDate").HasColumnType("datetime2");
            Property(p => p.IsOperation).HasColumnName("IsOperation");

            Property(p => p.SupplierId).HasColumnName("SupplierId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.AirlinesId).HasColumnName("AirlinesId");
            Property(p => p.ImportCategoryId).HasColumnName("ImportCategoryId");

            HasOptional(o => o.Supplier).WithMany().HasForeignKey(o => o.SupplierId);
            HasRequired(o => o.AircraftType).WithMany().HasForeignKey(o => o.AircraftTypeId);
            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);
            HasRequired(o => o.ImportCategory).WithMany().HasForeignKey(o => o.ImportCategoryId);

            HasMany(o => o.OperationHistories).WithRequired().HasForeignKey(o => o.AircraftId);
            HasMany(o => o.OwnershipHistories).WithRequired().HasForeignKey(o => o.AircraftId);
            HasMany(o => o.AircraftBusinesses).WithRequired().HasForeignKey(o => o.AircraftId);

        }
    }
}
