#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:33:59
// 文件名：AircraftCategoryEntityConfiguration
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
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
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


        }
    }
}
