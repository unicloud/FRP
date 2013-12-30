#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/29 11:12:51
// 文件名：AircraftCategoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.AircraftCategoryAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftCategory实体相关配置
    /// </summary>
    internal class AircraftCategoryEntityConfiguration : EntityTypeConfiguration<AircraftCategory>
    {
        public AircraftCategoryEntityConfiguration()
        {
            ToTable("AircraftCategory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Category).HasColumnName("Category");
            Property(p => p.Regional).HasColumnName("Regional");


        }
    }
}
