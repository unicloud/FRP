#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:44:51
// 文件名：AircraftPurchaseReceptionAppService
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;

#endregion

namespace UniCloud.Application.PurchaseBC.ReceptionServices
{
    /// <summary>
    ///     采购飞机接收项目服务实现
    /// </summary>
    public class AircraftPurchaseReceptionAppService : IAircraftPurchaseReceptionAppService
    {
        private readonly IAircraftPurchaseReceptionQuery _aircraftPurchaseReceptionQuery;
        private readonly IReceptionRepository _receptionRepository;

        public AircraftPurchaseReceptionAppService(IAircraftPurchaseReceptionQuery aircraftPurchaseReceptionQuery,
            IReceptionRepository receptionRepository)
        {
            _aircraftPurchaseReceptionQuery = aircraftPurchaseReceptionQuery;
            _receptionRepository = receptionRepository;
        }

        #region AircraftPurchaseReceptionDTO

        /// <summary>
        ///     获取所有采购飞机接收项目
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftPurchaseReceptionDTO> GetAircraftPurchaseReceptions()
        {
            var queryBuilder =
                new QueryBuilder<AircraftPurchaseReception>();
            return _aircraftPurchaseReceptionQuery.AircraftPurchaseReceptionDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增采购飞机接收项目。
        /// </summary>
        /// <param name="aircraftPurchaseReception">采购飞机接收项目DTO。</param>
        [Insert(typeof (AircraftPurchaseReceptionDTO))]
        public void InsertAircraftPurchaseReception(AircraftPurchaseReceptionDTO aircraftPurchaseReception)
        {
            var newAircraftPurchaseReception = ReceptionFactory.CreateAircraftLeaseReception(DateTime.Now);
            _receptionRepository.Add(newAircraftPurchaseReception);
        }

        /// <summary>
        ///     更新采购飞机接收项目。
        /// </summary>
        /// <param name="aircraftPurchaseReception">采购飞机接收项目DTO。</param>
        [Update(typeof (AircraftPurchaseReceptionDTO))]
        public void ModifyAircraftPurchaseReception(AircraftPurchaseReceptionDTO aircraftPurchaseReception)
        {
            var updateAircraftPurchaseReception =
                _receptionRepository.GetFiltered(t=>t.ReceptionNumber==aircraftPurchaseReception.ReceptionNumber).FirstOrDefault(); //获取需要更新的对象。

            //更新。 
            _receptionRepository.Modify(updateAircraftPurchaseReception);
        }

        /// <summary>
        ///     删除采购飞机接收项目。
        /// </summary>
        /// <param name="aircraftPurchaseReception">采购飞机接收项目DTO。</param>
        [Delete(typeof (AircraftPurchaseReceptionDTO))]
        public void DeleteAircraftPurchaseReception(AircraftPurchaseReceptionDTO aircraftPurchaseReception)
        {
            var newAircraftPurchaseReception =
                _receptionRepository.GetFiltered(t => t.ReceptionNumber == aircraftPurchaseReception.ReceptionNumber).FirstOrDefault(); //获取需要删除的对象。
            _receptionRepository.Remove(newAircraftPurchaseReception); //删除采购飞机接收项目。
        }

        #endregion
    }
}