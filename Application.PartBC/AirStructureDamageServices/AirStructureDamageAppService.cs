#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 14:29:43
// 文件名：AirStructureDamageAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 14:29:43
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PartBC.DTO;
using UniCloud.Application.PartBC.Query.AirStructureDamageQueries;
using UniCloud.Domain.PartBC.Aggregates.AirStructureDamageAgg;

#endregion

namespace UniCloud.Application.PartBC.AirStructureDamageServices
{
    /// <summary>
    /// 实现AirStructureDamage的服务接口。
    ///  用于处理AirStructureDamage相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class AirStructureDamageAppService : ContextBoundObject, IAirStructureDamageAppService
    {
        private readonly IAirStructureDamageQuery _airStructureDamageQuery;
        private readonly IAirStructureDamageRepository _airStructureDamageRepository;

        public AirStructureDamageAppService(IAirStructureDamageQuery airStructureDamageQuery, IAirStructureDamageRepository airStructureDamageRepository)
        {
            _airStructureDamageQuery = airStructureDamageQuery;
            _airStructureDamageRepository = airStructureDamageRepository;
        }

        #region AirStructureDamageDTO

        /// <summary>
        /// 获取所有AirStructureDamage。
        /// </summary>
        public IQueryable<AirStructureDamageDTO> GetAirStructureDamages()
        {
            var queryBuilder = new QueryBuilder<AirStructureDamage>();
            return _airStructureDamageQuery.AirStructureDamageDTOQuery(queryBuilder);
        }

        /// <summary>
        ///  新增AirStructureDamage。
        /// </summary>
        /// <param name="dto">AirStructureDamageDTO。</param>
        [Insert(typeof(AirStructureDamageDTO))]
        public void InsertAirStructureDamage(AirStructureDamageDTO dto)
        {
            //创建AirStructureDamage
            var newAirStructureDamage = AirStructureDamageFactory.CreateAirStructureDamage();
            AirStructureDamageFactory.SetAirStructureDamage(newAirStructureDamage, dto.AircraftId, dto.AircraftReg, dto.AircraftType, dto.AircraftSeries,
                dto.Source, dto.ReportNo, dto.ReportType, dto.Description, dto.ReportDate, dto.CloseDate, dto.RepairDeadline, dto.Status, dto.Level,
                dto.IsDefer, dto.TotalCost, dto.TecAssess, dto.TreatResult, dto.DocumentId, dto.DocumentName);

            _airStructureDamageRepository.Add(newAirStructureDamage);
        }

        /// <summary>
        ///  更新AirStructureDamage。
        /// </summary>
        /// <param name="dto">AirStructureDamageDTO。</param>
        [Update(typeof(AirStructureDamageDTO))]
        public void ModifyAirStructureDamage(AirStructureDamageDTO dto)
        {
            //获取需要更新的对象
            var updateAirStructureDamage = _airStructureDamageRepository.Get(dto.Id);

            if (updateAirStructureDamage != null)
            {
                //更新主表：
                AirStructureDamageFactory.SetAirStructureDamage(updateAirStructureDamage, dto.AircraftId, dto.AircraftReg, dto.AircraftType, dto.AircraftSeries,
             dto.Source, dto.ReportNo, dto.ReportType, dto.Description, dto.ReportDate, dto.CloseDate, dto.RepairDeadline, dto.Status, dto.Level,
             dto.IsDefer, dto.TotalCost, dto.TecAssess, dto.TreatResult, dto.DocumentId, dto.DocumentName);
            }
            _airStructureDamageRepository.Modify(updateAirStructureDamage);
        }

        /// <summary>
        ///  删除AirStructureDamage。
        /// </summary>
        /// <param name="dto">AirStructureDamageDTO。</param>
        [Delete(typeof(AirStructureDamageDTO))]
        public void DeleteAirStructureDamage(AirStructureDamageDTO dto)
        {
            if (dto == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delAirStructureDamage = _airStructureDamageRepository.Get(dto.Id);
            //获取需要删除的对象。

            if (delAirStructureDamage != null)
            {
                _airStructureDamageRepository.Remove(delAirStructureDamage); //删除AirStructureDamage。
            }
        }
        #endregion
    }
}
