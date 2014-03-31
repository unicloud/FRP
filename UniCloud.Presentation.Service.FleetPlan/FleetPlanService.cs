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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
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
            context.WritingEntity += context_WritingEntity;
        }

        /// <summary>
        /// Properties marked with this Attribute are not serialized in the payload when sent to the server
        /// </summary>
        [AttributeUsage(AttributeTargets.Property)]
        public class DoNotSerializeAttribute : Attribute
        {
        }

        private void context_WritingEntity(object sender, ReadingWritingEntityEventArgs e)
        {
            // e.Data gives you the XElement for the Serialization of the Entity 
            //Using XLinq  , you can  add/Remove properties to the element Payload  
            XName xnEntityProperties = XName.Get("properties", e.Data.GetNamespaceOfPrefix("m").NamespaceName);
            XElement xePayload = null;
            foreach (PropertyInfo property in e.Entity.GetType().GetProperties())
            {
                object[] doNotSerializeAttributes = property.GetCustomAttributes(typeof(DoNotSerializeAttribute), false);
                if (doNotSerializeAttributes.Length > 0)
                {
                    if (xePayload == null)
                    {
                        xePayload =
                            e.Data.Descendants().First(xe => xe.Name == xnEntityProperties);
                    }
                    //The XName of the property we are going to remove from the payload
                    XName xnProperty = XName.Get(property.Name, e.Data.GetNamespaceOfPrefix("d").NamespaceName);
                    //Get the Property of the entity  you don't want sent to the server
                    foreach (XElement xeRemoveThisProperty in xePayload.Descendants(xnProperty).ToList())
                    {
                        //Remove this property from the Payload sent to the server 
                        xeRemoveThisProperty.Remove();
                    }
                }
            }
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
        ///     所有川航机型
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> GetAircraftTypes(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.AircraftTypes, loaded, forceLoad);
        }

        /// <summary>
        ///     所有民航机型
        /// </summary>
        public QueryableDataServiceCollectionView<CAACAircraftTypeDTO> GetCaacAircraftTypes(Action loaded,
            bool forceLoad = false)
        {
            return GetStaticData(Context.CaacAircraftTypes, loaded, forceLoad);
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

        /// <summary>
        /// 活动类型集合
        /// </summary>
        public List<ActionCategoryDTO> ActionCategories
        {
            get { return GetActionCategories(null).SourceCollection.Cast<ActionCategoryDTO>().ToList(); }
        }

        /// <summary>
        /// 机型集合
        /// </summary>
        public List<AircraftTypeDTO> AircraftTypes
        {
            get { return GetAircraftTypes(null).SourceCollection.Cast<AircraftTypeDTO>().ToList(); }
        }

        /// <summary>
        /// 座级集合
        /// </summary>
        public List<AircraftCategoryDTO> AircraftCategories
        {
            get { return GetAircraftCategories(null).SourceCollection.Cast<AircraftCategoryDTO>().ToList(); }
        }
        #endregion

        #region 业务逻辑

        #region 计划

        /// <summary>
        /// 获取计划明细中集合属性ActionCategories的值
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="service"></param>
        public ObservableCollection<ActionCateDTO> GetActionCategoriesForPlanHistory(PlanHistoryDTO ph)
        {
            List<ActionCategoryDTO> actioncategories;
            if (ph.PlanType == 1 && ph.ActionType == "引进")
                actioncategories = this.ActionCategories.Where(p => p.ActionType == "引进").ToList();
            else if (ph.PlanType == 1 && ph.ActionType == "退出")
                actioncategories = this.ActionCategories.Where(p => p.ActionType == "退出").ToList();
            else if (ph.PlanType == 2 && ph.ActionType == "变更")
                actioncategories = this.ActionCategories.Where(p => p.ActionType == "变更").ToList();
            else actioncategories = this.ActionCategories.ToList();
            ph.ActionCategories = new ObservableCollection<ActionCateDTO>();
            actioncategories.ForEach(p => ph.ActionCategories.Add(new ActionCateDTO
            {
                Id = p.Id,
                ActionType = p.ActionType,
                ActionName = p.ActionName,
                NeedRequest = p.NeedRequest,
            }));

            return ph.ActionCategories;
        }

        /// <summary>
        /// 获取计划明细中集合属性AircraftCategories的值
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="service"></param>
        public ObservableCollection<AircraftCateDTO> GetAircraftCategoriesForPlanHistory(PlanHistoryDTO ph)
        {
            List<AircraftCategoryDTO> aircraftCategories;
            if (ph.ActionName == "客改货")
                aircraftCategories = this.AircraftCategories.Where(p => p.Category == "货机").ToList();
            else if (ph.ActionName == "货改客")
                aircraftCategories = this.AircraftCategories.Where(p => p.Category == "客机").ToList();
            else aircraftCategories = this.AircraftCategories.ToList();
            ph.AircraftCategories = new ObservableCollection<AircraftCateDTO>();
            aircraftCategories.ForEach(p => ph.AircraftCategories.Add(new AircraftCateDTO
            {
                Id = p.Id,
                Category = p.Category,
                Regional = p.Regional,
            }));
            return ph.AircraftCategories;
        }

        /// <summary>
        /// 获取计划明细中集合属性AircraftTypes的值
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="service"></param>
        /// <returns></returns>
        public ObservableCollection<AircraftTyDTO> GetAircraftTypesForPlanHistory(PlanHistoryDTO ph)
        {
            List<AircraftTypeDTO> aircraftTypes;
            if (ph.Regional != null)
                aircraftTypes = this.AircraftTypes.Where(p => p.Regional == ph.Regional).ToList();
            else aircraftTypes = this.AircraftTypes.ToList();
            ph.AircraftTypes = new ObservableCollection<AircraftTyDTO>();
            aircraftTypes.ForEach(p => ph.AircraftTypes.Add(new AircraftTyDTO
            {
                Id = p.Id,
                Name = p.Name,
                AircraftCategoryId = p.AircraftCategoryId,
                Regional = p.Regional,
            }));

            return ph.AircraftTypes;
        }

        /// <summary>
        /// 当计划明细中活动类型发生变化的时候，需要改变相应属性：目标类型、净增座位、净增商载
        /// </summary>
        /// <param name="ph"></param>
        /// <param name="service"></param>
        public void OnChangedActionCategory(PlanHistoryDTO ph)
        {
            var actionCategory = this.ActionCategories.FirstOrDefault(ac => ac.Id == ph.ActionCategoryId);
            if (actionCategory != null)
            {
                if (actionCategory.ActionType == "退出")
                {
                    if (ph.PlanAircraftId != null && ph.AircraftId != null)
                    {
                        if (ph.SeatingCapacity > 0)
                            ph.SeatingCapacity = -ph.SeatingCapacity;
                        if (ph.CarryingCapacity > 0)
                            ph.CarryingCapacity = -ph.CarryingCapacity;
                    }
                }

                if (actionCategory.ActionType == "引进" || actionCategory.ActionType == "退出")
                {
                    if (ph.TargetCategoryId == ph.ActionCategoryId) return;
                    ph.TargetCategoryId = ph.ActionCategoryId;
                }
                else
                {
                    ActionCategoryDTO actionCategoryDTO;
                    // 改变目标引进方式
                    switch (actionCategory.ActionName)
                    {
                        case "一般改装":
                            if (ph.PlanAircraftId != null && ph.AircraftId != null)
                            {
                                var aircraftImport = ph.AircraftImportCategoryId == null
                                    ? Guid.Empty
                                    : Guid.Parse(ph.AircraftImportCategoryId.ToString());
                                if (ph.TargetCategoryId == aircraftImport) return;
                                ph.TargetCategoryId = aircraftImport;
                            }
                            break;
                        case "客改货":
                            if (ph.PlanAircraftId != null && ph.AircraftId != null)
                            {
                                var aircraftImport = ph.AircraftImportCategoryId == null
                                    ? Guid.Empty
                                    : Guid.Parse(ph.AircraftImportCategoryId.ToString());
                                if (ph.TargetCategoryId == aircraftImport) return;
                                ph.TargetCategoryId = aircraftImport;
                                ph.AircraftTypeId = Guid.Empty;

                            }
                            break;
                        case "货改客":
                            if (ph.PlanAircraftId != null && ph.AircraftId != null)
                            {
                                var aircraftImport = ph.AircraftImportCategoryId == null
                                    ? Guid.Empty
                                    : Guid.Parse(ph.AircraftImportCategoryId.ToString());
                                if (ph.TargetCategoryId == aircraftImport) return;
                                ph.TargetCategoryId = aircraftImport;
                                ph.AircraftTypeId = Guid.Empty;
                            }
                            break;
                        case "售后融资租赁":
                            actionCategoryDTO = this.ActionCategories.FirstOrDefault(a => a.ActionName == "融资租赁");
                            if (actionCategoryDTO != null)
                            {
                                if (ph.TargetCategoryId == actionCategoryDTO.Id) return;
                                else
                                    ph.TargetCategoryId = actionCategoryDTO.Id;
                            }

                            break;
                        case "售后经营租赁":
                            actionCategoryDTO = this.ActionCategories.FirstOrDefault(a => a.ActionName == "经营租赁");
                            if (actionCategoryDTO != null)
                            {
                                if (ph.TargetCategoryId == actionCategoryDTO.Id) return;
                                else
                                    ph.TargetCategoryId = actionCategoryDTO.Id;
                            }
                            break;
                        case "租转购":
                            actionCategoryDTO = this.ActionCategories.FirstOrDefault(a => a.ActionName == "购买");
                            if (actionCategoryDTO != null)
                            {
                                if (ph.TargetCategoryId == actionCategoryDTO.Id) return;
                                else
                                    ph.TargetCategoryId = actionCategoryDTO.Id;
                            }
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// 创建新年度的初始化计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="allPlanHistories"></param>
        /// <param name="newAnnual"></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        public PlanDTO CreateNewYearPlan(PlanDTO lastPlan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories, AnnualDTO newAnnual)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CreateNewYearPlan(lastPlan, allPlanHistories, newAnnual);
            }
        }

        /// <summary>
        /// 创建新版本的运力增减计划
        /// </summary>
        /// <param name="lastPlan"></param>
        /// <param name="allPlanHistories"></param>
        /// <returns><see cref="IFleetPlanService"/></returns>
        public PlanDTO CreateNewVersionPlan(PlanDTO lastPlan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CreateNewVersionPlan(lastPlan, allPlanHistories);
            }
        }

        /// <summary>
        /// 创建运力增减计划明细
        /// </summary>
        /// <param name="plan">计划</param>
        /// <param name="allPlanHistories"></param>
        /// <param name="planAircraft">计划飞机</param>
        /// <param name="aircraft">计划飞机</param>
        /// <param name="actionType">活动类型</param>
        /// <param name="planType">判断是否运营\变更计划</param>
        /// <returns></returns>
        public PlanHistoryDTO CreatePlanHistory(PlanDTO plan, QueryableDataServiceCollectionView<PlanHistoryDTO> allPlanHistories, ref PlanAircraftDTO planAircraft, AircraftDTO aircraft, string actionType, int planType)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                return pb.CreatePlanHistory(plan, allPlanHistories, ref planAircraft, aircraft, actionType, planType, this);
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
        public void CompletePlan(PlanHistoryDTO planDetail, AircraftDTO aircraft, ref AircraftDTO editAircraft)
        {
            using (var pb = new FleetPlanServiceHelper())
            {
                pb.CompletePlan(planDetail, aircraft, ref editAircraft, this);
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