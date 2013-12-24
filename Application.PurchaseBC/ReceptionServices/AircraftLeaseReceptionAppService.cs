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
using System.Collections.ObjectModel;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PurchaseBC.DTO;
using UniCloud.Application.PurchaseBC.Query.ReceptionQueries;
using UniCloud.Domain.PurchaseBC.Aggregates.ContractAircraftAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.ReceptionAgg;
using UniCloud.Domain.PurchaseBC.Aggregates.SupplierAgg;
using UniCloud.Domain.PurchaseBC.Enums;

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
        private readonly ISupplierRepository _supplierRepository;
        private readonly IContractAircraftRepository _contractAircraftRepository;

        public AircraftLeaseReceptionAppService(IAircraftLeaseReceptionQuery aircraftLeaseReceptionQuery,
            IReceptionRepository receptionRepository,
            ISupplierRepository supplierRepository,
            IContractAircraftRepository contractAircraftRepository)
        {
            _aircraftLeaseReceptionQuery = aircraftLeaseReceptionQuery;
            _receptionRepository = receptionRepository;
            _supplierRepository = supplierRepository;
            _contractAircraftRepository = contractAircraftRepository;
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
            var supplier = _supplierRepository.GetFiltered(p => p.SupplierCompanyId == aircraftLeaseReception.SupplierId).FirstOrDefault();

            var newAircraftLeaseReception = ReceptionFactory.CreateAircraftLeaseReception();
            var date = DateTime.Now.Date;
            var seq = _receptionRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            newAircraftLeaseReception.SetReceptionNumber(seq);
            newAircraftLeaseReception.Description = aircraftLeaseReception.Description;
            newAircraftLeaseReception.StartDate = aircraftLeaseReception.StartDate;
            newAircraftLeaseReception.SetStatus(ReceptionStatus.开始);
            newAircraftLeaseReception.EndDate = aircraftLeaseReception.EndDate;
            newAircraftLeaseReception.SetSupplier(supplier);
            newAircraftLeaseReception.SourceId = aircraftLeaseReception.SourceId;
            if (aircraftLeaseReception.ReceptionLines != null)
            {
                foreach (var receptionLine in aircraftLeaseReception.ReceptionLines)
                {
                    var leaseConAc =
                        _contractAircraftRepository.GetFiltered(p => p.Id == receptionLine.ContractAircraftId)
                            .OfType<LeaseContractAircraft>()
                            .FirstOrDefault();
                    var newRecepitonLine = ReceptionFactory.CreateAircraftLeaseReceptionLine();
                    newRecepitonLine.ReceivedAmount = receptionLine.ReceivedAmount;
                    newRecepitonLine.AcceptedAmount = receptionLine.AcceptedAmount;
                    newRecepitonLine.SetCompleted();
                    newRecepitonLine.Note = receptionLine.Note;
                    newRecepitonLine.DeliverDate = receptionLine.DeliverDate;
                    newRecepitonLine.DeliverPlace = receptionLine.DeliverPlace;
                    newRecepitonLine.DailNumber = receptionLine.DailNumber;
                    newRecepitonLine.FlightNumber = receptionLine.FlightNumber;
                    newRecepitonLine.SetContractAircraft(leaseConAc);
                    newAircraftLeaseReception.ReceptionLines.Add(newRecepitonLine);
                }
            }
            if (aircraftLeaseReception.ReceptionSchedules != null)
                foreach (var schdeule in aircraftLeaseReception.ReceptionSchedules)
                {
                    var newSchedule = new ReceptionSchedule();
                    //newSchedule.Body = schdeule.Body;
                    //newSchedule.Subject = schdeule.Subject;
                    //newSchedule.Importance = schdeule.Importance;
                    //newSchedule.Start = schdeule.Start;
                    //newSchedule.End = schdeule.End;
                    //newSchedule.IsAllDayEvent = schdeule.IsAllDayEvent;
                    //newSchedule.Group = schdeule.Group;
                    //newSchedule.Tempo = schdeule.Tempo;
                    //newSchedule.Location = schdeule.Location;
                    //newSchedule.UniqueId = schdeule.UniqueId;
                    //newSchedule.Url = schdeule.Url;
                    newAircraftLeaseReception.ReceptionSchedules.Add(newSchedule);
                }

            _receptionRepository.Add(newAircraftLeaseReception);
        }

        /// <summary>
        ///     更新租赁飞机接收项目。
        /// </summary>
        /// <param name="aircraftLeaseReception">租赁飞机接收项目DTO。</param>
        [Update(typeof(AircraftLeaseReceptionDTO))]
        public void ModifyAircraftLeaseReception(AircraftLeaseReceptionDTO aircraftLeaseReception)
        {
            var supplier = _supplierRepository.GetFiltered(p => p.SupplierCompanyId == aircraftLeaseReception.SupplierId).FirstOrDefault();
            var updateAircraftLeaseReception = _receptionRepository.GetFiltered(t => t.Id == aircraftLeaseReception.AircraftLeaseReceptionId).FirstOrDefault();
            //获取需要更新的对象。
            if (updateAircraftLeaseReception != null)
            {
                updateAircraftLeaseReception.SetReceptionNumber(aircraftLeaseReception.ReceptionNumber);
                updateAircraftLeaseReception.Description = aircraftLeaseReception.Description;
                updateAircraftLeaseReception.StartDate = aircraftLeaseReception.StartDate;
                updateAircraftLeaseReception.EndDate = aircraftLeaseReception.EndDate;
                updateAircraftLeaseReception.SetSupplier(supplier);
                updateAircraftLeaseReception.SetStatus((ReceptionStatus)aircraftLeaseReception.Status);
                updateAircraftLeaseReception.SourceId = aircraftLeaseReception.SourceId;
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
            if (aircraftLeaseReception == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delAircraftLeaseReception = _receptionRepository.Get(aircraftLeaseReception.AircraftLeaseReceptionId);
            //获取需要删除的对象。
            if (delAircraftLeaseReception != null)
            {
                _receptionRepository.DeleteReception(delAircraftLeaseReception);//删除租赁飞机接收项目。
            }
        }

        #endregion
        #region 更新从表方法

        private void AddOrUpdateReceptionLine(AircraftLeaseReceptionLineDTO receptionLine,
            IEnumerable<ReceptionLine> formerReceptionLines)
        {
            //获取源接收行
            var existReceptionLine = formerReceptionLines.FirstOrDefault(p => p.Id == receptionLine.AircraftLeaseReceptionLineId);
            //if (existReceptionLine == null) 

        }

        private void DeleteReceptionLine(ReceptionLine formerReceptionLine, IEnumerable<AircraftLeaseReceptionLineDTO> updateReceptionLines)
        {
        }

        #endregion
    }
}