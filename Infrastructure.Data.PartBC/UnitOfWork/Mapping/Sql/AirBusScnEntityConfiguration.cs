#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/26 17:36:12
// 文件名：AirBusScnEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/26 17:36:12
// 修改说明：
// ========================================================================*/
#endregion

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg;

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// AirBusScn实体相关配置
    /// </summary>
    internal class AirBusScnEntityConfiguration : EntityTypeConfiguration<AirBusScn>
    {
        public AirBusScnEntityConfiguration()
        {
            ToTable("AirBusScn", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.Title).HasColumnName("Title");
            Property(p => p.CSCNumber).HasColumnName("CSCNumber").HasMaxLength(100);
            Property(p => p.ModNumber).HasColumnName("ModNumber").HasMaxLength(100);
            Property(p => p.ScnNumber).HasColumnName("ScnNumber").HasMaxLength(100);
            Property(p => p.Description).HasColumnName("Description").HasMaxLength(100);
            Property(p => p.ScnStatus).HasColumnName("ScnStatus");
        }

    }
}
