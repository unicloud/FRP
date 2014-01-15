#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 10:35:27
// 文件名：AircraftLicenseEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 10:35:27
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AircraftLicenseAgg;

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///      AircraftLicense实体相关配置
    /// </summary>
    internal class AircraftLicenseEntityConfiguration: EntityTypeConfiguration<AircraftLicense>
    {
        public AircraftLicenseEntityConfiguration()
        {
            ToTable("AircraftLicense", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.ExpireDate).HasColumnName("ExpireDate").HasColumnType("datetime2");
            Property(p => p.IssuedDate).HasColumnName("IssuedDate").HasColumnType("datetime2");
            Property(p => p.LicenseFile).HasColumnName("LicenseFile");

            Property(p => p.IssuedUnit).HasColumnName("IssuedUnit");
            Property(p => p.State).HasColumnName("State");
            Property(p => p.ValidMonths).HasColumnName("ValidMonths");
            Property(p => p.LicenseTypeId).HasColumnName("LicenseTypeId");
            Property(p => p.AircraftId).HasColumnName("AircraftId");
            HasOptional(o => o.LicenseType).WithMany().HasForeignKey(o => o.LicenseTypeId);
            HasRequired(o => o.Aircraft).WithMany().HasForeignKey(o => o.AircraftId);
        }
    }
}
