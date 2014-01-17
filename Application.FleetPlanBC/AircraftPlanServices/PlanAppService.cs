#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：PlanAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.AircraftPlanQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftPlanServices
{
    /// <summary>
    ///     实现运力增减计划服务接口。
    ///     用于处理运力增减计划相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class PlanAppService : IPlanAppService
    {
        private readonly IActionCategoryRepository _actionCategoryRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IAnnualRepository _annualRepository;
        private readonly IPlanQuery _planQuery;
        private readonly IPlanRepository _planRepository;

        public PlanAppService(IPlanQuery planQuery, IActionCategoryRepository actionCategoryRepository
            , IAircraftTypeRepository aircraftTypeRepository, IAirlinesRepository airlinesRepository,
            IAnnualRepository annualRepository,IAircraftRepository aircraftRepository,
            IPlanRepository planRepository)
        {
            _planQuery = planQuery;
            _actionCategoryRepository = actionCategoryRepository;
            _aircraftRepository = aircraftRepository;
            _aircraftTypeRepository = aircraftTypeRepository;
            _airlinesRepository = airlinesRepository;
            _annualRepository = annualRepository;
            _planRepository = planRepository;
        }

        #region PlanDTO

        /// <summary>
        ///     获取所有运力增减计划
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlanDTO> GetPlans()
        {
            var queryBuilder =
                new QueryBuilder<Plan>();
            return _planQuery.PlanDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增运力增减计划。
        /// </summary>
        /// <param name="dto">运力增减计划DTO。</param>
        [Insert(typeof(PlanDTO))]
        public void InsertPlan(PlanDTO dto)
        {
            var airlines = _airlinesRepository.Get(dto.AirlinesId); //获取航空公司
            var annual = _annualRepository.Get(dto.AnnualId); //获取计划年度

            //创建运力增减计划
            var newPlan = PlanFactory.CreatePlan(dto.VersionNumber);
            newPlan.SetPlanStatus(PlanStatus.草稿);
            newPlan.SetAirlines(airlines);
            newPlan.SetAnnual(annual);
            newPlan.SetTitle(dto.Title);

            //添加
            dto.PlanHistories.ToList().ForEach(line => InsertPlanHistory(newPlan, line));

            _planRepository.Add(newPlan);
        }

        /// <summary>
        ///     更新运力增减计划。
        /// </summary>
        /// <param name="dto">运力增减计划DTO。</param>
        [Update(typeof (PlanDTO))]
        public void ModifyPlan(PlanDTO dto)
        {
            var airlines = _airlinesRepository.Get(dto.AirlinesId); //获取航空公司
            var annual = _annualRepository.Get(dto.AnnualId); //获取计划年度

            //获取需要更新的对象
            var updatePlan = _planRepository.Get(dto.Id);

            if (updatePlan != null)
            {
                //更新主表：
                updatePlan.SetPlanStatus((PlanStatus)dto.Status);
                updatePlan.SetPlanPublishStatus((PlanPublishStatus)dto.PublishStatus);
                updatePlan.SetAirlines(airlines);
                updatePlan.SetAnnual(annual);
                updatePlan.SetDocNumber(dto.DocNumber);
                updatePlan.SetDocument(dto.DocumentId, dto.DocName);
                updatePlan.SetTitle(dto.Title);

                //更新计划明细：
                var dtoPlanHistories = dto.PlanHistories;
                var planHistories = updatePlan.PlanHistories;
                DataHelper.DetailHandle(dtoPlanHistories.ToArray(),
                    planHistories.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertPlanHistory(updatePlan, i),
                    UpdatePlanHistory,
                    d => _planRepository.RemovePlanHistory(d));
            }
            _planRepository.Modify(updatePlan);
        }

        /// <summary>
        ///     删除运力增减计划。
        /// </summary>
        /// <param name="dto">运力增减计划DTO。</param>
        [Delete(typeof (PlanDTO))]
        public void DeletePlan(PlanDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delPlan = _planRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delPlan != null)
            {
                _planRepository.DeletePlan(delPlan); //删除运力增减计划。
            }
        }

        #region 处理计划明细

        /// <summary>
        ///     插入计划明细
        /// </summary>
        /// <param name="plan">运力增减计划</param>
        /// <param name="planHistoryDto">计划历史DTO</param>
        private void InsertPlanHistory(Plan plan, PlanHistoryDTO planHistoryDto)
        {
            //获取
            var actionCategory = _actionCategoryRepository.Get(planHistoryDto.ActionCategoryId);
            var targetCategory = _actionCategoryRepository.Get(planHistoryDto.TargetCategoryId);
            var aircraftType = _aircraftTypeRepository.Get(planHistoryDto.AircraftTypeId);
            var airlines = _airlinesRepository.Get(planHistoryDto.AirlinesId);
            var annual = _annualRepository.Get(planHistoryDto.PerformAnnualId);
            // 添加接机行
            if (planHistoryDto.PlanType == 1)
            {
                var newPlanHistory = plan.AddNewOperationPlan();
                newPlanHistory.SetActionCategory(actionCategory, targetCategory);
                newPlanHistory.SetAircraftType(aircraftType);
                newPlanHistory.SetAirlines(airlines);
                newPlanHistory.SetCarryingCapacity(planHistoryDto.CarryingCapacity);
                newPlanHistory.SetNote(planHistoryDto.Note);
                newPlanHistory.SetPerformDate(annual, planHistoryDto.PerformMonth);
                newPlanHistory.SetPlanAircraft(planHistoryDto.PlanAircraftId);
                newPlanHistory.SetSeatingCapacity(planHistoryDto.SeatingCapacity);
            }
            else if (planHistoryDto.PlanType == 2)
            {
                var newPlanHistory = plan.AddNewChangePlan();
                newPlanHistory.SetActionCategory(actionCategory, targetCategory);
                newPlanHistory.SetAircraftType(aircraftType);
                newPlanHistory.SetAirlines(airlines);
                newPlanHistory.SetCarryingCapacity(planHistoryDto.CarryingCapacity);
                newPlanHistory.SetNote(planHistoryDto.Note);
                newPlanHistory.SetPerformDate(annual, planHistoryDto.PerformMonth);
                newPlanHistory.SetPlanAircraft(planHistoryDto.PlanAircraftId);
                newPlanHistory.SetSeatingCapacity(planHistoryDto.SeatingCapacity);
            }
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="planHistoryDto">计划历史DTO</param>
        /// <param name="planHistory">计划历史</param>
        private void UpdatePlanHistory(PlanHistoryDTO planHistoryDto, PlanHistory planHistory)
        {
            //获取
            var actionCategory = _actionCategoryRepository.Get(planHistoryDto.ActionCategoryId);
            var targetCategory = _actionCategoryRepository.Get(planHistoryDto.TargetCategoryId);
            var aircraftType = _aircraftTypeRepository.Get(planHistoryDto.AircraftTypeId);
            var airlines = _airlinesRepository.Get(planHistoryDto.AirlinesId);
            var annual = _annualRepository.Get(planHistoryDto.PerformAnnualId);
            var operationHistory = _aircraftRepository.GetPh(planHistoryDto.RelatedGuid);
            var aircraftBusiness = _aircraftRepository.GetAb(planHistoryDto.RelatedGuid);

            // 更新计划历史
            if (planHistoryDto.PlanType == 1)
            {
                planHistory.SetActionCategory(actionCategory, targetCategory);
                planHistory.SetAircraftType(aircraftType);
                planHistory.SetAirlines(airlines);
                planHistory.SetCarryingCapacity(planHistoryDto.CarryingCapacity);
                planHistory.SetNote(planHistoryDto.Note);
                planHistory.SetPerformDate(annual, planHistoryDto.PerformMonth);
                planHistory.SetPlanAircraft(planHistoryDto.PlanAircraftId);
                planHistory.SetSeatingCapacity(planHistoryDto.SeatingCapacity);
                planHistory.SetApprovalHistory(planHistoryDto.ApprovalHistoryId);
                var operationPlan = planHistory as OperationPlan;
                if (operationPlan != null)
                    operationPlan.SetOperationHistory(operationHistory);
            }
            else if (planHistoryDto.PlanType == 2)
            {
                planHistory.SetActionCategory(actionCategory, targetCategory);
                planHistory.SetAircraftType(aircraftType);
                planHistory.SetAirlines(airlines);
                planHistory.SetCarryingCapacity(planHistoryDto.CarryingCapacity);
                planHistory.SetNote(planHistoryDto.Note);
                planHistory.SetPerformDate(annual, planHistoryDto.PerformMonth);
                planHistory.SetPlanAircraft(planHistoryDto.PlanAircraftId);
                planHistory.SetSeatingCapacity(planHistoryDto.SeatingCapacity);
                planHistory.SetApprovalHistory(planHistoryDto.ApprovalHistoryId);
                var changePlan = planHistory as ChangePlan;
                if (changePlan != null)
                    changePlan.SetAircraftBusiness(aircraftBusiness);
            }
        }

        #endregion

        #endregion
    }
}