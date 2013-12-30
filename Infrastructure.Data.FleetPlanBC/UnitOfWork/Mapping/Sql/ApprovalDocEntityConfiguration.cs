#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:38:15
// 文件名：ApprovalDocEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.ApprovalDocAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     ApprovalDoc实体相关配置
    /// </summary>
    internal class ApprovalDocEntityConfiguration : EntityTypeConfiguration<ApprovalDoc>
    {
        public ApprovalDocEntityConfiguration()
        {
            ToTable("ApprovalDoc", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.CaacExamineDate).HasColumnName("CaacExamineDate").HasColumnType("datetime2");
            Property(p => p.NdrcExamineDate).HasColumnName("NdrcExamineDate").HasColumnType("datetime2");
            Property(p => p.CaacApprovalNumber).HasColumnName("CaacApprovalNumber");
            Property(p => p.NdrcApprovalNumber).HasColumnName("NdrcApprovalNumber");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.Note).HasColumnName("Note");

            Property(p => p.DispatchUnitId).HasColumnName("DispatchUnitId");
            Property(p => p.CaacDocumentId).HasColumnName("CaacDocumentId");
            Property(p => p.NdrcDocumentId).HasColumnName("NdrcDocumentId");

            HasRequired(o => o.DispatchUnit).WithMany().HasForeignKey(o => o.DispatchUnitId);

        }
    }
}
