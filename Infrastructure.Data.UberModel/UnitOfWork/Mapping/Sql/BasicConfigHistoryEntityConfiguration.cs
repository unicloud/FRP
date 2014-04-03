#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 17:59:33
// 文件名：BasicConfigHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.BasicConfigHistoryAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     BasicConfigHistory实体相关配置
    /// </summary>
    internal class BasicConfigHistoryEntityConfiguration : EntityTypeConfiguration<BasicConfigHistory>
    {
        public BasicConfigHistoryEntityConfiguration()
        {
            ToTable("BasicConfigHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");

            Property(p => p.ContractAircraftId).HasColumnName("ContractAircraftId");
            Property(p => p.BasicConfigGroupId).HasColumnName("BasicConfigGroupId");

            HasRequired(p => p.BasicConfigGroup).WithMany().HasForeignKey(o => o.BasicConfigGroupId);
        }
    }
}