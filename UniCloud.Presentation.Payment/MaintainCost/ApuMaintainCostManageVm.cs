#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 15:43:45
// 文件名：ApuMaintainCostManageVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 15:43:45
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
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
    [Export(typeof(ApuMaintainCostManageVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ApuMaintainCostManageVm : EditViewModelBase
    {
        private readonly IPaymentService _service;
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
        private FilterDescriptor _annualFilter;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public ApuMaintainCostManageVm(IPaymentService service, IFleetPlanService fleetPlanService)
            : base(service)
        {
            _fleetPlanService = fleetPlanService;
            _service = service;
            _context = _service.Context;
            InitialVm(); //初始化Apu维修成本

        }
        #region 年度
        public QueryableDataServiceCollectionView<AnnualDTO> Annuals { get; set; }
        private AnnualDTO _annual;
        public AnnualDTO Annual
        {
            get { return _annual; }
            set
            {
                _annual = value;
                if (_annual != null)
                {
                    _annualFilter.Value = _annual.Year;
                    ApuMaintainCosts.Load(true);
                }
                RaisePropertyChanged(() => Annual);
            }
        }
        #endregion

        #region 发票
        public QueryableDataServiceCollectionView<APUMaintainInvoiceDTO> ApuMaintainInvoices { get; set; }
        #endregion

        public Dictionary<int, MaintainCostType> MaintainCostTypes
        {
            get { return Enum.GetValues(typeof(MaintainCostType)).Cast<object>().ToDictionary(value => (int)value, value => (MaintainCostType)value); }
        }

        public Dictionary<int, ContractRepairtType> ContractRepairtTypes
        {
            get { return Enum.GetValues(typeof(ContractRepairtType)).Cast<object>().ToDictionary(value => (int)value, value => (ContractRepairtType)value); }
        }

        #region 加载Apu维修成本

        private ApuMaintainCostDTO _apuMaintainCost;

        /// <summary>
        ///     选择Apu维修成本。
        /// </summary>
        public ApuMaintainCostDTO ApuMaintainCost
        {
            get { return _apuMaintainCost; }
            set
            {
                if (_apuMaintainCost != value)
                {
                    _apuMaintainCost = value;
                    RaisePropertyChanged(() => ApuMaintainCost);
                }
            }
        }

        /// <summary>
        ///     获取所有Apu维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ApuMaintainCostDTO> ApuMaintainCosts { get; set; }

        /// <summary>
        ///     初始化Apu维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            CellEditEndCommand = new DelegateCommand<object>(CellEditEnd);
            ApuMaintainInvoices = new QueryableDataServiceCollectionView<APUMaintainInvoiceDTO>(_context, _context.APUMaintainInvoices);
            ApuMaintainCosts = _service.CreateCollection(_context.ApuMaintainCosts);
            ApuMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("Year", FilterOperator.IsEqualTo, 0);
            ApuMaintainCosts.FilterDescriptors.Add(_annualFilter);
            ApuMaintainCosts.LoadedData += (sender, e) =>
            {
                if (ApuMaintainCost == null)
                    ApuMaintainCost = ApuMaintainCosts.FirstOrDefault();
                RefreshCommandState();
            };

            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Annuals);
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
            if (!ApuMaintainCosts.AutoLoad)
                ApuMaintainCosts.AutoLoad = true;
            ApuMaintainInvoices.Load(true);
        }

        #endregion

        #region 单元格编辑事件
        public DelegateCommand<object> CellEditEndCommand { get; set; }

        private void CellEditEnd(object sender)
        {
            ApuMaintainCost.Hour = ApuMaintainCost.BudgetHour * ApuMaintainCost.HourPercent;
            ApuMaintainCost.ContractRepairFeeUsd = ApuMaintainCost.Hour * ApuMaintainCost.YearBudgetRate;
            ApuMaintainCost.ContractRepairFeeRmb = ApuMaintainCost.ContractRepairFeeUsd * ApuMaintainCost.Rate;
            ApuMaintainCost.TotalTax = ApuMaintainCost.ContractRepairFeeRmb * (1 + ApuMaintainCost.CustomRate);
            ApuMaintainCost.AddedValue = ApuMaintainCost.AddedValueRate * ApuMaintainCost.TotalTax;
            ApuMaintainCost.IncludeAddedValue = ApuMaintainCost.AddedValue + ApuMaintainCost.ContractRepairFeeRmb;
        }
        #endregion
    }
}