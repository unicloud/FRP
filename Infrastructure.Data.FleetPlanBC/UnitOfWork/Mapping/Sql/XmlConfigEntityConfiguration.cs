#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:47:12
// 文件名：XmlConfigEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     XmlConfig实体相关配置
    /// </summary>
    internal class XmlConfigEntityConfiguration : EntityTypeConfiguration<XmlConfig>
    {
        public XmlConfigEntityConfiguration()
        {
            ToTable("XmlConfig", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Ignore(x => x.XmlContent);

            Property(p => p.ConfigType).HasColumnName("ConfigType");
            Property(p => p.ConfigContent).HasColumnName("ConfigContent").HasColumnType("xml");
            Property(p => p.VersionNumber).HasColumnName("VersionNumber");

        }
    }
}
