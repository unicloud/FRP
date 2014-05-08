#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 9:46:24
// 文件名：AnnualMaintainPlanFactory
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 9:46:24
// 修改说明：
// ========================================================================*/
#endregion

namespace UniCloud.Domain.UberModel.Aggregates.AnnualMaintainPlanAgg
{
    /// <summary>
    /// AnnualMaintainPlan工厂。
    /// </summary>
    public static class AnnualMaintainPlanFactory
    {
        /// <summary>
        /// 创建发动机
        /// </summary>
        /// <returns></returns>
        public static EngineMaintainPlan CreatEngineMaintainPlan()
        {
            var engineMaintainPlan = new EngineMaintainPlan();
            engineMaintainPlan.GenerateNewIdentity();
            return engineMaintainPlan;
        }

        /// <summary>
        /// 创建发动机维修计划明细
        /// </summary>
        /// <returns></returns>
        public static EngineMaintainPlanDetail CreatEngineMaintainPlanDetail()
        {
            var detail = new EngineMaintainPlanDetail();
            detail.GenerateNewIdentity();
            return detail;
        }

        public static void SetEngineMaintainPlan(EngineMaintainPlan engineMaintainPlan, int maintainPlanType, decimal dollarRate, string companyLeader, string departmentLeader, string budgetManager, string phoneNumber)
        {
            engineMaintainPlan.MaintainPlanType = maintainPlanType;
            engineMaintainPlan.DollarRate = dollarRate;
            engineMaintainPlan.CompanyLeader = companyLeader;
            engineMaintainPlan.DepartmentLeader = departmentLeader;
            engineMaintainPlan.BudgetManager = budgetManager;
            engineMaintainPlan.PhoneNumber = phoneNumber;
        }

    }
}
