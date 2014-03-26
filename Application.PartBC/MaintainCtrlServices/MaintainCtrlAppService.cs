#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:47

// 文件名：MaintainCtrlAppService
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
using UniCloud.Application.PartBC.Query.MaintainCtrlQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.BasicConfigAgg;
using UniCloud.Domain.PartBC.Aggregates.CtrlUnitAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;
using UniCloud.Domain.PartBC.Aggregates.SpecialConfigAgg;

#endregion

namespace UniCloud.Application.PartBC.MaintainCtrlServices
{
    /// <summary>
    ///     实现MaintainCtrl的服务接口。
    ///     用于处理MaintainCtrl相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class MaintainCtrlAppService : ContextBoundObject, IMaintainCtrlAppService
    {
        private readonly IBasicConfigRepository _basicConfigRepository;
        private readonly ICtrlUnitRepository _ctrlUnitRepository;
        private readonly IMaintainCtrlQuery _maintainCtrlQuery;
        private readonly IMaintainCtrlRepository _maintainCtrlRepository;
        private readonly IMaintainWorkRepository _maintainWorkRepository;
        private readonly IPnRegRepository _pnRegRepository;
        private readonly ISpecialConfigRepository _specialConfigRepository;

        public MaintainCtrlAppService(IMaintainCtrlQuery maintainCtrlQuery,
            IMaintainCtrlRepository maintainCtrlRepository,
            ICtrlUnitRepository ctrlUnitRepository,
            IMaintainWorkRepository maintainWorkRepository,
            IPnRegRepository pnRegRepository,
            IBasicConfigRepository basicConfigRepository,
            ISpecialConfigRepository specialConfigRepository)
        {
            _maintainCtrlQuery = maintainCtrlQuery;
            _maintainCtrlRepository = maintainCtrlRepository;
            _ctrlUnitRepository = ctrlUnitRepository;
            _maintainWorkRepository = maintainWorkRepository;
            _pnRegRepository = pnRegRepository;
            _basicConfigRepository = basicConfigRepository;
            _specialConfigRepository = specialConfigRepository;
        }

        #region ItemMaintainCtrlDTO

        /// <summary>
        ///     获取所有ItemMaintainCtrl。
        /// </summary>
        public IQueryable<ItemMaintainCtrlDTO> GetItemMaintainCtrls()
        {
            var queryBuilder =
                new QueryBuilder<ItemMaintainCtrl>();
            return _maintainCtrlQuery.ItemMaintainCtrlDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增ItemMaintainCtrl。
        /// </summary>
        /// <param name="dto">ItemMaintainCtrlDTO。</param>
        [Insert(typeof (ItemMaintainCtrlDTO))]
        public void InsertItemMaintainCtrl(ItemMaintainCtrlDTO dto)
        {
            BasicConfig basicConfig = _basicConfigRepository.Get(dto.Id);
            SpecialConfig specialConfig = _specialConfigRepository.Get(dto.Id);

            if (basicConfig != null && specialConfig == null)
            {
                //创建项维修控制组
                ItemMaintainCtrl newItemMainCtrl = MaintainCtrlFactory.CreateItemMaintainCtrl(basicConfig,
                    ((ControlStrategy) dto.CtrlStrategy));
                //添加维修控制明细
                dto.MaintainCtrlLines.ToList().ForEach(line => InsertMainCtrlLine(newItemMainCtrl, line));

                _maintainCtrlRepository.Add(newItemMainCtrl);
            }
            else if (basicConfig == null && specialConfig != null)
            {
                //创建项维修控制组
                ItemMaintainCtrl newItemMainCtrl = MaintainCtrlFactory.CreateItemMaintainCtrl(specialConfig,
                    ((ControlStrategy) dto.CtrlStrategy));
                //添加维修控制明细
                dto.MaintainCtrlLines.ToList().ForEach(line => InsertMainCtrlLine(newItemMainCtrl, line));

                _maintainCtrlRepository.Add(newItemMainCtrl);
            }
        }

        /// <summary>
        ///     更新ItemMaintainCtrl。
        /// </summary>
        /// <param name="dto">ItemMaintainCtrlDTO。</param>
        [Update(typeof (ItemMaintainCtrlDTO))]
        public void ModifyItemMaintainCtrl(ItemMaintainCtrlDTO dto)
        {
            //获取需要更新的对象
            var updateItemMainCtrl = _maintainCtrlRepository.Get(dto.Id) as ItemMaintainCtrl;

            if (updateItemMainCtrl != null)
            {
                BasicConfig basicConfig = _basicConfigRepository.Get(dto.Id);
                SpecialConfig specialConfig = _specialConfigRepository.Get(dto.Id);

                //更新主表：
                updateItemMainCtrl.SetCtrlStrategy((ControlStrategy) dto.CtrlStrategy);
                if (basicConfig != null && specialConfig == null)
                {
                    updateItemMainCtrl.SetAcConfig(basicConfig);
                }
                else if (basicConfig == null && specialConfig != null)
                {
                    updateItemMainCtrl.SetAcConfig(specialConfig);
                }

                //更新维修控制明细：
                List<MaintainCtrlLineDTO> dtoMaintainCtrlLines = dto.MaintainCtrlLines;
                ICollection<MaintainCtrlLine> maintainCtrlLines = updateItemMainCtrl.MaintainCtrlLines;
                DataHelper.DetailHandle(dtoMaintainCtrlLines.ToArray(),
                    maintainCtrlLines.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertMainCtrlLine(updateItemMainCtrl, i),
                    UpdateMainCtrlLine,
                    d => _maintainCtrlRepository.RemoveMaintainCtrlLine(d));
            }
            _maintainCtrlRepository.Modify(updateItemMainCtrl);
        }

        /// <summary>
        ///     删除ItemMaintainCtrl。
        /// </summary>
        /// <param name="dto">ItemMaintainCtrlDTO。</param>
        [Delete(typeof (ItemMaintainCtrlDTO))]
        public void DeleteItemMaintainCtrl(ItemMaintainCtrlDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delItemMainCtrl = _maintainCtrlRepository.Get(dto.Id) as ItemMaintainCtrl;
            //获取需要删除的对象。

            if (delItemMainCtrl != null)
            {
                _maintainCtrlRepository.DeleteItemMaintainCtrl(delItemMainCtrl); //删除项维修控制组。
            }
        }

        #endregion

        #region PnMaintainCtrlDTO

        /// <summary>
        ///     获取所有PnMaintainCtrl。
        /// </summary>
        public IQueryable<PnMaintainCtrlDTO> GetPnMaintainCtrls()
        {
            var queryBuilder =
                new QueryBuilder<PnMaintainCtrl>();
            return _maintainCtrlQuery.PnMaintainCtrlDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增PnMaintainCtrl。
        /// </summary>
        /// <param name="dto">PnMaintainCtrlDTO。</param>
        [Insert(typeof (PnMaintainCtrlDTO))]
        public void InsertPnMaintainCtrl(PnMaintainCtrlDTO dto)
        {
            //获取附件
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);

            //创建附件维修控制组
            PnMaintainCtrl newPnMainCtrl = MaintainCtrlFactory.CreatePnMaintainCtrl(pnReg,(ControlStrategy) dto.CtrlStrategy);

            //添加维修控制明细
            dto.MaintainCtrlLines.ToList().ForEach(line => InsertMainCtrlLine(newPnMainCtrl, line));

            _maintainCtrlRepository.Add(newPnMainCtrl);
        }

        /// <summary>
        ///     更新PnMaintainCtrl。
        /// </summary>
        /// <param name="dto">PnMaintainCtrlDTO。</param>
        [Update(typeof (PnMaintainCtrlDTO))]
        public void ModifyPnMaintainCtrl(PnMaintainCtrlDTO dto)
        {
            //获取附件
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);

            //获取需要更新的对象
            var updatePnMainCtrl = _maintainCtrlRepository.Get(dto.Id) as PnMaintainCtrl;

            if (updatePnMainCtrl != null)
            {
                //更新主表：
                updatePnMainCtrl.SetCtrlStrategy((ControlStrategy) dto.CtrlStrategy);
                updatePnMainCtrl.SetPnReg(pnReg);

                //更新维修控制明细：
                List<MaintainCtrlLineDTO> dtoMaintainCtrlLines = dto.MaintainCtrlLines;
                ICollection<MaintainCtrlLine> maintainCtrlLines = updatePnMainCtrl.MaintainCtrlLines;
                DataHelper.DetailHandle(dtoMaintainCtrlLines.ToArray(),
                    maintainCtrlLines.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertMainCtrlLine(updatePnMainCtrl, i),
                    UpdateMainCtrlLine,
                    d => _maintainCtrlRepository.RemoveMaintainCtrlLine(d));
            }
            _maintainCtrlRepository.Modify(updatePnMainCtrl);
        }

        /// <summary>
        ///     删除PnMaintainCtrl。
        /// </summary>
        /// <param name="dto">PnMaintainCtrlDTO。</param>
        [Delete(typeof (PnMaintainCtrlDTO))]
        public void DeletePnMaintainCtrl(PnMaintainCtrlDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delPnMainCtrl = _maintainCtrlRepository.Get(dto.Id) as PnMaintainCtrl;
            //获取需要删除的对象。

            if (delPnMainCtrl != null)
            {
                _maintainCtrlRepository.DeletePnMaintainCtrl(delPnMainCtrl); //删除附件维修控制组。
            }
        }

        #endregion

        #region SnMaintainCtrlDTO

        /// <summary>
        ///     获取所有SnMaintainCtrl。
        /// </summary>
        public IQueryable<SnMaintainCtrlDTO> GetSnMaintainCtrls()
        {
            var queryBuilder =
                new QueryBuilder<SnMaintainCtrl>();
            return _maintainCtrlQuery.SnMaintainCtrlDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增SnMaintainCtrl。
        /// </summary>
        /// <param name="dto">SnMaintainCtrlDTO。</param>
        [Insert(typeof (SnMaintainCtrlDTO))]
        public void InsertSnMaintainCtrl(SnMaintainCtrlDTO dto)
        {
            //创建序号维修控制组
            SnMaintainCtrl newSnMainCtrl = MaintainCtrlFactory.CreateSnMaintainCtrl(dto.SnScope, (ControlStrategy)dto.CtrlStrategy);

            //添加维修控制明细
            dto.MaintainCtrlLines.ToList().ForEach(line => InsertMainCtrlLine(newSnMainCtrl, line));

            _maintainCtrlRepository.Add(newSnMainCtrl);
        }

        /// <summary>
        ///     更新SnMaintainCtrl。
        /// </summary>
        /// <param name="dto">SnMaintainCtrlDTO。</param>
        [Update(typeof (SnMaintainCtrlDTO))]
        public void ModifySnMaintainCtrl(SnMaintainCtrlDTO dto)
        {
            //获取需要更新的对象
            var updateSnMainCtrl = _maintainCtrlRepository.Get(dto.Id) as SnMaintainCtrl;

            if (updateSnMainCtrl != null)
            {
                //更新主表：
                updateSnMainCtrl.SetCtrlStrategy((ControlStrategy) dto.CtrlStrategy);
                updateSnMainCtrl.SetSnScope(dto.SnScope);

                //更新维修控制明细：
                List<MaintainCtrlLineDTO> dtoMaintainCtrlLines = dto.MaintainCtrlLines;
                ICollection<MaintainCtrlLine> maintainCtrlLines = updateSnMainCtrl.MaintainCtrlLines;
                DataHelper.DetailHandle(dtoMaintainCtrlLines.ToArray(),
                    maintainCtrlLines.ToArray(),
                    c => c.Id, p => p.Id,
                    i => InsertMainCtrlLine(updateSnMainCtrl, i),
                    UpdateMainCtrlLine,
                    d => _maintainCtrlRepository.RemoveMaintainCtrlLine(d));
            }
            _maintainCtrlRepository.Modify(updateSnMainCtrl);
        }

        /// <summary>
        ///     删除SnMaintainCtrl。
        /// </summary>
        /// <param name="dto">SnMaintainCtrlDTO。</param>
        [Delete(typeof (SnMaintainCtrlDTO))]
        public void DeleteSnMaintainCtrl(SnMaintainCtrlDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delSnMainCtrl = _maintainCtrlRepository.Get(dto.Id) as SnMaintainCtrl;
            //获取需要删除的对象。

            if (delSnMainCtrl != null)
            {
                _maintainCtrlRepository.DeleteSnMaintainCtrl(delSnMainCtrl); //删除序号维修控制组。
            }
        }

        #endregion

        #region 处理维修控制明细

        /// <summary>
        ///     插入维修控制明细
        /// </summary>
        /// <param name="maintainCtrl">维修控制组</param>
        /// <param name="line">维修控制明细DTO</param>
        private void InsertMainCtrlLine(MaintainCtrl maintainCtrl, MaintainCtrlLineDTO line)
        {
            //获取
            MaintainWork maintainWork = _maintainWorkRepository.Get(line.MaintainWorkId);
            CtrlUnit ctrlUnit = _ctrlUnitRepository.Get(line.CtrlUnitId);

            // 添加维修控制明细
            MaintainCtrlLine newMainCtrlLine = maintainCtrl.AddNewMaintainCtrlLine();
            newMainCtrlLine.SetCtrlUnit(ctrlUnit);
            newMainCtrlLine.SetMaintainWork(maintainWork);
            newMainCtrlLine.SetMaxInterval(line.MaxInterval);
            newMainCtrlLine.SetMinInterval(line.MinInterval);
            newMainCtrlLine.SetStandardInterval(line.StandardInterval);
        }

        /// <summary>
        ///     更新维修控制明细
        /// </summary>
        /// <param name="lineDto">维修控制明细DTO</param>
        /// <param name="line">维修控制明细</param>
        private void UpdateMainCtrlLine(MaintainCtrlLineDTO lineDto, MaintainCtrlLine line)
        {
            //获取
            MaintainWork maintainWork = _maintainWorkRepository.Get(lineDto.MaintainWorkId);
            CtrlUnit ctrlUnit = _ctrlUnitRepository.Get(lineDto.CtrlUnitId);

            // 更新维修控制明细
            line.SetCtrlUnit(ctrlUnit);
            line.SetMaintainWork(maintainWork);
            line.SetMaxInterval(lineDto.MaxInterval);
            line.SetMinInterval(lineDto.MinInterval);
            line.SetStandardInterval(lineDto.StandardInterval);
        }

        #endregion
    }
}