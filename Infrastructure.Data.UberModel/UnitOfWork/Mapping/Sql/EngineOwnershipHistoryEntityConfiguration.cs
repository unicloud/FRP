#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/29 11:15:32
// 文件名：EngineOwnershipHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.EngineAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     EngineOwnershipHistory实体相关配置
    /// </summary>
    internal class EngineOwnershipHistoryEntityConfiguration : EntityTypeConfiguration<EngineOwnershipHistory>
    {
        public EngineOwnershipHistoryEntityConfiguration()
        {
            ToTable("EngineOwnershipHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");

            Property(p => p.EngineId).HasColumnName("EngineId");
            Property(p => p.SupplierId).HasColumnName("SupplierId");

            HasRequired(o => o.Supplier).WithMany().HasForeignKey(o => o.SupplierId);

        }
    }
}
