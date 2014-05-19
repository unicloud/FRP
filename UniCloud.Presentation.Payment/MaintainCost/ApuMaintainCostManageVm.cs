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
using UniCloud.Presentation.CommonExtension;
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
                    _annualFilter.Value = _annual.Id;
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
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            DeleteCommand = new DelegateCommand<object>(OnDelete, CanDelete);
            ApuMaintainInvoices = new QueryableDataServiceCollectionView<APUMaintainInvoiceDTO>(_context, _context.APUMaintainInvoices);
            ApuMaintainCosts = _service.CreateCollection(_context.ApuMaintainCosts);
            ApuMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("AnnualId", FilterOperator.IsEqualTo, Guid.Empty);
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
                    Annual = Annuals.FirstOrDefault();
            };
        }

        #endregion

        #region 命令

        #region 新增保证金命令

        public DelegateCommand<object> AddCommand { get; set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAdd(object sender)
        {
            ApuMaintainCost = new ApuMaintainCostDTO
                                     {
                                         Id = RandomHelper.Next(),
                                         AnnualId = Annual.Id
                                     };
            var invoice = ApuMaintainInvoices.FirstOrDefault();
            if (invoice != null)
            {
                ApuMaintainCost.MaintainInvoiceId = invoice.APUMaintainInvoiceId;
                ApuMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                ApuMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
            ApuMaintainCosts.AddNew(ApuMaintainCost);
        }

        /// <summary>
        ///     判断新增保证金命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAdd(object sender)
        {
            return true;
        }

        #endregion

        #region 删除保证金命令

        public DelegateCommand<object> DeleteCommand { get; set; }

        /// <summary>
        ///     执行删除保证金命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelete(object sender)
        {
            if (ApuMaintainCost == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                ApuMaintainCosts.Remove(ApuMaintainCost);
                                                ApuMaintainCost = ApuMaintainCosts.FirstOrDefault();
                                            });
        }

        /// <summary>
        ///     判断删除保证金命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDelete(object sender)
        {
            return true;
        }

        #endregion

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

        public void SelectedChanged(object sender)
        {
            if (sender is APUMaintainInvoiceDTO)
            {
                var invoice = sender as APUMaintainInvoiceDTO;
                if (ApuMaintainCost.MaintainInvoiceId != invoice.APUMaintainInvoiceId)
                {
                    ApuMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                    ApuMaintainCost.AcutalAmount = invoice.PaidAmount;
                }
            }
        }

        #region 单元格编辑事件
        public DelegateCommand<object> CellEditEndCommand { get; set; }

        private void CellEditEnd(object sender)
        {
            ApuMaintainCost.YearBudgetRate = ApuMaintainCost.LastYearRate * ApuMaintainCost.YearAddedRate;
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