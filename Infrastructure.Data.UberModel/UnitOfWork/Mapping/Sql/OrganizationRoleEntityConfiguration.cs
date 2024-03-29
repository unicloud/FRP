﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：OrganizationRoleEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.OrganizationRoleAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
   /// <summary>
   /// OrganizationRole实体相关配置
   /// </summary>
   internal class OrganizationRoleEntityConfiguration: EntityTypeConfiguration<OrganizationRole>
   {
      public OrganizationRoleEntityConfiguration()
      {
         ToTable("OrganizationRole", DbConfig.Schema);
         
         HasKey(p => p.Id);
         Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         Property(p => p.OrganizationId).HasColumnName("OrganizationId");
         Property(p => p.RoleId).HasColumnName("RoleId");
      }
      
   }
}
