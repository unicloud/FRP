#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/16 13:39:48
// 文件名：NonFhaMaintainCostManageVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/16 13:39:48
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
using SupplierDTO = UniCloud.Presentation.Service.FleetPlan.FleetPlan.SupplierDTO;

#endregion

namespace UniCloud.Presentation.Payment.MaintainCost
{
    [Export(typeof(NonFhaMaintainCostManageVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class NonFhaMaintainCostManageVm : EditViewModelBase
    {
        private readonly IPaymentService _service;
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
        private FilterDescriptor _annualFilter;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public NonFhaMaintainCostManageVm(IPaymentService service, IFleetPlanService fleetPlanService)
            : base(service)
        {
            _fleetPlanService = fleetPlanService;
            _service = service;
            _context = _service.Context;
            InitialVm(); //初始化非FHA.超包修维修成本

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
                    NonFhaMaintainCosts.Load(true);
                }
                RaisePropertyChanged(() => Annual);
            }
        }
        #endregion

        #region 飞机
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }
        public QueryableDataServiceCollectionView<ActionCategoryDTO> ActionCategories { get; set; }
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }
        #endregion

        #region 发票
        public QueryableDataServiceCollectionView<EngineMaintainInvoiceDTO> EngineMaintainInvoices { get; set; }
        #endregion

        public Dictionary<int, MaintainCostType> MaintainCostTypes
        {
            get { return Enum.GetValues(typeof(MaintainCostType)).Cast<object>().ToDictionary(value => (int)value, value => (MaintainCostType)value); }
        }

        public Dictionary<int, ContractRepairtType> ContractRepairtTypes
        {
            get { return Enum.GetValues(typeof(ContractRepairtType)).Cast<object>().ToDictionary(value => (int)value, value => (ContractRepairtType)value); }
        }

        #region 加载非FHA.超包修维修成本

        private NonFhaMaintainCostDTO _nonFhaMaintainCost;

        /// <summary>
        ///     选择非FHA.超包修维修成本。
        /// </summary>
        public NonFhaMaintainCostDTO NonFhaMaintainCost
        {
            get { return _nonFhaMaintainCost; }
            set
            {
                if (_nonFhaMaintainCost != value)
                {
                    _nonFhaMaintainCost = value;
                    RaisePropertyChanged(() => NonFhaMaintainCost);
                }
            }
        }

        /// <summary>
        ///     获取所有非FHA.超包修维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<NonFhaMaintainCostDTO> NonFhaMaintainCosts { get; set; }

