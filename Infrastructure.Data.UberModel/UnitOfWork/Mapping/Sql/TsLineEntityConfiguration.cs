#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：TsLineEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.TechnicalSolutionAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// TsLine实体相关配置
    /// </summary>
    internal class TsLineEntityConfiguration : EntityTypeConfiguration<TsLine>
    {
        public TsLineEntityConfiguration()
        {
            ToTable("TsLine", DbConfig.Schema);

            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Pn).HasColumnName("Pn").HasMaxLength(100);
            Property(p => p.Description).HasColumnName("Description").HasMaxLength(100);
            Property(p => p.TsNumber).HasColumnName("TsNumber").HasMaxLength(100);
            Property(p => p.TsId).HasColumnName("TsId");

            HasMany(o => o.Dependencies).WithRequired().HasForeignKey(o => o.TsLineId);
        }

    }
}
