using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using System.Xml.Linq;
using System.Linq;

namespace UniCloud.Fleet.XmlConfigs
{

    public class FleetTrendAllXml : BaseXml
    {

        public FleetTrendAllXml(XmlConfigService service)
            : base(service)
        {
            _ConfigType = "飞机运力";
        }

        protected override void GenerateXmlContent(XElement RootNode)
        {
            //清空节点原有数据
            RootNode.RemoveAll();
            //所有飞机
            var AllAircraft = this.GetAllAircraft().ToList();
            //当前航空公司
            string AirlinesName = GetCurrentAirlinesShortName();
            //按月生成每个月的数据
            DateTime startTime = GetOperationStartDate();
            DateTime endTime = GetOperationEndDate();
            for (DateTime time = startTime; time <= endTime; time = GetMonthEndofDateTime(time.AddMonths(1)))
            {
                //生成时间节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", FormatDate(time)));
                RootNode.Add(DateTimeNode);

                //当前时间点飞机(包括子公司)
                var MonthAircraft = AllAircraft.Where(p =>
                    p.AircraftBusinesses.Any(q =>
                        q.StartDate <= time && !(q.EndDate != null && q.EndDate < time)) &&
                        p.OperationHistories.Any(pp => pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time)));

                //飞机数
                XElement AircraftNode = new XElement("Type", new XAttribute("TypeName", "飞机数（子）"), new XAttribute("Amount", MonthAircraft.Count()));
                DateTimeNode.Add(AircraftNode);


                //当前时间点飞机(不包括子公司)
                var MonthAircraft1 = AllAircraft.Where(p =>
                    p.AircraftBusinesses.Any(q =>
                        q.StartDate <= time && !(q.EndDate != null && q.EndDate < time)) &&
                        p.OperationHistories.Any(pp => (pp.Airlines.CnShortName == AirlinesName ) &&
                            pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time)));

                //当前时间点飞机的运营权历史
                var MonthOperationHistoriy = MonthAircraft1.Select(o =>
                            o.OperationHistories.FirstOrDefault(a => a.StartDate <= time && !(a.EndDate != null && a.EndDate < time)));

                //飞机数
                XElement AircraftNode1 = new XElement("Type", new XAttribute("TypeName", "飞机数"), new XAttribute("Amount", MonthAircraft1.Count()));
                DateTimeNode.Add(AircraftNode1);

                if (MonthOperationHistoriy != null)
                {
                    //只属于母公司的运营权历史
                    var OperationHistoriySuperior =
                        MonthOperationHistoriy.Where(p => p.Airlines.CnShortName == AirlinesName);
                    if (OperationHistoriySuperior != null && OperationHistoriySuperior.Count() > 0)
                    {
                        AircraftNode1.Add(new XElement("Item", new XAttribute("Name", AirlinesName), new XAttribute("Percent", GetPercent(OperationHistoriySuperior.Count(), MonthOperationHistoriy.Count())), OperationHistoriySuperior.Count()));
                    }


                    //属于分公司的运营权历史
                    var SubOperationHistory = MonthOperationHistoriy.Where(p => p.Airlines.CnShortName == AirlinesName);
                    if (SubOperationHistory != null && SubOperationHistory.Count() > 0)
                    {
                        foreach (var item in SubOperationHistory.GroupBy(p => p.Airlines.CnShortName).ToList())
                        {
                            //分公司的飞机数
                            AircraftNode1.Add(new XElement("Item", new XAttribute("Name", item.Key), new XAttribute("Percent", GetPercent(item.Count(), MonthOperationHistoriy.Count())), item.Count()));
                        }
                    }


                    //属于分子公司的运营权历史
                    var OperationHistoriyMember = MonthOperationHistoriy.Where(a => a.Airlines.CnShortName != AirlinesName  && a.StartDate <= time && !(a.EndDate != null && a.EndDate < time));
                    if (OperationHistoriyMember != null && OperationHistoriyMember.Count() > 0)
                    {
                        foreach (var item in OperationHistoriyMember.GroupBy(p => p.Airlines.CnShortName).ToList())
                        {
                            AircraftNode1.Add(new XElement("Item", new XAttribute("Name", item.Key), new XAttribute("Percent", GetPercent(item.Count(), MonthOperationHistoriy.Count())), item.Count()));
                        }
                    }
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
                    XElement RootNode = new XElement("FleetTrendPnr");
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
