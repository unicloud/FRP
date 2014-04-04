#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/20 9:50:51
// 文件名：DocumentTypeEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/20 9:50:51
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.CommonServiceBC.Aggregates.DocumentTypeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.CommonServiceBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///      DocumentType实体相关配置
    /// </summary>
    internal class DocumentTypeEntityConfiguration : EntityTypeConfiguration<DocumentType>
    {
        public DocumentTypeEntityConfiguration()
        {
            ToTable("DocumentType", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");
        }
    }
}
