#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AircraftAppService
// 版本：V1.0.0
//
// 修改者： 时间：
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.AircraftQueries;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;
#endregion

namespace UniCloud.Application.PartBC.AircraftServices
{
    /// <summary>
    /// 实现Aircraft的服务接口。
    ///  用于处理Aircraft相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftAppService : IAircraftAppService
    {
        private readonly IAircraftQuery _aircraftQuery;

        public AircraftAppService(IAircraftQuery aircraftQuery)
        {
            _aircraftQuery = aircraftQuery;
        }

        #region AircraftDTO

        /// <summary>
        /// 获取所有Aircraft。
        /// </summary>
        public IQueryable<AircraftDTO> GetAircrafts()
        {
            var queryBuilder =
               new QueryBuilder<Aircraft>();
            return _aircraftQuery.AircraftDTOQuery(queryBuilder);
        }

        #endregion

    }
}
