#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/17 15:24:45
// 文件名：AirframeMaintainContractEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/17 15:24:45
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.MaintainContractAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AirframeMaintainContract实体相关配置
    /// </summary>
    internal class AirframeMaintainContractEntityConfiguration : EntityTypeConfiguration<AirframeMaintainContract>
    {
      public AirframeMaintainContractEntityConfiguration()
        {
            ToTable("AirframeMaintainContract", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
