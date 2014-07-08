#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 10:00:39
// 文件名：SpecialRefitMaintainCostManageVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 10:00:39
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof (SpecialRefitMaintainCostManageVm))]
    public class SpecialRefitMaintainCostManageVm : EditViewModelBase
    {
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
        private readonly IPaymentService _service;
        private FilterDescriptor _annualFilter;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public SpecialRefitMaintainCostManageVm(IPaymentService service, IFleetPlanService fleetPlanService)
            : base(service)
        {
            _fleetPlanService = fleetPlanService;
            _service = service;
            _context = _service.Context;
            InitialVm(); //初始化特修改装维修成本
        }

        #region 年度

        private AnnualDTO _annual;
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }

        public AnnualDTO Annual
        {
            get { return _annual; }
            set
            {
                _annual = value;
                if (_annual != null)
                {
                    _annualFilter.Value = _annual.Year;
                    SpecialRefitMaintainCosts.Load(true);
                }
                RaisePropertyChanged(() => Annual);
            }
        }

        #endregion

        #region 发票

        public QueryableDataServiceCollectionView<SpecialRefitInvoiceDTO> SpecialRefitInvoices { get; set; }

        #endregion

        #region 加载特修改装维修成本

        private SpecialRefitMaintainCostDTO _specialRefitMaintainCost;

        /// <summary>
        ///     选择特修改装维修成本。
        /// </summary>
        public SpecialRefitMaintainCostDTO SpecialRefitMaintainCost
        {
            get { return _specialRefitMaintainCost; }
            set
            {
                if (_specialRefitMaintainCost != value)
                {
                    _specialRefitMaintainCost = value;
                    RaisePropertyChanged(() => SpecialRefitMaintainCost);
                }
            }
        }

        /// <summary>
        ///     获取所有特修改装维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SpecialRefitMaintainCostDTO> SpecialRefitMaintainCosts { get; set; }

        /// <summary>
        ///     初始化特修改装维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            CellEditEndCommand = new DelegateCommand<object>(CellEditEnd);
            SpecialRefitInvoices = new QueryableDataServiceCollectionView<SpecialRefitInvoiceDTO>(_context,
                _context.SpecialRefitInvoices);
            SpecialRefitMaintainCosts = _service.CreateCollection(_context.SpecialRefitMaintainCosts);
            SpecialRefitMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("Year", FilterOperator.IsEqualTo, 0);
            SpecialRefitMaintainCosts.FilterDescriptors.Add(_annualFilter);
            SpecialRefitMaintainCosts.LoadedData += (sender, e) =>
            {
                if (SpecialRefitMaintainCost == null)
                    SpecialRefitMaintainCost = SpecialRefitMaintainCosts.FirstOrDefault();
                RefreshCommandState();
            };
            _service.RegisterCollectionView(SpecialRefitInvoices);

            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_fleetPlanService.Context,
                _fleetPlanService.Context.Annuals);
            Annuals.LoadedData += (o, e) =>
            {
                if (Annual == null)
                    Annual = Annuals.FirstOrDefault(p => p.Year == DateTime.Now.Year);
            };
        }

        #endregion

        #region 命令

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            if (!Annuals.AutoLoad)
                Annuals.AutoLoad = true;
            if (!SpecialRefitMaintainCosts.AutoLoad)
                SpecialRefitMaintainCosts.AutoLoad = true;
            SpecialRefitInvoices.Load(true);
        }

        #endregion

        #region 单元格编辑事件

        public DelegateCommand<object> CellEditEndCommand { get; set; }

        private void CellEditEnd(object sender)
        {
        }

        #endregion
    }
}