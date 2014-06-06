#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/15，23:04
// 文件名：SnRemInstRecordAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.SnRemInstRecordQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRemInstRecordAgg;

#endregion

namespace UniCloud.Application.PartBC.SnRemInstRecordServices
{
    /// <summary>
    ///     实现拆换记录服务接口。
    ///     用于处理拆换记录相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class SnRemInstRecordAppService : ISnRemInstRecordAppService
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly ISnRemInstRecordQuery _snRemInstRecordQuery;
        private readonly ISnRemInstRecordRepository _snRemInstRecordRepository;

        public SnRemInstRecordAppService(ISnRemInstRecordQuery snRemInstRecordQuery,
            ISnRemInstRecordRepository snRemInstRecordRepository,
            IAircraftRepository aircraftRepository)
        {
            _snRemInstRecordQuery = snRemInstRecordQuery;
            _snRemInstRecordRepository = snRemInstRecordRepository;
            _aircraftRepository = aircraftRepository;
        }

        #region SnRemInstRecordDTO

        /// <summary>
        ///     获取所有拆换记录
        /// </summary>
        /// <returns></returns>
        public IQueryable<SnRemInstRecordDTO> GetSnRemInstRecords()
        {
            var queryBuilder =
                new QueryBuilder<SnRemInstRecord>();
            return _snRemInstRecordQuery.SnRemInstRecordDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增SnRemInstRecord。
        /// </summary>
        /// <param name="dto">SnRemInstRecordDTO。</param>
        [Insert(typeof (SnRemInstRecordDTO))]
        public void InsertSnRemInstRecord(SnRemInstRecordDTO dto)
        {
            Aircraft aircraft = _aircraftRepository.Get(dto.AircraftId);

            SnRemInstRecord newSnRemInstRecord = SnRemInstRecordFactory.CreateSnRemInstRecord(dto.ActionNo,
                dto.ActionDate, dto.ActionType,
                dto.Position, dto.Reason, aircraft);
            newSnRemInstRecord.ChangeCurrentIdentity(dto.Id);

            _snRemInstRecordRepository.Add(newSnRemInstRecord);
        }

        /// <summary>
        ///     更新SnRemInstRecord。
        /// </summary>
        /// <param name="dto">SnRemInstRecordDTO。</param>
        [Update(typeof (SnRemInstRecordDTO))]
        public void ModifySnRemInstRecord(SnRemInstRecordDTO dto)
        {
            Aircraft aircraft = _aircraftRepository.Get(dto.AircraftId);

            SnRemInstRecord updateSnRemInstRecord = _snRemInstRecordRepository.Get(dto.Id); //获取需要更新的对象。

            if (updateSnRemInstRecord != null)
            {
                //更新。
                updateSnRemInstRecord.SetActionNo(dto.ActionNo);
                updateSnRemInstRecord.SetActionDate(dto.ActionDate);
                updateSnRemInstRecord.SetActionType((ActionType) dto.ActionType);
                updateSnRemInstRecord.SetPosition(dto.Position);
                updateSnRemInstRecord.SetReason(dto.Reason);
                updateSnRemInstRecord.SetAircraft(aircraft);
                _snRemInstRecordRepository.Modify(updateSnRemInstRecord);
            }
        }

        /// <summary>
        ///     删除SnRemInstRecord。
        /// </summary>
        /// <param name="dto">SnRemInstRecordDTO。</param>
        [Delete(typeof (SnRemInstRecordDTO))]
        public void DeleteSnRemInstRecord(SnRemInstRecordDTO dto)
        {
            SnRemInstRecord delSnRemInstRecord = _snRemInstRecordRepository.Get(dto.Id); //获取需要删除的对象。
            _snRemInstRecordRepository.Remove(delSnRemInstRecord); //删除SnRemInstRecord。
        }

        #endregion
    }
}