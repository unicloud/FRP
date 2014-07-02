#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/9 9:54:25
// 文件名：ManageAircraftMaintainPlanVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/9 9:54:25
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.ManageAnnualMaintainPlan
{
    [Export(typeof (ManageAircraftMaintainPlanVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class ManageAircraftMaintainPlanVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IFleetPlanService _fleetPlanService;
        private readonly IPartService _service;
        private FilterDescriptor _annualFilterDescriptor;

        [ImportingConstructor]
        public ManageAircraftMaintainPlanVm(IPartService service, IFleetPlanService fleetPlanService)
            : base(service)
        {
            _service = service;
            _fleetPlanService = fleetPlanService;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            AddCommand = new DelegateCommand<object>(OnAdd, CanAdd);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddDetailCommand = new DelegateCommand<object>(OnAddDetail, CanAddDetail);
            RemoveDetailCommand = new DelegateCommand<object>(OnRemoveDetail, CanRemoveDetail);
            CellEditEndCommand = new DelegateCommand<object>(CellEditEnd);

            // 创建并注册CollectionView
            Annuals = _fleetPlanService.CreateCollection(_fleetPlanService.Context.Annuals);
            AircraftMaintainPlans = _service.CreateCollection(_context.AircraftMaintainPlans,
                o => o.AircraftMaintainPlanDetails);
            _annualFilterDescriptor = new FilterDescriptor("AnnualId", FilterOperator.IsEqualTo, Guid.Empty);
            AircraftMaintainPlans.FilterDescriptors.Add(_annualFilterDescriptor);
            AircraftMaintainPlans.LoadedData +=
                (o, e) => { AircraftMaintainPlan = AircraftMaintainPlans.FirstOrDefault(); };
            _service.RegisterCollectionView(AircraftMaintainPlans);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 标题

        private string _title;

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        #endregion

        #region 页面是否可用

        private bool _isEnable;

        public bool IsEnable
        {
            get { return _isEnable; }
            set
            {
                _isEnable = value;
                RaisePropertyChanged(() => IsEnable);
            }
        }

        #endregion

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
                    Title = Annual.Year + "年公司机队C检、退租、起落架、整机喷漆、重大改装计划";
                    _annualFilterDescriptor.Value = _annual.Id;
                    AircraftMaintainPlans.Load(true);
                }
                RaisePropertyChanged(() => Annual);
            }
        }

        #endregion

        #region 飞机送修计划

        /// <summary>
        ///     选中的飞机送修计划
        /// </summary>
        private AircraftMaintainPlanDTO _aircraftMaintainPlan;

        /// <summary>
        ///     飞机送修计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftMaintainPlanDTO> AircraftMaintainPlans { get; set; }

        public AircraftMaintainPlanDTO AircraftMaintainPlan
        {
            get { return _aircraftMaintainPlan; }
            set
            {
                _aircraftMaintainPlan = value;
                IsEnable = _aircraftMaintainPlan != null;
                RaisePropertyChanged(() => AircraftMaintainPlan);
            }
        }

        #endregion

        #region 飞机送修计划明细

        /// <summary>
        ///     选中的飞机送修计划明细
        /// </summary>
        private AircraftMaintainPlanDetailDTO _aircraftMaintainPlanDetail;

        public AircraftMaintainPlanDetailDTO AircraftMaintainPlanDetail
        {
            get { return _aircraftMaintainPlanDetail; }
            set
            {
                if (_aircraftMaintainPlanDetail != value)
                {
                    _aircraftMaintainPlanDetail = value;
                    RaisePropertyChanged(() => AircraftMaintainPlanDetail);
                }
            }
        }

        #endregion

        #endregion

        #region 加载数据

        /// <summary>
        ///     加载数据方法
        ///     <remarks>
        ///         导航到此页面时调用。
        ///         可在此处将CollectionView的AutoLoad属性设为True，以实现数据的自动加载。
        ///     </remarks>
        /// </summary>
        public override void LoadData()
        {
            // 将CollectionView的AutoLoad属性设为True
            if (!Annuals.AutoLoad)
                Annuals.AutoLoad = true;
        }

        #endregion

        #endregion

        #region 操作

        #region 创建新飞机送修计划

        /// <summary>
        ///     创建新飞机送修计划
        /// </summary>
        public DelegateCommand<object> AddCommand { get; set; }

        protected void OnAdd(object obj)
        {
            if (Annual == null)
            {
                MessageAlert("请选择一个年度进行创建！");
                return;
            }
            if (AircraftMaintainPlan != null)
            {
                MessageAlert("已有当前年度送修计划！");
                return;
            }
            AircraftMaintainPlan = new AircraftMaintainPlanDTO
            {
                Id = RandomHelper.Next(),
                AnnualId = Annual.Id,
                Note = Title
            };
            AircraftMaintainPlans.AddNew(AircraftMaintainPlan);
        }

        protected bool CanAdd(object obj)
        {
            return true;
        }

        #endregion

        #region 删除飞机送修计划

        /// <summary>
        ///     删除飞机送修计划
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; set; }

        protected void OnRemove(object obj)
        {
            if (AircraftMaintainPlan == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                AircraftMaintainPlans.Remove(AircraftMaintainPlan);
                AircraftMaintainPlan = AircraftMaintainPlans.FirstOrDefault();
            });
        }

        protected bool CanRemove(object obj)
        {
            return true;
        }

        #endregion

        #region 增加飞机送修计划明细

        /// <summary>
        ///     增加飞机送修计划明细
        /// </summary>
        public DelegateCommand<object> AddDetailCommand { get; set; }

        protected void OnAddDetail(object obj)
        {
            if (AircraftMaintainPlan == null)
            {
                MessageAlert("请新建年度送修计划！");
                return;
            }

            AircraftMaintainPlanDetail = new AircraftMaintainPlanDetailDTO
            {
                Id = RandomHelper.Next(),
                InDate = DateTime.Now,
                OutDate = DateTime.Now,
                Cycle = 1
            };
            if (AircraftMaintainPlanDetail.InDate.Month < 7)
            {
                AircraftMaintainPlan.FirstHalfYear += 1;
            }
            else
            {
                AircraftMaintainPlan.SecondHalfYear += 1;
            }
            AircraftMaintainPlan.AircraftMaintainPlanDetails.Add(AircraftMaintainPlanDetail);
        }

        protected bool CanAddDetail(object obj)
        {
            return true;
        }

        #endregion

        #region 移除飞机送修计划明细

        /// <summary>
        ///     移除飞机送修计划明细
        /// </summary>
        public DelegateCommand<object> RemoveDetailCommand { get; private set; }

        protected void OnRemoveDetail(object obj)
        {
            if (AircraftMaintainPlanDetail == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                if (AircraftMaintainPlanDetail.InDate.Month < 7)
                {
                    AircraftMaintainPlan.FirstHalfYear -= 1;
                }
                else
                {
                    AircraftMaintainPlan.SecondHalfYear -= 1;
                }
                AircraftMaintainPlan.AircraftMaintainPlanDetails.Remove(
                    AircraftMaintainPlanDetail);
                AircraftMaintainPlanDetail =
                    AircraftMaintainPlan.AircraftMaintainPlanDetails.FirstOrDefault();
            });
        }

        protected bool CanRemoveDetail(object obj)
        {
            return true;
        }

        #endregion

        #region 单元格编辑事件

        public DelegateCommand<object> CellEditEndCommand { get; set; }

        private void CellEditEnd(object sender)
        {
            if (sender is RadGridView)
            {
                var girdView = sender as RadGridView;
                var currentColumn = girdView.CurrentColumn;
                if (currentColumn.UniqueName.Equals("InDate", StringComparison.OrdinalIgnoreCase))
                {
                    AircraftMaintainPlan.FirstHalfYear = 0;
                    AircraftMaintainPlan.SecondHalfYear = 0;
                    AircraftMaintainPlan.AircraftMaintainPlanDetails.ToList().ForEach(p =>
                    {
                        if (p.InDate.Year < Annual.Year || p.InDate.Month < 7)
                        {
                            AircraftMaintainPlan.FirstHalfYear++;
                        }
                        else
                        {
                            AircraftMaintainPlan.SecondHalfYear++;
                        }
                    });
                }
            }
            AircraftMaintainPlanDetail.Cycle =
                (AircraftMaintainPlanDetail.OutDate.Date - AircraftMaintainPlanDetail.InDate.Date).Days + 1;
        }

        #endregion

        #endregion
    }
}