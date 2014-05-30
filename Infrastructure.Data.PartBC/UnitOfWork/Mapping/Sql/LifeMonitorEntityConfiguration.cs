#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:57

// 文件名：LifeMonitorEntityConfiguration
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
    /// LifeMonitor实体相关配置
    /// </summary>
    internal class LifeMonitorEntityConfiguration : EntityTypeConfiguration<LifeMonitor>
    {
        public LifeMonitorEntityConfiguration()
        {
            ToTable("LifeMonitor", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.WorkDescription).HasColumnName("WorkDescription").HasMaxLength(100);
            Property(p => p.MointorStart).HasColumnName("MointorStart").HasColumnType("datetime2");
            Property(p => p.MointorEnd).HasColumnName("MointorEnd").HasColumnType("datetime2");
            Property(p => p.SnRegId).HasColumnName("SnRegId");
        }

    }
}
