#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/25 13:52:33
// 文件名：FleetPlanPublishVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(FleetPlanPublishVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FleetPlanPublishVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;
        private FilterDescriptor _annualDescriptor;
        private AnnualDTO _curAnnual = new AnnualDTO();


        [ImportingConstructor]
        public FleetPlanPublishVM(IRegionManager regionManager, IFleetPlanService service)
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
            CurAnnuals = _service.CreateCollection(_context.Annuals);
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
                    _curAnnual = CurAnnuals.First();
                }
                Plans.Load(true);
                RefreshCommandState();
            };
            _service.RegisterCollectionView(CurAnnuals);//注册查询集合

            Plans = _service.CreateCollection(_context.Plans);
            Plans.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                ViewPlans.Clear();
                ViewPlans.AddRange(e.Entities.Cast<PlanDTO>().Where(p => p.Year == _curAnnual.Year));
                PublishingPlan = ViewPlans.Where(p =>
                    p.PublishStatus > (int)PlanPublishStatus.待发布 &&
                    p.PublishStatus < (int)PlanPublishStatus.已发布);
                SelPlan = ViewPlans.OrderBy(p => p.VersionNumber).LastOrDefault();
                LastPublishedPlan =
                    ViewPlans.OrderBy(p => p.VersionNumber)
                        .LastOrDefault(p => p.PlanPublishStatus == PlanPublishStatus.已发布);
                RefreshCommandState();
            };
            _service.RegisterCollectionView(Plans);//注册查询集合
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            CommitCommand = new DelegateCommand<object>(OnCommit, CanCommit);
            ExamineCommand = new DelegateCommand<object>(OnExamine, CanExamine);
            SendCommand = new DelegateCommand<object>(OnSend, CanSend);
            RepealCommand = new DelegateCommand<object>(OnRepeal, CanRepeal);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 当前计划年度

        /// <summary>
        ///     当前计划年度
        /// </summary>
        public QueryableDataServiceCollectionView<AnnualDTO> CurAnnuals { get; set; }

        #endregion

        #region 所有运力增减计划集合

        /// <summary>
        /// 所有运力增减计划集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> Plans { get; set; }

        #endregion

        #region 最新发布的计划

        private PlanDTO _lastPublishedPlan;

        /// <summary>
        ///     最新发布的计划
        /// </summary>
        public PlanDTO LastPublishedPlan
        {
            get { return _lastPublishedPlan; }
            private set
            {
                if (_lastPublishedPlan != value)
                {
                    _lastPublishedPlan = value;
                    RaisePropertyChanged(() => LastPublishedPlan);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 发布中的计划集合

        private IEnumerable<PlanDTO> _publishingPlan;

        /// <summary>
        ///     发布中的计划集合
        /// </summary>
        public IEnumerable<PlanDTO> PublishingPlan
        {
            get { return _publishingPlan; }
            private set
            {
                if (!Equals(_publishingPlan, value))
                {
                    _publishingPlan = value;
                    RaisePropertyChanged(() => PublishingPlan);
                    // 刷新按钮状态
                    RefreshCommandState();
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
            CurAnnuals.Load(true);
        }

        #region 业务

        #region 当前年度运力增减计划集合

        /// <summary>
        /// 当前年度运力增减计划集合
        /// </summary>
        private ObservableCollection<PlanDTO> _viewPlans = new ObservableCollection<PlanDTO>();

        /// <summary>
        /// 当前年度运力增减
        /// </summary>
        public ObservableCollection<PlanDTO> ViewPlans
        {
            get { return _viewPlans; }
            private set
            {
                if (_viewPlans != null)
                {
                    _viewPlans = value;
                    RaisePropertyChanged(() => ViewPlans);
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
                    PlanHistories.Clear();
                    foreach (var ph in value.PlanHistories)
                    {
                        PlanHistories.Add(ph);
                    }
                    RaisePropertyChanged(() => SelPlan);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 计划明细集合

        private ObservableCollection<PlanHistoryDTO> _planHistories = new ObservableCollection<PlanHistoryDTO>();

        /// <summary>
        ///     计划明细集合
        /// </summary>
        public ObservableCollection<PlanHistoryDTO> PlanHistories
        {
            get { return _planHistories; }
            private set
            {
                if (_planHistories != value)
                {
                    _planHistories = value;
                    RaisePropertyChanged(() => PlanHistories);
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
            CommitCommand.RaiseCanExecuteChanged();
            ExamineCommand.RaiseCanExecuteChanged();
            SendCommand.RaiseCanExecuteChanged();
            RepealCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 提交审核

        /// <summary>
        ///     提交审核
        /// </summary>
        public DelegateCommand<object> CommitCommand { get; private set; }

        private void OnCommit(object obj)
        {
            SelPlan.PublishStatus = (int)PlanPublishStatus.待审核;
            PublishingPlan.ToList().Add(SelPlan);
            RefreshCommandState();
        }

        private bool CanCommit(object obj)
        {
            // 没有选中计划，或者当前已有发布中的计划时，按钮不可用
            if (SelPlan == null || PublishingPlan.Any())
            {
                return false;
            }
            // 有未保存内容时，按钮不可用
            if (this._service.HasChanges)
            {
                return false;
            }
            // 1、不存在已发布的计划
            // 编制状态处于已提交，且发布状态为待发布时，按钮可用
            if (LastPublishedPlan == null)
            {
                return SelPlan.Status == (int)PlanStatus.已提交 &&
                       SelPlan.PublishStatus == (int)PlanPublishStatus.待发布;
            }
            // 2、存在已发布的计划
            // 编制状态处于已提交，发布状态为待发布，且选中计划的版本高于最后一份已发布计划版本时，按钮可用
            return SelPlan.Status == (int)PlanStatus.已提交 &&
                   SelPlan.PublishStatus == (int)PlanPublishStatus.待发布 &&
                   SelPlan.VersionNumber > LastPublishedPlan.VersionNumber;
        }

        #endregion

        #region 审核

        /// <summary>
        ///     审核
        /// </summary>
        public DelegateCommand<object> ExamineCommand { get; private set; }

        private void OnExamine(object obj)
        {
            SelPlan.PublishStatus = (int)PlanPublishStatus.已审核;
            RefreshCommandState();
        }

        private bool CanExamine(object obj)
        {
            // 有未保存内容时，按钮不可用
            if (_service.HasChanges)
            {
                return false;
            }
            // 选中计划发布状态为待审核时，按钮可用
            return SelPlan != null && SelPlan.PublishStatus == (int)PlanPublishStatus.待审核;
        }

        #endregion

        #region 发送

        /// <summary>
        ///     发送
        /// </summary>
        public DelegateCommand<object> SendCommand { get; private set; }

        private void OnSend(object obj)
        {

            MessageConfirm("确认发布计划", "是否向【民航局】报送计划发布情况？", (o, e) =>
            {
                if (e.DialogResult == true)
                {
                    // 审核、已提交状态下可以发送。如果已处于提交状态，需要重新发送的，不必改变状态。
                    if (this.SelPlan.PublishStatus != (int)PlanPublishStatus.已发布)
                    {
                        this.SelPlan.PublishStatus = (int)PlanPublishStatus.已发布;
                        //_service.SubmitChanges(sc =>
                        //{
                        //    if (sc.Error == null)
                        //    {
                        //        // 发送不成功的，也认为是已经做了发送操作，不回滚状态。始终可以重新发送。
                        //        _service.TransferPlan(this.SelPlan.PlanID, tp => { }, null);
                        //    }
                        //}, null);
                    }
                    else
                    {
                        //_service.TransferPlan(this.SelPlan.PlanID, tp => { }, null);
                    }
                }
            });
            RefreshCommandState();
        }

        private bool CanSend(object obj)
        {
            // 没有选中计划，按钮不可用
            if (this.SelPlan == null)
            {
                return false;
            }
            // 选中计划发布状态不是已审核与已提交时，按钮不可用。
            if (this.SelPlan.PublishStatus != (int)PlanPublishStatus.已审核 && this.SelPlan.PublishStatus != (int)PlanPublishStatus.已发布)
            {
                return false;
            }
            // 当前没有未保存内容时，按钮可用
            return !_service.HasChanges;
        }

        #endregion

        #region 撤销发布

        /// <summary>
        ///     撤销发布
        /// </summary>
        public DelegateCommand<object> RepealCommand { get; private set; }

        private void OnRepeal(object obj)
        {
            var content = this.SelPlan.PublishStatus == (int)PlanPublishStatus.已发布
               ? "是否要将发布状态撤回到待发布（同时向民航局报送）？"
               : "是否要将发布状态撤回到待发布（不会向民航局报送）？";
            MessageConfirm("确认撤销发布", content, (o, e) =>
             {
                 if (e.DialogResult == true)
                 {
                     // 1、发布状态为已发布时，需要向民航局发送邮件
                     if (this.SelPlan.PublishStatus == (int)PlanPublishStatus.已发布)
                     {
                         this.SelPlan.PublishStatus = (int)PlanPublishStatus.待发布;
                         //_service.SubmitChanges(sc =>
                         //{
                         //    if (sc.Error == null)
                         //    {
                         //        this.service.TransferPlan(this.SelPlan.PlanID, tp => { }, null);
                         //        RefreshButtonState();
                         //    }
                         //}, null);
                         RefreshCommandState();
                     }
                     // 2、发布状态不是已发布时，无需向民航局发送邮件
                     else
                     {
                         SelPlan.PublishStatus = (int)PlanPublishStatus.待发布;
                         //_service.SubmitChanges(sc => { }, null);
                         RefreshCommandState();
                     }
                 }
             });

            PublishingPlan.ToList().Remove(SelPlan);
            RefreshCommandState();
        }

        private bool CanRepeal(object obj)
        {
            // 有未保存内容时，按钮不可用
            if (_service.HasChanges)
            {
                return false;
            }
            // 选中计划的发布状态不是待发布，且没有未保存的内容时，按钮可用
            return SelPlan != null && SelPlan.PublishStatus > (int)PlanPublishStatus.待发布 &&
                   !_service.HasChanges;
        }

        #endregion

        #endregion
    }
}