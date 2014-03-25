#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/03/23，23:03
// 文件名：PlanHistoryAppService.cs
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
using UniCloud.Application.FleetPlanBC.Query.PlanHistoryQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftPlanHistoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftPlanHistoryServices
{
    /// <summary>
    ///     实现计划明细服务接口。
    ///     用于处理计划明细相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class PlanHistoryAppService : IPlanHistoryAppService
    {
        private readonly IActionCategoryRepository _actionCategoryRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IAnnualRepository _annualRepository;
        private readonly IPlanHistoryQuery _planHistoryQuery;
        private readonly IPlanHistoryRepository _planHistoryRepository;

        public PlanHistoryAppService(IPlanHistoryQuery planHistoryQuery,
            IActionCategoryRepository actionCategoryRepository,
            IAircraftTypeRepository aircraftTypeRepository,
            IAircraftRepository aircraftRepository,
            IAirlinesRepository airlinesRepository,
            IAnnualRepository annualRepository,
            IPlanHistoryRepository planHistoryRepository)
        {
            _planHistoryQuery = planHistoryQuery;
            _actionCategoryRepository = actionCategoryRepository;
            _aircraftRepository = aircraftRepository;
            _aircraftTypeRepository = aircraftTypeRepository;
            _airlinesRepository = airlinesRepository;
            _annualRepository = annualRepository;
            _planHistoryRepository = planHistoryRepository;
        }

        #region PlanHistoryDTO

        /// <summary>
        ///     获取所有计划明细
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlanHistoryDTO> GetPlanHistories()
        {
            var queryBuilder =
                new QueryBuilder<PlanHistory>();
            return _planHistoryQuery.PlanHistoryDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增计划明细。
        /// </summary>
        /// <param name="dto">计划明细DTO。</param>
        [Insert(typeof(PlanHistoryDTO))]
        public void InsertPlanHistory(PlanHistoryDTO dto)
        {
            //获取
            var actionCategory = _actionCategoryRepository.Get(dto.ActionCategoryId);
            var targetCategory = _actionCategoryRepository.Get(dto.TargetCategoryId);
            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);
            var annual = _annualRepository.Get(dto.PerformAnnualId);
            // 添加接机行
            if (dto.PlanType == 1)
            {
                var newPlanHistory = PlanHistoryFactory.CreateOperationPlan(dto.PlanId);
                newPlanHistory.SetActionCategory(actionCategory, targetCategory);
                newPlanHistory.SetAircraftType(aircraftType);
                newPlanHistory.SetAirlines(airlines);
                newPlanHistory.SetCarryingCapacity(dto.CarryingCapacity);
                newPlanHistory.SetNote(dto.Note);
                newPlanHistory.SetPerformDate(annual, dto.PerformMonth);
                newPlanHistory.SetPlanAircraft(dto.PlanAircraftId);
                newPlanHistory.SetSeatingCapacity(dto.SeatingCapacity);
                _planHistoryRepository.Add(newPlanHistory);
            }
            else if (dto.PlanType == 2)
            {
                var newPlanHistory = PlanHistoryFactory.CreateChangePlan(dto.PlanId);
                newPlanHistory.SetActionCategory(actionCategory, targetCategory);
                newPlanHistory.SetAircraftType(aircraftType);
                newPlanHistory.SetAirlines(airlines);
                newPlanHistory.SetCarryingCapacity(dto.CarryingCapacity);
                newPlanHistory.SetNote(dto.Note);
                newPlanHistory.SetPerformDate(annual, dto.PerformMonth);
                newPlanHistory.SetPlanAircraft(dto.PlanAircraftId);
                newPlanHistory.SetSeatingCapacity(dto.SeatingCapacity);
                _planHistoryRepository.Add(newPlanHistory);
            }
        }

        /// <summary>
        ///     更新计划明细。
        /// </summary>
        /// <param name="dto">计划明细DTO。</param>
        [Update(typeof(PlanHistoryDTO))]
        public void ModifyPlanHistory(PlanHistoryDTO dto)
        {
            //获取
            var actionCategory = _actionCategoryRepository.Get(dto.ActionCategoryId);
            var targetCategory = _actionCategoryRepository.Get(dto.TargetCategoryId);
            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);
            var annual = _annualRepository.Get(dto.PerformAnnualId);
            var operationHistory = _aircraftRepository.GetPh(dto.RelatedGuid);
            var aircraftBusiness = _aircraftRepository.GetAb(dto.RelatedGuid);

            //获取需要更新的对象
            var updatePlanHistory = _planHistoryRepository.Get(dto.Id);

            // 更新计划历史
            if (dto.PlanType == 1)
            {
                updatePlanHistory.SetActionCategory(actionCategory, targetCategory);
                updatePlanHistory.SetAircraftType(aircraftType);
                updatePlanHistory.SetAirlines(airlines);
                updatePlanHistory.SetCarryingCapacity(dto.CarryingCapacity);
                updatePlanHistory.SetNote(dto.Note);
                updatePlanHistory.SetPerformDate(annual, dto.PerformMonth);
                updatePlanHistory.SetPlanAircraft(dto.PlanAircraftId);
                updatePlanHistory.SetSeatingCapacity(dto.SeatingCapacity);
                updatePlanHistory.SetApprovalHistory(dto.ApprovalHistoryId);
                var operationPlan = updatePlanHistory as OperationPlan;
                if (operationPlan != null)
                    operationPlan.SetOperationHistory(operationHistory);
            }
            else if (dto.PlanType == 2)
            {
                updatePlanHistory.SetActionCategory(actionCategory, targetCategory);
                updatePlanHistory.SetAircraftType(aircraftType);
                updatePlanHistory.SetAirlines(airlines);
                updatePlanHistory.SetCarryingCapacity(dto.CarryingCapacity);
                updatePlanHistory.SetNote(dto.Note);
                updatePlanHistory.SetPerformDate(annual, dto.PerformMonth);
                updatePlanHistory.SetPlanAircraft(dto.PlanAircraftId);
                updatePlanHistory.SetSeatingCapacity(dto.SeatingCapacity);
                updatePlanHistory.SetApprovalHistory(dto.ApprovalHistoryId);
                var changePlan = updatePlanHistory as ChangePlan;
                if (changePlan != null)
                    changePlan.SetAircraftBusiness(aircraftBusiness);
            }
            //if (updatePlanHistory.PlanAircraftId != null)
            //{
            //    var persitPlanAircraft = _planAircraftRepository.Get(updatePlanHistory.PlanAircraftId);
            //    if (persitPlanAircraft.Status != (ManageStatus)planHistoryDto.ManageStatus)
            //    {
            //        persitPlanAircraft.SetManageStatus((ManageStatus)planHistoryDto.ManageStatus);
            //    }
            //}
            _planHistoryRepository.Modify(updatePlanHistory);
        }

        /// <summary>
        ///     删除计划明细。
        /// </summary>
        /// <param name="dto">计划明细DTO。</param>
        [Delete(typeof(PlanHistoryDTO))]
        public void DeletePlanHistory(PlanHistoryDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delPlanHistory = _planHistoryRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delPlanHistory != null)
            {
                _planHistoryRepository.Remove(delPlanHistory); //删除计划明细。
            }
        }
        #endregion
    }
}
