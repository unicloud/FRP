#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/5/8 13:59:12
// 文件名：ManageEngineMaintainPlanVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/5/8 13:59:12
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
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
    [Export(typeof(ManageEngineMaintainPlanVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageEngineMaintainPlanVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private readonly IFleetPlanService _fleetPlanService;
        private FilterDescriptor _annualFilterDescriptor;
        private FilterDescriptor _maintainPlanTypeFilterDescriptor;
        private int _maintainPlanType;

        [ImportingConstructor]
        public ManageEngineMaintainPlanVm(IRegionManager regionManager, IPartService service,
            IFleetPlanService fleetPlanService)
            : base(service)
        {
            _regionManager = regionManager;
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
            EngineMaintainPlans = _service.CreateCollection(_context.EngineMaintainPlans,
                o => o.EngineMaintainPlanDetails);
            _annualFilterDescriptor = new FilterDescriptor("AnnualId", FilterOperator.IsEqualTo, Guid.Empty);
            _maintainPlanTypeFilterDescriptor = new FilterDescriptor("MaintainPlanType", FilterOperator.IsEqualTo, 0);
            EngineMaintainPlans.FilterDescriptors.Add(_annualFilterDescriptor);
            EngineMaintainPlans.FilterDescriptors.Add(_maintainPlanTypeFilterDescriptor);
            EngineMaintainPlans.LoadedData += (o, e) =>
                                              {
                                                  EngineMaintainPlan = EngineMaintainPlans.FirstOrDefault();
                                              };
            _service.RegisterCollectionView(EngineMaintainPlans);
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

        #region 美元汇率
        private decimal _dollarRate;
        public decimal DollarRate
        {
            get { return _dollarRate; }
            set
            {
                _dollarRate = value;
                EngineMaintainPlan.DollarRate = _dollarRate;
                EngineMaintainPlan.EngineMaintainPlanDetails.ToList().ForEach(p =>
                                                                              {
                                                                                  p.FeeTotalSum = p.FeeLittleSum * DollarRate;
                                                                                  p.BudgetToalSum = p.FeeTotalSum + p.CustomsTax + p.FreightFee;
                                                                              });
                RaisePropertyChanged(() => DollarRate);
            }
        }
        #endregion

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
                    Title = _maintainPlanType == 0 ? Annual.Year + "年发动机非FHA送修预算表" : Annual.Year + "年发动机超包修预算表";
                    _annualFilterDescriptor.Value = _annual.Id;
                    EngineMaintainPlans.Load(true);
                }
                RaisePropertyChanged(() => Annual);
            }
        }
        #endregion

        #region 发动机送修计划

        /// <summary>
        ///     发动机送修计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineMaintainPlanDTO> EngineMaintainPlans { get; set; }

        /// <summary>
        ///     选中的发动机送修计划
        /// </summary>
        private EngineMaintainPlanDTO _engineMaintainPlan;

        public EngineMaintainPlanDTO EngineMaintainPlan
        {
            get { return _engineMaintainPlan; }
            set
            {
                _engineMaintainPlan = value;
                if (_engineMaintainPlan != null)
                {
                    IsEnable = true;
                    DollarRate = _engineMaintainPlan.DollarRate;
                }
                else
                {
                    IsEnable = false;
                }
                RaisePropertyChanged(() => EngineMaintainPlan);
            }
        }

        #endregion

        #region 发动机送修计划明细

        /// <summary>
        ///     选中的发动机送修计划明细
        /// </summary>
        private EngineMaintainPlanDetailDTO _engineMaintainPlanDetail;

        public EngineMaintainPlanDetailDTO EngineMaintainPlanDetail
        {
            get { return _engineMaintainPlanDetail; }
            set
            {
                if (_engineMaintainPlanDetail != value)
                {
                    _engineMaintainPlanDetail = value;
                    RaisePropertyChanged(() => EngineMaintainPlanDetail);
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
            if (!EngineMaintainPlans.AutoLoad)
                EngineMaintainPlans.AutoLoad = true;
            EngineMaintainPlans.Load(true);
        }

        #endregion

        #endregion

        #region 操作

        #region 创建新发动机送修计划

        /// <summary>
        ///     创建新发动机送修计划
        /// </summary>
        public DelegateCommand<object> AddCommand { get; set; }

        protected void OnAdd(object obj)
        {
            if (Annual == null)
            {
                MessageAlert("请选择一个年度进行创建！");
                return;
            }
            if (EngineMaintainPlan != null)
            {
                MessageAlert("已有当前年度送修计划！");
                return;
            }
            EngineMaintainPlan = new EngineMaintainPlanDTO
                                 {
                                     Id = RandomHelper.Next(),
                                     AnnualId = Annual.Id,
                                     MaintainPlanType = _maintainPlanType
                                 };
            EngineMaintainPlans.AddNew(EngineMaintainPlan);
        }

        protected bool CanAdd(object obj)
        {
            return true;
        }

        #endregion

        #region 删除发动机送修计划

        /// <summary>
        ///     删除发动机送修计划
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; set; }

        protected void OnRemove(object obj)
        {
            if (EngineMaintainPlan == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                EngineMaintainPlans.Remove(EngineMaintainPlan);
                                                EngineMaintainPlan = EngineMaintainPlans.FirstOrDefault();
                                            });
        }

        protected bool CanRemove(object obj)
        {
            return true;
        }

        #endregion

        #region 增加发动机送修计划明细

        /// <summary>
        ///     增加发动机送修计划明细
        /// </summary>
        public DelegateCommand<object> AddDetailCommand { get; set; }

        protected void OnAddDetail(object obj)
        {
            if (EngineMaintainPlan == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }

            EngineMaintainPlanDetail = new EngineMaintainPlanDetailDTO { Id = RandomHelper.Next() };
            EngineMaintainPlan.EngineMaintainPlanDetails.Add(EngineMaintainPlanDetail);
        }

        protected bool CanAddDetail(object obj)
        {
            return true;
        }

        #endregion

        #region 移除发动机送修计划明细

        /// <summary>
        ///     移除发动机送修计划明细
        /// </summary>
        public DelegateCommand<object> RemoveDetailCommand { get; private set; }

        protected void OnRemoveDetail(object obj)
        {
            if (EngineMaintainPlanDetail == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                EngineMaintainPlan.EngineMaintainPlanDetails.Remove(
                                                    EngineMaintainPlanDetail);
                                                EngineMaintainPlanDetail =
                                                    EngineMaintainPlan.EngineMaintainPlanDetails.FirstOrDefault();
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
            EngineMaintainPlanDetail.FeeLittleSum = EngineMaintainPlanDetail.NonFhaFee +
                                                    EngineMaintainPlanDetail.PartFee +
                                                    EngineMaintainPlanDetail.ChangeLlpFee;
            EngineMaintainPlanDetail.FeeTotalSum = EngineMaintainPlanDetail.FeeLittleSum * DollarRate;
            EngineMaintainPlanDetail.BudgetToalSum = EngineMaintainPlanDetail.FeeTotalSum +
                                                     EngineMaintainPlanDetail.CustomsTax +
                                                     EngineMaintainPlanDetail.FreightFee;
        }
        #endregion

        #region RadPaneGroup页面切换

        public void PaneSelectedChange(int maintainPlanType)
        {
            _maintainPlanType = maintainPlanType;
            if (Annual != null)
            {
                Title = _maintainPlanType == 0 ? Annual.Year + "年发动机非FHA送修预算表" : Annual.Year + "年发动机超包修预算表";
                _annualFilterDescriptor.Value = Annual.Id;
            }
            _maintainPlanTypeFilterDescriptor.Value = _maintainPlanType;
            EngineMaintainPlans.Load(true);
        }
        #endregion
        #endregion
    }
}
