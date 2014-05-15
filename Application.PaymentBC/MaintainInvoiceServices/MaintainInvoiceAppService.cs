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

using System;
using System.Collections.Generic;
using System.Linq;
using UniCloud.Application.AOP.Log;
using UniCloud.Application.ApplicationExtension;
using UniCloud.Application.PaymentBC.DTO;
using UniCloud.Application.PaymentBC.Query.MaintainInvoiceQueries;
using UniCloud.Domain.PaymentBC.Aggregates.InvoiceAgg;
using UniCloud.Domain.PaymentBC.Aggregates.MaintainInvoiceAgg;

#endregion

namespace UniCloud.Application.PaymentBC.MaintainInvoiceServices
{
    /// <summary>
    ///     实现发动机维修发票接口。
    ///     用于处于维修发票相关信息的服务，供Distributed Services调用。
    /// </summary>
    [LogAOP]
    public class MaintainInvoiceAppService : ContextBoundObject, IMaintainInvoiceAppService
    {
        private readonly IMaintainInvoiceQuery _maintainInvoiceQuery;
        private readonly IInvoiceRepository _invoiceRepository;

        public MaintainInvoiceAppService(IMaintainInvoiceQuery maintainInvoiceQuery, IInvoiceRepository invoiceRepository)
        {
            _maintainInvoiceQuery = maintainInvoiceQuery;
            _invoiceRepository = invoiceRepository;
        }

