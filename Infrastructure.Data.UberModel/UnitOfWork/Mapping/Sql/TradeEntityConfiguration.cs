#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，22:11
// 方案：FRP
// 项目：Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.TradeAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Trade实体相关配置
    /// </summary>
    internal class TradeEntityConfiguration : EntityTypeConfiguration<Trade>
    {
        public TradeEntityConfiguration()
        {
            ToTable("Trade", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.TradeNumber).HasColumnName("TradeNumber");
            Property(p => p.TradeType).HasColumnName("TradeType");
            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.Description).HasColumnName("Description");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.IsClosed).HasColumnName("IsClosed");
            Property(p => p.CloseDate).HasColumnName("CloseDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.Signatory).HasColumnName("Signatory");
            Property(p => p.Note).HasColumnName("Note");

            Property(p => p.SupplierId).HasColumnName("SupplierId");

            HasRequired(t => t.Supplier).WithMany().HasForeignKey(t => t.SupplierId);
        }
    }
}