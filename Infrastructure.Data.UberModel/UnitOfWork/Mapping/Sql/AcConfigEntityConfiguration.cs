#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AcConfigEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AcConfig实体相关配置
    /// </summary>
    internal class AcConfigEntityConfiguration : EntityTypeConfiguration<AcConfig>
    {
        public AcConfigEntityConfiguration()
        {
            ToTable("AcConfig", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.ItemId).HasColumnName("ItemId");
            Property(p => p.Position).HasColumnName("Position");
            Property(p => p.Description).HasColumnName("Description").HasMaxLength(100);
            Property(p => p.ParentId).HasColumnName("ParentId");
            Property(p => p.RootId).HasColumnName("RootId");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");

            HasMany(o => o.SubAcConfigs).WithOptional().HasForeignKey(o => o.ParentId);
        }
    }
}