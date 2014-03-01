#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 17:17:13
// 文件名：AdSbEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 17:17:13
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AdSbAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// AdSb实体相关配置
    /// </summary>
    internal class AdSbEntityConfiguration : EntityTypeConfiguration<AdSb>
    {
        public AdSbEntityConfiguration()
        {
            ToTable("AdSb", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.AircraftSeries).HasColumnName("AircraftSeries");
            Property(p => p.ComplyAircraft).HasColumnName("ComplyAircraft");
            Property(p => p.ComplyClose).HasColumnName("ComplyClose");
            Property(p => p.ComplyDate).HasColumnName("ComplyDate").HasColumnType("datetime2");
            Property(p => p.ComplyFee).HasColumnName("ComplyFee").HasColumnType("decimal").HasPrecision(16, 4);
            Property(p => p.ComplyFeeCurrency).HasColumnName("ComplyFeeCurrency");
            Property(p => p.ComplyFeeNotes).HasColumnName("ComplyFeeNotes");
            Property(p => p.ComplyNotes).HasColumnName("ComplyNotes");
            Property(p => p.ComplyStatus).HasColumnName("ComplyStatus");
            Property(p => p.FileNo).HasColumnName("FileNo");
            Property(p => p.FileType).HasColumnName("FileType");
            Property(p => p.FileVersion).HasColumnName("FileVersion");
        }
    }
}
