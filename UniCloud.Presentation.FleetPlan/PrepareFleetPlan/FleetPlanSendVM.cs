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
using System.ComponentModel.Composition;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
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

        private readonly IRegionManager _regionManager;
        private FleetPlanData _context;
        private DocumentDTO _document = new DocumentDTO();
        private FilterDescriptor _planDescriptor;

        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public FleetPlanSendVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
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
            ViewPlans = Service.CreateCollection(_context.Plans);
            _planDescriptor = new FilterDescriptor("Year", FilterOperator.IsEqualTo, DateTime.Now.Year);
            ViewPlans.FilterDescriptors.Add(_planDescriptor);
            Service.RegisterCollectionView(ViewPlans);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            AttachCommand = new DelegateCommand<object>(OnAttach, CanAttach);
            SendCommand = new DelegateCommand<object>(OnSend, CanSend);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _context = new FleetPlanData(AgentHelper.FleetPlanServiceUri);
            return new FleetPlanService(_context);
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
            ViewPlans.Load(true);

            //获取当前计划
            var plan = ViewPlans.FirstOrDefault(p => p.IsCurrentVersion == true);
            _curPlan.Clear();
            CurPlan.Add(plan);
        }

        #region 业务

        #region 当前年度运力增减计划集合

        /// <summary>
        ///     当前年度运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> ViewPlans { get; set; }

        #endregion

        #region 当前运力增减计划
        private ObservableItemCollection<PlanDTO> _curPlan=new ObservableItemCollection<PlanDTO>();

        /// <summary>
        ///     当前运力增减计划
        /// </summary>
        public ObservableItemCollection<PlanDTO> CurPlan
        {
            get { return this._curPlan; }
            private set
            {
                if (this._curPlan != value)
                {
                    _curPlan = value;
                    this.RaisePropertyChanged(() => this.CurPlan);
                }
            }
        }

        #endregion

        #region 选择的计划

        private PlanDTO _selPlan;

        /// <summary>
        /// 选择的计划
        /// </summary>
        public PlanDTO SelPlan
        {
            get { return this._selPlan; }
            private set
            {
                if (this._selPlan != value)
                {
                    _selPlan = value;
                    this.RaisePropertyChanged(() => this.SelPlan);
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
