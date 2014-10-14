#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2013/12/30，15:12
// 文件名：AircraftTypeAppService.cs
// 程序集：UniCloud.Application.FleetPlanBC
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.AircraftTypeQueries;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftTypeAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.AircraftTypeServices
{
    /// <summary>
    ///     实现机型服务接口。
    ///     用于处理机型相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftTypeAppService : IAircraftTypeAppService
    {
        private readonly IAircraftTypeQuery _aircraftTypeQuery;
        private readonly IAircraftTypeRepository _aircraftTypeRepository;
        public AircraftTypeAppService(IAircraftTypeQuery aircraftTypeQuery, IAircraftTypeRepository aircraftTypeRepository)
        {
            _aircraftTypeQuery = aircraftTypeQuery;
            _aircraftTypeRepository = aircraftTypeRepository;
        }

        #region AircraftTypeDTO

        /// <summary>
        ///     获取所有机型
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftTypeDTO> GetAircraftTypes()
        {
            var queryBuilder =
                new QueryBuilder<AircraftType>();
            return _aircraftTypeQuery.AircraftTypeDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增飞机机型。
        /// </summary>
        /// <param name="aircraftType">飞机机型DTO。</param>
        [Insert(typeof(AircraftTypeDTO))]
        public void InsertAircraftType(AircraftTypeDTO aircraftType)
        {
            var newAircraftType = AircraftTypeFactory.CreateAircraftType();
            AircraftTypeFactory.SetAircraftType(newAircraftType, aircraftType.Name, aircraftType.Description,aircraftType.AircraftCategoryId,aircraftType.AircraftSeriesId, aircraftType.ManufacturerId);
            _aircraftTypeRepository.Add(newAircraftType);
        }


        /// <summary>
        ///     更新飞机机型。
        /// </summary>
        /// <param name="aircraftType">飞机机型DTO。</param>
        [Update(typeof(AircraftTypeDTO))]
        public void ModifyAircraftType(AircraftTypeDTO aircraftType)
        {
            var updateAircraftType = _aircraftTypeRepository.Get(aircraftType.AircraftTypeId); //获取需要更新的对象。
            AircraftTypeFactory.SetAircraftType(updateAircraftType, aircraftType.Name, aircraftType.Description, aircraftType.AircraftCategoryId, aircraftType.AircraftSeriesId, aircraftType.ManufacturerId);
            _aircraftTypeRepository.Modify(updateAircraftType);
        }

        /// <summary>
        ///     删除飞机机型。
        /// </summary>
        /// <param name="aircraftType">飞机机型DTO。</param>
        [Delete(typeof(AircraftTypeDTO))]
        public void DeleteAircraftType(AircraftTypeDTO aircraftType)
        {
            var deleteAircraftType = _aircraftTypeRepository.Get(aircraftType.AircraftTypeId); //获取需要删除的对象。
            _aircraftTypeRepository.Remove(deleteAircraftType); //删除飞机机型。
        }
        #endregion
    }
}
