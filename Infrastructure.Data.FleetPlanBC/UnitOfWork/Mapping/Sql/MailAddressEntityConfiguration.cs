#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:44:37
// 文件名：MailAddressEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.MailAddressAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     MailAddress实体相关配置
    /// </summary>
    internal class MailAddressEntityConfiguration : EntityTypeConfiguration<MailAddress>
    {
        public MailAddressEntityConfiguration()
        {
            ToTable("MailAddress", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.SmtpHost).HasColumnName("SmtpHost");
            Property(p => p.Pop3Host).HasColumnName("Pop3Host");
            Property(p => p.SendPort).HasColumnName("SendPort");
            Property(p => p.ReceivePort).HasColumnName("ReceivePort");
            Property(p => p.LoginUser).HasColumnName("LoginUser");
            Property(p => p.LoginPassword).HasColumnName("LoginPassword");
            Property(p => p.Address).HasColumnName("Address");
            Property(p => p.DisplayName).HasColumnName("DisplayName");
            Property(p => p.SendSSL).HasColumnName("SendSSL");
            Property(p => p.StartTLS).HasColumnName("StartTLS");
            Property(p => p.ReceiveSSL).HasColumnName("ReceiveSSL");
            Property(p => p.ServerType).HasColumnName("ServerType");

        }
    }
}
