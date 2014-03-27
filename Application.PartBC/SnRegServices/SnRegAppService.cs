#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：SnRegAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.SnRegQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;
#endregion

namespace UniCloud.Application.PartBC.SnRegServices
{
    /// <summary>
    /// 实现SnReg的服务接口。
    ///  用于处理SnReg相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class SnRegAppService : ISnRegAppService
    {
        private readonly ISnRegQuery _snRegQuery;
        private readonly ISnRegRepository _snRegRepository;
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IPnRegRepository _pnRegRepository;
        private readonly IMaintainWorkRepository _maintainWorkRepository;
        public SnRegAppService(ISnRegQuery snRegQuery, ISnRegRepository snRegRepository,
            IAircraftRepository aircraftRepository, IPnRegRepository pnRegRepository,
            IMaintainWorkRepository maintainWorkRepository)
        {
            _snRegQuery = snRegQuery;
            _snRegRepository = snRegRepository;
            _aircraftRepository = aircraftRepository;
            _pnRegRepository = pnRegRepository;
            _maintainWorkRepository = maintainWorkRepository;
        }

        #region SnRegDTO

        /// <summary>
        /// 获取所有SnReg。
        /// </summary>
        public IQueryable<SnRegDTO> GetSnRegs()
        {
            var queryBuilder =
               new QueryBuilder<SnReg>();
            return _snRegQuery.SnRegDTOQuery(queryBuilder);
        }

        public IQueryable<ApuEngineSnRegDTO> GetApuEngineSnRegs()
        {
            var queryBuilder =
             new QueryBuilder<SnReg>();
            return _snRegQuery.ApuEngineSnRegDTOQuery(queryBuilder);
        }
 
            /// <summary>
        ///  新增SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Insert(typeof(SnRegDTO))]
        public void InsertSnReg(SnRegDTO dto)
        {
            var aircraft = _aircraftRepository.Get(dto.AircraftId);//获取运营飞机
            var pnReg = _pnRegRepository.Get(dto.PnRegId);//获取附件

            //创建序号件
            var newSnReg = SnRegFactory.CreateSnReg(dto.InstallDate, pnReg, dto.Sn, dto.TSN, dto.TSR, dto.CSN, dto.CSR);
            newSnReg.SetAircraft(aircraft);
            newSnReg.SetIsStop(dto.IsStop);
            newSnReg.SetSnStatus((SnStatus)dto.Status);

            //添加装机历史
            dto.SnHistories.ToList().ForEach(snHistory => InsertSnHistory(newSnReg, snHistory));

            //添加到寿监控
            dto.LiftMonitors.ToList().ForEach(lifeMonitor => InsertLifeMonitor(newSnReg, lifeMonitor));

            _snRegRepository.Add(newSnReg);
        }

        /// <summary>
        ///  更新SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Update(typeof(SnRegDTO))]
        public void ModifySnReg(SnRegDTO dto)
        {
            var aircraft = _aircraftRepository.Get(dto.AircraftId);//获取运营飞机
            var pnReg = _pnRegRepository.Get(dto.PnRegId);//获取附件

            //获取需要更新的对象
            var updateSnReg = _snRegRepository.Get(dto.Id);

            if (updateSnReg != null)
            {
                //更新主表：
                SnRegFactory.UpdateSnReg(updateSnReg,dto.InstallDate, pnReg, dto.Sn, dto.TSN, dto.TSR, dto.CSN, dto.CSR);
                updateSnReg.SetAircraft(aircraft);
                updateSnReg.SetIsStop(dto.IsStop);
                updateSnReg.SetSnStatus((SnStatus)dto.Status);


                //更新装机历史集合：
                var dtoSnHistories = dto.SnHistories;
                var snHistories = updateSnReg.SnHistories;
                DataHelper.DetailHandle(dtoSnHistories.ToArray(),
                    snHistories.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertSnHistory(updateSnReg, i),
                    UpdateSnHistory,
                    d => _snRegRepository.RemoveSnHistory(d));

                //更新到寿监控集合：
                var dtoLiftMonitors = dto.LiftMonitors;
                var liftMonitors = updateSnReg.LifeMonitors;
                DataHelper.DetailHandle(dtoLiftMonitors.ToArray(),
                    liftMonitors.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertLifeMonitor(updateSnReg, i),
                    UpdateLifeMonitor,
                    d => _snRegRepository.RemoveLifeMonitor(d));
            }
            _snRegRepository.Modify(updateSnReg);
        }

        /// <summary>
        ///  删除SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Delete(typeof(SnRegDTO))]
        public void DeleteSnReg(SnRegDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delSnReg = _snRegRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delSnReg != null)
            {
                _snRegRepository.DeleteSnReg(delSnReg); //删除序号件。
            }
        }

