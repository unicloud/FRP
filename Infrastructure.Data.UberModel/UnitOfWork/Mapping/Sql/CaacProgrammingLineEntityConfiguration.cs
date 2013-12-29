#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/29 11:15:02
// 文件名：CaacProgrammingLineEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.CaacPorgrammingAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     CaacProgrammingLine实体相关配置
    /// </summary>
    internal class CaacProgrammingLineEntityConfiguration : EntityTypeConfiguration<CaacProgrammingLine>
    {
        public CaacProgrammingLineEntityConfiguration()
        {
            ToTable("CaacProgrammingLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Number).HasColumnName("Number");
            Property(p => p.Year).HasColumnName("Year");

            Property(p => p.AircraftCategoryId).HasColumnName("AircraftCategoryId");
            Property(p => p.CaacProgrammingId).HasColumnName("CaacProgrammingId");

            HasRequired(o => o.AircraftCategory).WithMany().HasForeignKey(o => o.AircraftCategoryId);

        }
    }
}