        /// <summary>
        ///     初始化非FHA.超包修维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            CellEditEndCommand = new DelegateCommand<object>(CellEditEnd);
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            DeleteCommand = new DelegateCommand<object>(OnDelete, CanDelete);
            EngineMaintainInvoices = new QueryableDataServiceCollectionView<EngineMaintainInvoiceDTO>(_context, _context.EngineMaintainInvoices);
            NonFhaMaintainCosts = _service.CreateCollection(_context.NonFhaMaintainCosts);
            NonFhaMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("AnnualId", FilterOperator.IsEqualTo, Guid.Empty);
            NonFhaMaintainCosts.FilterDescriptors.Add(_annualFilter);
            NonFhaMaintainCosts.LoadedData += (sender, e) =>
            {
                if (NonFhaMaintainCost == null)
                    NonFhaMaintainCost = NonFhaMaintainCosts.FirstOrDefault();
                RefreshCommandState();
            };

            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Aircrafts);
            AircraftTypes = new QueryableDataServiceCollectionView<AircraftTypeDTO>(_fleetPlanService.Context, _fleetPlanService.Context.AircraftTypes);
            ActionCategories = new QueryableDataServiceCollectionView<ActionCategoryDTO>(_fleetPlanService.Context, _fleetPlanService.Context.ActionCategories);
            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Suppliers);
            Annuals = new QueryableDataServiceCollectionView<AnnualDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Annuals);
            Annuals.LoadedData += (o, e) =>
            {
                if (Annual == null)
                    Annual = Annuals.FirstOrDefault();
            };
        }

        #endregion

        #region 命令

        #region 新增非FHA.超保修命令

        public DelegateCommand<object> AddCommand { get; set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnAdd(object sender)
        {
            NonFhaMaintainCost = new NonFhaMaintainCostDTO
                                     {
                                         Id = RandomHelper.Next(),
                                         InMaintainTime = DateTime.Now,
                                         OutMaintainTime = DateTime.Now,
                                         AnnualId = Annual.Id
                                     };
            var aircraft = Aircrafts.FirstOrDefault();
            if (aircraft != null)
            {
                NonFhaMaintainCost.AircraftId = aircraft.AircraftId;
                NonFhaMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                NonFhaMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
            }
            var supplier = Suppliers.FirstOrDefault();
            if (supplier != null)
                NonFhaMaintainCost.SupplierId = supplier.Id;
            var invoice = EngineMaintainInvoices.FirstOrDefault();
            if (invoice != null)
            {
                NonFhaMaintainCost.MaintainInvoiceId = invoice.EngineMaintainInvoiceId;
                NonFhaMaintainCost.AcutalInMaintainTime = invoice.InMaintainTime;
                NonFhaMaintainCost.AcutalOutMaintainTime = invoice.OutMaintainTime;
                NonFhaMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                NonFhaMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
            NonFhaMaintainCosts.AddNew(NonFhaMaintainCost);
        }

        /// <summary>
        ///     判断新增非FHA.超保修命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAdd(object sender)
        {
            return true;
        }

        #endregion

        #region 删除非FHA.超保修命令

        public DelegateCommand<object> DeleteCommand { get; set; }

        /// <summary>
        ///     执行删除非FHA.超保修命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelete(object sender)
        {
            if (NonFhaMaintainCost == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                NonFhaMaintainCosts.Remove(NonFhaMaintainCost);
                                                NonFhaMaintainCost = NonFhaMaintainCosts.FirstOrDefault();
                                            });
        }

        /// <summary>
        ///     判断删除非FHA.超保修命令是否可用。
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
            if (!NonFhaMaintainCosts.AutoLoad)
                NonFhaMaintainCosts.AutoLoad = true;
            EngineMaintainInvoices.Load(true);
            Aircrafts.Load(true);
            AircraftTypes.Load(true);
            ActionCategories.Load(true);
            Suppliers.Load(true);
        }

        #endregion

        public void SelectedChanged(object sender)
        {
            if (sender is AircraftDTO)
            {
                var aircraft = sender as AircraftDTO;
                if (NonFhaMaintainCost.AircraftId != aircraft.AircraftId)
                {
                    NonFhaMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                    NonFhaMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
                }
            }
            else if (sender is EngineMaintainInvoiceDTO)
            {
                var invoice = sender as EngineMaintainInvoiceDTO;
                if (NonFhaMaintainCost.MaintainInvoiceId != invoice.EngineMaintainInvoiceId)
                {
                    NonFhaMaintainCost.AcutalInMaintainTime = invoice.InMaintainTime;
                    NonFhaMaintainCost.AcutalOutMaintainTime = invoice.OutMaintainTime;
                    NonFhaMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                    NonFhaMaintainCost.AcutalAmount = invoice.PaidAmount;
                }
            }
        }

        #region 单元格编辑事件
        public DelegateCommand<object> CellEditEndCommand { get; set; }

        private void CellEditEnd(object sender)
        {
            var aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == NonFhaMaintainCost.AircraftId);
            if (aircraft != null)
            {
                NonFhaMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                NonFhaMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
            }
            var invoice = EngineMaintainInvoices.FirstOrDefault(p => p.EngineMaintainInvoiceId == NonFhaMaintainCost.MaintainInvoiceId);
            if (invoice != null)
            {
                NonFhaMaintainCost.AcutalInMaintainTime = invoice.InMaintainTime;
                NonFhaMaintainCost.AcutalOutMaintainTime = invoice.OutMaintainTime;
                NonFhaMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                NonFhaMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
            NonFhaMaintainCost.FeeLittleSum = NonFhaMaintainCost.NonFhaFee +
                                                    NonFhaMaintainCost.PartFee +
                                                    NonFhaMaintainCost.ChangeLlpFee;
            NonFhaMaintainCost.FeeTotalSum = NonFhaMaintainCost.FeeLittleSum * NonFhaMaintainCost.Rate;
        }
        #endregion
    }
}

