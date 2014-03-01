#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SnReg实体相关配置
    /// </summary>
    internal class SnRegEntityConfiguration : EntityTypeConfiguration<SnReg>
    {
        public SnRegEntityConfiguration()
        {
            ToTable("SnReg", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Sn).HasColumnName("Sn").HasMaxLength(100);
            Property(p => p.TSN).HasColumnName("TSN");
            Property(p => p.TSR).HasColumnName("TSR");
            Property(p => p.CSN).HasColumnName("CSN");
            Property(p => p.CSR).HasColumnName("CSR");
            Property(p => p.InstallDate).HasColumnName("InstallDate").HasColumnType("datetime2");
            Property(p => p.Pn).HasColumnName("Pn").HasMaxLength(100);
            Property(p => p.IsStop).HasColumnName("IsStop");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime2");
            Property(p => p.RegNumber).HasColumnName("RegNumber").HasMaxLength(100);

            Property(p => p.PnRegId).HasColumnName("PnRegId");
            Property(p => p.AircraftId).HasColumnName("AircraftId");

            HasMany(o => o.SnHistories).WithRequired().HasForeignKey(o => o.SnRegId);
            HasMany(o => o.LifeMonitors).WithRequired().HasForeignKey(o => o.SnRegId);
        }
    }
}