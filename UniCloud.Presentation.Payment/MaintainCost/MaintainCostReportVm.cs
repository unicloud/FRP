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

        private ObservableCollection<MaintainCost> _apuMaintainCostReports;
        public ObservableCollection<MaintainCost> ApuMaintainCostReports
        {
            get { return _apuMaintainCostReports; }
            set
            {
                _apuMaintainCostReports = value;
                RaisePropertyChanged(() => ApuMaintainCostReports);
            }
        }

        private ObservableCollection<MaintainCost> _fhaMaintainCostReports;
        public ObservableCollection<MaintainCost> FhaMaintainCostReports
        {
            get { return _fhaMaintainCostReports; }
            set
            {
                _fhaMaintainCostReports = value;
                RaisePropertyChanged(() => FhaMaintainCostReports);
            }
        }

        private ObservableCollection<MaintainCost> _nonFhaMaintainCostReports;
        public ObservableCollection<MaintainCost> NonFhaMaintainCostReports
        {
            get { return _nonFhaMaintainCostReports; }
            set
            {
                _nonFhaMaintainCostReports = value;
                RaisePropertyChanged(() => NonFhaMaintainCostReports);
            }
        }

        private ObservableCollection<MaintainCost> _regularCheckMaintainCostReports;
        public ObservableCollection<MaintainCost> RegularCheckMaintainCostReports
        {
            get { return _regularCheckMaintainCostReports; }
            set
            {
                _regularCheckMaintainCostReports = value;
                RaisePropertyChanged(() => RegularCheckMaintainCostReports);
            }
        }

        private ObservableCollection<MaintainCost> _specialRefitMaintainCostReports;
        public ObservableCollection<MaintainCost> SpecialRefitMaintainCostReports
        {
            get { return _specialRefitMaintainCostReports; }
            set
            {
                _specialRefitMaintainCostReports = value;
                RaisePropertyChanged(() => SpecialRefitMaintainCostReports);
            }
        }

        private ObservableCollection<MaintainCost> _undercartMaintainCostReports;
        public ObservableCollection<MaintainCost> UndercartMaintainCostReports
        {
            get { return _undercartMaintainCostReports; }
            set
            {
                _undercartMaintainCostReports = value;
                RaisePropertyChanged(() => UndercartMaintainCostReports);
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
        /// <summary>
        ///     获取所有Fha维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<FhaMaintainCostDTO> FhaMaintainCosts { get; set; }
        /// <summary>
        ///     获取所有非FHA.超包修维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<NonFhaMaintainCostDTO> NonFhaMaintainCosts { get; set; }
        /// <summary>
        ///     获取所有定检维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<RegularCheckMaintainCostDTO> RegularCheckMaintainCosts { get; set; }
        /// <summary>
        ///     获取所有特修改装维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SpecialRefitMaintainCostDTO> SpecialRefitMaintainCosts { get; set; }
        /// <summary>
        ///     获取所有起落架维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<UndercartMaintainCostDTO> UndercartMaintainCosts { get; set; }

        /// <summary>
        ///     初始化维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            ApuMaintainCosts = _service.CreateCollection(_context.ApuMaintainCosts);
            ApuMaintainCosts.LoadedData += (sender, e) =>
            {
                var maintainCostReports = new ObservableCollection<MaintainCost>();
                Annuals.ToList().ForEach(p =>
                                         {
                                             var maintainCostReport = new MaintainCost
                                                                         {
                                                                             Annual = new DateTime(p.Year, 1, 1),
                                                                             Budget = ApuMaintainCosts.Where(
                                                                                     q => q.AnnualId == p.Id)
                                                                                 .Sum(t => t.IncludeAddedValue),
                                                                             Actual = ApuMaintainCosts.Where(
                                                                                 q => q.AnnualId == p.Id)
                                                                             .Sum(t => t.AcutalAmount)
                                                                         };
                                             if (maintainCostReport.Budget != 0 && maintainCostReport.Actual != 0)
                                                 maintainCostReports.Add(maintainCostReport);
                                         });
                ApuMaintainCostReports = maintainCostReports;
            };
            FhaMaintainCosts = _service.CreateCollection(_context.FhaMaintainCosts);
            FhaMaintainCosts.LoadedData += (sender, e) =>
            {
                var maintainCostReports = new ObservableCollection<MaintainCost>();
                Annuals.ToList().ForEach(p =>
                {
                    var maintainCostReport = new MaintainCost
                    {
                        Annual = new DateTime(p.Year, 1, 1),
                        Budget = FhaMaintainCosts.Where(
                                q => q.AnnualId == p.Id)
                            .Sum(t => t.CustomAdded),
                        Actual = FhaMaintainCosts.Where(
                            q => q.AnnualId == p.Id)
                        .Sum(t => t.AcutalAmount)
                    };
                    if (maintainCostReport.Budget != 0 && maintainCostReport.Actual != 0)
                        maintainCostReports.Add(maintainCostReport);
                });
                FhaMaintainCostReports = maintainCostReports;
            };
            NonFhaMaintainCosts = _service.CreateCollection(_context.NonFhaMaintainCosts);
            NonFhaMaintainCosts.LoadedData += (sender, e) =>
            {
                var maintainCostReports = new ObservableCollection<MaintainCost>();
                Annuals.ToList().ForEach(p =>
                {
                    var maintainCostReport = new MaintainCost
                    {
                        Annual = new DateTime(p.Year, 1, 1),
                        Budget = NonFhaMaintainCosts.Where(
                                q => q.AnnualId == p.Id)
                            .Sum(t => t.FinancialApprovalAmount),
                        Actual = NonFhaMaintainCosts.Where(
                            q => q.AnnualId == p.Id)
                        .Sum(t => t.AcutalAmount)
                    };
                    if (maintainCostReport.Budget != 0 && maintainCostReport.Actual != 0)
                        maintainCostReports.Add(maintainCostReport);
                });
                NonFhaMaintainCostReports = maintainCostReports;
            };
            RegularCheckMaintainCosts = _service.CreateCollection(_context.RegularCheckMaintainCosts);
            RegularCheckMaintainCosts.LoadedData += (sender, e) =>
            {
                var maintainCostReports = new ObservableCollection<MaintainCost>();
                Annuals.ToList().ForEach(p =>
                {
                    var maintainCostReport = new MaintainCost
                    {
                        Annual = new DateTime(p.Year, 1, 1),
                        Budget = RegularCheckMaintainCosts.Where(
                                q => q.AnnualId == p.Id)
                            .Sum(t => t.FinancialApprovalAmount),
                        Actual = RegularCheckMaintainCosts.Where(
                            q => q.AnnualId == p.Id)
                        .Sum(t => t.AcutalAmount)
                    };
                    if (maintainCostReport.Budget != 0 && maintainCostReport.Actual != 0)
                        maintainCostReports.Add(maintainCostReport);
                });
                RegularCheckMaintainCostReports = maintainCostReports;
            };
            SpecialRefitMaintainCosts = _service.CreateCollection(_context.SpecialRefitMaintainCosts);
            SpecialRefitMaintainCosts.LoadedData += (sender, e) =>
            {
                var maintainCostReports = new ObservableCollection<MaintainCost>();
                Annuals.ToList().ForEach(p =>
                {
                    var maintainCostReport = new MaintainCost
                    {
                        Annual = new DateTime(p.Year, 1, 1),
                        Budget = SpecialRefitMaintainCosts.Where(
                                q => q.AnnualId == p.Id)
                            .Sum(t => t.FinancialApprovalAmount),
                        Actual = SpecialRefitMaintainCosts.Where(
                            q => q.AnnualId == p.Id)
                        .Sum(t => t.AcutalAmount)
                    };
                    if (maintainCostReport.Budget != 0 && maintainCostReport.Actual != 0)
                        maintainCostReports.Add(maintainCostReport);
                });
                SpecialRefitMaintainCostReports = maintainCostReports;
            };
            UndercartMaintainCosts = _service.CreateCollection(_context.UndercartMaintainCosts);
            UndercartMaintainCosts.LoadedData += (sender, e) =>
            {
                var maintainCostReports = new ObservableCollection<MaintainCost>();
                Annuals.ToList().ForEach(p =>
                {
                    var maintainCostReport = new MaintainCost
                    {
                        Annual = new DateTime(p.Year, 1, 1),
                        Budget = UndercartMaintainCosts.Where(
                                q => q.AnnualId == p.Id)
                            .Sum(t => t.FinancialApprovalAmount),
                        Actual = UndercartMaintainCosts.Where(
                            q => q.AnnualId == p.Id)
                        .Sum(t => t.AcutalAmount)
                    };
                    if (maintainCostReport.Budget != 0 && maintainCostReport.Actual != 0)
                        maintainCostReports.Add(maintainCostReport);
                });
                UndercartMaintainCostReports = maintainCostReports;
            };
            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Annuals);
            Annuals.LoadedData += (o, e) =>
                                  {
                                      ApuMaintainCosts.Load(true);
                                      FhaMaintainCosts.Load(true);
                                      NonFhaMaintainCosts.Load(true);
                                      RegularCheckMaintainCosts.Load(true);
                                      SpecialRefitMaintainCosts.Load(true);
                                      UndercartMaintainCosts.Load(true);
                                  };
        }

        #endregion

        #region 命令

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
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
                    var maintainCost = selectedPoint.DataItem as MaintainCost;
                    if (maintainCost != null)
                    {
                        SelectTitle = maintainCost.Annual.Year + "年维修成本明细";
                        var maintainCostDetails = new ObservableCollection<MaintainCostDetail>();
                        var regularCheckMaintainCost = RegularCheckMaintainCostReports.FirstOrDefault(p => p.Annual.Year == maintainCost.Annual.Year);
                        if (regularCheckMaintainCost != null)
                        {
                            var maintainCostDetail = new MaintainCostDetail
                                                     {
                                                         Name = "年度定检",
                                                         Actual = regularCheckMaintainCost.Actual,
                                                         Budget = regularCheckMaintainCost.Budget
                                                     };
                            maintainCostDetails.Add(maintainCostDetail);
                        }
                        var nonFhaMaintainCost = NonFhaMaintainCostReports.FirstOrDefault(p => p.Annual.Year == maintainCost.Annual.Year);
                        if (nonFhaMaintainCost != null)
                        {
                            var maintainCostDetail = new MaintainCostDetail
                                                     {
                                                         Name = "非FHA.超包修",
                                                         Actual = nonFhaMaintainCost.Actual,
                                                         Budget = nonFhaMaintainCost.Budget
                                                     };
                            maintainCostDetails.Add(maintainCostDetail);
                        }
                        var undercartMaintainCost = UndercartMaintainCostReports.FirstOrDefault(p => p.Annual.Year == maintainCost.Annual.Year);
                        if (undercartMaintainCost != null)
                        {
                            var maintainCostDetail = new MaintainCostDetail
                                                     {
                                                         Name = "起落架",
                                                         Actual = undercartMaintainCost.Actual,
                                                         Budget = undercartMaintainCost.Budget
                                                     };
                            maintainCostDetails.Add(maintainCostDetail);
                        }
                        var specialRefitMaintainCost = SpecialRefitMaintainCostReports.FirstOrDefault(p => p.Annual.Year == maintainCost.Annual.Year);
                        if (specialRefitMaintainCost != null)
                        {
                            var maintainCostDetail = new MaintainCostDetail
                                                     {
                                                         Name = "特修改装",
                                                         Actual = specialRefitMaintainCost.Actual,
                                                         Budget = specialRefitMaintainCost.Budget
                                                     };
                            maintainCostDetails.Add(maintainCostDetail);
                        }
                        var fhaMaintainCost = FhaMaintainCostReports.FirstOrDefault(p => p.Annual.Year == maintainCost.Annual.Year);
                        if (fhaMaintainCost != null)
                        {
                            var maintainCostDetail = new MaintainCostDetail
                                                     {
                                                         Name = "FHA",
                                                         Actual = fhaMaintainCost.Actual,
                                                         Budget = fhaMaintainCost.Budget
                                                     };
                            maintainCostDetails.Add(maintainCostDetail);
                        }
                        var apuMaintainCost = ApuMaintainCostReports.FirstOrDefault(p => p.Annual.Year == maintainCost.Annual.Year);
                        if (apuMaintainCost != null)
                        {
                            var maintainCostDetail = new MaintainCostDetail
                                                     {
                                                         Name = "APU",
                                                         Actual = apuMaintainCost.Actual,
                                                         Budget = apuMaintainCost.Budget
                                                     };
                            maintainCostDetails.Add(maintainCostDetail);
                        }
                        MaintainCostDetails = maintainCostDetails;
                    }
                }
            }
        }
    }

    public class MaintainCost
    {
        public DateTime Annual { get; set; }
        public decimal Budget { get; set; }
        public decimal Actual { get; set; }
    }

    public class MaintainCostDetail
    {
        public string Name { get; set; }
        public decimal Actual { get; set; }
        public decimal Budget { get; set; }
    }
}
