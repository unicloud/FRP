#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：ContractAircraftEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    /// ContractAircraft实体相关配置
    /// </summary>
    internal class ContractAircraftEntityConfiguration : EntityTypeConfiguration<ContractAircraft>
    {
        public ContractAircraftEntityConfiguration()
        {
            ToTable("ContractAircraft", DbConfig.Schema);

            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(p => p.ContractName).HasColumnName("ContractName").HasMaxLength(100);
            Property(p => p.ContractNumber).HasColumnName("ContractNumber").HasMaxLength(100);
            Property(p => p.RankNumber).HasColumnName("RankNumber").HasMaxLength(100);
            Property(p => p.CSCNumber).HasColumnName("CSCNumber").HasMaxLength(100);
            Property(p => p.SerialNumber).HasColumnName("SerialNumber").HasMaxLength(100);
            Property(p => p.IsValid).HasColumnName("IsValid");
            
            Property(p => p.BasicConfigGroupId).HasColumnName("BasicConfigGroupId");
        }

    }
}
