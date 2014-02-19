#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：BasicConfigGroupEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.BasicConfigGroupAgg;
#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// BasicConfigGroup实体相关配置
    /// </summary>
    internal class BasicConfigGroupEntityConfiguration : EntityTypeConfiguration<BasicConfigGroup>
    {
        public BasicConfigGroupEntityConfiguration()
        {
            ToTable("BasicConfigGroup", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            //Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datatime2");
            //Property(p => p.Description).HasColumnName("Description").HasMaxLength(100);
            //Property(p => p.GroupNo).HasColumnName("GroupNo").HasMaxLength(100);
            //Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");

            HasRequired(p => p.AircraftType).WithMany().HasForeignKey(o => o.AircraftTypeId);

            HasMany(o => o.BasicConfigs).WithRequired().HasForeignKey(o => o.BasicConfigGroupId);

        }

    }
}
