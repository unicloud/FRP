#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，17:11
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
using UniCloud.Domain.PurchaseBC.Aggregates.MaintainContractAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     MaintainContract实体相关配置
    /// </summary>
    internal class MaintainContractEntityConfiguration : EntityTypeConfiguration<MaintainContract>
    {
        public MaintainContractEntityConfiguration()
        {
            ToTable("MaintainContract", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Number).HasColumnName("Number");
            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.Signatory).HasColumnName("Signatory");
            Property(p => p.SignDate).HasColumnName("SignDate").HasColumnType("datetime2");
            Property(p => p.Abstract).HasColumnName("Abstract");
            Property(p => p.DocumentName).HasColumnName("DocumentName");
            Property(p => p.DocumentId).HasColumnName("DocumentId");
            Property(p => p.SignatoryId).HasColumnName("SignatoryId");

            HasRequired(m => m.Supplier).WithMany().HasForeignKey(m => m.SignatoryId);
        }
    }
}