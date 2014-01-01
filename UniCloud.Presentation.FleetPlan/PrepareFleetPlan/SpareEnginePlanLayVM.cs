#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/25 13:54:26
// 文件名：SpareEnginePlanLayVM
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
using Telerik.Windows.Controls;
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
    [Export(typeof(SpareEnginePlanLayVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SpareEnginePlanLayVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private FleetPlanData _context;
        private DocumentDTO _document = new DocumentDTO();

        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public SpareEnginePlanLayVM(IRegionManager regionManager)
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
            EnginePlans = Service.CreateCollection(_context.EnginePlans, o => o.EnginePlanHistories);
            Service.RegisterCollectionView(EnginePlans);

        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
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

        }

        #region 业务

        #region 备发计划集合

        /// <summary>
        ///     备发计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePlanDTO> EnginePlans { get; set; }

        #endregion

        #region 选择的备发计划

        private EnginePlanDTO _selEnginePlan;

        /// <summary>
        /// 选择的备发计划
        /// </summary>
        public EnginePlanDTO SelEnginePlan
        {
            get { return this._selEnginePlan; }
            private set
            {
                if (this._selEnginePlan != value)
                {
                    _selEnginePlan = value;
                    this.RaisePropertyChanged(() => this.SelEnginePlan);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的备发计划明细

        private EnginePlanHistoryDTO _selEnginePlanHistory;

        /// <summary>
        /// 选择的备发计划明细
        /// </summary>
        public EnginePlanHistoryDTO SelEnginePlanHistory
        {
            get { return this._selEnginePlanHistory; }
            private set
            {
                if (this._selEnginePlanHistory != value)
                {
                    _selEnginePlanHistory = value;
                    this.RaisePropertyChanged(() => this.SelEnginePlanHistory);

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
            AddAttachCommand.RaiseCanExecuteChanged();
            NewCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            CommitCommand.RaiseCanExecuteChanged();
            CheckCommand.RaiseCanExecuteChanged();
            AddEntityCommand.RaiseCanExecuteChanged();
            RemoveEntityCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建备发计划

        /// <summary>
        ///     创建新备发计划
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {

        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除备发计划

        /// <summary>
        ///     删除备发计划
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {

        }

        private bool CanRemove(object obj)
        {
            return _selEnginePlan != null;
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {

        }

        private bool CanCommit(object obj)
        {
            return true;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> CheckCommand { get; private set; }

        private void OnCheck(object obj)
        {

        }

        private bool CanCheck(object obj)
        {
            return true;
        }

        #endregion

        #region 增加备发计划明细

        /// <summary>
        ///     增加备发计划明细
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {

        }

        private bool CanAddEntity(object obj)
        {
            return _selEnginePlan != null;
        }

        #endregion

        #region 移除规划行

        /// <summary>
        ///     移除规划行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selEnginePlanHistory != null)
            {
                SelEnginePlan.EnginePlanHistories.Remove(_selEnginePlanHistory);
            }
        }

        private bool CanRemoveEntity(object obj)
        {
            return _selEnginePlanHistory != null;
        }

        #endregion

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            DocumentView.ViewModel.InitData(false, _selEnginePlan.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        protected override bool CanAddAttach(object obj)
        {
            return _selEnginePlan != null;
        }
        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                _document = DocumentView.Tag as DocumentDTO;
                SelEnginePlan.DocumentId = _document.DocumentId;
                SelEnginePlan.DocName = _document.Name;
            }
        }
        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            if (SelEnginePlan == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(true, _selEnginePlan.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();

        }
        #endregion

        #endregion
    }
}
