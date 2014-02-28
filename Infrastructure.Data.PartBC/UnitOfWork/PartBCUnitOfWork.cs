#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/10 14:34:55
// 文件名：PartBCUnitOfWork
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using UniCloud.Domain.PartBC.Aggregates.AcDailyUtilizationAgg;
using UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.ModAgg;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.TechnicalSolutionAgg;
using UniCloud.Infrastructure.Data.PartBC.UnitOfWork.Mapping.Sql;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork
{
    public class PartBCUnitOfWork : BaseContext<PartBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<AcDailyUtilization> _acDailyUtilizations;
        private IDbSet<AirBusScn> _airBusScns;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<AirStructureDamage> _airStructureDamages;
        private IDbSet<BasicConfigGroup> _basicConfigGroups;
        private IDbSet<ContractAircraft> _contractAircrafts;
        private IDbSet<CtrlUnit> _ctrlUnits;
        private IDbSet<MaintainCtrl> _maintainCtrls;
        private IDbSet<MaintainWork> _maintainWorks;
        private IDbSet<Mod> _mods;
        private IDbSet<OilMonitor> _oilMonitors;
        private IDbSet<PnReg> _pneRegs;
        private IDbSet<Scn> _scns;
        private IDbSet<SnReg> _snRegs;
        private IDbSet<SpecialConfig> _specialConfigs;
        private IDbSet<TechnicalSolution> _technicalSolutions;

        public IDbSet<AcDailyUtilization> AcDailyUtilizations
        {
            get { return _acDailyUtilizations ?? (_acDailyUtilizations = base.Set<AcDailyUtilization>()); }
        }

        public IDbSet<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = base.Set<Aircraft>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = base.Set<AircraftType>()); }
        }

        public IDbSet<AirStructureDamage> AirStructureDamages
        {
            get
            {
                return _airStructureDamages ?? (_airStructureDamages = Set<AirStructureDamage>());
            }
        }

        public IDbSet<BasicConfigGroup> BasicConfigGroups
        {
            get { return _basicConfigGroups ?? (_basicConfigGroups = base.Set<BasicConfigGroup>()); }
        }

        public IDbSet<ContractAircraft> ContractAircrafts
        {
            get { return _contractAircrafts ?? (_contractAircrafts = base.Set<ContractAircraft>()); }
        }

        public IDbSet<CtrlUnit> CtrlUnits
        {
            get { return _ctrlUnits ?? (_ctrlUnits = base.Set<CtrlUnit>()); }
        }

        public IDbSet<MaintainCtrl> MaintainCtrls
        {
            get { return _maintainCtrls ?? (_maintainCtrls = base.Set<MaintainCtrl>()); }
        }

        public IDbSet<MaintainWork> MaintainWorks
        {
            get { return _maintainWorks ?? (_maintainWorks = base.Set<MaintainWork>()); }
        }

        public IDbSet<Mod> Mods
        {
            get { return _mods ?? (_mods = base.Set<Mod>()); }
        }

        public IDbSet<OilMonitor> OilMonitors
        {
            get { return _oilMonitors ?? (_oilMonitors = base.Set<OilMonitor>()); }
        }

        public IDbSet<PnReg> PnRegs
        {
            get { return _pneRegs ?? (_pneRegs = base.Set<PnReg>()); }
        }

        public IDbSet<Scn> Scns
        {
            get { return _scns ?? (_scns = base.Set<Scn>()); }
        }

        public IDbSet<AirBusScn> AirBusScns
        {
            get { return _airBusScns ?? (_airBusScns = base.Set<AirBusScn>()); }
        }

        public IDbSet<SnReg> SnRegs
        {
            get { return _snRegs ?? (_snRegs = base.Set<SnReg>()); }
        }

        public IDbSet<SpecialConfig> SpecialConfigs
        {
            get { return _specialConfigs ?? (_specialConfigs = base.Set<SpecialConfig>()); }
        }

        public IDbSet<TechnicalSolution> TechnicalSolutions
        {
            get { return _technicalSolutions ?? (_technicalSolutions = base.Set<TechnicalSolution>()); }
        }

        #endregion

        #region DbContext 重载

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // 移除不需要的公约
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            // 添加通过“TypeConfiguration”类的方式建立的配置
            if (DbConfig.DbUniCloud.Contains("Oracle"))
            {
                OracleConfigurations(modelBuilder);
            }
            else if (DbConfig.DbUniCloud.Contains("Sql"))
            {
                SqlConfigurations(modelBuilder);
            }
        }

        /// <summary>
        ///     Oracle数据库
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void OracleConfigurations(DbModelBuilder modelBuilder)
        {
        }

        /// <summary>
        ///     SqlServer数据库
        /// </summary>
        /// <param name="modelBuilder"></param>
        private static void SqlConfigurations(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations

                #region AcDailyUtilizationAgg

                .Add(new AcDailyUtilizationEntityConfiguration())
                #endregion

                #region AircraftAgg

                .Add(new AircraftEntityConfiguration())
                #endregion

                #region AircraftTypeAgg

                .Add(new AircraftTypeEntityConfiguration())
                #endregion

                #region BasicConfigGroupAgg

                .Add(new BasicConfigGroupEntityConfiguration())
                .Add(new BasicConfigEntityConfiguration())
                #endregion

                #region ContractAircraftAgg

                .Add(new ContractAircraftEntityConfiguration())
                #endregion

                #region CtrlUnitAgg

                .Add(new CtrlUnitEntityConfiguration())
                #endregion

                #region MaintainCtrlAgg

                .Add(new MaintainCtrlEntityConfiguration())
                .Add(new ItemMaintainCtrlEntityConfiguration())
                .Add(new PnMaintainCtrlEntityConfiguration())
                .Add(new SnMaintainCtrlEntityConfiguration())
                .Add(new MaintainCtrlLineEntityConfiguration())
                #endregion

                #region MaintainWorkAgg

                .Add(new MaintainWorkEntityConfiguration())
                #endregion

                #region ModAgg

                .Add(new ModEntityConfiguration())
                #endregion

                #region OilMonitorAgg

                .Add(new OilMonitorEntityConfiguration())
                
                #endregion

                #region OilUserAgg

                .Add(new OilUserEntityConfiguration())
                .Add(new EngineOilEntityConfiguration())
                .Add(new APUOilEntityConfiguration())
                
                #endregion


                #region PnRegAgg

                .Add(new PnRegEntityConfiguration())
                #endregion

                #region ScnAgg

                .Add(new ScnEntityConfiguration())
                .Add(new ApplicableAircraftEntityConfiguration())
                .Add(new AirBusScnEntityConfiguration())
                #endregion

                #region SnRegAgg

                .Add(new SnRegEntityConfiguration())
                .Add(new SnHistoryEntityConfiguration())
                .Add(new LifeMonitorEntityConfiguration())
                #endregion

                #region SpecialConfigAgg

                .Add(new AcConfigEntityConfiguration())
                .Add(new SpecialConfigEntityConfiguration())

                #endregion

                #region TechnicalSolutionAgg

                .Add(new TechnicalSolutionEntityConfiguration())
                .Add(new TsLineEntityConfiguration())
                .Add(new DependencyEntityConfiguration())
                #endregion
.Add(new AirStructureDamageEntityConfiguration())
                ;
        }

        #endregion
    }
}