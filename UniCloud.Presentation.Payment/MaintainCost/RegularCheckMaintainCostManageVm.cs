#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 14:21:24
// 文件名：RegularCheckMaintainCostManageVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 14:21:24
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
    [Export(typeof(RegularCheckMaintainCostManageVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RegularCheckMaintainCostManageVm : EditViewModelBase
    {
        private readonly IPaymentService _service;
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
        private FilterDescriptor _annualFilter;
        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public RegularCheckMaintainCostManageVm(IPaymentService service, IFleetPlanService fleetPlanService)
            : base(service)
        {
            _fleetPlanService = fleetPlanService;
            _service = service;
            _context = _service.Context;
            InitialVm(); //初始化定检维修成本

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
                    RegularCheckMaintainCosts.Load(true);
                }
                RaisePropertyChanged(() => Annual);
            }
        }
        #endregion

        #region 飞机
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }
        public QueryableDataServiceCollectionView<ActionCategoryDTO> ActionCategories { get; set; }
        #endregion

        #region 发票
        public QueryableDataServiceCollectionView<AirframeMaintainInvoiceDTO> AirframeMaintainInvoices { get; set; }
        #endregion

        public Dictionary<int, RegularCheckType> RegularCheckTypes
        {
            get { return Enum.GetValues(typeof(RegularCheckType)).Cast<object>().ToDictionary(value => (int)value, value => (RegularCheckType)value); }
        }

        #region 加载定检维修成本

        private RegularCheckMaintainCostDTO _regularCheckMaintainCost;

        /// <summary>
        ///     选择定检维修成本。
        /// </summary>
        public RegularCheckMaintainCostDTO RegularCheckMaintainCost
        {
            get { return _regularCheckMaintainCost; }
            set
            {
                if (_regularCheckMaintainCost != value)
                {
                    _regularCheckMaintainCost = value;
                    RaisePropertyChanged(() => RegularCheckMaintainCost);
                }
            }
        }

        /// <summary>
        ///     获取所有定检维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<RegularCheckMaintainCostDTO> RegularCheckMaintainCosts { get; set; }

        /// <summary>
        ///     初始化定检维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            CellEditEndCommand = new DelegateCommand<object>(CellEditEnd);
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            DeleteCommand = new DelegateCommand<object>(OnDelete, CanDelete);
            AirframeMaintainInvoices = new QueryableDataServiceCollectionView<AirframeMaintainInvoiceDTO>(_context, _context.AirframeMaintainInvoices);
            RegularCheckMaintainCosts = _service.CreateCollection(_context.RegularCheckMaintainCosts);
            RegularCheckMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("AnnualId", FilterOperator.IsEqualTo, Guid.Empty);
            RegularCheckMaintainCosts.FilterDescriptors.Add(_annualFilter);
            RegularCheckMaintainCosts.LoadedData += (sender, e) =>
            {
                if (RegularCheckMaintainCost == null)
                    RegularCheckMaintainCost = RegularCheckMaintainCosts.FirstOrDefault();
                RefreshCommandState();
            };

            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_fleetPlanService.Context, _fleetPlanService.Context.Aircrafts);
            AircraftTypes = new QueryableDataServiceCollectionView<AircraftTypeDTO>(_fleetPlanService.Context, _fleetPlanService.Context.AircraftTypes);
            ActionCategories = new QueryableDataServiceCollectionView<ActionCategoryDTO>(_fleetPlanService.Context, _fleetPlanService.Context.ActionCategories);
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
            RegularCheckMaintainCost = new RegularCheckMaintainCostDTO
                                     {
                                         Id = RandomHelper.Next(),
                                         InMaintainTime = DateTime.Now,
                                         OutMaintainTime = DateTime.Now,
                                         AnnualId = Annual.Id
                                     };
            RegularCheckMaintainCost.TotalDays =
                (RegularCheckMaintainCost.OutMaintainTime.Date - RegularCheckMaintainCost.InMaintainTime.Date).Days + 1;
            var aircraft = Aircrafts.FirstOrDefault();
            if (aircraft != null)
            {
                RegularCheckMaintainCost.AircraftId = aircraft.AircraftId;
                RegularCheckMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                RegularCheckMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
            }
            var invoice = AirframeMaintainInvoices.FirstOrDefault();
            if (invoice != null)
            {
                RegularCheckMaintainCost.MaintainInvoiceId = invoice.AirframeMaintainInvoiceId;
                RegularCheckMaintainCost.AcutalInMaintainTime = invoice.InMaintainTime;
                RegularCheckMaintainCost.AcutalOutMaintainTime = invoice.OutMaintainTime;
                RegularCheckMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                RegularCheckMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
            RegularCheckMaintainCosts.AddNew(RegularCheckMaintainCost);
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
            if (RegularCheckMaintainCost == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                RegularCheckMaintainCosts.Remove(RegularCheckMaintainCost);
                                                RegularCheckMaintainCost = RegularCheckMaintainCosts.FirstOrDefault();
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
            if (!RegularCheckMaintainCosts.AutoLoad)
                RegularCheckMaintainCosts.AutoLoad = true;
            AirframeMaintainInvoices.Load(true);
            Aircrafts.Load(true);
            AircraftTypes.Load(true);
            ActionCategories.Load(true);
        }

        #endregion

        public void SelectedChanged(object sender)
        {
            if (sender is AircraftDTO)
            {
                var aircraft = sender as AircraftDTO;
                if (RegularCheckMaintainCost.AircraftId != aircraft.AircraftId)
                {
                    RegularCheckMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                    RegularCheckMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
                }
            }
            else if (sender is AirframeMaintainInvoiceDTO)
            {
                var invoice = sender as AirframeMaintainInvoiceDTO;
                if (RegularCheckMaintainCost.MaintainInvoiceId != invoice.AirframeMaintainInvoiceId)
                {
                    RegularCheckMaintainCost.AcutalInMaintainTime = invoice.InMaintainTime;
                    RegularCheckMaintainCost.AcutalOutMaintainTime = invoice.OutMaintainTime;
                    RegularCheckMaintainCost.AcutalTotalDays =
                        (invoice.OutMaintainTime.Date - invoice.InMaintainTime.Date).Days + 1;
                    RegularCheckMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                    RegularCheckMaintainCost.AcutalAmount = invoice.PaidAmount;
                }
            }
        }

        #region 单元格编辑事件
        public DelegateCommand<object> CellEditEndCommand { get; set; }

        private void CellEditEnd(object sender)
        {
            RegularCheckMaintainCost.TotalDays = (RegularCheckMaintainCost.OutMaintainTime.Date - RegularCheckMaintainCost.InMaintainTime.Date).Days + 1;
            RegularCheckMaintainCost.AcutalTotalDays = (RegularCheckMaintainCost.AcutalOutMaintainTime.Date - RegularCheckMaintainCost.AcutalInMaintainTime.Date).Days + 1;
        }
        #endregion
    }
}
