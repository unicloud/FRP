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
using System.Collections.Generic;
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
        ///     所有川航机型
        /// </summary>
        QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftTypes(Action loaded, bool forceLoad = false);

        /// <summary>
        ///     所有民航机型
        /// </summary>
        QueryableDataServiceCollectionView<CAACAircraftTypeDTO> GetCaacAircraftTypes(Action loaded, bool forceLoad = false);


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

        /// <summary>
        /// 活动类型集合
        /// </summary>
        List<ActionCategoryDTO> ActionCategories { get; }

        /// <summary>
        /// 机型集合
        /// </summary>
        List<AircraftTypeDTO> AircraftTypes { get; }

        /// <summary>
        /// 座级集合
        /// </summary>
        List<AircraftCategoryDTO> AircraftCategories { get; }


        #endregion

        #region 业务逻辑

        #region 计划
        /// <summary>
        /// 获取计划明细中集合属性ActionCategories的值
        /// </summary>
        /// <param name="ph"></param>
        ObservableCollection<ActionCateDTO> GetActionCategoriesForPlanHistory(PlanHistoryDTO ph);

        /// <summary>
        /// 获取计划明细中集合属性AircraftCategories的值
        /// </summary>
        /// <param name="ph"></param>
        ObservableCollection<AircraftCateDTO> GetAircraftCategoriesForPlanHistory(PlanHistoryDTO ph);

        /// <summary>
        /// 获取计划明细中集合属性AircraftTypes的值
        /// </summary>
        /// <param name="ph"></param>
        ObservableCollection<AircraftTyDTO> GetAircraftTypesForPlanHistory(PlanHistoryDTO ph);

        /// <summary>
        /// 当计划明细中活动类型发生变化的时候，需要改变相应属性：目标类型、净增座位、净增商载
        /// </summary>
        /// <param name="ph"></param>
        void OnChangedActionCategory(PlanHistoryDTO ph);

        /// <summary>
        ///     创建新年度的初始化计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="allPlanHistories"></param>
        /// <param name="newAnnual"></param>
        /// <returns>
        ///     新年度的初始化计划
        /// </returns>
        PlanDTO CreateNewYearPlan(PlanDTO lastPlan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories, AnnualDTO newAnnual);

        /// <summary>
        ///     创建新版本的运力增减计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="allPlanHistories"></param>
        /// <returns>新版本的运力增减计划</returns>
        PlanDTO CreateNewVersionPlan(PlanDTO lastPlan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories);

        /// <summary>
        ///     创建运力增减计划明细
        /// </summary>
        /// <param name="plan">计划 </param>
        /// <param name="allPlanHistories"></param>
        /// <param name="planAircraft">计划飞机</param>
        /// <param name="aircraft">运营飞机</param>
        /// <param name="actionType">活动类型</param>
        /// <param name="planType"></param>
        /// <returns>计划明细</returns>
        PlanHistoryDTO CreatePlanHistory(PlanDTO plan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories, ref PlanAircraftDTO planAircraft, AircraftDTO aircraft, string actionType, int planType);

        /// <summary>
        ///     完成运力增减计划
        /// </summary>
        /// <param name="planDetail">计划明细</param>
        /// <param name="aircraft">飞机</param>
        /// <param name="editAircraft">飞机</param>
        /// <returns>飞机</returns>
        void CompletePlan(PlanHistoryDTO planDetail, AircraftDTO aircraft, ref  AircraftDTO editAircraft);

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

        #region 数据传输

        /// <summary>
        ///  发送申请
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentRequest"></param>
        /// <param name="fleetContext"></param>
        void TransferRequest(Guid currentAirlines, Guid currentRequest, FleetPlanData fleetContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="fleetContext"></param>
        void TransferPlan(Guid currentAirlines, Guid currentPlan, FleetPlanData fleetContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentApprovalDoc"></param>
        /// <param name="fleetContext"></param>
        void TransferApprovalDoc(Guid currentAirlines, Guid currentApprovalDoc, FleetPlanData fleetContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlanHistory"></param>
        /// <param name="fleetContext"></param>
        void TransferPlanHistory(Guid currentAirlines, Guid currentPlanHistory, FleetPlanData fleetContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentOwnershipHistory"></param>
        /// <param name="fleetContext"></param>
        void TransferOwnershipHistory(Guid currentAirlines, Guid currentOwnershipHistory, FleetPlanData fleetContext);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <param name="fleetContext"></param>
        void TransferPlanAndRequest(Guid currentAirlines, Guid currentPlan, Guid currentRequest,
            FleetPlanData fleetContext);

        /// <summary>
        /// 传输计划申请批文（针对指标飞机数据）
        /// </summary>
        /// <param name="currentAirlines"></param>
        /// <param name="currentPlan"></param>
        /// <param name="currentRequest"></param>
        /// <param name="currentApprovalDoc"></param>
        /// <param name="fleetContext"></param>
        /// <returns></returns>
        void TransferApprovalRequest(Guid currentAirlines, Guid currentPlan, Guid currentRequest, Guid currentApprovalDoc, FleetPlanData fleetContext);

        #endregion
    }
}