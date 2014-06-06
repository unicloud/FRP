#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：EnginePlanAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.EnginePlanQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AnnualAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EnginePlanAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.EnginePlanServices
{
    /// <summary>
    ///     实现备发计划服务接口。
    ///     用于处理备发计划相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class EnginePlanAppService : ContextBoundObject, IEnginePlanAppService
    {
        private readonly IActionCategoryRepository _actionCategoryRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IAnnualRepository _annualRepository;
        private readonly IEnginePlanQuery _enginePlanQuery;
        private readonly IEnginePlanRepository _enginePlanRepository;
        private readonly IEngineTypeRepository _engineTypeRepository;

        public EnginePlanAppService(IEnginePlanQuery enginePlanQuery, IActionCategoryRepository actionCategoryRepository,
            IAirlinesRepository airlinesRepository, IAnnualRepository annualRepository,
            IEnginePlanRepository enginePlanRepository, IEngineTypeRepository engineTypeRepository)
        {
            _enginePlanQuery = enginePlanQuery;
            _actionCategoryRepository = actionCategoryRepository;
            _airlinesRepository = airlinesRepository;
            _annualRepository = annualRepository;
            _enginePlanRepository = enginePlanRepository;
            _engineTypeRepository = engineTypeRepository;
        }

        #region EnginePlanDTO

        /// <summary>
        ///     获取所有备发计划
        /// </summary>
        /// <returns></returns>
        public IQueryable<EnginePlanDTO> GetEnginePlans()
        {
            var queryBuilder =
                new QueryBuilder<EnginePlan>();
            return _enginePlanQuery.EnginePlanDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增备发计划。
        /// </summary>
        /// <param name="dto">备发计划DTO。</param>
        [Insert(typeof (EnginePlanDTO))]
        public void InsertEnginePlan(EnginePlanDTO dto)
        {
            Airlines airlines = _airlinesRepository.GetAll().FirstOrDefault(p => p.IsCurrent); //获取航空公司
            Annual annual = _annualRepository.Get(dto.AnnualId); //获取计划年度

            //创建备发计划
            EnginePlan newEnginePlan = EnginePlanFactory.CreateEnginePlan(dto.VersionNumber);
            newEnginePlan.SetEnginePlanStatus(EnginePlanStatus.草稿);
            newEnginePlan.SetAirlines(airlines);
            newEnginePlan.SetAnnual(annual);
            newEnginePlan.SetTitle(dto.Title);
            newEnginePlan.SetNote(dto.Note);

            //添加
            dto.EnginePlanHistories.ToList().ForEach(line => InsertEnginePlanHistory(newEnginePlan, line));

            _enginePlanRepository.Add(newEnginePlan);
        }

        /// <summary>
        ///     更新备发计划。
        /// </summary>
        /// <param name="dto">备发计划DTO。</param>
        [Update(typeof (EnginePlanDTO))]
        public void ModifyEnginePlan(EnginePlanDTO dto)
        {
            Airlines airlines = _airlinesRepository.GetAll().FirstOrDefault(p => p.IsCurrent); //获取航空公司
            Annual annual = _annualRepository.Get(dto.AnnualId); //获取计划年度

            //获取需要更新的对象
            EnginePlan updateEnginePlan = _enginePlanRepository.Get(dto.Id);

            if (updateEnginePlan != null)
            {
                //更新主表：
                updateEnginePlan.SetEnginePlanStatus((EnginePlanStatus) dto.Status);
                updateEnginePlan.SetAirlines(airlines);
                updateEnginePlan.SetAnnual(annual);
                updateEnginePlan.SetDocNumber(dto.DocNumber);
                updateEnginePlan.SetDocument(dto.DocumentId, dto.DocName);
                updateEnginePlan.SetTitle(dto.Title);
                updateEnginePlan.SetNote(dto.Note);

                //更新接机行：
                List<EnginePlanHistoryDTO> dtoEnginePlanHistories = dto.EnginePlanHistories;
                ICollection<EnginePlanHistory> engienPlanHistories = updateEnginePlan.EnginePlanHistories;
                DataHelper.DetailHandle(dtoEnginePlanHistories.ToArray(),
                    engienPlanHistories.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertEnginePlanHistory(updateEnginePlan, i),
                    UpdateEnginePlanHistory,
                    d => _enginePlanRepository.RemoveEnginePlanHistory(d));
            }
            _enginePlanRepository.Modify(updateEnginePlan);
        }

        /// <summary>
        ///     删除备发计划。
        /// </summary>
        /// <param name="dto">备发计划DTO。</param>
        [Delete(typeof (EnginePlanDTO))]
        public void DeleteEnginePlan(EnginePlanDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            EnginePlan delEnginePlan = _enginePlanRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delEnginePlan != null)
            {
                _enginePlanRepository.DeleteEnginePlan(delEnginePlan); //删除备发计划。
            }
        }

        #region 处理计划明细

        /// <summary>
        ///     插入计划明细
        /// </summary>
        /// <param name="enginePlan">备发计划</param>
        /// <param name="enginePlanHistoryDto">计划历史DTO</param>
        private void InsertEnginePlanHistory(EnginePlan enginePlan, EnginePlanHistoryDTO enginePlanHistoryDto)
        {
            //获取
            ActionCategory actionCategory = _actionCategoryRepository.Get(enginePlanHistoryDto.ActionCategoryId);
            EngineType engineType = _engineTypeRepository.Get(enginePlanHistoryDto.EngineTypeId);
            Annual annual = _annualRepository.Get(enginePlanHistoryDto.PerformAnnualId);

            // 添加接机行
            EnginePlanHistory newEnginePlanHistory = enginePlan.AddNewEnginePlanHistory();
            newEnginePlanHistory.SetActionCategory(actionCategory);
            newEnginePlanHistory.SetEngineType(engineType);
            newEnginePlanHistory.SetMaxThrust(enginePlanHistoryDto.MaxThrust);
            newEnginePlanHistory.SetNote(enginePlanHistoryDto.Note);
            newEnginePlanHistory.SetPerformDate(annual, enginePlanHistoryDto.PerformMonth);
            newEnginePlanHistory.SetPlanEngine(enginePlanHistoryDto.PlanEngineId);
            newEnginePlanHistory.SetPlanStatus(EnginePlanDeliverStatus.计划);
        }

        /// <summary>
        ///     更新
        /// </summary>
        /// <param name="enginePlanHistoryDto">计划历史DTO</param>
        /// <param name="enginePlanHistory">计划历史</param>
        private void UpdateEnginePlanHistory(EnginePlanHistoryDTO enginePlanHistoryDto,
            EnginePlanHistory enginePlanHistory)
        {
            //获取
            ActionCategory actionCategory = _actionCategoryRepository.Get(enginePlanHistoryDto.ActionCategoryId);
            EngineType engineType = _engineTypeRepository.Get(enginePlanHistoryDto.EngineTypeId);
            Annual annual = _annualRepository.Get(enginePlanHistoryDto.PerformAnnualId);

            // 更新计划历史
            enginePlanHistory.SetActionCategory(actionCategory);
            enginePlanHistory.SetEngineType(engineType);
            enginePlanHistory.SetMaxThrust(enginePlanHistoryDto.MaxThrust);
            enginePlanHistory.SetNote(enginePlanHistoryDto.Note);
            enginePlanHistory.SetImportDate(enginePlanHistoryDto.ImportDate);
            enginePlanHistory.SetPerformDate(annual, enginePlanHistoryDto.PerformMonth);
            enginePlanHistory.SetPlanEngine(enginePlanHistoryDto.PlanEngineId);
            enginePlanHistory.SetPlanStatus((EnginePlanDeliverStatus) enginePlanHistoryDto.Status);
        }

        #endregion

        #endregion
    }
}