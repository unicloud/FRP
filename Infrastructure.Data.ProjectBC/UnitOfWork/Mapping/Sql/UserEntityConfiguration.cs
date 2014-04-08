#region 版本信息

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/01/08，17:38
// 方案：FRP
// 项目：Infrastructure.Data.ProjectBC
// 版本：V1.0.0
// 
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.ProjectBC.Aggregates.UserAgg;

#endregion

namespace UniCloud.Infrastructure.Data.ProjectBC.UnitOfWork.Mapping.Sql
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

            Property(p => p.EmployeeCode).HasColumnName("EmployeeCode");
            Property(p => p.FirstName).HasColumnName("FirstName");
            Property(p => p.LaseName).HasColumnName("LaseName");
            Property(p => p.DisplayName).HasColumnName("DisplayName");
        }
    }
}