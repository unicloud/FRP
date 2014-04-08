#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/8 15:37:39
// 文件名：AircraftCabinTypeAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/8 15:37:39
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.BaseManagementBC.DTO;
using UniCloud.Application.BaseManagementBC.Query.AircraftCabinTypeQueries;
using UniCloud.Domain.BaseManagementBC.Aggregates.AircraftCabinTypeAgg;

#endregion

namespace UniCloud.Application.BaseManagementBC.AircraftCabinTypeServices
{
    /// <summary>
    ///     实现飞机舱位类型服务接口。
    ///     用于处理飞机舱位类型相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class AircraftCabinTypeAppService : ContextBoundObject, IAircraftCabinTypeAppService
    {
        private readonly IAircraftCabinTypeQuery _aircraftCabinTypeQuery;
        private readonly IAircraftCabinTypeRepository _aircraftCabinTypeRepository;

        public AircraftCabinTypeAppService(IAircraftCabinTypeQuery aircraftCabinTypeQuery,
            IAircraftCabinTypeRepository aircraftCabinTypeRepository)
        {
            _aircraftCabinTypeQuery = aircraftCabinTypeQuery;
            _aircraftCabinTypeRepository = aircraftCabinTypeRepository;
        }

        #region AircraftCabinTypeDTO

        /// <summary>
        ///     获取所有飞机舱位类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftCabinTypeDTO> GetAircraftCabinTypes()
        {
            var queryBuilder = new QueryBuilder<AircraftCabinType>();
            return _aircraftCabinTypeQuery.AircraftCabinTypeDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增飞机舱位类型。
        /// </summary>
        /// <param name="aircraftCabinType">飞机舱位类型DTO。</param>
        [Insert(typeof(AircraftCabinTypeDTO))]
        public void InsertAircraftCabinType(AircraftCabinTypeDTO aircraftCabinType)
        {
            var newAircraftCabinType = AircraftCabinTypeFactory.CreateAircraftCabinType();
            AircraftCabinTypeFactory.SetAircraftCabinType(newAircraftCabinType, aircraftCabinType.Name, aircraftCabinType.Note);

            _aircraftCabinTypeRepository.Add(newAircraftCabinType);
        }

        /// <summary>
        ///     更新飞机舱位类型。
        /// </summary>
        /// <param name="aircraftCabinType">飞机舱位类型DTO。</param>
        [Update(typeof(AircraftCabinTypeDTO))]
        public void ModifyAircraftCabinType(AircraftCabinTypeDTO aircraftCabinType)
        {
            var updateAircraftCabinType = _aircraftCabinTypeRepository.Get(aircraftCabinType.Id); //获取需要更新的对象。
            AircraftCabinTypeFactory.SetAircraftCabinType(updateAircraftCabinType, aircraftCabinType.Name, aircraftCabinType.Note);

            _aircraftCabinTypeRepository.Modify(updateAircraftCabinType);
        }

        /// <summary>
        ///     删除飞机舱位类型。
        /// </summary>
        /// <param name="aircraftCabinType">飞机舱位类型DTO。</param>
        [Delete(typeof(AircraftCabinTypeDTO))]
        public void DeleteAircraftCabinType(AircraftCabinTypeDTO aircraftCabinType)
        {
            var deleteAircraftCabinType = _aircraftCabinTypeRepository.Get(aircraftCabinType.Id); //获取需要删除的对象。
            _aircraftCabinTypeRepository.Remove(deleteAircraftCabinType); //删除飞机舱位类型。
        }
        #endregion
    }
}