        /// <summary>
        ///     获取所有发票。
        /// </summary>
        /// <returns>所有发票。</returns>
        public IQueryable<BaseInvoiceDTO> GetInvoices()
        {
            var queryBuilder = new QueryBuilder<Invoice>();
            return _maintainInvoiceQuery.InvoiceDTOQuery(queryBuilder);
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
            newEngineMaintainInvoice.SetInvoiceNumber(GetMaxInvoiceNumber());
            MaintainInvoiceFactory.SetMaintainInvoice(newEngineMaintainInvoice, engineMaintainInvoice.SerialNumber,
                engineMaintainInvoice.InvoideCode, engineMaintainInvoice.InvoiceDate, engineMaintainInvoice.SupplierName, engineMaintainInvoice.SupplierId,
                engineMaintainInvoice.InvoiceValue, engineMaintainInvoice.PaidAmount, engineMaintainInvoice.OperatorName,
                engineMaintainInvoice.Reviewer, engineMaintainInvoice.Status, engineMaintainInvoice.CurrencyId,
                engineMaintainInvoice.DocumentName, engineMaintainInvoice.DocumentId, engineMaintainInvoice.PaymentScheduleLineId,
                engineMaintainInvoice.InMaintainTime, engineMaintainInvoice.OutMaintainTime);
            if (engineMaintainInvoice.MaintainInvoiceLines != null)
            {
                foreach (var maintainInvoiceLine in engineMaintainInvoice.MaintainInvoiceLines)
                {
                    var newMaintainInvoiceLine = MaintainInvoiceFactory.CreateInvoiceLine();
                    MaintainInvoiceFactory.SetInvoiceLine(newMaintainInvoiceLine, maintainInvoiceLine.MaintainItem, maintainInvoiceLine.ItemName, maintainInvoiceLine.UnitPrice, maintainInvoiceLine.Amount,
                  maintainInvoiceLine.Note);
                    newEngineMaintainInvoice.InvoiceLines.Add(newMaintainInvoiceLine);
                }
            }
            newEngineMaintainInvoice.SetInvoiceValue();
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
                _invoiceRepository.Get(engineMaintainInvoice.EngineMaintainInvoiceId) as MaintainInvoice; //获取需要更新的对象。
            MaintainInvoiceFactory.SetMaintainInvoice(updateEngineMaintainInvoice, engineMaintainInvoice.SerialNumber,
                engineMaintainInvoice.InvoideCode, engineMaintainInvoice.InvoiceDate, engineMaintainInvoice.SupplierName, engineMaintainInvoice.SupplierId,
                engineMaintainInvoice.InvoiceValue, engineMaintainInvoice.PaidAmount, engineMaintainInvoice.OperatorName,
               engineMaintainInvoice.Reviewer, engineMaintainInvoice.Status, engineMaintainInvoice.CurrencyId,
               engineMaintainInvoice.DocumentName, engineMaintainInvoice.DocumentId, engineMaintainInvoice.PaymentScheduleLineId,
               engineMaintainInvoice.InMaintainTime, engineMaintainInvoice.OutMaintainTime);
            UpdateMaintainInvoiceLines(engineMaintainInvoice.MaintainInvoiceLines, updateEngineMaintainInvoice);
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
                _invoiceRepository.GetMaintainInvoice(engineMaintainInvoice.EngineMaintainInvoiceId);//获取需要删除的对象。
            UpdateMaintainInvoiceLines(new List<MaintainInvoiceLineDTO>(), deleteEngineMaintainInvoice);
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
            newApuMaintainInvoice.SetInvoiceNumber(GetMaxInvoiceNumber());
            MaintainInvoiceFactory.SetMaintainInvoice(newApuMaintainInvoice, apuMaintainInvoice.SerialNumber,
                apuMaintainInvoice.InvoideCode, apuMaintainInvoice.InvoiceDate, apuMaintainInvoice.SupplierName, apuMaintainInvoice.SupplierId,
                apuMaintainInvoice.InvoiceValue, apuMaintainInvoice.PaidAmount, apuMaintainInvoice.OperatorName,
               apuMaintainInvoice.Reviewer, apuMaintainInvoice.Status, apuMaintainInvoice.CurrencyId,
               apuMaintainInvoice.DocumentName, apuMaintainInvoice.DocumentId, apuMaintainInvoice.PaymentScheduleLineId,
               apuMaintainInvoice.InMaintainTime, apuMaintainInvoice.OutMaintainTime);
            if (apuMaintainInvoice.MaintainInvoiceLines != null)
            {
                foreach (var maintainInvoiceLine in apuMaintainInvoice.MaintainInvoiceLines)
                {
                    var newMaintainInvoiceLine = MaintainInvoiceFactory.CreateInvoiceLine();
                    MaintainInvoiceFactory.SetInvoiceLine(newMaintainInvoiceLine, maintainInvoiceLine.MaintainItem, maintainInvoiceLine.ItemName, maintainInvoiceLine.UnitPrice, maintainInvoiceLine.Amount,
                 maintainInvoiceLine.Note);
                    newApuMaintainInvoice.InvoiceLines.Add(newMaintainInvoiceLine);
                }
            }
            newApuMaintainInvoice.SetInvoiceValue();
            _invoiceRepository.Add(newApuMaintainInvoice);
        }

        /// <summary>
        ///     更新APU维修发票。
        /// </summary>
        /// <param name="apuMaintainInvoice">APU维修发票DTO。</param>
        [Update(typeof(APUMaintainInvoiceDTO))]
        public void ModifyApuMaintainInvoice(APUMaintainInvoiceDTO apuMaintainInvoice)
        {
            var updateApuMaintainInvoice = _invoiceRepository.GetMaintainInvoice(apuMaintainInvoice.APUMaintainInvoiceId);//获取需要更新的对象。
            MaintainInvoiceFactory.SetMaintainInvoice(updateApuMaintainInvoice, apuMaintainInvoice.SerialNumber,
                apuMaintainInvoice.InvoideCode, apuMaintainInvoice.InvoiceDate, apuMaintainInvoice.SupplierName, apuMaintainInvoice.SupplierId,
                apuMaintainInvoice.InvoiceValue, apuMaintainInvoice.PaidAmount, apuMaintainInvoice.OperatorName,
               apuMaintainInvoice.Reviewer, apuMaintainInvoice.Status, apuMaintainInvoice.CurrencyId,
               apuMaintainInvoice.DocumentName, apuMaintainInvoice.DocumentId, apuMaintainInvoice.PaymentScheduleLineId,
               apuMaintainInvoice.InMaintainTime, apuMaintainInvoice.OutMaintainTime);
            UpdateMaintainInvoiceLines(apuMaintainInvoice.MaintainInvoiceLines, updateApuMaintainInvoice);

            _invoiceRepository.Modify(updateApuMaintainInvoice);
            _invoiceRepository.UnitOfWork.Commit();
        }

