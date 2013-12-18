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
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractEngineAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;

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
        private readonly ISupplierRepository _supplierRepository;
        private readonly IContractEngineRepository _contractEngineRepository;

        public EnginePurchaseReceptionAppService(IEnginePurchaseReceptionQuery enginePurchaseReceptionQuery,
            IReceptionRepository receptionRepository,
            ISupplierRepository supplierRepository,
            IContractEngineRepository contractEngineRepository)
        {
            _enginePurchaseReceptionQuery = enginePurchaseReceptionQuery;
            _receptionRepository = receptionRepository;
            _supplierRepository = supplierRepository;
            _contractEngineRepository = contractEngineRepository;
        }

        #region EnginePurchaseReceptionDTO

        /// <summary>
        ///     获取所有购买发动机接收项目
        /// </summary>
        /// <returns></returns>
        public IQueryable<EnginePurchaseReceptionDTO> GetEnginePurchaseReceptions()
        {
            var queryBuilder =
                new QueryBuilder<EnginePurchaseReception>();
            return _enginePurchaseReceptionQuery.EnginePurchaseReceptionDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增购买发动机接收项目。
        /// </summary>
        /// <param name="enginePurchaseReception">购买发动机接收项目DTO。</param>
        [Insert(typeof(EnginePurchaseReceptionDTO))]
        public void InsertEnginePurchaseReception(EnginePurchaseReceptionDTO enginePurchaseReception)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.SupplierCompanyId == enginePurchaseReception.SupplierId).FirstOrDefault();

            var newEnginePurchaseReception = ReceptionFactory.CreateEnginePurchaseReception();
            var date = DateTime.Now.Date;
            var seq = _receptionRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            newEnginePurchaseReception.SetReceptionNumber(seq);
            newEnginePurchaseReception.Description = enginePurchaseReception.Description;
            newEnginePurchaseReception.StartDate = enginePurchaseReception.StartDate;
            newEnginePurchaseReception.SetStatus(0);
            newEnginePurchaseReception.EndDate = enginePurchaseReception.EndDate;
            newEnginePurchaseReception.SetSupplier(supplier);
            newEnginePurchaseReception.SourceId = enginePurchaseReception.SourceId;
            if (enginePurchaseReception.ReceptionLines != null)
            {
                foreach (var receptionLine in enginePurchaseReception.ReceptionLines)
                {
                    var purchaseConAc =
                        _contractEngineRepository.GetFiltered(p => p.Id == receptionLine.ContractEngineId)
                            .OfType<PurchaseContractEngine>()
                            .FirstOrDefault();
                    var newRecepitonLine = ReceptionFactory.CreateEnginePurchaseReceptionLine();
                    newRecepitonLine.ReceivedAmount = receptionLine.ReceivedAmount;
                    newRecepitonLine.AcceptedAmount = receptionLine.AcceptedAmount;
                    newRecepitonLine.SetCompleted();
                    newRecepitonLine.Note = receptionLine.Note;
                    newRecepitonLine.DeliverDate = receptionLine.DeliverDate;
                    newRecepitonLine.DeliverPlace = receptionLine.DeliverPlace;
                    newRecepitonLine.SetContractEngine(purchaseConAc);
                    newEnginePurchaseReception.ReceptionLines.Add(newRecepitonLine);
                }
            }
            if (enginePurchaseReception.ReceptionSchedules != null)
                foreach (var schdeule in enginePurchaseReception.ReceptionSchedules)
                {
                    var newSchedule = new ReceptionSchedule();
                    newSchedule.Body = schdeule.Body;
                    newSchedule.Subject = schdeule.Subject;
                    newSchedule.Importance = schdeule.Importance;
                    newSchedule.Start = schdeule.Start;
                    newSchedule.End = schdeule.End;
                    newSchedule.IsAllDayEvent = schdeule.IsAllDayEvent;
                    newSchedule.Group = schdeule.Group;
                    newSchedule.Tempo = schdeule.Tempo;
                    newSchedule.Location = schdeule.Location;
                    newSchedule.UniqueId = schdeule.UniqueId;
                    newSchedule.Url = schdeule.Url;
                    newEnginePurchaseReception.ReceptionSchedules.Add(newSchedule);
                }

            _receptionRepository.Add(newEnginePurchaseReception);
        }

        /// <summary>
        ///     更新购买发动机接收项目。
        /// </summary>
        /// <param name="enginePurchaseReception">购买发动机接收项目DTO。</param>
        [Update(typeof(EnginePurchaseReceptionDTO))]
        public void ModifyEnginePurchaseReception(EnginePurchaseReceptionDTO enginePurchaseReception)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.SupplierCompanyId == enginePurchaseReception.SupplierId).FirstOrDefault();
            var updateEnginePurchaseReception = _receptionRepository.GetFiltered(t => t.Id == enginePurchaseReception.EnginePurchaseReceptionId).FirstOrDefault();
            //获取需要更新的对象。
            if (updateEnginePurchaseReception != null)
            {
                updateEnginePurchaseReception.Description = enginePurchaseReception.Description;
                updateEnginePurchaseReception.StartDate = enginePurchaseReception.StartDate;
                updateEnginePurchaseReception.EndDate = enginePurchaseReception.EndDate;
                updateEnginePurchaseReception.SetSupplier(supplier);
                updateEnginePurchaseReception.SourceId = enginePurchaseReception.SourceId;
                //更新主表。 

                    var updateReceptionLines = enginePurchaseReception.ReceptionLines;
                    var formerReceptionLines = updateEnginePurchaseReception.ReceptionLines;
                if (enginePurchaseReception.ReceptionLines != null)//更新从表需要双向比对变更
                {

                    foreach (var receptionLine in updateReceptionLines)
                    {
                        AddOrUpdateReceptionLine(receptionLine,formerReceptionLines);
                        //更新或删除此接收行
                    }
                }
                if (updateEnginePurchaseReception.ReceptionLines != null)
                {
                    foreach (var formerReceptionLine in formerReceptionLines)
                    {
                        DeleteReceptionLine(formerReceptionLine, updateReceptionLines);
                    }
                }


                //更新从表。
            }
            _receptionRepository.Modify(updateEnginePurchaseReception);
        }

        /// <summary>
        ///     删除购买发动机接收项目。
        /// </summary>
        /// <param name="enginePurchaseReception">购买发动机接收项目DTO。</param>
        [Delete(typeof(EnginePurchaseReceptionDTO))]
        public void DeleteEnginePurchaseReception(EnginePurchaseReceptionDTO enginePurchaseReception)
        {
            if (enginePurchaseReception == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delEnginePurchaseReception = _receptionRepository.Get(enginePurchaseReception.EnginePurchaseReceptionId);
            //获取需要删除的对象。
            if (delEnginePurchaseReception != null)
            {
                _receptionRepository.DeleteReception(delEnginePurchaseReception);//删除租赁飞机接收项目。
            }
        }

        #endregion
        #region 更新从表方法

        private void AddOrUpdateReceptionLine(EnginePurchaseReceptionLineDTO receptionLine,
            IEnumerable<ReceptionLine> formerReceptionLines)
        {
            //获取源接收行
            var existReceptionLine = formerReceptionLines.FirstOrDefault(p => p.Id == receptionLine.EnginePurchaseReceptionLineId);
            //if (existReceptionLine == null) 

        }

        private void DeleteReceptionLine(ReceptionLine formerReceptionLine, IEnumerable<EnginePurchaseReceptionLineDTO> updateReceptionLines)
        {
        }

        #endregion
    }
}