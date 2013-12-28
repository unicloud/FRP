#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:34:29
// 文件名：PlanEntityConfiguration
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
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Plan实体相关配置
    /// </summary>
    internal class PlanEntityConfiguration : EntityTypeConfiguration<Plan>
    {
        public PlanEntityConfiguration()
        {
            ToTable("Plan", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


        }
    }
}
