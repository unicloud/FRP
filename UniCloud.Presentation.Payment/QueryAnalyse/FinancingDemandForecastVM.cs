#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/12 9:55:35
// 文件名：FinancingDemandForecastVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.QueryAnalyse
{
    [Export(typeof(FinancingDemandForecastVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FinancingDemandForecastVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PaymentData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPaymentService _service;
        private RadDateTimePicker EndDateTimePicker; //开始时间控件， 结束时间控件
        private RadDateTimePicker StartDateTimePicker; //开始时间控件， 结束时间控件
        private Point _panOffset;
        private Size _zoom;

        [ImportingConstructor]
        public FinancingDemandForecastVM(IRegionManager regionManager, IPaymentService service)
            : base(service)
        {
            _regionManager = regionManager;
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
            Zoom = new Size(1, 1);
            PaymentSchedules = _service.CreateCollection(_context.PaymentSchedules);
            PaymentSchedules.LoadedData += (o, e) =>
                                           {
                                               _loadPaymentSchedules = true;
                                               LoadComplete();
                                           };
            BaseInvoices = _service.CreateCollection(_context.Invoices);
            BaseInvoices.LoadedData += (o, e) =>
                                       {
                                           _loadInvoices = true;
                                           LoadComplete();
                                       };
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            //NewCommand = new DelegateCommand<object>(OnNew, CanNew);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region ViewModel 属性 EndDate --结束时间

        private DateTime? _endDate = Convert.ToDateTime(DateTime.Now.AddYears(2).ToString("yyyy/M"));

        /// <summary>
        ///     结束时间
        /// </summary>
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {
                if (EndDate != value)
                {
                    if (value == null)
                    {
                        EndDateTimePicker.SelectedValue = _endDate;
                        return;
                    }
                    _endDate = value;
                    RaisePropertyChanged(() => EndDate);
                    //处理界面需要展示的集合
                    CreateViewFinancingDemandData();
                }
            }
        }

        #endregion

        #region ViewModel 属性 StartDate --开始时间

        private DateTime? _startDate = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1);

        /// <summary>
        ///     开始时间
        /// </summary>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {
                if (StartDate != value)
                {
                    if (value == null)
                    {
                        StartDateTimePicker.SelectedValue = _startDate;
                        return;
                    }
                    _startDate = value;
                    RaisePropertyChanged(() => StartDate);
                    //处理界面需要展示的集合
                    CreateViewFinancingDemandData();
                }
            }
        }

        #endregion

        #region ViewModel 属性 SelectedIndex --时间的统计方式

        private int _selectedIndex;

        /// <summary>
        ///     时间的统计方式
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {
                if (SelectedIndex != value)
                {
                    _selectedIndex = value;
                    //处理界面需要展示的集合
                    CreateViewFinancingDemandData();
                    RaisePropertyChanged(() => SelectedIndex);
                }
            }
        }

        #endregion

        #endregion

        #region 加载数据

        public Point PanOffset
        {
            get { return _panOffset; }
            set
            {
                if (_panOffset != value)
                {
                    _panOffset = value;
                    RaisePropertyChanged("PanOffset");
                }
            }
        }

        public Size Zoom
        {
            get { return _zoom; }
            set
            {
                if (_zoom != value)
                {
                    _zoom = value;
                    RaisePropertyChanged("Zoom");
                }
            }
        }

        #region 付款计划
        public QueryableDataServiceCollectionView<PaymentScheduleDTO> PaymentSchedules { get; set; }
        private bool _loadPaymentSchedules;
        #endregion

        #region 发票
        public QueryableDataServiceCollectionView<BaseInvoiceDTO> BaseInvoices { get; set; }
        private bool _loadInvoices;
        #endregion

        /// <summary>
        ///     加载数据方法
        ///     <remarks>
        ///         导航到此页面时调用。
        ///         可在此处将CollectionView的AutoLoad属性设为True，以实现数据的自动加载。
        ///     </remarks>
        /// </summary>
        public override void LoadData()
        {
            _loadPaymentSchedules = false;
            _loadInvoices = false;
            PaymentSchedules.Load(true);
            BaseInvoices.Load(true);
            IsBusy = !(_loadPaymentSchedules && _loadInvoices);
        }

        #region 业务

        private void LoadComplete()
        {
            if (_loadInvoices && _loadPaymentSchedules)
            {
                _financingDemands.Clear();
                PaymentSchedules.ToList().ForEach(p => p.PaymentScheduleLines.ToList().ForEach(
                    q =>
                    {
                        var dataItem =
                            FinancingDemands.FirstOrDefault(
                                a => a.Year == q.ScheduleDate.Year && a.Month == q.ScheduleDate.Month);
                        if (dataItem == null)
                        {
                            dataItem = new FinancingDemand
                            {
                                TimeStamp = q.ScheduleDate.Date,
                                Year = q.ScheduleDate.Year,
                                Month = q.ScheduleDate.Month,
                                Amount = q.Amount,
                                PaidAmount =
                                    BaseInvoices.Where(t => t.PaymentScheduleLineId == q.PaymentScheduleLineId)
                                    .Sum(o => o.PaidAmount),
                            };
                            dataItem.RemainAmount = dataItem.Amount - dataItem.PaidAmount;
                            FinancingDemands.Add(dataItem);
                        }
                        else
                        {
                            dataItem.Amount += q.Amount;
                            dataItem.PaidAmount += BaseInvoices.Where(
                                t => t.PaymentScheduleLineId == q.PaymentScheduleLineId)
                                .Sum(o => o.PaidAmount);
                            dataItem.RemainAmount = dataItem.Amount - dataItem.PaidAmount;
                        }
                    }));
                RaisePropertyChanged(() => FinancingDemands);
                CreateViewFinancingDemandData();
                IsBusy = !(_loadPaymentSchedules && _loadInvoices);
            }
        }


        private void CreateViewFinancingDemandData()
        {
            if (FinancingDemands != null)
            {
                _viewFinancingDemands.Clear();
                foreach (var dataItem in FinancingDemands)
                {
                    if (SelectedIndex == 1) //按半年统计
                    {
                        if (dataItem.Month != 6 && dataItem.Month != 12)
                        {
                            continue;
                        }
                    }
                    else if (SelectedIndex == 2) //按年份统计
                    {
                        if (dataItem.Month != 12)
                        {
                            continue;
                        }
                    }

                    if (dataItem.TimeStamp < StartDate || dataItem.TimeStamp > EndDate) continue;

                    ViewFinancingDemands.Add(dataItem);
                }
            }
        }

        #region 资金需求

        private ObservableCollection<FinancingDemand> _financingDemands = new ObservableCollection<FinancingDemand>();


        private ObservableCollection<FinancingDemand> _viewFinancingDemands = new ObservableCollection<FinancingDemand>();

        /// <summary>
        ///     资金需求
        /// </summary>
        public ObservableCollection<FinancingDemand> FinancingDemands
        {
            get { return _financingDemands; }
            set
            {
                if (_financingDemands != value)
                {
                    _financingDemands = value;
                    RaisePropertyChanged(() => FinancingDemands);
                }
            }
        }

        /// <summary>
        ///     用于界面展示的集合
        /// </summary>
        public ObservableCollection<FinancingDemand> ViewFinancingDemands
        {
            get { return _viewFinancingDemands; }
            set
            {
                if (_viewFinancingDemands != value)
                {
                    _viewFinancingDemands = value;
                    RaisePropertyChanged(() => ViewFinancingDemands);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        /// <summary>
        ///     控制趋势图中折线（饼状）的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckboxChecked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox != null)
            {
                var grid =
                    (((checkbox.Parent as StackPanel).Parent as StackPanel).Parent as ScrollViewer).Parent as Grid;
                (grid.Children[0] as RadCartesianChart).Series.FirstOrDefault(
                    p => p.DisplayName == checkbox.Content.ToString()).Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        ///     控制趋势图中折线（饼状）的隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void CheckboxUnchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox != null)
            {
                var stackPanel = checkbox.Parent as StackPanel;
                if (stackPanel != null)
                {
                    var grid = ((stackPanel.Parent as StackPanel).Parent as ScrollViewer).Parent as Grid;
                    (grid.Children[0] as RadCartesianChart).Series.FirstOrDefault(
                        p => p.DisplayName == checkbox.Content.ToString()).Visibility = Visibility.Collapsed;
                }
            }
        }

        #endregion
    }

    public class FinancingDemand
    {
        /// <summary>
        ///     主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        ///     时间
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        ///     年度
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        ///     月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        ///     累计金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        ///     已付金额
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        ///     剩余金额（资金需求量）
        /// </summary>
        public decimal RemainAmount { get; set; }
    }
}