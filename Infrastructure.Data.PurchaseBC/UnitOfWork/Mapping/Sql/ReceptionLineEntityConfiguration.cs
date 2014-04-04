#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，20:11
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
    ///     ReceptionLine实体相关配置
    /// </summary>
    internal class ReceptionLineEntityConfiguration : EntityTypeConfiguration<ReceptionLine>
    {
        public ReceptionLineEntityConfiguration()
        {
            ToTable("ReceptionLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ReceivedAmount).HasColumnName("ReceivedAmount");
            Property(p => p.AcceptedAmount).HasColumnName("AcceptedAmount");
            Property(p => p.IsCompleted).HasColumnName("IsCompleted");
            Property(p => p.Note).HasColumnName("Note");
            Property(p => p.DeliverDate).HasColumnName("DeliverDate").HasColumnType("datetime2");
            Property(p => p.DeliverPlace).HasColumnName("DeliverPlace");

            Property(p => p.ReceptionId).HasColumnName("ReceptionId");
        }
    }
}