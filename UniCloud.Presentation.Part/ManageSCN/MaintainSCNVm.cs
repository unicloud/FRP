#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/21 15:56:19
// 文件名：MaintainSCNVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/21 15:56:19
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.ManageSCN
{
    [Export(typeof(MaintainSCNVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MaintainSCNVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
       

        [ImportingConstructor]
        public MaintainSCNVm(IRegionManager regionManager, IPartService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            // 创建并注册CollectionView
           
        }

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     SCN类型
        /// </summary>
        public Array ScnTypes
        {
            get { return Enum.GetValues(typeof(ScnType)); }
        }

        #endregion

        #region 加载数据

        /// <summary>
        ///     加载数据方法
        ///     <remarks>
        ///         导航到此页面时调用。
        ///         可在此处将CollectionView的AutoLoad属性设为True，以实现数据的自动加载。
        ///     </remarks>
        /// </summary>
        public override void LoadData()
        {
            // 将CollectionView的AutoLoad属性设为True
           
        }

        #endregion

        #endregion

        #region 操作

        #region 创建新付款通知

        protected  void OnAddInvoice(object obj)
        {
            //PaymentNotice = new PaymentNoticeDTO
            //{
            //    PaymentNoticeId = RandomHelper.Next(),
            //    CreateDate = DateTime.Now,
            //    DeadLine = DateTime.Now,
            //    Status = 0,
            //};
            //PaymentNotices.AddNew(PaymentNotice);
        }

        protected  bool CanAddInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 删除付款通知

        protected  void OnRemoveInvoice(object obj)
        {
            //if (PaymentNotice == null)
            //{
            //    MessageAlert("请选择一条记录！");
            //    return;
            //}
            //MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            //{
            //    if (arg.DialogResult != true) return;
            //    PaymentNotices.Remove(_paymentNotice);
            //});
        }

        protected  bool CanRemoveInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 增加付款通知行

        protected  void OnAddInvoiceLine(object obj)
        {
            //if (PaymentNotice == null)
            //{
            //    MessageAlert("请选择一条付款通知记录！");
            //    return;
            //}

            //SelectInvoicesWindow = new SelectInvoices();
            //SelectInvoicesWindow.ViewModel.InitData(PaymentNotice);
            //SelectInvoicesWindow.ShowDialog();

            ////var maintainInvoiceLine = new PaymentNoticeLineDTO
            ////{
            ////    PaymentNoticeLineId = RandomHelper.Next(),
            ////};

            ////PaymentNotice.PaymentNoticeLines.Add(maintainInvoiceLine);
        }

        protected  bool CanAddInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 移除付款通知行

        protected  void OnRemoveInvoiceLine(object obj)
        {
            //if (PaymentNoticeLine == null)
            //{
            //    MessageAlert("请选择一条付款通知明细！");
            //    return;
            //}
            //MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            //{
            //    if (arg.DialogResult != true) return;
            //    PaymentNotice.PaymentNoticeLines.Remove(PaymentNoticeLine);
            //});
        }

        protected  bool CanRemoveInvoiceLine(object obj)
        {
            return true;
        }

        #endregion

        #region 提交付款通知

        protected  void OnSubmitInvoice(object obj)
        {
            //if (PaymentNotice == null)
            //{
            //    MessageAlert("请选择一条付款通知记录！");
            //    return;
            //}
            //PaymentNotice.Status = (int)PaymentNoticeStatus.待审核;
        }

        protected  bool CanSubmitInvoice(object obj)
        {
            return true;
        }

        #endregion

        #region 审核付款通知

        protected  void OnReviewInvoice(object obj)
        {
            //if (PaymentNotice == null)
            //{
            //    MessageAlert("请选择一条付款通知记录！");
            //    return;
            //}
            //PaymentNotice.Status = (int)PaymentNoticeStatus.已审核;
            //PaymentNotice.Reviewer = "admin";
            //PaymentNotice.ReviewDate = DateTime.Now;
        }

        protected  bool CanReviewInvoice(object obj)
        {
            return true;
        }

        #endregion

        #endregion
    }
}
