#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/23 16:18:26
// 文件名：FlightLogEntityConfiguration
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using UniCloud.Domain.PartBC.Aggregates.FlightLogAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql
{
    /// <summary>
    ///     FlightLog实体相关配置
    /// </summary>
    internal class FlightLogEntityConfiguration : EntityTypeConfiguration<FlightLog>
    {
        public FlightLogEntityConfiguration()
        {
            ToTable("FlightLog", DbConfig.Schema);
            HasKey(p => p.Id);
            Property(p => p.Id).HasColumnName("ID").HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            Property(p => p.FlightNum).HasColumnName("FlightNum");
            Property(p => p.LogNo).HasColumnName("LogNo");
            Property(p => p.LegNo).HasColumnName("LegNo");
            Property(p => p.AcReg).HasColumnName("AcReg");
            Property(p => p.MSN).HasColumnName("MSN");
            Property(p => p.FlightType).HasColumnName("FlightType");
            Property(p => p.FlightDate).HasColumnName("FlightDate").HasColumnType("datetime2");
            Property(p => p.BlockOn).HasColumnName("BlockOn");
            Property(p => p.TakeOff).HasColumnName("TakeOff");
            Property(p => p.Landing).HasColumnName("Landing");
            Property(p => p.BlockStop).HasColumnName("BlockStop");
            Property(p => p.TotalFH).HasColumnName("TotalFH");
            Property(p => p.TotalBH).HasColumnName("TotalBH");
            Property(p => p.FlightHours).HasColumnName("FlightHours");
            Property(p => p.BlockHours).HasColumnName("BlockHours");
            Property(p => p.TotalCycles).HasColumnName("TotalCycles");
            Property(p => p.Cycle).HasColumnName("Cycle");
            Property(p => p.DepartureAirport).HasColumnName("DepartureAirport");
            Property(p => p.ArrivalAirport).HasColumnName("ArrivalAirport");
            Property(p => p.ToGoNumber).HasColumnName("ToGoNumber");
            Property(p => p.ApuCycle).HasColumnName("ApuCycle");
            Property(p => p.ApuMM).HasColumnName("ApuMM");
            Property(p => p.ENG1OilDep).HasColumnName("ENG1OilDep");
            Property(p => p.ENG1OilArr).HasColumnName("ENG1OilArr");
            Property(p => p.ENG2OilDep).HasColumnName("ENG2OilDep");
            Property(p => p.ENG2OilArr).HasColumnName("ENG2OilArr");
            Property(p => p.ENG3OilDep).HasColumnName("ENG3OilDep");
            Property(p => p.ENG3OilArr).HasColumnName("ENG3OilArr");
            Property(p => p.ENG4OilDep).HasColumnName("ENG4OilDep");
            Property(p => p.ENG4OilArr).HasColumnName("ENG4OilArr");
            Property(p => p.ApuOilDep).HasColumnName("ApuOilDep");
            Property(p => p.ApuOilArr).HasColumnName("ApuOilArr");
            Property(p => p.CreateDate).HasColumnName("CreateDate").HasColumnType("datetime2");
            Property(p => p.UpdateDate).HasColumnName("UpdateDate").HasColumnType("datetime2");
        }
    }
}