        /// <summary>
        ///     删除APU维修发票。
        /// </summary>
        /// <param name="apuMaintainInvoice">APU维修发票DTO。</param>
        [Delete(typeof(APUMaintainInvoiceDTO))]
        public void DeleteApuMaintainInvoice(APUMaintainInvoiceDTO apuMaintainInvoice)
        {
            var deleteApuMaintainInvoice = _invoiceRepository.GetMaintainInvoice(apuMaintainInvoice.APUMaintainInvoiceId);
            //获取需要删除的对象。
            UpdateMaintainInvoiceLines(new List<MaintainInvoiceLineDTO>(), deleteApuMaintainInvoice);
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
            newAirframeMaintainInvoice.SetInvoiceNumber(GetMaxInvoiceNumber());
            MaintainInvoiceFactory.SetMaintainInvoice(newAirframeMaintainInvoice, airframeMaintainInvoice.SerialNumber,
                airframeMaintainInvoice.InvoideCode, airframeMaintainInvoice.InvoiceDate, airframeMaintainInvoice.SupplierName, airframeMaintainInvoice.SupplierId,
                airframeMaintainInvoice.InvoiceValue, airframeMaintainInvoice.PaidAmount, airframeMaintainInvoice.OperatorName,
                airframeMaintainInvoice.Reviewer, airframeMaintainInvoice.Status, airframeMaintainInvoice.CurrencyId,
                airframeMaintainInvoice.DocumentName, airframeMaintainInvoice.DocumentId, airframeMaintainInvoice.PaymentScheduleLineId,
                airframeMaintainInvoice.InMaintainTime, airframeMaintainInvoice.OutMaintainTime);
            if (airframeMaintainInvoice.MaintainInvoiceLines != null)
            {
                foreach (var maintainInvoiceLine in airframeMaintainInvoice.MaintainInvoiceLines)
                {
                    var newMaintainInvoiceLine = MaintainInvoiceFactory.CreateInvoiceLine();
                    MaintainInvoiceFactory.SetInvoiceLine(newMaintainInvoiceLine, maintainInvoiceLine.MaintainItem, maintainInvoiceLine.ItemName, maintainInvoiceLine.UnitPrice, maintainInvoiceLine.Amount,
                 maintainInvoiceLine.Note);
                    newAirframeMaintainInvoice.InvoiceLines.Add(newMaintainInvoiceLine);
                }
            }
            newAirframeMaintainInvoice.SetInvoiceValue();
            _invoiceRepository.Add(newAirframeMaintainInvoice);
        }

        /// <summary>
        ///     更新机身维修发票。
        /// </summary>
        /// <param name="airframeMaintainInvoice">机身维修发票DTO。</param>
        [Update(typeof(AirframeMaintainInvoiceDTO))]
        public void ModifyAirframeMaintainInvoice(AirframeMaintainInvoiceDTO airframeMaintainInvoice)
        {
            var updateAirframeMaintainInvoice = _invoiceRepository.GetMaintainInvoice(airframeMaintainInvoice.AirframeMaintainInvoiceId);//获取需要更新的对象。
            MaintainInvoiceFactory.SetMaintainInvoice(updateAirframeMaintainInvoice, airframeMaintainInvoice.SerialNumber,
                 airframeMaintainInvoice.InvoideCode, airframeMaintainInvoice.InvoiceDate, airframeMaintainInvoice.SupplierName, airframeMaintainInvoice.SupplierId,
                 airframeMaintainInvoice.InvoiceValue, airframeMaintainInvoice.PaidAmount, airframeMaintainInvoice.OperatorName,
                 airframeMaintainInvoice.Reviewer, airframeMaintainInvoice.Status, airframeMaintainInvoice.CurrencyId,
                 airframeMaintainInvoice.DocumentName, airframeMaintainInvoice.DocumentId, airframeMaintainInvoice.PaymentScheduleLineId,
                 airframeMaintainInvoice.InMaintainTime, airframeMaintainInvoice.OutMaintainTime);
            UpdateMaintainInvoiceLines(airframeMaintainInvoice.MaintainInvoiceLines, updateAirframeMaintainInvoice);
            _invoiceRepository.Modify(updateAirframeMaintainInvoice);
        }

