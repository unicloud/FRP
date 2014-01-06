#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 14:17:06
// 文件名：IFleetPlanService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 14:17:06
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan
{
    public interface IFleetPlanService : IService
    {
        /// <summary>
        ///     数据服务上下文
        /// </summary>
        FleetPlanData Context { get; }


        #region 获取静态数据

        /// <summary>
        /// 所有航空公司
        /// </summary>
        QueryableDataServiceCollectionView<AirlinesDTO> GetAirlineses(bool forceLoad = false);

        /// <summary>
        /// 所有机型
        /// </summary>
        QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftTypes(bool forceLoad = false);
        #endregion

        #region 公共属性

        /// <summary>
        /// 民航局ID
        /// </summary>
        string Caacid { get; }

        /// <summary>
        /// 当前航空公司
        /// </summary>
        AirlinesDTO CurrentAirlines( bool forceLoad = false);

        /// <summary>
        /// 当前年度
        /// </summary>
        AnnualDTO CurrentAnnual(Action loaded, bool forceLoad = false);

        #endregion

        #region 业务逻辑

        #region 计划

        /// <summary>
        /// 创建新年度的初始化计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="newAnnual"></param>
        /// <param name="newYear"></param>
        /// <param name="curAirlines"></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        PlanDTO CreateNewYearPlan(PlanDTO lastPlan, Guid newAnnual, int newYear, AirlinesDTO curAirlines);

        /// <summary>
        /// 创建新版本的运力增减计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        PlanDTO CreateNewVersionPlan(PlanDTO lastPlan);

        /// <summary>
        /// 创建运力增减计划明细
        /// </summary>
        /// <param name="plan"><see cref="IFleetPlanService"/></param>
        /// <param name="planAircraft"><see cref="IFleetPlanService"/></param>
        /// <param name="actionType"><see cref="IFleetPlanService"/></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        PlanHistoryDTO CreatePlanHistory(PlanDTO plan, PlanAircraftDTO planAircraft, string actionType);

        /// <summary>
        ///  移除运力增减计划明细
        /// </summary>
        /// <param name="planDetail"><see cref="IFleetPlanService"/></param>
        void RemovePlanDetail(PlanHistoryDTO planDetail);

        /// <summary>
        /// 完成运力增减计划
        /// </summary>
        /// <param name="planDetail"><see cref="IFleetPlanService"/></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        AircraftDTO CompletePlan(PlanHistoryDTO planDetail);

        /// <summary>
        /// 获取所有有效的计划
        /// </summary>
        /// <returns><see cref="IFleetPlanService"/></returns>
        ObservableCollection<PlanDTO> GetAllValidPlan();

        #endregion

        #region 批文管理

        #endregion

        #region 运营管理

        /// <summary>
        /// 创建所有权历史
        /// </summary>
        /// <param name="aircraft"><see cref="IFleetPlanService"/></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        OwnershipHistoryDTO CreateNewOwnership(AircraftDTO aircraft);

        /// <summary>
        /// 移除所有权历史
        /// </summary>
        /// <param name="ownership"><see cref="IFleetPlanService"/></param>
        void RemoveOwnership(OwnershipHistoryDTO ownership);

        #endregion

        #endregion
    }
}