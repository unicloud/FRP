#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/22 16:35:51
// 文件名：OtherSupplierEntityConfiguration
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/22 16:35:51
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierRoleAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     MaintainSupplier实体相关配置
    /// </summary>
    public class OtherSupplierEntityConfiguration : EntityTypeConfiguration<OtherSupplier>
    {
        public OtherSupplierEntityConfiguration()
        {
            ToTable("OtherSupplier", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

        }
    }
}
