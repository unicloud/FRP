#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/25 16:03:50
// 文件名：RelatedDocEntityConfiguration
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
using UniCloud.Domain.FleetPlanBC.Aggregates.RelatedDocAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     RelatedDoc实体相关配置
    /// </summary>
    internal class RelatedDocEntityConfiguration : EntityTypeConfiguration<RelatedDoc>
    {
        public RelatedDocEntityConfiguration()
        {
            ToTable("RelatedDoc", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.SourceId).HasColumnName("SourceId");
            Property(p => p.DocumentId).HasColumnName("DocumentId");
            Property(p => p.DocumentName).HasColumnName("DocumentName");
        }
    }
}
