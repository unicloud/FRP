#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/2 17:58:12
// 文件名：SnHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SnHistory实体相关配置
    /// </summary>
    internal class SnHistoryEntityConfiguration : EntityTypeConfiguration<SnHistory>
    {
        public SnHistoryEntityConfiguration()
        {
            ToTable("SnHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Sn).HasColumnName("Sn").HasMaxLength(100);
            Property(p => p.SnRegId).HasColumnName("SnRegId");
            Property(p => p.Pn).HasColumnName("Pn").HasMaxLength(100);
            Property(p => p.PnRegId).HasColumnName("PnRegId");
            Property(p => p.ActionDate).HasColumnName("ActionDate").HasColumnType("datetime2");
            Property(p => p.ActionType).HasColumnName("ActionType");
            Property(p => p.RemInstRecordId).HasColumnName("RemInstRecordId");
            Property(p => p.CSN).HasColumnName("CSN");
            Property(p => p.TSN).HasColumnName("TSN");
            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");

            HasOptional(o => o.RemInstRecord).WithMany().HasForeignKey(o => o.RemInstRecordId);
        }
    }
}