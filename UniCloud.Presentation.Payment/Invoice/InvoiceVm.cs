#region Version Info
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2013/12/17 21:07:04
// 文件名：InvoiceVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2013/12/17 21:07:04
// 修改说明：
// ========================================================================*/
#endregion
using System;
using Microsoft.Practices.Prism.Commands;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

namespace UniCloud.Presentation.Payment.Invoice
{
    public class InvoiceVm : EditViewModelBase
    {
        protected PaymentData PaymentDataService;

        public InvoiceVm()
        {
            AddInvoiceCommand = new DelegateCommand<object>(OnAddInvoice, CanAddInvoice);
            RemoveInvoiceCommand = new DelegateCommand<object>(OnRemoveInvoice, CanRemoveInvoice);
            AddInvoiceLineCommand = new DelegateCommand<object>(OnAddInvoiceLine, CanAddInvoiceLine);
            RemoveInvoiceLineCommand = new DelegateCommand<object>(OnRemoveInvoiceLine, CanRemoveInvoiceLine);
            SubmitInvoiceCommand = new DelegateCommand<object>(OnSubmitInvoice, CanSubmitInvoice);
            ReviewInvoiceCommand = new DelegateCommand<object>(OnReviewInvoice, CanReviewInvoice);
        }

        #region 创建新发票
        /// <summary>
        ///  创建新发票
        /// </summary>
        public DelegateCommand<object> AddInvoiceCommand { get; set; }
        protected virtual void OnAddInvoice(object obj)
        {
        }

        protected virtual bool CanAddInvoice(object obj)
        {
            return true;
        }
        #endregion

        #region 删除发票
        /// <summary>
        ///     删除发票
        /// </summary>
        public DelegateCommand<object> RemoveInvoiceCommand { get; set; }

        protected virtual void OnRemoveInvoice(object obj)
        {
        }

        protected virtual bool CanRemoveInvoice(object obj)
        {
            return true;
        }
        #endregion

        #region 增加维修发票行
        /// <summary>
        ///     增加维修发票行
        /// </summary>
        public DelegateCommand<object> AddInvoiceLineCommand { get; set; }

        protected virtual void OnAddInvoiceLine(object obj)
        {
        }

        protected virtual bool CanAddInvoiceLine(object obj)
        {
            return true;
        }
        #endregion

        #region 移除维修发票行
        /// <summary>
        ///     移除维修发票行
        /// </summary>
        public DelegateCommand<object> RemoveInvoiceLineCommand { get; private set; }

        protected virtual void OnRemoveInvoiceLine(object obj)
        {
        }

        protected virtual bool CanRemoveInvoiceLine(object obj)
        {
            return true;
        }
        #endregion

        #region 提交发票
        /// <summary>
        ///  提交发票
        /// </summary>
        public DelegateCommand<object> SubmitInvoiceCommand { get; set; }
        protected virtual void OnSubmitInvoice(object obj)
        {
        }

        protected virtual bool CanSubmitInvoice(object obj)
        {
            return true;
        }
        #endregion

        #region 审核发票
        /// <summary>
        ///  审核发票
        /// </summary>
        public DelegateCommand<object> ReviewInvoiceCommand { get; set; }

        protected virtual void OnReviewInvoice(object obj)
        {
        }

        protected virtual bool CanReviewInvoice(object obj)
        {
            return true;
        }
        #endregion

        protected override IService CreateService()
        {
            PaymentDataService = new PaymentData(AgentHelper.PaymentUri);
            return new PaymentService(PaymentDataService);
        }

        public override void LoadData()
        {
        }
    }
}
