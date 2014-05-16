#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/13 17:46:44
// 文件名：FunctionItemData
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/13 17:46:44
// 修改说明：
// ========================================================================*/
#endregion

using System.Collections.Generic;
using UniCloud.Domain.UberModel.Aggregates.FunctionItemAgg;
using UniCloud.Infrastructure.Data.UberModel.UnitOfWork;

namespace UniCloud.Infrastructure.Data.UberModel.InitialData
{
    public class FunctionItemData : InitialDataBase
    {
        public FunctionItemData(UberModelUnitOfWork context)
            : base(context)
        {
        }


        /// <summary>
        ///     初始化承运人相关信息。
        /// </summary>
        /// <returns></returns>
        public override void InitialData()
        {
            var functionItems = new List<FunctionItem>();
            #region 飞行构型
            var menu1 = FunctionItemFactory.CreateFunctionItem("飞机构型", null, 1, false, false, string.Empty);
            functionItems.Add(menu1);

            var menu11 = FunctionItemFactory.CreateFunctionItem("管理飞机构型", menu1.Id, 101, false, false, string.Empty);
            var menu111 = FunctionItemFactory.CreateFunctionItem("维护飞机系列", menu11.Id, 10101, false, false, "UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig.ManagerAircraftSeries");
            var menu112 = FunctionItemFactory.CreateFunctionItem("维护飞机型号", menu11.Id, 10102, false, false, "UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig.ManagerAircraftType");
            var menu113 = FunctionItemFactory.CreateFunctionItem("维护飞机配置", menu11.Id, 10103, false, false, "UniCloud.Presentation.AircraftConfig.ManagerAircraftConfig.ManagerAircraftConfiguration");
            menu11.SubFunctionItems.Add(menu111);
            menu11.SubFunctionItems.Add(menu112);
            menu11.SubFunctionItems.Add(menu113);
            menu1.SubFunctionItems.Add(menu11);

            var menu12 = FunctionItemFactory.CreateFunctionItem("管理飞机数据", menu1.Id, 102, false, false, string.Empty);
            var menu121 = FunctionItemFactory.CreateFunctionItem("维护飞机证照", menu12.Id, 10201, false, false, "UniCloud.Presentation.AircraftConfig.ManagerAircraftData.ManagerAircraftLicense");
            menu12.SubFunctionItems.Add(menu121);
            menu1.SubFunctionItems.Add(menu12);


            #endregion

            #region 运力规划

            var menu2 = FunctionItemFactory.CreateFunctionItem("运力规划", null, 2, false, false, string.Empty);
            functionItems.Add(menu2);

            var menu21 = FunctionItemFactory.CreateFunctionItem("编制运力规划", menu2.Id, 201, false, false, string.Empty);
            var menu211 = FunctionItemFactory.CreateFunctionItem("民航机队规划", menu21.Id, 20101, false, false, "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.CaacProgramming");
            var menu212 = FunctionItemFactory.CreateFunctionItem("川航机队规划", menu21.Id, 20102, false, false, "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.AirProgramming");
            var menu213 = FunctionItemFactory.CreateFunctionItem("准备编制", menu21.Id, 20103, false, false, "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanPrepare");
            var menu214 = FunctionItemFactory.CreateFunctionItem("编制计划", menu21.Id, 20104, false, false, "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanLay");
            var menu215 = FunctionItemFactory.CreateFunctionItem("报送计划", menu21.Id, 20105, false, false, "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanSend");
            var menu216 = FunctionItemFactory.CreateFunctionItem("发布计划", menu21.Id, 20106, false, false, "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.FleetPlanPublish");
            var menu217 = FunctionItemFactory.CreateFunctionItem("编制备发计划", menu21.Id, 20107, false, false, "UniCloud.Presentation.FleetPlan.PrepareFleetPlan.SpareEnginePlanLay");
            menu21.SubFunctionItems.Add(menu211);
            menu21.SubFunctionItems.Add(menu212);
            menu21.SubFunctionItems.Add(menu213);
            menu21.SubFunctionItems.Add(menu214);
            menu21.SubFunctionItems.Add(menu215);
            menu21.SubFunctionItems.Add(menu216);
            menu21.SubFunctionItems.Add(menu217);
            menu2.SubFunctionItems.Add(menu21);

            var menu22 = FunctionItemFactory.CreateFunctionItem("执行运力规划", menu2.Id, 202, false, false, string.Empty);
            var menu221 = FunctionItemFactory.CreateFunctionItem("维护申请", menu22.Id, 20201, false, false, "UniCloud.Presentation.FleetPlan.Requests.Request");
            var menu222 = FunctionItemFactory.CreateFunctionItem("维护批文", menu22.Id, 20202, false, false, "UniCloud.Presentation.FleetPlan.Approvals.Approval");
            var menu223 = FunctionItemFactory.CreateFunctionItem("完成计划", menu22.Id, 20203, false, false, "UniCloud.Presentation.FleetPlan.PerformFleetPlan.FleetPlanDeliver");
            menu22.SubFunctionItems.Add(menu221);
            menu22.SubFunctionItems.Add(menu222);
            menu22.SubFunctionItems.Add(menu223);
            menu2.SubFunctionItems.Add(menu22);

            var menu23 = FunctionItemFactory.CreateFunctionItem("更新飞机数据", menu2.Id, 203, false, false, string.Empty);
            var menu231 = FunctionItemFactory.CreateFunctionItem("更新飞机数据", menu23.Id, 20301, false, false, "UniCloud.Presentation.FleetPlan.AircraftOwnerShips.AircraftOwnership");
            menu23.SubFunctionItems.Add(menu231);
            menu2.SubFunctionItems.Add(menu23);

            var menu24 = FunctionItemFactory.CreateFunctionItem("分析计划执行", menu2.Id, 204, false, false, string.Empty);
            var menu241 = FunctionItemFactory.CreateFunctionItem("查询计划", menu24.Id, 20401, false, false, "UniCloud.Presentation.FleetPlan.QueryPlans.QueryPlan");
            var menu242 = FunctionItemFactory.CreateFunctionItem("分析计划执行", menu24.Id, 20402, false, false, "UniCloud.Presentation.FleetPlan.PerformFleetPlan.QueryPerformPlan");
            var menu243 = FunctionItemFactory.CreateFunctionItem("查询申请", menu24.Id, 20403, false, false, "UniCloud.Presentation.FleetPlan.Requests.QueryRequest");
            var menu244 = FunctionItemFactory.CreateFunctionItem("查询批文", menu24.Id, 20404, false, false, "UniCloud.Presentation.FleetPlan.Approvals.QueryApproval");
            menu24.SubFunctionItems.Add(menu241);
            menu24.SubFunctionItems.Add(menu242);
            menu24.SubFunctionItems.Add(menu243);
            menu24.SubFunctionItems.Add(menu244);
            menu2.SubFunctionItems.Add(menu24);

            var menu25 = FunctionItemFactory.CreateFunctionItem("查询分析", menu2.Id, 205, false, false, string.Empty);
            var menu251 = FunctionItemFactory.CreateFunctionItem("查询飞机档案", menu25.Id, 20501, false, false, string.Empty);
            var menu252 = FunctionItemFactory.CreateFunctionItem("分析运力趋势", menu25.Id, 20502, false, false, "UniCloud.Presentation.FleetPlan.QueryAnalyse.FleetTrend");
            var menu253 = FunctionItemFactory.CreateFunctionItem("分析客机运力趋势", menu25.Id, 20503, false, false, "UniCloud.Presentation.FleetPlan.QueryAnalyse.PassengerAircraftTrend");
            var menu254 = FunctionItemFactory.CreateFunctionItem("统计在册飞机", menu25.Id, 20504, false, false, "UniCloud.Presentation.FleetPlan.QueryAnalyse.CountRegisteredFleet");
            var menu255 = FunctionItemFactory.CreateFunctionItem("分析飞机引进方式", menu25.Id, 20505, false, false, "UniCloud.Presentation.FleetPlan.QueryAnalyse.AircraftImportType");
            var menu256 = FunctionItemFactory.CreateFunctionItem("分析机队结构", menu25.Id, 20506, false, false, "UniCloud.Presentation.FleetPlan.QueryAnalyse.FleetStructure");
            var menu257 = FunctionItemFactory.CreateFunctionItem("分析飞机机龄", menu25.Id, 20507, false, false, "UniCloud.Presentation.FleetPlan.QueryAnalyse.FleetAge");
            menu25.SubFunctionItems.Add(menu251);
            menu25.SubFunctionItems.Add(menu252);
            menu25.SubFunctionItems.Add(menu253);
            menu25.SubFunctionItems.Add(menu254);
            menu25.SubFunctionItems.Add(menu255);
            menu25.SubFunctionItems.Add(menu256);
            menu25.SubFunctionItems.Add(menu257);
            menu2.SubFunctionItems.Add(menu25);

            #endregion

            #region 采购合同

            var menu3 = FunctionItemFactory.CreateFunctionItem("采购合同", null, 3, false, false, string.Empty);
            functionItems.Add(menu3);

            var menu31 = FunctionItemFactory.CreateFunctionItem("管理合作公司", menu3.Id, 301, false, false, string.Empty);
            var menu311 = FunctionItemFactory.CreateFunctionItem("维护供应商类别", menu31.Id, 30101, false, false, "UniCloud.Presentation.Purchase.Supplier.SupplierRoleManager");
            var menu312 = FunctionItemFactory.CreateFunctionItem("维护供应商可供物料", menu31.Id, 30102, false, false, "UniCloud.Presentation.Purchase.Supplier.SupplierMaterialManager");
            var menu313 = FunctionItemFactory.CreateFunctionItem("维护联系人", menu31.Id, 30103, false, false, "UniCloud.Presentation.Purchase.Supplier.LinkManManager");
            var menu314 = FunctionItemFactory.CreateFunctionItem("查询供应商", menu31.Id, 30104, false, false, "UniCloud.Presentation.Purchase.Supplier.QuerySupplier");
            var menu315 = FunctionItemFactory.CreateFunctionItem("维护BFE承运人", menu31.Id, 30105, false, false, "UniCloud.Presentation.Purchase.Forwarder.ForwarderManager");
            menu31.SubFunctionItems.Add(menu311);
            menu31.SubFunctionItems.Add(menu312);
            menu31.SubFunctionItems.Add(menu313);
            menu31.SubFunctionItems.Add(menu314);
            menu31.SubFunctionItems.Add(menu315);
            menu3.SubFunctionItems.Add(menu31);

            var menu32 = FunctionItemFactory.CreateFunctionItem("管理采购合同", menu3.Id, 302, false, false, string.Empty);
            var menu321 = FunctionItemFactory.CreateFunctionItem("管理飞机购买合同", menu32.Id, 30201, false, false, "UniCloud.Presentation.Purchase.Contract.AircraftPurchase");
            var menu322 = FunctionItemFactory.CreateFunctionItem("管理飞机租赁合同", menu32.Id, 30202, false, false, "UniCloud.Presentation.Purchase.Contract.AircraftLease");
            var menu323 = FunctionItemFactory.CreateFunctionItem("管理发动机购买合同", menu32.Id, 30203, false, false, "UniCloud.Presentation.Purchase.Contract.EnginePurchase");
            var menu324 = FunctionItemFactory.CreateFunctionItem("管理发动机租赁合同", menu32.Id, 30204, false, false, "UniCloud.Presentation.Purchase.Contract.EngineLease");
            var menu325 = FunctionItemFactory.CreateFunctionItem("管理BFE合同", menu32.Id, 30205, false, false, "UniCloud.Presentation.Purchase.Contract.BFEPurchase");
            menu32.SubFunctionItems.Add(menu321);
            menu32.SubFunctionItems.Add(menu322);
            menu32.SubFunctionItems.Add(menu323);
            menu32.SubFunctionItems.Add(menu324);
            menu32.SubFunctionItems.Add(menu325);
            menu3.SubFunctionItems.Add(menu32);

            var menu33 = FunctionItemFactory.CreateFunctionItem("管理维修合同", menu3.Id, 303, false, false, string.Empty);
            var menu331 = FunctionItemFactory.CreateFunctionItem("管理发动机维修合同", menu33.Id, 30301, false, false, "UniCloud.Presentation.Purchase.Contract.EngineMaintain");
            var menu332 = FunctionItemFactory.CreateFunctionItem("管理APU维修合同", menu33.Id, 30302, false, false, "UniCloud.Presentation.Purchase.Contract.ApuMaintain");
            var menu333 = FunctionItemFactory.CreateFunctionItem("管理起落架维修合同", menu33.Id, 30303, false, false, "UniCloud.Presentation.Purchase.Contract.UndercartMaintain");
            var menu334 = FunctionItemFactory.CreateFunctionItem("管理机身维修合同", menu33.Id, 30304, false, false, "UniCloud.Presentation.Purchase.Contract.AirframeMaintain");
            menu33.SubFunctionItems.Add(menu331);
            menu33.SubFunctionItems.Add(menu332);
            menu33.SubFunctionItems.Add(menu333);
            menu33.SubFunctionItems.Add(menu334);
            menu3.SubFunctionItems.Add(menu33);

            var menu34 = FunctionItemFactory.CreateFunctionItem("管理接机", menu3.Id, 304, false, false, string.Empty);
            var menu341 = FunctionItemFactory.CreateFunctionItem("匹配计划飞机", menu34.Id, 30401, false, false, "UniCloud.Presentation.Purchase.Reception.MatchingPlanAircraftManager");
            var menu342 = FunctionItemFactory.CreateFunctionItem("维护租赁飞机交付项目", menu34.Id, 30402, false, false, "UniCloud.Presentation.Purchase.Reception.AircraftLeaseReceptionManager");
            var menu343 = FunctionItemFactory.CreateFunctionItem("维护采购飞机交付项目", menu34.Id, 30403, false, false, "UniCloud.Presentation.Purchase.Reception.AircraftPurchaseReceptionManager");
            var menu344 = FunctionItemFactory.CreateFunctionItem("维护租赁发动机交付项目", menu34.Id, 30404, false, false, "UniCloud.Presentation.Purchase.Reception.EngineLeaseReceptionManager");
            var menu345 = FunctionItemFactory.CreateFunctionItem("维护采购发动机交付项目", menu34.Id, 30405, false, false, "UniCloud.Presentation.Purchase.Reception.EnginePurchaseReceptionManager");
            menu34.SubFunctionItems.Add(menu341);
            menu34.SubFunctionItems.Add(menu342);
            menu34.SubFunctionItems.Add(menu343);
            menu34.SubFunctionItems.Add(menu344);
            menu34.SubFunctionItems.Add(menu345);
            menu3.SubFunctionItems.Add(menu34);

            var menu35 = FunctionItemFactory.CreateFunctionItem("查询分析", menu3.Id, 305, false, false, string.Empty);
            var menu351 = FunctionItemFactory.CreateFunctionItem("合同管理", menu35.Id, 30501, false, false, "UniCloud.Presentation.Purchase.Contract.ManageContracts.ManageContract");
            var menu352 = FunctionItemFactory.CreateFunctionItem("查询合同", menu35.Id, 30502, false, false, "UniCloud.Presentation.Purchase.Contract.QueryContracts.QueryContractMain");
            var menu353 = FunctionItemFactory.CreateFunctionItem("分析飞机价格", menu35.Id, 30503, false, false, "UniCloud.Presentation.Purchase.QueryAnalyse.AnalyseAircraftPrice");
            var menu354 = FunctionItemFactory.CreateFunctionItem("分析发动机价格", menu35.Id, 30504, false, false, string.Empty);
            menu35.SubFunctionItems.Add(menu351);
            menu35.SubFunctionItems.Add(menu352);
            menu35.SubFunctionItems.Add(menu353);
            menu35.SubFunctionItems.Add(menu354);
            menu3.SubFunctionItems.Add(menu35);


            #endregion

            #region 应付款

            var menu4 = FunctionItemFactory.CreateFunctionItem("应付款", null, 4, false, false, string.Empty);
            functionItems.Add(menu4);
            var menu41 = FunctionItemFactory.CreateFunctionItem("管理付款计划", menu4.Id, 401, false, false, string.Empty);
            var menu411 = FunctionItemFactory.CreateFunctionItem("管理飞机付款计划", menu41.Id, 40101, false, false, "UniCloud.Presentation.Payment.PaymentSchedules.AcPaymentSchedule");
            var menu412 = FunctionItemFactory.CreateFunctionItem("管理发动机付款计划", menu41.Id, 40102, false, false, "UniCloud.Presentation.Payment.PaymentSchedules.EnginePaymentSchedule");
            var menu413 = FunctionItemFactory.CreateFunctionItem("管理BFE付款计划", menu41.Id, 40103, false, false, "UniCloud.Presentation.Payment.PaymentSchedules.StandardPaymentSchedule");
            var menu414 = FunctionItemFactory.CreateFunctionItem("管理维修付款计划", menu41.Id, 40104, false, false, "UniCloud.Presentation.Payment.PaymentSchedules.MaintainPaymentSchedule");
            menu41.SubFunctionItems.Add(menu411);
            menu41.SubFunctionItems.Add(menu412);
            menu41.SubFunctionItems.Add(menu413);
            menu41.SubFunctionItems.Add(menu414);
            menu4.SubFunctionItems.Add(menu41);

            var menu42 = FunctionItemFactory.CreateFunctionItem("管理采购发票", menu4.Id, 402, false, false, string.Empty);
            var menu421 = FunctionItemFactory.CreateFunctionItem("维护采购发票", menu42.Id, 40201, false, false, "UniCloud.Presentation.Payment.Invoice.PurchaseInvoiceManager");
            var menu422 = FunctionItemFactory.CreateFunctionItem("维护采购预付款发票", menu42.Id, 40202, false, false, "UniCloud.Presentation.Payment.Invoice.PurchasePrepayInvoiceManager");
            var menu423 = FunctionItemFactory.CreateFunctionItem("维护维修预付款发票", menu42.Id, 40203, false, false, "UniCloud.Presentation.Payment.Invoice.MaintainPrepayInvoiceManager");
            var menu424 = FunctionItemFactory.CreateFunctionItem("维护租赁发票", menu42.Id, 40204, false, false, "UniCloud.Presentation.Payment.Invoice.LeaseInvoiceManager");
            var menu425 = FunctionItemFactory.CreateFunctionItem("维护采购贷项单", menu42.Id, 40205, false, false, "UniCloud.Presentation.Payment.Invoice.PurchaseCreditNoteManager");
            var menu426 = FunctionItemFactory.CreateFunctionItem("维护维修贷项单", menu42.Id, 40206, false, false, "UniCloud.Presentation.Payment.Invoice.MaintainCreditNoteManager");
            var menu427 = FunctionItemFactory.CreateFunctionItem("维护杂项发票", menu42.Id, 40207, false, false, "UniCloud.Presentation.Payment.Invoice.SundryInvoiceManager");
            var menu428 = FunctionItemFactory.CreateFunctionItem("维护特修改装发票", menu42.Id, 40208, false, false, "UniCloud.Presentation.Payment.Invoice.SpecialRefitInvoiceManager");
            menu42.SubFunctionItems.Add(menu421);
            menu42.SubFunctionItems.Add(menu422);
            menu42.SubFunctionItems.Add(menu423);
            menu42.SubFunctionItems.Add(menu424);
            menu42.SubFunctionItems.Add(menu425);
            menu42.SubFunctionItems.Add(menu426);
            menu42.SubFunctionItems.Add(menu427);
            menu42.SubFunctionItems.Add(menu428);
            menu4.SubFunctionItems.Add(menu42);


            var menu43 = FunctionItemFactory.CreateFunctionItem("管理维修发票", menu4.Id, 403, false, false, string.Empty);
            var menu431 = FunctionItemFactory.CreateFunctionItem("维护发动机维修发票", menu43.Id, 40301, false, false, "UniCloud.Presentation.Payment.MaintainInvoice.EngineMaintain");
            var menu432 = FunctionItemFactory.CreateFunctionItem("维护APU维修发票", menu43.Id, 40302, false, false, "UniCloud.Presentation.Payment.MaintainInvoice.APUMaintain");
            var menu433 = FunctionItemFactory.CreateFunctionItem("维护起落架维修发票", menu43.Id, 40303, false, false, "UniCloud.Presentation.Payment.MaintainInvoice.UndercartMaintain");
            var menu434 = FunctionItemFactory.CreateFunctionItem("维护机身维修发票", menu43.Id, 40304, false, false, "UniCloud.Presentation.Payment.MaintainInvoice.AirframeMaintain");
            menu43.SubFunctionItems.Add(menu431);
            menu43.SubFunctionItems.Add(menu432);
            menu43.SubFunctionItems.Add(menu433);
            menu43.SubFunctionItems.Add(menu434);
            menu4.SubFunctionItems.Add(menu43);

            var menu44 = FunctionItemFactory.CreateFunctionItem("维护付款通知", menu4.Id, 404, false, false, "UniCloud.Presentation.Payment.PaymentNotice.PaymentNotice");
            menu4.SubFunctionItems.Add(menu44);

            var menu45 = FunctionItemFactory.CreateFunctionItem("管理保函", menu4.Id, 405, false, false, string.Empty);
            var menu451 = FunctionItemFactory.CreateFunctionItem("维护租赁保证金", menu45.Id, 40501, false, false, "UniCloud.Presentation.Payment.Guarantees.LeaseGuarantee");
            var menu452 = FunctionItemFactory.CreateFunctionItem("维护大修储备金", menu45.Id, 40502, false, false, "UniCloud.Presentation.Payment.Guarantees.MaintainGuarantee");
            menu45.SubFunctionItems.Add(menu451);
            menu45.SubFunctionItems.Add(menu452);
            menu4.SubFunctionItems.Add(menu45);

            var menu46 = FunctionItemFactory.CreateFunctionItem("管理飞机价格", menu4.Id, 406, false, false, string.Empty);
            var menu461 = FunctionItemFactory.CreateFunctionItem("设置飞机价格公式", menu46.Id, 40601, false, false, string.Empty);
            var menu462 = FunctionItemFactory.CreateFunctionItem("设置发动机价格公式", menu46.Id, 40602, false, false, string.Empty);
            var menu463 = FunctionItemFactory.CreateFunctionItem("计算飞机价格", menu46.Id, 40603, false, false, string.Empty);
            menu46.SubFunctionItems.Add(menu461);
            menu46.SubFunctionItems.Add(menu462);
            menu46.SubFunctionItems.Add(menu463);
            menu4.SubFunctionItems.Add(menu46);

            var menu47 = FunctionItemFactory.CreateFunctionItem("维修成本", menu4.Id, 407, false, false, string.Empty);
            var menu471 = FunctionItemFactory.CreateFunctionItem("定检", menu47.Id, 40701, false, false, "UniCloud.Presentation.Payment.MaintainCost.RegularCheckMaintainCostManage");
            var menu472 = FunctionItemFactory.CreateFunctionItem("预测资金需求", menu47.Id, 40702, false, false, "UniCloud.Presentation.Payment.QueryAnalyse.FinancingDemandForecast");
            var menu473 = FunctionItemFactory.CreateFunctionItem("起落架", menu47.Id, 40703, false, false, "UniCloud.Presentation.Payment.MaintainCost.UndercartMaintainCostManage");
            var menu474 = FunctionItemFactory.CreateFunctionItem("特修改装", menu47.Id, 40704, false, false, "UniCloud.Presentation.Payment.MaintainCost.SpecialRefitMaintainCostManage");
            menu47.SubFunctionItems.Add(menu471);
            menu47.SubFunctionItems.Add(menu472);
            menu47.SubFunctionItems.Add(menu473);
            menu47.SubFunctionItems.Add(menu474);
            menu4.SubFunctionItems.Add(menu47);

            var menu48 = FunctionItemFactory.CreateFunctionItem("查询分析", menu4.Id, 408, false, false, string.Empty);
            var menu481 = FunctionItemFactory.CreateFunctionItem("查询付款计划", menu48.Id, 40801, false, false, "UniCloud.Presentation.Payment.PaymentSchedules.QueryPaymentSchedule");
            var menu482 = FunctionItemFactory.CreateFunctionItem("预测资金需求", menu48.Id, 40802, false, false, "UniCloud.Presentation.Payment.QueryAnalyse.FinancingDemandForecast");
            var menu483 = FunctionItemFactory.CreateFunctionItem("分析付款计划执行", menu48.Id, 40803, false, false, "UniCloud.Presentation.Payment.QueryAnalyse.PaymentScheduleExecute");
            var menu484 = FunctionItemFactory.CreateFunctionItem("分析维修成本", menu48.Id, 40804, false, false, "UniCloud.Presentation.Payment.QueryAnalyse.AnalyseMaintenanceCosts");
            menu48.SubFunctionItems.Add(menu481);
            menu48.SubFunctionItems.Add(menu482);
            menu48.SubFunctionItems.Add(menu483);
            menu48.SubFunctionItems.Add(menu484);
            menu4.SubFunctionItems.Add(menu48);

            #endregion

            #region 项目管理

            var menu5 = FunctionItemFactory.CreateFunctionItem("项目管理", null, 5, false, false, string.Empty);
            functionItems.Add(menu5);

            var menu51 = FunctionItemFactory.CreateFunctionItem("配置工作组", menu5.Id, 501, false, false, "UniCloud.Presentation.Project.Template.WorkGroup");
            var menu52 = FunctionItemFactory.CreateFunctionItem("配置任务模板", menu5.Id, 502, false, false, string.Empty);
            var menu53 = FunctionItemFactory.CreateFunctionItem("配置项目模板", menu5.Id, 503, false, false, string.Empty);
            var menu54 = FunctionItemFactory.CreateFunctionItem("管理项目计划", menu5.Id, 504, false, false, string.Empty);

            menu5.SubFunctionItems.Add(menu51);
            menu5.SubFunctionItems.Add(menu52);
            menu5.SubFunctionItems.Add(menu53);
            menu5.SubFunctionItems.Add(menu54);

            #endregion

            #region 适航管理

            var menu6 = FunctionItemFactory.CreateFunctionItem("适航管理", null, 6, false, false, string.Empty);
            functionItems.Add(menu6);
            var menu61 = FunctionItemFactory.CreateFunctionItem("查询AD/SB", menu6.Id, 601, false, false, "UniCloud.Presentation.Part.ManageAdSb.QueryAdSb");
            menu6.SubFunctionItems.Add(menu61);

            #endregion

            #region 发动机管理

            var menu7 = FunctionItemFactory.CreateFunctionItem("发动机管理", null, 7, false, false, string.Empty);
            functionItems.Add(menu7);

            var menu71 = FunctionItemFactory.CreateFunctionItem("管理基础配置", menu7.Id, 701, false, false, "UniCloud.Presentation.Part.BaseConfigurations.BaseConfiguration");
            menu7.SubFunctionItems.Add(menu71);

            var menu72 = FunctionItemFactory.CreateFunctionItem("维护件序号", menu7.Id, 702, false, false, "UniCloud.Presentation.Part.PnRegAndSnReg.ManagePnAndSnView");
            menu7.SubFunctionItems.Add(menu72);

            var menu73 = FunctionItemFactory.CreateFunctionItem("管理技术解决方案", menu7.Id, 703, false, false, "UniCloud.Presentation.Part.ManageTS.TechnicalSolutionView");
            menu7.SubFunctionItems.Add(menu73);

            var menu74 = FunctionItemFactory.CreateFunctionItem("管理发动机构型", menu7.Id, 704, false, false, string.Empty);
            var menu741 = FunctionItemFactory.CreateFunctionItem("管理基本构型组", menu74.Id, 70401, false, false, "UniCloud.Presentation.Part.EngineConfig.BasicConfigGroupView");
            var menu742 = FunctionItemFactory.CreateFunctionItem("管理特定选型", menu74.Id, 70402, false, false, "UniCloud.Presentation.Part.EngineConfig.SpecialConfigView");
            var menu743 = FunctionItemFactory.CreateFunctionItem("比较构型差异", menu74.Id, 70403, false, false, "UniCloud.Presentation.Part.EngineConfig.ConfigCompareView");
            menu74.SubFunctionItems.Add(menu741);
            menu74.SubFunctionItems.Add(menu742);
            menu74.SubFunctionItems.Add(menu743);
            menu7.SubFunctionItems.Add(menu74);

            var menu75 = FunctionItemFactory.CreateFunctionItem("滑油监控", menu7.Id, 705, false, false, string.Empty);
            var menu751 = FunctionItemFactory.CreateFunctionItem("管理发动机滑油", menu75.Id, 70501, false, false, "UniCloud.Presentation.Part.OilMonitor.EngineOil");
            var menu752 = FunctionItemFactory.CreateFunctionItem("管理APU滑油", menu75.Id, 70502, false, false, string.Empty);
            menu75.SubFunctionItems.Add(menu751);
            menu75.SubFunctionItems.Add(menu752);
            menu7.SubFunctionItems.Add(menu75);

            var menu76 = FunctionItemFactory.CreateFunctionItem("修正飞机利用率", menu7.Id, 706, false, false, "UniCloud.Presentation.Part.MaintainAcDailyUtilization.CorrectAcDailyUtilization");
            menu7.SubFunctionItems.Add(menu76);

            var menu77 = FunctionItemFactory.CreateFunctionItem("维护拆换记录", menu7.Id, 707, false, false, "UniCloud.Presentation.Part.SnHistories.SnHistory");
            menu7.SubFunctionItems.Add(menu77);

            var menu78 = FunctionItemFactory.CreateFunctionItem("维护在位信息", menu7.Id, 708, false, false, string.Empty);
            menu7.SubFunctionItems.Add(menu78);

            var menu79 = FunctionItemFactory.CreateFunctionItem("查询拆装历史", menu7.Id, 709, false, false, "UniCloud.Presentation.Part.SnHistories.QuerySnHistory");
            menu7.SubFunctionItems.Add(menu79);

            var menu710 = FunctionItemFactory.CreateFunctionItem("控制维修", menu7.Id, 710, false, false, string.Empty);
            var menu7101 = FunctionItemFactory.CreateFunctionItem("查看控制方案", menu7.Id, 71001, false, false, string.Empty);
            var menu7102 = FunctionItemFactory.CreateFunctionItem("查询到寿日期", menu7.Id, 71002, false, false, string.Empty);
            menu710.SubFunctionItems.Add(menu7101);
            menu710.SubFunctionItems.Add(menu7102);
            menu7.SubFunctionItems.Add(menu710);

            #endregion

            #region 附件管理

            var menu8 = FunctionItemFactory.CreateFunctionItem("附件管理", null, 8, false, false, string.Empty);
            functionItems.Add(menu8);

            var menu81 = FunctionItemFactory.CreateFunctionItem("管理SCN/MSCN", menu8.Id, 801, false, false, string.Empty);
            var menu811 = FunctionItemFactory.CreateFunctionItem("维护SCN/MSCN", menu81.Id, 80101, false, false, "UniCloud.Presentation.Part.ManageSCN.MaintainScn");
            var menu812 = FunctionItemFactory.CreateFunctionItem("对比SCN/MSCN", menu81.Id, 80102, false, false, "UniCloud.Presentation.Part.ManageSCN.CompareScn");
            menu81.SubFunctionItems.Add(menu811);
            menu81.SubFunctionItems.Add(menu812);
            menu8.SubFunctionItems.Add(menu81);


            var menu82 = FunctionItemFactory.CreateFunctionItem("维护结构损伤", menu8.Id, 802, false, false, "UniCloud.Presentation.Part.ManageAirStructureDamage.MaintainAirStructureDamage");
            menu8.SubFunctionItems.Add(menu82);

            var menu83 = FunctionItemFactory.CreateFunctionItem("管理年度送修计划", menu8.Id, 803, false, false, string.Empty);
            var menu831 = FunctionItemFactory.CreateFunctionItem("管理发动机年度送修计划", menu83.Id, 80301, false, false, "UniCloud.Presentation.Part.ManageAnnualMaintainPlan.ManageEngineMaintainPlan");
            var menu832 = FunctionItemFactory.CreateFunctionItem("管理机身、起落架年度送修计划", menu83.Id, 80302, false, false, "UniCloud.Presentation.Part.ManageAnnualMaintainPlan.ManageAircraftMaintainPlan");
            menu83.SubFunctionItems.Add(menu831);
            menu83.SubFunctionItems.Add(menu832);
            menu8.SubFunctionItems.Add(menu83);
            #endregion

            #region 基础管理

            var menu9 = FunctionItemFactory.CreateFunctionItem("基础管理", null, 9, false, false, string.Empty);
            functionItems.Add(menu9);

            var menu91 = FunctionItemFactory.CreateFunctionItem("管理授权", menu9.Id, 901, false, false, string.Empty);
            var menu911 = FunctionItemFactory.CreateFunctionItem("管理用户", menu91.Id, 90101, false, false, "UniCloud.Presentation.BaseManagement.ManagePermission.ManageUser");
            var menu912 = FunctionItemFactory.CreateFunctionItem("管理权限", menu91.Id, 90102, false, false, "UniCloud.Presentation.BaseManagement.ManagePermission.ManageFunctionsInRole");
            var menu913 = FunctionItemFactory.CreateFunctionItem("管理用户角色", menu91.Id, 90103, false, false, "UniCloud.Presentation.BaseManagement.ManagePermission.ManageUserInRole");
            var menu914 = FunctionItemFactory.CreateFunctionItem("管理组织机构角色", menu91.Id, 90104, false, false, "UniCloud.Presentation.BaseManagement.ManagePermission.ManageOrganizationInRole");
            menu91.SubFunctionItems.Add(menu911);
            menu91.SubFunctionItems.Add(menu912);
            menu91.SubFunctionItems.Add(menu913);
            menu91.SubFunctionItems.Add(menu914);
            menu9.SubFunctionItems.Add(menu91);

            var menu92 = FunctionItemFactory.CreateFunctionItem("管理运营资质", menu9.Id, 902, false, false, string.Empty);
            var menu921 = FunctionItemFactory.CreateFunctionItem("维护证照种类", menu92.Id, 90201, false, false, "UniCloud.Presentation.AircraftConfig.ManagerAircraftData.ManagerLicenseType");
            var menu922 = FunctionItemFactory.CreateFunctionItem("维护经营证照", menu92.Id, 90202, false, false, "UniCloud.Presentation.BaseManagement.ManageOperationQualification.ManageBusinessLicense");
            menu92.SubFunctionItems.Add(menu921);
            menu92.SubFunctionItems.Add(menu922);
            menu9.SubFunctionItems.Add(menu92);

            var menu93 = FunctionItemFactory.CreateFunctionItem("维护基础配置", menu9.Id, 903, false, false, string.Empty);
            var menu931 = FunctionItemFactory.CreateFunctionItem("维护分支机构", menu93.Id, 90301, false, false, "UniCloud.Presentation.BaseManagement.ManageSubsidiary.BranchCompany");
            var menu932 = FunctionItemFactory.CreateFunctionItem("管理系统配置", menu93.Id, 90302, false, false, "UniCloud.Presentation.BaseManagement.MaintainBaseSettings.ManageSystemConfig");
            var menu933 = FunctionItemFactory.CreateFunctionItem("管理提醒策略", menu93.Id, 90303, false, false, string.Empty);
            var menu934 = FunctionItemFactory.CreateFunctionItem("配置邮件账号", menu93.Id, 90304, false, false, "UniCloud.Presentation.BaseManagement.MaintainBaseSettings.ConfigMailAddress");
            menu93.SubFunctionItems.Add(menu931);
            menu93.SubFunctionItems.Add(menu932);
            menu93.SubFunctionItems.Add(menu933);
            menu93.SubFunctionItems.Add(menu934);
            menu9.SubFunctionItems.Add(menu93);

            #endregion

            #region 文档库

            var menu10 = FunctionItemFactory.CreateFunctionItem("文档库", null, 10, false, false, string.Empty);
            functionItems.Add(menu10);

            var menu101 = FunctionItemFactory.CreateFunctionItem("维护文档类型", menu10.Id, 1001, false, false, "UniCloud.Presentation.CommonService.DocumentTypeManager.ManagerDocumentType");
            var menu102 = FunctionItemFactory.CreateFunctionItem("搜索文档", menu10.Id, 1002, false, false, "UniCloud.Presentation.CommonService.SearchDocument.SearchDocumentMain");
            menu10.SubFunctionItems.Add(menu101);
            menu10.SubFunctionItems.Add(menu102);

            #endregion
            functionItems.ForEach(p => Context.FunctionItems.Add(p));
        }
    }
}
