#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/03，10:04
// 文件名：SnInstallHistoryAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.SnInstallHistoryQueries;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnInstallHistoryAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Application.PartBC.SnInstallHistoryServices
{
    /// <summary>
    ///     实现序号件装机历史服务接口。
    ///     用于处理序号件装机历史相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class SnInstallHistoryAppService : ISnInstallHistoryAppService
    {
        private readonly ISnInstallHistoryQuery _snInstallHistoryQuery;
        private readonly ISnInstallHistoryRepository _snInstallHistoryRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IPnRegRepository _pnRegRepository;
        private readonly ISnRegRepository _snRegRepository;
        public SnInstallHistoryAppService(ISnInstallHistoryQuery snInstallHistoryQuery,
            ISnInstallHistoryRepository snInstallHistoryRepository,
            IAircraftRepository aircraftRepository,
            IPnRegRepository pnRegRepository,
            ISnRegRepository snRegRepository)
        {
            _aircraftRepository = aircraftRepository;
            _pnRegRepository = pnRegRepository;
            _snInstallHistoryQuery = snInstallHistoryQuery;
            _snInstallHistoryRepository = snInstallHistoryRepository;
            _snRegRepository = snRegRepository;

        }

        #region SnInstallHistoryDTO

        /// <summary>
        ///     获取所有typeName
        /// </summary>
        /// <returns></returns>
        public IQueryable<SnInstallHistoryDTO> GetSnInstallHistories()
        {
            var queryBuilder =
                new QueryBuilder<SnInstallHistory>();
            return _snInstallHistoryQuery.SnInstallHistoryDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增SnInstallHistory。
        /// </summary>
        /// <param name="dto">SnInstallHistoryDTO。</param>
        [Insert(typeof(SnInstallHistoryDTO))]
        public void InsertSnInstallHistory(SnInstallHistoryDTO dto)
        {
            var snReg = _snRegRepository.Get(dto.SnRegId);
            var pnReg = _pnRegRepository.Get(dto.PnRegId);
            var aircraft = _aircraftRepository.Get(dto.AircraftId);

            var newSnInstallHistory = SnInstallHistoryFactory.CreateSnInstallHistory(snReg, pnReg,
                dto.CSN, dto.CSR, dto.TSN, dto.TSR, aircraft, dto.InstallDate, dto.RemoveDate);

            _snInstallHistoryRepository.Add(newSnInstallHistory);
        }

        /// <summary>
        ///  更新SnInstallHistory。
        /// </summary>
        /// <param name="dto">SnInstallHistoryDTO。</param>
        [Update(typeof(SnInstallHistoryDTO))]
        public void ModifySnInstallHistory(SnInstallHistoryDTO dto)
        {
            var snReg = _snRegRepository.Get(dto.SnRegId);
            var pnReg = _pnRegRepository.Get(dto.PnRegId);
            var aircraft = _aircraftRepository.Get(dto.AircraftId);

            var dbSnInstallHistory = _snInstallHistoryRepository.Get(dto.Id); //获取需要更新的对象。

            if (dbSnInstallHistory != null)
            {
                //更新。
                var updateSnInstallHistory = SnInstallHistoryFactory.CreateSnInstallHistory(snReg, pnReg,
                    dto.CSN, dto.CSR, dto.TSN, dto.TSR, aircraft, dto.InstallDate, dto.RemoveDate);
                updateSnInstallHistory.ChangeCurrentIdentity(dbSnInstallHistory.Id);
                _snInstallHistoryRepository.Modify(updateSnInstallHistory);
            }
        }

        /// <summary>
        ///  删除SnInstallHistory。
        /// </summary>
        /// <param name="dto">SnInstallHistoryDTO。</param>
        [Delete(typeof(SnInstallHistoryDTO))]
        public void DeleteSnInstallHistory(SnInstallHistoryDTO dto)
        {
            var delSnInstallHistory = _snInstallHistoryRepository.Get(dto.Id); //获取需要删除的对象。
            _snInstallHistoryRepository.Remove(delSnInstallHistory); //删除SnInstallHistory。
        }
        #endregion
    }
}