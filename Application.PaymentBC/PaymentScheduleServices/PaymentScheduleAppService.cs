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
using UniCloud.Application.PaymentBC.Query.PaymentScheduleQueries;
using UniCloud.Domain.PaymentBC.Aggregates.PaymentScheduleAgg;

#endregion

namespace UniCloud.Application.PaymentBC.PaymentScheduleServices
{
    /// <summary>
    ///     实现发动机维修发票接口。
    ///     用于处于维修发票相关信息的服务，供Distributed Services调用。
    /// </summary>
   [LogAOP]
    public class PaymentScheduleAppService : ContextBoundObject, IPaymentScheduleAppService
    {
        private readonly IPaymentScheduleQuery _paymentScheduleQuery;
        private readonly IPaymentScheduleRepository _paymentScheduleRepository;

        public PaymentScheduleAppService(IPaymentScheduleQuery paymentScheduleQuery,
                                         IPaymentScheduleRepository paymentScheduleRepository)
        {
            _paymentScheduleQuery = paymentScheduleQuery;
            _paymentScheduleRepository = paymentScheduleRepository;
        }

        #region 所有付款计划

        /// <summary>
        ///     查询所有付款计划
        /// </summary>
        /// <returns></returns>
        public IQueryable<PaymentScheduleDTO> GetPaymentSchedules()
        {
            var query = new QueryBuilder<PaymentSchedule>();
            return _paymentScheduleQuery.PaymentSchedulesQuery(query);
        }

        #endregion

        #region 飞机付款计划

        /// <summary>
        ///     查询飞机付款计划
        /// </summary>
        /// <returns></returns>
        public IQueryable<AcPaymentScheduleDTO> GetAcPaymentSchedules()
        {
            var query = new QueryBuilder<PaymentSchedule>();
            return _paymentScheduleQuery.AcPaymentSchedulesQuery(query);
        }

        /// <summary>
        ///     新增飞机付款计划
        /// </summary>
        /// <param name="acPaymentSchedule"></param>
        [Insert(typeof (AcPaymentScheduleDTO))]
        public void InsertAcPaymentSchedule(AcPaymentScheduleDTO acPaymentSchedule)
        {
            if (acPaymentSchedule == null)
            {
                throw new Exception("飞机付款计划不能为空");
            }

            var newAcPaymentSchedule = PaymentScheduleFactory.CreateAcPaymentSchedule(acPaymentSchedule.SupplierName,
                                                                                      acPaymentSchedule.SupplierId,
                                                                                      acPaymentSchedule.CurrencyId,
                                                                                      acPaymentSchedule.ContractAcId);
            InsertPaymentSchedule(newAcPaymentSchedule, acPaymentSchedule.PaymentScheduleLines); //新增飞机付款计划
        }

        /// <summary>
        ///     更新飞机付款计划
        /// </summary>
        /// <param name="acPaymentSchedule"></param>
        [Update(typeof (AcPaymentScheduleDTO))]
        public void ModifyAcPaymentSchedule(AcPaymentScheduleDTO acPaymentSchedule)
        {
            if (acPaymentSchedule == null)
            {
                throw new Exception("飞机付款计划不能为空");
            }
            var persistAcPayment =
                _paymentScheduleRepository.Get(acPaymentSchedule.AcPaymentScheduleId) as AircraftPaymentSchedule;
            if (persistAcPayment == null)
            {
                throw new Exception("找不到需要更新的付款计划");
            }
            //更新飞机付款计划
            if (!persistAcPayment.SupplierId.Equals(acPaymentSchedule.SupplierId))
            {
                persistAcPayment.SetSupplier(acPaymentSchedule.SupplierId, acPaymentSchedule.SupplierName);
            }
            if (!persistAcPayment.CurrencyId.Equals(acPaymentSchedule.CurrencyId))
            {
                persistAcPayment.SetCurrency(acPaymentSchedule.CurrencyId);
            }
            if (!persistAcPayment.ContractAircraftId.Equals(acPaymentSchedule.ContractAcId))
            {
                persistAcPayment.SetContractAircraft(acPaymentSchedule.ContractAcId);
            }

            UpdatePaymentSchedule(persistAcPayment, acPaymentSchedule.PaymentScheduleLines); //更新飞机付款计划
        }

        /// <summary>
        ///     删除飞机付款计划
        /// </summary>
        /// <param name="acPaymentSchedule"></param>
        [Delete(typeof (AcPaymentScheduleDTO))]
        public void DeleteAcPaymentSchedule(AcPaymentScheduleDTO acPaymentSchedule)
        {
            if (acPaymentSchedule == null)
            {
                throw new Exception("飞机付款计划不能为空");
            }
            DeletePaymentSchedule(acPaymentSchedule.AcPaymentScheduleId); //删除飞机付款计划
        }

