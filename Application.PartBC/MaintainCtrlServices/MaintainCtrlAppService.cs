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
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.MaintainCtrlQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainCtrlAgg;
using UniCloud.Domain.PartBC.Aggregates.MaintainWorkAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

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
        private readonly IItemRepository _itemRepository;
        private readonly IMaintainCtrlQuery _maintainCtrlQuery;
        private readonly IMaintainCtrlRepository _maintainCtrlRepository;
        private readonly IMaintainWorkRepository _maintainWorkRepository;
        private readonly IPnRegRepository _pnRegRepository;

        public MaintainCtrlAppService(IMaintainCtrlQuery maintainCtrlQuery,
            IItemRepository itemRepository,
            IMaintainCtrlRepository maintainCtrlRepository,
            IMaintainWorkRepository maintainWorkRepository,
            IPnRegRepository pnRegRepository)
        {
            _itemRepository = itemRepository;
            _maintainCtrlQuery = maintainCtrlQuery;
            _maintainCtrlRepository = maintainCtrlRepository;
            _maintainWorkRepository = maintainWorkRepository;
            _pnRegRepository = pnRegRepository;
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
            Item item = _itemRepository.Get(dto.ItemId);
            MaintainWork maintainWork = _maintainWorkRepository.Get(dto.MaintainWorkId);

            //创建项维修控制组
            ItemMaintainCtrl newItemMainCtrl = MaintainCtrlFactory.CreateItemMaintainCtrl(item,
                ((ControlStrategy) dto.CtrlStrategy),dto.Description,dto.CtrlDetail,maintainWork);
            
            _maintainCtrlRepository.Add(newItemMainCtrl);
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
                Item item = _itemRepository.Get(dto.ItemId);
                MaintainWork maintainWork = _maintainWorkRepository.Get(dto.MaintainWorkId);

                //更新主表：
                updateItemMainCtrl.SetCtrlStrategy((ControlStrategy) dto.CtrlStrategy);
                updateItemMainCtrl.SetItem(item);
                updateItemMainCtrl.SetDescription(dto.Description);
                updateItemMainCtrl.SetCtrlDetail(dto.CtrlDetail);
                updateItemMainCtrl.SetMaintainWork(maintainWork);
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
                _maintainCtrlRepository.Remove(delItemMainCtrl); //删除项维修控制组。
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
            MaintainWork maintainWork = _maintainWorkRepository.Get(dto.MaintainWorkId);

            //创建附件维修控制组
            PnMaintainCtrl newPnMainCtrl = MaintainCtrlFactory.CreatePnMaintainCtrl(pnReg,
                (ControlStrategy)dto.CtrlStrategy, dto.Description, dto.CtrlDetail, maintainWork);

            _maintainCtrlRepository.Add(newPnMainCtrl);
        }

        /// <summary>
        ///     更新PnMaintainCtrl。
        /// </summary>
        /// <param name="dto">PnMaintainCtrlDTO。</param>
        [Update(typeof (PnMaintainCtrlDTO))]
        public void ModifyPnMaintainCtrl(PnMaintainCtrlDTO dto)
        {
            //获取需要更新的对象
            var updatePnMainCtrl = _maintainCtrlRepository.Get(dto.Id) as PnMaintainCtrl;

            if (updatePnMainCtrl != null)
            {
                //获取附件
                PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);
                MaintainWork maintainWork = _maintainWorkRepository.Get(dto.MaintainWorkId);
                
                //更新主表：
                updatePnMainCtrl.SetCtrlStrategy((ControlStrategy) dto.CtrlStrategy);
                updatePnMainCtrl.SetPnReg(pnReg);
                updatePnMainCtrl.SetDescription(dto.Description);
                updatePnMainCtrl.SetCtrlDetail(dto.CtrlDetail);
                updatePnMainCtrl.SetMaintainWork(maintainWork);

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
                _maintainCtrlRepository.Remove(delPnMainCtrl); //删除附件维修控制组。
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
            MaintainWork maintainWork = _maintainWorkRepository.Get(dto.MaintainWorkId);

            //创建序号维修控制组
            SnMaintainCtrl newSnMainCtrl = MaintainCtrlFactory.CreateSnMaintainCtrl(dto.SnScope,
                (ControlStrategy)dto.CtrlStrategy, dto.Description, dto.CtrlDetail, maintainWork);

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
                MaintainWork maintainWork = _maintainWorkRepository.Get(dto.MaintainWorkId);

                //更新主表：
                updateSnMainCtrl.SetCtrlStrategy((ControlStrategy) dto.CtrlStrategy);
                updateSnMainCtrl.SetSnScope(dto.SnScope);
                updateSnMainCtrl.SetDescription(dto.Description);
                updateSnMainCtrl.SetCtrlDetail(dto.CtrlDetail);
                updateSnMainCtrl.SetMaintainWork(maintainWork);
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
                _maintainCtrlRepository.Remove(delSnMainCtrl); //删除序号维修控制组。
            }
        }

        #endregion
    }
}