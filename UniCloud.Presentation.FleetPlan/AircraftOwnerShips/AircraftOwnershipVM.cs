#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/09，08:01
// 文件名：AircraftOwnershipVM.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan.Enums;

#endregion

namespace UniCloud.Presentation.FleetPlan.AircraftOwnerShips
{
    [Export(typeof (AircraftOwnershipVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AircraftOwnershipVM : EditViewModelBase
    {
        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;


        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public AircraftOwnershipVM(IFleetPlanService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialAircraft(); //初始化飞机信息 
            InitialCommand(); //初始化命令
        }

        #region 加载飞机

        private AircraftDTO _selectedAircraft;


        private OwnershipHistoryDTO _selectedOwnershipHistory;

        /// <summary>
        ///     选择飞机。
        /// </summary>
        public AircraftDTO SelectedAircraft
        {
            get { return _selectedAircraft; }
            set
            {
                if (_selectedAircraft != value)
                {
                    _selectedAircraft = value;
                    RefreshCommandState();
                    RaisePropertyChanged(() => SelectedAircraft);
                    RaisePropertyChanged(() => OwnershipTitle);
                }
            }
        }

        /// <summary>
        ///     选择所有权。
        /// </summary>
        public OwnershipHistoryDTO SelectedOwnershipHistory
        {
            get { return _selectedOwnershipHistory; }
            set
            {
                if (_selectedOwnershipHistory != value)
                {
                    _selectedOwnershipHistory = value;
                    RaisePropertyChanged(() => SelectedOwnershipHistory);
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     获取所有飞机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftDTO> AircraftsView { get; set; }

        /// <summary>
        ///     初始化飞机信息。
        /// </summary>
        private void InitialAircraft()
        {
            AircraftsView = _service.CreateCollection(_context.Aircrafts, o => o.OwnershipHistories);
            AircraftsView.FilterDescriptors.Add(new FilterDescriptor("IsOperation",FilterOperator.IsEqualTo,true));
            AircraftsView.PageSize = 20;
            AircraftsView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedAircraft == null)
                {
                    SelectedAircraft = e.Entities.Cast<AircraftDTO>().FirstOrDefault();
                }
                RefreshCommandState();
            };
        }

        #endregion

        #region 获取供应商

        /// <summary>
        ///     供应商
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }

        /// <summary>
        ///     加载供应商
        /// </summary>
        private void LoadSupplier()
        {
            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers), true);
        }

        #endregion

        #region 命令

        #region 新增所有权命令

        public DelegateCommand<object> AddOwnershipCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddOwnership(object sender)
        {
            StartDisplayDate  =
                SelectedAircraft.OwnershipHistories.Select(p => p.StartDate).OrderBy(p => p).LastOrDefault();
            //新建所有权历史
            var newOwnership = new OwnershipHistoryDTO
            {
                OwnershipHistoryId = Guid.NewGuid(),
                StartDate = (StartDisplayDate == null ||StartDisplayDate.Value==DateTime.MinValue)? DateTime.Now:StartDisplayDate.Value.AddDays(1)
            };

            SelectedOwnershipHistory = newOwnership;
            SelectedAircraft.OwnershipHistories.Add(newOwnership);
            RefreshCommandState();
        }

        /// <summary>
        ///     判断新增命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddOwnership(object sender)
        {
            return GetButtonState() && SelectedAircraft != null 
                &&!SelectedAircraft.OwnershipHistories.Any(p=>p.Status<(int)OperationStatus.已审核)
                &&!_service.HasChanges;
        }

        #endregion

        #region 删除所有权

        public DelegateCommand<object> RemoveOwnershipCommand { get; private set; }

        /// <summary>
        ///     执行删除命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndRemoveOwnership(object sender)
        {
            if (SelectedOwnershipHistory==null)
            {
                MessageAlert("请选择需要删除的所有权");
            }
            SelectedAircraft.OwnershipHistories.Remove(SelectedOwnershipHistory);
            RefreshCommandState();
        }

        /// <summary>
        ///     判断删除命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanRemoveOwnership(object sender)
        {
            return GetButtonState() && SelectedOwnershipHistory != null && SelectedOwnershipHistory.Status<(int)OperationStatus.已审核;
        }

        #endregion

        #region 提交审核

        public DelegateCommand<object> SubmitOwnershipCommand { get; private set; }

        /// <summary>
        ///     提交审核。
        /// </summary>
        /// <param name="sender"></param>
        public void OnSubmitOwnership(object sender)
        {
            if (SelectedOwnershipHistory == null)
            {
                MessageAlert("所有权不能为空");
                return;
            }
            SelectedOwnershipHistory.Status = (int) OperationStatus.待审核;
            RefreshCommandState();
        }

        /// <summary>
        ///     判断提交审核命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>提交审核命令是否可用。</returns>
        public bool CanSubmitOwnership(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedOwnershipHistory != null && SelectedOwnershipHistory.Status < (int) RequestStatus.待审核;
        }

        #endregion

        #region 审核

        public DelegateCommand<object> ReviewOwnershipCommand { get; private set; }

        /// <summary>
        ///     执行编辑付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnReviewOwnership(object sender)
        {
            if (SelectedOwnershipHistory == null)
            {
                MessageAlert("所有权不能为空");
                return;
            }
            SelectedOwnershipHistory.Status = (int) OperationStatus.已审核;
            RefreshCommandState();
        }

        /// <summary>
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanReviewOwnership(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedOwnershipHistory != null && SelectedOwnershipHistory.Status < (int) OperationStatus.已审核;
        }

        #endregion

        /// <summary>
        ///     获取按钮状态
        /// </summary>
        /// <returns></returns>
        private bool GetButtonState()
        {
            //当处于加载中，按钮是不可用的
            return !AircraftsView.IsLoading
                   && !AircraftsView.IsSubmittingChanges;
        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialCommand()
        {
            AddOwnershipCommand = new DelegateCommand<object>(OndAddOwnership, CanAddOwnership);
            SubmitOwnershipCommand = new DelegateCommand<object>(OnSubmitOwnership,
                CanSubmitOwnership);
            ReviewOwnershipCommand = new DelegateCommand<object>(OnReviewOwnership,
                CanReviewOwnership);
            RemoveOwnershipCommand = new DelegateCommand<object>(OndRemoveOwnership, CanRemoveOwnership);
        }

        #endregion

        #region 属性

        /// <summary>
        ///     所有权历史
        /// </summary>
        public string OwnershipTitle
        {
            get
            {
                if (SelectedAircraft != null)
                {
                    return SelectedAircraft.RegNumber + "所有权历史";
                }
                return "飞机所有权历史";
            }
        }

        private DateTime? _startDisplayDate;
        /// <summary>
        /// 开始时间 
        /// </summary>
        public DateTime? StartDisplayDate
        {
            get { return _startDisplayDate; }
            set
            {
                if (_startDisplayDate!=value)
                {
                    _startDisplayDate = value;
                    RaisePropertyChanged(()=>StartDisplayDate);
                }
            }
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            LoadSupplier();
            if (!AircraftsView.AutoLoad)
            {
                AircraftsView.AutoLoad = true;
            }
            else
            {
                AircraftsView.Load(true);
            }
        }

        protected override void RefreshCommandState()
        {
            AddOwnershipCommand.RaiseCanExecuteChanged();
            RemoveOwnershipCommand.RaiseCanExecuteChanged();
            SubmitOwnershipCommand.RaiseCanExecuteChanged();
            ReviewOwnershipCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}