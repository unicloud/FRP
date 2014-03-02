#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/18 9:25:47

// 文件名：AcDailyUtilizationAppService
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
using UniCloud.Application.PartBC.Query.AcDailyUtilizationQueries;
using UniCloud.Domain.PartBC.Aggregates.AcDailyUtilizationAgg;
using UniCloud.Domain.PartBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Application.PartBC.AcDailyUtilizationServices
{
    /// <summary>
    /// 实现AcDailyUtilization的服务接口。
    ///  用于处理AcDailyUtilization相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AcDailyUtilizationAppService : IAcDailyUtilizationAppService
    {
        private readonly IAcDailyUtilizationQuery _acDailyUtilizationQuery;
        private readonly IAcDailyUtilizationRepository _acDailyUtilizationRepository;
        private readonly IAircraftRepository _aircraftRepository;
        public AcDailyUtilizationAppService(IAcDailyUtilizationQuery acDailyUtilizationQuery,
            IAcDailyUtilizationRepository acDailyUtilizationRepository,
            IAircraftRepository aircraftRepository)
        {
            _acDailyUtilizationQuery = acDailyUtilizationQuery;
            _acDailyUtilizationRepository = acDailyUtilizationRepository;
            _aircraftRepository = aircraftRepository;
        }

        #region AcDailyUtilizationDTO

        /// <summary>
        /// 获取所有AcDailyUtilization。
        /// </summary>
        public IQueryable<AcDailyUtilizationDTO> GetAcDailyUtilizations()
        {
            var queryBuilder =
               new QueryBuilder<AcDailyUtilization>();
            return _acDailyUtilizationQuery.AcDailyUtilizationDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增AcDailyUtilization。
        /// </summary>
        /// <param name="dto">AcDailyUtilizationDTO。</param>
        [Insert(typeof(AcDailyUtilizationDTO))]
        public void InsertAcDailyUtilization(AcDailyUtilizationDTO dto)
        {
            //获取
            var aircraft = _aircraftRepository.Get(dto.AircraftId);

            var newAcDailyUtilization = AcDailyUtilizationFactory.CreateAcDailyUtilization(aircraft,dto.CalculatedHour,dto.CalculatedCycle,
                dto.Year,dto.Month);
            newAcDailyUtilization.SetIsCurrent(dto.IsCurrent);
            _acDailyUtilizationRepository.Add(newAcDailyUtilization);
        }

        /// <summary>
        ///  更新AcDailyUtilization。
        /// </summary>
        /// <param name="dto">AcDailyUtilizationDTO。</param>
        [Update(typeof(AcDailyUtilizationDTO))]
        public void ModifyAcDailyUtilization(AcDailyUtilizationDTO dto)
        {           
            //获取
            var aircraft = _aircraftRepository.Get(dto.AircraftId);

            var updateAcDailyUtilization = _acDailyUtilizationRepository.Get(dto.Id); //获取需要更新的对象。

            //更新。
            var newAcDailyUtilization = AcDailyUtilizationFactory.UpdateAcDailyUtilization(aircraft, dto.CalculatedHour,
                dto.CalculatedCycle,
                dto.AmendHour, dto.AmendCycle, dto.Year, dto.Month);          
            
            newAcDailyUtilization.ChangeCurrentIdentity(updateAcDailyUtilization.Id);

            _acDailyUtilizationRepository.Merge(updateAcDailyUtilization,newAcDailyUtilization);
        }

        /// <summary>
        ///  删除AcDailyUtilization。
        /// </summary>
        /// <param name="dto">AcDailyUtilizationDTO。</param>
        [Delete(typeof(AcDailyUtilizationDTO))]
        public void DeleteAcDailyUtilization(AcDailyUtilizationDTO dto)
        {
            var delAcDailyUtilization = _acDailyUtilizationRepository.Get(dto.Id); //获取需要删除的对象。
            _acDailyUtilizationRepository.Remove(delAcDailyUtilization); //删除AcDailyUtilization。
        }

        #endregion

    }
}
