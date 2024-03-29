﻿#region Version Info
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

using System;

namespace UniCloud.Domain.PartBC.Aggregates.AnnualMaintainPlanAgg
{
    /// <summary>
    /// AnnualMaintainPlan工厂。
    /// </summary>
    public static class AnnualMaintainPlanFactory
    {
        /// <summary>
        /// 创建发动机维修计划
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

        public static void SetEngineMaintainPlan(EngineMaintainPlan engineMaintainPlan, int maintainPlanType, decimal dollarRate, string companyLeader, string departmentLeader,
            string budgetManager, string phoneNumber, Guid annual)
        {
            engineMaintainPlan.MaintainPlanType = maintainPlanType;
            engineMaintainPlan.DollarRate = dollarRate;
            engineMaintainPlan.CompanyLeader = companyLeader;
            engineMaintainPlan.DepartmentLeader = departmentLeader;
            engineMaintainPlan.BudgetManager = budgetManager;
            engineMaintainPlan.PhoneNumber = phoneNumber;
            engineMaintainPlan.AnnualId = annual;
        }

        public static void SetEngineMaintainPlanDetail(EngineMaintainPlanDetail engineMaintainPlanDetail, decimal changeLlpFee, int changeLlpNumber, decimal customsTax,
            string engineNumber, decimal freightFee, DateTime inMaintainDate, string maintainLevel, decimal nonFhaFee, string note, DateTime outMaintainDate, decimal partFee,
            string tsnCsn, string tsrCsr, decimal feeLittleSum, decimal feeTotalSum, decimal budgetToalSum)
        {
            engineMaintainPlanDetail.ChangeLlpFee = changeLlpFee;
            engineMaintainPlanDetail.ChangeLlpNumber = changeLlpNumber;
            engineMaintainPlanDetail.CustomsTax = customsTax;
            engineMaintainPlanDetail.EngineNumber = engineNumber;
            engineMaintainPlanDetail.FreightFee = freightFee;
            engineMaintainPlanDetail.InMaintainDate = inMaintainDate;
            engineMaintainPlanDetail.MaintainLevel = maintainLevel;
            engineMaintainPlanDetail.NonFhaFee = nonFhaFee;
            engineMaintainPlanDetail.Note = note;
            engineMaintainPlanDetail.OutMaintainDate = outMaintainDate;
            engineMaintainPlanDetail.PartFee = partFee;
            engineMaintainPlanDetail.TsnCsn = tsnCsn;
            engineMaintainPlanDetail.TsrCsr = tsrCsr;
            engineMaintainPlanDetail.FeeLittleSum = feeLittleSum;
            engineMaintainPlanDetail.FeeTotalSum = feeTotalSum;
            engineMaintainPlanDetail.BudgetToalSum = budgetToalSum;
        }

        /// <summary>
        /// 创建飞机维修计划
        /// </summary>
        /// <returns></returns>
        public static AircraftMaintainPlan CreatAircraftMaintainPlan()
        {
            var aircraftMaintainPlan = new AircraftMaintainPlan();
            aircraftMaintainPlan.GenerateNewIdentity();
            return aircraftMaintainPlan;
        }

        /// <summary>
        /// 创建飞机维修计划明细
        /// </summary>
        /// <returns></returns>
        public static AircraftMaintainPlanDetail CreatAircraftMaintainPlanDetail()
        {
            var detail = new AircraftMaintainPlanDetail();
            detail.GenerateNewIdentity();
            return detail;
        }

        public static void SetAircraftMaintainPlan(AircraftMaintainPlan aircraftMaintainPlan, int firstHalfYear, int secondHalfYear, string note, Guid annual)
        {
            aircraftMaintainPlan.FirstHalfYear = firstHalfYear;
            aircraftMaintainPlan.SecondHalfYear = secondHalfYear;
            aircraftMaintainPlan.Note = note;
            aircraftMaintainPlan.AnnualId = annual;
        }

        public static void SetAircraftMaintainPlanDetail(AircraftMaintainPlanDetail aircraftMaintainPlanDetail, string aircraftNumber, string aircraftType,
           string level, DateTime inDate, DateTime outDate, int cycle)
        {
            aircraftMaintainPlanDetail.AircraftNumber = aircraftNumber;
            aircraftMaintainPlanDetail.AircraftType = aircraftType;
            aircraftMaintainPlanDetail.Level = level;
            aircraftMaintainPlanDetail.InDate = inDate;
            aircraftMaintainPlanDetail.OutDate = outDate;
            aircraftMaintainPlanDetail.Cycle = cycle;
        }
    }
}