        #endregion

        #region 发动机付款计划

        /// <summary>
        ///     发动机付款计划
        /// </summary>
        /// <returns></returns>
        public IQueryable<EnginePaymentScheduleDTO> GetEnginePaymentSchedules()
        {
            var query = new QueryBuilder<PaymentSchedule>();
            return _paymentScheduleQuery.EnginePaymentSchedulesQuery(query);
        }

        /// <summary>
        ///     新增发动机付款计划
        /// </summary>
        /// <param name="eginePaymentSchedule"></param>
        [Insert(typeof (EnginePaymentScheduleDTO))]
        public void InsertEnginePaymentSchedule(EnginePaymentScheduleDTO eginePaymentSchedule)
        {
            if (eginePaymentSchedule == null)
            {
                throw new Exception("发动机付款计划不能为空");
            }

            var newEnginePaymentSchedule =
                PaymentScheduleFactory.CreateEnginePaymentSchedule(eginePaymentSchedule.SupplierName,
                                                                   eginePaymentSchedule.SupplierId,
                                                                   eginePaymentSchedule.CurrencyId,
                                                                   eginePaymentSchedule.ContractEngineId);
            InsertPaymentSchedule(newEnginePaymentSchedule, eginePaymentSchedule.PaymentScheduleLines); //新增发动机付款计划
        }

        /// <summary>
        ///     更新发动机付款计划
        /// </summary>
        /// <param name="eginePaymentSchedule"></param>
        [Update(typeof (EnginePaymentScheduleDTO))]
        public void ModifyEnginePaymentSchedule(EnginePaymentScheduleDTO eginePaymentSchedule)
        {
            if (eginePaymentSchedule == null)
            {
                throw new Exception("发动机付款计划不能为空");
            }
            var persistEnginePayment =
                _paymentScheduleRepository.Get(eginePaymentSchedule.EnginePaymentScheduleId) as EnginePaymentSchedule;
            if (persistEnginePayment == null)
            {
                throw new Exception("找不到需要更新的付款计划");
            }
            //更新飞机付款计划
            if (!persistEnginePayment.SupplierId.Equals(eginePaymentSchedule.SupplierId))
            {
                persistEnginePayment.SetSupplier(eginePaymentSchedule.SupplierId, eginePaymentSchedule.SupplierName);
            }
            if (!persistEnginePayment.CurrencyId.Equals(eginePaymentSchedule.CurrencyId))
            {
                persistEnginePayment.SetCurrency(eginePaymentSchedule.CurrencyId);
            }
            if (!persistEnginePayment.ContractEngineId.Equals(eginePaymentSchedule.ContractEngineId))
            {
                persistEnginePayment.SetContractEngine(eginePaymentSchedule.ContractEngineId);
            }

            UpdatePaymentSchedule(persistEnginePayment, eginePaymentSchedule.PaymentScheduleLines); //更新发动机付款计划
        }

        /// <summary>
        ///     删除发动机付款计划
        /// </summary>
        /// <param name="eginePaymentSchedule"></param>
        [Delete(typeof (EnginePaymentScheduleDTO))]
        public void DeleteEnginePaymentSchedule(EnginePaymentScheduleDTO eginePaymentSchedule)
        {
            if (eginePaymentSchedule == null)
            {
                throw new Exception("发动机付款计划不能为空");
            }
            DeletePaymentSchedule(eginePaymentSchedule.EnginePaymentScheduleId);
        }

        #endregion

        #region 标准付款计划

        /// <summary>
        ///     获取所有标准付款计划
        /// </summary>
        /// <returns>所有标准付款计划</returns>
        public IQueryable<StandardPaymentScheduleDTO> GetStandardPaymentSchedules()
        {
            var query = new QueryBuilder<PaymentSchedule>();
            return _paymentScheduleQuery.StandardPaymentSchedulesQuery(query);
        }

        /// <summary>
        ///     新增标准付款计划
        /// </summary>
        /// <param name="standardPaymentSchedule">标准付款计划DTO。</param>
        [Insert(typeof (StandardPaymentScheduleDTO))]
        public void InsertStandardPaymentSchedule(StandardPaymentScheduleDTO standardPaymentSchedule)
        {
            if (standardPaymentSchedule == null)
            {
                throw new Exception("付款计划不能为空");
            }

            var newStandardPaymentSchedule =
                PaymentScheduleFactory.CreateStandardPaymentSchedule(standardPaymentSchedule.SupplierName,
                                                                                      standardPaymentSchedule.SupplierId,
                                                                                      standardPaymentSchedule.CurrencyId,
                                                                                      standardPaymentSchedule.OrderId);
            InsertPaymentSchedule(newStandardPaymentSchedule, standardPaymentSchedule.PaymentScheduleLines); //新增飞机付款计划
        }

