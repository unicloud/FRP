#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:27:46
// 文件名：SupplierEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Supplier实体相关配置
    /// </summary>
    internal class SupplierEntityConfiguration : EntityTypeConfiguration<Supplier>
    {
        public SupplierEntityConfiguration()
        {
            ToTable("Supplier", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.Code).HasColumnName("Code");
            Property(p => p.CnName).HasColumnName("CnName");
            Property(p => p.EnName).HasColumnName("EnName");
            Property(p => p.CnShortName).HasColumnName("CnShortName");
            Property(p => p.EnShortName).HasColumnName("EnShortName");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime2");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.Note).HasColumnName("Note");
            Property(p => p.SupplierType).HasColumnName("SupplierType");
            Property(p => p.AirlineGuid).HasColumnName("AirlineGuid");

            Property(p => p.SupplierCompanyId).HasColumnName("SupplierCompanyId");

            HasRequired(s => s.SupplierCompany).WithMany(s => s.Suppliers).HasForeignKey(s => s.SupplierCompanyId);

        }
    }
}
