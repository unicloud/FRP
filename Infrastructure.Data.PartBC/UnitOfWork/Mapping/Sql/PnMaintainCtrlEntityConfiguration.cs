#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 16:16:57

// 文件名：PnMaintainCtrlEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
   /// <summary>
   /// PnMaintainCtrl实体相关配置
   /// </summary>
   internal class PnMaintainCtrlEntityConfiguration: EntityTypeConfiguration<PnMaintainCtrl>
   {
      public PnMaintainCtrlEntityConfiguration()
      {
         ToTable("PnMaintainCtrl", DbConfig.Schema);
         
         HasKey(p => p.Id);
         Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
         Property(p => p.Pn).HasColumnName("Pn").HasMaxLength(100);
         Property(p => p.PnRegId).HasColumnName("PnRegId");
      }
      
   }
}
