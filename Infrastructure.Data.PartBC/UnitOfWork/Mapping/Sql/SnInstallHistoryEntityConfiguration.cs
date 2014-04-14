#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 17:58:12
// 文件名：SnInstallHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.SnInstallHistoryAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SnInstallHistory实体相关配置
    /// </summary>
    internal class SnInstallHistoryEntityConfiguration : EntityTypeConfiguration<SnInstallHistory>
    {
        public SnInstallHistoryEntityConfiguration()
        {
            ToTable("SnInstallHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Sn).HasColumnName("Sn").HasMaxLength(100);
            Property(p => p.SnRegId).HasColumnName("SnRegId");
            Property(p => p.Pn).HasColumnName("Pn").HasMaxLength(100);
            Property(p => p.PnRegId).HasColumnName("PnRegId");
            Property(p => p.InstallDate).HasColumnName("InstallDate").HasColumnType("datetime2");
            Property(p => p.RemoveDate).HasColumnName("RemoveDate").HasColumnType("datetime2");
            Property(p => p.InstallReason).HasColumnName("InstallReason").HasMaxLength(100);
            Property(p => p.RemoveReason).HasColumnName("RemoveReason").HasMaxLength(100);
            Property(p => p.CSN).HasColumnName("CSN");
            Property(p => p.CSR).HasColumnName("CSR");
            Property(p => p.TSN).HasColumnName("TSN");
            Property(p => p.TSR).HasColumnName("TSR");
            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
        }
    }
}