#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，21:11
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
using UniCloud.Domain.UberModel.Aggregates.ContractEngineAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     ContractEngine实体相关配置
    /// </summary>
    internal class ContractEngineEntityConfiguration : EntityTypeConfiguration<ContractEngine>
    {
        public ContractEngineEntityConfiguration()
        {
            ToTable("ContractEngine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ContractName).HasColumnName("ContractName");
            Property(p => p.ContractNumber).HasColumnName("ContractNumber");
            Property(p => p.RankNumber).HasColumnName("RankNumber");
            Property(p => p.SerialNumber).HasColumnName("SerialNumber");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.ReceivedAmount).HasColumnName("ReceivedAmount");
            Property(p => p.AcceptedAmount).HasColumnName("AcceptedAmount");
            Property(p => p.Status).HasColumnName("Status");

            Property(p => p.PartId).HasColumnName("PartID");
            Property(p => p.ImportCategoryId).HasColumnName("ImportCategoryId");
            Property(p => p.SupplierId).HasColumnName("SupplierId");

            HasRequired(c => c.Part).WithMany().HasForeignKey(c => c.PartId);
            HasRequired(c => c.ImportCategory).WithMany().HasForeignKey(c => c.ImportCategoryId);
        }
    }
}