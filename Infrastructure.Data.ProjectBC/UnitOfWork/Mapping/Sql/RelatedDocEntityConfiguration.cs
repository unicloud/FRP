#region 版本信息

// ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，17:01
// 方案：FRP
// 项目：Infrastructure.Data.ProjectBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.ProjectBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     RelatedDoc实体相关配置
    /// </summary>
    internal class RelatedDocEntityConfiguration : EntityTypeConfiguration<RelatedDoc>
    {
        public RelatedDocEntityConfiguration()
        {
            ToTable("RelatedDoc", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.SourceId).HasColumnName("SourceId");
            Property(p => p.DocumentId).HasColumnName("DocumentId");
            Property(p => p.DocumentName).HasColumnName("DocumentName");
        }
    }
}