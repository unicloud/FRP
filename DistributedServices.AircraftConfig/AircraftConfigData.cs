#region 版本控制

// =====================================================
// 版权所有 (C) 2014 UniCloud 
// 【本类功能概述】
// 
// 作者：丁志浩 时间：2013/11/29，13:11
// 方案：FRP
// 项目：DistributedServices.AircraftConfig
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// =====================================================

#endregion

#region 命名空间

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
using UniCloud.DistributedServices.Data;
using UniCloud.Infrastructure.Data;
using UniCloud.Infrastructure.Unity;

#endregion

namespace UniCloud.DistributedServices.AircraftConfig
{
    /// <summary>
    ///     飞机构型模块数据类
    /// </summary>
    public class AircraftConfigData : ServiceData
    {
        private readonly IActionCategoryAppService _actionCategoryAppService;
        private readonly IAircraftAppService _aircraftAppService;
        private readonly IAircraftCategoryAppService _aircraftCategoryAppService;
        private readonly IAircraftConfigurationAppService _aircraftConfigurationAppService;
        private readonly IAircraftLicenseAppService _aircraftLicenseAppService;
        private readonly IAircraftSeriesAppService _aircraftSeriesAppService;
        private readonly IAircraftTypeAppService _aircraftTypeAppService;
        private readonly IManufacturerAppService _manufacturerAppService;

        public AircraftConfigData()
            : base("UniCloud.Application.AircraftConfigBC.DTO", UniContainer.Resolve<IQueryableUnitOfWork>())
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
                return GetStaticData("actionCategoriesAircraftConfig",
                    () => _actionCategoryAppService.GetActionCategories());
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
            get
            {
                return GetStaticData("manufactoriesAircraftConfig", () => _manufacturerAppService.GetManufacturers());
            }
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