#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/04/07，21:04
// 文件名：InstallControllerAppService.cs
// 程序集：UniCloud.Application.PartBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.InstallControllerQueries;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.PartBC.Aggregates.InstallControllerAgg;
using UniCloud.Domain.PartBC.Aggregates.ItemAgg;
using UniCloud.Domain.PartBC.Aggregates.PnRegAgg;

#endregion

namespace UniCloud.Application.PartBC.InstallControllerServices
{
    /// <summary>
    ///     实现装机控制服务接口。
    ///     用于处理装机控制相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class InstallControllerAppService : IInstallControllerAppService
    {
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        private readonly IInstallControllerQuery _installControllerQuery;
        private readonly IInstallControllerRepository _installControllerRepository;
        private readonly IItemRepository _itemRepository;
        private readonly IPnRegRepository _pnRegRepository;

        public InstallControllerAppService(IInstallControllerQuery installControllerQuery,
            IInstallControllerRepository installControllerRepository,
            IAircraftTypeRepository aircraftTypeRepository,
            IItemRepository itemRepository,
            IPnRegRepository pnRegRepository)
        {
            _installControllerQuery = installControllerQuery;
            _installControllerRepository = installControllerRepository;
            _aircraftTypeRepository = aircraftTypeRepository;
            _itemRepository = itemRepository;
            _pnRegRepository = pnRegRepository;
        }

        #region InstallControllerDTO

        /// <summary>
        ///     获取所有装机控制。
        /// </summary>
        public IQueryable<InstallControllerDTO> GetInstallControllers()
        {
            var queryBuilder =
                new QueryBuilder<InstallController>();
            return _installControllerQuery.InstallControllerDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增InstallController。
        /// </summary>
        /// <param name="dto">InstallControllerDTO。</param>
        [Insert(typeof (InstallControllerDTO))]
        public void InsertInstallController(InstallControllerDTO dto)
        {
            Item item = _itemRepository.Get(dto.ItemId);
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);
            AircraftType aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);

            InstallController newInstallController = InstallControllerFactory.CreateInstallController(dto.StartDate,
                dto.EndDate, item, pnReg, aircraftType);

            //添加依赖项
            dto.Dependencies.ToList().ForEach(dependency => InsertDependency(newInstallController, dependency));

            _installControllerRepository.Add(newInstallController);
        }

        /// <summary>
        ///     更新InstallController。
        /// </summary>
        /// <param name="dto">InstallControllerDTO。</param>
        [Update(typeof (InstallControllerDTO))]
        public void ModifyInstallController(InstallControllerDTO dto)
        {
            Item item = _itemRepository.Get(dto.ItemId);
            PnReg pnReg = _pnRegRepository.Get(dto.PnRegId);
            AircraftType aircraftType = _aircraftTypeRepository.Get(dto.AircraftTypeId);

            InstallController updateInstallController = _installControllerRepository.Get(dto.Id); //获取需要更新的对象。

            //更新主表。
            updateInstallController.SetAircraftType(aircraftType);
            updateInstallController.SetEndDate(dto.EndDate);
            updateInstallController.SetItem(item);
            updateInstallController.SetPnReg(pnReg);
            updateInstallController.SetStartDate(dto.StartDate);

            //更新依赖项集合：
            List<DependencyDTO> dtoDependencies = dto.Dependencies;
            ICollection<Dependency> dependencies = updateInstallController.Dependencies;
            DataHelper.DetailHandle(dtoDependencies.ToArray(),
                dependencies.ToArray(),
                c => c.Id, p => p.Id,
                i => InsertDependency(updateInstallController, i),
                UpdateDependency,
                d => _installControllerRepository.RemoveDependency(d));

            _installControllerRepository.Modify(updateInstallController);
        }

        /// <summary>
        ///     删除InstallController。
        /// </summary>
        /// <param name="dto">InstallControllerDTO。</param>
        [Delete(typeof (InstallControllerDTO))]
        public void DeleteInstallController(InstallControllerDTO dto)
        {
            InstallController delInstallController = _installControllerRepository.Get(dto.Id); //获取需要删除的对象。

            _installControllerRepository.DeleteInstallController(delInstallController); //删除InstallController。
        }

        #region 处理依赖项

        /// <summary>
        ///     插入依赖项
        /// </summary>
        /// <param name="installController">附件</param>
        /// <param name="dependencyDto">依赖项DTO</param>
        private void InsertDependency(InstallController installController, DependencyDTO dependencyDto)
        {
            //获取
            PnReg pnReg = _pnRegRepository.Get(dependencyDto.DependencyPnId);

            // 添加依赖项
            installController.AddNewDependency(pnReg);
        }

        /// <summary>
        ///     更新依赖项
        /// </summary>
        /// <param name="dependencyDto">依赖项DTO</param>
        /// <param name="dependency">依赖项</param>
        private void UpdateDependency(DependencyDTO dependencyDto, Dependency dependency)
        {
            //获取
            PnReg pnReg = _pnRegRepository.Get(dependencyDto.DependencyPnId);

            dependency.SetPnReg(pnReg);
        }

        #endregion

        #endregion
    }
}