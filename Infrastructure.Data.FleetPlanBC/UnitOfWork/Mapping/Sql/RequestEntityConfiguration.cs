#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/28 10:46:31
// 文件名：RequestEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.FleetPlanBC.Aggregates.RequestAgg;

#endregion

namespace UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     Request实体相关配置
    /// </summary>
    internal class RequestEntityConfiguration : EntityTypeConfiguration<Request>
    {
        public RequestEntityConfiguration()
        {
            ToTable("Request", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(p => p.Title).HasColumnName("Title");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.SubmitDate).HasColumnName("SubmitDate").HasColumnType("datetime2");
            Property(p => p.IsFinished).HasColumnName("IsFinished");
            Property(p => p.CaacDocNumber).HasColumnName("CaacDocNumber");
            Property(p => p.Status).HasColumnName("Status");
            Property(p => p.Note).HasColumnName("Note");

            Property(p => p.ApprovalDocId).HasColumnName("ApprovalDocId");
            Property(p => p.CaacDocumentId).HasColumnName("CaacDocumentId");
            Property(p => p.AirlinesId).HasColumnName("AirlinesId");

            HasOptional(o => o.ApprovalDoc).WithMany().HasForeignKey(o => o.ApprovalDocId);
            HasRequired(o => o.Airlines).WithMany().HasForeignKey(o => o.AirlinesId);

        }
    }
}
