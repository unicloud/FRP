#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/09，23:12
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.Aggregates.DocumentAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Document实体相关配置
    /// </summary>
    internal class DocumentEntityConfiguration : EntityTypeConfiguration<Document>
    {
        public DocumentEntityConfiguration()
        {
            ToTable("Document", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.FileName).HasColumnName("FileName");
            Property(p => p.Extension).HasColumnName("Extension");
            Property(p => p.FileStorage).HasColumnName("FileStorage");
            Property(p => p.Abstract).HasColumnName("Abstract");
            Property(p => p.Note).HasColumnName("Note");
            Property(p => p.Uploader).HasColumnName("Uploader");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.CreateTime).HasColumnName("CreateTime").HasColumnType("datetime2");
            Property(p => p.Status).HasColumnName("Status");
        }
    }
}