        /// <summary>
        ///     删除机身维修发票。
        /// </summary>
        /// <param name="airframeMaintainInvoice">机身维修发票DTO。</param>
        [Delete(typeof(AirframeMaintainInvoiceDTO))]
        public void DeleteAirframeMaintainInvoice(AirframeMaintainInvoiceDTO airframeMaintainInvoice)
        {
            var deleteAirframeMaintainInvoice = _invoiceRepository.GetMaintainInvoice(airframeMaintainInvoice.AirframeMaintainInvoiceId);
            //获取需要删除的对象。
            UpdateMaintainInvoiceLines(new List<MaintainInvoiceLineDTO>(), deleteAirframeMaintainInvoice);
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
            newUndercartMaintainInvoice.SetInvoiceNumber(GetMaxInvoiceNumber());
            MaintainInvoiceFactory.SetMaintainInvoice(newUndercartMaintainInvoice, undercartMaintainInvoice.SerialNumber,
                undercartMaintainInvoice.InvoideCode, undercartMaintainInvoice.InvoiceDate, undercartMaintainInvoice.SupplierName, undercartMaintainInvoice.SupplierId,
                undercartMaintainInvoice.InvoiceValue, undercartMaintainInvoice.PaidAmount, undercartMaintainInvoice.OperatorName,
               undercartMaintainInvoice.Reviewer, undercartMaintainInvoice.Status, undercartMaintainInvoice.CurrencyId,
               undercartMaintainInvoice.DocumentName, undercartMaintainInvoice.DocumentId, undercartMaintainInvoice.PaymentScheduleLineId,
               undercartMaintainInvoice.InMaintainTime, undercartMaintainInvoice.OutMaintainTime);
            if (undercartMaintainInvoice.MaintainInvoiceLines != null)
            {
                foreach (var maintainInvoiceLine in undercartMaintainInvoice.MaintainInvoiceLines)
                {
                    var newMaintainInvoiceLine = MaintainInvoiceFactory.CreateInvoiceLine();
                    MaintainInvoiceFactory.SetInvoiceLine(newMaintainInvoiceLine, maintainInvoiceLine.MaintainItem, maintainInvoiceLine.ItemName, maintainInvoiceLine.UnitPrice, maintainInvoiceLine.Amount,
                 maintainInvoiceLine.Note);
                    newUndercartMaintainInvoice.InvoiceLines.Add(newMaintainInvoiceLine);
                }
            }
            newUndercartMaintainInvoice.SetInvoiceValue();
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
                _invoiceRepository.GetMaintainInvoice(undercartMaintainInvoice.UndercartMaintainInvoiceId);//获取需要更新的对象。
            MaintainInvoiceFactory.SetMaintainInvoice(updateUndercartMaintainInvoice, undercartMaintainInvoice.SerialNumber,
                  undercartMaintainInvoice.InvoideCode, undercartMaintainInvoice.InvoiceDate, undercartMaintainInvoice.SupplierName, undercartMaintainInvoice.SupplierId,
                  undercartMaintainInvoice.InvoiceValue, undercartMaintainInvoice.PaidAmount, undercartMaintainInvoice.OperatorName,
                 undercartMaintainInvoice.Reviewer, undercartMaintainInvoice.Status, undercartMaintainInvoice.CurrencyId,
                 undercartMaintainInvoice.DocumentName, undercartMaintainInvoice.DocumentId, undercartMaintainInvoice.PaymentScheduleLineId,
                 undercartMaintainInvoice.InMaintainTime, undercartMaintainInvoice.OutMaintainTime);
            UpdateMaintainInvoiceLines(undercartMaintainInvoice.MaintainInvoiceLines, updateUndercartMaintainInvoice);
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
                _invoiceRepository.GetMaintainInvoice(undercartMaintainInvoice.UndercartMaintainInvoiceId);
            //获取需要删除的对象。
            UpdateMaintainInvoiceLines(new List<MaintainInvoiceLineDTO>(), deleteUndercartMaintainInvoice);
            _invoiceRepository.Remove(deleteUndercartMaintainInvoice); //删除Undercart维修发票。
        }
        #endregion

