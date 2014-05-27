﻿#region Version Info
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
using UniCloud.Presentation.Service.Payment.Payment.Enums;

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
            InitialVm();

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

        #region 维修成本分析
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
        private bool _loadedApuMaintainCost;

        /// <summary>
        ///     获取所有Fha维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<FhaMaintainCostDTO> FhaMaintainCosts { get; set; }
        private bool _loadedFhaMaintainCost;

        /// <summary>
        ///     获取所有非FHA.超包修维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<NonFhaMaintainCostDTO> NonFhaMaintainCosts { get; set; }
        private bool _loadedNonFhaMaintainCost;

        /// <summary>
        ///     获取所有定检维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<RegularCheckMaintainCostDTO> RegularCheckMaintainCosts { get; set; }
        private bool _loadedRegularCheckMaintainCost;

        /// <summary>
        ///     获取所有特修改装维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SpecialRefitMaintainCostDTO> SpecialRefitMaintainCosts { get; set; }
        private bool _loadedSpecialRefitMaintainCost;

        /// <summary>
        ///     获取所有起落架维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<UndercartMaintainCostDTO> UndercartMaintainCosts { get; set; }
        private bool _loadedUndercartMaintainCost;

        /// <summary>
        ///     初始化维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            #region 维修成本
            ApuMaintainCosts = _service.CreateCollection(_context.ApuMaintainCosts);
            ApuMaintainCosts.LoadedData += (sender, e) =>
            {
                _loadedApuMaintainCost = true;
                LoadMaintainCostComplete();
            };
            FhaMaintainCosts = _service.CreateCollection(_context.FhaMaintainCosts);
            FhaMaintainCosts.LoadedData += (sender, e) =>
            {
                _loadedFhaMaintainCost = true;
                LoadMaintainCostComplete();
            };

            NonFhaMaintainCosts = _service.CreateCollection(_context.NonFhaMaintainCosts);
            NonFhaMaintainCosts.LoadedData += (sender, e) =>
            {
                _loadedNonFhaMaintainCost = true;
                LoadMaintainCostComplete();
            };

            RegularCheckMaintainCosts = _service.CreateCollection(_context.RegularCheckMaintainCosts);
            RegularCheckMaintainCosts.LoadedData += (sender, e) =>
            {
                _loadedRegularCheckMaintainCost = true;
                LoadMaintainCostComplete();
            };

            SpecialRefitMaintainCosts = _service.CreateCollection(_context.SpecialRefitMaintainCosts);
            SpecialRefitMaintainCosts.LoadedData += (sender, e) =>
            {
                _loadedSpecialRefitMaintainCost = true;
                LoadMaintainCostComplete();
            };

            UndercartMaintainCosts = _service.CreateCollection(_context.UndercartMaintainCosts);
            UndercartMaintainCosts.LoadedData += (sender, e) =>
            {
                _loadedUndercartMaintainCost = true;
                LoadMaintainCostComplete();
            };
            #endregion

            #region 维修发票
            ApuMaintainInvoices = _service.CreateCollection(_context.APUMaintainInvoices);
            ApuMaintainInvoices.LoadedData += (sender, e) =>
            {
                _loadApuMaintainInvoice = true;
                LoadMaintainInvoiceComplete();
            };

            EngineMaintainInvoices = _service.CreateCollection(_context.EngineMaintainInvoices);
            EngineMaintainInvoices.LoadedData += (sender, e) =>
            {
                _loadEngineMaintainInvoice = true;
                LoadMaintainInvoiceComplete();
            };

            AirframeMaintainInvoices = _service.CreateCollection(_context.AirframeMaintainInvoices);
            AirframeMaintainInvoices.LoadedData += (sender, e) =>
            {
                _loadAirframeMaintainInvoice = true;
                LoadMaintainInvoiceComplete();
            };

            UndercartMaintainInvoices = _service.CreateCollection(_context.UndercartMaintainInvoices);
            UndercartMaintainInvoices.LoadedData += (sender, e) =>
            {
                _loadUndercartMaintainInvoice = true;
                LoadMaintainInvoiceComplete();
            };

            SpecialRefitInvoices = _service.CreateCollection(_context.SpecialRefitInvoices);
            SpecialRefitInvoices.LoadedData += (sender, e) =>
            {
                _loadSpecialRefitInvoice = true;
                LoadMaintainInvoiceComplete();
            };
            #endregion
            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Annuals);
            var sort = new SortDescriptor { Member = "Year", SortDirection = ListSortDirection.Ascending };
            Annuals.SortDescriptors.Add(sort);
            Annuals.LoadedData += (o, e) =>
                                  {
                                      #region 维修成本
                                      _maintainCostReports = new ObservableCollection<MaintainCost>();
                                      _loadedApuMaintainCost = false;
                                      ApuMaintainCosts.Load(true);
                                      _loadedFhaMaintainCost = false;
                                      FhaMaintainCosts.Load(true);
                                      _loadedNonFhaMaintainCost = false;
                                      NonFhaMaintainCosts.Load(true);
                                      _loadedRegularCheckMaintainCost = false;
                                      RegularCheckMaintainCosts.Load(true);
                                      _loadedSpecialRefitMaintainCost = false;
                                      SpecialRefitMaintainCosts.Load(true);
                                      _loadedUndercartMaintainCost = false;
                                      UndercartMaintainCosts.Load(true);
                                      #endregion

                                      #region 维修发票
                                      Data = new ObservableCollection<MaintainItemData>();
                                      _loadApuMaintainInvoice = false;
                                      ApuMaintainInvoices.Load(true);
                                      _loadUndercartMaintainInvoice = false;
                                      UndercartMaintainInvoices.Load(true);
                                      _loadEngineMaintainInvoice = false;
                                      EngineMaintainInvoices.Load(true);
                                      _loadAirframeMaintainInvoice = false;
                                      AirframeMaintainInvoices.Load(true);
                                      _loadSpecialRefitInvoice = false;
                                      SpecialRefitInvoices.Load(true);
                                      #endregion
                                  };
        }

        private void LoadMaintainCostComplete()
        {
            if (_loadedApuMaintainCost && _loadedFhaMaintainCost && _loadedNonFhaMaintainCost && _loadedRegularCheckMaintainCost && _loadedSpecialRefitMaintainCost &&
                _loadedUndercartMaintainCost)
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
                    maintainCostReport.ApuBudget = ApuMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.IncludeAddedValue);
                    maintainCostReport.TotalBudget += maintainCostReport.ApuBudget;
                    maintainCostReport.ApuActual = ApuMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.AcutalAmount);
                    maintainCostReport.TotalActual += maintainCostReport.ApuActual;

                    maintainCostReport.FhaBudget = FhaMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.CustomAdded);
                    maintainCostReport.TotalBudget += maintainCostReport.FhaBudget;
                    maintainCostReport.FhaActual = FhaMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.AcutalAmount);
                    maintainCostReport.TotalActual += maintainCostReport.FhaActual;

                    maintainCostReport.NonFhaBudget = NonFhaMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.FinancialApprovalAmount);
                    maintainCostReport.TotalBudget += maintainCostReport.NonFhaBudget;
                    maintainCostReport.NonFhaActual = NonFhaMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.AcutalAmount);
                    maintainCostReport.TotalActual += maintainCostReport.NonFhaActual;

                    maintainCostReport.RegularCheckBudget = RegularCheckMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.FinancialApprovalAmount);
                    maintainCostReport.TotalBudget += maintainCostReport.RegularCheckBudget;
                    maintainCostReport.RegularCheckActual = RegularCheckMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.AcutalAmount);
                    maintainCostReport.TotalActual += maintainCostReport.RegularCheckActual;

                    maintainCostReport.SpecialRefitBudget = SpecialRefitMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.FinancialApprovalAmount);
                    maintainCostReport.TotalBudget += maintainCostReport.SpecialRefitBudget;
                    maintainCostReport.SpecialRefitActual = SpecialRefitMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.AcutalAmount);
                    maintainCostReport.TotalActual += maintainCostReport.SpecialRefitActual;

                    maintainCostReport.UndercartBudget = UndercartMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.FinancialApprovalAmount);
                    maintainCostReport.TotalBudget += maintainCostReport.UndercartBudget;
                    maintainCostReport.UndercartActual = UndercartMaintainCosts.Where(q => q.Year == p.Year).Sum(t => t.AcutalAmount);
                    maintainCostReport.TotalActual += maintainCostReport.UndercartActual;
                    if (!exist && maintainCostReport.TotalBudget != 0 && maintainCostReport.TotalActual != 0)
                        MaintainCostReports.Add(maintainCostReport);
                });

                RaisePropertyChanged(() => MaintainCostReports);
                var maintainCost = MaintainCostReports.LastOrDefault();
                GenerateDetail(maintainCost);
                IsBusy = false;
            }
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
        #endregion

        #region 维修项分析
        public QueryableDataServiceCollectionView<APUMaintainInvoiceDTO> ApuMaintainInvoices { get; set; }
        private bool _loadApuMaintainInvoice;
        public QueryableDataServiceCollectionView<EngineMaintainInvoiceDTO> EngineMaintainInvoices { get; set; }
        private bool _loadEngineMaintainInvoice;
        public QueryableDataServiceCollectionView<AirframeMaintainInvoiceDTO> AirframeMaintainInvoices { get; set; }
        private bool _loadAirframeMaintainInvoice;
        public QueryableDataServiceCollectionView<UndercartMaintainInvoiceDTO> UndercartMaintainInvoices { get; set; }
        private bool _loadUndercartMaintainInvoice;
        public QueryableDataServiceCollectionView<SpecialRefitInvoiceDTO> SpecialRefitInvoices { get; set; }
        private bool _loadSpecialRefitInvoice;

        public ObservableCollection<MaintainItemData> Data { get; set; }

        private void LoadMaintainInvoiceComplete()
        {
            if (_loadUndercartMaintainInvoice && _loadApuMaintainInvoice && _loadEngineMaintainInvoice &&
                _loadAirframeMaintainInvoice && _loadSpecialRefitInvoice)
            {
                Annuals.ToList().ForEach(p =>
                                         {
                                             var apuMaintainInvoices = ApuMaintainInvoices.Where(q => q.InvoiceDate.Year == p.Year).ToList();
                                             apuMaintainInvoices.ForEach(t => t.MaintainInvoiceLines.ToList().ForEach(u =>
                                                                                                                    {
                                                                                                                        var data = Data.FirstOrDefault(o => o.Type == u.ItemName);
                                                                                                                        if (data == null)
                                                                                                                        {
                                                                                                                            data = new MaintainItemData
                                                                                                                                   {
                                                                                                                                       Name = ((ItemNameType)u.ItemName).ToString(),
                                                                                                                                       Type = u.ItemName,
                                                                                                                                       Data = new ObservableCollection<MaintainItem>()
                                                                                                                                   };
                                                                                                                            Data.Add(data);
                                                                                                                        }
                                                                                                                        var detail = data.Data.FirstOrDefault(o => o.Year == p.Year);
                                                                                                                        if (detail == null)
                                                                                                                        {
                                                                                                                            detail = new MaintainItem { Name = data.Name, Year = p.Year, Amount = u.Amount * u.UnitPrice };
                                                                                                                            data.Data.Add(detail);
                                                                                                                        }
                                                                                                                        else
                                                                                                                        {
                                                                                                                            detail.Amount += u.Amount * u.UnitPrice;
                                                                                                                        }
                                                                                                                    }));
                                             var engineMaintainInvoices = EngineMaintainInvoices.Where(q => q.InvoiceDate.Year == p.Year).ToList();
                                             engineMaintainInvoices.ForEach(t => t.MaintainInvoiceLines.ToList().ForEach(u =>
                                             {
                                                 var data = Data.FirstOrDefault(o => o.Type == u.ItemName);
                                                 if (data == null)
                                                 {
                                                     data = new MaintainItemData
                                                     {
                                                         Name = ((ItemNameType)u.ItemName).ToString(),
                                                         Type = u.ItemName,
                                                         Data = new ObservableCollection<MaintainItem>()
                                                     };
                                                     Data.Add(data);
                                                 }
                                                 var detail = data.Data.FirstOrDefault(o => o.Year == p.Year);
                                                 if (detail == null)
                                                 {
                                                     detail = new MaintainItem { Name = data.Name, Year = p.Year, Amount = u.Amount * u.UnitPrice };
                                                     data.Data.Add(detail);
                                                 }
                                                 else
                                                 {
                                                     detail.Amount += u.Amount * u.UnitPrice;
                                                 }
                                             }));
                                             var airframeMaintainInvoices = AirframeMaintainInvoices.Where(q => q.InvoiceDate.Year == p.Year).ToList();
                                             airframeMaintainInvoices.ForEach(t => t.MaintainInvoiceLines.ToList().ForEach(u =>
                                             {
                                                 var data = Data.FirstOrDefault(o => o.Type == u.ItemName);
                                                 if (data == null)
                                                 {
                                                     data = new MaintainItemData
                                                     {
                                                         Name = ((ItemNameType)u.ItemName).ToString(),
                                                         Type = u.ItemName,
                                                         Data = new ObservableCollection<MaintainItem>()
                                                     };
                                                     Data.Add(data);
                                                 }
                                                 var detail = data.Data.FirstOrDefault(o => o.Year == p.Year);
                                                 if (detail == null)
                                                 {
                                                     detail = new MaintainItem { Name = data.Name, Year = p.Year, Amount = u.Amount * u.UnitPrice };
                                                     data.Data.Add(detail);
                                                 }
                                                 else
                                                 {
                                                     detail.Amount += u.Amount * u.UnitPrice;
                                                 }
                                             }));
                                             var undercartMaintainInvoices = UndercartMaintainInvoices.Where(q => q.InvoiceDate.Year == p.Year).ToList();
                                             undercartMaintainInvoices.ForEach(t => t.MaintainInvoiceLines.ToList().ForEach(u =>
                                             {
                                                 var data = Data.FirstOrDefault(o => o.Type == u.ItemName);
                                                 if (data == null)
                                                 {
                                                     data = new MaintainItemData
                                                     {
                                                         Name = ((ItemNameType)u.ItemName).ToString(),
                                                         Type = u.ItemName,
                                                         Data = new ObservableCollection<MaintainItem>()
                                                     };
                                                     Data.Add(data);
                                                 }
                                                 var detail = data.Data.FirstOrDefault(o => o.Year == p.Year);
                                                 if (detail == null)
                                                 {
                                                     detail = new MaintainItem { Name = data.Name, Year = p.Year, Amount = u.Amount * u.UnitPrice };
                                                     data.Data.Add(detail);
                                                 }
                                                 else
                                                 {
                                                     detail.Amount += u.Amount * u.UnitPrice;
                                                 }
                                             }));
                                             var specialRefitInvoices = SpecialRefitInvoices.Where(q => q.InvoiceDate.Year == p.Year).ToList();
                                             specialRefitInvoices.ForEach(t => t.MaintainInvoiceLines.ToList().ForEach(u =>
                                             {
                                                 var data = Data.FirstOrDefault(o => o.Type == u.ItemName);
                                                 if (data == null)
                                                 {
                                                     data = new MaintainItemData
                                                     {
                                                         Name = ((ItemNameType)u.ItemName).ToString(),
                                                         Type = u.ItemName,
                                                         Data = new ObservableCollection<MaintainItem>()
                                                     };
                                                     Data.Add(data);
                                                 }
                                                 var detail = data.Data.FirstOrDefault(o => o.Year == p.Year);
                                                 if (detail == null)
                                                 {
                                                     detail = new MaintainItem { Name = data.Name, Year = p.Year, Amount = u.Amount * u.UnitPrice };
                                                     data.Data.Add(detail);
                                                 }
                                                 else
                                                 {
                                                     detail.Amount += u.Amount * u.UnitPrice;
                                                 }
                                             }));
                                         });
                RaisePropertyChanged(() => Data);
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

    public class MaintainItem
    {
        public string Name { get; set; }
        public int Year { get; set; }
        public decimal Amount { get; set; }
    }

    public class MaintainItemData
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public ObservableCollection<MaintainItem> Data { get; set; }
    }
}
