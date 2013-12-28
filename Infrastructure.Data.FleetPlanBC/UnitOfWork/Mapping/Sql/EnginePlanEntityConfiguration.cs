#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:43:26
// 文件名：EnginePlanEntityConfiguration
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
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     EnginePlan实体相关配置
    /// </summary>
    internal class EnginePlanEntityConfiguration : EntityTypeConfiguration<EnginePlan>
    {
        public EnginePlanEntityConfiguration()
        {
            ToTable("EnginePlan", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);


        }
    }
}
