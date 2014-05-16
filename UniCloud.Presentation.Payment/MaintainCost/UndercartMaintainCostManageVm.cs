﻿#region Version Info
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
    [Export(typeof(UndercartMaintainCostManageVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class UndercartMaintainCostManageVm : EditViewModelBase
    {
        private readonly IPaymentService _service;
        private readonly PaymentData _context;
        private readonly IFleetPlanService _fleetPlanService;
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
            get { return Enum.GetValues(typeof(MaintainCostType)).Cast<object>().ToDictionary(value => (int)value, value => (MaintainCostType)value); }
        }

        public Dictionary<int, UndercartPart> UndercartParts
        {
            get { return Enum.GetValues(typeof(UndercartPart)).Cast<object>().ToDictionary(value => (int)value, value => (UndercartPart)value); }
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
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            DeleteCommand = new DelegateCommand<object>(OnDelete, CanDelete);
            UndercartMaintainInvoices = new QueryableDataServiceCollectionView<UndercartMaintainInvoiceDTO>(_context, _context.UndercartMaintainInvoices);
            UndercartMaintainCosts = _service.CreateCollection(_context.UndercartMaintainCosts);
            UndercartMaintainCosts.PageSize = 20;
            _annualFilter = new FilterDescriptor("AnnualId", FilterOperator.IsEqualTo, Guid.Empty);
            UndercartMaintainCosts.FilterDescriptors.Add(_annualFilter);
            UndercartMaintainCosts.LoadedData += (sender, e) =>
            {
                if (UndercartMaintainCost == null)
                    UndercartMaintainCost = UndercartMaintainCosts.FirstOrDefault();
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
            UndercartMaintainCost = new UndercartMaintainCostDTO
                                     {
                                         Id = RandomHelper.Next(),
                                         InMaintainTime = DateTime.Now,
                                         OutMaintainTime = DateTime.Now,
                                         AnnualId = Annual.Id
                                     };
            UndercartMaintainCost.TotalDays =
                (UndercartMaintainCost.OutMaintainTime.Date - UndercartMaintainCost.InMaintainTime.Date).Days + 1;
            var aircraft = Aircrafts.FirstOrDefault();
            if (aircraft != null)
            {
                UndercartMaintainCost.AircraftId = aircraft.AircraftId;
                UndercartMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                UndercartMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
            }
            var invoice = UndercartMaintainInvoices.FirstOrDefault();
            if (invoice != null)
            {
                UndercartMaintainCost.MaintainInvoiceId = invoice.UndercartMaintainInvoiceId;
                UndercartMaintainCost.AcutalInMaintainTime = invoice.InMaintainTime;
                UndercartMaintainCost.AcutalOutMaintainTime = invoice.OutMaintainTime;
                UndercartMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                UndercartMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
            UndercartMaintainCosts.AddNew(UndercartMaintainCost);
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
            if (UndercartMaintainCost == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                UndercartMaintainCosts.Remove(UndercartMaintainCost);
                                                UndercartMaintainCost = UndercartMaintainCosts.FirstOrDefault();
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
                UndercartMaintainCost.ActionCategoryId = aircraft.ImportCategoryId;
                UndercartMaintainCost.AircraftTypeId = aircraft.AircraftTypeId;
            }
            else if (sender is UndercartMaintainInvoiceDTO)
            {
                var invoice = sender as UndercartMaintainInvoiceDTO;
                UndercartMaintainCost.AcutalInMaintainTime = invoice.InMaintainTime;
                UndercartMaintainCost.AcutalOutMaintainTime = invoice.OutMaintainTime;
                UndercartMaintainCost.AcutalTotalDays = (invoice.OutMaintainTime.Date - invoice.InMaintainTime.Date).Days + 1;
                UndercartMaintainCost.AcutalBudgetAmount = invoice.InvoiceValue;
                UndercartMaintainCost.AcutalAmount = invoice.PaidAmount;
            }
        }
    }
}

