#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQibin 时间：2014/01/04，11:01
// 文件名：AircraftSeriesAppService.cs
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
using UniCloud.Application.AircraftConfigBC.Query.AircraftSeriesQueries;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftSeriesAgg;

#endregion

namespace UniCloud.Application.AircraftConfigBC.AircraftSeriesServices
{
    /// <summary>
    ///     实现飞机系列服务接口。
    ///     用于处理飞机系列相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftSeriesAppService : IAircraftSeriesAppService
    {
        private readonly IAircraftSeriesQuery _aircraftSeriesQuery;
        private readonly IAircraftSeriesRepository _aircraftSeriesRepository;

        public AircraftSeriesAppService(IAircraftSeriesQuery aircraftSeriesQuery, IAircraftSeriesRepository aircraftSeriesRepository)
        {
            _aircraftSeriesQuery = aircraftSeriesQuery;
            _aircraftSeriesRepository = aircraftSeriesRepository;
        }

        #region AircraftSeriesDTO

        /// <summary>
        ///     获取所有飞机系列
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftSeriesDTO> GetAircraftSeries()
        {
            var queryBuilder =
                new QueryBuilder<AircraftSeries>();
            return _aircraftSeriesQuery.AircraftSeriesDTOQuery(queryBuilder);
        }

         /// <summary>
        ///     新增飞机系列。
        /// </summary>
        /// <param name="aircraftSeries">飞机系列DTO。</param>
        [Insert(typeof(AircraftSeriesDTO))]
        public void InsertAircraftSeries(AircraftSeriesDTO aircraftSeries)
        {
            var newAircraftSeries = AircraftSeriesFactory.CreateAircraftSeries();
            AircraftSeriesFactory.SetAircraftSeries(newAircraftSeries, aircraftSeries.Name, aircraftSeries.Description, aircraftSeries.ManufacturerId);
            _aircraftSeriesRepository.Add(newAircraftSeries);
        }


        /// <summary>
        ///     更新飞机系列。
        /// </summary>
        /// <param name="aircraftSeries">飞机系列DTO。</param>
        [Update(typeof(AircraftSeriesDTO))]
        public void ModifyAircraftSeries(AircraftSeriesDTO aircraftSeries)
        {
            var updateAircraftSeries = _aircraftSeriesRepository.Get(aircraftSeries.Id); //获取需要更新的对象。
            AircraftSeriesFactory.SetAircraftSeries(updateAircraftSeries, aircraftSeries.Name, aircraftSeries.Description, aircraftSeries.ManufacturerId);
            _aircraftSeriesRepository.Modify(updateAircraftSeries);
        }

        /// <summary>
        ///     删除飞机系列。
        /// </summary>
        /// <param name="aircraftSeries">飞机系列DTO。</param>
        [Delete(typeof(AircraftSeriesDTO))]
        public void DeleteAircraftSeries(AircraftSeriesDTO aircraftSeries)
        {
            var deleteAircraftSeries = _aircraftSeriesRepository.Get(aircraftSeries.Id); //获取需要删除的对象。
            _aircraftSeriesRepository.Remove(deleteAircraftSeries); //删除飞机系列。
        }
        #endregion
    }
}
