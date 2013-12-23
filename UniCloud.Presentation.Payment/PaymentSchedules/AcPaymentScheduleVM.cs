#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/17，10:12
// 文件名：AcPaymentScheduleVM.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Payment.PaymentSchedules.PaymentScheduleExtension;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof (AcPaymentScheduleVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AcPaymentScheduleVM : EditViewModelBase
    {
        private PaymentData _context;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public AcPaymentScheduleVM()
        {
            InitialContractAircraft(); // 初始化合同飞机信息。
            InitialAcPaymentSchedule(); //初始化付款计划
            InitialCommand(); //初始化命令
            InitialCurrency(); //初始化币种
            InitialPaymentAppointment(); //初始化PaymentAppointment
        }

        #region 加载合同飞机

        private ContractAircraftDTO _selectedContractAircraft;

        /// <summary>
        ///     选择合同飞机。
        /// </summary>
        public ContractAircraftDTO SelectedContractAircraft
        {
            get { return _selectedContractAircraft; }
            set
            {
                if (_selectedContractAircraft != value)
                {
                    _selectedContractAircraft = value;
                    if (value != null)
                    {
                        LoadAcPaymentScheduleByContractAcId();
                    }
                    RaisePropertyChanged(() => SelectedContractAircraft);
                }
            }
        }

        /// <summary>
        ///     获取所有合同飞机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircraftsView { get; set; }

        /// <summary>
        ///     初始化合同飞机信息。
        /// </summary>
        private void InitialContractAircraft()
        {
            ContractAircraftsView = Service.CreateCollection(_context.ContractAircrafts);
            ContractAircraftsView.PageSize = 20;
            ContractAircraftsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedContractAircraft == null)
                {
                    SelectedContractAircraft = e.Entities.Cast<ContractAircraftDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载合同飞机下的付款计划

        private FilterDescriptor _paymnetFilterOperator; //付款计划查询

        private AcPaymentScheduleDTO _selectedAcPaymentSchedule;

        /// <summary>
        ///     选择飞机付款计划
        /// </summary>
        public AcPaymentScheduleDTO SelectedAcPaymentSchedule
        {
            get { return _selectedAcPaymentSchedule; }
            set
            {
                _selectedAcPaymentSchedule = value;
                AddPaymentAppointments();
                RaisePropertyChanged(() => SelectedAcPaymentSchedule);
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AcPaymentScheduleDTO> AcPaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialAcPaymentSchedule()
        {
            AcPaymentSchedulesView = Service.CreateCollection(_context.AcPaymentSchedules);
            _paymnetFilterOperator = new FilterDescriptor("ContractAcId", FilterOperator.IsEqualTo, 0);
            AcPaymentSchedulesView.FilterDescriptors.Add(_paymnetFilterOperator);
            AcPaymentSchedulesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedAcPaymentSchedule = e.Entities.Cast<AcPaymentScheduleDTO>().FirstOrDefault();
                RefreshCommandState(); //刷新按钮状态
            };
        }

        /// <summary>
        ///     根据合同飞机Id查询付款计划
        /// </summary>
        private void LoadAcPaymentScheduleByContractAcId()
        {
            _paymnetFilterOperator.Value = SelectedContractAircraft.ContractAircrafId;
            if (!AcPaymentSchedulesView.AutoLoad)
            {
                AcPaymentSchedulesView.AutoLoad = true;
            }
            else
            {
                AcPaymentSchedulesView.Load(true);
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
            CurrencysView = Service.CreateCollection(_context.Currencies);
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

        #region 属性

        private PaymentScheduleLineDTO _selectPaymentScheduleLine;

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

        #endregion

        #region 命令

        #region 新增飞机付款计划命令

        public DelegateCommand<object> AddPaymentScheduleCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddPaymentSchedule(object sender)
        {
            if (SelectedContractAircraft == null)
            {
                MessageAlert("提示", "合同飞机不能为空");
                return;
            }
            if (AcPaymentSchedulesView.Count >= 1)
            {
                MessageAlert("提示", "已存在付款计划");
                return;
            }
            //新增飞机付款计划
            SelectedAcPaymentSchedule = new AcPaymentScheduleDTO
            {
                AcPaymentScheduleId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                ContractAcId = SelectedContractAircraft.ContractAircrafId,
                SupplierId = SelectedContractAircraft.SupplierId == null
                    ? 0
                    : SelectedContractAircraft
                        .SupplierId.Value,
                SupplierName = SelectedContractAircraft.SupplierName,
            };
            //美元作为默认选中货币
            var currencyDto = CurrencysView.FirstOrDefault(p => p.Name.Equals("美元"));
            if (currencyDto != null)
                SelectedAcPaymentSchedule.CurrencyId = currencyDto.Id;
            AcPaymentSchedulesView.AddNewItem(SelectedAcPaymentSchedule);
            RefreshCommandState(); //刷新按钮状态
        }

        /// <summary>
        ///     判断新增飞机付款计划命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentSchedule(object sender)
        {
            if (AcPaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedContractAircraft != null && AcPaymentSchedulesView.Count <= 0;
        }

        #endregion

        #region 新增飞机付款计划行命令

        public DelegateCommand<object> AddPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行新增付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddPaymentScheduleLine(object sender)
        {
            if (SelectedAcPaymentSchedule == null)
            {
                MessageAlert("提示", "付款计划不能为空");
                return;
            }
            //新增飞机付款计划行
            SelectPaymentScheduleLine = new PaymentScheduleLineDTO
            {
                PaymentScheduleLineId = RandomHelper.Next(),
                PaymentScheduleId = SelectedAcPaymentSchedule.AcPaymentScheduleId,
                ScheduleDate = DateTime.Now,
            };
            SelectedAcPaymentSchedule.PaymentScheduleLines.Add(SelectPaymentScheduleLine);
        }

        /// <summary>
        ///     判断新增飞机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentScheduleLine(object sender)
        {
            if (AcPaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedContractAircraft != null && SelectedAcPaymentSchedule != null
                   && !SelectedAcPaymentSchedule.IsCompleted;
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
            if (SelectPaymentScheduleLine == null)
            {
                MessageAlert("提示", "请选择需要删除的付款计划明细");
                return;
            }
            SelectedAcPaymentSchedule.PaymentScheduleLines.Remove(SelectPaymentScheduleLine);
            RefreshCommandState(); //刷新按钮状态
        }

        /// <summary>
        ///     判断删除飞机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanDelPaymentScheduleLine(object sender)
        {
            if (AcPaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            //付款计划跟发票建立关联，则不能删除
            return SelectedContractAircraft != null && SelectedAcPaymentSchedule != null
                   && !SelectedAcPaymentSchedule.IsCompleted && SelectPaymentScheduleLine != null;
        }

        #endregion

        #region 批量增加付款计划行

        public DelegateCommand<object> BatchAddPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行新增付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndBatchAddPaymentScheduleLine(object sender)
        {
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
        public bool CanBatchAddPaymentScheduleLine(object sender)
        {
            if (AcPaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedContractAircraft != null && SelectedAcPaymentSchedule != null
                   && !SelectedAcPaymentSchedule.IsCompleted;
        }

        #endregion

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialCommand()
        {
            AddPaymentScheduleCommand = new DelegateCommand<object>(OndAddPaymentSchedule, CanAddPaymentSchedule);
            AddPaymentScheduleLineCommand = new DelegateCommand<object>(OndAddPaymentScheduleLine,
                CanAddPaymentScheduleLine);
            DelPaymentScheduleLineCommand = new DelegateCommand<object>(OndDelPaymentScheduleLine,
                CanDelPaymentScheduleLine);
            BatchAddPaymentScheduleLineCommand = new DelegateCommand<object>(OndBatchAddPaymentScheduleLine,
                CanBatchAddPaymentScheduleLine);
        }
        #endregion

        #region 方法
        public void PaymentScheduleView_OnShowDialog(object sender, ShowDialogEventArgs e)
        {
            if (e.DialogViewModel is RecurrenceDialogViewModel)
            {
                var viewModel = e.DialogViewModel as RecurrenceDialogViewModel;
                viewModel.RecurrenceRangeType = RecurrenceRangeType.RepeatUntil;
            }
        }

        #endregion

        #region Schedule相关处理

        #region 属性

        private DateTime _endDate = DateTime.MinValue;


        private ObservableCollection<Appointment> _minimapAppointments;
        private PaymentAppointmentCollection _paymentAppointmentCollection;
        private DateTime _selectionEndDate = DateTime.MinValue;
        private DateTime _selectionStartDate = DateTime.MinValue;
        private DateTime _startDate = DateTime.MinValue;
        private DateTime _visibleEndDate = DateTime.MinValue;
        private DateTime _visibleStartDate = DateTime.MinValue;


        public DateTime StartDate
        {
            get { return _startDate; }
            set
            {
                if (_startDate == value)
                    return;

                _startDate = value;
                RaisePropertyChanged(()=>StartDate);
            }
        }

        public DateTime EndDate
        {
            get { return _endDate; }
            set
            {
                if (_endDate == value)
                    return;

                _endDate = value;
                RaisePropertyChanged(()=>EndDate);
            }
        }

        public DateTime VisibleStartDate
        {
            get { return _visibleStartDate; }
            set
            {
                if (_visibleStartDate == value)
                    return;

                _visibleStartDate = value;
                RaisePropertyChanged(()=>VisibleStartDate);
            }
        }

        public DateTime VisibleEndDate
        {
            get { return _visibleEndDate; }
            set
            {
                if (_visibleEndDate == value)
                    return;

                _visibleEndDate = value;
                RaisePropertyChanged(()=>VisibleEndDate);
            }
        }

        public DateTime SelectionStartDate
        {
            get { return _selectionStartDate; }
            set
            {
                if (_selectionStartDate == value)
                    return;

                _selectionStartDate = value;
                RaisePropertyChanged(()=>SelectionStartDate);
            }
        }

        public DateTime SelectionEndDate
        {
            get { return _selectionEndDate; }
            set
            {
                if (_selectionEndDate == value)
                    return;

                _selectionEndDate = value;
                RaisePropertyChanged(()=>SelectionEndDate);
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
        ///     时间轴的最小化Appointment
        /// </summary>
        public ObservableCollection<Appointment> MinimapAppointments
        {
            get
            {
                return _minimapAppointments ??
                       (_minimapAppointments = new ObservableCollection<Appointment>(PaymentAppointmentCollection));
            }
        }

        /// <summary>
        ///     任务状态集合
        /// </summary>
        public CategoryCollection Categories
        {
            get { return AppointmentConvertHelper.GetCategoryCollection(); }
        }

        #endregion

        #region 方法

        /// <summary>
        /// 设置相关时间信息
        /// </summary>
        private void SetDateTimeBar()
        {
           var appointments= PaymentAppointmentCollection.OrderBy(p => p.Start);
           var firstAppointment = appointments.FirstOrDefault();
           var lastAppointment = appointments.LastOrDefault();
           if (firstAppointment != null)
               StartDate = firstAppointment.Start;
           if (lastAppointment!=null)
            {
                EndDate = lastAppointment.End;
            }
            VisibleStartDate = StartDate;
            VisibleEndDate = EndDate;
            SelectionStartDate = DateTime.Now;
            SelectionEndDate = DateTime.Now;
        }
        #endregion

        #region ScheduleView新增处理

        /// <summary>
        ///     创建付款计划行
        /// </summary>
        public DelegateCommand<object> CreatePaymentScheduleCommand { set; get; }

        public void OnCreatePaymentSchedule(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = scheduleView.EditedAppointment as PaymentAppointment;
                if (appointment == null)
                {
                    return;
                }
                if (appointment.RecurrenceRule != null)
                {
                    var occurrenceAppointment = appointment.RecurrenceRule.Pattern.GetOccurrences(appointment.Start);
                    var occurrenceDateTiems = occurrenceAppointment as DateTime[] ?? occurrenceAppointment.ToArray();
                    var allPaymentAppointments = AppointmentConvertHelper.GetOccurrences(appointment,
                        occurrenceDateTiems);
                    _paymentAppointmentCollection.AddRange(allPaymentAppointments);
                    _paymentAppointmentCollection.Remove(appointment);
                }

                RaisePropertyChanged(() => PaymentAppointmentCollection);
                RaisePropertyChanged(() => MinimapAppointments);
            }
        }

        #endregion

        /// <summary>
        ///     付款计划Schedule相关处理
        /// </summary>
        private void InitialPaymentAppointment()
        {
            _paymentAppointmentCollection = new PaymentAppointmentCollection();
            CreatePaymentScheduleCommand = new DelegateCommand<object>(OnCreatePaymentSchedule);
        }

        /// <summary>
        ///     添加PaymentAppointment集合
        /// </summary>
        private void AddPaymentAppointments()
        {
            _paymentAppointmentCollection.Clear();
            if (SelectedAcPaymentSchedule != null)
            {
                _paymentAppointmentCollection.ConvertAppointmentAndAdd(SelectedAcPaymentSchedule.PaymentScheduleLines);
            }
            RaisePropertyChanged(() => PaymentAppointmentCollection);
            RaisePropertyChanged(() => MinimapAppointments);
        }

        #endregion

        #region 重载保存命令操作

        /// <summary>
        ///     保存前操作
        /// </summary>
        /// <param name="sender"></param>
        protected override bool OnSaveExecuting(QueryableDataServiceCollectionViewBase sender)
        {
            if (SelectedAcPaymentSchedule != null)
            {
                if (SelectedAcPaymentSchedule.PaymentScheduleLines.Count > 0) return true;
                MessageAlert("提示", "付款计划行不能为空");
                return false;
            }
            MessageAlert("提示", "飞机付款计划不能为空");
            return false;
        }

        #endregion

        #region 重载基类服务

        protected override IService CreateService()
        {
            _context = new PaymentData(AgentHelper.PaymentUri);
            return new PaymentService(_context);
        }

        public override void LoadData()
        {
            if (!ContractAircraftsView.AutoLoad)
            {
                ContractAircraftsView.AutoLoad = true;
            }
            else
            {
                ContractAircraftsView.AutoLoad = true;
            }
            CurrencysView.AutoLoad = true;
        }

        /// <summary>
        ///     刷新按钮状态
        /// </summary>
        public override void RefreshCommandState()
        {
            SaveCommand.RaiseCanExecuteChanged();
            AbortCommand.RaiseCanExecuteChanged();
            AddPaymentScheduleCommand.RaiseCanExecuteChanged();
            AddPaymentScheduleLineCommand.RaiseCanExecuteChanged();
            BatchAddPaymentScheduleLineCommand.RaiseCanExecuteChanged();
            DelPaymentScheduleLineCommand.RaiseCanExecuteChanged();
        }

        #endregion


     
    }
}