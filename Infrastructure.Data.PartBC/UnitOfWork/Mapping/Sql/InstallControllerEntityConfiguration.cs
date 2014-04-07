#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/7 16:27:42
// 文件名：InstallControllerEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     InstallController实体相关配置
    /// </summary>
    internal class InstallControllerEntityConfiguration : EntityTypeConfiguration<InstallController>
    {
        public InstallControllerEntityConfiguration()
        {
            ToTable("InstallController", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.PnRegId).HasColumnName("PnRegId");
            Property(p => p.ItemId).HasColumnName("ItemId");
            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");

            HasRequired(p => p.AircraftType).WithMany().HasForeignKey(o => o.AircraftTypeId);
            HasRequired(p => p.Item).WithMany().HasForeignKey(o => o.ItemId);
            HasRequired(p => p.PnReg).WithMany().HasForeignKey(o => o.PnRegId);

            HasMany(o => o.Dependencies).WithRequired().HasForeignKey(o => o.InstallControllerId);
        }
    }
}