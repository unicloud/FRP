#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:27:21
// 文件名：ManufacturerEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.AircraftConfigBC.Aggregates.ManufacturerAgg;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql
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
            Property(p => p.Type).HasColumnName("Type");

        }
    }
}
