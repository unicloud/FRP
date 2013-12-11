#region 版本信息

// =====================================================
// 版权所有 (C) 2013 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，14:39
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.GuaranteeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Guarantee实体相关配置
    /// </summary>
    internal class GuaranteeEntityConfiguration : EntityTypeConfiguration<Guarantee>
    {
        public GuaranteeEntityConfiguration()
        {
            ToTable("Guarantee", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");
            Property(p => p.Amount).HasColumnName("Amount");
            Property(p => p.SupplierName).HasColumnName("SupplierName");
            Property(p => p.OperatorName).HasColumnName("OperatorName");
            Property(p => p.Reviewer).HasColumnName("Reviewer");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.ReviewDate).HasColumnName("ReviewDate").HasColumnType("datetime2");
            Property(p => p.Status).HasColumnName("Status");

            Property(p => p.SupplierId).HasColumnName("SupplierId");
            Property(p => p.CurrencyId).HasColumnName("CurrencyId");

            HasRequired(g => g.Supplier).WithMany().HasForeignKey(g => g.SupplierId);
            HasRequired(g => g.Currency).WithMany().HasForeignKey(g => g.CurrencyId);
        }
    }
}