#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/15 16:58:43
// 文件名：UndercartMaintainCostManage
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/15 16:58:43
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
    [Export(typeof (UndercartMaintainCostManageVm))]
    public class UndercartMaintainCostManageVm : EditViewModelBase
    {
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
        private readonly IPaymentService _service;
        private FilterDescriptor _annualFilter;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public UndercartMaintainCostManageVm(IPaymentService service, IFleetPlanService fleetPlanService)
            : base(service)
        {
            _fleetPlanService = fleetPlanService;
            _service = service;
            _context = _service.Context;
            InitialVm(); //初始化起落架维修成本
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
                    UndercartMaintainCosts.Load(true);
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

        public QueryableDataServiceCollectionView<UndercartMaintainInvoiceDTO> UndercartMaintainInvoices { get; set; }

        #endregion

        public Dictionary<int, MaintainCostType> MaintainCosts
        {
            get
            {
                return Enum.GetValues(typeof (MaintainCostType))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (MaintainCostType) value);
            }
        }

        public Dictionary<int, UndercartPart> UndercartParts
        {
            get
            {
                return Enum.GetValues(typeof (UndercartPart))
                    .Cast<object>()
                    .ToDictionary(value => (int) value, value => (UndercartPart) value);
            }
        }

        #region 加载起落架维修成本

        private UndercartMaintainCostDTO _undercartMaintainCost;

        /// <summary>
        ///     选择起落架维修成本。
        /// </summary>
        public UndercartMaintainCostDTO UndercartMaintainCost
        {
            get { return _undercartMaintainCost; }
            set
            {
                if (_undercartMaintainCost != value)
                {
                    _undercartMaintainCost = value;
                    RaisePropertyChanged(() => UndercartMaintainCost);
                }
            }
        }

        /// <summary>
        ///     获取所有起落架维修成本信息。
        /// </summary>
        public QueryableDataServiceCollectionView<UndercartMaintainCostDTO> UndercartMaintainCosts { get; set; }

        /// <summary>
        ///     初始化起落架维修成本信息。
        /// </summary>
        private void InitialVm()
        {
            CellEditEndCommand = new DelegateCommand<object>(CellEditEnd);
            UndercartMaintainInvoices = new QueryableDataServiceCollectionView<UndercartMaintainInvoiceDTO>(_context,
                _context.UndercartMaintainInvoices);
            UndercartMaintainCosts = _service.CreateCollection(_context.UndercartMaintainCosts);
            UndercartMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("Year", FilterOperator.IsEqualTo, 0);
            UndercartMaintainCosts.FilterDescriptors.Add(_annualFilter);
            UndercartMaintainCosts.LoadedData += (sender, e) =>
            {
                if (UndercartMaintainCost == null)
                    UndercartMaintainCost = UndercartMaintainCosts.FirstOrDefault();
                RefreshCommandState();
            };
            _service.RegisterCollectionView(UndercartMaintainCosts);

            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_fleetPlanService.Context,
                _fleetPlanService.Context.Aircrafts);
            AircraftTypes = new QueryableDataServiceCollectionView<AircraftTypeDTO>(_fleetPlanService.Context,
                _fleetPlanService.Context.AircraftTypes);
            ActionCategories = new QueryableDataServiceCollectionView<ActionCategoryDTO>(_fleetPlanService.Context,
                _fleetPlanService.Context.ActionCategories);
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
            if (!UndercartMaintainCosts.AutoLoad)
                UndercartMaintainCosts.AutoLoad = true;
            UndercartMaintainInvoices.Load(true);
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
                if (UndercartMaintainCost.AircraftId != aircraft.AircraftId)
                {
                    UndercartMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                    UndercartMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
                }
            }
            else if (sender is UndercartMaintainInvoiceDTO)
            {
                var invoice = sender as UndercartMaintainInvoiceDTO;
                if (UndercartMaintainCost.MaintainInvoiceId != invoice.UndercartMaintainInvoiceId)
                {
                    UndercartMaintainCost.AcutalInMaintainTime = invoice.InMaintainTime;
                    UndercartMaintainCost.AcutalOutMaintainTime = invoice.OutMaintainTime;
                    UndercartMaintainCost.AcutalTotalDays =
                        (invoice.OutMaintainTime.Date - invoice.InMaintainTime.Date).Days + 1;
                    UndercartMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                    UndercartMaintainCost.AcutalAmount = invoice.InvoiceValue;
                }
            }
        }

        #region 单元格编辑事件

        public DelegateCommand<object> CellEditEndCommand { get; set; }

        private void CellEditEnd(object sender)
        {
            var aircraft = Aircrafts.FirstOrDefault(p => p.AircraftId == UndercartMaintainCost.AircraftId);
            if (aircraft != null)
            {
                UndercartMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                UndercartMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
            }
            UndercartMaintainCost.TotalDays =
                (UndercartMaintainCost.OutMaintainTime.Date - UndercartMaintainCost.InMaintainTime.Date).Days + 1;
            UndercartMaintainCost.AcutalTotalDays =
                (UndercartMaintainCost.AcutalOutMaintainTime.Date - UndercartMaintainCost.AcutalInMaintainTime.Date)
                    .Days + 1;
        }

        #endregion
    }
}