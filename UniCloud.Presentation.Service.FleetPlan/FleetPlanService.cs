#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 14:17:13
// 文件名：FleetPlanService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 14:17:13
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.Service.FleetPlan
{
    [Export(typeof(IFleetPlanService))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetPlanService : ServiceBase, IFleetPlanService
    {
        public FleetPlanService()
        {
            context = new FleetPlanData(AgentHelper.FleetPlanServiceUri);
        }

        #region IFleetPlanService 成员

        public FleetPlanData Context
        {
            get { return context as FleetPlanData; }
        }

        #region 获取静态数据

        /// <summary>
        ///     所有航空公司
        /// </summary>
        public QueryableDataServiceCollectionView<AirlinesDTO> GetAirlineses(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.Airlineses, loaded, forceLoad);
        }

        /// <summary>
        ///     所有机型
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftTypes(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.AircraftTypes, loaded, forceLoad);
        }

        /// <summary>
        ///     获取供应商
        /// </summary>
        /// <param name="loaded">回调</param>
        /// <param name="forceLoad">是否强制加载</param>
        /// <returns>供应商集合</returns>
        public QueryableDataServiceCollectionView<SupplierDTO> GetSupplier(Action loaded, bool forceLoad = false)
        {
            return GetStaticData(Context.Suppliers, loaded, forceLoad);
        }

        /// <summary>
        ///     所有座级
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCategoryDTO> GetAircraftCategories(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.AircraftCategories, loaded, forceLoad);
        }

        /// <summary>
        ///     所有活动类型
        /// </summary>
        public QueryableDataServiceCollectionView<ActionCategoryDTO> GetActionCategories(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.ActionCategories, loaded, forceLoad);
        }

        /// <summary>
        ///     所有飞机系列
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftSeriesDTO> GetAircraftSeries(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.AircraftSeries, loaded, forceLoad);
        }
        #endregion

        #region 公共属性

        /// <summary>
        ///     民航局ID
        /// </summary>
        public string Caacid
        {
            get { return "31A9DE51-C207-4A73-919C-21521F17FEF9"; }
        }

        /// <summary>
        ///     当前航空公司
        /// </summary>
        public AirlinesDTO CurrentAirlines(bool forceLoad = false)
        {
            return GetStaticData(Context.Airlineses, () => { }, forceLoad).FirstOrDefault(a => a.IsCurrent);
        }

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
        public PlanDTO CreateNewYearPlan(PlanDTO lastPlan, Guid newAnnual, int newYear, AirlinesDTO curAirlines)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CreateNewYearPlan(lastPlan, newAnnual, newYear, curAirlines);
            }
        }

        /// <summary>
        /// 创建新版本的运力增减计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        public PlanDTO CreateNewVersionPlan(PlanDTO lastPlan)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CreateNewVersionPlan(lastPlan);
            }
        }

        /// <summary>
        /// 创建运力增减计划明细
        /// </summary>
        /// <param name="plan">计划</param>
        /// <param name="planAircraft">计划飞机</param>
        /// <param name="aircraft">计划飞机</param>
        /// <param name="actionType">活动类型</param>
        /// <param name="planType">判断是否运营\变更计划</param>
        /// <returns></returns>
        public PlanHistoryDTO CreatePlanHistory(PlanDTO plan,ref PlanAircraftDTO planAircraft,AircraftDTO aircraft, string actionType, int planType)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CreatePlanHistory(plan,ref planAircraft,aircraft,actionType, planType, this);
            }
        }

        /// <summary>
        ///  移除运力增减计划明细
        /// </summary>
        /// <param name="planDetail"><see cref="IFleetPlanService"/></param>
        public void RemovePlanDetail(PlanHistoryDTO planDetail)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                //pb.RemovePlanDetail(planDetail, this);
            }
        }

        /// <summary>
        /// 完成运力增减计划
        /// </summary>
        /// <param name="planDetail">计划明细</param>
        /// <param name="aircraft">飞机</param>
        /// <param name="editAircraft">飞机</param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        public void CompletePlan(PlanHistoryDTO planDetail,AircraftDTO aircraft,ref AircraftDTO editAircraft)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
              pb.CompletePlan(planDetail,aircraft,ref editAircraft, this);
            }
        }

        /// <summary>
        /// 获取所有有效的计划
        /// </summary>
        /// <returns><see cref="IFleetPlanService"/></returns>
        public ObservableCollection<PlanDTO> GetAllValidPlan()
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.GetAllValidPlan(this);
            }
        }

        #endregion

        #region 批文管理

        #endregion

        #region 运营管理

        /// <summary>
        /// 创建所有权历史
        /// </summary>
        /// <param name="aircraft"><see cref="IFleetPlanService"/></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        public OwnershipHistoryDTO CreateNewOwnership(AircraftDTO aircraft)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CreateNewOwnership(aircraft, this);
            }
        }

        /// <summary>
        /// 移除所有权历史
        /// </summary>
        /// <param name="ownership"><see cref="IFleetPlanService"/></param>
        public void RemoveOwnership(OwnershipHistoryDTO ownership)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                pb.RemoveOwnership(ownership, this);
            }
        }

        #endregion

        #endregion

        #endregion
    }
}