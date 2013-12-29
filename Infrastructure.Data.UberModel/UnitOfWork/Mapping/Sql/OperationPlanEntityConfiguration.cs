#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/29 11:16:48
// 文件名：OperationPlanEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AircraftPlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     OperationPlan实体相关配置
    /// </summary>
    internal class OperationPlanEntityConfiguration : EntityTypeConfiguration<OperationPlan>
    {
        public OperationPlanEntityConfiguration()
        {
            ToTable("OperationPlan", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.OperationHistoryId).HasColumnName("OperationHistoryId");

            HasOptional(o => o.OperationHistory).WithMany().HasForeignKey(o => o.OperationHistoryId);

        }
    }
}
