#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.FleetPlan.PrepareFleetPlan;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.Requests
{
    [Export(typeof(RequestVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RequestVM : EditViewModelBase
    {

        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private FilterDescriptor _annualDescriptor;
        private FilterDescriptor _planDescriptor;
        private FilterDescriptor _planHistoryDescriptor;

        [ImportingConstructor]
        public RequestVM(IRegionManager regionManager, IFleetPlanService service)
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
            Requests = _service.CreateCollection(_context.Requests, o => o.ApprovalHistories, o => o.RelatedDocs);
            _service.RegisterCollectionView(Requests);

            CurAnnuals = new QueryableDataServiceCollectionView<AnnualDTO>(_context, _context.Annuals);
            _annualDescriptor = new FilterDescriptor("IsOpen", FilterOperator.IsEqualTo, true);
            CurAnnuals.FilterDescriptors.Add(_annualDescriptor);
            CurAnnuals.LoadedData += (sender, e) =>
            {
                if (CurAnnuals.Count != 0)
                {
                    _planDescriptor.Value = CurAnnuals.First().Year;
                    if (!Plans.AutoLoad)
                        Plans.AutoLoad = true;
                    else
                        Plans.Load(true);
                    RefreshCommandState();
                }
            };

            Plans = new QueryableDataServiceCollectionView<PlanDTO>(_context, _context.Plans);
            _planDescriptor = new FilterDescriptor("Year", FilterOperator.IsEqualTo, -1);
            var sort = new SortDescriptor { Member = "VersionNumber", SortDirection = ListSortDirection.Ascending };
            Plans.SortDescriptors.Add(sort);
            Plans.FilterDescriptors.Add(_planDescriptor);
            Plans.LoadedData += (sender, e) =>
            {
                var curPlan = Plans.OrderBy(p => p.VersionNumber).LastOrDefault();
                if (curPlan != null)
                {
                    _planHistoryDescriptor.Value = curPlan.Id;
                    if (!CurPlanHistories.AutoLoad)
                        CurPlanHistories.AutoLoad = true;
                    else
                        CurPlanHistories.Load(true);
                }
                RefreshCommandState();
            };

            CurPlanHistories = new QueryableDataServiceCollectionView<PlanHistoryDTO>(_context, _context.PlanHistories);
            _planHistoryDescriptor = new FilterDescriptor("PlanId", FilterOperator.IsEqualTo, Guid.Empty);
            var group = new GroupDescriptor { Member = "CanRequest", SortDirection = ListSortDirection.Descending };
            CurPlanHistories.GroupDescriptors.Add(group);
            CurPlanHistories.FilterDescriptors.Add(_planHistoryDescriptor);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveDocCommand = new DelegateCommand<object>(OnRemoveDoc, CanRemoveDoc);
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            CheckCommand = new DelegateCommand<object>(OnCheck, CanCheck);
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
        }

        #endregion

        #region 数据

        #region 公共属性

        public Dictionary<int, CanRequest> CanRequests
        {
            get
            {
                return Enum.GetValues(typeof(CanRequest)).Cast<object>().ToDictionary(value => (int)value, value => (CanRequest)value);
            }
        }

        #region 当前计划年度

        /// <summary>
        ///     当前计划年度
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> CurAnnuals { get; set; }

        #endregion

        #region 当前年度运力增减计划集合

        /// <summary>
        /// 当前年度运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> Plans { get; set; }

        #endregion

        #region 当前运力增减计划

        private ObservableCollection<PlanDTO> _curPlan = new ObservableCollection<PlanDTO>();

        /// <summary>
        ///     当前运力增减计划
        /// </summary>
        public ObservableCollection<PlanDTO> CurPlan
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

        /// <summary>
        /// 当前年度运力增减计划明细集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanHistoryDTO> CurPlanHistories { get; set; }


        private PlanHistoryDTO _selPlanHistory;

        /// <summary>
        /// 选择的计划明细
        /// </summary>
        public PlanHistoryDTO SelPlanHistory
        {
            get { return this._selPlanHistory; }
            private set
            {
                if (this._selPlanHistory != value)
                {
                    _selPlanHistory = value;
                    this.RaisePropertyChanged(() => this.SelPlanHistory);
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
            if (!Requests.AutoLoad)
                Requests.AutoLoad = true;
            else
                Requests.Load(true);

            CurAnnuals.Load(true);
        }

        #region 业务

        #region 所有申请集合

        /// <summary>
        ///     所有申请集合
        /// </summary>
        public QueryableDataServiceCollectionView<RequestDTO> Requests { get; set; }


        private RequestDTO _selRequest;

        /// <summary>
        /// 选择的申请
        /// </summary>
        public RequestDTO SelRequest
        {
            get { return this._selRequest; }
            private set
            {
                if (this._selRequest != value)
                {
                    _selRequest = value;
                    this.RaisePropertyChanged(() => this.SelRequest);
                }
            }
        }


        private ApprovalHistoryDTO _selApprovalHistory;

        /// <summary>
        /// 选择的申请明细
        /// </summary>
        public ApprovalHistoryDTO SelApprovalHistory
        {
            get { return this._selApprovalHistory; }
            private set
            {
                if (this._selApprovalHistory != value)
                {
                    _selApprovalHistory = value;
                    this.RaisePropertyChanged(() => this.SelApprovalHistory);
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
            RemoveDocCommand.RaiseCanExecuteChanged();
            CommitCommand.RaiseCanExecuteChanged();
            CheckCommand.RaiseCanExecuteChanged();
        }

        #endregion

        protected override bool CanAddAttach(object obj)
        {
            return _selRequest != null;
        }

        #region 添加附件成功后执行的操作

        /// <summary>
        ///     子窗口关闭后执行的操作
        /// </summary>
        /// <param name="doc">添加的附件</param>
        /// <param name="sender">添加附件命令的参数</param>
        protected override void WindowClosed(DocumentDTO doc, object sender)
        {
            base.WindowClosed(doc, sender);
            if (sender is Guid)
            {
                SelRequest.CaacDocumentId = doc.DocumentId;
                SelRequest.CaacDocumentName = doc.Name;
            }
            else
            {
                var relatedDoc = new RelatedDocDTO
                {
                    Id = RandomHelper.Next(),
                    DocumentId = doc.DocumentId,
                    DocumentName = doc.Name,
                    SourceId = SelRequest.Id
                };
                SelRequest.RelatedDocs.Add(relatedDoc);
            }
        }

        #endregion
        #region 创建新申请

        /// <summary>
        ///     创建新计划
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

        #region 删除关联文档

        /// <summary>
        ///     删除关联文档
        /// </summary>
        public DelegateCommand<object> RemoveDocCommand { get; private set; }

        private void OnRemoveDoc(object obj)
        {
            var doc = obj as RelatedDocDTO;
            if (doc != null)
            {
                MessageConfirm("确认移除", "是否移除关联文档：" + doc.DocumentName + "？", (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        _selRequest.RelatedDocs.Remove(doc);
                    }
                });
            }
        }

        private bool CanRemoveDoc(object obj)
        {
            return _selRequest != null;
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

        #region GridView单元格变更处理

        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        ///     GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "ActionType"))
                {
                    var planhistory = gridView.CurrentCellInfo.Item as PlanHistoryDTO;
                    if (planhistory != null)
                    {
                        planhistory.AircraftCategories = new ObservableCollection<AircraftCateDTO>();
                        planhistory.AircraftCategories.AddRange(_service.GetAircraftCategoriesForPlanHistory(planhistory));

                        var actionCategory =
                            SelPlanHistory.ActionCategories.FirstOrDefault(p => p.Id == planhistory.ActionCategoryId);
                        if (actionCategory != null && actionCategory.NeedRequest)
                        {
                            planhistory.NeedRequest = actionCategory.NeedRequest;
                            planhistory.CanRequest = (int)CanRequest.未报计划;
                            planhistory.CanDeliver = (int)CanDeliver.未申请;
                        }
                        else if (actionCategory != null && !actionCategory.NeedRequest)
                        {
                            planhistory.NeedRequest = actionCategory.NeedRequest;
                            planhistory.CanRequest = (int)CanRequest.无需申请;
                            planhistory.CanDeliver = (int)CanDeliver.可交付;
                        }
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}