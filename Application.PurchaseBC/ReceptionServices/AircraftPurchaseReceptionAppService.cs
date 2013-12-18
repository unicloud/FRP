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
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;

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
        private readonly ISupplierRepository _supplierRepository;
        private readonly IContractAircraftRepository _contractAircraftRepository;

        public AircraftPurchaseReceptionAppService(IAircraftPurchaseReceptionQuery aircraftPurchaseReceptionQuery,
            IReceptionRepository receptionRepository,
            ISupplierRepository supplierRepository,
            IContractAircraftRepository contractAircraftRepository)
        {
            _aircraftPurchaseReceptionQuery = aircraftPurchaseReceptionQuery;
            _receptionRepository = receptionRepository;
            _supplierRepository = supplierRepository;
            _contractAircraftRepository = contractAircraftRepository;
        }

        #region AircraftPurchaseReceptionDTO

        /// <summary>
        ///     获取所有购买飞机接收项目
        /// </summary>
        /// <returns></returns>
        public IQueryable<AircraftPurchaseReceptionDTO> GetAircraftPurchaseReceptions()
        {
            var queryBuilder =
                new QueryBuilder<AircraftPurchaseReception>();
            return _aircraftPurchaseReceptionQuery.AircraftPurchaseReceptionDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增购买飞机接收项目。
        /// </summary>
        /// <param name="aircraftPurchaseReception">购买飞机接收项目DTO。</param>
        [Insert(typeof(AircraftPurchaseReceptionDTO))]
        public void InsertAircraftPurchaseReception(AircraftPurchaseReceptionDTO aircraftPurchaseReception)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.SupplierCompanyId == aircraftPurchaseReception.SupplierId).FirstOrDefault();

            var newAircraftPurchaseReception = ReceptionFactory.CreateAircraftPurchaseReception();
            var date = DateTime.Now.Date;
            var seq = _receptionRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            newAircraftPurchaseReception.SetReceptionNumber(seq);
            newAircraftPurchaseReception.Description = aircraftPurchaseReception.Description;
            newAircraftPurchaseReception.StartDate = aircraftPurchaseReception.StartDate;
            newAircraftPurchaseReception.SetStatus(0);
            newAircraftPurchaseReception.EndDate = aircraftPurchaseReception.EndDate;
            newAircraftPurchaseReception.SetSupplier(supplier);
            newAircraftPurchaseReception.SourceId = aircraftPurchaseReception.SourceId;
            if (aircraftPurchaseReception.ReceptionLines != null)
            {
                foreach (var receptionLine in aircraftPurchaseReception.ReceptionLines)
                {
                    var purchaseConAc =
                        _contractAircraftRepository.GetFiltered(p => p.Id == receptionLine.ContractAircraftId)
                            .OfType<PurchaseContractAircraft>()
                            .FirstOrDefault();
                    var newRecepitonLine = ReceptionFactory.CreateAircraftPurchaseReceptionLine();
                    newRecepitonLine.ReceivedAmount = receptionLine.ReceivedAmount;
                    newRecepitonLine.AcceptedAmount = receptionLine.AcceptedAmount;
                    newRecepitonLine.SetCompleted();
                    newRecepitonLine.Note = receptionLine.Note;
                    newRecepitonLine.DeliverDate = receptionLine.DeliverDate;
                    newRecepitonLine.DeliverPlace = receptionLine.DeliverPlace;
                    newRecepitonLine.DailNumber = receptionLine.DailNumber;
                    newRecepitonLine.FlightNumber = receptionLine.FlightNumber;
                    newRecepitonLine.SetContractAircraft(purchaseConAc);
                    newAircraftPurchaseReception.ReceptionLines.Add(newRecepitonLine);
                }
            }
            if (aircraftPurchaseReception.ReceptionSchedules != null)
                foreach (var schdeule in aircraftPurchaseReception.ReceptionSchedules)
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
                    newAircraftPurchaseReception.ReceptionSchedules.Add(newSchedule);
                }

            _receptionRepository.Add(newAircraftPurchaseReception);
        }

        /// <summary>
        ///     更新购买飞机接收项目。
        /// </summary>
        /// <param name="aircraftPurchaseReception">购买飞机接收项目DTO。</param>
        [Update(typeof(AircraftPurchaseReceptionDTO))]
        public void ModifyAircraftPurchaseReception(AircraftPurchaseReceptionDTO aircraftPurchaseReception)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.SupplierCompanyId == aircraftPurchaseReception.SupplierId).FirstOrDefault();
            var updateAircraftPurchaseReception = _receptionRepository.GetFiltered(t => t.Id == aircraftPurchaseReception.AircraftPurchaseReceptionId).FirstOrDefault();
            //获取需要更新的对象。
            if (updateAircraftPurchaseReception != null)
            {
                updateAircraftPurchaseReception.Description = aircraftPurchaseReception.Description;
                updateAircraftPurchaseReception.StartDate = aircraftPurchaseReception.StartDate;
                updateAircraftPurchaseReception.EndDate = aircraftPurchaseReception.EndDate;
                updateAircraftPurchaseReception.SetSupplier(supplier);
                updateAircraftPurchaseReception.SourceId = aircraftPurchaseReception.SourceId;
                //更新主表。 

                var updateReceptionLines = aircraftPurchaseReception.ReceptionLines;
                var formerReceptionLines = updateAircraftPurchaseReception.ReceptionLines;
                if (aircraftPurchaseReception.ReceptionLines != null)//更新从表需要双向比对变更
                {

                    foreach (var receptionLine in updateReceptionLines)
                    {
                        AddOrUpdateReceptionLine(receptionLine, formerReceptionLines);
                        //更新或删除此接收行
                    }
                }
                if (updateAircraftPurchaseReception.ReceptionLines != null)
                {
                    foreach (var formerReceptionLine in formerReceptionLines)
                    {
                        DeleteReceptionLine(formerReceptionLine, updateReceptionLines);
                    }
                }


                //更新从表。
            }
            _receptionRepository.Modify(updateAircraftPurchaseReception);
        }

        /// <summary>
        ///     删除购买飞机接收项目。
        /// </summary>
        /// <param name="aircraftPurchaseReception">购买飞机接收项目DTO。</param>
        [Delete(typeof(AircraftPurchaseReceptionDTO))]
        public void DeleteAircraftPurchaseReception(AircraftPurchaseReceptionDTO aircraftPurchaseReception)
        {
            if (aircraftPurchaseReception == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delAircraftPurchaseReception = _receptionRepository.Get(aircraftPurchaseReception.AircraftPurchaseReceptionId);
            //获取需要删除的对象。
            if (delAircraftPurchaseReception != null)
            {
                _receptionRepository.DeleteReception(delAircraftPurchaseReception);//删除租赁飞机接收项目。
            }
        }

        #endregion
        #region 更新从表方法

        private void AddOrUpdateReceptionLine(AircraftPurchaseReceptionLineDTO receptionLine,
            IEnumerable<ReceptionLine> formerReceptionLines)
        {
            //获取源接收行
            var existReceptionLine = formerReceptionLines.FirstOrDefault(p => p.Id == receptionLine.AircraftPurchaseReceptionLineId);
            //if (existReceptionLine == null) 

        }

        private void DeleteReceptionLine(ReceptionLine formerReceptionLine, IEnumerable<AircraftPurchaseReceptionLineDTO> updateReceptionLines)
        {
        }

        #endregion
    }
}