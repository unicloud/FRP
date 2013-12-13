#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，18:11
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
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Reception实体相关配置
    /// </summary>
    internal class ReceptionEntityConfiguration : EntityTypeConfiguration<Reception>
    {
        public ReceptionEntityConfiguration()
        {
            ToTable("Reception", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ReceptionNumber).HasColumnName("ReceptionNumber");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.IsClosed).HasColumnName("IsClosed");
            Property(p => p.CloseDate).HasColumnName("CloseDate").HasColumnType("datetime2").IsOptional();
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2").IsOptional();
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.SourceId).HasColumnName("SourceId");

            Property(p => p.SupplierId).HasColumnName("SupplierId");

            HasRequired(r => r.Supplier).WithMany().HasForeignKey(r => r.SupplierId);
            HasMany(r => r.ReceptionLines).WithRequired().HasForeignKey(r => r.ReceptionId);
            HasMany(r => r.ReceptionSchedules).WithRequired().HasForeignKey(r => r.ReceptionId);
        }
    }
}