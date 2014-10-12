//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.AircraftConfigBC.ActionCategoryServices;
using UniCloud.Application.AircraftConfigBC.AircraftCategoryServices;
using UniCloud.Application.AircraftConfigBC.AircraftSeriesServices;
using UniCloud.Application.AircraftConfigBC.AircraftTypeServices;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.ManufacturerServices;
using UniCloud.Infrastructure.Utilities.Container;

namespace UniCloud.DistributedServices.AircraftConfig
{

    /// <summary>
    /// 飞机构型模块数据类
    /// </summary>
    public class AircraftConfigData : ExposeData.ExposeData
    {
         private readonly IAircraftSeriesAppService _aircraftSeriesAppService;
        private readonly IActionCategoryAppService _actionCategoryAppService;
        private readonly IAircraftCategoryAppService _aircraftCategoryAppService;
        private readonly IAircraftTypeAppService _aircraftTypeAppService;
        private readonly IManufacturerAppService _manufacturerAppService;


        public AircraftConfigData()
            : base("UniCloud.Application.AircraftConfigBC.DTO")
        {
            _actionCategoryAppService = DefaultContainer.Resolve<IActionCategoryAppService>();
            _aircraftSeriesAppService = DefaultContainer.Resolve<IAircraftSeriesAppService>();
            _aircraftCategoryAppService = DefaultContainer.Resolve<IAircraftCategoryAppService>();
            _aircraftTypeAppService = DefaultContainer.Resolve<IAircraftTypeAppService>();
            _manufacturerAppService = DefaultContainer.Resolve<IManufacturerAppService>();
        }

        #region 活动类型

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<ActionCategoryDTO> ActionCategories
        {
            get
            {
                return GetStaticData("actionCategoriesFleetPlan", () => _actionCategoryAppService.GetActionCategories());
            }
        }

        #endregion

        #region 飞机系列集合

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<AircraftSeriesDTO> AircraftSeries
        {
            get { return GetStaticData("AircraftSeriesFleetPlan", () => _aircraftSeriesAppService.GetAircraftSeries()); }
        }

        #endregion

        #region 座级

        /// <summary>
        ///     座级集合
        /// </summary>
        public IQueryable<AircraftCategoryDTO> AircraftCategories
        {
            get
            {
                return GetStaticData("aircraftCategoriesFleetPlan",
                    () => _aircraftCategoryAppService.GetAircraftCategories());
            }
        }

        #endregion

        #region 机型

        /// <summary>
        ///     机型集合
        /// </summary>
        public IQueryable<AircraftTypeDTO> AircraftTypes
        {
            get { return GetStaticData("aircraftTypesFleetPlan", () => _aircraftTypeAppService.GetAircraftTypes()); }
        }

        #endregion

        #region 制造商

        /// <summary>
        ///     制造商集合
        /// </summary>
        public IQueryable<ManufacturerDTO> Manufacturers
        {
            get { return GetStaticData("manufactoriesFleetPlan", () => _manufacturerAppService.GetManufacturers()); }
        }

        #endregion
    }
}