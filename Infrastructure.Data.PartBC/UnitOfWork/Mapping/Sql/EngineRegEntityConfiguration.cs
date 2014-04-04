#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/22，21:12
// 方案：FRP
// 项目：Infrastructure.Data.PartBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     EngineReg实体相关配置
    /// </summary>
    internal class EngineRegEntityConfiguration : EntityTypeConfiguration<EngineReg>
    {
        public EngineRegEntityConfiguration()
        {
            ToTable("EngineReg", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.NeedMonitor).HasColumnName("NeedMonitor");
            Property(p => p.MonitorStatus).HasColumnName("MonitorStatus");

            Property(p => p.ThrustId).HasColumnName("ThrustId");

            HasRequired(e => e.Thrust).WithMany().HasForeignKey(e => e.ThrustId);
        }
    }
}