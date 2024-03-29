#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SpecialConfigEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.SpecialConfigAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SpecialConfig实体相关配置
    /// </summary>
    internal class SpecialConfigEntityConfiguration : EntityTypeConfiguration<SpecialConfig>
    {
        public SpecialConfigEntityConfiguration()
        {
            ToTable("SpecialConfig", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");

            Property(p => p.ContractAircraftId).HasColumnName("ContractAircraftId");
        }
    }
}