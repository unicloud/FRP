#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/29 11:12:11
// 文件名：AircraftBusinessEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftBusiness实体相关配置
    /// </summary>
    internal class AircraftBusinessEntityConfiguration : EntityTypeConfiguration<AircraftBusiness>
    {
        public AircraftBusinessEntityConfiguration()
        {
            ToTable("AircraftBusiness", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.SeatingCapacity).HasColumnName("SeatingCapacity");
            Property(p => p.CarryingCapacity).HasColumnName("CarryingCapacity");
            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");
            Property(p => p.Status).HasColumnName("Status");

            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.ImportCategoryId).HasColumnName("ImportCategoryId");

            HasRequired(o => o.AircraftType).WithMany().HasForeignKey(o => o.AircraftTypeId);
            HasRequired(o => o.ImportCategory).WithMany().HasForeignKey(o => o.ImportCategoryId);
        }
    }
}
