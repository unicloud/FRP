#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/29 14:00:10
// 文件名：MaintainPaymentScheduleEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/29 14:00:10
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.PaymentScheduleAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
   /// <summary>
    ///      MaintainPaymentSchedule实体相关配置
    /// </summary>
    internal class MaintainPaymentScheduleEntityConfiguration: EntityTypeConfiguration< MaintainPaymentSchedule>
    {
        public MaintainPaymentScheduleEntityConfiguration()
        {
            ToTable("MaintainPaymentSchedule", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
