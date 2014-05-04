#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:47:01
// 文件名：XmlSettingEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.BaseManagementBC.Aggregates.XmlSettingAgg;

#endregion

namespace UniCloud.Infrastructure.Data.BaseManagementBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     XmlSetting实体相关配置
    /// </summary>
    internal class XmlSettingEntityConfiguration : EntityTypeConfiguration<XmlSetting>
    {
        public XmlSettingEntityConfiguration()
        {
            ToTable("XmlSetting", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.SettingType).HasColumnName("SettingType");
            Property(p => p.SettingContent).HasColumnName("SettingContent").HasColumnType("xml");
            
        }
    }
}