        /// <summary>
        ///     修改标准付款计划
        /// </summary>
        /// <param name="standardPaymentSchedule">标准付款计划DTO。</param>
        [Update(typeof (StandardPaymentScheduleDTO))]
        public void ModifyStandardPaymentSchedule(StandardPaymentScheduleDTO standardPaymentSchedule)
        {
            if (standardPaymentSchedule == null)
            {
                throw new Exception("付款计划不能为空");
            }
            var persistStandardPayment =
                _paymentScheduleRepository.Get(standardPaymentSchedule.StandardPaymentScheduleId) as
                    StandardPaymentSchedule;
            if (persistStandardPayment == null)
            {
                throw new Exception("找不到需要更新的付款计划");
            }
            //更新标准付款计划
            if (!persistStandardPayment.SupplierId.Equals(standardPaymentSchedule.SupplierId))
            {
                persistStandardPayment.SetSupplier(standardPaymentSchedule.SupplierId,
                    standardPaymentSchedule.SupplierName);
            }
            if (!persistStandardPayment.CurrencyId.Equals(standardPaymentSchedule.CurrencyId))
            {
                persistStandardPayment.SetCurrency(standardPaymentSchedule.CurrencyId);
            }
            if (!persistStandardPayment.OrderId.Equals(standardPaymentSchedule.OrderId))
            {
                persistStandardPayment.SetOrderId(standardPaymentSchedule.OrderId);
            }

            UpdatePaymentSchedule(persistStandardPayment, standardPaymentSchedule.PaymentScheduleLines); //更新标准付款计划
        }

        /// <summary>
        ///     删除标准付款计划
        /// </summary>
        /// <param name="standardPaymentSchedule">标准付款计划DTO。</param>
        [Delete(typeof (StandardPaymentScheduleDTO))]
        public void DeleteStandardPaymentSchedule(StandardPaymentScheduleDTO standardPaymentSchedule)
        {
            if (standardPaymentSchedule == null)
            {
                throw new Exception("飞机付款计划不能为空");
            }
            DeletePaymentSchedule(standardPaymentSchedule.StandardPaymentScheduleId); //删除标准付款计划
        }

        #endregion

        #region 维修付款计划

        /// <summary>
        ///     获取所有维修付款计划
        /// </summary>
        /// <returns>所有维修付款计划</returns>
        public IQueryable<MaintainPaymentScheduleDTO> GetMaintainPaymentSchedules()
        {
            var query = new QueryBuilder<PaymentSchedule>();
            return _paymentScheduleQuery.MaintainPaymentSchedulesQuery(query);
        }

        /// <summary>
        ///     新增维修付款计划
        /// </summary>
        /// <param name="maintainPaymentSchedule">维修付款计划DTO。</param>
        [Insert(typeof(MaintainPaymentScheduleDTO))]
        public void InsertMaintainPaymentSchedule(MaintainPaymentScheduleDTO maintainPaymentSchedule)
        {
            if (maintainPaymentSchedule == null)
            {
                throw new Exception("付款计划不能为空");
            }

            var newMaintainPaymentSchedule =
                PaymentScheduleFactory.CreateMaintainPaymentSchedule(maintainPaymentSchedule.SupplierName,
                                                                                      maintainPaymentSchedule.SupplierId,
                                                                                      maintainPaymentSchedule.CurrencyId);
            InsertPaymentSchedule(newMaintainPaymentSchedule, maintainPaymentSchedule.PaymentScheduleLines); //新增飞机付款计划
        }

        /// <summary>
        ///     修改维修付款计划
        /// </summary>
        /// <param name="maintainPaymentSchedule">维修付款计划DTO。</param>
        [Update(typeof(MaintainPaymentScheduleDTO))]
        public void ModifyMaintainPaymentSchedule(MaintainPaymentScheduleDTO maintainPaymentSchedule)
        {
            if (maintainPaymentSchedule == null)
            {
                throw new Exception("付款计划不能为空");
            }
            var persistMaintainPayment =
                _paymentScheduleRepository.Get(maintainPaymentSchedule.MaintainPaymentScheduleId) as
                    MaintainPaymentSchedule;
            if (persistMaintainPayment == null)
            {
                throw new Exception("找不到需要更新的付款计划");
            }
            //更新维修付款计划
            if (!persistMaintainPayment.SupplierId.Equals(maintainPaymentSchedule.SupplierId))
            {
                persistMaintainPayment.SetSupplier(maintainPaymentSchedule.SupplierId,
                    maintainPaymentSchedule.SupplierName);
            }
            if (!persistMaintainPayment.CurrencyId.Equals(maintainPaymentSchedule.CurrencyId))
            {
                persistMaintainPayment.SetCurrency(maintainPaymentSchedule.CurrencyId);
            }

            UpdatePaymentSchedule(persistMaintainPayment, maintainPaymentSchedule.PaymentScheduleLines); //更新维修付款计划
        }

