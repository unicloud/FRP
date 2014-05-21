using System;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using System.Xml.Linq;
using System.Linq;

namespace UniCloud.Fleet.XmlConfigs
{
    public class FleetRegisteredXml : BaseXml
    {

        public FleetRegisteredXml(XmlConfigService XmlService)
            : base(XmlService)
        {
            _ConfigType = "在册分析";
        }

        protected override void GenerateXmlContent(XElement RootNode)
        {
            //清空节点原有数据
            RootNode.RemoveAll();
            //所有飞机
            var AllAircraft = this.GetAllAircraft().Where(o => o.AircraftBusinesses != null).ToList();
            //所有机型
            var AircraftTypeList = this.GetAllAircraftTypeList();
            //按月生成每个月的数据
            DateTime startTime = GetMonthEndofDateTime(Convert.ToDateTime(AllAircraft.Min(p => p.AircraftBusinesses.Min(pp => pp.StartDate))));
            DateTime endTime = GetMonthEndofDateTime(DateTime.Now);
            if (startTime.Year < 1900) startTime = endTime;
            for (DateTime time = startTime; time <= endTime; time = GetMonthEndofDateTime(time.AddMonths(1)))
            {
                //生成时间节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", FormatDate(time)));
                RootNode.Add(DateTimeNode);
                //获取当月可统计在册的所有飞机
                var MonthAircraft = AllAircraft.Where(p =>
                    {
                        var aircraftbusinesses = p.AircraftBusinesses;
                        if (aircraftbusinesses.Count() == 0) return false;
                        if (aircraftbusinesses.Min(pp => pp.StartDate) > time) return false;
                        if (aircraftbusinesses.Any(pp => pp.EndDate == null)) return true;
                        if (aircraftbusinesses.Max(pp => pp.EndDate < time.AddDays(1 - time.Day))) return false;
                        return true;
                    });

                //当月的全部在册飞机数
                double averageall = MonthAircraft.Sum(p =>
                    {
                        var aircraftbusinesses = p.AircraftBusinesses;
                        //当月最小时间
                        DateTime? minstartdate = time.AddDays(1 - time.Day);
                        if (aircraftbusinesses.Min(pp => pp.StartDate) > minstartdate)
                        {
                            minstartdate = aircraftbusinesses.Min(pp => pp.StartDate);
                        }
                        //当月最大时间
                        DateTime? maxstartdate = time;
                        if (!aircraftbusinesses.Any(pp => pp.EndDate == null) && aircraftbusinesses.Max(pp => pp.EndDate) <= time)
                        {
                            maxstartdate = aircraftbusinesses.Max(pp => pp.EndDate);
                        }
                        return Convert.ToDateTime(maxstartdate).Day - Convert.ToDateTime(minstartdate).Day;
                    }) / Convert.ToDouble(time.Day);

                //所有机型的在册飞机数节点
                XElement TypeNode = new XElement("Type", new XAttribute("TypeName", "机型"), new XAttribute("Amount", Math.Round(averageall, 4)));
                DateTimeNode.Add(TypeNode);
                foreach (string AircraftType in AircraftTypeList)
                {
                    //当月所选机型的可统计在册的所有飞机      
                    var AircraftByType = MonthAircraft.Where(o => o.AircraftBusinesses.Any(oo => oo.AircraftType.Name == AircraftType
                        && oo.StartDate <= time && !(oo.EndDate != null && oo.EndDate < time.AddDays(1 - time.Day))));

                    //当月含有所选机型的在册飞机数
                    double average = AircraftByType.Sum(p =>
                        {
                            var aircraftbusinesses = p.AircraftBusinesses.Where(oo => oo.AircraftType.Name == AircraftType
                                && oo.StartDate <= time && !(oo.EndDate != null && oo.EndDate < time.AddDays(1 - time.Day)));

                            return aircraftbusinesses.Sum(pp =>
                                {
                                    //当月最小时间
                                    DateTime? minstartdate = time.AddDays(1 - time.Day);
                                    if (pp.StartDate > minstartdate)
                                    {
                                        minstartdate = pp.StartDate;
                                    }
                                    //当月最大时间
                                    DateTime? maxstartdate = time;
                                    if (pp.EndDate != null && pp.EndDate <= time)
                                    {
                                        maxstartdate = pp.EndDate;
                                    }
                                    return Convert.ToDateTime(maxstartdate).Day - Convert.ToDateTime(minstartdate).Day;
                                });
                        }) / Convert.ToDouble(time.Day);

                    //对应机型的在册飞机数节点
                    XElement ItemNode = new XElement("Item", new XAttribute("Name", AircraftType), Math.Round(average, 4));
                    TypeNode.Add(ItemNode);
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
                    XElement RootNode = new XElement("FleetTrendCargo");
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