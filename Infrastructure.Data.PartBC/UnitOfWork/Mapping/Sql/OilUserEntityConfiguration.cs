#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/02/27，16:06
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
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     OilUser实体相关配置
    /// </summary>
    internal class OilUserEntityConfiguration : EntityTypeConfiguration<OilUser>
    {
        public OilUserEntityConfiguration()
        {
            ToTable("OilUser", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Sn).HasColumnName("Sn");
            Property(p => p.TSN).HasColumnName("TSN");
            Property(p => p.TSR).HasColumnName("TSR");
            Property(p => p.CSN).HasColumnName("CSN");
            Property(p => p.CSR).HasColumnName("CSR");
            Property(p => p.NeedMonitor).HasColumnName("NeedMonitor");
            Property(p => p.MonitorStatus).HasColumnName("MonitorStatus");

            Property(p => p.SnRegID).HasColumnName("SnRegID");

            HasMany(u => u.OilMonitors).WithRequired().HasForeignKey(u => u.OilUserID);
        }
    }
}