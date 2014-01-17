#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 12:06:16
// 文件名：AircraftAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 12:06:16
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.AircraftQueries;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftLicenseAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.AircraftServices
{
    /// <summary>
    ///     实现实际飞机接口。
    ///     用于处于实际飞机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftAppService : IAircraftAppService
    {
        private readonly IAircraftQuery _aircraftQuery;
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftAppService(IAircraftQuery aircraftQuery, IAircraftRepository aircraftRepository)
        {
            _aircraftQuery = aircraftQuery;
            _aircraftRepository = aircraftRepository;
        }

        #region AircraftDTO
        /// <summary>
        ///     获取所有实际飞机。
        /// </summary>
        /// <returns>所有实际飞机。</returns>
        public IQueryable<AircraftDTO> GetAircrafts()
        {
            var queryBuilder = new QueryBuilder<Aircraft>();
            return _aircraftQuery.AircraftDTOQuery(queryBuilder);
        }



        /// <summary>
        ///     更新实际飞机。
        /// </summary>
        /// <param name="dto">实际飞机DTO。</param>
        [Update(typeof(AircraftDTO))]
        public void ModifyAircraft(AircraftDTO dto)
        {
            var updateAircraft = _aircraftRepository.Get(dto.AircraftId); //获取需要更新的对象。
            UpdateAircraftLicenses(dto.AircraftLicenses, updateAircraft);
            _aircraftRepository.Modify(updateAircraft);
        }

        #region 更新飞机证照集合
        /// <summary>
        /// 更新飞机证照集合
        /// </summary>
        /// <param name="sourceAircraftLicenses">客户端集合</param>
        /// <param name="dstAircraft">数据库集合</param>
        private void UpdateAircraftLicenses(IEnumerable<AircraftLicenseDTO> sourceAircraftLicenses, Aircraft dstAircraft)
        {
            var aircraftLicense = new List<AircraftLicense>();
            foreach (var sourceAircraftLicense in sourceAircraftLicenses)
            {
                var result = dstAircraft.Licenses.FirstOrDefault(p => p.Id == sourceAircraftLicense.AircraftLicenseId);
                if (result == null)
                {
                    result = AircraftLicenseFactory.CreateAircraftLicense();
                    result.ChangeCurrentIdentity(sourceAircraftLicense.AircraftLicenseId);
                }
                AircraftLicenseFactory.SetAircraftLicense(result, sourceAircraftLicense.Name, sourceAircraftLicense.Description, sourceAircraftLicense.IssuedUnit,
                        sourceAircraftLicense.IssuedDate, sourceAircraftLicense.ValidMonths, sourceAircraftLicense.ExpireDate, sourceAircraftLicense.State, sourceAircraftLicense.FileName, sourceAircraftLicense.DocumentId);
                aircraftLicense.Add(result);
            }
            dstAircraft.Licenses.ToList().ForEach(p =>
            {
                if (aircraftLicense.FirstOrDefault(t => t.Id == p.Id) == null)
                {
                    _aircraftRepository.RemoveAircraftLicense(p);
                }
            });
            dstAircraft.Licenses = aircraftLicense;
        }
        #endregion
        #endregion
    }
}
