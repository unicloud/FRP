//------------------------------------------------------------------------------
// 
//------------------------------------------------------------------------------

using System.Linq;
using UniCloud.Application.AircraftConfigBC.ActionCategoryServices;
using UniCloud.Application.AircraftConfigBC.AircraftCategoryServices;
using UniCloud.Application.AircraftConfigBC.AircraftConfigurationServices;
using UniCloud.Application.AircraftConfigBC.AircraftLicenseServices;
using UniCloud.Application.AircraftConfigBC.AircraftSeriesServices;
using UniCloud.Application.AircraftConfigBC.AircraftServices;
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
        private readonly IAircraftLicenseAppService _aircraftLicenseAppService;
        private readonly IAircraftAppService _aircraftAppService;
        private readonly IAircraftConfigurationAppService _aircraftConfigurationAppService;
        public AircraftConfigData()
            : base("UniCloud.Application.AircraftConfigBC.DTO")
        {
            _actionCategoryAppService = UniContainer.Resolve<IActionCategoryAppService>();
            _aircraftSeriesAppService = UniContainer.Resolve<IAircraftSeriesAppService>();
            _aircraftCategoryAppService = UniContainer.Resolve<IAircraftCategoryAppService>();
            _aircraftTypeAppService = UniContainer.Resolve<IAircraftTypeAppService>();
            _manufacturerAppService = UniContainer.Resolve<IManufacturerAppService>();
            _aircraftLicenseAppService = UniContainer.Resolve<IAircraftLicenseAppService>();
            _aircraftAppService = UniContainer.Resolve<IAircraftAppService>();
            _aircraftConfigurationAppService = UniContainer.Resolve<IAircraftConfigurationAppService>();
        }

        #region 活动类型

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<ActionCategoryDTO> ActionCategories
        {
            get
            {
                return GetStaticData("actionCategoriesAircraftConfig", () => _actionCategoryAppService.GetActionCategories());
            }
        }

        #endregion

        #region 飞机系列集合

        /// <summary>
        ///     活动类型集合
        /// </summary>
        public IQueryable<AircraftSeriesDTO> AircraftSeries
        {
            get { return _aircraftSeriesAppService.GetAircraftSeries(); }
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
                return GetStaticData("aircraftCategoriesAircraftConfig",
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
            get { return _aircraftTypeAppService.GetAircraftTypes(); }
        }

        /// <summary>
        ///     民航机型集合
        /// </summary>
        public IQueryable<CAACAircraftTypeDTO> CAACAircraftTypes
        {
            get { return _aircraftTypeAppService.GetCAACAircraftTypes(); }
        }
        #endregion

        #region 制造商

        /// <summary>
        ///     制造商集合
        /// </summary>
        public IQueryable<ManufacturerDTO> Manufacturers
        {
            get { return GetStaticData("manufactoriesAircraftConfig", () => _manufacturerAppService.GetManufacturers()); }
        }

        #endregion

        #region 飞机证照
        /// <summary>
        ///     证照类型
        /// </summary>
        public IQueryable<LicenseTypeDTO> LicenseTypes
        {
            get { return _aircraftLicenseAppService.GetLicenseTypes(); }
        }
       
        #endregion

        #region 实际飞机

        /// <summary>
        ///     实际飞机集合
        /// </summary>
        public IQueryable<AircraftDTO> Aircrafts
        {
            get { return _aircraftAppService.GetAircrafts(); }
        }

        #endregion

        #region 飞机配置
        /// <summary>
        ///     飞机配置集合
        /// </summary>
        public IQueryable<AircraftConfigurationDTO> AircraftConfigurations
        {
            get { return _aircraftConfigurationAppService.GetAircraftConfigurations(); }
        }
        #endregion
    }
}