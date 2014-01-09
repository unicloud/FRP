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
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class FleetPlanService : ServiceBase, IFleetPlanService
    {
        private static AirlinesDTO _curAirlines;
        private static AnnualDTO _curAnnual;
        private static QueryableDataServiceCollectionView<AnnualDTO> _annual;
        private static QueryableDataServiceCollectionView<AirlinesDTO> _airlines;
        private static QueryableDataServiceCollectionView<AircraftTypeDTO> _aircraftTypes;

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
        /// 所有航空公司
        /// </summary>
        public QueryableDataServiceCollectionView<AirlinesDTO> GetAirlineses(bool forceLoad = false)
        {
            Action loaded = () => { };
            return GetStaticData(_airlines, loaded, Context.Airlineses);
        }

        /// <summary>
        /// 所有机型
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftTypes(bool forceLoad = false)
        {
            Action loaded = () => { };
            return GetStaticData(_aircraftTypes, loaded, Context.AircraftTypes);
        }

        #endregion

        #region 公共属性

        /// <summary>
        /// 民航局ID
        /// </summary>
        public string Caacid
        {
            get
            {
                return "31A9DE51-C207-4A73-919C-21521F17FEF9";
            }
        }

        /// <summary>
        /// 当前航空公司
        /// </summary>
        public AirlinesDTO CurrentAirlines(Action loaded, bool forceLoad = false)
        {
            Action loaded1 = () => { };
            var airlinesDescriptor = new FilterDescriptor("IsCurrent", FilterOperator.IsEqualTo, true);
            var airlines= GetStaticData(_curAirlines, loaded1, Context.Airlineses, airlinesDescriptor);
            return airlines;
        }

        /// <summary>
        /// 当前年度
        /// </summary>
        public AnnualDTO CurrentAnnual(Action loaded, bool forceLoad = false)
        {
            Action loaded1 = () => { };
            var annualDescriptor = new FilterDescriptor("IsOpen", FilterOperator.IsEqualTo, true);
            return GetStaticData(_curAnnual, loaded1, Context.Annuals, annualDescriptor);
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
        /// <param name="plan"><see cref="IFleetPlanService"/></param>
        /// <param name="planAircraft"><see cref="IFleetPlanService"/></param>
        /// <param name="actionType"><see cref="IFleetPlanService"/></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        public PlanHistoryDTO CreatePlanHistory(PlanDTO plan, PlanAircraftDTO planAircraft, string actionType)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CreateOperationPlan(plan, planAircraft, actionType, this);
            }
        }

        /// <summary>
        /// 完成运力增减计划
        /// </summary>
        /// <param name="planDetail"><see cref="IFleetPlanService"/></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        public AircraftDTO CompletePlan(PlanHistoryDTO planDetail)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CompletePlan(planDetail, this);
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