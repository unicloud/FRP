#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/1/1 9:49:58
// 文件名：StaticLoad
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.ActionCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftCategoryQueries;
using UniCloud.Application.FleetPlanBC.Query.AircraftTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.AirlinesQueries;
using UniCloud.Application.FleetPlanBC.Query.EngineTypeQueries;
using UniCloud.Application.FleetPlanBC.Query.ManagerQueries;
using UniCloud.Application.FleetPlanBC.Query.ManufacturerQueries;
using UniCloud.Application.FleetPlanBC.Query.ProgrammingQueries;
using UniCloud.Application.FleetPlanBC.Query.SupplierQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.ActionCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftCategoryAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.AirlinesAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.EngineTypeAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManagerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ManufacturerAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.ProgrammingAgg;
using UniCloud.Domain.FleetPlanBC.Aggregates.SupplierAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC
{
    /// <summary>
    /// </summary>
    public class StaticLoad : IStaticLoad
    {
        private static bool _refreshActionCategory;
        private static bool _refreshAircraftType;
        private static bool _refreshAircraftCategory;
        private static bool _refreshAirlines;
        private static bool _refreshEngineType;
        private static bool _refreshManager;
        private static bool _refreshManufacturer;
        private static bool _refreshProgramming;
        private static bool _refreshSupplier;


        private static IList<ActionCategoryDTO> _actionCategories;
        private static IList<AircraftTypeDTO> _aircraftTypes;
        private static IList<AircraftCategoryDTO> _aircraftCategories;
        private static IList<AirlinesDTO> _aiurAirlineses;
        private static IList<EngineTypeDTO> _engineTypes;
        private static IList<ManagerDTO> _managers;
        private static IList<ManufacturerDTO> _manufacturers;
        private static IList<ProgrammingDTO> _programmings;
        private static IList<SupplierDTO> _suppliers;

        private readonly IActionCategoryQuery _actionCategoryQuery;
        private readonly IAircraftTypeQuery _aircraftTypeQuery;
        private readonly IAircraftCategoryQuery _aircraftCategoryQuery;
        private readonly IAirlinesQuery _airlinesQuery;
        private readonly IEngineTypeQuery _engineTypeQuery;
        private readonly IManagerQuery _managerQuery;
        private readonly IManufacturerQuery _manufacturerQuery;
        private readonly IProgrammingQuery _programmingQuery;
        private readonly ISupplierQuery _supplierQuery;

        public StaticLoad(IActionCategoryQuery actionCategoryQuery,
            IAircraftTypeQuery aircraftTypeQuery,IAircraftCategoryQuery aircraftCategoryQuery,
            IAirlinesQuery airlinesQuery,IEngineTypeQuery engineTypeQuery,
            IManagerQuery managerQuery,IManufacturerQuery manufacturerQuery,
            IProgrammingQuery programmingQuery,ISupplierQuery supplierQuery)
        {
            _actionCategoryQuery = actionCategoryQuery;
            _aircraftTypeQuery = aircraftTypeQuery;
            _aircraftCategoryQuery = aircraftCategoryQuery;
            _airlinesQuery = airlinesQuery;
            _engineTypeQuery = engineTypeQuery;
            _managerQuery = managerQuery;
            _manufacturerQuery = manufacturerQuery;
            _programmingQuery = programmingQuery;
            _supplierQuery = supplierQuery;
        }

        #region IStaticLoad 成员

        /// <summary>
        /// 设置刷新活动类型集合
        /// </summary>
        public void RefreshActionCategory()
        {
            _refreshActionCategory = true;
        }

        /// <summary>
        /// 设置刷新机型集合
        /// </summary>
        public void RefreshAircraftType()
        {
            _refreshAircraftType = true;
        }

        /// <summary>
        /// 设置刷新座级集合
        /// </summary>
        public void RefreshAircraftCategory()
        {
            _refreshAircraftCategory = true;
        }

        /// <summary>
        /// 设置刷新航空公司集合
        /// </summary>
        public void RefreshAirlines()
        {
            _refreshAirlines = true;
        }

        /// <summary>
        /// 设置刷新发动机型号集合
        /// </summary>
        public void RefreshEngineType()
        {
            _refreshEngineType = true;
        }

        /// <summary>
        /// 设置刷新管理者集合
        /// </summary>
        public void RefreshManager()
        {
            _refreshManager = true;
        }

        /// <summary>
        /// 设置刷新制造商集合
        /// </summary>
        public void RefreshManufacturer()
        {
            _refreshManufacturer = true;
        }

        /// <summary>
        /// 设置刷新规划期间集合
        /// </summary>
        public void RefreshProgramming()
        {
            _refreshProgramming = true;
        }

        /// <summary>
        /// 设置刷新供应商集合
        /// </summary>
        public void RefreshSupplier()
        {
            _refreshSupplier = true;
        }

        /// <summary>
        ///     获取活动类型静态集合
        /// </summary>
        /// <returns>活动类型静态集合</returns>
        public IQueryable<ActionCategoryDTO> GetActionCategories()
        {
            if (_actionCategories == null || _refreshActionCategory)
            {
                var query = new QueryBuilder<ActionCategory>();
                _actionCategories = _actionCategoryQuery.ActionCategoryDTOQuery(query).ToList();
                _refreshActionCategory = false;
            }
            return _actionCategories.AsQueryable();
        }

        /// <summary>
        ///     获取机型静态集合
        /// </summary>
        /// <returns>机型静态集合</returns>
        public IQueryable<AircraftTypeDTO> GetAircraftTypes()
        {
            if (_aircraftTypes == null || _refreshAircraftType)
            {
                var query = new QueryBuilder<AircraftType>();
                _aircraftTypes = _aircraftTypeQuery.AircraftTypeDTOQuery(query).ToList();
                _refreshAircraftType = false;
            }
            return _aircraftTypes.AsQueryable();
        }

        /// <summary>
        ///     获取座级静态集合
        /// </summary>
        /// <returns>座级静态集合</returns>
        public IQueryable<AircraftCategoryDTO> GetAircraftCategories()
        {
            if (_aircraftCategories == null || _refreshAircraftCategory)
            {
                var query = new QueryBuilder<AircraftCategory>();
                _aircraftCategories = _aircraftCategoryQuery.AircraftCategoryDTOQuery(query).ToList();
                _refreshAircraftCategory = false;
            }
            return _aircraftCategories.AsQueryable();
        }

        /// <summary>
        ///     获取航空公司静态集合
        /// </summary>
        /// <returns>航空公司静态集合</returns>
        public IQueryable<AirlinesDTO> GetAirlineses()
        {
            if (_aiurAirlineses == null || _refreshAirlines)
            {
                var query = new QueryBuilder<Airlines>();
                _aiurAirlineses = _airlinesQuery.AirlinesDTOQuery(query).ToList();
                _refreshAirlines = false;
            }
            return _aiurAirlineses.AsQueryable();
        }

        /// <summary>
        ///     获取发动机型号静态集合
        /// </summary>
        /// <returns>发动机型号静态集合</returns>
        public IQueryable<EngineTypeDTO> GetEngineTypes()
        {
            if (_engineTypes == null || _refreshEngineType)
            {
                var query = new QueryBuilder<EngineType>();
                _engineTypes = _engineTypeQuery.EngineTypeDTOQuery(query).ToList();
                _refreshEngineType = false;
            }
            return _engineTypes.AsQueryable();
        }

        /// <summary>
        ///     获取管理者静态集合
        /// </summary>
        /// <returns>管理者静态集合</returns>
        public IQueryable<ManagerDTO> GetManagers()
        {
            if (_managers == null || _refreshManager)
            {
                var query = new QueryBuilder<Manager>();
                _managers = _managerQuery.ManagerDTOQuery(query).ToList();
                _refreshManager = false;
            }
            return _managers.AsQueryable();
        }

        /// <summary>
        ///     获取制造商静态集合
        /// </summary>
        /// <returns>制造商静态集合</returns>
        public IQueryable<ManufacturerDTO> GetManufacturers()
        {
            if (_manufacturers == null || _refreshManufacturer)
            {
                var query = new QueryBuilder<Manufacturer>();
                _manufacturers = _manufacturerQuery.ManufacturerDTOQuery(query).ToList();
                _refreshManufacturer = false;
            }
            return _manufacturers.AsQueryable();
        }

        /// <summary>
        ///     获取规划期间静态集合
        /// </summary>
        /// <returns>规划期间静态集合</returns>
        public IQueryable<ProgrammingDTO> GetProgrammings()
        {
            if (_programmings == null || _refreshProgramming)
            {
                var query = new QueryBuilder<Programming>();
                _programmings = _programmingQuery.ProgrammingDTOQuery(query).ToList();
                _refreshProgramming = false;
            }
            return _programmings.AsQueryable();
        }

        /// <summary>
        ///     获取供应商静态集合
        /// </summary>
        /// <returns>供应商静态集合</returns>
        public IQueryable<SupplierDTO> GetSuppliers()
        {
            if (_suppliers == null || _refreshSupplier)
            {
                var query = new QueryBuilder<Supplier>();
                _suppliers = _supplierQuery.SupplierDTOQuery(query).ToList();
                _refreshSupplier = false;
            }
            return _suppliers.AsQueryable();
        }

        #endregion
    }
}
