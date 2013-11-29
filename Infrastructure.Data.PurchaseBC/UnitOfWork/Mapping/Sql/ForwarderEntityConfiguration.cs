#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，17:11
// 文件名：ForwarderEntityConfiguration.cs
// 程序集：UniCloud.Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.Aggregates.ForwarderAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Forwarder实体相关配置
    /// </summary>
    internal class ForwarderEntityConfiguration : EntityTypeConfiguration<Forwarder>
    {
        public ForwarderEntityConfiguration()
        {
            ToTable("Forwarder", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.CnName).HasColumnName("CnName");
            Property(p => p.EnName).HasColumnName("EnName");
            Property(p => p.CnName).HasColumnName("CnName");
            Property(p => p.Tel).HasColumnName("Tel");
            Property(p => p.Attn).HasColumnName("Attn");
            Property(p => p.Fax).HasColumnName("Fax");
            Property(p => p.Email).HasColumnName("Email");
        }
    }
}