using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

namespace UniCloud.Fleet.XmlConfigs
{

    public class SupplierXml : BaseXml
    {

        public SupplierXml(XmlConfigService service)
            : base(service)
        {
            _ConfigType = "供应商";
        }

        protected override void GenerateXmlContent(XElement RootNode)
        {
            //清空节点原有数据
            RootNode.RemoveAll();
            //获取所有飞机
            var AllAircraft = this.GetAllAircraft();
            //供应商列表
            IEnumerable<string> SupplierList = _XmlService.AllOwnershipHistory.Select(q => q.Supplier.CnShortName).Distinct();
            //按月生成每个月的数据
            DateTime startTime = GetOperationStartDate();
            DateTime endTime = GetOperationEndDate();
            for (DateTime time = startTime; time <= endTime; time = GetMonthEndofDateTime(time.AddMonths(1)))
            {
                //时间节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", FormatDate(time)));
                RootNode.Add(DateTimeNode);
                //当前时间点的飞机总数
                int Amount = AllAircraft.Count(p => p.OperationHistories.Any(pp => pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time)) &&
                                                    p.OwnershipHistories.Any(pp => pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time)));
                //供应商节点
                XElement SupplierNode = new XElement("Type", new XAttribute("TypeName", "供应商"), new XAttribute("Amount", Amount));
                DateTimeNode.Add(SupplierNode);

                //各个供应商数据节点
                foreach (var name in SupplierList)
                {
                    int AirNum = AllAircraft.Count(p => p.OperationHistories.Any(pp => pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time)) &&
                                                        p.OwnershipHistories.Any(pp => pp.Supplier.CnShortName == name &&
                                                                                       pp.StartDate <= time && !(pp.EndDate != null && pp.EndDate < time)));
                    XElement EachSupplierNode = new XElement("Item", new XAttribute("Name", name), new XAttribute("Percent", GetPercent(AirNum, Amount)), AirNum);
                    SupplierNode.Add(EachSupplierNode);
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
                    XElement RootNode = new XElement("Supplier");
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
