#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:35:26
// 文件名：AirlinesEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AirlinesAgg;

#endregion

namespace UniCloud.Infrastructure.Data.AircraftConfigBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Airlines实体相关配置
    /// </summary>
    internal class AirlinesEntityConfiguration : EntityTypeConfiguration<Airlines>
    {
        public AirlinesEntityConfiguration()
        {
            ToTable("Airlines", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.CnName).HasColumnName("CnName");
            Property(p => p.EnName).HasColumnName("EnName");
            Property(p => p.CnShortName).HasColumnName("CnShortName");
            Property(p => p.EnShortName).HasColumnName("EnShortName");
            Property(p => p.ICAOCode).HasColumnName("ICAOCode");
            Property(p => p.IATACode).HasColumnName("IATACode");
            Property(p => p.IsCurrent).HasColumnName("IsCurrent");

        }
    }
}
