#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：EngineAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.EngineQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.EngineServices
{
    /// <summary>
    ///     实现发动机服务接口。
    ///     用于处理发动机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class EngineAppService : IEngineAppService
    {
        private readonly IEngineQuery _engineQuery;
        private readonly IActionCategoryRepository _actionCategoryRepository;
        private readonly IAirlinesRepository _airlinesRepository;
        private readonly IEngineRepository _engineRepository;
        private readonly IEngineTypeRepository _engineTypeRepository;
        private readonly ISupplierRepository _supplierRepository;

        public EngineAppService(IEngineQuery engineQuery, IActionCategoryRepository actionCategoryRepository,
            IAirlinesRepository airlinesRepository, IEngineRepository engineRepository,
            IEngineTypeRepository engineTypeRepository, ISupplierRepository supplierRepository)
        {
            _engineQuery = engineQuery;
            _actionCategoryRepository = actionCategoryRepository;
            _airlinesRepository = airlinesRepository;
            _engineRepository = engineRepository;
            _engineTypeRepository = engineTypeRepository;
            _supplierRepository = supplierRepository;
        }

        #region EngineDTO

        /// <summary>
        ///     获取所有发动机
        /// </summary>
        /// <returns></returns>
        public IQueryable<EngineDTO> GetEngines()
        {
            var queryBuilder =
                new QueryBuilder<Engine>();
            return _engineQuery.EngineDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增实际发动机。
        /// </summary>
        /// <param name="dto">实际发动机DTO。</param>
        [Insert(typeof(EngineDTO))]
        public void InsertEngine(EngineDTO dto)
        {
            //获取相关数据
            var engineType = _engineTypeRepository.Get(dto.EngineTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);
            var importCategory = _actionCategoryRepository.Get(dto.ImportCategoryId);
            var supplier = _supplierRepository.Get(dto.SupplierId);

            //创建新的实际发动机
            var newEngine = EngineFactory.CreateEngine();
            newEngine.SetEngineType(engineType);
            newEngine.SetAirlines(airlines);
            newEngine.SetExportDate(dto.ExportDate);
            newEngine.SetFactoryDate(dto.FactoryDate);
            newEngine.SetImportCategory(importCategory);
            newEngine.SetImportDate(dto.ImportDate);
            newEngine.SetMaxThrust(dto.MaxThrust);
            newEngine.SetSerialNumber(dto.SerialNumber);
            newEngine.SetSupplier(supplier);

            //添加商业数据历史
            dto.EngineBusinessHistories.ToList().ForEach(line => InsertEngineBusinesHistory(newEngine, line));

            //添加所有权历史
            dto.EngineOwnerShipHistories.ToList().ForEach(line => InsertEngineOwnershipHistory(newEngine, line));

            _engineRepository.Add(newEngine);
        }


        /// <summary>
        ///     更新实际发动机。
        /// </summary>
        /// <param name="dto">实际发动机DTO。</param>
        [Update(typeof(EngineDTO))]
        public void ModifyEngine(EngineDTO dto)
        {
            //获取相关数据
            var engineType = _engineTypeRepository.Get(dto.EngineTypeId);
            var airlines = _airlinesRepository.Get(dto.AirlinesId);
            var importCategory = _actionCategoryRepository.Get(dto.ImportCategoryId);
            var supplier = _supplierRepository.Get(dto.SupplierId);

            //获取
            var updateEngine = _engineRepository.Get(dto.Id);
            if (updateEngine != null)
            {
                //更新实际发动机
                updateEngine.SetEngineType(engineType);
                updateEngine.SetAirlines(airlines);
                updateEngine.SetExportDate(dto.ExportDate);
                updateEngine.SetFactoryDate(dto.FactoryDate);
                updateEngine.SetImportCategory(importCategory);
                updateEngine.SetImportDate(dto.ImportDate);
                updateEngine.SetMaxThrust(dto.MaxThrust);
                updateEngine.SetSerialNumber(dto.SerialNumber);
                updateEngine.SetSupplier(supplier);

                //更新商业数据历史：
                var dtoEngineBusinessHistories = dto.EngineBusinessHistories;
                var engineBusinessHistories = updateEngine.EngineBusinessHistories;
                DataHelper.DetailHandle(dtoEngineBusinessHistories.ToArray(),
                    engineBusinessHistories.ToArray(),
                    c => c.EngineId, p => p.Id,
                    i => InsertEngineBusinesHistory(updateEngine, i),
                    UpdateEngineBusinessHistory,
                    d => _engineRepository.RemoveEngineBusinessHistory(d));

                //更新所有权历史：
                var dtoEngineOwnerShipHistories = dto.EngineOwnerShipHistories;
                var engineOwnerShipHistories = updateEngine.EngineOwnerShipHistories;
                DataHelper.DetailHandle(dtoEngineOwnerShipHistories.ToArray(),
                    engineOwnerShipHistories.ToArray(),
                    c => c.EngineId, p => p.Id,
                    i => InsertEngineOwnershipHistory(updateEngine, i),
                    UpdateEngineOwnershipHistory,
                    d => _engineRepository.RemoveEngineOwnershipHistory(d));
            }
            _engineRepository.Modify(updateEngine);
        }

        /// <summary>
        ///     删除实际发动机。
        /// </summary>
        /// <param name="dto">实际发动机DTO。</param>
        [Delete(typeof(EngineDTO))]
        public void DeleteEngine(EngineDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delEngine = _engineRepository.Get(dto.Id);
            //获取需要删除的对象。
            if (delEngine != null)
            {
                _engineRepository.DeleteEngine(delEngine); //删除实际发动机。
            }
        }

        #region 处理实际发动机的相关数据

        /// <summary>
        ///     插入商业数据历史
        /// </summary>
        /// <param name="engine">实际发动机</param>
        /// <param name="engineBhDto">商业数据历史DTO</param>
        private void InsertEngineBusinesHistory(Engine engine, EngineBusinessHistoryDTO engineBhDto)
        {
            //获取相关数据
            var engineType = _engineTypeRepository.Get(engineBhDto.EngineTypeId);
            var importCategory = _actionCategoryRepository.Get(engineBhDto.ImportCategoryId);

            //添加商业数据历史
            var newEngineBh = engine.AddNewEngineBusinessHistory();
            newEngineBh.SetEngineType(engineType);
            newEngineBh.SetEndDate(engineBhDto.EndDate);
            newEngineBh.SetImportCategory(importCategory);
            newEngineBh.SetStartDate(engineBhDto.StartDate);
            newEngineBh.SetMaxThrust(engineBhDto.MaxThrust);

        }

        /// <summary>
        ///     插入所有权历史
        /// </summary>
        /// <param name="engine">实际发动机</param>
        /// <param name="engineOhDto">所有权历史DTO</param>
        private void InsertEngineOwnershipHistory(Engine engine, EngineOwnershipHistoryDTO engineOhDto)
        {
            //获取相关数据
            var supplier = _supplierRepository.Get(engineOhDto.SupplierId);

            //添加所有权历史
            var newEngineOh = engine.AddNewEngineOwnershipHistory();
            newEngineOh.SetEndDate(engineOhDto.EndDate);
            newEngineOh.SetStartDate(engineOhDto.StartDate);
            newEngineOh.SetSupplier(supplier);
        }

        /// <summary>
        ///     更新商业数据历史
        /// </summary>
        /// <param name="engineBhDto">商业数据历史DTO</param>
        /// <param name="engineBh">商业数据历史</param>
        private void UpdateEngineBusinessHistory(EngineBusinessHistoryDTO engineBhDto, EngineBusinessHistory engineBh)
        {
            //获取相关数据
            var engineType = _engineTypeRepository.Get(engineBhDto.EngineTypeId);
            var importCategory = _actionCategoryRepository.Get(engineBhDto.ImportCategoryId);

            //更新商业数据历史
            engineBh.SetEngineType(engineType);
            engineBh.SetEndDate(engineBhDto.EndDate);
            engineBh.SetImportCategory(importCategory);
            engineBh.SetStartDate(engineBhDto.StartDate);
            engineBh.SetMaxThrust(engineBhDto.MaxThrust);
        }

        /// <summary>
        ///     更新所有权历史
        /// </summary>
        /// <param name="engineOhDto">所有权历史DTO</param>
        /// <param name="engineOh">所有权历史</param>
        private void UpdateEngineOwnershipHistory(EngineOwnershipHistoryDTO engineOhDto, EngineOwnershipHistory engineOh)
        {
            //获取相关数据
            var supplier = _supplierRepository.Get(engineOhDto.SupplierId);

            //更新所有权历史
            engineOh.SetEndDate(engineOhDto.EndDate);
            engineOh.SetStartDate(engineOhDto.StartDate);
            engineOh.SetSupplier(supplier);
        }
        #endregion
        #endregion
    }
}
