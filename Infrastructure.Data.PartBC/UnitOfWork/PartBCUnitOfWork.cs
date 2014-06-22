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
using UniCloud.Domain.PartBC.Aggregates.AcDailyUtilizationAgg;
using UniCloud.Domain.PartBC.Aggregates.AdSbAgg;
using UniCloud.Domain.PartBC.Aggregates.AirBusScnAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftSeriesAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg;
using UniCloud.Domain.PartBC.Aggregates.AnnualAgg;
using UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigGroupAgg;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
using UniCloud.Domain.PartBC.Aggregates.FlightLogAgg;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.ModAgg;
using UniCloud.Domain.PartBC.Aggregates.OilMonitorAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.ScnAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.ThresholdAgg;
using UniCloud.Domain.PartBC.Aggregates.ThrustAgg;

#endregion

namespace UniCloud.Infrastructure.Data.PartBC.UnitOfWork
{
    public class PartBCUnitOfWork : UniContext<PartBCUnitOfWork>
    {
        #region IDbSet成员

        private IDbSet<AcDailyUtilization> _acDailyUtilizations;
        private IDbSet<AdSb> _adSbs;
        private IDbSet<AirBusScn> _airBusScns;
        private IDbSet<AirStructureDamage> _airStructureDamages;
        private IDbSet<AircraftMaintainPlan> _aircraftMaintainPlans;
        private IDbSet<AircraftSeries> _aircraftSeries;
        private IDbSet<AircraftType> _aircraftTypes;
        private IDbSet<Aircraft> _aircrafts;
        private IDbSet<Annual> _annuals;
        private IDbSet<BasicConfigGroup> _basicConfigGroups;
        private IDbSet<BasicConfigHistory> _basicConfigHistories;
        private IDbSet<BasicConfig> _basicConfigs;
        private IDbSet<ContractAircraft> _contractAircrafts;
        private IDbSet<CtrlUnit> _ctrlUnits;
        private IDbSet<EngineMaintainPlan> _engineMaintainPlans;
        private IDbSet<FlightLog> _flightLogs;
        private IDbSet<InstallController> _installControllers;
        private IDbSet<Item> _items;
        private IDbSet<MaintainCtrl> _maintainCtrls;
        private IDbSet<MaintainWork> _maintainWorks;
        private IDbSet<Mod> _mods;
        private IDbSet<OilMonitor> _oilMonitors;
        private IDbSet<PnReg> _pnRegs;
        private IDbSet<Scn> _scns;
        private IDbSet<SnHistory> _snHistories;
        private IDbSet<SnReg> _snRegs;
        private IDbSet<SnRemInstRecord> _snRemInstRecords;
        private IDbSet<SpecialConfig> _specialConfigs;
        private IDbSet<Threshold> _thresholds;
        private IDbSet<Thrust> _thrusts;


        public IDbSet<AcDailyUtilization> AcDailyUtilizations
        {
            get { return _acDailyUtilizations ?? (_acDailyUtilizations = Set<AcDailyUtilization>()); }
        }

        public IDbSet<AdSb> AdSbs
        {
            get { return _adSbs ?? (_adSbs = Set<AdSb>()); }
        }

        public IDbSet<AirBusScn> AirBusScns
        {
            get { return _airBusScns ?? (_airBusScns = Set<AirBusScn>()); }
        }

        public IDbSet<AirStructureDamage> AirStructureDamages
        {
            get { return _airStructureDamages ?? (_airStructureDamages = Set<AirStructureDamage>()); }
        }

        public IDbSet<AircraftMaintainPlan> AircraftMaintainPlans
        {
            get { return _aircraftMaintainPlans ?? (_aircraftMaintainPlans = Set<AircraftMaintainPlan>()); }
        }

        public IDbSet<AircraftSeries> AircraftSeries
        {
            get { return _aircraftSeries ?? (_aircraftSeries = Set<AircraftSeries>()); }
        }

