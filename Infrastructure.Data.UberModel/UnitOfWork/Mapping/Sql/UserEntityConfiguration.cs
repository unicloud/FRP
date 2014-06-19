#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：chency 时间：2014/2/21 14:58:58

// 文件名：UserEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.UserAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     User实体相关配置
    /// </summary>
    internal class UserEntityConfiguration : EntityTypeConfiguration<User>
    {
        public UserEntityConfiguration()
        {
            ToTable("User", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.UserName).HasColumnName("UserName").HasMaxLength(100);
            Property(p => p.LoweredUserName).HasColumnName("LoweredUserName").HasMaxLength(100);
            Property(p => p.OrganizationNo).HasColumnName("OrganizationNo");
            Property(p => p.FirstName).HasColumnName("FirstName").HasMaxLength(100);
            Property(p => p.LastName).HasColumnName("LastName").HasMaxLength(100);
            Property(p => p.DisplayName).HasColumnName("DisplayName").HasMaxLength(100);
            Property(p => p.Password).HasColumnName("Password").HasMaxLength(100);
            Property(p => p.PasswordFormat).HasColumnName("PasswordFormat");
            Property(p => p.PasswordQuestion).HasColumnName("PasswordQuestion").HasMaxLength(100);
            Property(p => p.PasswordAnswer).HasColumnName("PasswordAnswer").HasMaxLength(100);
            Property(p => p.Email).HasColumnName("Email").HasMaxLength(50);
            Property(p => p.Mobile).HasColumnName("Mobile").HasMaxLength(50);
            Property(p => p.Comment).HasColumnName("Comment").HasMaxLength(200);
            Property(p => p.IsApproved).HasColumnName("IsApproved");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.IsLockedOut).HasColumnName("IsLockedOut");
            Property(p => p.FailedPasswordAttemptCount).HasColumnName("FailedPasswordAttemptCount");
            Property(p => p.FailedPasswordAnswerAttemptCount).HasColumnName("FailedPasswordAnswerAttemptCount");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.LastPasswordChangedDate).HasColumnName("LastPasswordChangedDate").HasColumnType("datetime2");
            Property(p => p.LastLockoutDate).HasColumnName("LastLockoutDate").HasColumnType("datetime2");
            Property(p => p.FailedPasswordAttemptWindowStart)
                .HasColumnName("FailedPasswordAttemptWindowStart")
                .HasColumnType("datetime2");
            Property(p => p.FailedPasswordAnswerAttemptWindowStart)
                .HasColumnName("FailedPasswordAnswerAttemptWindowStart")
                .HasColumnType("datetime2");
            Property(p => p.LastActivityDate).HasColumnName("LastActivityDate").HasColumnType("datetime2");
            Property(p => p.LastLoginDate).HasColumnName("LastLoginDate").HasColumnType("datetime2");

            HasMany(p => p.UserRoles).WithOptional().HasForeignKey(p => p.UserId);
        }
    }
}