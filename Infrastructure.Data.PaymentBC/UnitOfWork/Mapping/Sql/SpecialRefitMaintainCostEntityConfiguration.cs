#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 9:43:21
// 文件名：SpecialRefitMaintainCostEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 9:43:21
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainCostAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     SpecialRefitMaintainCost实体相关配置
    /// </summary>
    internal class SpecialRefitMaintainCostEntityConfiguration : EntityTypeConfiguration<SpecialRefitMaintainCost>
    {
        public SpecialRefitMaintainCostEntityConfiguration()
        {
            ToTable("SpecialRefitMaintainCost", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Project).HasColumnName("Project");
            Property(p => p.Info).HasColumnName("Info");
            Property(p => p.Note).HasColumnName("Note");
        }
    }
}
