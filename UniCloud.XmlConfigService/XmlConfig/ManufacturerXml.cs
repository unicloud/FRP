using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

namespace UniCloud.Fleet.XmlConfigs
{

    public class ManafacturerXml : BaseXml
    {

        public ManafacturerXml(XmlConfigService service)
            : base(service)
        {
            _ConfigType = "制造商";
        }

        protected override void GenerateXmlContent(XElement RootNode)
        {
            //清空节点原有数据
            RootNode.RemoveAll();
            //获取所有飞机
            var AllAircraft = this.GetAllAircraft().ToList();
            //所有制造商列表
            IEnumerable<string> ManafacturerList = AllAircraft.Select(p => p.AircraftType.Manufacturer.CnName).Distinct();
            //按月生成每个月的数据
            DateTime startTime = GetOperationStartDate();
            DateTime endTime = GetOperationEndDate();
            for (DateTime time = startTime; time <= endTime; time = GetMonthEndofDateTime(time.AddMonths(1)))
            {
                //当前时间点的所有飞机
                var MonthAircraft = AllAircraft.Where(p => p.OperationHistories.Any(pp =>
                     pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time)));
                //时间节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", FormatDate(time)));
                RootNode.Add(DateTimeNode);
                //飞机总数
                int Amount = MonthAircraft.Count();
                //制造商节点
                XElement ManafacturerNode = new XElement("Type", new XAttribute("TypeName", "制造商"), new XAttribute("Amount", Amount));
                DateTimeNode.Add(ManafacturerNode);
                //各个制造商数据节点
                foreach (var name in ManafacturerList)
                {
                    int AirNum = MonthAircraft.Where(p => p.AircraftType.Manufacturer.CnName == name).Count();
                    XElement EachManaFacturerNode = new XElement("Item", new XAttribute("Name", name), new XAttribute("Percent", GetPercent(AirNum, Amount)), AirNum);
                    ManafacturerNode.Add(EachManaFacturerNode);
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
                    XElement RootNode = new XElement("Manufacturer");
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
