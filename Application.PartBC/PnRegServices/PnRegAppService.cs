#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：PnRegAppService
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
using UniCloud.Application.PartBC.Query.PnRegQueries;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Application.PartBC.PnRegServices
{
    /// <summary>
    ///     实现PnReg的服务接口。
    ///     用于处理PnReg相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class PnRegAppService : ContextBoundObject, IPnRegAppService
    {
        private readonly IItemRepository _itemRepository;
        private readonly IPnRegQuery _pnRegQuery;
        private readonly IPnRegRepository _pnRegRepository;

        public PnRegAppService(IPnRegQuery pnRegQuery, IPnRegRepository pnRegRepository,
            IItemRepository itemRepository)
        {
            _pnRegQuery = pnRegQuery;
            _pnRegRepository = pnRegRepository;
            _itemRepository = itemRepository;
        }

        #region PnRegDTO

        /// <summary>
        ///     获取所有PnReg。
        /// </summary>
        public IQueryable<PnRegDTO> GetPnRegs()
        {
            var queryBuilder =
                new QueryBuilder<PnReg>();
            return _pnRegQuery.PnRegDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增PnReg。
        /// </summary>
        /// <param name="dto">PnRegDTO。</param>
        [Insert(typeof (PnRegDTO))]
        public void InsertPnReg(PnRegDTO dto)
        {
            Item item = _itemRepository.Get(dto.ItemId);

            PnReg newPnReg = PnRegFactory.CreatePnReg(dto.IsLife, dto.Pn);
            newPnReg.SetItem(item);

            //添加依赖项
            dto.Dependencies.ToList().ForEach(dependency => InsertDependency(newPnReg, dependency));

            _pnRegRepository.Add(newPnReg);
        }

        /// <summary>
        ///     更新PnReg。
        /// </summary>
        /// <param name="dto">PnRegDTO。</param>
        [Update(typeof (PnRegDTO))]
        public void ModifyPnReg(PnRegDTO dto)
        {
            Item item = _itemRepository.Get(dto.ItemId);

            PnReg updatePnReg = _pnRegRepository.Get(dto.Id); //获取需要更新的对象。

            //更新主表。
            updatePnReg.SetPn(dto.Pn);
            updatePnReg.SetIsLife(dto.IsLife);
            updatePnReg.SetItem(item);

            //更新依赖项集合：
            List<DependencyDTO> dtoDependencies = dto.Dependencies;
            ICollection<Dependency> dependencies = updatePnReg.Dependencies;
            DataHelper.DetailHandle(dtoDependencies.ToArray(),
                dependencies.ToArray(),
                c => c.Id, p => p.Id,
                i => InsertDependency(updatePnReg, i),
                UpdateDependency,
                d => _pnRegRepository.RemoveDependency(d));

            _pnRegRepository.Modify(updatePnReg);
        }

        /// <summary>
        ///     删除PnReg。
        /// </summary>
        /// <param name="dto">PnRegDTO。</param>
        [Delete(typeof (PnRegDTO))]
        public void DeletePnReg(PnRegDTO dto)
        {
            PnReg delPnReg = _pnRegRepository.Get(dto.Id); //获取需要删除的对象。

            _pnRegRepository.DeletePnReg(delPnReg); //删除PnReg。
        }

        #region 处理依赖项

        /// <summary>
        ///     插入依赖项
        /// </summary>
        /// <param name="pnReg">附件</param>
        /// <param name="dependencyDto">依赖项DTO</param>
        private void InsertDependency(PnReg pnReg, DependencyDTO dependencyDto)
        {
            //获取
            PnReg dependencyPnReg = _pnRegRepository.Get(dependencyDto.DependencyPnId);

            // 添加依赖项
            pnReg.AddNewDependency(dependencyPnReg);
        }

        /// <summary>
        ///     更新依赖项
        /// </summary>
        /// <param name="dependencyDto">依赖项DTO</param>
        /// <param name="dependency">依赖项</param>
        private void UpdateDependency(DependencyDTO dependencyDto, Dependency dependency)
        {
            //获取
            PnReg dependencyPnReg = _pnRegRepository.Get(dependencyDto.DependencyPnId);

            dependency.SetPnReg(dependencyPnReg);
        }

        #endregion

        #endregion
    }
}