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

using System;
using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.AircraftQueries;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftAgg;
using UniCloud.Domain.Common.Enums;

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

        public AircraftAppService(IAircraftQuery aircraftQuery,IAircraftRepository aircraftRepository)
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
          
        }


        #endregion
    }
}