        #region SpecialRefitInvoiceDTO

        /// <summary>
        ///     获取所有特修改装发票
        /// </summary>
        /// <returns></returns>
        public IQueryable<SpecialRefitInvoiceDTO> GetSpecialRefitInvoices()
        {
            var queryBuilder =
                new QueryBuilder<SpecialRefitInvoice>();
            return _maintainInvoiceQuery.SpecialRefitInvoiceDTOQuery(queryBuilder);
        }

        /// <summary>
        ///     新增特修改装发票。
        /// </summary>
        /// <param name="specialRefitInvoice">特修改装发票DTO。</param>
        [Insert(typeof(SpecialRefitInvoiceDTO))]
        public void InsertSpecialRefitInvoice(SpecialRefitInvoiceDTO specialRefitInvoice)
        {
            var newSpecialRefitInvoice = MaintainInvoiceFactory.CreateSpecialRefitInvoice();
            newSpecialRefitInvoice.SetInvoiceNumber(GetMaxInvoiceNumber());
            MaintainInvoiceFactory.SetMaintainInvoice(newSpecialRefitInvoice, "0",
                specialRefitInvoice.InvoideCode, specialRefitInvoice.InvoiceDate, specialRefitInvoice.SupplierName, specialRefitInvoice.SupplierId,
                specialRefitInvoice.InvoiceValue, specialRefitInvoice.PaidAmount, specialRefitInvoice.OperatorName,
               specialRefitInvoice.Reviewer, specialRefitInvoice.Status, specialRefitInvoice.CurrencyId,
               specialRefitInvoice.DocumentName, specialRefitInvoice.DocumentId, specialRefitInvoice.PaymentScheduleLineId,
               specialRefitInvoice.InMaintainTime, specialRefitInvoice.OutMaintainTime);
            if (specialRefitInvoice.MaintainInvoiceLines != null)
            {
                foreach (var maintainInvoiceLine in specialRefitInvoice.MaintainInvoiceLines)
                {
                    var newMaintainInvoiceLine = MaintainInvoiceFactory.CreateInvoiceLine();
                    MaintainInvoiceFactory.SetInvoiceLine(newMaintainInvoiceLine, maintainInvoiceLine.MaintainItem, maintainInvoiceLine.ItemName, 1, maintainInvoiceLine.Amount,
                 maintainInvoiceLine.Note);
                    newSpecialRefitInvoice.InvoiceLines.Add(newMaintainInvoiceLine);
                }
            }
            newSpecialRefitInvoice.SetInvoiceValue();
            _invoiceRepository.Add(newSpecialRefitInvoice);
        }

        /// <summary>
        ///     更新特修改装发票。
        /// </summary>
        /// <param name="specialRefitInvoice">特修改装发票DTO。</param>
        [Update(typeof(SpecialRefitInvoiceDTO))]
        public void ModifySpecialRefitInvoice(SpecialRefitInvoiceDTO specialRefitInvoice)
        {
            var updateSpecialRefitInvoice =
               _invoiceRepository.GetMaintainInvoice(specialRefitInvoice.SpecialRefitId);//获取需要更新的对象。
            MaintainInvoiceFactory.SetMaintainInvoice(updateSpecialRefitInvoice, "0",
                  specialRefitInvoice.InvoideCode, specialRefitInvoice.InvoiceDate, specialRefitInvoice.SupplierName, specialRefitInvoice.SupplierId,
                  specialRefitInvoice.InvoiceValue, specialRefitInvoice.PaidAmount, specialRefitInvoice.OperatorName,
                 specialRefitInvoice.Reviewer, specialRefitInvoice.Status, specialRefitInvoice.CurrencyId,
                 specialRefitInvoice.DocumentName, specialRefitInvoice.DocumentId, specialRefitInvoice.PaymentScheduleLineId,
                 specialRefitInvoice.InMaintainTime, specialRefitInvoice.OutMaintainTime);
            UpdateMaintainInvoiceLines(specialRefitInvoice.MaintainInvoiceLines, updateSpecialRefitInvoice);
            _invoiceRepository.Modify(updateSpecialRefitInvoice);
        }

