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
        ///     所有航空公司
        /// </summary>
        QueryableDataServiceCollectionView<AirlinesDTO> GetAirlineses(Action loaded, bool forceLoad = false);

        /// <summary>
        ///     所有机型
        /// </summary>
        QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftTypes(Action loaded, bool forceLoad = false);

        /// <summary>
        ///     获取供应商
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <param name="forceLoad">是否强制加载</param>
        /// <returns>供应商集合</returns>
        QueryableDataServiceCollectionView<SupplierDTO> GetSupplier(Action loaded, bool forceLoad = false);

        /// <summary>
        ///     所有座级
        /// </summary>
        QueryableDataServiceCollectionView<AircraftCategoryDTO> GetAircraftCategories(Action loaded,
            bool forceLoad = false);

        /// <summary>
        ///     所有活动类型
        /// </summary>
        QueryableDataServiceCollectionView<ActionCategoryDTO> GetActionCategories(Action loaded,
            bool forceLoad = false);

        /// <summary>
        ///     所有飞机系列
        /// </summary>
        QueryableDataServiceCollectionView<AircraftSeriesDTO> GetAircraftSeries(Action loaded,
            bool forceLoad = false);
        #endregion

        #region 公共属性

        /// <summary>
        ///     民航局ID
        /// </summary>
        string Caacid { get; }

        /// <summary>
        ///     当前航空公司
        /// </summary>
        AirlinesDTO CurrentAirlines(bool forceLoad = false);

        #endregion

        #region 业务逻辑

        #region 计划

        /// <summary>
        ///     创建新年度的初始化计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="newAnnual"></param>
        /// <param name="newYear"></param>
        /// <param name="curAirlines"></param>
        /// <returns>
        ///     新年度的初始化计划
        /// </returns>
        PlanDTO CreateNewYearPlan(PlanDTO lastPlan, Guid newAnnual, int newYear, AirlinesDTO curAirlines);

        /// <summary>
        ///     创建新版本的运力增减计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <returns>新版本的运力增减计划</returns>
        PlanDTO CreateNewVersionPlan(PlanDTO lastPlan);

        /// <summary>
        ///     创建运力增减计划明细
        /// </summary>
        /// <param name="plan">计划 </param>
        /// <param name="planAircraft">计划飞机</param>
        /// <param name="actionType">活动类型</param>
        /// <returns>计划明细</returns>
        PlanHistoryDTO CreatePlanHistory(PlanDTO plan, PlanAircraftDTO planAircraft, string actionType, int planType);


        /// <summary>
        ///     移除运力增减计划明细
        /// </summary>
        /// <param name="planDetail">计划明细</param>
        void RemovePlanDetail(PlanHistoryDTO planDetail);
        /// <summary>
        ///     完成运力增减计划
        /// </summary>
        /// <param name="planDetail">计划明细</param>
        /// <returns>飞机</returns>
        AircraftDTO CompletePlan(PlanHistoryDTO planDetail);

        /// <summary>
        ///     获取所有有效的计划
        /// </summary>
        /// <returns>有效计划集合</returns>
        ObservableCollection<PlanDTO> GetAllValidPlan();

        #endregion

        #region 批文管理

        #endregion

        #region 运营管理

        /// <summary>
        ///     创建所有权历史
        /// </summary>
        /// <param name="aircraft">运营飞机</param>
        /// <returns>所有权历史记录</returns>
        OwnershipHistoryDTO CreateNewOwnership(AircraftDTO aircraft);

        /// <summary>
        ///     移除所有权历史
        /// </summary>
        /// <param name="ownership">所有权历史记录</param>
        void RemoveOwnership(OwnershipHistoryDTO ownership);

        #endregion

        #endregion
    }
}