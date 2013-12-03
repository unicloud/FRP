#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:40:26
// 文件名：AircraftLeaseReceptionAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.ReceptionServices
{
    /// <summary>
    ///     租赁飞机接收项目服务实现
    /// </summary>
    public class AircraftLeaseReceptionAppService : IAircraftLeaseReceptionAppService
    {
        private readonly IAircraftLeaseReceptionQuery _aircraftLeaseReceptionQuery;
        private readonly IReceptionRepository _receptionRepository;

        public AircraftLeaseReceptionAppService(IAircraftLeaseReceptionQuery aircraftLeaseReceptionQuery,
            IReceptionRepository receptionRepository)
        {
            _aircraftLeaseReceptionQuery = aircraftLeaseReceptionQuery;
            _receptionRepository = receptionRepository;
        }

        #region AircraftLeaseReceptionDTO

        /// <summary>
        ///     获取所有租赁飞机接收项目
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftLeaseReceptionDTO> GetAircraftLeaseReceptions()
        {
            var queryBuilder =
                new QueryBuilder<AircraftLeaseReception>();
            return _aircraftLeaseReceptionQuery.AircraftLeaseReceptionDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增租赁飞机接收项目。
        /// </summary>
        /// <param name="aircraftLeaseReception">租赁飞机接收项目DTO。</param>
        [Insert(typeof(AircraftLeaseReceptionDTO))]
        public void InsertAircraftLeaseReception(AircraftLeaseReceptionDTO aircraftLeaseReception)
        {
            var newAircraftLeaseReception = ReceptionFactory.CreateAircraftLeaseReception(DateTime.Now);
            _receptionRepository.Add(newAircraftLeaseReception);
        }

        /// <summary>
        ///     更新租赁飞机接收项目。
        /// </summary>
        /// <param name="aircraftLeaseReception">租赁飞机接收项目DTO。</param>
        [Update(typeof(AircraftLeaseReceptionDTO))]
        public void ModifyAircraftLeaseReception(AircraftLeaseReceptionDTO aircraftLeaseReception)
        {

            var updateAircraftLeaseReception = _receptionRepository.GetFiltered(t => t.ReceptionNumber == aircraftLeaseReception.ReceptionNumber).FirstOrDefault();
            //获取需要更新的对象。
            if (updateAircraftLeaseReception != null)
            {
                updateAircraftLeaseReception.StartDate = aircraftLeaseReception.StartDate;
                updateAircraftLeaseReception.EndDate = aircraftLeaseReception.EndDate;
                //更新主表。 

                //更新从表。
            }
            _receptionRepository.Modify(updateAircraftLeaseReception);
        }

        /// <summary>
        ///     删除租赁飞机接收项目。
        /// </summary>
        /// <param name="aircraftLeaseReception">租赁飞机接收项目DTO。</param>
        [Delete(typeof(AircraftLeaseReceptionDTO))]
        public void DeleteAircraftLeaseReception(AircraftLeaseReceptionDTO aircraftLeaseReception)
        {
            var newAircraftLeaseReception = _receptionRepository.GetFiltered(t => t.ReceptionNumber == aircraftLeaseReception.ReceptionNumber).FirstOrDefault();
            //获取需要删除的对象。
            _receptionRepository.Remove(newAircraftLeaseReception); //删除租赁飞机接收项目。
        }

        #endregion
    }
}