        /// <summary>
        ///  更新Apu、Engine的SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Update(typeof(ApuEngineSnRegDTO))]
        public void ModifyApuEngineSnReg(ApuEngineSnRegDTO dto)
        {
            //获取需要更新的对象
            var updateSnReg = _snRegRepository.Get(dto.Id);

            if (updateSnReg != null)
            {

                //更新装机历史集合：
                var dtoSnHistories = dto.SnHistories;
                var snHistories = updateSnReg.SnHistories;
                DataHelper.DetailHandle(dtoSnHistories.ToArray(),
                    snHistories.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertSnHistory(updateSnReg, i),
                    UpdateSnHistory,
                    d => _snRegRepository.RemoveSnHistory(d));
                _snRegRepository.Modify(updateSnReg);
            }
        }

        #region 处理装机历史

        /// <summary>
        ///     插入装机历史
        /// </summary>
        /// <param name="snReg">序号件</param>
        /// <param name="snHistoryDto">装机历史DTO</param>
        private void InsertSnHistory(SnReg snReg, SnHistoryDTO snHistoryDto)
        {
            //获取
            var aircraft = _aircraftRepository.Get(snHistoryDto.AircraftId);//获取运营飞机

            // 添加装机历史
            var newSnHistory = snReg.AddNewSnHistory();
            newSnHistory.SetAircraft(aircraft);
            newSnHistory.SetCSN(snHistoryDto.CSN);
            newSnHistory.SetCSR(snHistoryDto.CSR);
            newSnHistory.SetFiNumber(snHistoryDto.FiNumber);
            newSnHistory.SetInstallDate(snHistoryDto.InstallDate);
            newSnHistory.SetRemoveDate(snHistoryDto.RemoveDate);
            newSnHistory.SetTSN(snHistoryDto.TSN);
            newSnHistory.SetTSR(snHistoryDto.TSR);
        }

        /// <summary>
        ///     更新装机历史
        /// </summary>
        /// <param name="snHistoryDto">装机历史DTO</param>
        /// <param name="snHistory">装机历史</param>
        private void UpdateSnHistory(SnHistoryDTO snHistoryDto, SnHistory snHistory)
        {
            //获取
            var aircraft = _aircraftRepository.Get(snHistoryDto.AircraftId);//获取运营飞机

            // 添加装机历史
            snHistory.SetAircraft(aircraft);
            snHistory.SetCSN(snHistoryDto.CSN);
            snHistory.SetCSR(snHistoryDto.CSR);
            snHistory.SetFiNumber(snHistoryDto.FiNumber);
            snHistory.SetInstallDate(snHistoryDto.InstallDate);
            snHistory.SetRemoveDate(snHistoryDto.RemoveDate);
            snHistory.SetTSN(snHistoryDto.TSN);
            snHistory.SetTSR(snHistoryDto.TSR);
            snHistory.SetSn(snHistoryDto.Sn);

        }

        #endregion

        #region 处理到寿监控

        /// <summary>
        ///     插入到寿监控
        /// </summary>
        /// <param name="snReg">序号件</param>
        /// <param name="lifeMonitorDto">到寿监控DTO</param>
        private void InsertLifeMonitor(SnReg snReg, LifeMonitorDTO lifeMonitorDto)
        {
            //获取
            var maintainWork = _maintainWorkRepository.Get(lifeMonitorDto.MaintainWorkId);

            // 添加到寿监控
            snReg.AddNewLifeMonitor(maintainWork, lifeMonitorDto.MointorStart, lifeMonitorDto.MointorEnd);
        }

        /// <summary>
        ///     更新到寿监控
        /// </summary>
        /// <param name="lifeMonitorDto">到寿监控DTO</param>
        /// <param name="lifeMonitor">到寿监控</param>
        private void UpdateLifeMonitor(LifeMonitorDTO lifeMonitorDto, LifeMonitor lifeMonitor)
        {
            //获取
            var maintainWork = _maintainWorkRepository.Get(lifeMonitorDto.MaintainWorkId);

            lifeMonitor.SetMaintainWork(maintainWork);
            lifeMonitor.SetMointorPeriod(lifeMonitorDto.MointorStart, lifeMonitorDto.MointorEnd);

        }

        #endregion
        #endregion

    }
}
