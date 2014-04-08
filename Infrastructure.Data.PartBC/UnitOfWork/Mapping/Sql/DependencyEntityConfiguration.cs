#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:48

// 文件名：DependencyEntityConfiguration
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
    ///     Dependency实体相关配置
    /// </summary>
    internal class DependencyEntityConfiguration : EntityTypeConfiguration<Dependency>
    {
        public DependencyEntityConfiguration()
        {
            ToTable("Dependency", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.InstallControllerId).HasColumnName("InstallControllerId");
            Property(p => p.DependencyPnId).HasColumnName("DependencyPnId");
            Property(p => p.Pn).HasColumnName("Pn").HasMaxLength(100);
        }
    }
}