#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/3/13 14:08:09
// 文件名：ProgrammingFileEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.ProgrammingFileAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     ProgrammingFile实体相关配置
    /// </summary>
    internal class ProgrammingFileEntityConfiguration : EntityTypeConfiguration<ProgrammingFile>
    {
        public ProgrammingFileEntityConfiguration()
        {
            ToTable("ProgrammingFile", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.IssuedDate).HasColumnName("IssuedDate").HasColumnType("datetime2");
            Property(p => p.DocName).HasColumnName("DocName");
            Property(p => p.DocNumber).HasColumnName("DocNumber");
            Property(p => p.Type).HasColumnName("Type");

            Property(p => p.ProgrammingId).HasColumnName("ProgrammingId");
            Property(p => p.DocumentId).HasColumnName("DocumentId");
            Property(p => p.IssuedUnitId).HasColumnName("IssuedUnitId");

            HasRequired(o => o.Programming).WithMany().HasForeignKey(o => o.ProgrammingId);
            HasRequired(o => o.IssuedUnit).WithMany().HasForeignKey(o => o.IssuedUnitId);
        }
    }
}