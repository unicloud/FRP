#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:45:54
// 文件名：EngineLeaseReceptionAppService
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
    ///     租赁发动机接收项目服务实现
    /// </summary>
    public class EngineLeaseReceptionAppService : IEngineLeaseReceptionAppService
    {
        private readonly IEngineLeaseReceptionQuery _engineLeaseReceptionQuery;
        private readonly IReceptionRepository _receptionRepository;

        public EngineLeaseReceptionAppService(IEngineLeaseReceptionQuery engineLeaseReceptionQuery,
            IReceptionRepository receptionRepository)
        {
            _engineLeaseReceptionQuery = engineLeaseReceptionQuery;
            _receptionRepository = receptionRepository;
        }

        #region EngineLeaseReceptionDTO

        /// <summary>
        ///     获取所有租赁发动机接收项目
        /// </summary>
        /// <returns></returns>
        public IQueryable<EngineLeaseReceptionDTO> GetEngineLeaseReceptions()
        {
            var queryBuilder =
                new QueryBuilder<EngineLeaseReception>();
            return _engineLeaseReceptionQuery.EngineLeaseReceptionDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增租赁发动机接收项目。
        /// </summary>
        /// <param name="engineLeaseReception">租赁发动机接收项目DTO。</param>
        [Insert(typeof (EngineLeaseReceptionDTO))]
        public void InsertEngineLeaseReception(EngineLeaseReceptionDTO engineLeaseReception)
        {
            var newEngineLeaseReception = ReceptionFactory.CreateEngineLeaseReception();

            _receptionRepository.Add(newEngineLeaseReception);
        }

        /// <summary>
        ///     更新租赁发动机接收项目。
        /// </summary>
        /// <param name="engineLeaseReception">租赁发动机接收项目DTO。</param>
        [Update(typeof (EngineLeaseReceptionDTO))]
        public void ModifyEngineLeaseReception(EngineLeaseReceptionDTO engineLeaseReception)
        {
            var updateEngineLeaseReception = _receptionRepository.GetFiltered(t => t.ReceptionNumber == engineLeaseReception.ReceptionNumber).FirstOrDefault(); //获取需要更新的对象。
                //获取需要更新的对象。

            //更新。 
            _receptionRepository.Modify(updateEngineLeaseReception);
        }

        /// <summary>
        ///     删除租赁发动机接收项目。
        /// </summary>
        /// <param name="engineLeaseReception">租赁发动机接收项目DTO。</param>
        [Delete(typeof (EngineLeaseReceptionDTO))]
        public void DeleteEngineLeaseReception(EngineLeaseReceptionDTO engineLeaseReception)
        {
            var newEngineLeaseReception = _receptionRepository.GetFiltered(t => t.ReceptionNumber == engineLeaseReception.ReceptionNumber).FirstOrDefault(); //获取需要删除的对象。
                //获取需要删除的对象。
            _receptionRepository.Remove(newEngineLeaseReception); //删除租赁发动机接收项目。
        }

        #endregion

    }
}