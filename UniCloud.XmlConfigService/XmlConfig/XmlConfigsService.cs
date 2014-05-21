using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using UniCloud.Infrastructure.Data.FleetPlanBC.UnitOfWork;

namespace UniCloud.Fleet.XmlConfigs
{
    public class XmlConfigService
    {

        private List<object> _XmlConfigCollection;
        private FleetPlanBCUnitOfWork _context;
        private bool _IsAirlines = false;
        private Guid _AirlinesGuid;

        public XmlConfigService()
        {

            _context = new FleetPlanBCUnitOfWork();
            _XmlConfigCollection = new List<object>();
            //载入生成所有XmlConfig的所有相关数据
            this.GetAllRelativeData();
            //注册所有XmlConfig类型
            this.RegisterXmlConfig();
        }

        public XmlConfigService(bool isAirlines)
        {
            _IsAirlines = isAirlines;
            _context = new FleetPlanBCUnitOfWork();
            _XmlConfigCollection = new List<object>();
            //载入生成所有XmlConfig的所有相关数据
            this.GetAllRelativeData();
            //注册所有XmlConfig类型
            this.RegisterXmlConfig();
        }

        public XmlConfigService(FleetPlanBCUnitOfWork context, bool isAirlines)
        {
            _IsAirlines = isAirlines;
            _context = context;
            _XmlConfigCollection = new List<object>();
            //载入生成所有XmlConfig的所有相关数据
            this.GetAllRelativeData();
            //注册所有XmlConfig类型
            this.RegisterXmlConfig();
        }

        #region 属性
        public bool IsAirlines
        {
            get { return _IsAirlines; }
        }

        public Guid AirlinesGuid
        {
            get { return _AirlinesGuid; }
        }

        #endregion


        #region XmlConfig 相关数据

        private List<XmlConfig> _AllXmlConfig;
        private List<Plan> _AllPlan;
        private List<Airlines> _AllAirlines;
        private List<OperationHistory> _AllOperationHistory;
        private List<AircraftBusiness> _AllAircraftBusiness;
        private List<Aircraft> _AllAircraft;
        private List<AircraftType> _AllAircraftType;
        private List<AircraftCategory> _AllAircraftCategory;
        private List<OwnershipHistory> _AllOwnershipHistory;
        private List<PlanHistory> _AllPlanHistory;
        
        public List<XmlConfig> AllXmlConfig
        {
            get { return _AllXmlConfig; }
        }

        public List<Plan> AllPlan
        {
            get { return _AllPlan; }
        }

        public List<Airlines> AllAirlines
        {
            get { return _AllAirlines; }
        }

        public List<OperationHistory> AllOperationHistory
        {
            get { return _AllOperationHistory; }
        }

        public List<AircraftBusiness> AllAircraftBusiness
        {
            get { return _AllAircraftBusiness; }
        }

        public List<Aircraft> AllAircraft
        {
            get { return _AllAircraft; }
        }

        public List<OwnershipHistory> AllOwnershipHistory
        {
            get { return _AllOwnershipHistory; }
        }

        public List<AircraftType> AllAircraftType
        {
            get { return _AllAircraftType; }
        }

        public List<AircraftCategory> AllAircraftCategory
        {
            get { return _AllAircraftCategory; }
        }

        public List<PlanHistory> AllPlanHistory
        {
            get { return _AllPlanHistory; }
        }

        //获取当前航空公司
        private void GetCurrentAirlines()
        {
            if (this._IsAirlines)
            {
                //最新计划
                var lastplan = _context.Plans.OrderByDescending(p => p.CreateDate).FirstOrDefault();
                if (lastplan != null)
                {
                    _AirlinesGuid = lastplan.AirlinesId;
                }
                else
                {
                    //最新运营历史
                    var operationHistory = _context.CreateSet<OperationHistory>().Where(p => p.EndDate == null).OrderByDescending(p => p.StartDate).FirstOrDefault();
                    if (operationHistory!= null)
                   {
                       _AirlinesGuid = operationHistory.AirlinesId;
                   }
                   else
                   {
                       _AirlinesGuid = new Guid(); 
                   }
                }
            }
            else
            {
                this._AirlinesGuid = new Guid();
            }
        }

