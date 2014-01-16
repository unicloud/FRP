#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/1/16 14:36:31
// 文件名：AircraftLicenseAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/1/16 14:36:31
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.AircraftLicenseQueries;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.AircraftLicenseServices
{
    /// <summary>
    ///     实现飞机证照服务接口。
    ///     用于处理飞机证照相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftLicenseAppService : IAircraftLicenseAppService
    {
        private readonly IAircraftLicenseQuery _aircraftLicenseQuery;
        private readonly IAircraftLicenseRepository _aircraftLicenseRepository;
        private readonly ILicenseTypeRepository _licenseTypeRepository;

        public AircraftLicenseAppService(IAircraftLicenseQuery aircraftLicenseQuery, IAircraftLicenseRepository aircraftLicenseRepository,
            ILicenseTypeRepository licenseTypeRepository)
        {
            _aircraftLicenseQuery = aircraftLicenseQuery;
            _aircraftLicenseRepository = aircraftLicenseRepository;
            _licenseTypeRepository = licenseTypeRepository;
        }

        #region AircraftLicenseDTO

        /// <summary>
        ///     获取所有飞机证照
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftLicenseDTO> GetAircraftLicenses()
        {
            var queryBuilder =
                new QueryBuilder<AircraftLicense>();
            return _aircraftLicenseQuery.AircraftLicenseDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增飞机证照。
        /// </summary>
        /// <param name="aircraftLicense">飞机证照DTO。</param>
        [Insert(typeof(AircraftLicenseDTO))]
        public void InsertAircraftLicense(AircraftLicenseDTO aircraftLicense)
        {
            var newAircraftLicense = AircraftLicenseFactory.CreateAircraftLicense();
            AircraftLicenseFactory.SetAircraftLicense(newAircraftLicense, aircraftLicense.Name, aircraftLicense.Description, aircraftLicense.IssuedUnit,
                aircraftLicense.IssuedDate, aircraftLicense.ValidMonths, aircraftLicense.ExpireDate, aircraftLicense.State, aircraftLicense.FileName, aircraftLicense.LicenseFile);
            _aircraftLicenseRepository.Add(newAircraftLicense);
        }


        /// <summary>
        ///     更新飞机证照。
        /// </summary>
        /// <param name="aircraftLicense">飞机证照DTO。</param>
        [Update(typeof(AircraftLicenseDTO))]
        public void ModifyAircraftLicense(AircraftLicenseDTO aircraftLicense)
        {
            var updateAircraftLicense = _aircraftLicenseRepository.Get(aircraftLicense.AircraftLicenseId); //获取需要更新的对象。
            AircraftLicenseFactory.SetAircraftLicense(updateAircraftLicense, aircraftLicense.Name, aircraftLicense.Description, aircraftLicense.IssuedUnit,
                aircraftLicense.IssuedDate, aircraftLicense.ValidMonths, aircraftLicense.ExpireDate, aircraftLicense.State, aircraftLicense.FileName, aircraftLicense.LicenseFile);
            _aircraftLicenseRepository.Modify(updateAircraftLicense);
        }

        /// <summary>
        ///     删除飞机证照。
        /// </summary>
        /// <param name="aircraftLicense">飞机证照DTO。</param>
        [Delete(typeof(AircraftLicenseDTO))]
        public void DeleteAircraftLicense(AircraftLicenseDTO aircraftLicense)
        {
            var deleteAircraftLicense = _aircraftLicenseRepository.Get(aircraftLicense.AircraftLicenseId); //获取需要删除的对象。
            _aircraftLicenseRepository.Remove(deleteAircraftLicense); //删除飞机证照。
        }
        #endregion

        #region LicenseTypeDTO

        /// <summary>
        ///     获取所有证照类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<LicenseTypeDTO> GetLicenseTypes()
        {
            var queryBuilder = new QueryBuilder<LicenseType>();
            return _aircraftLicenseQuery.LicenseTypeDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增证照类型。
        /// </summary>
        /// <param name="licenseType">证照类型DTO。</param>
        [Insert(typeof(LicenseTypeDTO))]
        public void InsertLicenseType(LicenseTypeDTO licenseType)
        {
            var newLicenseType = LicenseTypeFactory.CreateLicenseType();
            LicenseTypeFactory.SetLicenseType(newLicenseType, licenseType.Name, licenseType.HasFile, licenseType.Description);
            _licenseTypeRepository.Add(newLicenseType);
        }


        /// <summary>
        ///     更新证照类型。
        /// </summary>
        /// <param name="licenseType">证照类型DTO。</param>
        [Update(typeof(LicenseTypeDTO))]
        public void ModifyLicenseType(LicenseTypeDTO licenseType)
        {
            var updateLicenseType = _licenseTypeRepository.Get(licenseType.LicenseTypeId); //获取需要更新的对象。
            LicenseTypeFactory.SetLicenseType(updateLicenseType, licenseType.Name, licenseType.HasFile, licenseType.Description);
            _licenseTypeRepository.Modify(updateLicenseType);
        }

        /// <summary>
        ///     删除证照类型。
        /// </summary>
        /// <param name="licenseType">证照类型DTO。</param>
        [Delete(typeof(LicenseTypeDTO))]
        public void DeleteLicenseType(LicenseTypeDTO licenseType)
        {
            var deleteLicenseType = _licenseTypeRepository.Get(licenseType.LicenseTypeId); //获取需要删除的对象。
            _licenseTypeRepository.Remove(deleteLicenseType); //删除证照类型。
        }
        #endregion
    }
}
