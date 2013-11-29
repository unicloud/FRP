#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/06，18:11
// 方案：FRP
// 项目：Infrastructure.Data.PurchaseBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PurchaseBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     AircraftPurchaseReceptionLine实体相关配置
    /// </summary>
    internal class AircraftPurchaseReceptionLineEntityConfiguration :
        EntityTypeConfiguration<AircraftPurchaseReceptionLine>
    {
        public AircraftPurchaseReceptionLineEntityConfiguration()
        {
            ToTable("AircraftPurchaseReceptionLine", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.ContractAircraftId).HasColumnName("ContractAircraftId");

            HasRequired(a => a.PurchaseContractAircraft).WithMany().HasForeignKey(a => a.ContractAircraftId);
        }
    }
}