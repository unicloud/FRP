#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，14:12
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.OrderAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Order实体相关配置
    /// </summary>
    internal class OrderEntityConfiguration : EntityTypeConfiguration<Order>
    {
        public OrderEntityConfiguration()
        {
            ToTable("Order", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Version).HasColumnName("Version");
            Property(p => p.ContractNumber).HasColumnName("ContractNumber");
            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.OperatorName).HasColumnName("OperatorName");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.OrderDate).HasColumnName("OrderDate").HasColumnType("datetime2");
            Property(p => p.RepealDate).HasColumnName("RepealDate").HasColumnType("datetime2");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.IsCompleted).HasColumnName("IsCompleted");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.ContractName).HasColumnName("ContractName");
            Property(p => p.ContractDocGuid).HasColumnName("ContractDocGuid");
            Property(p => p.SourceGuid).HasColumnName("SourceGuid");
            Property(p => p.Note).HasColumnName("Note");

            Property(p => p.CurrencyId).HasColumnName("CurrencyId");
            Property(p => p.LinkmanId).HasColumnName("LinkmanId");

            HasRequired(o => o.Currency).WithMany().HasForeignKey(o => o.CurrencyId);
            HasOptional(o => o.Linkman).WithMany().HasForeignKey(o => o.LinkmanId);
            HasMany(o => o.OrderLines).WithRequired().HasForeignKey(o => o.OrderId);
        }
    }
}