        /// <summary>
        ///     删除特修改装发票。
        /// </summary>
        /// <param name="specialRefitInvoice">特修改装发票DTO。</param>
        [Delete(typeof(SpecialRefitInvoiceDTO))]
        public void DeleteSpecialRefitInvoice(SpecialRefitInvoiceDTO specialRefitInvoice)
        {
            if (specialRefitInvoice == null)
            {
                throw new ArgumentException("参数为空！");
            }
            var delSpecialRefitInvoice = _invoiceRepository.GetBasePurchaseInvoice(specialRefitInvoice.SpecialRefitId);
            //获取需要删除的对象。
            if (delSpecialRefitInvoice != null)
            {
                _invoiceRepository.DeleteInvoice(delSpecialRefitInvoice); //删除特修改装发票。
            }
        }

        #endregion

        #region 更新发票行集合
        /// <summary>
        /// 更新发票行集合
        /// </summary>
        /// <param name="sourceMaintainInvoiceLines">客户端集合</param>
        /// <param name="dstMaintainInvoice">数据库集合</param>
        private void UpdateMaintainInvoiceLines(IEnumerable<MaintainInvoiceLineDTO> sourceMaintainInvoiceLines, MaintainInvoice dstMaintainInvoice)
        {
            var maintainInvoiceLines = new List<MaintainInvoiceLine>();
            foreach (var sourceMaintainInvoiceLine in sourceMaintainInvoiceLines)
            {
                var result = dstMaintainInvoice.InvoiceLines.FirstOrDefault(p => p.Id == sourceMaintainInvoiceLine.MaintainInvoiceLineId);
                if (result == null)
                {
                    result = MaintainInvoiceFactory.CreateInvoiceLine();
                    result.ChangeCurrentIdentity(sourceMaintainInvoiceLine.MaintainInvoiceLineId);
                }
                MaintainInvoiceFactory.SetInvoiceLine(result, sourceMaintainInvoiceLine.MaintainItem, sourceMaintainInvoiceLine.ItemName, sourceMaintainInvoiceLine.UnitPrice, sourceMaintainInvoiceLine.Amount,
                    sourceMaintainInvoiceLine.Note);
                maintainInvoiceLines.Add(result);
            }
            dstMaintainInvoice.InvoiceLines.ToList().ForEach(p =>
                                                                     {
                                                                         if (maintainInvoiceLines.FirstOrDefault(t => t.Id == p.Id) == null)
                                                                         {
                                                                             _invoiceRepository.RemoveInvoiceLine(p);
                                                                         }
                                                                     });
            dstMaintainInvoice.InvoiceLines = maintainInvoiceLines;
            dstMaintainInvoice.SetInvoiceValue();
        }
        #endregion

        private static int _maxInvoiceNumber;
        private int GetMaxInvoiceNumber()
        {
            var date = DateTime.Now.Date.ToString("yyyyMMdd").Substring(0, 8);
            var noticeNumber = _invoiceRepository.GetAll().Max(p => p.InvoiceNumber);

            int seq = 1;
            if (!string.IsNullOrEmpty(noticeNumber) && noticeNumber.StartsWith(date))
            {
                seq = Int32.Parse(noticeNumber.Substring(8)) + 1;
            }
            if (seq <= _maxInvoiceNumber)
            {
                seq = _maxInvoiceNumber;
            }
            else
            {
                _maxInvoiceNumber = seq;
            }
            _maxInvoiceNumber++;
            return seq;
        }
    }
}
