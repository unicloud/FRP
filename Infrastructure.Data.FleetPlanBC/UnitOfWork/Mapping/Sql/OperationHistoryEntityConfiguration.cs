#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:28:53
// 文件名：OperationHistoryEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     OperationHistory实体相关配置
    /// </summary>
    internal class OperationHistoryEntityConfiguration : EntityTypeConfiguration<OperationHistory>
    {
        public OperationHistoryEntityConfiguration()
        {
            ToTable("OperationHistory", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.RegNumber).HasColumnName("RegNumber");
            Property(p => p.StartDate).HasColumnName("StartDate").HasColumnType("datetime2");
            Property(p => p.StopDate).HasColumnName("StopDate").HasColumnType("datetime2");
            Property(p => p.TechReceiptDate).HasColumnName("TechReceiptDate").HasColumnType("datetime2");
            Property(p => p.ReceiptDate).HasColumnName("ReceiptDate").HasColumnType("datetime2");
            Property(p => p.TechDeliveryDate).HasColumnName("TechDeliveryDate").HasColumnType("datetime2");
            Property(p => p.OnHireDate).HasColumnName("OnHireDate").HasColumnType("datetime2");
            Property(p => p.EndDate).HasColumnName("EndDate").HasColumnType("datetime2");
            Property(p => p.Note).HasColumnName("Note");

            Property(p => p.AircraftId).HasColumnName("AircraftId");
            Property(p => p.AirlinesId).HasColumnName("AirlinesId");
            Property(p => p.ImportCategoryId).HasColumnName("ImportCategoryId");
            Property(p => p.ExportCategoryId).HasColumnName("ExportCategoryId");

            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);
            HasRequired(o => o.ImportCategory).WithMany().HasForeignKey(o => o.ImportCategoryId);
            HasOptional(o => o.ExportCategory).WithMany().HasForeignKey(o => o.ExportCategoryId);

        }
    }
}
