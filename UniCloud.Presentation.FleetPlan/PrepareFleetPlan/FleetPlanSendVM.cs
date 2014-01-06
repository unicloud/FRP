#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/25 13:53:17
// 文件名：FleetPlanSendVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(FleetPlanSendVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetPlanSendVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        [Import]
        public DocumentViewer DocumentView;
        private DocumentDTO _document = new DocumentDTO();
        private FilterDescriptor _annualDescriptor;

        [ImportingConstructor]
        public FleetPlanSendVM(IRegionManager regionManager, IFleetPlanService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
            InitializeVM();
            InitializerCommand();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            CurAnnuals = _service.CreateCollection(_context.Annuals.Expand(p => p.Plans), o => o.Plans);
            _annualDescriptor = new FilterDescriptor("IsOpen", FilterOperator.IsEqualTo, true);
            CurAnnuals.FilterDescriptors.Add(_annualDescriptor);
            CurAnnuals.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (CurAnnuals.Count != 0)
                {
                    _plans.Clear();
                    Plans = e.Entities.Cast<AnnualDTO>().First().Plans;
                    _curPlan.Clear();
                    CurPlan.Add(e.Entities.Cast<AnnualDTO>().First().Plans.OrderBy(p => p.VersionNumber).FirstOrDefault());
                }
                RefreshCommandState();
            };
            _service.RegisterCollectionView(CurAnnuals);//注册查询集合
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            AttachCommand = new DelegateCommand<object>(OnAttach, CanAttach);
            SendCommand = new DelegateCommand<object>(OnSend, CanSend);
        }

        #endregion

        #region 数据

        #region 公共属性

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
            CurAnnuals.AutoLoad = true;
        }

        #region 业务

        #region 当前计划年度

        /// <summary>
        ///     当前计划年度
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> CurAnnuals { get; set; }

        #endregion

        #region 当前年度运力增减计划集合

        private ObservableCollection<PlanDTO> _plans=new ObservableCollection<PlanDTO>();

        /// <summary>
        /// 当前年度运力增减计划集合
        /// </summary>
        public ObservableCollection<PlanDTO> Plans
        {
            get { return this._plans; }
            private set
            {
                if (this._plans != value)
                {
                    _plans = value;
                    this.RaisePropertyChanged(() => this.Plans);
                }
            }
        }
        #endregion

        #region 当前运力增减计划

        private ObservableItemCollection<PlanDTO> _curPlan = new ObservableItemCollection<PlanDTO>();

        /// <summary>
        ///     当前运力增减计划
        /// </summary>
        public ObservableItemCollection<PlanDTO> CurPlan
        {
            get { return _curPlan; }
            private set
            {
                if (_curPlan != value)
                {
                    _curPlan = value;
                    RaisePropertyChanged(() => CurPlan);
                }
            }
        }

        #endregion

        #region 选择的计划

        private PlanDTO _selPlan;

        /// <summary>
        ///     选择的计划
        /// </summary>
        public PlanDTO SelPlan
        {
            get { return _selPlan; }
            private set
            {
                if (_selPlan != value)
                {
                    _selPlan = value;
                    RaisePropertyChanged(() => SelPlan);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
            AttachCommand.RaiseCanExecuteChanged();
            SendCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 添加计划文档

        /// <summary>
        ///     添加计划文档
        /// </summary>
        public DelegateCommand<object> AttachCommand { get; private set; }

        private void OnAttach(object obj)
        {
        }

        private bool CanAttach(object obj)
        {
            return true;
        }

        #endregion

        #region 报送计划

        /// <summary>
        ///     报送计划
        /// </summary>
        public DelegateCommand<object> SendCommand { get; private set; }

        private void OnSend(object obj)
        {
        }

        private bool CanSend(object obj)
        {
            return true;
        }

        #endregion

        #region 查看附件

        protected override void OnViewAttach(object sender)
        {
            if (SelPlan == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(true, _selPlan.DocumentId, null);
            DocumentView.ShowDialog();
        }

        #endregion

        #endregion
    }
}