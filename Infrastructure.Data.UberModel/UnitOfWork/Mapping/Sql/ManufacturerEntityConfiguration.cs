#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，22:11
// 文件名：ManufacturerEntityConfiguration.cs
// 程序集：UniCloud.Infrastructure.Data.UberModel
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.UberModel.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Infrastructure.Data.UberModel.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Manufacturer实体相关配置
    /// </summary>
    internal class ManufacturerEntityConfiguration : EntityTypeConfiguration<Manufacturer>
    {
        public ManufacturerEntityConfiguration()
        {
            ToTable("Manufacturer", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.CnName).HasColumnName("CnName");
            Property(p => p.EnName).HasColumnName("EnName");
            Property(p => p.CnShortName).HasColumnName("CnShortName");
            Property(p => p.EnShortName).HasColumnName("EnShortName");
            Property(p => p.Note).HasColumnName("Note");
        }
    }
}