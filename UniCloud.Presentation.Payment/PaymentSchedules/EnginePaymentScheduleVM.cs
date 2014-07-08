#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/17，10:12
// 文件名：EnginePaymentScheduleVM.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

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
    [Export(typeof (EnginePaymentScheduleVM))]
    public class EnginePaymentScheduleVM : EditViewModelBase
    {
        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public EnginePaymentScheduleVM(IPaymentService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialContractEngine(); // 初始化合同发动机信息。
            InitialEnginePaymentSchedule(); //初始化付款计划
            InitialCommand(); //初始化命令
            InitialCurrency(); //初始化币种
        }

        #region 加载合同发动机

        private ContractEngineDTO _selectedContractEngine;

        /// <summary>
        ///     选择合同发动机。
        /// </summary>
        public ContractEngineDTO SelectedContractEngine
        {
            get { return _selectedContractEngine; }
            set
            {
                if (_selectedContractEngine != value)
                {
                    _selectedContractEngine = value;
                    if (value != null)
                    {
                        LoadEnginePaymentScheduleByContractEngineId();
                    }
                    RaisePropertyChanged(() => SelectedContractEngine);
                }
            }
        }

        /// <summary>
        ///     获取所有合同发动机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ContractEngineDTO> ContractEnginesView { get; set; }

        /// <summary>
        ///     初始化合同发动机信息。
        /// </summary>
        private void InitialContractEngine()
        {
            ContractEnginesView = _service.CreateCollection(_context.ContractEngines);
            ContractEnginesView.PageSize = 20;
            ContractEnginesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedContractEngine == null)
                {
                    SelectedContractEngine = e.Entities.Cast<ContractEngineDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载合同发动机下的付款计划

        private FilterDescriptor _paymnetFilterOperator; //付款计划查询

        private PaymentScheduleLineDTO _selectPaymentScheduleLine;
        private EnginePaymentScheduleDTO _selectedEnginePaymentSchedule;

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
        ///     选择发动机付款计划
        /// </summary>
        public EnginePaymentScheduleDTO SelectedEnginePaymentSchedule
        {
            get { return _selectedEnginePaymentSchedule; }
            set
            {
                _selectedEnginePaymentSchedule = value;
                AddPaymentAppointments();
                RaisePropertyChanged(() => SelectedEnginePaymentSchedule);
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePaymentScheduleDTO> EnginePaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialEnginePaymentSchedule()
        {
            EnginePaymentSchedulesView = _service.CreateCollection(_context.EnginePaymentSchedules,
                o => o.PaymentScheduleLines);
            _paymnetFilterOperator = new FilterDescriptor("ContractEngineId", FilterOperator.IsEqualTo, 0);
            EnginePaymentSchedulesView.FilterDescriptors.Add(_paymnetFilterOperator);
            EnginePaymentSchedulesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedEnginePaymentSchedule = e.Entities.Cast<EnginePaymentScheduleDTO>().FirstOrDefault();
                RefreshCommandState(); //刷新按钮状态
            };
        }

        /// <summary>
        ///     根据合同发动机Id查询付款计划
        /// </summary>
        private void LoadEnginePaymentScheduleByContractEngineId()
        {
            _paymnetFilterOperator.Value = SelectedContractEngine.ContractEngineId;
            if (!EnginePaymentSchedulesView.AutoLoad)
            {
                EnginePaymentSchedulesView.AutoLoad = true;
            }
            else
            {
                EnginePaymentSchedulesView.Load(true);
            }
        }

        #endregion

        #region 加载币种

        private CurrencyDTO _selectedCurrency;

        /// <summary>
        ///     选择币种
        /// </summary>
        public CurrencyDTO SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                RaisePropertyChanged(() => SelectedCurrency);
            }
        }

        /// <summary>
        ///     获取所有币种。
        /// </summary>
        public QueryableDataServiceCollectionView<CurrencyDTO> CurrencysView { get; set; }

        /// <summary>
        ///     初始化币种信息。
        /// </summary>
        private void InitialCurrency()
        {
            CurrencysView = _service.CreateCollection(_context.Currencies);
            CurrencysView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedCurrency = e.Entities.Cast<CurrencyDTO>().FirstOrDefault();
            };
        }

        #endregion

        #region 命令

        #region 新增发动机付款计划命令

        public DelegateCommand<object> AddPaymentScheduleCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddPaymentSchedule(object sender)
        {
            if (SelectedContractEngine == null)
            {
                MessageAlert("提示", "合同发动机不能为空");
                return;
            }
            if (EnginePaymentSchedulesView.Count >= 1)
            {
                MessageAlert("提示", "已存在付款计划");
                return;
            }
            //新增发动机付款计划
            SelectedEnginePaymentSchedule = new EnginePaymentScheduleDTO
            {
                EnginePaymentScheduleId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                ContractEngineId = SelectedContractEngine.ContractEngineId,
                SupplierId = SelectedContractEngine.SupplierId == null
                    ? 0
                    : SelectedContractEngine
                        .SupplierId.Value,
                SupplierName = SelectedContractEngine.SupplierName,
            };
            //美元作为默认选中货币
            var currencyDto = CurrencysView.FirstOrDefault(p => p.Name.Equals("美元"));
            if (currencyDto != null)
                SelectedEnginePaymentSchedule.CurrencyId = currencyDto.Id;
            EnginePaymentSchedulesView.AddNewItem(SelectedEnginePaymentSchedule);
            RefreshCommandState(); //刷新按钮状态
        }

        /// <summary>
        ///     判断新增发动机付款计划命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentSchedule(object sender)
        {
            if (EnginePaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedContractEngine != null && EnginePaymentSchedulesView.Count <= 0;
        }

        #endregion

        #region 删除机付款计划行命令

        public DelegateCommand<object> DelPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行删除付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndDelPaymentScheduleLine(object sender)
        {
            var selPayment = sender as PaymentScheduleLineDTO;
            if (selPayment == null)
            {
                MessageAlert("提示", "请选择需要删除的付款计划明细");
                return;
            }
            MessageConfirm("提示", "是否删除该记录", (s, e) =>
            {
                if (e.DialogResult == true)
                {
                    SelectedEnginePaymentSchedule.PaymentScheduleLines.Remove(selPayment);
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
        /// <returns>新增命令是否可用。</returns>
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
        public void OndAddPaymentScheduleLine(object sender)
        {
            if (SelectedEnginePaymentSchedule == null)
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
            if (EnginePaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedContractEngine != null && SelectedEnginePaymentSchedule != null
                   && !SelectedEnginePaymentSchedule.IsCompleted;
        }

        #endregion

        #region 编辑付款计划行

        public DelegateCommand<object> EditPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行编辑付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndEditPaymentScheduleLine(object sender)
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
        /// <returns>新增命令是否可用。</returns>
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
            AddPaymentScheduleCommand = new DelegateCommand<object>(OndAddPaymentSchedule, CanAddPaymentSchedule);
            DelPaymentScheduleLineCommand = new DelegateCommand<object>(OndDelPaymentScheduleLine,
                CanDelPaymentScheduleLine);
            AddPaymentScheduleLineCommand = new DelegateCommand<object>(OndAddPaymentScheduleLine,
                CanAddPaymentScheduleLine);
            EditPaymentScheduleLineCommand = new DelegateCommand<object>(OndEditPaymentScheduleLine,
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
                if (EnginePaymentSchedulesView.IsSubmittingChanges)
                {
                    return false;
                }
                return SelectedContractEngine != null && SelectedEnginePaymentSchedule != null;
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
            if (SelectedEnginePaymentSchedule != null)
            {
                _paymentAppointmentCollection.ConvertAppointmentAndAdd(
                    SelectedEnginePaymentSchedule.PaymentScheduleLines);
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
                SelectedEnginePaymentSchedule.PaymentScheduleLines.Add(paymentLine);
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
            if (SelectedEnginePaymentSchedule == null)
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
                        SelectedEnginePaymentSchedule.PaymentScheduleLines.FirstOrDefault(
                            p => p.PaymentScheduleLineId == int.Parse(appointment.UniqueId));
                    if (paymentScheduleLine != null)
                    {
                        SelectedEnginePaymentSchedule.PaymentScheduleLines.Remove(paymentScheduleLine);
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
            if (SelectedEnginePaymentSchedule == null)
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
                        SelectedEnginePaymentSchedule.PaymentScheduleLines.FirstOrDefault(
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
            if (SelectedEnginePaymentSchedule != null)
            {
                if (SelectedEnginePaymentSchedule.PaymentScheduleLines.Count > 0) return true;
                MessageAlert("提示", "付款计划行不能为空");
                return false;
            }
            MessageAlert("提示", "发动机付款计划不能为空");
            return false;
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            if (!ContractEnginesView.AutoLoad)
                ContractEnginesView.AutoLoad = true;
            CurrencysView.AutoLoad = true;
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