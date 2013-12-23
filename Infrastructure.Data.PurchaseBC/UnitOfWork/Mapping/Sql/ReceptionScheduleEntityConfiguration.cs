#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/11/20 14:49:23
// 文件名：ReceptionScheduleEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     ReceptionSchedule实体相关配置
    /// </summary>
    internal class ReceptionScheduleEntityConfiguration : EntityTypeConfiguration<ReceptionSchedule>
    {
        public ReceptionScheduleEntityConfiguration()
        {
            ToTable("ReceptionSchedule", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Subject).HasColumnName("Subject");
            Property(p => p.Body).HasColumnName("Body");
            Property(p => p.Importance).HasColumnName("Importance");
            Property(p => p.Tempo).HasColumnName("Tempo");
            Property(p => p.Start).HasColumnName("Start").HasColumnType("datetime2");
            Property(p => p.End).HasColumnName("End").HasColumnType("datetime2");
            Property(p => p.IsAllDayEvent).HasColumnName("IsAllDayEvent"); 
            Property(p => p.Group).HasColumnName("Group");

            Property(p => p.ReceptionId).HasColumnName("ReceptionId");
        }
    }
}
