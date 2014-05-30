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
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.SnRegQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SnRegAgg;

#endregion

namespace UniCloud.Application.PartBC.SnRegServices
{
    /// <summary>
    ///     实现SnReg的服务接口。
    ///     用于处理SnReg相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class SnRegAppService : ContextBoundObject, ISnRegAppService
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IPnRegRepository _pnRegRepository;
        private readonly ISnRegQuery _snRegQuery;
        private readonly ISnRegRepository _snRegRepository;

        public SnRegAppService(ISnRegQuery snRegQuery, ISnRegRepository snRegRepository,
            IAircraftRepository aircraftRepository, IPnRegRepository pnRegRepository)
        {
            _snRegQuery = snRegQuery;
            _snRegRepository = snRegRepository;
            _aircraftRepository = aircraftRepository;
            _pnRegRepository = pnRegRepository;
        }

        #region SnRegDTO

        /// <summary>
        ///     获取所有SnReg。
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
        ///     新增SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Insert(typeof(SnRegDTO))]
        public void InsertSnReg(SnRegDTO dto)
        {
            Aircraft aircraft = _aircraftRepository.Get(dto.AircraftId); //获取运营飞机
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId); //获取附件

            //创建序号件
            SnReg newSnReg = SnRegFactory.CreateSnReg(dto.InstallDate, pnReg, dto.Sn);
            newSnReg.SetAircraft(aircraft);
            newSnReg.SetIsLife(dto.IsLife, dto.IsLifeCst, dto.Rate);
            newSnReg.SetSnStatus((SnStatus)dto.Status);
            //添加到寿监控
            dto.LiftMonitors.ToList().ForEach(lifeMonitor => InsertLifeMonitor(newSnReg, lifeMonitor));

            _snRegRepository.Add(newSnReg);
        }

        /// <summary>
        ///     更新SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Update(typeof(SnRegDTO))]
        public void ModifySnReg(SnRegDTO dto)
        {
            Aircraft aircraft = _aircraftRepository.Get(dto.AircraftId); //获取运营飞机
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId); //获取附件

            //获取需要更新的对象
            SnReg updateSnReg = _snRegRepository.Get(dto.Id);

            if (updateSnReg != null)
            {
                //更新主表：
                SnRegFactory.UpdateSnReg(updateSnReg, dto.InstallDate, pnReg, dto.Sn);
                updateSnReg.SetAircraft(aircraft);
                updateSnReg.SetIsLife(dto.IsLife, dto.IsLifeCst, dto.Rate);
                updateSnReg.SetSnStatus((SnStatus)dto.Status);

                //更新到寿监控集合：
                List<LifeMonitorDTO> dtoLiftMonitors = dto.LiftMonitors;
                ICollection<LifeMonitor> liftMonitors = updateSnReg.LifeMonitors;
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
        ///     删除SnReg。
        /// </summary>
        /// <param name="dto">SnRegDTO。</param>
        [Delete(typeof(SnRegDTO))]
        public void DeleteSnReg(SnRegDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            SnReg delSnReg = _snRegRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delSnReg != null)
            {
                _snRegRepository.DeleteSnReg(delSnReg); //删除序号件。
            }
        }

        #region 处理到寿监控

        /// <summary>
        ///     插入到寿监控
        /// </summary>
        /// <param name="snReg">序号件</param>
        /// <param name="lifeMonitorDto">到寿监控DTO</param>
        private void InsertLifeMonitor(SnReg snReg, LifeMonitorDTO lifeMonitorDto)
        {
            // 添加到寿监控
            snReg.AddNewLifeMonitor(lifeMonitorDto.WorkDescription, lifeMonitorDto.MointorStart, lifeMonitorDto.MointorEnd);
        }

        /// <summary>
        ///     更新到寿监控
        /// </summary>
        /// <param name="lifeMonitorDto">到寿监控DTO</param>
        /// <param name="lifeMonitor">到寿监控</param>
        private void UpdateLifeMonitor(LifeMonitorDTO lifeMonitorDto, LifeMonitor lifeMonitor)
        {
            lifeMonitor.SetWorkDescription(lifeMonitorDto.WorkDescription);
            lifeMonitor.SetMointorPeriod(lifeMonitorDto.MointorStart, lifeMonitorDto.MointorEnd);
        }

        #endregion

        #endregion
    }
}