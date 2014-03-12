#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/12 14:23:19
// 文件名：AircraftConfigurationAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/12 14:23:19
// 修改说明：
// ========================================================================*/
#endregion

using System.Linq;
using UniCloud.Application.AircraftConfigBC.DTO;
using UniCloud.Application.AircraftConfigBC.Query.AircraftConfigurationQueries;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Domain.AircraftConfigBC.Aggregates.AircraftConfigurationAgg;

namespace UniCloud.Application.AircraftConfigBC.AircraftConfigurationServices
{
    /// <summary>
    ///     实现飞机配置服务接口。
    ///     用于处理飞机配置相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftConfigurationAppService : IAircraftConfigurationAppService
    {
        private readonly IAircraftConfigurationQuery _aircraftConfigurationQuery;
        private readonly IAircraftConfigurationRepository _aircraftConfigurationRepository;

        public AircraftConfigurationAppService(IAircraftConfigurationQuery aircraftConfigurationQuery, IAircraftConfigurationRepository aircraftConfigurationRepository)
        {
            _aircraftConfigurationQuery = aircraftConfigurationQuery;
            _aircraftConfigurationRepository = aircraftConfigurationRepository;
        }

        #region AircraftConfigurationDTO

        /// <summary>
        ///     获取所有飞机配置类型
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftConfigurationDTO> GetAircraftConfigurations()
        {
            var queryBuilder = new QueryBuilder<AircraftConfiguration>();
            return _aircraftConfigurationQuery.AircraftConfigurationDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增飞机配置类型。
        /// </summary>
        /// <param name="aircraftConfiguration">飞机配置类型DTO。</param>
        [Insert(typeof(AircraftConfigurationDTO))]
        public void InsertAircraftConfiguration(AircraftConfigurationDTO aircraftConfiguration)
        {
            var newAircraftConfiguration = AircraftConfigurationFactory.CreateAircraftConfiguration();
            AircraftConfigurationFactory.SetAircraftConfiguration(newAircraftConfiguration, aircraftConfiguration.ConfigCode, aircraftConfiguration.AircraftSeriesId, aircraftConfiguration.AircraftTypeId, aircraftConfiguration.BEW,
                aircraftConfiguration.BW, aircraftConfiguration.BWF, aircraftConfiguration.BWI, aircraftConfiguration.Description, aircraftConfiguration.MCC, aircraftConfiguration.MLW, aircraftConfiguration.MMFW, aircraftConfiguration.MTOW,
                aircraftConfiguration.MTW, aircraftConfiguration.MZFW, aircraftConfiguration.FileName, aircraftConfiguration.FileContent);

            aircraftConfiguration.AircraftCabins.ToList().ForEach(aircraftCabin => InsertAircraftCabin(newAircraftConfiguration, aircraftCabin));
            _aircraftConfigurationRepository.Add(newAircraftConfiguration);
        }

        /// <summary>
        ///     更新飞机配置类型。
        /// </summary>
        /// <param name="aircraftConfiguration">飞机配置类型DTO。</param>
        [Update(typeof(AircraftConfigurationDTO))]
        public void ModifyAircraftConfiguration(AircraftConfigurationDTO aircraftConfiguration)
        {
            var updateAircraftConfiguration = _aircraftConfigurationRepository.Get(aircraftConfiguration.Id); //获取需要更新的对象。
            AircraftConfigurationFactory.SetAircraftConfiguration(updateAircraftConfiguration, aircraftConfiguration.ConfigCode, aircraftConfiguration.AircraftSeriesId, aircraftConfiguration.AircraftTypeId, aircraftConfiguration.BEW,
                aircraftConfiguration.BW, aircraftConfiguration.BWF, aircraftConfiguration.BWI, aircraftConfiguration.Description, aircraftConfiguration.MCC, aircraftConfiguration.MLW, aircraftConfiguration.MMFW, aircraftConfiguration.MTOW,
                aircraftConfiguration.MTW, aircraftConfiguration.MZFW, aircraftConfiguration.FileName, aircraftConfiguration.FileContent);

            var dtoAircraftCabins = aircraftConfiguration.AircraftCabins;
            var aircraftCabins = updateAircraftConfiguration.AircraftCabins;
            DataHelper.DetailHandle(dtoAircraftCabins.ToArray(),
                aircraftCabins.ToArray(),
                c => c.Id, p => p.Id,
                i => InsertAircraftCabin(updateAircraftConfiguration, i),
                UpdateAircraftCabin,
                d => _aircraftConfigurationRepository.DeleteAircraftCabin(d));
            _aircraftConfigurationRepository.Modify(updateAircraftConfiguration);
        }

        /// <summary>
        ///     删除飞机配置类型。
        /// </summary>
        /// <param name="aircraftConfiguration">飞机配置类型DTO。</param>
        [Delete(typeof(AircraftConfigurationDTO))]
        public void DeleteAircraftConfiguration(AircraftConfigurationDTO aircraftConfiguration)
        {
            var deleteAircraftConfiguration = _aircraftConfigurationRepository.Get(aircraftConfiguration.Id); //获取需要删除的对象。
            _aircraftConfigurationRepository.DeleteAircraftConfiguration(deleteAircraftConfiguration); //删除飞机配置类型。
        }

        /// <summary>
        ///     插入舱位
        /// </summary>
        /// <param name="aircraftConfiguration">飞机配置</param>
        /// <param name="aircraftCabinDto">舱位DTO</param>
        private void InsertAircraftCabin(AircraftConfiguration aircraftConfiguration, AircraftCabinDTO aircraftCabinDto)
        {
            // 添加舱位
            var aircraftCabin = aircraftConfiguration.AddNewAircraftCabin();
            AircraftConfigurationFactory.SetAircraftCabin(aircraftCabin, aircraftCabinDto.AircraftCabinType, aircraftCabinDto.SeatNumber, aircraftCabinDto.Note);
        }

        /// <summary>
        ///     更新舱位
        /// </summary>
        /// <param name="aircraftCabinDto">舱位DTO</param>
        /// <param name="aircraftCabin">舱位</param>
        private void UpdateAircraftCabin(AircraftCabinDTO aircraftCabinDto, AircraftCabin aircraftCabin)
        {
            // 更新舱位
            AircraftConfigurationFactory.SetAircraftCabin(aircraftCabin, aircraftCabinDto.AircraftCabinType, aircraftCabinDto.SeatNumber, aircraftCabinDto.Note);
        }
        #endregion
    }
}
