#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 14:08:45

// 文件名：ScnLineEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
   /// <summary>
   /// ScnLine实体相关配置
   /// </summary>
   internal class ScnLineEntityConfiguration: EntityTypeConfiguration<ScnLine>
   {
      public ScnLineEntityConfiguration()
      {
         ToTable("ScnLine", DbConfig.Schema);
         
         HasKey(p => p.Id);
         Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
      }
      
   }
}