        public IDbSet<AircraftType> AircraftTypes
        {
            get { return _aircraftTypes ?? (_aircraftTypes = Set<AircraftType>()); }
        }

        public IDbSet<Aircraft> Aircrafts
        {
            get { return _aircrafts ?? (_aircrafts = Set<Aircraft>()); }
        }

        public IDbSet<Annual> Annuals
        {
            get { return _annuals ?? (_annuals = Set<Annual>()); }
        }

        public IDbSet<BasicConfigGroup> BasicConfigGroups
        {
            get { return _basicConfigGroups ?? (_basicConfigGroups = Set<BasicConfigGroup>()); }
        }

        public IDbSet<BasicConfigHistory> BasicConfigHistories
        {
            get { return _basicConfigHistories ?? (_basicConfigHistories = Set<BasicConfigHistory>()); }
        }

        public IDbSet<BasicConfig> BasicConfigs
        {
            get { return _basicConfigs ?? (_basicConfigs = Set<BasicConfig>()); }
        }

        public IDbSet<ContractAircraft> ContractAircrafts
        {
            get { return _contractAircrafts ?? (_contractAircrafts = Set<ContractAircraft>()); }
        }

        public IDbSet<CtrlUnit> CtrlUnits
        {
            get { return _ctrlUnits ?? (_ctrlUnits = Set<CtrlUnit>()); }
        }

        public IDbSet<EngineMaintainPlan> EngineMaintainPlans
        {
            get { return _engineMaintainPlans ?? (_engineMaintainPlans = Set<EngineMaintainPlan>()); }
        }

        public IDbSet<FlightLog> FlightLogs
        {
            get { return _flightLogs ?? (_flightLogs = Set<FlightLog>()); }
        }

        public IDbSet<InstallController> InstallControllers
        {
            get { return _installControllers ?? (_installControllers = Set<InstallController>()); }
        }

        public IDbSet<Item> Items
        {
            get { return _items ?? (_items = Set<Item>()); }
        }

        public IDbSet<MaintainCtrl> MaintainCtrls
        {
            get { return _maintainCtrls ?? (_maintainCtrls = Set<MaintainCtrl>()); }
        }

        public IDbSet<MaintainWork> MaintainWorks
        {
            get { return _maintainWorks ?? (_maintainWorks = Set<MaintainWork>()); }
        }

        public IDbSet<Mod> Mods
        {
            get { return _mods ?? (_mods = Set<Mod>()); }
        }

        public IDbSet<OilMonitor> OilMonitors
        {
            get { return _oilMonitors ?? (_oilMonitors = Set<OilMonitor>()); }
        }

        public IDbSet<PnReg> PnRegs
        {
            get { return _pnRegs ?? (_pnRegs = Set<PnReg>()); }
        }

        public IDbSet<Scn> Scns
        {
            get { return _scns ?? (_scns = Set<Scn>()); }
        }

        public IDbSet<SnReg> SnRegs
        {
            get { return _snRegs ?? (_snRegs = Set<SnReg>()); }
        }

        public IDbSet<SnHistory> SnHistories
        {
            get { return _snHistories ?? (_snHistories = Set<SnHistory>()); }
        }

        public IDbSet<SnRemInstRecord> SnRemInstRecords
        {
            get { return _snRemInstRecords ?? (_snRemInstRecords = Set<SnRemInstRecord>()); }
        }

        public IDbSet<SpecialConfig> SpecialConfigs
        {
            get { return _specialConfigs ?? (_specialConfigs = Set<SpecialConfig>()); }
        }

        public IDbSet<Threshold> Thresholds
        {
            get { return _thresholds ?? (_thresholds = Set<Threshold>()); }
        }

        public IDbSet<Thrust> Thrusts
        {
            get { return _thrusts ?? (_thrusts = Set<Thrust>()); }
        }

        #endregion
    }
}