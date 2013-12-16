#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/16 11:16:04
// 文件名：MaintainInvoiceAppService
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/16 11:16:04
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System.Linq;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.MaintainInvoiceQueries;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;

#endregion

namespace UniCloud.Application.PaymentBC.MaintainInvoiceServices
{
    /// <summary>
    ///     实现发动机维修发票接口。
    ///     用于处于维修发票相关信息的服务，供Distributed Services调用。
    /// </summary>
    public class MaintainInvoiceAppService : IMaintainInvoiceAppService
    {
        private readonly IMaintainInvoiceQuery _maintainInvoiceQuery;
        private readonly IMaintainInvoiceRepository _invoiceRepository;

        public MaintainInvoiceAppService(IMaintainInvoiceQuery maintainInvoiceQuery, IMaintainInvoiceRepository invoiceRepository)
        {
            _maintainInvoiceQuery = maintainInvoiceQuery;
            _invoiceRepository = invoiceRepository;
        }

        #region EngineMaintainInvoiceDTO
        /// <summary>
        ///     获取所有发动机维修发票。
        /// </summary>
        /// <returns>所有发动机维修发票。</returns>
        public IQueryable<EngineMaintainInvoiceDTO> GetEngineMaintainInvoices()
        {
            var queryBuilder = new QueryBuilder<MaintainInvoice>();
            return _maintainInvoiceQuery.EngineMaintainInvoiceDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增发动机维修发票。
        /// </summary>
        /// <param name="engineMaintainInvoice">发动机维修发票DTO。</param>
        [Insert(typeof(EngineMaintainInvoiceDTO))]
        public void InsertEngineMaintainInvoice(EngineMaintainInvoiceDTO engineMaintainInvoice)
        {
            var newEngineMaintainInvoice = MaintainInvoiceFactory.CreateEngineMaintainInvoice();
            MaintainInvoiceFactory.SetMaintainInvoice(newEngineMaintainInvoice, engineMaintainInvoice.SerialNumber, engineMaintainInvoice.InvoiceNumber,
                engineMaintainInvoice.InvoideCode, engineMaintainInvoice.InvoiceDate, engineMaintainInvoice.SupplierName, engineMaintainInvoice.SupplierId,
                engineMaintainInvoice.InvoiceValue, engineMaintainInvoice.PaidAmount, engineMaintainInvoice.OperatorName,
                engineMaintainInvoice.Reviewer, engineMaintainInvoice.Status, engineMaintainInvoice.CurrencyId);
            _invoiceRepository.Add(newEngineMaintainInvoice);
        }


        /// <summary>
        ///     更新发动机维修发票。
        /// </summary>
        /// <param name="engineMaintainInvoice">发动机维修发票DTO。</param>
        [Update(typeof(EngineMaintainInvoiceDTO))]
        public void ModifyEngineMaintainInvoice(EngineMaintainInvoiceDTO engineMaintainInvoice)
        {
            var updateEngineMaintainInvoice =
                _invoiceRepository.Get(engineMaintainInvoice.EngineMaintainInvoiceId); //获取需要更新的对象。
            MaintainInvoiceFactory.SetMaintainInvoice(updateEngineMaintainInvoice, engineMaintainInvoice.SerialNumber, engineMaintainInvoice.InvoiceNumber,
                engineMaintainInvoice.InvoideCode, engineMaintainInvoice.InvoiceDate, engineMaintainInvoice.SupplierName, engineMaintainInvoice.SupplierId,
                engineMaintainInvoice.InvoiceValue, engineMaintainInvoice.PaidAmount, engineMaintainInvoice.OperatorName,
               engineMaintainInvoice.Reviewer, engineMaintainInvoice.Status, engineMaintainInvoice.CurrencyId);
            _invoiceRepository.Modify(updateEngineMaintainInvoice);
        }

        /// <summary>
        ///     删除发动机维修发票。
        /// </summary>
        /// <param name="engineMaintainInvoice">发动机维修发票DTO。</param>
        [Delete(typeof(EngineMaintainInvoiceDTO))]
        public void DeleteEngineMaintainInvoice(EngineMaintainInvoiceDTO engineMaintainInvoice)
        {
            var deleteEngineMaintainInvoice =
                _invoiceRepository.Get(engineMaintainInvoice.EngineMaintainInvoiceId); //获取需要删除的对象。
            _invoiceRepository.Remove(deleteEngineMaintainInvoice); //删除发动机维修发票。
        }
        #endregion

        #region APUMaintainInvoiceDTO
        /// <summary>
        ///     获取所有APU维修发票。
        /// </summary>
        /// <returns>所有APU维修发票。</returns>
        public IQueryable<APUMaintainInvoiceDTO> GetApuMaintainInvoices()
        {
            var queryBuilder = new QueryBuilder<MaintainInvoice>();
            return _maintainInvoiceQuery.APUMaintainInvoiceDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增APU维修发票。
        /// </summary>
        /// <param name="apuMaintainInvoice">APU维修发票DTO。</param>
        [Insert(typeof(APUMaintainInvoiceDTO))]
        public void InsertApuMaintainInvoice(APUMaintainInvoiceDTO apuMaintainInvoice)
        {
            var newApuMaintainInvoice = MaintainInvoiceFactory.CreateApuMaintainInvoice();
            MaintainInvoiceFactory.SetMaintainInvoice(newApuMaintainInvoice, apuMaintainInvoice.SerialNumber, apuMaintainInvoice.InvoiceNumber,
                apuMaintainInvoice.InvoideCode, apuMaintainInvoice.InvoiceDate, apuMaintainInvoice.SupplierName, apuMaintainInvoice.SupplierId,
                apuMaintainInvoice.InvoiceValue, apuMaintainInvoice.PaidAmount, apuMaintainInvoice.OperatorName,
               apuMaintainInvoice.Reviewer, apuMaintainInvoice.Status, apuMaintainInvoice.CurrencyId);
            _invoiceRepository.Add(newApuMaintainInvoice);
        }

        /// <summary>
        ///     更新APU维修发票。
        /// </summary>
        /// <param name="apuMaintainInvoice">APU维修发票DTO。</param>
        [Update(typeof(APUMaintainInvoiceDTO))]
        public void ModifyApuMaintainInvoice(APUMaintainInvoiceDTO apuMaintainInvoice)
        {
            var updateApuMaintainInvoice = _invoiceRepository.Get(apuMaintainInvoice.APUMaintainInvoiceId);
            MaintainInvoiceFactory.SetMaintainInvoice(updateApuMaintainInvoice, apuMaintainInvoice.SerialNumber, apuMaintainInvoice.InvoiceNumber,
                apuMaintainInvoice.InvoideCode, apuMaintainInvoice.InvoiceDate, apuMaintainInvoice.SupplierName, apuMaintainInvoice.SupplierId,
                apuMaintainInvoice.InvoiceValue, apuMaintainInvoice.PaidAmount, apuMaintainInvoice.OperatorName,
               apuMaintainInvoice.Reviewer, apuMaintainInvoice.Status, apuMaintainInvoice.CurrencyId);
            //获取需要更新的对象。
            _invoiceRepository.Modify(updateApuMaintainInvoice);
        }

        /// <summary>
        ///     删除APU维修发票。
        /// </summary>
        /// <param name="apuMaintainInvoice">APU维修发票DTO。</param>
        [Delete(typeof(APUMaintainInvoiceDTO))]
        public void DeleteApuMaintainInvoice(APUMaintainInvoiceDTO apuMaintainInvoice)
        {
            var deleteApuMaintainInvoice = _invoiceRepository.Get(apuMaintainInvoice.APUMaintainInvoiceId);
            //获取需要删除的对象。
            _invoiceRepository.Remove(deleteApuMaintainInvoice); //删除APU维修发票。
        }
        #endregion

        #region AirframeMaintainInvoiceDTO
        /// <summary>
        ///     获取所有机身维修发票。
        /// </summary>
        /// <returns>所有机身维修发票。</returns>
        public IQueryable<AirframeMaintainInvoiceDTO> GetAirframeMaintainInvoices()
        {
            var queryBuilder = new QueryBuilder<MaintainInvoice>();
            return _maintainInvoiceQuery.AirframeMaintainInvoiceDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增机身维修发票。
        /// </summary>
        /// <param name="airframeMaintainInvoice">机身维修发票DTO。</param>
        [Insert(typeof(AirframeMaintainInvoiceDTO))]
        public void InsertAirframeMaintainInvoice(AirframeMaintainInvoiceDTO airframeMaintainInvoice)
        {
            var newAirframeMaintainInvoice = MaintainInvoiceFactory.CreateAirframeMaintainInvoice();
            MaintainInvoiceFactory.SetMaintainInvoice(newAirframeMaintainInvoice, airframeMaintainInvoice.SerialNumber, airframeMaintainInvoice.InvoiceNumber,
                airframeMaintainInvoice.InvoideCode, airframeMaintainInvoice.InvoiceDate, airframeMaintainInvoice.SupplierName, airframeMaintainInvoice.SupplierId,
                airframeMaintainInvoice.InvoiceValue, airframeMaintainInvoice.PaidAmount, airframeMaintainInvoice.OperatorName,
                airframeMaintainInvoice.Reviewer, airframeMaintainInvoice.Status, airframeMaintainInvoice.CurrencyId);
            _invoiceRepository.Add(newAirframeMaintainInvoice);
        }

        /// <summary>
        ///     更新机身维修发票。
        /// </summary>
        /// <param name="airframeMaintainInvoice">机身维修发票DTO。</param>
        [Update(typeof(AirframeMaintainInvoiceDTO))]
        public void ModifyAirframeMaintainInvoice(AirframeMaintainInvoiceDTO airframeMaintainInvoice)
        {
            var updateAirframeMaintainInvoice = _invoiceRepository.Get(airframeMaintainInvoice.AirframeMaintainInvoiceId);
            MaintainInvoiceFactory.SetMaintainInvoice(updateAirframeMaintainInvoice, airframeMaintainInvoice.SerialNumber, airframeMaintainInvoice.InvoiceNumber,
                 airframeMaintainInvoice.InvoideCode, airframeMaintainInvoice.InvoiceDate, airframeMaintainInvoice.SupplierName, airframeMaintainInvoice.SupplierId,
                 airframeMaintainInvoice.InvoiceValue, airframeMaintainInvoice.PaidAmount, airframeMaintainInvoice.OperatorName,
                 airframeMaintainInvoice.Reviewer, airframeMaintainInvoice.Status, airframeMaintainInvoice.CurrencyId);
            //获取需要更新的对象。
            _invoiceRepository.Modify(updateAirframeMaintainInvoice);
        }

        /// <summary>
        ///     删除机身维修发票。
        /// </summary>
        /// <param name="airframeMaintainInvoice">机身维修发票DTO。</param>
        [Delete(typeof(AirframeMaintainInvoiceDTO))]
        public void DeleteAirframeMaintainInvoice(AirframeMaintainInvoiceDTO airframeMaintainInvoice)
        {
            var deleteAirframeMaintainInvoice = _invoiceRepository.Get(airframeMaintainInvoice.AirframeMaintainInvoiceId);
            //获取需要删除的对象。
            _invoiceRepository.Remove(deleteAirframeMaintainInvoice); //删除机身维修发票。
        }
        #endregion

        #region UndercartMaintainInvoiceDTO
        /// <summary>
        ///     获取所有起落架维修发票。
        /// </summary>
        /// <returns>所有起落架维修发票。</returns>
        public IQueryable<UndercartMaintainInvoiceDTO> GetUndercartMaintainInvoices()
        {
            var queryBuilder = new QueryBuilder<MaintainInvoice>();
            return _maintainInvoiceQuery.UndercartMaintainInvoiceDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增起落架维修发票。
        /// </summary>
        /// <param name="undercartMaintainInvoice">起落架维修发票DTO。</param>
        [Insert(typeof(UndercartMaintainInvoiceDTO))]
        public void InsertUndercartMaintainInvoice(UndercartMaintainInvoiceDTO undercartMaintainInvoice)
        {
            var newUndercartMaintainInvoice = MaintainInvoiceFactory.CreateUndercartMaintainInvoice();
            MaintainInvoiceFactory.SetMaintainInvoice(newUndercartMaintainInvoice, undercartMaintainInvoice.SerialNumber, undercartMaintainInvoice.InvoiceNumber,
                undercartMaintainInvoice.InvoideCode, undercartMaintainInvoice.InvoiceDate, undercartMaintainInvoice.SupplierName, undercartMaintainInvoice.SupplierId,
                undercartMaintainInvoice.InvoiceValue, undercartMaintainInvoice.PaidAmount, undercartMaintainInvoice.OperatorName,
               undercartMaintainInvoice.Reviewer, undercartMaintainInvoice.Status, undercartMaintainInvoice.CurrencyId);
            _invoiceRepository.Add(newUndercartMaintainInvoice);
        }

        /// <summary>
        ///     更新起落架维修发票。
        /// </summary>
        /// <param name="undercartMaintainInvoice">起落架维修发票DTO。</param>
        [Update(typeof(UndercartMaintainInvoiceDTO))]
        public void ModifyUndercartMaintainInvoice(UndercartMaintainInvoiceDTO undercartMaintainInvoice)
        {
            var updateUndercartMaintainInvoice =
                _invoiceRepository.Get(undercartMaintainInvoice.UndercartMaintainInvoiceId);
            MaintainInvoiceFactory.SetMaintainInvoice(updateUndercartMaintainInvoice, undercartMaintainInvoice.SerialNumber, undercartMaintainInvoice.InvoiceNumber,
                  undercartMaintainInvoice.InvoideCode, undercartMaintainInvoice.InvoiceDate, undercartMaintainInvoice.SupplierName, undercartMaintainInvoice.SupplierId,
                  undercartMaintainInvoice.InvoiceValue, undercartMaintainInvoice.PaidAmount, undercartMaintainInvoice.OperatorName,
                 undercartMaintainInvoice.Reviewer, undercartMaintainInvoice.Status, undercartMaintainInvoice.CurrencyId);
            //获取需要更新的对象。
            _invoiceRepository.Modify(updateUndercartMaintainInvoice);
        }

        /// <summary>
        ///     删除起落架维修发票。
        /// </summary>
        /// <param name="undercartMaintainInvoice">起落架维修发票DTO。</param>
        [Delete(typeof(UndercartMaintainInvoiceDTO))]
        public void DeleteUndercartMaintainInvoice(UndercartMaintainInvoiceDTO undercartMaintainInvoice)
        {
            var deleteUndercartMaintainInvoice =
                _invoiceRepository.Get(undercartMaintainInvoice.UndercartMaintainInvoiceId);
            //获取需要删除的对象。
            _invoiceRepository.Remove(deleteUndercartMaintainInvoice); //删除Undercart维修发票。
        }
        #endregion
    }
}
