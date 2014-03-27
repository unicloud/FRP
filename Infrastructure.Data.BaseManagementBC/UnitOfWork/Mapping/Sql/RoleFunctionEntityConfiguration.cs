#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：RoleFunctionEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.BaseManagementBC.Aggregates.RoleFunctionAgg;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork.Mapping.Sql
{
   /// <summary>
   /// RoleFunction实体相关配置
   /// </summary>
   internal class RoleFunctionEntityConfiguration: EntityTypeConfiguration<RoleFunction>
   {
      public RoleFunctionEntityConfiguration()
      {
         ToTable("RoleFunction", DbConfig.Schema);
         
         HasKey(p => p.Id);
         Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         Property(p => p.FunctionItemId).HasColumnName("FunctionItemId");
         Property(p => p.RoleId).HasColumnName("RoleId");

         HasRequired(o => o.FunctionItem).WithMany().HasForeignKey(o => o.FunctionItemId);
      }
      
   }
}
