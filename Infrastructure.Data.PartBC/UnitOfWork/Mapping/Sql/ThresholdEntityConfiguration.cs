#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/5/20 15:52:31
// 文件名：ThresholdEntityConfiguration
// 版本：V1.0.0
//
// 修改者：  时间：2014/5/20 15:52:31
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.ThresholdAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// Threshold实体相关配置
    /// </summary>
    internal class ThresholdEntityConfiguration : EntityTypeConfiguration<Threshold>
    {
        public ThresholdEntityConfiguration()
        {
            ToTable("Threshold", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.PnRegId).HasColumnName("PnRegId");
            Property(p => p.TotalThreshold).HasColumnName("TotalThreshold");
            Property(p => p.IntervalThreshold).HasColumnName("IntervalThreshold");
            Property(p => p.DeltaIntervalThreshold).HasColumnName("DeltaIntervalThreshold");
            Property(p => p.Average3Threshold).HasColumnName("Average3Threshold");
            Property(p => p.Average7Threshold).HasColumnName("Average7Threshold");
        }
    }
}
