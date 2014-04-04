#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:57

// 文件名：MaintainCtrlLineEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.MaintainCtrlAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     MaintainCtrlLine实体相关配置
    /// </summary>
    internal class MaintainCtrlLineEntityConfiguration : EntityTypeConfiguration<MaintainCtrlLine>
    {
        public MaintainCtrlLineEntityConfiguration()
        {
            ToTable("MaintainCtrlLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.StandardInterval).HasColumnName("StandardInterval");
            Property(p => p.MaxInterval).HasColumnName("MaxInterval");
            Property(p => p.MinInterval).HasColumnName("MinInterval");
            Property(p => p.CtrlUnitId).HasColumnName("CtrlUnitId");
            Property(p => p.MaintainWorkId).HasColumnName("MaintainWorkId");
            Property(p => p.MaintainCtrlId).HasColumnName("MaintainCtrlId");

            HasRequired(p => p.CtrlUnit).WithMany().HasForeignKey(o => o.CtrlUnitId);
            HasRequired(p => p.MaintainWork).WithMany().HasForeignKey(m => m.MaintainWorkId);
        }
    }
}