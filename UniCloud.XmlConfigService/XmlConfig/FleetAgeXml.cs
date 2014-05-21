using System;
using System.Collections.Generic;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;
using System.Xml.Linq;
using System.Linq;

namespace UniCloud.Fleet.XmlConfigs
{
    public class FleetAgeXml : BaseXml
    {

        public FleetAgeXml(XmlConfigService XmlService)
            : base(XmlService)
        {
            _ConfigType = "机龄分析";
        }

        protected override void GenerateXmlContent(XElement RootNode)
        {

            //清空节点原有数据
            RootNode.RemoveAll();
            //所有飞机
            var AllAircraft = this.GetAllAircraft().Where(o => o.FactoryDate != null).ToList();
            //所有机型
            var AircraftTypeList = this.GetAllAircraftTypeList();
            //按月生成每个月的数据
            DateTime startTime = GetMonthEndofDateTime(Convert.ToDateTime(AllAircraft.Min(p => p.FactoryDate)));
            DateTime endTime = GetMonthEndofDateTime(DateTime.Now);
            if (startTime.Year < 1900) startTime = endTime;
            for (DateTime time = startTime; time <= endTime; time = GetMonthEndofDateTime(time.AddMonths(1)))
            {
                //生成时间节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", FormatDate(time)));
                RootNode.Add(DateTimeNode);
                //每个月份可计算机龄的飞机集合
                var MonthAircraft = AllAircraft.Where(o => o.AircraftBusinesses.Any(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time)) && o.FactoryDate <= time && !(o.ExportDate != null && o.ExportDate < time));
                //所有飞机的平均机年龄分布
                GetAgeNodeByDateTime(ref DateTimeNode, MonthAircraft, time, "所有机型");
                foreach (string AircraftType in AircraftTypeList)
                {
                    GetAgeNodeByDateTime(ref DateTimeNode, MonthAircraft, time, AircraftType);
                }
            }
        }

        /// <summary>
        /// 根据参数生成相应机型的平均机龄XML节点
        /// </summary>
        /// <param name="DateTimeNode">Xml的时间节点</param>
        /// <param name="MonthAircraft">月份飞机集合</param>
        /// <param name="time">选中时间点</param>
        /// <param name="AircraftType">机型</param>
        private void GetAgeNodeByDateTime(ref XElement DateTimeNode, IEnumerable<Aircraft> MonthAircraft, DateTime time, string AircraftType)
        {
            IEnumerable<Aircraft> aircraft = MonthAircraft;
            if (AircraftType != "所有机型")
            {
                aircraft = MonthAircraft.Where(o => o.AircraftBusinesses.FirstOrDefault(p => p.StartDate <= time && !(p.EndDate != null && p.EndDate < time)).AircraftType.Name == AircraftType);
            }
            //当前时间点的机型对应的平均年龄
            double AverageAge = 0;
            if (aircraft != null && aircraft.Count() > 0)
            {
                AverageAge = aircraft.ToList().Average(p => (time.Year - Convert.ToDateTime(p.FactoryDate).Year) * 12 + (time.Month - Convert.ToDateTime(p.FactoryDate).Month));
            }
            //机型节点
            XElement TypeNode = new XElement("Type", new XAttribute("TypeName", AircraftType), new XAttribute("Amount", Math.Round(AverageAge, 4)));
            DateTimeNode.Add(TypeNode);
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