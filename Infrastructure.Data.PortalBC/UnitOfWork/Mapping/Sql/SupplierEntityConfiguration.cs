#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2014/06/21，10:06
// 方案：FRP
// 项目：Infrastructure.Data.PortalBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PortalBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PortalBC.UnitOfWork.Mapping.Sql
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
            Property(p => p.AirlineGuid).HasColumnName("AirlineGuid");
        }
    }
}