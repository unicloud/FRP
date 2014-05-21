using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

namespace UniCloud.Fleet.XmlConfigs
{

    public class ImportTypeXml : BaseXml
    {

        public ImportTypeXml(XmlConfigService service)
            : base(service)
        {
            _ConfigType = "引进方式";
        }

        protected override void GenerateXmlContent(XElement RootNode)
        {
            //清空节点原有数据
            RootNode.RemoveAll();
            //所有飞机运行记录(引进类型的飞机)
            var AllOperationHistory = this.GetAllAircraftOperationHistory().Where(p =>
                    p.ImportCategory.ActionType == "引进").ToList();
            //引进方式列表
            IEnumerable<string> AircraftImportList = this.GetAllImportTypeList();
            DateTime startTime = GetOperationStartDate();
            DateTime endTime = GetOperationEndDate();
            for (DateTime time = startTime; time <= endTime; time = GetMonthEndofDateTime(time.AddMonths(1)))
            {
                //计算time对应时间所有引进飞机
                var MonthOperationHistory = AllOperationHistory.Where(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time));
                //时间节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", FormatDate(time)));
                RootNode.Add(DateTimeNode);
                //引进飞机总数
                int Amount = MonthOperationHistory.Count();
                //引进方式
                XElement RegionalNode = new XElement("Type", new XAttribute("TypeName", "引进方式"), new XAttribute("Amount", Amount));
                DateTimeNode.Add(RegionalNode);
                // 各种引进方式
                foreach (var name in AircraftImportList)
                {
                    int AirNum = MonthOperationHistory.Where(p => this.SameImportType(p.ImportCategory.ActionName, name)).Count();
                    XElement CategoryNode = new XElement("Item", new XAttribute("Name", name), new XAttribute("Percent", GetPercent(AirNum, Amount)), AirNum);
                    RegionalNode.Add(CategoryNode);
                }
            }
        }

        protected override XmlConfig GetXmlConfig()
        {
            if (AllXmlConfigs != null)
            {
                var SpeXmlConfigs = AllXmlConfigs.Where(p => p.ConfigType == _ConfigType);
                if (SpeXmlConfigs == null || SpeXmlConfigs.Count() < 1)
                {
                    XElement RootNode = new XElement("ImportType");
                    XmlConfig XmlConfig1 = XmlConfigFactory.CreateXmlConfig(_ConfigType, RootNode);
                    //设置编辑状态
                    this.EditState = TEditState.esNew;
                    return XmlConfig1;
                }
                else
                {
                    XmlConfig XmlConfig1 = (XmlConfig)SpeXmlConfigs.FirstOrDefault();
                    //设置编辑状态
                    this.EditState = TEditState.esEdit;
                    return XmlConfig1;
                }
            }
            else
            {
                //设置编辑状态
                this.EditState = TEditState.esNone;
                return null;
            }
        }

    }
}
