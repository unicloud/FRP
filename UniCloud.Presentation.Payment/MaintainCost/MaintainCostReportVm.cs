#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/21 9:41:19
// 文件名：MaintainCostReportVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/21 9:41:19
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Charting;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(MaintainCostReportVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MaintainCostReportVm : ViewModelBase
    {
        private readonly IPaymentService _service;
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public MaintainCostReportVm(IPaymentService service, IFleetPlanService fleetPlanService)
            : base(service)
        {
            _fleetPlanService = fleetPlanService;
            _service = service;
            _context = _service.Context;
            InitialVm(); //初始化Apu维修成本

        }

        #region 年度
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }

        private string _selectTitle;
        public string SelectTitle
        {
            get { return _selectTitle; }
            set
            {
                _selectTitle = value;
                RaisePropertyChanged(() => SelectTitle);
            }
        }
        #endregion

        #region 维修成本报告
        private ObservableCollection<MaintainCost> _maintainCostReports;
        public ObservableCollection<MaintainCost> MaintainCostReports
        {
            get { return _maintainCostReports; }
            set
            {
                _maintainCostReports = value;
                RaisePropertyChanged(() => MaintainCostReports);
            }
        }

        private ObservableCollection<MaintainCostDetail> _maintainCostDetails;
        public ObservableCollection<MaintainCostDetail> MaintainCostDetails
        {
            get { return _maintainCostDetails; }
            set
            {
                _maintainCostDetails = value;
                RaisePropertyChanged(() => MaintainCostDetails);
            }
        }
        #endregion

        #region 加载维修成本

        /// <summary>
        ///     获取所有Apu维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ApuMaintainCostDTO> ApuMaintainCosts { get; set; }
        private bool _loadedApu;

        /// <summary>
        ///     获取所有Fha维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<FhaMaintainCostDTO> FhaMaintainCosts { get; set; }
        private bool _loadedFha;

        /// <summary>
        ///     获取所有非FHA.超包修维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<NonFhaMaintainCostDTO> NonFhaMaintainCosts { get; set; }
        private bool _loadedNonFha;

        /// <summary>
        ///     获取所有定检维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<RegularCheckMaintainCostDTO> RegularCheckMaintainCosts { get; set; }
        private bool _loadedRegularCheck;

        /// <summary>
        ///     获取所有特修改装维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SpecialRefitMaintainCostDTO> SpecialRefitMaintainCosts { get; set; }
        private bool _loadedSpecialRefit;

        /// <summary>
        ///     获取所有起落架维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<UndercartMaintainCostDTO> UndercartMaintainCosts { get; set; }
        private bool _loadedUndercart;

        /// <summary>
        ///     初始化维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            ApuMaintainCosts = _service.CreateCollection(_context.ApuMaintainCosts);
            ApuMaintainCosts.LoadedData += (sender, e) =>
            {
                Annuals.ToList().ForEach(p =>
                    {
                        var maintainCostReport = MaintainCostReports.FirstOrDefault(q => q.Annual.Year == p.Year);
                        bool exist = true;
                        if (maintainCostReport == null)
                        {
                            exist = false;
                            maintainCostReport = new MaintainCost { Annual = new DateTime(p.Year, 1, 1) };
                        }
                        maintainCostReport.ApuBudget = ApuMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.IncludeAddedValue);
                        maintainCostReport.TotalBudget += maintainCostReport.ApuBudget;
                        maintainCostReport.ApuActual = ApuMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.AcutalAmount);
                        maintainCostReport.TotalActual += maintainCostReport.ApuActual;
                        if (!exist && maintainCostReport.TotalBudget != 0 && maintainCostReport.TotalActual != 0)
                            MaintainCostReports.Add(maintainCostReport);
                    });
                _loadedApu = true;
                LoadComplete();
            };
            FhaMaintainCosts = _service.CreateCollection(_context.FhaMaintainCosts);
            FhaMaintainCosts.LoadedData += (sender, e) =>
            {
                Annuals.ToList().ForEach(p =>
                    {
                        var maintainCostReport = MaintainCostReports.FirstOrDefault(q => q.Annual.Year == p.Year);
                        bool exist = true;
                        if (maintainCostReport == null)
                        {
                            exist = false;
                            maintainCostReport = new MaintainCost { Annual = new DateTime(p.Year, 1, 1) };
                        }
                        maintainCostReport.FhaBudget = FhaMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.CustomAdded);
                        maintainCostReport.TotalBudget += maintainCostReport.FhaBudget;
                        maintainCostReport.FhaActual = FhaMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.AcutalAmount);
                        maintainCostReport.TotalActual += maintainCostReport.FhaActual;
                        if (!exist && maintainCostReport.TotalBudget != 0 && maintainCostReport.TotalActual != 0)
                            MaintainCostReports.Add(maintainCostReport);
                    });
                _loadedFha = true;
                LoadComplete();
            };

            NonFhaMaintainCosts = _service.CreateCollection(_context.NonFhaMaintainCosts);
            NonFhaMaintainCosts.LoadedData += (sender, e) =>
            {
                Annuals.ToList().ForEach(p =>
                    {
                        var maintainCostReport = MaintainCostReports.FirstOrDefault(q => q.Annual.Year == p.Year);
                        bool exist = true;
                        if (maintainCostReport == null)
                        {
                            exist = false;
                            maintainCostReport = new MaintainCost { Annual = new DateTime(p.Year, 1, 1) };
                        }
                        maintainCostReport.NonFhaBudget = NonFhaMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.FinancialApprovalAmount);
                        maintainCostReport.TotalBudget += maintainCostReport.NonFhaBudget;
                        maintainCostReport.NonFhaActual = NonFhaMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.AcutalAmount);
                        maintainCostReport.TotalActual += maintainCostReport.NonFhaActual;
                        if (!exist && maintainCostReport.TotalBudget != 0 && maintainCostReport.TotalActual != 0)
                            MaintainCostReports.Add(maintainCostReport);
                    });
                _loadedNonFha = true;
                LoadComplete();
            };

            RegularCheckMaintainCosts = _service.CreateCollection(_context.RegularCheckMaintainCosts);
            RegularCheckMaintainCosts.LoadedData += (sender, e) =>
            {
                Annuals.ToList().ForEach(p =>
                    {
                        var maintainCostReport = MaintainCostReports.FirstOrDefault(q => q.Annual.Year == p.Year);
                        bool exist = true;
                        if (maintainCostReport == null)
                        {
                            exist = false;
                            maintainCostReport = new MaintainCost { Annual = new DateTime(p.Year, 1, 1) };
                        }
                        maintainCostReport.RegularCheckBudget = RegularCheckMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.FinancialApprovalAmount);
                        maintainCostReport.TotalBudget += maintainCostReport.RegularCheckBudget;
                        maintainCostReport.RegularCheckActual = RegularCheckMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.AcutalAmount);
                        maintainCostReport.TotalActual += maintainCostReport.RegularCheckActual;
                        if (!exist && maintainCostReport.TotalBudget != 0 && maintainCostReport.TotalActual != 0)
                            MaintainCostReports.Add(maintainCostReport);
                    });
                _loadedRegularCheck = true;
                LoadComplete();
            };

            SpecialRefitMaintainCosts = _service.CreateCollection(_context.SpecialRefitMaintainCosts);
            SpecialRefitMaintainCosts.LoadedData += (sender, e) =>
            {
                Annuals.ToList().ForEach(p =>
                    {
                        var maintainCostReport = MaintainCostReports.FirstOrDefault(q => q.Annual.Year == p.Year);
                        bool exist = true;
                        if (maintainCostReport == null)
                        {
                            exist = false;
                            maintainCostReport = new MaintainCost { Annual = new DateTime(p.Year, 1, 1) };
                        }
                        maintainCostReport.SpecialRefitBudget = SpecialRefitMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.FinancialApprovalAmount);
                        maintainCostReport.TotalBudget += maintainCostReport.SpecialRefitBudget;
                        maintainCostReport.SpecialRefitActual = SpecialRefitMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.AcutalAmount);
                        maintainCostReport.TotalActual += maintainCostReport.SpecialRefitActual;
                        if (!exist && maintainCostReport.TotalBudget != 0 && maintainCostReport.TotalActual != 0)
                            MaintainCostReports.Add(maintainCostReport);
                    });
                _loadedSpecialRefit = true;
                LoadComplete();
            };

            UndercartMaintainCosts = _service.CreateCollection(_context.UndercartMaintainCosts);
            UndercartMaintainCosts.LoadedData += (sender, e) =>
            {
                Annuals.ToList().ForEach(p =>
                    {
                        var maintainCostReport = MaintainCostReports.FirstOrDefault(q => q.Annual.Year == p.Year);
                        bool exist = true;
                        if (maintainCostReport == null)
                        {
                            exist = false;
                            maintainCostReport = new MaintainCost { Annual = new DateTime(p.Year, 1, 1) };
                        }
                        maintainCostReport.UndercartBudget = UndercartMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.FinancialApprovalAmount);
                        maintainCostReport.TotalBudget += maintainCostReport.UndercartBudget;
                        maintainCostReport.UndercartActual = UndercartMaintainCosts.Where(q => q.AnnualId == p.Id).Sum(t => t.AcutalAmount);
                        maintainCostReport.TotalActual += maintainCostReport.UndercartActual;
                        if (!exist && maintainCostReport.TotalBudget != 0 && maintainCostReport.TotalActual != 0)
                            MaintainCostReports.Add(maintainCostReport);
                    });
                _loadedUndercart = true;
                LoadComplete();
            };

            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Annuals);
            Annuals.LoadedData += (o, e) =>
                                  {
                                      _maintainCostReports = new ObservableCollection<MaintainCost>();
                                      _loadedApu = false;
                                      ApuMaintainCosts.Load(true);
                                      _loadedFha = false;
                                      FhaMaintainCosts.Load(true);
                                      _loadedNonFha = false;
                                      NonFhaMaintainCosts.Load(true);
                                      _loadedRegularCheck = false;
                                      RegularCheckMaintainCosts.Load(true);
                                      _loadedSpecialRefit = false;
                                      SpecialRefitMaintainCosts.Load(true);
                                      _loadedUndercart = false;
                                      UndercartMaintainCosts.Load(true);
                                  };
        }

        private void LoadComplete()
        {
            if (_loadedApu && _loadedFha && _loadedNonFha && _loadedRegularCheck && _loadedSpecialRefit &&
                _loadedUndercart)
            {
                IsBusy = false;
                RaisePropertyChanged(() => MaintainCostReports);
                var maintainCost = MaintainCostReports.LastOrDefault();
                GenerateDetail(maintainCost);
            }
        }
        #endregion

        #region 命令

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            IsBusy = true;
            Annuals.Load(true);
        }

        #endregion

        /// <summary>
        ///     趋势图的选择点事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChartSelectionBehaviorSelectionChanged(object sender, ChartSelectionChangedEventArgs e)
        {
            var chartSelectionBehavior = sender as ChartSelectionBehavior;
            if (chartSelectionBehavior != null)
            {
                DataPoint selectedPoint = chartSelectionBehavior.Chart.SelectedPoints.FirstOrDefault();
                if (selectedPoint != null)
                {
                    var maintainCostItem = selectedPoint.DataItem as MaintainCost;
                    if (maintainCostItem != null)
                    {
                        var maintainCost = MaintainCostReports.FirstOrDefault(p => p.Annual.Year == maintainCostItem.Annual.Year);
                        GenerateDetail(maintainCost);
                    }
                }
            }
        }

        private void GenerateDetail(MaintainCost maintainCost)
        {
            if (maintainCost != null)
            {
                SelectTitle = maintainCost.Annual.Year + "年维修成本明细";
                MaintainCostDetails = new ObservableCollection<MaintainCostDetail>
                                                  {
                                                      new MaintainCostDetail
                                                      {
                                                          Name = "年度定检",
                                                          Actual = maintainCost.RegularCheckActual,
                                                          Budget = maintainCost.RegularCheckBudget,
                                                          Color =
                                                              maintainCost.RegularCheckActual >
                                                              maintainCost.RegularCheckBudget
                                                                  ? "#FFF90202"
                                                                  : "#FFCCCCCC"
                                                      },
                                                      new MaintainCostDetail
                                                      {
                                                          Name = "非FHA.超包修",
                                                          Actual = maintainCost.NonFhaActual,
                                                          Budget = maintainCost.NonFhaBudget,
                                                          Color =
                                                              maintainCost.NonFhaActual > maintainCost.NonFhaBudget
                                                                  ? "#FFF90202"
                                                                  : "#FFCCCCCC"
                                                      },
                                                      new MaintainCostDetail
                                                      {
                                                          Name = "起落架",
                                                          Actual = maintainCost.UndercartActual,
                                                          Budget = maintainCost.UndercartBudget,
                                                          Color =
                                                              maintainCost.UndercartActual >
                                                              maintainCost.UndercartBudget
                                                                  ? "#FFF90202"
                                                                  : "#FFCCCCCC"
                                                      },
                                                      new MaintainCostDetail
                                                      {
                                                          Name = "特修改装",
                                                          Actual = maintainCost.SpecialRefitActual,
                                                          Budget = maintainCost.SpecialRefitBudget,
                                                          Color =
                                                              maintainCost.SpecialRefitActual >
                                                              maintainCost.SpecialRefitBudget
                                                                  ? "#FFF90202"
                                                                  : "#FFCCCCCC"
                                                      },
                                                      new MaintainCostDetail
                                                      {
                                                          Name = "FHA",
                                                          Actual = maintainCost.FhaActual,
                                                          Budget = maintainCost.FhaBudget,
                                                          Color =
                                                              maintainCost.FhaActual > maintainCost.FhaBudget
                                                                  ? "#FFF90202"
                                                                  : "#FFCCCCCC"
                                                      },
                                                      new MaintainCostDetail
                                                      {
                                                          Name = "APU",
                                                          Actual = maintainCost.ApuActual,
                                                          Budget = maintainCost.ApuBudget,
                                                          Color =
                                                              maintainCost.ApuActual > maintainCost.ApuBudget
                                                                  ? "#FFF90202"
                                                                  : "#FFCCCCCC"
                                                      },
                                                  };
            }
        }
    }

    public class MaintainCost : INotifyPropertyChanged
    {
        public DateTime Annual { get; set; }
        public decimal ApuBudget { get; set; }
        private decimal _apuActual;
        public decimal ApuActual
        {
            get
            {
                return _apuActual;
            }
            set
            {
                _apuActual = value;
                OnPropertyChanged("ApuActual");
            }
        }

        public decimal FhaBudget { get; set; }
        private decimal _fhaActual;
        public decimal FhaActual
        {
            get
            {
                return _fhaActual;
            }
            set
            {
                _fhaActual = value;
                OnPropertyChanged("FhaActual");
            }
        }

        public decimal NonFhaBudget { get; set; }
        private decimal _nonFhaActual;
        public decimal NonFhaActual
        {
            get
            {
                return _nonFhaActual;
            }
            set
            {
                _nonFhaActual = value;
                OnPropertyChanged("NonFhaActual");
            }
        }

        public decimal RegularCheckBudget { get; set; }
        private decimal _regularCheckActual;
        public decimal RegularCheckActual
        {
            get
            {
                return _regularCheckActual;
            }
            set
            {
                _regularCheckActual = value;
                OnPropertyChanged("RegularCheckActual");
            }
        }

        public decimal SpecialRefitBudget { get; set; }
        private decimal _specialRefitActual;
        public decimal SpecialRefitActual
        {
            get { return _specialRefitActual; }
            set
            {
                _specialRefitActual = value;
                OnPropertyChanged("SpecialRefitActual");
            }
        }

        public decimal UndercartBudget { get; set; }
        private decimal _undercartActual;
        public decimal UndercartActual
        {
            get
            {
                return _undercartActual;
            }
            set
            {
                _undercartActual = value;
                OnPropertyChanged("UndercartActual");
            }
        }

        public decimal TotalBudget { get; set; }
        public decimal TotalActual { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public class MaintainCostDetail
    {
        public string Name { get; set; }
        public decimal Actual { get; set; }
        public decimal Budget { get; set; }
        public string Color { get; set; }
    }
}
