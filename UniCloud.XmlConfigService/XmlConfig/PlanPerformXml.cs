using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.XmlConfigAgg;

namespace UniCloud.Fleet.XmlConfigs
{

    public class PlanPerformXml : BaseXml
    {

        public PlanPerformXml(XmlConfigService service)
            : base(service)
        {
            _ConfigType = "计划执行";
        }


        /// <summary>
        /// 获取指定航空公司和年份的计划完成情况的百分比
        /// </summary>
        /// <param name="EnumerablePlan"></param>
        /// <returns></returns>
        private decimal GetPlanPerformance(IEnumerable<Plan> EnumerablePlan)
        {
            decimal Percent = 0;

            var enumerablePlan  = EnumerablePlan as Plan[] ?? EnumerablePlan.ToArray();
            if (EnumerablePlan != null && enumerablePlan.Any())
            {
                var planHistories = GetAllPlanHistory();
                Plan plan = enumerablePlan.FirstOrDefault();
                if (plan == null) return 0;
                decimal Amount = planHistories.Count(p => p.PlanId == plan.Id);
                decimal Finish = planHistories.OfType<OperationPlan>().Count(p => p.PlanId == plan.Id && p.OperationHistory != null) +
                            planHistories.OfType<ChangePlan>().Count(p => p.PlanId == plan.Id && p.AircraftBusiness != null);
                if (Amount != 0)
                {
                    Percent = Math.Round(Finish * 100 / Amount, 2);
                }

            }
            return Percent;
        }

        protected override void GenerateXmlContent(XElement RootNode)
        {
            //清空节点原有数据
            RootNode.RemoveAll();
            //当前版本的计划
            var AllPlan = GetAllPlan().ToList();
            //航空公司列表
            IEnumerable<string> AirLinesList = AllPlan.Select(p => p.Airlines.CnShortName).Distinct();
            //开始年
            int BeginYear = !AllPlan.Any() ? DateTime.Now.Year : AllPlan.Min(q => q.Annual.Year);
            //结束年
            int EndYear = !AllPlan.Any() ? DateTime.Now.Year : AllPlan.Max(q => q.Annual.Year);

            //按年生成每年的数据
            for (int year = BeginYear; year <= EndYear; year++)
            {
                //时间节点
                XElement DateTimeNode = new XElement("DateTime", new XAttribute("EndOfMonth", year.ToString()));
                RootNode.Add(DateTimeNode);

                //年度计划
                var YearPlan = AllPlan.Where(p => p.Annual.Year == year);
                //航空公司节点
                XElement TypeNode = new XElement("Type", new XAttribute("TypeName", "航空公司"));
                DateTimeNode.Add(TypeNode);
                //每个航空公司节点
                foreach (var name in AirLinesList) // 航空公司
                {
                    var AirlinesPlan = YearPlan.Where(p => p.Airlines.CnShortName == name);
                    decimal AirlinesPercent = GetPlanPerformance(AirlinesPlan);
                    XElement AirlinesNode = new XElement("Item", new XAttribute("Name", name), FormatDecimal(AirlinesPercent));
                    TypeNode.Add(AirlinesNode);
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
                    XElement RootNode = new XElement("PerformAnalyse");
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
