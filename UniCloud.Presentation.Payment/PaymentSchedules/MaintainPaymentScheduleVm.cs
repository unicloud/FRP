#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/4/30 13:39:02
// 文件名：MaintainPaymentScheduleVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/4/30 13:39:02
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Globalization;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Payment.PaymentSchedules.PaymentScheduleExtension;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof(MaintainPaymentScheduleVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MaintainPaymentScheduleVm : EditViewModelBase
    {
        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public MaintainPaymentScheduleVm(IPaymentService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialPaymentSchedule(); //初始化付款计划
            InitialCommand(); //初始化命令
            InitialCurrency(); //初始化币种
        }


        #region 合同飞机下的付款计划相关

        private PaymentScheduleLineDTO _selectPaymentScheduleLine;
        private MaintainPaymentScheduleDTO _selectedPaymentSchedule;

        /// <summary>
        ///     选择付款计划
        /// </summary>
        public MaintainPaymentScheduleDTO SelectedPaymentSchedule
        {
            get { return _selectedPaymentSchedule; }
            set
            {
                _selectedPaymentSchedule = value;
                AddPaymentAppointments();
                RaisePropertyChanged(() => SelectedPaymentSchedule);
            }
        }

        /// <summary>
        ///     选中付款计划明细
        /// </summary>
        public PaymentScheduleLineDTO SelectPaymentScheduleLine
        {
            get { return _selectPaymentScheduleLine; }
            set
            {
                _selectPaymentScheduleLine = value;
                RaisePropertyChanged(() => SelectPaymentScheduleLine);
                DelPaymentScheduleLineCommand.RaiseCanExecuteChanged(); //刷新删除按钮状态
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainPaymentScheduleDTO> PaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialPaymentSchedule()
        {
            PaymentSchedulesView = _service.CreateCollection(_context.MaintainPaymentSchedules, o => o.PaymentScheduleLines);
            PaymentSchedulesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedPaymentSchedule = e.Entities.Cast<MaintainPaymentScheduleDTO>().FirstOrDefault();
                RefreshCommandState(); //刷新按钮状态
            };
            PaymentSchedulesView.PropertyChanged += (s, o) => { };
        }

        #endregion

        #region 加载币种

        /// <summary>
        ///     获取所有币种。
        /// </summary>
        public QueryableDataServiceCollectionView<CurrencyDTO> CurrencysView { get; set; }
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }
        /// <summary>
        ///     初始化币种信息。
        /// </summary>
        private void InitialCurrency()
        {
            CurrencysView = _service.CreateCollection(_context.Currencies);
            Suppliers = _service.CreateCollection(_context.Suppliers);
        }

        #endregion

        #region 命令

        #region 新增飞机付款计划命令

        public DelegateCommand<object> AddPaymentScheduleCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddPaymentSchedule(object sender)
        {
            //新增飞机付款计划
            SelectedPaymentSchedule = new MaintainPaymentScheduleDTO
            {
                MaintainPaymentScheduleId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
            };
            var supplier = Suppliers.FirstOrDefault();
            if (supplier != null)
            {
                SelectedPaymentSchedule.SupplierId = supplier.SupplierId;
                SelectedPaymentSchedule.SupplierName = supplier.Name;
            }
            //美元作为默认选中货币
            var currencyDto = CurrencysView.FirstOrDefault(p => p.Name.Equals("美元"));
            if (currencyDto != null)
                SelectedPaymentSchedule.CurrencyId = currencyDto.Id;
            PaymentSchedulesView.AddNewItem(SelectedPaymentSchedule);
            RefreshCommandState(); //刷新按钮状态
        }

        /// <summary>
        ///     判断新增飞机付款计划命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentSchedule(object sender)
        {
            return true;
        }

        #endregion

        #region 删除机付款计划行命令

        public DelegateCommand<object> DelPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行删除付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelPaymentScheduleLine(object sender)
        {
            var selPayment = sender as PaymentScheduleLineDTO;
            if (selPayment == null)
            {
                MessageAlert("提示", "请选择需要删除的付款计划明细");
                return;
            }
            MessageConfirm("提示", "是否删除改记录", (s, e) =>
            {
                if (e.DialogResult == true)
                {
                    SelectedPaymentSchedule.PaymentScheduleLines.Remove(selPayment);
                    //在删除计划明细的同时，需要删除日程控件的中Appointment
                    var paymentAppointment = _paymentAppointmentCollection.FirstOrDefault(
                        p => p.UniqueId == selPayment.PaymentScheduleLineId.ToString(CultureInfo.InvariantCulture));
                    if (paymentAppointment != null)
                    {
                        _paymentAppointmentCollection.Remove(paymentAppointment);
                        RaisePropertyChanged(() => PaymentAppointmentCollection);
                    }
                    RefreshCommandState(); //刷新按钮状态
                }
            });
        }

        /// <summary>
        ///     判断删除飞机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDelPaymentScheduleLine(object sender)
        {
            return true;
        }

        #endregion

        #region 增加付款计划行

        public DelegateCommand<object> AddPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行新增付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAddPaymentScheduleLine(object sender)
        {
            if (SelectedPaymentSchedule == null)
            {
                MessageAlert("提示", "付款计划不能为空");
                return;
            }
            var view = sender as RadScheduleView;
            if (view != null)
            {
                RadScheduleViewCommands.CreateAppointmentWithDialog.Execute(null, view);
            }
        }

        /// <summary>
        ///     判断新增飞机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentScheduleLine(object sender)
        {
            if (PaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedPaymentSchedule != null && !SelectedPaymentSchedule.IsCompleted;
        }

        #endregion

        #region 编辑付款计划行

        public DelegateCommand<object> EditPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行编辑付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnEditPaymentScheduleLine(object sender)
        {
            var selPayment = sender as PaymentScheduleLineDTO;
            if (selPayment == null)
            {
                MessageAlert("提示", "请选择需要删除的付款计划明细");
                return;
            }

            if (_scheduleView != null)
            {
                var ediAppointment =
                    PaymentAppointmentCollection.FirstOrDefault(
                        p => p.UniqueId == selPayment.PaymentScheduleLineId.ToString(CultureInfo.InvariantCulture));
                if (ediAppointment != null)
                {
                    RadScheduleViewCommands.EditAppointment.Execute(ediAppointment, _scheduleView);
                }
            }
        }

        /// <summary>
        ///     判断编辑飞机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>编辑命令是否可用。</returns>
        public bool CanEditPaymentScheduleLine(object sender)
        {
            return true;
        }

        #endregion

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialCommand()
        {
            AddPaymentScheduleCommand = new DelegateCommand<object>(OnAddPaymentSchedule, CanAddPaymentSchedule);
            DelPaymentScheduleLineCommand = new DelegateCommand<object>(OnDelPaymentScheduleLine,
                CanDelPaymentScheduleLine);
            AddPaymentScheduleLineCommand = new DelegateCommand<object>(OnAddPaymentScheduleLine,
                CanAddPaymentScheduleLine);
            EditPaymentScheduleLineCommand = new DelegateCommand<object>(OnEditPaymentScheduleLine,
                CanEditPaymentScheduleLine);
        }

        #endregion

        #region Schedule相关处理

        private readonly PaymentAppointmentCollection _paymentAppointmentCollection = new PaymentAppointmentCollection();
        private RadScheduleView _scheduleView;

        public bool ScheduleViewEnable
        {
            get
            {
                if (PaymentSchedulesView.IsSubmittingChanges)
                {
                    return false;
                }
                return SelectedPaymentSchedule != null;
            }
        }

        /// <summary>
        ///     付款计划Appointment集合
        /// </summary>
        public PaymentAppointmentCollection PaymentAppointmentCollection
        {
            get { return _paymentAppointmentCollection; }
        }

        /// <summary>
        ///     任务状态集合
        /// </summary>
        public CategoryCollection Categories
        {
            get { return AppointmentConvertHelper.GetCategoryCollection(); }
        }

        /// <summary>
        ///     添加PaymentAppointment集合
        /// </summary>
        private void AddPaymentAppointments()
        {
            _paymentAppointmentCollection.Clear();
            if (SelectedPaymentSchedule != null)
            {
                _paymentAppointmentCollection.ConvertAppointmentAndAdd(SelectedPaymentSchedule.PaymentScheduleLines);
            }
            RaisePropertyChanged(() => PaymentAppointmentCollection);
        }

        /// <summary>
        ///     付款计划Appointment转化为付款计划行DTO
        /// </summary>
        private void AppointmentToPaymentLineConvert(List<PaymentAppointment> paymentAppointments)
        {
            paymentAppointments.ForEach(p =>
            {
                var paymentLine = AppointmentConvertHelper.ConvertToPaymentScheduleLine(p);
                SelectedPaymentSchedule.PaymentScheduleLines.Add(paymentLine);
            });
        }

        /// <summary>
        ///     付款计划日程控件弹出子窗体句柄
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PaymentScheduleView_OnShowDialog(object sender, ShowDialogEventArgs e)
        {
            var schedule = sender as RadScheduleView;
            if (schedule == null)
            {
                return;
            }
            if (e.DialogViewModel is AppointmentDialogViewModel)
            {
                var viewModel = e.DialogViewModel as AppointmentDialogViewModel;
                if (viewModel.ViewMode == AppointmentViewMode.Edit)
                {
                    var appointment = schedule.EditedAppointment as PaymentAppointment;
                    if (appointment != null)
                    {
                        appointment.EditRecurrenceDialog = false;
                    }
                    return;
                }
            }
            if (e.DialogViewModel is RecurrenceDialogViewModel)
            {
                var viewModel = e.DialogViewModel as RecurrenceDialogViewModel;
                viewModel.RecurrenceRangeType = RecurrenceRangeType.RepeatUntil;
            }
        }

        /// <summary>
        ///     删除Appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PaymentScheduleView_OnAppointmentDeleted(object sender, AppointmentDeletedEventArgs e)
        {
            if (SelectedPaymentSchedule == null)
            {
                MessageAlert("提示", "付款计划不能为空");
                return;
            }
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = e.Appointment as PaymentAppointment;
                if (appointment != null)
                {
                    var paymentScheduleLine =
                        SelectedPaymentSchedule.PaymentScheduleLines.FirstOrDefault(
                            p => p.PaymentScheduleLineId == int.Parse(appointment.UniqueId));
                    if (paymentScheduleLine != null)
                    {
                        SelectedPaymentSchedule.PaymentScheduleLines.Remove(paymentScheduleLine);
                    }
                }
            }
        }

        /// <summary>
        ///     编辑Appointment
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PaymentScheduleView_OnAppointmentEdited(object sender, AppointmentEditedEventArgs e)
        {
            if (SelectedPaymentSchedule == null)
            {
                MessageAlert("提示", "付款计划不能为空");
                return;
            }
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = e.Appointment as PaymentAppointment;
                if (appointment != null)
                {
                    var paymentScheduleLine =
                        SelectedPaymentSchedule.PaymentScheduleLines.FirstOrDefault(
                            p => p.PaymentScheduleLineId == int.Parse(appointment.UniqueId));
                    if (paymentScheduleLine != null)
                    {
                        AppointmentConvertHelper.ConvertToPaymentScheduleLine(appointment, paymentScheduleLine);
                    }
                }
            }
        }

        /// <summary>
        ///     增加付款计划行
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void PaymentScheduleView_OnAppointmentCreated(object sender, AppointmentCreatedEventArgs e)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = e.CreatedAppointment as PaymentAppointment;
                if (appointment == null)
                {
                    return;
                }
                var convertToPaymentLines = new List<PaymentAppointment>();
                //是否循环，如果循环，则批量增加
                if (appointment.RecurrenceRule != null)
                {
                    var occurrenceAppointment = appointment.RecurrenceRule.Pattern.GetOccurrences(appointment.Start);
                    //循环时间集合
                    var occurrenceDateTiems = occurrenceAppointment as DateTime[] ?? occurrenceAppointment.ToArray();
                    var allPaymentAppointments = AppointmentConvertHelper.GetOccurrences(appointment,
                        occurrenceDateTiems); //所有循环的Appointment，包括当前Appointment
                    _paymentAppointmentCollection.AddRange(allPaymentAppointments);
                    convertToPaymentLines.AddRange(allPaymentAppointments);
                    _paymentAppointmentCollection.Remove(appointment);
                }
                else
                {
                    appointment.UniqueId = RandomHelper.Next().ToString(CultureInfo.InvariantCulture);
                    convertToPaymentLines.Add(appointment);
                }
                //Appointment转化DTO，同时添加到需要增加的集合中
                AppointmentToPaymentLineConvert(convertToPaymentLines);
                RaisePropertyChanged(() => PaymentAppointmentCollection);
            }
            RaisePropertyChanged(() => SelectedPaymentSchedule.PaymentScheduleLines);
        }

        public void PaymentScheduleView_OnLoaded(object sender, RoutedEventArgs e)
        {
            if (_scheduleView == null)
            {
                _scheduleView = sender as RadScheduleView;
            }
        }

        #endregion

        #region 重载保存命令操作

        /// <summary>
        ///     保存前操作
        /// </summary>
        /// <param name="sender"></param>
        protected override bool OnSaveExecuting(object sender)
        {
            if (SelectedPaymentSchedule != null)
            {
                if (SelectedPaymentSchedule.PaymentScheduleLines.Count > 0) return true;
                MessageAlert("提示", "付款计划行不能为空");
                return false;
            }
            MessageAlert("提示", "付款计划不能为空");
            return false;
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            CurrencysView.AutoLoad = true;
            Suppliers.Load(true);
            if (!PaymentSchedulesView.AutoLoad)
                PaymentSchedulesView.AutoLoad = true;
            PaymentSchedulesView.Load(true);
        }

        /// <summary>
        ///     刷新按钮状态
        /// </summary>
        protected override void RefreshCommandState()
        {
            SaveCommand.RaiseCanExecuteChanged();
            AbortCommand.RaiseCanExecuteChanged();
            AddPaymentScheduleCommand.RaiseCanExecuteChanged();
            AddPaymentScheduleLineCommand.RaiseCanExecuteChanged();
            DelPaymentScheduleLineCommand.RaiseCanExecuteChanged();
            RaisePropertyChanged(() => ScheduleViewEnable);
        }

        #endregion
    }
}
