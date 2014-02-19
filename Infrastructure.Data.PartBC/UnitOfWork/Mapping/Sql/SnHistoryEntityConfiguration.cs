#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnHistoryEntityConfiguration
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
    /// SnHistory实体相关配置
    /// </summary>
    internal class SnHistoryEntityConfiguration : EntityTypeConfiguration<SnHistory>
    {
        public SnHistoryEntityConfiguration()
        {
            ToTable("SnHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Sn).HasColumnName("Sn").HasMaxLength(100);
            Property(p => p.InstallDate).HasColumnName("InstallDate").HasColumnType("datetime2");
            Property(p => p.RemoveDate).HasColumnName("RemoveDate").HasColumnType("datetime2");
            Property(p => p.FiNumber).HasColumnName("FiNumber").HasMaxLength(100);
            Property(p => p.CSN).HasColumnName("CSN").HasMaxLength(100);
            Property(p => p.CSR).HasColumnName("CSR").HasMaxLength(100);
            Property(p => p.TSN).HasColumnName("TSN").HasMaxLength(100);
            Property(p => p.TSR).HasColumnName("TSR").HasMaxLength(100);
            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.SnRegId).HasColumnName("SnRegId");
        }

    }
}
