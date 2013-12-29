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

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.FleetPlanBC.DTO;
using UniCloud.Application.FleetPlanBC.Query.AircraftQueries;
using UniCloud.Domain.FleetPlanBC.Aggregates.AircraftAgg;

#endregion

namespace UniCloud.Application.FleetPlanBC.AircraftServices
{
    /// <summary>
    ///     实现实际飞机接口。
    ///     用于处于维修发票相关信息的服务，供Distributed Services调用。
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
        [Insert(typeof(AircraftDTO))]
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
        [Update(typeof(AircraftDTO))]
        public void ModifyAircraft(AircraftDTO aircraft)
        {
            //var updateEngineAircraft =
            //    _invoiceRepository.Get(engineAircraft.EngineAircraftId); //获取需要更新的对象。
            //AircraftFactory.SetAircraft(updateEngineAircraft, engineAircraft.SerialNumber, 
            //    engineAircraft.InvoideCode, engineAircraft.InvoiceDate, engineAircraft.SupplierName, engineAircraft.SupplierId,
            //    engineAircraft.InvoiceValue, engineAircraft.PaidAmount, engineAircraft.OperatorName,
            //   engineAircraft.Reviewer, engineAircraft.Status, engineAircraft.CurrencyId, engineAircraft.DocumentName, engineAircraft.DocumentId);
            //UpdateAircraftLines(engineAircraft.AircraftLines, updateEngineAircraft);
            //_invoiceRepository.Modify(updateEngineAircraft);
        }

        /// <summary>
        ///     删除实际飞机。
        /// </summary>
        /// <param name="aircraft">实际飞机DTO。</param>
        [Delete(typeof(AircraftDTO))]
        public void DeleteAircraft(AircraftDTO aircraft)
        {
            //var deleteEngineAircraft =
            //    _invoiceRepository.Get(engineAircraft.EngineAircraftId); //获取需要删除的对象。
            //UpdateAircraftLines(new List<AircraftLineDTO>(), deleteEngineAircraft);
            //_invoiceRepository.Remove(deleteEngineAircraft); //删除实际飞机。
        }
        #endregion
    }
}
