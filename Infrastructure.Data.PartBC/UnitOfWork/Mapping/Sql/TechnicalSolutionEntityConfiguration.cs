#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：TechnicalSolutionEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// TechnicalSolution实体相关配置
    /// </summary>
    internal class TechnicalSolutionEntityConfiguration : EntityTypeConfiguration<TechnicalSolution>
    {
        public TechnicalSolutionEntityConfiguration()
        {
            ToTable("TechnicalSolution", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.FiNumber).HasColumnName("FiNumber").HasMaxLength(100);
            Property(p => p.TsNumber).HasColumnName("TsNumber").HasMaxLength(100);
            Property(p => p.Position).HasColumnName("Position").HasMaxLength(100);

            HasMany(o => o.TsLines).WithRequired().HasForeignKey(o => o.TsId);
        }

    }
}
