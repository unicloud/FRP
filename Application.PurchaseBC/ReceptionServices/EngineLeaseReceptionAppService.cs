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
    ///     租赁发动机接收项目服务实现
    /// </summary>
    public class EngineLeaseReceptionAppService : IEngineLeaseReceptionAppService
    {
        private readonly IEngineLeaseReceptionQuery _engineLeaseReceptionQuery;
        private readonly IReceptionRepository _receptionRepository;
        private readonly ISupplierRepository _supplierRepository;
        private readonly IContractEngineRepository _contractEngineRepository;

        public EngineLeaseReceptionAppService(IEngineLeaseReceptionQuery engineLeaseReceptionQuery,
            IReceptionRepository receptionRepository,
            ISupplierRepository supplierRepository,
            IContractEngineRepository contractEngineRepository)
        {
            _engineLeaseReceptionQuery = engineLeaseReceptionQuery;
            _receptionRepository = receptionRepository;
            _supplierRepository = supplierRepository;
            _contractEngineRepository = contractEngineRepository;
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
        [Insert(typeof(EngineLeaseReceptionDTO))]
        public void InsertEngineLeaseReception(EngineLeaseReceptionDTO engineLeaseReception)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.SupplierCompanyId == engineLeaseReception.SupplierId).FirstOrDefault();

            var newEngineLeaseReception = ReceptionFactory.CreateEngineLeaseReception();
            newEngineLeaseReception.SetReceptionNumber(1);
            newEngineLeaseReception.Description = engineLeaseReception.Description;
            newEngineLeaseReception.StartDate = engineLeaseReception.StartDate;
            newEngineLeaseReception.SetStatus(0);
            newEngineLeaseReception.EndDate = engineLeaseReception.EndDate;
            newEngineLeaseReception.SetSupplier(supplier);
            newEngineLeaseReception.SourceId = engineLeaseReception.SourceId;
            if (engineLeaseReception.ReceptionLines != null)
            {
                foreach (var receptionLine in engineLeaseReception.ReceptionLines)
                {
                    var leaseConAc =
                        _contractEngineRepository.GetFiltered(p => p.Id == receptionLine.ContractEngineId)
                            .OfType<LeaseContractEngine>()
                            .FirstOrDefault();
                    var newRecepitonLine = ReceptionFactory.CreateEngineLeaseReceptionLine();
                    newRecepitonLine.ReceivedAmount = receptionLine.ReceivedAmount;
                    newRecepitonLine.AcceptedAmount = receptionLine.AcceptedAmount;
                    newRecepitonLine.SetCompleted();
                    newRecepitonLine.Note = receptionLine.Note;
                    newRecepitonLine.DeliverDate = receptionLine.DeliverDate;
                    newRecepitonLine.DeliverPlace = receptionLine.DeliverPlace;
                    newRecepitonLine.SetContractEngine(leaseConAc);
                    newEngineLeaseReception.ReceptionLines.Add(newRecepitonLine);
                }
            }
            if (engineLeaseReception.ReceptionSchedules != null)
                foreach (var schdeule in engineLeaseReception.ReceptionSchedules)
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
                    newEngineLeaseReception.ReceptionSchedules.Add(newSchedule);
                }

            _receptionRepository.Add(newEngineLeaseReception);
        }

        /// <summary>
        ///     更新租赁发动机接收项目。
        /// </summary>
        /// <param name="engineLeaseReception">租赁发动机接收项目DTO。</param>
        [Update(typeof(EngineLeaseReceptionDTO))]
        public void ModifyEngineLeaseReception(EngineLeaseReceptionDTO engineLeaseReception)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.SupplierCompanyId == engineLeaseReception.SupplierId).FirstOrDefault();
            var updateEngineLeaseReception = _receptionRepository.GetFiltered(t => t.Id == engineLeaseReception.EngineLeaseReceptionId).FirstOrDefault();
            //获取需要更新的对象。
            if (updateEngineLeaseReception != null)
            {
                updateEngineLeaseReception.Description = engineLeaseReception.Description;
                updateEngineLeaseReception.StartDate = engineLeaseReception.StartDate;
                updateEngineLeaseReception.EndDate = engineLeaseReception.EndDate;
                updateEngineLeaseReception.SetSupplier(supplier);
                updateEngineLeaseReception.SourceId = engineLeaseReception.SourceId;
                //更新主表。 

                    var updateReceptionLines = engineLeaseReception.ReceptionLines;
                    var formerReceptionLines = updateEngineLeaseReception.ReceptionLines;
                if (engineLeaseReception.ReceptionLines != null)//更新从表需要双向比对变更
                {

                    foreach (var receptionLine in updateReceptionLines)
                    {
                        AddOrUpdateReceptionLine(receptionLine,formerReceptionLines);
                        //更新或删除此接收行
                    }
                }
                if (updateEngineLeaseReception.ReceptionLines != null)
                {
                    foreach (var formerReceptionLine in formerReceptionLines)
                    {
                        DeleteReceptionLine(formerReceptionLine, updateReceptionLines);
                    }
                }


                //更新从表。
            }
            _receptionRepository.Modify(updateEngineLeaseReception);
        }

        /// <summary>
        ///     删除租赁发动机接收项目。
        /// </summary>
        /// <param name="engineLeaseReception">租赁发动机接收项目DTO。</param>
        [Delete(typeof(EngineLeaseReceptionDTO))]
        public void DeleteEngineLeaseReception(EngineLeaseReceptionDTO engineLeaseReception)
        {
            if (engineLeaseReception == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delEngineLeaseReception = _receptionRepository.Get(engineLeaseReception.EngineLeaseReceptionId);
            //获取需要删除的对象。
            if (delEngineLeaseReception != null)
            {
                _receptionRepository.DeleteReception(delEngineLeaseReception);//删除租赁飞机接收项目。
            }
        }

        #endregion
        #region 更新从表方法

        private void AddOrUpdateReceptionLine(EngineLeaseReceptionLineDTO receptionLine,
            IEnumerable<ReceptionLine> formerReceptionLines)
        {
            //获取源接收行
            var existReceptionLine = formerReceptionLines.FirstOrDefault(p => p.Id == receptionLine.EngineLeaseReceptionLineId);
            //if (existReceptionLine == null) 

        }

        private void DeleteReceptionLine(ReceptionLine formerReceptionLine, IEnumerable<EngineLeaseReceptionLineDTO> updateReceptionLines)
        {
        }

        #endregion
    }
}