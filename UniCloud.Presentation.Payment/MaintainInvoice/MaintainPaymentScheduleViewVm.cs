#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/4 15:35:48
// 文件名：MaintainPaymentScheduleViewVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/4 15:35:48
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.MaintainInvoice
{
    [Export(typeof (MaintainPaymentScheduleViewVm))]
    public class MaintainPaymentScheduleViewVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        [ImportingConstructor]
        public MaintainPaymentScheduleViewVm(IPaymentService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitializeVM();
            InitializerCommand();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            MaintainPaymentSchedules = _service.CreateCollection(_context.MaintainPaymentSchedules,
                o => o.PaymentScheduleLines);
            MaintainPaymentSchedules.LoadedData += (o, e) =>
            {
                if (SelectMaintainPaymentSchedule == null)
                    SelectMaintainPaymentSchedule = MaintainPaymentSchedules.FirstOrDefault();
            };
            _service.RegisterCollectionView(MaintainPaymentSchedules); //注册查询集合。

            PaymentSchedules = new QueryableDataServiceCollectionView<PaymentScheduleDTO>(_context,
                _context.PaymentSchedules);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 币种集合

        /// <summary>
        ///     币种集合
        /// </summary>
        public QueryableDataServiceCollectionView<CurrencyDTO> Currencies { get; set; }

        #endregion

        #region 供应商集合

        /// <summary>
        ///     供应商集合
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        #endregion

        private Type _currentType;

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
        }

        public void InitData(Type type, EventHandler<WindowClosedEventArgs> closed)
        {
            prepayPayscheduleChildView.Tag = null;
            _currentType = type;
            prepayPayscheduleChildView.Closed -= closed;
            prepayPayscheduleChildView.Closed += closed;
            Currencies = _service.GetCurrency(() => RaisePropertyChanged(() => Currencies));
            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers));
            MaintainPaymentSchedules.Load(true);
            PaymentSchedules.Load(true);
        }

        #endregion

        #endregion

        #region 操作

        #endregion

        #region 子窗体相关操作

        [Import] public MaintainPaymentScheduleView prepayPayscheduleChildView; //初始化子窗体

        #region 付款计划集合

        private MaintainPaymentScheduleDTO _selectMaintainPaymentSchedule;
        private PaymentScheduleLineDTO _selectPaymentScheduleLine;

        /// <summary>
        ///     所有付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PaymentScheduleDTO> PaymentSchedules { get; set; }

        /// <summary>
        ///     维修付款计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainPaymentScheduleDTO> MaintainPaymentSchedules { get; set; }

        /// <summary>
        ///     选择的维修付款计划
        /// </summary>
        public MaintainPaymentScheduleDTO SelectMaintainPaymentSchedule
        {
            get { return _selectMaintainPaymentSchedule; }
            set
            {
                if (_selectMaintainPaymentSchedule != value)
                {
                    _selectMaintainPaymentSchedule = value;
                    if (_selectMaintainPaymentSchedule != null)
                    {
                        SelectPaymentScheduleLine = _selectMaintainPaymentSchedule.PaymentScheduleLines.FirstOrDefault();
                    }
                    RaisePropertyChanged(() => SelectMaintainPaymentSchedule);
                }
            }
        }

        public PaymentScheduleLineDTO SelectPaymentScheduleLine
        {
            get { return _selectPaymentScheduleLine; }
            set
            {
                _selectPaymentScheduleLine = value;
                RaisePropertyChanged(() => SelectPaymentScheduleLine);
            }
        }

        #endregion

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            prepayPayscheduleChildView.Close();
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancelExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            if (SelectMaintainPaymentSchedule != null)
            {
                if (SelectPaymentScheduleLine == null)
                {
                    MessageAlert("请选择一条付款计划行！");
                }
                else
                {
                    if (_currentType == typeof (AirframeMaintainInvoiceDTO))
                    {
                        var maintainInvoice = new AirframeMaintainInvoiceDTO
                        {
                            AirframeMaintainInvoiceId = RandomHelper.Next(),
                            CreateDate = DateTime.Now,
                            InvoiceDate = DateTime.Now,
                            InMaintainTime = DateTime.Now,
                            OutMaintainTime = DateTime.Now,
                            SupplierId = SelectMaintainPaymentSchedule.SupplierId,
                            SupplierName = SelectMaintainPaymentSchedule.SupplierName,
                            CurrencyId = SelectMaintainPaymentSchedule.CurrencyId,
                            PaymentScheduleLineId = SelectPaymentScheduleLine.PaymentScheduleLineId,
                            OperatorName = StatusData.curUser
                        };
                        prepayPayscheduleChildView.Tag = maintainInvoice;
                    }
                    else if (_currentType == typeof (APUMaintainInvoiceDTO))
                    {
                        var maintainInvoice = new APUMaintainInvoiceDTO
                        {
                            APUMaintainInvoiceId = RandomHelper.Next(),
                            CreateDate = DateTime.Now,
                            InvoiceDate = DateTime.Now,
                            InMaintainTime = DateTime.Now,
                            OutMaintainTime = DateTime.Now,
                            SupplierId = SelectMaintainPaymentSchedule.SupplierId,
                            SupplierName = SelectMaintainPaymentSchedule.SupplierName,
                            CurrencyId = SelectMaintainPaymentSchedule.CurrencyId,
                            PaymentScheduleLineId = SelectPaymentScheduleLine.PaymentScheduleLineId,
                        };
                        prepayPayscheduleChildView.Tag = maintainInvoice;
                    }
                    else if (_currentType == typeof (EngineMaintainInvoiceDTO))
                    {
                        var maintainInvoice = new EngineMaintainInvoiceDTO
                        {
                            EngineMaintainInvoiceId = RandomHelper.Next(),
                            CreateDate = DateTime.Now,
                            InvoiceDate = DateTime.Now,
                            InMaintainTime = DateTime.Now,
                            OutMaintainTime = DateTime.Now,
                            SupplierId = SelectMaintainPaymentSchedule.SupplierId,
                            SupplierName = SelectMaintainPaymentSchedule.SupplierName,
                            CurrencyId = SelectMaintainPaymentSchedule.CurrencyId,
                            PaymentScheduleLineId = SelectPaymentScheduleLine.PaymentScheduleLineId,
                        };
                        prepayPayscheduleChildView.Tag = maintainInvoice;
                    }
                    else if (_currentType == typeof (UndercartMaintainInvoiceDTO))
                    {
                        var maintainInvoice = new UndercartMaintainInvoiceDTO
                        {
                            UndercartMaintainInvoiceId = RandomHelper.Next(),
                            CreateDate = DateTime.Now,
                            InvoiceDate = DateTime.Now,
                            InMaintainTime = DateTime.Now,
                            OutMaintainTime = DateTime.Now,
                            SupplierId = SelectMaintainPaymentSchedule.SupplierId,
                            SupplierName = SelectMaintainPaymentSchedule.SupplierName,
                            CurrencyId = SelectMaintainPaymentSchedule.CurrencyId,
                            PaymentScheduleLineId = SelectPaymentScheduleLine.PaymentScheduleLineId,
                        };
                        prepayPayscheduleChildView.Tag = maintainInvoice;
                    }
                    else
                    {
                        var maintainInvoice = new SpecialRefitInvoiceDTO
                        {
                            SpecialRefitId = RandomHelper.Next(),
                            CreateDate = DateTime.Now,
                            InvoiceDate = DateTime.Now,
                            SupplierId = SelectMaintainPaymentSchedule.SupplierId,
                            SupplierName = SelectMaintainPaymentSchedule.SupplierName,
                            CurrencyId = SelectMaintainPaymentSchedule.CurrencyId,
                            PaymentScheduleLineId = SelectPaymentScheduleLine.PaymentScheduleLineId,
                        };
                        prepayPayscheduleChildView.Tag = maintainInvoice;
                    }
                    prepayPayscheduleChildView.Close();
                }
            }
            else
            {
                MessageAlert("未选中维修付款计划！");
            }
        }


        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommitExecute(object sender)
        {
            return true;
        }

        #endregion

        #endregion

        #endregion
    }
}