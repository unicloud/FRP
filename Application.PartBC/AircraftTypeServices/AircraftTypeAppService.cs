#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/17 11:42:36

// 文件名：AircraftTypeAppService
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
using UniCloud.Application.PartBC.Query.AircraftTypeQueries;
using UniCloud.Domain.PartBC.Aggregates.AircraftTypeAgg;
#endregion

namespace UniCloud.Application.PartBC.AircraftTypeServices
{
    /// <summary>
    /// 实现AircraftType的服务接口。
    ///  用于处理AircraftType相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftTypeAppService : IAircraftTypeAppService
    {
        private readonly IAircraftTypeQuery _aircraftTypeQuery;

        public AircraftTypeAppService(IAircraftTypeQuery aircraftTypeQuery)
        {
            _aircraftTypeQuery = aircraftTypeQuery;
        }

        #region AircraftTypeDTO

        /// <summary>
        /// 获取所有AircraftType。
        /// </summary>
        public IQueryable<AircraftTypeDTO> GetAircraftTypes()
        {
            var queryBuilder =
               new QueryBuilder<AircraftType>();
            return _aircraftTypeQuery.AircraftTypeDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增AircraftType。
        /// </summary>
        /// <param name="dto">AircraftTypeDTO。</param>
        [Insert(typeof(AircraftTypeDTO))]
        public void InsertAircraftType(AircraftTypeDTO dto)
        {
        }

        /// <summary>
        ///  更新AircraftType。
        /// </summary>
        /// <param name="dto">AircraftTypeDTO。</param>
        [Update(typeof(AircraftTypeDTO))]
        public void ModifyAircraftType(AircraftTypeDTO dto)
        {
        }

        /// <summary>
        ///  删除AircraftType。
        /// </summary>
        /// <param name="dto">AircraftTypeDTO。</param>
        [Delete(typeof(AircraftTypeDTO))]
        public void DeleteAircraftType(AircraftTypeDTO dto)
        {
        }

        #endregion

    }
}
