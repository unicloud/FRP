#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，10:04
// 文件名：SnHistoryAppService.cs
// 程序集：UniCloud.Application.PartBC
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
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.SnHistoryQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;

#endregion

namespace UniCloud.Application.PartBC.SnHistoryServices
{
    /// <summary>
    ///     实现序号件装机历史服务接口。
    ///     用于处理序号件装机历史相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class SnHistoryAppService : ContextBoundObject, ISnHistoryAppService
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IPnRegRepository _pnRegRepository;
        private readonly ISnHistoryQuery _snHistoryQuery;
        private readonly ISnHistoryRepository _snHistoryRepository;
        private readonly ISnRegRepository _snRegRepository;
        private readonly ISnRemInstRecordRepository _snRemInstRecordRepository;

        public SnHistoryAppService(ISnHistoryQuery snHistoryQuery,
            ISnHistoryRepository snHistoryRepository,
            IAircraftRepository aircraftRepository,
            IPnRegRepository pnRegRepository,
            ISnRegRepository snRegRepository,
            ISnRemInstRecordRepository snRemInstRecordRepository)
        {
            _aircraftRepository = aircraftRepository;
            _pnRegRepository = pnRegRepository;
            _snHistoryQuery = snHistoryQuery;
            _snHistoryRepository = snHistoryRepository;
            _snRegRepository = snRegRepository;
            _snRemInstRecordRepository = snRemInstRecordRepository;
        }

        #region SnHistoryDTO

        /// <summary>
        ///     获取所有typeName
        /// </summary>
        /// <returns></returns>
        public IQueryable<SnHistoryDTO> GetSnHistories()
        {
            var queryBuilder =
                new QueryBuilder<SnHistory>();
            return _snHistoryQuery.SnHistoryDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增SnHistory。
        /// </summary>
        /// <param name="dto">SnHistoryDTO。</param>
        [Insert(typeof (SnHistoryDTO))]
        public void InsertSnHistory(SnHistoryDTO dto)
        {
            SnReg snReg = _snRegRepository.Get(dto.SnRegId);
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);
            Aircraft aircraft = _aircraftRepository.Get(dto.AircraftId);
            SnRemInstRecord remInstRecord = _snRemInstRecordRepository.Get(dto.RemInstRecordId);

            SnHistory newSnHistory = SnHistoryFactory.CreateSnHistory(snReg, pnReg,
                dto.CSN, dto.TSN, dto.CSN2, dto.TSN2, dto.ActionType, aircraft, dto.ActionDate, remInstRecord,
                dto.Status,dto.Position);

            _snHistoryRepository.Add(newSnHistory);
        }

        /// <summary>
        ///     更新SnHistory。
        /// </summary>
        /// <param name="dto">SnHistoryDTO。</param>
        [Update(typeof (SnHistoryDTO))]
        public void ModifySnHistory(SnHistoryDTO dto)
        {
            SnReg snReg = _snRegRepository.Get(dto.SnRegId);
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);
            Aircraft aircraft = _aircraftRepository.Get(dto.AircraftId);
            SnRemInstRecord remInstRecord = _snRemInstRecordRepository.Get(dto.RemInstRecordId);

            SnHistory updateSnHistory = _snHistoryRepository.Get(dto.Id); //获取需要更新的对象。

            if (updateSnHistory != null)
            {
                //更新。
                updateSnHistory.SetAircraft(aircraft);
                updateSnHistory.SetActionDate(dto.ActionDate);
                updateSnHistory.SetActionType((ActionType) dto.ActionType);
                updateSnHistory.SetSn(snReg);
                updateSnHistory.SetPn(pnReg);
                updateSnHistory.SetCSN(dto.CSN);
                updateSnHistory.SetTSN(dto.TSN);
                updateSnHistory.SetRemInstRecord(remInstRecord);
                updateSnHistory.SetPosition((Position)dto.Position);
                _snHistoryRepository.Modify(updateSnHistory);
            }
        }

        /// <summary>
        ///     删除SnHistory。
        /// </summary>
        /// <param name="dto">SnHistoryDTO。</param>
        [Delete(typeof (SnHistoryDTO))]
        public void DeleteSnHistory(SnHistoryDTO dto)
        {
            SnHistory delSnHistory = _snHistoryRepository.Get(dto.Id); //获取需要删除的对象。
            _snHistoryRepository.Remove(delSnHistory); //删除SnHistory。
        }

        #endregion
    }
}