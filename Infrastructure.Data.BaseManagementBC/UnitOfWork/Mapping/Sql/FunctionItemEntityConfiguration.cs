#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：FunctionItemEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.BaseManagementBC.Aggregates.FunctionItemAgg;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork.Mapping.Sql
{
   /// <summary>
   /// FunctionItem实体相关配置
   /// </summary>
   internal class FunctionItemEntityConfiguration: EntityTypeConfiguration<FunctionItem>
   {
      public FunctionItemEntityConfiguration()
      {
         ToTable("FunctionItem", DbConfig.Schema);
         
         HasKey(p => p.Id);
         Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
         Property(p => p.ParentItemId).HasColumnName("ParentItemId");
         Property(p => p.IsLeaf).HasColumnName("IsLeaf");
         Property(p => p.Sort).HasColumnName("Sort");
         Property(p => p.Name).HasColumnName("Name").HasMaxLength(100);
         Property(p => p.Description).HasColumnName("Description").HasMaxLength(100);
         Property(p => p.NaviUrl).HasColumnName("NaviUrl").HasMaxLength(100);
         Property(p => p.IsValid).HasColumnName("IsValid");
         Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
         Property(p => p.ImageUrl).HasColumnName("ImageUrl").HasMaxLength(100);
         Property(p => p.IsButton).HasColumnName("IsButton");
         HasMany(p => p.SubFunctionItems).WithOptional().HasForeignKey(p => p.ParentItemId);
      }
      
   }
}
