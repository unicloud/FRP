#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：huangqb 时间：2013/11/16 14:45:30
// 文件名：EnginePurchaseReceptionAppService
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
    ///     采购发动机接收项目服务实现
    /// </summary>
    public class EnginePurchaseReceptionAppService : IEnginePurchaseReceptionAppService
    {
        private readonly IEnginePurchaseReceptionQuery _enginePurchaseReceptionQuery;
        private readonly IReceptionRepository _receptionRepository;

        public EnginePurchaseReceptionAppService(IEnginePurchaseReceptionQuery enginePurchaseReceptionQuery,
            IReceptionRepository receptionRepository)
        {
            _enginePurchaseReceptionQuery = enginePurchaseReceptionQuery;
            _receptionRepository = receptionRepository;
        }

        #region EnginePurchaseReceptionDTO

        /// <summary>
        ///     获取所有采购发动机接收项目
        /// </summary>
        /// <returns></returns>
        public IQueryable<EnginePurchaseReceptionDTO> GetEnginePurchaseReceptions()
        {
            var queryBuilder =
                new QueryBuilder<EnginePurchaseReception>();
            return _enginePurchaseReceptionQuery.EnginePurchaseReceptionDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增采购发动机接收项目。
        /// </summary>
        /// <param name="enginePurchaseReception">采购发动机接收项目DTO。</param>
        [Insert(typeof (EnginePurchaseReceptionDTO))]
        public void InsertEnginePurchaseReception(EnginePurchaseReceptionDTO enginePurchaseReception)
        {
            var newEnginePurchaseReception = ReceptionFactory.CreateEnginePurchaseReception();
            _receptionRepository.Add(newEnginePurchaseReception);
        }

        /// <summary>
        ///     更新采购发动机接收项目。
        /// </summary>
        /// <param name="enginePurchaseReception">采购发动机接收项目DTO。</param>
        [Update(typeof (EnginePurchaseReceptionDTO))]
        public void ModifyEnginePurchaseReception(EnginePurchaseReceptionDTO enginePurchaseReception)
        {
            var updateEnginePurchaseReception =
                _receptionRepository.GetFiltered(t=>t.ReceptionNumber==enginePurchaseReception.ReceptionNumber).FirstOrDefault(); //获取需要更新的对象。

            //更新。 
            _receptionRepository.Modify(updateEnginePurchaseReception);
        }

        /// <summary>
        ///     删除采购发动机接收项目。
        /// </summary>
        /// <param name="enginePurchaseReception">采购发动机接收项目DTO。</param>
        [Delete(typeof (EnginePurchaseReceptionDTO))]
        public void DeleteEnginePurchaseReception(EnginePurchaseReceptionDTO enginePurchaseReception)
        {
            var newEnginePurchaseReception = _receptionRepository.GetFiltered(t => t.ReceptionNumber == enginePurchaseReception.ReceptionNumber).FirstOrDefault();
                //获取需要删除的对象。
            _receptionRepository.Remove(newEnginePurchaseReception); //删除采购发动机接收项目。
        }

        #endregion
    }
}