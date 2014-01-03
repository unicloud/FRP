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

using System.Collections.ObjectModel;
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

        #endregion

        #region 公共属性

        /// <summary>
        /// 民航局ID
        /// </summary>
        string Caacid { get; }

        /// <summary>
        /// 当前航空公司
        /// </summary>
        AirlinesDTO CurrentAirlines { get; }

        /// <summary>
        /// 当前年度
        /// </summary>
        AnnualDTO CurrentAnnual { get; }

        #endregion

        #region 业务逻辑

        #region 计划

        /// <summary>
        /// 设置当前年度
        /// </summary>
        AnnualDTO SetCurrentAnnual();

        /// <summary>
        /// 设置当前航空公司
        /// </summary>
        AirlinesDTO GetCurrentAirlines();

        /// <summary>
        /// 创建新年度的初始化计划
        /// </summary>
        /// <param name="title"><see cref="IFleetPlanService"/></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        PlanDTO CreateNewYearPlan(string title);

        /// <summary>
        /// 创建新版本的运力增减计划
        /// </summary>
        /// <param name="title"><see cref="IFleetPlanService"/></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        PlanDTO CreateNewVersionPlan(string title);

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