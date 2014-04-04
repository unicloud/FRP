#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：OrganizationEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.OrganizationAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
   /// <summary>
   /// Organization实体相关配置
   /// </summary>
   internal class OrganizationEntityConfiguration: EntityTypeConfiguration<Organization>
   {
      public OrganizationEntityConfiguration()
      {
         ToTable("Organization", DbConfig.Schema);
         HasKey(p => p.Id);
         Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         Property(p => p.Code).HasColumnName("Code").HasMaxLength(100);
         Property(p => p.Name).HasColumnName("Name").HasMaxLength(100);
         Property(p => p.LastUpdateTime).HasColumnName("LastUpdateTime").HasColumnType("datetime2");
         Property(p => p.IsValid).HasColumnName("IsValid");
         Property(p => p.ParentCode).HasColumnName("ParentCode");
         Property(p => p.Sort).HasColumnName("Sort");
         Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
         Property(p => p.Description).HasColumnName("Description").HasMaxLength(100);
         HasMany(p => p.SubOrganizations).WithOptional().HasForeignKey(p => p.ParentCode);
      }
      
   }
}
