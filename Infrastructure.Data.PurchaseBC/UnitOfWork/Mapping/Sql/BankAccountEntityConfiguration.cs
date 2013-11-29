#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，17:11
// 文件名：BankAccountEntityConfiguration.cs
// 程序集：UniCloud.Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.Aggregates.BankAccountAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     BankAccount实体相关配置
    /// </summary>
    internal class BankAccountEntityConfiguration : EntityTypeConfiguration<BankAccount>
    {
        public BankAccountEntityConfiguration()
        {
            ToTable("BankAccount", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Account).HasColumnName("Account");
            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Bank).HasColumnName("Bank");
            Property(p => p.Branch).HasColumnName("Branch");
            Property(p => p.Country).HasColumnName("Country");
            Property(p => p.Address).HasColumnName("Address");
            Property(p => p.IsCurrent).HasColumnName("IsCurrent");

            Property(p => p.SupplierId).HasColumnName("SupplierId");
        }
    }
}