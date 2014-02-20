#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ScnEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.ScnAgg;
#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// Scn实体相关配置
    /// </summary>
    internal class ScnEntityConfiguration : EntityTypeConfiguration<Scn>
    {
        public ScnEntityConfiguration()
        {
            ToTable("Scn", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CheckDate).HasColumnName("CheckDate").HasColumnType("datetime2");
            Property(p => p.CSCNumber).HasColumnName("CSCNumber").HasMaxLength(100);
            Property(p => p.ModNumber).HasColumnName("ModNumber").HasMaxLength(100);
            Property(p => p.TsNumber).HasColumnName("TsNumber").HasMaxLength(100);
            Property(p => p.Cost).HasColumnName("Cost").HasColumnType("decimal").HasPrecision(16, 4);
            Property(p => p.ScnNumber).HasColumnName("ScnNumber").HasMaxLength(100);
            Property(p => p.ScnType).HasColumnName("ScnType");
            Property(p => p.Description).HasColumnName("Description").HasMaxLength(100);
            Property(p => p.ScnDocName).HasColumnName("ScnDocName").HasMaxLength(100);
            Property(p => p.ScnDocumentId).HasColumnName("ScnDocumentId");

            HasMany(o => o.ApplicableAircrafts).WithRequired().HasForeignKey(o => o.ScnId);
        }

    }
}
