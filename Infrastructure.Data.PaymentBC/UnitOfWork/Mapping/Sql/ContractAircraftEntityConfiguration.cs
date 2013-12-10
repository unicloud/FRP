#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/12/10，14:12
// 方案：FRP
// 项目：Infrastructure.Data.PaymentBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PaymentBC.Aggregates.ContractAircraftAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PaymentBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     ContractAircraft实体相关配置
    /// </summary>
    internal class ContractAircraftEntityConfiguration : EntityTypeConfiguration<ContractAircraft>
    {
        public ContractAircraftEntityConfiguration()
        {
            ToTable("ContractAircraft", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ContractName).HasColumnName("ContractName");
            Property(p => p.ContractNumber).HasColumnName("ContractNumber");
            Property(p => p.RankNumber).HasColumnName("RankNumber");
            Property(p => p.CSCNumber).HasColumnName("CSCNumber");
            Property(p => p.SerialNumber).HasColumnName("SerialNumber");
            Property(p => p.IsValid).HasColumnName("IsValid");
            Property(p => p.ReceivedAmount).HasColumnName("ReceivedAmount");
            Property(p => p.AcceptedAmount).HasColumnName("AcceptedAmount");

            Property(p => p.AircraftTypeId).HasColumnName("AircraftTypeId");
            Property(p => p.ImportCategoryId).HasColumnName("ImportCategoryId");

            HasRequired(c => c.AircraftType).WithMany().HasForeignKey(c => c.AircraftTypeId);
            HasRequired(c => c.ImportCategory).WithMany().HasForeignKey(c => c.ImportCategoryId);
        }
    }
}