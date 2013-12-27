#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，22:11
// 文件名：SupplierEntityConfiguration.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Supplier实体相关配置
    /// </summary>
    internal class SupplierEntityConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierEntityConfiguration()
        {
            ToTable("Supplier", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.SupplierType).HasColumnName("SupplierType");
            Property(p => p.Code).HasColumnName("Code");
            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime2");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.Note).HasColumnName("Note");
            Property(p => p.AirlineGuid).HasColumnName("AirlineGuid");
            Property(p => p.SupplierCompanyId).HasColumnName("SupplierCompanyId");

            HasRequired(s => s.SupplierCompany).WithMany(s => s.Suppliers).HasForeignKey(s => s.SupplierCompanyId);
            HasMany(s => s.BankAccounts).WithRequired().HasForeignKey(b => b.SupplierId);
        }
    }
}