        /// <summary>
        ///     删除维修付款计划
        /// </summary>
        /// <param name="maintainPaymentSchedule">维修付款计划DTO。</param>
        [Delete(typeof(MaintainPaymentScheduleDTO))]
        public void DeleteMaintainPaymentSchedule(MaintainPaymentScheduleDTO maintainPaymentSchedule)
        {
            if (maintainPaymentSchedule == null)
            {
                throw new Exception("飞机付款计划不能为空");
            }
            DeletePaymentSchedule(maintainPaymentSchedule.MaintainPaymentScheduleId); //删除维修付款计划
        }

        #endregion

        /// <summary>
        ///     新增付款计划
        /// </summary>
        /// <param name="paymentSchedule"></param>
        /// <param name="addingPaymentScheduleLines">需要增加的付款计划明细</param>
        private void InsertPaymentSchedule(PaymentSchedule paymentSchedule,
            List<PaymentScheduleLineDTO> addingPaymentScheduleLines)
        {
            addingPaymentScheduleLines.ForEach(p => paymentSchedule
                                                    .AddPaymentScheduleLine(p.ScheduleDate,
                    p.Amount, p.Subject, p.Body, p.Importance, p.ProcessStatus, p.Start, p.End, p.IsAllDayEvent));
            _paymentScheduleRepository.Add(paymentSchedule);
        }

        /// <summary>
        ///     删除付款计划
        /// </summary>
        /// <param name="paymentScheduleId"></param>
        private void DeletePaymentSchedule(int paymentScheduleId)
        {
            var deletingPaySchedule = _paymentScheduleRepository.Get(paymentScheduleId);
            if (deletingPaySchedule == null)
            {
                throw new Exception("找不到需要删除的付款计划");
            }
            deletingPaySchedule.PaymentScheduleLines.ToList()
                .ForEach(p => _paymentScheduleRepository.RemovePaymentScheduleLine(p)
                );
            _paymentScheduleRepository.Remove(deletingPaySchedule);
        }

        /// <summary>
        ///     更新付款计划
        /// </summary>
        /// <param name="persistPaymentSchedule"></param>
        /// <param name="paymentScheduleLines"></param>
        private void UpdatePaymentSchedule(PaymentSchedule persistPaymentSchedule,
                                           List<PaymentScheduleLineDTO> paymentScheduleLines)
        {
            var addingPaymentDetail = new List<PaymentScheduleLineDTO>(); //需要添加的付款计划明细
            var deletingPaymentDetail = new List<PaymentScheduleLine>(); //需要删除的付款计划明细 
            paymentScheduleLines.ForEach(p =>
                {
                    //存在付款计划行
                    var persistPaymentScheduleLine =
                        persistPaymentSchedule.PaymentScheduleLines.FirstOrDefault(c => c.Id == p.PaymentScheduleLineId);
                    //更新
                    if (persistPaymentScheduleLine != null)
                    {
                        if (!persistPaymentScheduleLine.ScheduleDate.Equals(p.ScheduleDate))
                        {
                            persistPaymentScheduleLine.SetScheduleDate(p.ScheduleDate);
                        }
                        if (!persistPaymentScheduleLine.Amount.Equals(p.Amount))
                        {
                            persistPaymentScheduleLine.SetAmount(p.Amount);
                        }
                        if (!persistPaymentScheduleLine.Amount.Equals(p.Amount))
                        {
                            persistPaymentScheduleLine.SetAmount(p.Amount);
                        }
                    }
                    else
                {
                    //需要添加的付款计划明细,添加项
                        addingPaymentDetail.Add(p);
                    }
                });
            persistPaymentSchedule.PaymentScheduleLines.ToList().ForEach(p =>
                {
                    //存在付款计划行
                    var persistPaymentScheduleLine =
                        paymentScheduleLines.FirstOrDefault(c => c.PaymentScheduleLineId == p.Id);
                    if (persistPaymentScheduleLine == null)
                    {
                        deletingPaymentDetail.Add(p);
                    }
                });

            addingPaymentDetail.ForEach(p => persistPaymentSchedule
                .AddPaymentScheduleLine(p.ScheduleDate,
                    p.Amount, p.Subject, p.Body, p.Importance, p.ProcessStatus, p.Start, p.End, p.IsAllDayEvent));
            deletingPaymentDetail.ForEach(p => _paymentScheduleRepository.RemovePaymentScheduleLine(p));
        }
    }
}