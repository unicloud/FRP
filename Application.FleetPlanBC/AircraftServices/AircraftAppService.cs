#region Version Info

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/29 12:06:16
// 文件名：AircraftAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/29 12:06:16
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.AircraftQueries;
using UniCloud.Domain.Common.Enums;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftServices
{
    /// <summary>
    ///     实现实际飞机接口。
    ///     用于处于实际飞机相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class AircraftAppService : IAircraftAppService
    {
        private readonly IAircraftQuery _aircraftQuery;
        private readonly IAircraftRepository _aircraftRepository;

        public AircraftAppService(IAircraftQuery aircraftQuery, IAircraftRepository aircraftRepository)
        {
            _aircraftQuery = aircraftQuery;
            _aircraftRepository = aircraftRepository;
        }

        #region AircraftDTO

        /// <summary>
        ///     获取所有实际飞机。
        /// </summary>
        /// <returns>所有实际飞机。</returns>
        public IQueryable<AircraftDTO> GetAircrafts()
        {
            var queryBuilder = new QueryBuilder<Aircraft>();
            return _aircraftQuery.AircraftDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增实际飞机。
        /// </summary>
        /// <param name="aircraft">实际飞机DTO。</param>
        [Insert(typeof (AircraftDTO))]
        public void InsertAircraft(AircraftDTO aircraft)
        {
            //var newEngineAircraft = AircraftFactory.CreateEngineAircraft();
            //var date = DateTime.Now.Date;
            //var seq = _invoiceRepository.GetFiltered(t => t.CreateDate > date).Count() + 1;
            //newEngineAircraft.SetInvoiceNumber(seq);
            //AircraftFactory.SetAircraft(newEngineAircraft, engineAircraft.SerialNumber, 
            //    engineAircraft.InvoideCode, engineAircraft.InvoiceDate, engineAircraft.SupplierName, engineAircraft.SupplierId,
            //    engineAircraft.InvoiceValue, engineAircraft.PaidAmount, engineAircraft.OperatorName,
            //    engineAircraft.Reviewer, engineAircraft.Status, engineAircraft.CurrencyId, engineAircraft.DocumentName, engineAircraft.DocumentId);
            //if (engineAircraft.AircraftLines != null)
            //{
            //    foreach (var AircraftLine in engineAircraft.AircraftLines)
            //    {
            //        var newAircraftLine = AircraftFactory.CreateAircraftLine();
            //        AircraftFactory.SetAircraftLine(newAircraftLine, AircraftLine.MaintainItem, AircraftLine.ItemName, AircraftLine.UnitPrice,
            //            AircraftLine.Amount, AircraftLine.Note);
            //        newEngineAircraft.AircraftLines.Add(newAircraftLine);
            //    }
            //}
            //_invoiceRepository.Add(newEngineAircraft);
        }


        /// <summary>
        ///     更新实际飞机。
        /// </summary>
        /// <param name="aircraft">实际飞机DTO。</param>
        [Update(typeof (AircraftDTO))]
        public void ModifyAircraft(AircraftDTO aircraft)
        {
            if (aircraft == null)
            {
                throw new Exception("参数不能为空");
            }
            var persistAircraft =
                _aircraftRepository.Get(aircraft.AircraftId); //获取需要更新的对象。
            if (persistAircraft == null)
            {
                throw new Exception("找不到需要更新的飞机");
            }
            DataHelper.DetailHandle(aircraft.OwnershipHistories.ToArray()
                , persistAircraft.OwnershipHistories.ToArray(), p => p.AircraftId, p => p.Id,
                c => InsertOwnershipHistory(persistAircraft, c), (p, c) => ModifyOwnershipHistory(p, c, persistAircraft),
                DelOwnershipHistory);
        }

        /// <summary>
        ///     删除实际飞机。
        /// </summary>
        /// <param name="aircraft">实际飞机DTO。</param>
        [Delete(typeof (AircraftDTO))]
        public void DeleteAircraft(AircraftDTO aircraft)
        {
            //var deleteEngineAircraft =
            //    _invoiceRepository.Get(engineAircraft.EngineAircraftId); //获取需要删除的对象。
            //UpdateAircraftLines(new List<AircraftLineDTO>(), deleteEngineAircraft);
            //_invoiceRepository.Remove(deleteEngineAircraft); //删除实际飞机。
        }

        #endregion

        /// <summary>
        ///     新增所有权
        /// </summary>
        /// <param name="aircraft">飞机</param>
        /// <param name="ownershipHistory">所有权</param>
        private void InsertOwnershipHistory(Aircraft aircraft, OwnershipHistoryDTO ownershipHistory)
        {
            var updateOwnershipHistory = aircraft.OwnershipHistories.OrderBy(p => p.StartDate).LastOrDefault();
            DateTime? endDateTime = ownershipHistory.StartDate.AddDays(-1);
            if (updateOwnershipHistory != null) updateOwnershipHistory.SetEndDate(endDateTime);
            aircraft.AddNewOwnershipHistory(ownershipHistory.SupplierId, ownershipHistory.StartDate,
                ownershipHistory.EndDate, (OperationStatus)ownershipHistory.Status);
        }

        /// <summary>
        ///     删除所有权
        /// </summary>
        /// <param name="ownershipHistory">所有权</param>
        private void DelOwnershipHistory(OwnershipHistory ownershipHistory)
        {
            _aircraftRepository.RemoveOwnershipHistory(ownershipHistory);
        }

        private void ModifyOwnershipHistory(OwnershipHistoryDTO ownershipHistory,
            OwnershipHistory persistOwnershipHistory, Aircraft aircraft)
        {
            if (ownershipHistory.SupplierId != persistOwnershipHistory.SupplierId)
            {
                persistOwnershipHistory.SetSupplier(ownershipHistory.SupplierId);
            }
            if (ownershipHistory.StartDate != persistOwnershipHistory.StartDate)
            {
                persistOwnershipHistory.SetStartDate(ownershipHistory.StartDate);
                var updateOwnershipHistory = aircraft.OwnershipHistories.OrderBy(p => p.StartDate).LastOrDefault();
                DateTime? endDateTime = ownershipHistory.StartDate.AddDays(-1);
                if (updateOwnershipHistory != null) updateOwnershipHistory.SetEndDate(endDateTime);
            }
            if (ownershipHistory.EndDate != persistOwnershipHistory.EndDate)
            {
                persistOwnershipHistory.SetEndDate(ownershipHistory.EndDate);
            }
            if (ownershipHistory.Status!=(int)persistOwnershipHistory.Status)
            {
                persistOwnershipHistory.SetOperationStatus((OperationStatus)ownershipHistory.Status);
            }
        }
    }
}