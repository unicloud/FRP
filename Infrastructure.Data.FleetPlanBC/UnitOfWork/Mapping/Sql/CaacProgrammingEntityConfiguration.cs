#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:40:48
// 文件名：CaacProgrammingEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.CaacProgrammingAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     CaacProgramming实体相关配置
    /// </summary>
    internal class CaacProgrammingEntityConfiguration : EntityTypeConfiguration<CaacProgramming>
    {
        public CaacProgrammingEntityConfiguration()
        {
            ToTable("CaacProgramming", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Name).HasColumnName("Name");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.IssuedDate).HasColumnName("IssuedDate").HasColumnType("datetime2");
            Property(p => p.DocNumber).HasColumnName("DocNumber");
            Property(p => p.Note).HasColumnName("Note");
            Property(p => p.DocName).HasColumnName("DocName");

            Property(p => p.ProgrammingId).HasColumnName("ProgrammingId");
            Property(p => p.IssuedUnitId).HasColumnName("IssuedUnitId");
            Property(p => p.DocumentId).HasColumnName("DocumentId");

            HasRequired(o => o.Programming).WithMany().HasForeignKey(o => o.ProgrammingId);
            HasRequired(o => o.IssuedUnit).WithMany().HasForeignKey(o => o.IssuedUnitId);
            HasMany(o => o.CaacProgrammingLines).WithRequired().HasForeignKey(o => o.CaacProgrammingId);

        }
    }
}
