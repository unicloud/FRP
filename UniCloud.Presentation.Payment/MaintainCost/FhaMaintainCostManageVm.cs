#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/19 9:28:58
// 文件名：FhaMaintainCostManageVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/19 9:28:58
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
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

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(FhaMaintainCostManageVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FhaMaintainCostManageVm : EditViewModelBase
    {
        private readonly IPaymentService _service;
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
        private FilterDescriptor _annualFilter;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public FhaMaintainCostManageVm(IPaymentService service, IFleetPlanService fleetPlanService)
            : base(service)
        {
            _fleetPlanService = fleetPlanService;
            _service = service;
            _context = _service.Context;
            InitialVm(); //初始化Fha维修成本

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
                    FhaMaintainCosts.Load(true);
                }
                RaisePropertyChanged(() => Annual);
            }
        }
        #endregion

        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }

        #region 发票
        public QueryableDataServiceCollectionView<EngineMaintainInvoiceDTO> EngineMaintainInvoices { get; set; }
        #endregion

        #region 加载Fha维修成本

        private FhaMaintainCostDTO _fhaMaintainCost;

        /// <summary>
        ///     选择Fha维修成本。
        /// </summary>
        public FhaMaintainCostDTO FhaMaintainCost
        {
            get { return _fhaMaintainCost; }
            set
            {
                if (_fhaMaintainCost != value)
                {
                    _fhaMaintainCost = value;
                    RaisePropertyChanged(() => FhaMaintainCost);
                }
            }
        }

        /// <summary>
        ///     获取所有Fha维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<FhaMaintainCostDTO> FhaMaintainCosts { get; set; }

        /// <summary>
        ///     初始化Fha维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            CellEditEndCommand = new DelegateCommand<object>(CellEditEnd);
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            DeleteCommand = new DelegateCommand<object>(OnDelete, CanDelete);
            EngineMaintainInvoices = new QueryableDataServiceCollectionView<EngineMaintainInvoiceDTO>(_context, _context.EngineMaintainInvoices);
            FhaMaintainCosts = _service.CreateCollection(_context.FhaMaintainCosts);
            FhaMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("AnnualId", FilterOperator.IsEqualTo, Guid.Empty);
            FhaMaintainCosts.FilterDescriptors.Add(_annualFilter);
            FhaMaintainCosts.LoadedData += (sender, e) =>
            {
                if (FhaMaintainCost == null)
                    FhaMaintainCost = FhaMaintainCosts.FirstOrDefault();
                RefreshCommandState();
            };

            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Annuals);
            Annuals.LoadedData += (o, e) =>
            {
                if (Annual == null)
                    Annual = Annuals.FirstOrDefault();
            };
            AircraftTypes = new QueryableDataServiceCollectionView<AircraftTypeDTO>(_fleetPlanService.Context, _fleetPlanService.Context.AircraftTypes);
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
            FhaMaintainCost = new FhaMaintainCostDTO
                                     {
                                         Id = RandomHelper.Next(),
                                         AnnualId = Annual.Id
                                     };
            var invoice = EngineMaintainInvoices.FirstOrDefault();
            if (invoice != null)
            {
                FhaMaintainCost.MaintainInvoiceId = invoice.EngineMaintainInvoiceId;
                FhaMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                FhaMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
            FhaMaintainCosts.AddNew(FhaMaintainCost);
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
            if (FhaMaintainCost == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                FhaMaintainCosts.Remove(FhaMaintainCost);
                                                FhaMaintainCost = FhaMaintainCosts.FirstOrDefault();
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
            if (!FhaMaintainCosts.AutoLoad)
                FhaMaintainCosts.AutoLoad = true;
            EngineMaintainInvoices.Load(true);
            AircraftTypes.Load(true);
        }

        #endregion

        public void SelectedChanged(object sender)
        {
            if (sender is EngineMaintainInvoiceDTO)
            {

            }
        }

        #region 单元格编辑事件
        public DelegateCommand<object> CellEditEndCommand { get; set; }

        private void CellEditEnd(object sender)
        {
            var invoice = EngineMaintainInvoices.FirstOrDefault(p => p.EngineMaintainInvoiceId == FhaMaintainCost.MaintainInvoiceId);
            if (invoice != null)
            {
                FhaMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                FhaMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
            FhaMaintainCost.YearBudgetRate = FhaMaintainCost.LastYearRate * (1 + FhaMaintainCost.YearAddedRate);
            FhaMaintainCost.Hour = FhaMaintainCost.AirHour * FhaMaintainCost.HourPercent * 2;
            FhaMaintainCost.FhaFeeUsd = FhaMaintainCost.Hour * FhaMaintainCost.YearBudgetRate;
            FhaMaintainCost.FhaFeeRmb = FhaMaintainCost.FhaFeeUsd * FhaMaintainCost.Rate;
            FhaMaintainCost.CustomAddedRmb = FhaMaintainCost.FhaFeeRmb * FhaMaintainCost.Custom;
            FhaMaintainCost.TotalTax = FhaMaintainCost.FhaFeeRmb + FhaMaintainCost.CustomAddedRmb;
            FhaMaintainCost.AddedValue = FhaMaintainCost.AddedValueRate * FhaMaintainCost.TotalTax;
            FhaMaintainCost.IncludeAddedValue = FhaMaintainCost.AddedValue + FhaMaintainCost.TotalTax;
            FhaMaintainCost.CustomAdded = FhaMaintainCost.CustomAddedRmb + FhaMaintainCost.AddedValue;
        }
        #endregion
    }
}
