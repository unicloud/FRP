#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ApplicableAircraftEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// ApplicableAircraft实体相关配置
    /// </summary>
    internal class ApplicableAircraftEntityConfiguration : EntityTypeConfiguration<ApplicableAircraft>
    {
        public ApplicableAircraftEntityConfiguration()
        {
            ToTable("ApplicableAircraft", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.CompleteDate).HasColumnName("CompleteDate").HasColumnType("datetime2");
            Property(p => p.Cost).HasColumnName("Cost").HasColumnType("decimal").HasPrecision(16, 4);
            Property(p => p.ContractAircraftId).HasColumnName("ContractAircraftId");
            Property(p => p.ScnId).HasColumnName("ScnId");

            HasRequired(o => o.ContractAircraft).WithMany().HasForeignKey(o => o.ContractAircraftId);
        }

    }
}
