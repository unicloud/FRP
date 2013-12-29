#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/29 11:14:19
// 文件名：AirProgrammingLineEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AirProgrammingAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AirProgrammingLine实体相关配置
    /// </summary>
    internal class AirProgrammingLineEntityConfiguration : EntityTypeConfiguration<AirProgrammingLine>
    {
        public AirProgrammingLineEntityConfiguration()
        {
            ToTable("AirProgrammingLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Year).HasColumnName("Year");
            Property(p => p.BuyNum).HasColumnName("BuyNum");
            Property(p => p.ExportNum).HasColumnName("ExportNum");
            Property(p => p.LeaseNum).HasColumnName("LeaseNum");

            Property(p => p.AcTypeId).HasColumnName("AcTypeId");
            Property(p => p.AircraftCategoryId).HasColumnName("AircraftCategoryId");
            Property(p => p.AirProgrammingId).HasColumnName("AirProgrammingId");

            HasRequired(o => o.AircraftCategory).WithMany().HasForeignKey(o => o.AircraftCategoryId);

        }
    }
}
