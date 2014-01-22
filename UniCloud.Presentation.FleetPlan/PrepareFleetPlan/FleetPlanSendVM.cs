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
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

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
        private FilterDescriptor _annualDescriptor;
        private AnnualDTO _curAnnual = new AnnualDTO();

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
                if (!Plans.AutoLoad)
                    Plans.AutoLoad = true;
                else
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
                ViewPlans.AddRange(e.Entities.Cast<PlanDTO>().Where(p => p.Year == _curAnnual.Year).OrderBy(p => p.VersionNumber));
                CurPlan.Clear();
                CurPlan.Add(Plans.Where(p => p.Year == _curAnnual.Year).OrderBy(p => p.VersionNumber).LastOrDefault());
                SelPlan = CurPlan.FirstOrDefault();
                RefreshCommandState();
            };
            _service.RegisterCollectionView(Plans);//注册查询集合

        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            SendCommand = new DelegateCommand<object>(OnSend, CanSend);
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
        private ObservableCollection<PlanDTO> _viewPlans=new ObservableCollection<PlanDTO>();


        public ObservableCollection<PlanDTO> ViewPlans
        {
            get { return _viewPlans; }
            private set
            {
                if (!Equals(_viewPlans, value))
                {
                    _viewPlans = value;
                    RaisePropertyChanged(() => ViewPlans);
                }
            }
        }

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
            SendCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 添加计划文档

        protected override bool CanAddAttach(object obj)
        {
            // 当前计划处于审核状态时，按钮可用
            return SelPlan != null && SelPlan.Status == (int)PlanStatus.已审核;
        }

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
                SelPlan.DocumentId = doc.DocumentId;
                SelPlan.DocName = doc.Name;
            }
        }
        #endregion

        #region 报送计划

        /// <summary>
        ///     报送计划
        /// </summary>
        public DelegateCommand<object> SendCommand { get; private set; }

        private void OnSend(object obj)
        {
            var content = "计划选择了" + SelPlan.PlanHistories.Count(p => p.IsSubmit) + "项报送的明细项，是否向【民航局】报送该计划？";
            MessageConfirm("确认报送计划", content, (o, e) =>
             {
                 if (e.DialogResult == true)
                 {
                     // 审核、已提交状态下可以发送。如果已处于提交状态，需要重新发送的，不必改变状态。
                     if (SelPlan != null && SelPlan.Status != (int)PlanStatus.已提交)
                     {
                         SelPlan.Status = (int)PlanStatus.已提交;
                         SelPlan.IsFinished = true;
                     }
                     SelPlan.SubmitDate = DateTime.Now;
                     //this.service.SubmitChanges(sc =>
                     //{
                     //    if (sc.Error == null)
                     //    {
                     //        this.AttachCommand.RaiseCanExecuteChanged();
                     //        // 发送不成功的，也认为是已经做了发送操作，不回滚状态。始终可以重新发送。
                     //        this.service.TransferPlan(this.CurrentPlan.PlanID, tp => { }, null);
                     //        RefreshButtonState();
                     //    }
                     //}, null);
                 }
             });
        }

        private bool CanSend(object obj)
        {
            // 当前计划的文档非空，当前计划处于审核、已发送状态，且当前没有未保存内容时，按钮可用
            if (SelPlan == null)
            {
                return false;
            }
            //if (string.IsNullOrWhiteSpace(this.CurrentPlan.DocNumber) || this.CurrentPlan.AttachDoc == null)
            if (SelPlan.DocumentId == null)
            {
                return false;
            }
            if (SelPlan.Status != (int)PlanStatus.已审核 && SelPlan.Status != (int)PlanStatus.已提交)
            {
                return false;
            }
            return !_service.HasChanges;
        }

        #endregion

        #endregion
    }
}