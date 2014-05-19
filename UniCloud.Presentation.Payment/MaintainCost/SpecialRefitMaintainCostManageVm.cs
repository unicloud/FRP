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
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(SpecialRefitMaintainCostManageVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SpecialRefitMaintainCostManageVm : EditViewModelBase
    {
        private readonly IPaymentService _service;
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
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
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            DeleteCommand = new DelegateCommand<object>(OnDelete, CanDelete);
            SpecialRefitInvoices = new QueryableDataServiceCollectionView<SpecialRefitInvoiceDTO>(_context, _context.SpecialRefitInvoices);
            SpecialRefitMaintainCosts = _service.CreateCollection(_context.SpecialRefitMaintainCosts);
            SpecialRefitMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("AnnualId", FilterOperator.IsEqualTo, Guid.Empty);
            SpecialRefitMaintainCosts.FilterDescriptors.Add(_annualFilter);
            SpecialRefitMaintainCosts.LoadedData += (sender, e) =>
            {
                if (SpecialRefitMaintainCost == null)
                    SpecialRefitMaintainCost = SpecialRefitMaintainCosts.FirstOrDefault();
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
            SpecialRefitMaintainCost = new SpecialRefitMaintainCostDTO
                                     {
                                         Id = RandomHelper.Next(),
                                         AnnualId = Annual.Id
                                     };

            var invoice = SpecialRefitInvoices.FirstOrDefault();
            if (invoice != null)
            {
                SpecialRefitMaintainCost.MaintainInvoiceId = invoice.SpecialRefitId;
                SpecialRefitMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                SpecialRefitMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
            SpecialRefitMaintainCosts.AddNew(SpecialRefitMaintainCost);
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
            if (SpecialRefitMaintainCost == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                SpecialRefitMaintainCosts.Remove(SpecialRefitMaintainCost);
                                                SpecialRefitMaintainCost = SpecialRefitMaintainCosts.FirstOrDefault();
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
            if (!SpecialRefitMaintainCosts.AutoLoad)
                SpecialRefitMaintainCosts.AutoLoad = true;
            SpecialRefitInvoices.Load(true);
        }

        #endregion

        public void SelectedChanged(object sender)
        {
            if (sender is SpecialRefitInvoiceDTO)
            {
                var invoice = sender as SpecialRefitInvoiceDTO;
                if (SpecialRefitMaintainCost.MaintainInvoiceId != invoice.SpecialRefitId)
                {
                    SpecialRefitMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                    SpecialRefitMaintainCost.AcutalAmount = invoice.PaidAmount;
                }
            }
        }
    }
}

