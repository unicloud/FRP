﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:26:26
// 文件名：ActionCategoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ActionCategoryAgg;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     ActionCategory实体相关配置
    /// </summary>
    internal class ActionCategoryEntityConfiguration : EntityTypeConfiguration<ActionCategory>
    {
        public ActionCategoryEntityConfiguration()
        {
            ToTable("ActionCategory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.ActionType).HasColumnName("ActionType");
            Property(p => p.ActionName).HasColumnName("ActionName");
            Property(p => p.NeedRequest).HasColumnName("NeedRequest");

        }
    }
}
