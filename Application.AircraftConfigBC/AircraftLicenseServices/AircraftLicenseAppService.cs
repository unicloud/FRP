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

using System;
using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.AircraftLicenseQueries;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Domain.AircraftConfigBC.Aggregates.LicenseTypeAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.AircraftLicenseServices
{
    /// <summary>
    ///     实现飞机证照服务接口。
    ///     用于处理飞机证照相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class AircraftLicenseAppService : ContextBoundObject, IAircraftLicenseAppService
    {
        private readonly IAircraftLicenseQuery _aircraftLicenseQuery;
        private readonly ILicenseTypeRepository _licenseTypeRepository;

        public AircraftLicenseAppService(IAircraftLicenseQuery aircraftLicenseQuery, ILicenseTypeRepository licenseTypeRepository)
        {
            _aircraftLicenseQuery = aircraftLicenseQuery;
            _licenseTypeRepository = licenseTypeRepository;
        }

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
