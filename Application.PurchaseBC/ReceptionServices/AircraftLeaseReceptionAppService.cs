﻿#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:40:26
// 文件名：AircraftLeaseReceptionAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.ReceptionServices
{
    /// <summary>
    ///     租赁飞机接收项目服务实现
    /// </summary>
    [LogAOP]
    public class AircraftLeaseReceptionAppService : ContextBoundObject, IAircraftLeaseReceptionAppService
    {
        private readonly IContractAircraftRepository _contractAircraftRepository;
        private readonly IAircraftLeaseReceptionQuery _dtoQuery;
        private readonly IReceptionRepository _receptionRepository;
        private readonly ISupplierRepository _supplierRepository;

        public AircraftLeaseReceptionAppService(IAircraftLeaseReceptionQuery dtoQuery,
            IReceptionRepository receptionRepository,
            ISupplierRepository supplierRepository,
            IContractAircraftRepository contractAircraftRepository)
        {
            _dtoQuery = dtoQuery;
            _receptionRepository = receptionRepository;
            _supplierRepository = supplierRepository;
            _contractAircraftRepository = contractAircraftRepository;
        }

        #region AircraftLeaseReceptionDTO

        /// <summary>
        ///     获取所有租赁飞机接收项目
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftLeaseReceptionDTO> GetAircraftLeaseReceptions()
        {
            var queryBuilder =
                new QueryBuilder<AircraftLeaseReception>();
            return _dtoQuery.AircraftLeaseReceptionDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增租赁飞机接收项目。
        /// </summary>
        /// <param name="dto">租赁飞机接收项目DTO。</param>
        [Insert(typeof (AircraftLeaseReceptionDTO))]
        public void InsertAircraftLeaseReception(AircraftLeaseReceptionDTO dto)
        {
            //获取供应商
            Supplier supplier = _supplierRepository.Get(dto.SupplierId);

            //创建接机项目
            AircraftLeaseReception newReception = ReceptionFactory.CreateAircraftLeaseReception(dto.StartDate,
                dto.EndDate, dto.SourceId,
                dto.Description);

            // TODO:设置接机编号,如果当天的记录被删除过，流水号seq可能会重复
            DateTime date = DateTime.Now.Date;
            int seq = _receptionRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            newReception.SetReceptionNumber(seq);

            //设置供应商
            newReception.SetSupplier(supplier);

            //设置接机的状态
            newReception.SetStatus(ReceptionStatus.开始);

            //添加接机行
            dto.ReceptionLines.ToList().ForEach(line => InsertReceptionLine(newReception, line));
            //添加相关的接机日程
            dto.ReceptionSchedules.ToList().ForEach(scheduel => InsertReceptionSchedule(newReception, scheduel));

            _receptionRepository.Add(newReception);
        }

        /// <summary>
        ///     更新租赁飞机接收项目。
        /// </summary>
        /// <param name="dto">租赁飞机接收项目DTO。</param>
        [Update(typeof (AircraftLeaseReceptionDTO))]
        public void ModifyAircraftLeaseReception(AircraftLeaseReceptionDTO dto)
        {
            //获取供应商
            Supplier supplier = _supplierRepository.Get(dto.SupplierId);

            //获取需要更新的对象
            var updateReception = _receptionRepository.Get(dto.AircraftLeaseReceptionId) as AircraftLeaseReception;

            if (updateReception != null)
            {
                //更新主表：
                updateReception.SetReceptionNumber(dto.ReceptionNumber);
                updateReception.Description = dto.Description;
                updateReception.StartDate = dto.StartDate;
                updateReception.EndDate = dto.EndDate;
                updateReception.SetSupplier(supplier);
                updateReception.SetStatus((ReceptionStatus) dto.Status);
                updateReception.SourceId = dto.SourceId;

                //更新接机行：
                List<AircraftLeaseReceptionLineDTO> dtoReceptionLines = dto.ReceptionLines;
                ICollection<ReceptionLine> receptionLines = updateReception.ReceptionLines;
                DataHelper.DetailHandle(dtoReceptionLines.ToArray(),
                    receptionLines.OfType<AircraftLeaseReceptionLine>().ToArray(),
                    c => c.AircraftLeaseReceptionLineId, p => p.Id,
                    i => InsertReceptionLine(updateReception, i),
                    UpdateReceptionLine,
                    d => _receptionRepository.RemoveReceptionLine(d));
                //更新交付日程：
                List<ReceptionScheduleDTO> dtoReceptionSchedules = dto.ReceptionSchedules;
                ICollection<ReceptionSchedule> receptionSchedules = updateReception.ReceptionSchedules;
                DataHelper.DetailHandle(dtoReceptionSchedules.ToArray(),
                    receptionSchedules.ToArray(),
                    c => c.ReceptionScheduleId, p => p.Id,
                    i => InsertReceptionSchedule(updateReception, i),
                    UpdateReceptionSchedule,
                    d => _receptionRepository.RemoveReceptionSchedule(d));
            }
            _receptionRepository.Modify(updateReception);
        }

        /// <summary>
        ///     删除租赁飞机接收项目。
        /// </summary>
        /// <param name="dto">租赁飞机接收项目DTO。</param>
        [Delete(typeof (AircraftLeaseReceptionDTO))]
        public void DeleteAircraftLeaseReception(AircraftLeaseReceptionDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            Reception delAircraftLeaseReception = _receptionRepository.Get(dto.AircraftLeaseReceptionId);
            //获取需要删除的对象。
            if (delAircraftLeaseReception != null)
            {
                _receptionRepository.DeleteReception(delAircraftLeaseReception); //删除租赁飞机接收项目。
            }
        }

        #region 处理接机行

        /// <summary>
        ///     插入新接机行
        /// </summary>
        /// <param name="reception">接机项目</param>
        /// <param name="line">接机行DTO</param>
        private void InsertReceptionLine(AircraftLeaseReception reception, AircraftLeaseReceptionLineDTO line)
        {
            //获取合同飞机
            LeaseContractAircraft leaseConAc =
                _contractAircraftRepository.GetFiltered(p => p.Id == line.ContractAircraftId)
                    .OfType<LeaseContractAircraft>().FirstOrDefault();

            // 添加接机行
            AircraftLeaseReceptionLine newRecepitonLine =
                reception.AddNewAircraftLeaseReceptionLine(line.ReceivedAmount);
            newRecepitonLine.AcceptedAmount = line.AcceptedAmount;
            newRecepitonLine.SetCompleted();
            newRecepitonLine.DeliverDate = line.DeliverDate;
            newRecepitonLine.DeliverPlace = line.DeliverPlace;
            newRecepitonLine.DailNumber = line.DailNumber;
            newRecepitonLine.FlightNumber = line.FlightNumber;
            newRecepitonLine.SetContractAircraft(leaseConAc);
            newRecepitonLine.Note = line.Note;
        }

        /// <summary>
        ///     更新接机行
        /// </summary>
        /// <param name="line">接机行DTO</param>
        /// <param name="receptionLine">接机行</param>
        private void UpdateReceptionLine(AircraftLeaseReceptionLineDTO line, AircraftLeaseReceptionLine receptionLine)
        {
            //获取合同飞机
            LeaseContractAircraft leaseConAc =
                _contractAircraftRepository.GetFiltered(p => p.Id == line.ContractAircraftId)
                    .OfType<LeaseContractAircraft>().FirstOrDefault();


            // 更新订单行
            receptionLine.ReceivedAmount = line.ReceivedAmount;
            receptionLine.AcceptedAmount = line.AcceptedAmount;
            receptionLine.SetCompleted();
            receptionLine.DeliverDate = line.DeliverDate;
            receptionLine.DeliverPlace = line.DeliverPlace;
            receptionLine.DailNumber = line.DailNumber;
            receptionLine.FlightNumber = line.FlightNumber;
            receptionLine.SetContractAircraft(leaseConAc);
            receptionLine.Note = line.Note;
        }

        #endregion

        #region 处理接机日程

        /// <summary>
        ///     插入新接机日程
        /// </summary>
        /// <param name="reception">接机项目</param>
        /// <param name="schedule">接机行DTO</param>
        private void InsertReceptionSchedule(AircraftLeaseReception reception, ReceptionScheduleDTO schedule)
        {
            // 添加接机行
            var newSchedule = new ReceptionSchedule();
            newSchedule.SetSchedule(schedule.Subject, schedule.Body, schedule.Importance, schedule.Tempo, schedule.Start,
                schedule.End, schedule.IsAllDayEvent);
            newSchedule.Group = schedule.Group;
            reception.ReceptionSchedules.Add(newSchedule);
        }

        /// <summary>
        ///     更新接机日程
        /// </summary>
        /// <param name="schedule">接机日程DTO</param>
        /// <param name="receptionSchedule">接机日程</param>
        private void UpdateReceptionSchedule(ReceptionScheduleDTO schedule, ReceptionSchedule receptionSchedule)
        {
            // 更新订单行
            receptionSchedule.SetSchedule(schedule.Subject, schedule.Body, schedule.Importance, schedule.Tempo,
                schedule.Start,
                schedule.End, schedule.IsAllDayEvent);
            receptionSchedule.Group = schedule.Group;
        }

        #endregion

        #endregion
    }
}