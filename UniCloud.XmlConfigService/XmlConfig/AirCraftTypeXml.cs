using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

namespace UniCloud.Fleet.XmlConfigs
{

    public class AirCraftTypeXml : BaseXml
    {

        public AirCraftTypeXml(XmlConfigService XmlService)
            : base(XmlService)
        {
            _ConfigType = "座级机型";
        }

        #region 优化GenerateXmlContent方法

        protected void GenerateXmlContentNew(XElement RootNode)
        {
            RootNode.RemoveAll();
            //所有飞机运行历史
            var AllOperationHistory = this.GetAllAircraftOperationHistory().OrderBy(p => p.StartDate).ToList();
            //获取所有飞机 
            var AllAircrafts = GetAllAircraft().ToList();
            DateTime startTime = GetOperationStartDate();
            DateTime endTime = GetOperationEndDate();

            //所有座级
            IEnumerable<string> AircraftRegionalList = this.GetAllAircraftRegionalList();
            //所有机型
            IEnumerable<string> AircraftTypeList = this.GetAllAircraftTypeList();

            DateNodeList dnlXml = new DateNodeList(startTime, endTime);


            for (DateTime time = startTime; time <= endTime; time = GetMonthEndofDateTime(time.AddMonths(1)))
            {
                DateNode dtNode = new DateNode();
                dtNode.Name = FormatDate(time);
                dtNode.Time = time;
                dnlXml.AddNode(dtNode);

                //座级
                RegionalNode rnNode = new RegionalNode();
                rnNode.Name = "座级";
                rnNode.Amount = 0;
                dtNode.AddChildNode(rnNode);
                // 所有座级
                foreach (var name in AircraftRegionalList)
                {
                    LeafNode lfNode = new LeafNode();
                    lfNode.Name = name;
                    lfNode.Amount = 0;
                    lfNode.Percent = 0;
                    rnNode.AddChildNode(lfNode);
                }
                //机型
                TypeNode tpNode = new TypeNode();
                tpNode.Name = "机型";
                tpNode.Amount = 0;
                dtNode.AddChildNode(tpNode);

                // 所有机型
                foreach (var name in AircraftTypeList)
                {
                    LeafNode lfNode = new LeafNode();
                    lfNode.Name = name;
                    lfNode.Amount = 0;
                    lfNode.Percent = 0;
                    tpNode.AddChildNode(lfNode);
                }

            }

            foreach (var OperationHistory in AllOperationHistory)
            {
                DateTime dtStart = OperationHistory.StartDate == null ? DateTime.Now : (DateTime)OperationHistory.StartDate;
                DateTime dtEnd = OperationHistory.EndDate == null ? DateTime.Now : (DateTime)OperationHistory.EndDate;
                DateNode dtNode;
                int intBeginIndex, intEndIndex;
                dnlXml.GetDataNodeRange(dtStart, dtEnd, out intBeginIndex, out intEndIndex);
                var aircraft = AllAircrafts.FirstOrDefault(p => p.Id == OperationHistory.AircraftId);
                if (aircraft != null && aircraft.AircraftType != null && aircraft.AircraftType.AircraftCategory != null)
                {
                    string RegionalName = aircraft.AircraftType.AircraftCategory.Regional;
                    string TypeName = aircraft.AircraftType.Name;

                    for (int i = intBeginIndex; i <= intEndIndex; i++)
                    {
                        dtNode = dnlXml.GetItem(i);
                        if (dtNode != null)
                        {
                            List<BaseNode> childNodes;
                            childNodes = dtNode.ChildNodes[0].ChildNodes;

                            int intCount = childNodes.Count();
                            for (int j = 0; j < intCount; j++)
                            {
                                LeafNode lfNode = (LeafNode)childNodes[j];
                                if (lfNode.Name == RegionalName)
                                {
                                    lfNode.Amount += 1;
                                    dtNode.ChildNodes[0].Amount += 1;
                                    break;
                                }
                            }

                            childNodes = dtNode.ChildNodes[1].ChildNodes;
                            intCount = childNodes.Count();
                            for (int j = 0; j < intCount; j++)
                            {
                                LeafNode lfNode = (LeafNode)childNodes[j];
                                if (lfNode.Name == TypeName)
                                {
                                    lfNode.Amount += 1;
                                    dtNode.ChildNodes[1].Amount += 1;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            NodeToXmlContent(dnlXml, RootNode);
        }

        private void NodeToXmlContent(DateNodeList dnlXml, XElement RootNode)
        {
            int DataNodeCount = dnlXml.Count();
            DateNode dtNode;
            for (int i = 0; i < DataNodeCount; i++)
            {
                dtNode = dnlXml.GetItem(i);
                //日期节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", dtNode.Name));
                RootNode.Add(DateTimeNode);

                if (dtNode != null)
                {
                    List<BaseNode> childNodes;
                    childNodes = dtNode.ChildNodes[0].ChildNodes;
                    int Amount = dtNode.ChildNodes[0].Amount;
                    XElement RegionalNode = new XElement("Type", new XAttribute("TypeName", "座级"), new XAttribute("Amount", Amount));
                    DateTimeNode.Add(RegionalNode);

                    int intCount = childNodes.Count();
                    for (int j = 0; j < intCount; j++)
                    {
                        LeafNode lfNode = (LeafNode)childNodes[j];
                        XElement CategoryNode = new XElement("Item",
                                                    new XAttribute("Name", lfNode.Name),
                                                    new XAttribute("Percent", GetPercent(lfNode.Amount, Amount)), lfNode.Amount);
                        RegionalNode.Add(CategoryNode);
                    }

                    childNodes = dtNode.ChildNodes[1].ChildNodes;
                    Amount = dtNode.ChildNodes[1].Amount;
                    XElement TypeNode = new XElement("Type", new XAttribute("TypeName", "机型"), new XAttribute("Amount", Amount));
                    DateTimeNode.Add(TypeNode);

                    intCount = childNodes.Count();
                    for (int j = 0; j < intCount; j++)
                    {
                        LeafNode lfNode = (LeafNode)childNodes[j];
                        XElement CategoryNode = new XElement("Item", new XAttribute("Name", lfNode.Name),
                                        new XAttribute("Percent", GetPercent(lfNode.Amount, Amount)), lfNode.Amount);
                        TypeNode.Add(CategoryNode);
                    }
                }
            }
        }

        #endregion

        protected override void GenerateXmlContent(XElement RootNode)
        {
            RootNode.RemoveAll();
            //所有飞机
            List<Aircraft> AllAircraft = this.GetAllAircraft().ToList();
            //所有座级
            List<string> AircraftRegionalList = this.GetAllAircraftAircraftBusiness().Select(p => p.AircraftType.AircraftCategory.Regional).Distinct().ToList();
            //所有机型
            List<string> AircraftTypeList = this.GetAllAircraftAircraftBusiness().Select(p => p.AircraftType.Name).Distinct().ToList();

            DateTime startTime = GetOperationStartDate();
            DateTime endTime = GetOperationEndDate();
            for (DateTime time = startTime; time <= endTime; time = GetMonthEndofDateTime(time.AddMonths(1)))
            {

                //得到time对应时间的所有飞机,且在该时间点处于运行状态
                var MonthAircraft = AllAircraft.Where(p => p.OperationHistories.Any(pp => pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time))
                    && p.AircraftBusinesses.Any(pp => pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time)));
                //日期节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", FormatDate(time)));
                RootNode.Add(DateTimeNode);
                //总飞机数
                int Amount = MonthAircraft.Count();

                //座级
                XElement RegionalNode = new XElement("Type", new XAttribute("TypeName", "座级"), new XAttribute("Amount", Amount));
                DateTimeNode.Add(RegionalNode);
                // 所有座级
                foreach (string name in AircraftRegionalList)
                {
                    int AirNum = MonthAircraft.Count(p =>
                            p.AircraftBusinesses.FirstOrDefault(q => q.StartDate <= time && !(q.EndDate != null && q.EndDate < time)).AircraftType.AircraftCategory.Regional == name);
                    XElement CategoryNode = new XElement("Item", new XAttribute("Name", name), new XAttribute("Percent", GetPercent(AirNum, Amount)), AirNum);
                    RegionalNode.Add(CategoryNode);
                }

                //机型
                XElement TypeNode = new XElement("Type", new XAttribute("TypeName", "机型"), new XAttribute("Amount", Amount));
                DateTimeNode.Add(TypeNode);
                // 所有机型
                foreach (string name in AircraftTypeList)
                {
                    int AirNum = MonthAircraft.Count(p => p.AircraftBusinesses.FirstOrDefault(q => q.StartDate <= time && !(q.EndDate != null && q.EndDate < time)).AircraftType.Name == name);
                    XElement CategoryNode = new XElement("Item", new XAttribute("Name", name), new XAttribute("Percent", GetPercent(AirNum, Amount)), AirNum);
                    TypeNode.Add(CategoryNode);
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
                    XElement RootNode = new XElement("AircraftType");
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
