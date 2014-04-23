#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：PlanAircraftAppService.cs
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
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.PlanAircraftQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.PlanAircraftAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.PlanAircraftServices
{
    /// <summary>
    ///     实现计划飞机服务接口。
    ///     用于处理计划飞机相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class PlanAircraftAppService : ContextBoundObject, IPlanAircraftAppService
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IPlanAircraftQuery _planAircraftQuery;
        private readonly IPlanAircraftRepository _planAircraftRepository;

        public PlanAircraftAppService(IPlanAircraftQuery planAircraftQuery,
            IAircraftRepository aircraftRepository,
            IAircraftTypeRepository aircraftTypeRepository,
            IAirlinesRepository airlinesRepository,
            IPlanAircraftRepository planAircraftRepository)
        {
            _planAircraftQuery = planAircraftQuery;
            _aircraftRepository = aircraftRepository;
            _aircraftTypeRepository = aircraftTypeRepository;
            _airlinesRepository = airlinesRepository;
            _planAircraftRepository = planAircraftRepository;
        }

        #region PlanAircraftDTO

        /// <summary>
        ///     获取所有计划飞机
        /// </summary>
        /// <returns></returns>
        public IQueryable<PlanAircraftDTO> GetPlanAircrafts()
        {
            var queryBuilder =
                new QueryBuilder<PlanAircraft>();
            return _planAircraftQuery.PlanAircraftDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增计划飞机。
        /// </summary>
        /// <param name="dto">计划飞机DTO。</param>
        [Insert(typeof (PlanAircraftDTO))]
        public void InsertPlanAircraft(PlanAircraftDTO dto)
        {
            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);

            //创建计划飞机
            var newPlanAircraft = PlanAircraftFactory.CreatePlanAircraft();
            newPlanAircraft.ChangeCurrentIdentity(dto.Id);
            newPlanAircraft.SetAircraftType(aircraftType);
            newPlanAircraft.SetAirlines(airlines);
            newPlanAircraft.SetLock();
            newPlanAircraft.SetOwn();
            newPlanAircraft.SetManageStatus(ManageStatus.计划);

            _planAircraftRepository.Add(newPlanAircraft);
        }

        /// <summary>
        ///     更新计划飞机。
        /// </summary>
        /// <param name="dto">计划飞机DTO。</param>
        [Update(typeof (PlanAircraftDTO))]
        public void ModifyPlanAircraft(PlanAircraftDTO dto)
        {
            
            var aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);

            //获取需要更新的对象
            var updatePlanAircraft = _planAircraftRepository.Get(dto.Id);

            if (updatePlanAircraft != null)
            {
                //更新主表：
                if (dto.AircraftId != null)
                {
                    var aircraft = _aircraftRepository.Get(dto.AircraftId);
                    updatePlanAircraft.SetAircraft(aircraft);
                }
                updatePlanAircraft.SetAircraftType(aircraftType);
                updatePlanAircraft.SetAirlines(airlines);
                updatePlanAircraft.SetLock();
                updatePlanAircraft.SetOwn();
                updatePlanAircraft.SetManageStatus((ManageStatus) dto.Status);
            }
            _planAircraftRepository.Modify(updatePlanAircraft);
        }

        /// <summary>
        ///     删除计划飞机。
        /// </summary>
        /// <param name="dto">计划飞机DTO。</param>
        [Delete(typeof (PlanAircraftDTO))]
        public void DeletePlanAircraft(PlanAircraftDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delPlanAircraft = _planAircraftRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delPlanAircraft != null)
            {
                _planAircraftRepository.Remove(delPlanAircraft); //删除计划飞机。
            }
        }

        #endregion
    }
}