        ///载入生成所有XmlConfig的所有相关数据
        private void GetAllRelativeData()
        {
            //获取当前航空公司
            GetCurrentAirlines();
            //载入XmlConfig数据
            _AllXmlConfig = _context.XmlConfigs.ToList();

            //载入飞机类型
            _AllAircraftType = _context.AircraftTypes.ToList();

            //载入飞机座级
            _AllAircraftCategory = _context.AircraftCategories.ToList();

            //载入飞机数据
            _AllAircraft = _context.Aircrafts.ToList();

            //载入航空公司数据
            if (_IsAirlines)
            {
                _AllAirlines = _context.Airlineses.Where(p => p.Id == _AirlinesGuid).ToList();
            }
            else
            {
                _AllAirlines = _context.Airlineses.ToList();
            }

            //载入拥有者历史数据
            _AllOwnershipHistory = _context.CreateSet<OwnershipHistory>().ToList(); 

            //载入运营数据
            if (_IsAirlines)
            {
                _AllOperationHistory = _context.CreateSet<OperationHistory>().Where(p => p.AirlinesId == _AirlinesGuid).ToList();
            }
            else
            {
                _AllOperationHistory = _context.CreateSet<OperationHistory>().ToList();
            }

            //载入商业数据
            _AllAircraftBusiness = _context.CreateSet<AircraftBusiness>().ToList();

            //载入计划数据
            _AllPlan = _context.Plans.ToList();

            //载入计划明细数据
            _AllPlanHistory = _context.PlanHistories.ToList();
        }

        #endregion

        #region XmlConfig 相关操作

        //注册所有XmlConfig类型
        private void RegisterXmlConfig()
        {
            //机型
            _XmlConfigCollection.Add(new AirCraftTypeXml(this));
            ////客机
            //_XmlConfigCollection.Add(new FleetTrendPrnXml(this));
            ////货机
            //_XmlConfigCollection.Add(new FleetTrendCargoXml(this));
            //引进类型
            _XmlConfigCollection.Add(new ImportTypeXml(this));
            //供应商
            _XmlConfigCollection.Add(new SupplierXml(this));
            //制造商
            _XmlConfigCollection.Add(new ManafacturerXml(this));
            //计划执行
            _XmlConfigCollection.Add(new PlanPerformXml(this));
            //机龄分析
            _XmlConfigCollection.Add(new FleetAgeXml(this));
            //全部飞机
            _XmlConfigCollection.Add(new FleetTrendAllXml(this));
            //在册分析
            _XmlConfigCollection.Add(new FleetRegisteredXml(this));
        }

        //增加XmlConfig数据到XmlConfig集合
        public void AddXmlConfig(XmlConfig currentXmlConfig)
        {
            _context.XmlConfigs.Add(currentXmlConfig);
        }

        //更新XmlConfig数据到XmlConfig集合
        public void UpdateXmlConfig(XmlConfig currentXmlConfig)
        {
          //  _service.Update<XmlConfig>(currentXmlConfig);
            _context.Entry(currentXmlConfig).State = EntityState.Modified;
        }

        //更新XmlConfig内容
        public void UpdateAllXmlConfigContent()
        {
            _XmlConfigCollection.ForEach(p =>
            {
                BaseXml _XmlConfig = (BaseXml)p;
                _XmlConfig.UpdateXmlConfigContent();
            });
            //保存更新
            this._context.SaveChanges();
        }

        //更新XmlConfig标志
        public void UpdateAllXmlConfigFlag()
        {
            _XmlConfigCollection.ForEach(p =>
            {
                BaseXml _XmlConfig = (BaseXml)p;
                _XmlConfig.UpdateXmlConfigFlag();
            });

            //保存更新
            this._context.SaveChanges();
        }

        //更新XmlConfig特定内容
        public void UpdateXmlConfigContent(string XmlConfigType)
        {
            foreach (var p in _XmlConfigCollection)
            {
                BaseXml _XmlConfig = (BaseXml)p;
                if (_XmlConfig.ConfigType == XmlConfigType)
                {
                    _XmlConfig.UpdateXmlConfigContent();
                    break;
                }
            }

            //保存更新
            this._context.SaveChanges();
        }
        #endregion
    }
}
