using System;
using System.Collections.Generic;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using System.Xml.Linq;
using System.Linq;
using System.Collections.ObjectModel;

namespace UniCloud.Fleet.XmlConfigs
{

    public enum TEditState
    {
        esNew,
        esEdit,
        esDelete,
        esNone
    }

    public abstract class BaseXml
    {
        protected XmlConfigService _XmlService;

        protected TEditState EditState = TEditState.esNew;

        protected string _ConfigType = "";

        protected List<XmlConfig> AllXmlConfigs
        {
            get { return _XmlService.AllXmlConfig; }
        }

        public BaseXml(XmlConfigService XmlService)
        {
            _XmlService = XmlService;
        }
        
        //更新XmlConfig内容
        public bool UpdateXmlConfigContent()
        {
            XmlConfig XmlConfig1 = this.GetXmlConfig();
            if (XmlConfig1 == null) { return false; };
            //已经存在，且不用更新的，直接返回
            //if (this.EditState == TEditState.esEdit && XmlConfig1.VersionNumber == 1)
            //{
            //    return true;
            //}
            //设置修改状态
             XmlConfig1.VersionNumber = 1;
            //生成XmlContent
            XElement RootNode = XmlConfig1.XmlContent;
            GenerateXmlContent(RootNode);
            XmlConfig1.XmlContent = RootNode;
            //更新数据
            if (this.EditState == TEditState.esNew)
            {
                //增加到XmlConfig集合中
                this._XmlService.AddXmlConfig(XmlConfig1);
            }
            else if(this.EditState == TEditState.esEdit)
            {
                //更新到XmlConfig集合中
                this._XmlService.UpdateXmlConfig(XmlConfig1);
            }
            return true;
        }

        //更新XmlConfig标志
        public bool UpdateXmlConfigFlag()
        {
            XmlConfig XmlConfig1 = this.GetXmlConfig();
            if (XmlConfig1 == null) { return false; };
            //设置数据为需要重新更新
            XmlConfig1.VersionNumber = -1;
             //更新数据
            if (this.EditState == TEditState.esNew)
            {
                //增加到XmlConfig集合中
                this._XmlService.AddXmlConfig(XmlConfig1);
            }
            else if(this.EditState == TEditState.esEdit)
            {
                //更新到XmlConfig集合中
                this._XmlService.UpdateXmlConfig(XmlConfig1);
            }
            return true;
        }

        protected abstract void GenerateXmlContent(XElement RootNode);

        protected abstract XmlConfig GetXmlConfig();

        protected string GetPercent(int Num, int Amount)
        {
            return Num==0 || Amount == 0 ? "0%" : (Num * 100.0 / Amount).ToString("##0.##") + "%";
        }

        protected string GetPercent(decimal Num, decimal Amount)
        {
            return Num == 0 || Amount == 0 ? "0%" : (Num * 100 / Amount).ToString("##0.##") + "%";
        }

        protected string FormatDate(DateTime dt)
        {
            return dt.ToString("yyyy/M/d");
        }

        protected string FormatDecimal(decimal dc)
        {
            return dc.ToString("##0");
        }

        protected DateTime GetMonthEndofDateTime(DateTime dt)
        {
            return dt.AddDays(1 - dt.Day).AddMonths(1).AddDays(-1);
        }

        protected DateTime GetOperationStartDate()
        {
            var OperationHistories = GetAllAircraftOperationHistory();
            DateTime dt = DateTime.Now;
            if (OperationHistories.Count() != 0)
            {
                dt = (DateTime)OperationHistories.Min(p => p.StartDate);
            }
            return GetMonthEndofDateTime(dt);
        }

        protected DateTime GetOperationEndDate()
        {
            var OperationHistories = GetAllAircraftOperationHistory();
            DateTime dt = DateTime.Now;
            if (OperationHistories.Count() != 0)
            {
                if (!OperationHistories.Any(p => p.EndDate == null))
                {
                    var MaxEndDate = OperationHistories.Max(p => p.EndDate);
                    if (MaxEndDate != null)
                    {
                        dt = (DateTime)MaxEndDate;
                        dt = dt < DateTime.Now ? dt : DateTime.Now;
                    }
                }
            }
            return GetMonthEndofDateTime(dt);
        }

        protected string GetCurrentAirlinesShortName()
        {
            var firstOrDefault = _XmlService.AllAirlines.FirstOrDefault(p => p.IsCurrent == true);
            if (firstOrDefault != null)
                return firstOrDefault.CnShortName;
            return null;
        }

        protected List<Aircraft> GetAllAircraft()
        {
            return _XmlService.AllAircraft;
        }

        protected List<OperationHistory> GetAllAircraftOperationHistory()
        {
            return _XmlService.AllOperationHistory;
        }

        protected List<AircraftBusiness> GetAllAircraftAircraftBusiness()
        {
            return _XmlService.AllAircraftBusiness;
        }


        protected List<PlanHistory> GetAllPlanHistory()
        {
            return _XmlService.AllPlanHistory;
        }

        protected IEnumerable<string> GetAllAirLinesShortNameList()
        {
            return GetAllAircraftOperationHistory().Select(p => p.Airlines.CnShortName).Distinct().ToList();
        }

        protected IEnumerable<string> GetAllAircraftRegionalList()
        {
            //从商业数据中取座级
            return _XmlService.AllAircraftBusiness.Select(p => p.AircraftType.AircraftCategory.Regional).Distinct().ToList();

         //   return _XmlService.AllAircraftCategory.Select(p => p.Regional).Distinct().ToList();
        }

        protected IEnumerable<string> GetAllAircraftTypeList()
        {
            //从商业数据中取AircraftType
            return _XmlService.AllAircraftBusiness.Select(p => p.AircraftType.Name).Distinct().ToList();
          //  return _XmlService.AllAircraftType.Select(p => p.Name).ToList();
        }

        protected IEnumerable<string> GetAllImportTypeList()
        {

            List<string> AllImportTypeList = GetAllAircraftOperationHistory().Where(p =>
                    p.ImportCategory.ActionType == "引进").Select(p =>
                    p.ImportCategory.ActionName).Distinct().ToList();
            for (int i = AllImportTypeList.Count() - 1; i >= 0; i--)
            {
                if (AllImportTypeList[i].IndexOf("续租") >= 0)
                {
                    AllImportTypeList.RemoveAt(i);
                }
            }
            return AllImportTypeList;
        }


        protected bool SameImportType(string ImportType1, string ImportType2)
        {
            //return ImportType1 == ImportType2;
            int Index1 = ImportType1.IndexOf("续租");
            if (Index1 >= -1)
            {
                return ImportType1.Contains(ImportType2);
            }
            else
            {
                int Index2 = ImportType2.IndexOf("续租");
                if (Index2 >= -1)
                {
                    return ImportType2.Contains(ImportType1);
                }
                else
                {
                    return ImportType1 == ImportType2;
                }
            }

        }

        protected List<Plan> GetAllPlan()
        {
            return _XmlService.AllPlan;
        }

        public string ConfigType
        {
            get { return _ConfigType; }
        }
    }
}
     