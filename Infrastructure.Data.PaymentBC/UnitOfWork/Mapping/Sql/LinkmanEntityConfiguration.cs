#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，14:12
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.LinkmanAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Linkman实体相关配置
    /// </summary>
    internal class LinkmanEntityConfiguration : EntityTypeConfiguration<Linkman>
    {
        public LinkmanEntityConfiguration()
        {
            ToTable("Linkman", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.IsDefault).HasColumnName("IsDefault");
            Property(p => p.TelePhone).HasColumnName("TelePhone");
            Property(p => p.Mobile).HasColumnName("Mobile");
            Property(p => p.Fax).HasColumnName("Fax");
            Property(p => p.Email).HasColumnName("Email");
            Property(p => p.Department).HasColumnName("Department");
            Property(p => p.Note).HasColumnName("Note");
        }
    }
}