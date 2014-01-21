#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/15 10:33:16
// 文件名：LicenseTypeEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/15 10:33:16
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///    LicenseType实体相关配置
    /// </summary>
    internal class LicenseTypeEntityConfiguration: EntityTypeConfiguration<LicenseType>
    {
        public LicenseTypeEntityConfiguration()
        {
            ToTable("LicenseType", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Type).HasColumnName("Type");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.HasFile).HasColumnName("HasFile");

        }
    }
}
