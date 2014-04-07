#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 10:02:23
// 文件名：ItemMaintainCtrlEntityConfiguration
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
using UniCloud.Domain.UberModel.Aggregates.MaintainCtrlAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// PnReg实体相关配置
    /// </summary>
    internal class ItemMaintainCtrlEntityConfiguration : EntityTypeConfiguration<ItemMaintainCtrl>
    {
        public ItemMaintainCtrlEntityConfiguration()
        {
            ToTable("ItemMaintainCtrl", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ItemNo).HasColumnName("ItemNo").HasMaxLength(100);
            Property(p => p.ItemId).HasColumnName("ItemId");

            HasRequired(o => o.Item).WithMany().HasForeignKey(o => o.ItemId);
        }
    }
}
