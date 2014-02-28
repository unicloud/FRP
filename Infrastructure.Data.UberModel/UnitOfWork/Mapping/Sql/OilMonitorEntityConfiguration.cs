#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/22，21:07
// 方案：FRP
// 项目：Infrastructure.Data.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.OilMonitorAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     OilMonitor实体相关配置
    /// </summary>
    internal class OilMonitorEntityConfiguration : EntityTypeConfiguration<OilMonitor>
    {
        public OilMonitorEntityConfiguration()
        {
            ToTable("OilMonitor", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Date).HasColumnName("Date").HasColumnType("datetime2");
            Property(p => p.TSN).HasColumnName("TSN");
            Property(p => p.TSR).HasColumnName("TSR");
            Property(p => p.TotalRate).HasColumnName("TotalRate");
            Property(p => p.IntervalRate).HasColumnName("IntervalRate");
            Property(p => p.DeltaIntervalRate).HasColumnName("DeltaIntervalRate");
            Property(p => p.AverageRate3).HasColumnName("AverageRate3");
            Property(p => p.AverageRate7).HasColumnName("AverageRate7");

            Property(p => p.OilUserID).HasColumnName("OilUserID");
        }
    }
}