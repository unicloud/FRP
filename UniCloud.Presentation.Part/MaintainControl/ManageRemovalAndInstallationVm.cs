#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/16 0:17:49
// 文件名：ManageRemovalAndInstallationVm
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
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.GridView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.MaintainControl
{
    [Export(typeof(ManageRemovalAndInstallationVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class ManageRemovalAndInstallationVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private readonly PartData _context;
        private bool _addChildView = false;
        private bool _removeChildView = false;

        [ImportingConstructor]
        public ManageRemovalAndInstallationVm(IRegionManager regionManager, IPartService service)
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
            SnRemInstRecords = _service.CreateCollection(_context.SnRemInstRecords);
            SnRemInstRecords.PageSize = 20;
            SnRemInstRecords.LoadedData += (o, e) =>
                                           {
                                               if (SelSnRemInstRecord == null)
                                                   SelSnRemInstRecord = SnRemInstRecords.FirstOrDefault();
                                           };
            _service.RegisterCollectionView(SnRemInstRecords);

            SnRegs = _service.CreateCollection(_context.SnRegs);
            _service.RegisterCollectionView(SnRegs);

            SnHistories = _service.CreateCollection(_context.SnHistories);
            _service.RegisterCollectionView(SnHistories);

            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_context, _context.Aircrafts);
            Aircrafts.LoadedData += (s, e) => RefreshCommandState();
            PnRegs = new QueryableDataServiceCollectionView<PnRegDTO>(_context, _context.PnRegs);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddRemovalCommand = new DelegateCommand<object>(OnAddRemoval, CanAddRemoval);
            RemoveRemovalCommand = new DelegateCommand<object>(OnRemoveRemoval, CanRemoveRemoval);
            AddInstallationCommand = new DelegateCommand<object>(OnAddInstallation, CanAddInstallation);
            RemoveInstallationCommand = new DelegateCommand<object>(OnRemoveInstallation, CanRemoveInstallation);
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
        }

        #endregion

        #region 数据

        #region 公共属性

        /// <summary>
        ///     飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }

        /// <summary>
        ///     所有的装机历史集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnHistoryDTO> SnHistories { get; set; }

        /// <summary>
        ///     拆换类型
        /// </summary>
        public Dictionary<int, ActionType> ActionTypes
        {
            get { return Enum.GetValues(typeof(ActionType)).Cast<object>().ToDictionary(value => (int)value, value => (ActionType)value); }
        }

        /// <summary>
        ///     序号件状态
        /// </summary>
        public Dictionary<int, SnStatus> SnStatuss
        {
            get { return Enum.GetValues(typeof(SnStatus)).Cast<object>().ToDictionary(value => (int)value, value => (SnStatus)value); }
        }

        /// <summary>
        ///     所有的件号集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnRegDTO> PnRegs { get; set; }

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
            Aircrafts.Load(true);
            PnRegs.Load(true);

            if (!SnRemInstRecords.AutoLoad)
                SnRemInstRecords.AutoLoad = true;

            if (!SnRegs.AutoLoad)
                SnRegs.AutoLoad = true;

            if (!SnHistories.AutoLoad)
                SnHistories.AutoLoad = true;
        }

        #region 业务

        #region 序号件集合

        private ObservableCollection<SnRegDTO> _onBoardSnRegs = new ObservableCollection<SnRegDTO>();
        private ObservableCollection<SnRegDTO> _inStoreSnRegs = new ObservableCollection<SnRegDTO>();

        /// <summary>
        ///     序号件集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnRegDTO> SnRegs { get; set; }

        /// <summary>
        /// 在位件（可用于拆下的件）
        /// </summary>
        public ObservableCollection<SnRegDTO> OnBoardSnRegs
        {
            get { return _onBoardSnRegs; }
            private set
            {
                if (_onBoardSnRegs != value)
                {
                    _onBoardSnRegs = value;
                    RaisePropertyChanged(() => OnBoardSnRegs);
                }
            }
        }

        /// <summary>
        /// 在库件（可用于装上的件集合:在库、在修、出租的都行）
        /// 其中在修、出租的在系统中没有按正常入库流程考虑修完或出租收回的情况
        /// </summary>
        public ObservableCollection<SnRegDTO> InStoreSnRegs
        {
            get { return _inStoreSnRegs; }
            private set
            {
                if (_inStoreSnRegs != value)
                {
                    _inStoreSnRegs = value;
                    RaisePropertyChanged(() => InStoreSnRegs);
                }
            }
        }
        #endregion

        #region 拆换记录

        private SnRemInstRecordDTO _selSnRemInstRecord;

        /// <summary>
        ///     拆换记录集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnRemInstRecordDTO> SnRemInstRecords { get; set; }

        /// <summary>
        ///     选择的附件项
        /// </summary>
        public SnRemInstRecordDTO SelSnRemInstRecord
        {
            get { return _selSnRemInstRecord; }
            private set
            {
                if (_selSnRemInstRecord != value)
                {
                    _selSnRemInstRecord = value;
                    //获取界面拆换历史的数据集合
                    Installations = new ObservableCollection<SnHistoryDTO>();
                    Removals = new ObservableCollection<SnHistoryDTO>();
                    if (value != null && SnHistories.SourceCollection.Cast<SnHistoryDTO>().Count() != 0)
                    {
                        foreach (var snHistory in SnHistories.SourceCollection.Cast<SnHistoryDTO>())
                        {
                            if (snHistory.RemInstRecordId == value.Id && snHistory.ActionType == (int)ActionType.装上) Installations.Add(snHistory);
                            else if (snHistory.RemInstRecordId == value.Id && snHistory.ActionType == (int)ActionType.拆下) Removals.Add(snHistory);
                        }
                    }
                    SelInstallation = Installations.FirstOrDefault();
                    SelRemoval = Removals.FirstOrDefault();
                    //获取用于子窗体展示的件号集合
                    OnBoardSnRegs = new ObservableCollection<SnRegDTO>();
                    InStoreSnRegs = new ObservableCollection<SnRegDTO>();
                    if (value != null && value.AircraftId != Guid.Empty)
                    {
                        OnBoardSnRegs.AddRange(SnRegs.Where(p => p.SnStatus == SnStatus.装机));
                        InStoreSnRegs.AddRange(SnRegs.Where(p => p.SnStatus == SnStatus.在库 || p.SnStatus == SnStatus.在修 || p.SnStatus == SnStatus.出租));
                        RaisePropertyChanged(() => OnBoardSnRegs);
                        RaisePropertyChanged(() => InStoreSnRegs);
                    }
                    RaisePropertyChanged(() => SelSnRemInstRecord);
                    RaisePropertyChanged(() => Installations);
                    RaisePropertyChanged(() => Removals);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 拆下件集合

        private SnHistoryDTO _selRemoval;

        /// <summary>
        ///     拆下件集合
        /// </summary>
        public ObservableCollection<SnHistoryDTO> Removals { get; set; }

        /// <summary>
        ///     选择的拆下件
        /// </summary>
        public SnHistoryDTO SelRemoval
        {
            get { return _selRemoval; }
            set
            {
                if (_selRemoval != value)
                {
                    _selRemoval = value;
                    RaisePropertyChanged(() => SelRemoval);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 装上件集合

        private SnHistoryDTO _selInstallation;

        /// <summary>
        ///     装上件集合
        /// </summary>
        public ObservableCollection<SnHistoryDTO> Installations { get; set; }

        /// <summary>
        ///     选择的装上件
        /// </summary>
        public SnHistoryDTO SelInstallation
        {
            get { return _selInstallation; }
            set
            {
                if (_selInstallation != value)
                {
                    _selInstallation = value;
                    RaisePropertyChanged(() => SelInstallation);
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
            NewCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            AddRemovalCommand.RaiseCanExecuteChanged();
            RemoveRemovalCommand.RaiseCanExecuteChanged();
            AddInstallationCommand.RaiseCanExecuteChanged();
            RemoveInstallationCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建拆换记录

        /// <summary>
        ///     创建拆换记录
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            if (Aircrafts.SourceCollection.Cast<AircraftDTO>().Count() != 0)
            {
                var newSnRemInstRecord = new SnRemInstRecordDTO
                {
                    Id = RandomHelper.Next(),
                    ActionDate = DateTime.Now,
                    AircraftId = Aircrafts.First().Id
                };
                SnRemInstRecords.AddNew(newSnRemInstRecord);
                SelSnRemInstRecord = newSnRemInstRecord;
            }
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            return Aircrafts.SourceCollection.Cast<AircraftDTO>().Count() != 0;
        }

        #endregion

        #region 删除拆换记录

        /// <summary>
        ///     删除拆换记录
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            ////判断要删除的拆换记录中涉及到的序号件有没有后续的拆装记录，有的话不能删除
            //int count = 0;
            //var snInstallHistories = SnHistories.Where(p => p.InstallRecordId == SelSnRemInstRecord.Id && p.RemoveRecordId != null).ToList();
            //if (snInstallHistories != null && snInstallHistories.Count != 0)
            //{
            //    var snHistory = snInstallHistories.First();
            //    MessageAlert("此次拆装中存在装上序号件" + snHistory.Sn + "在之后有新的拆下动作，因此不能删除此拆换记录！");
            //    count = 1;
            //}

            //var snRemoveHistories = SnHistories.Where(p => p.RemoveRecordId == SelSnRemInstRecord.Id).ToList();
            //snRemoveHistories.ForEach(p =>
            //{
            //    var snHistory = SnHistories.Where(s => s.SnRegId == p.SnRegId).OrderBy(l => l.InstallDate).LastOrDefault();
            //    if (snHistory != null && snHistory.Id != p.Id)
            //    {
            //        MessageAlert("此次拆装中存在拆下序号件" + p.Sn + "在之后有新的装机动作，因此不能删除此拆换记录！");
            //        count = 1;
            //    }
            //});
            //if (count != 0) return;

            ////判断完之后，删除拆换记录（清除拆下信息，删除装上记录）
            //snRemoveHistories.ForEach(p =>
            //{
            //    p.RemoveRecordId = null;
            //    p.RemoveReason = null;
            //    p.RemoveReason = null;
            //    var snReg = SnRegs.FirstOrDefault(sn => sn.Id == p.SnRegId);
            //    if (snReg != null) snReg.AircraftId = p.AircraftId;
            //});
            //SnHistories.Where(p => p.InstallRecordId == SelSnRemInstRecord.Id && p.RemoveRecordId == null).ToList().ForEach(
            //    p =>
            //    {
            //        var snReg = SnRegs.FirstOrDefault(sn => sn.Id == p.SnRegId);
            //        if (snReg != null) snReg.AircraftId = null;
            //        SnHistories.Remove(p);
            //    });
            //SnRemInstRecords.Remove(SelSnRemInstRecord);
            //RefreshCommandState();
        }

        private bool CanRemove(object obj)
        {
            return SelSnRemInstRecord != null;
        }

        #endregion

        #region 增加拆下件

        /// <summary>
        ///     增加拆下件
        /// </summary>
        public DelegateCommand<object> AddRemovalCommand { get; private set; }

        private void OnAddRemoval(object obj)
        {
            if (SelSnRemInstRecord == null)
                MessageAlert("提示", "请先选中拆换记录！");
            else if (SelSnRemInstRecord.AircraftId == Guid.Empty)
                MessageAlert("提示", "请先选择飞机！");
            else
            {
                //初始化子窗体
                _childViewHeader = "在位附件";
                OnBoardSnRegs = new ObservableCollection<SnRegDTO>();
                OnBoardSnRegs.AddRange(SnRegs.Where(p => p.AircraftId == SelSnRemInstRecord.AircraftId));
                if (SelSnRemInstRecord != null && SelSnRemInstRecord.AircraftId != Guid.Empty)
                {
                    ViewSnRegs = OnBoardSnRegs;
                }
                _addChildView = false;
                _removeChildView = true;
                SnRegsChildView.ShowDialog();
            }
            RefreshCommandState();
        }

        private bool CanAddRemoval(object obj)
        {
            if (SelSnRemInstRecord == null)
                return false;
            if (SelSnRemInstRecord.AircraftId == Guid.Empty)
                return false;
            if (SelSnRemInstRecord.ActionType == (int)ActionType.装上)
                return false;
            return true;
        }

        #endregion

        #region 删除拆下件

        /// <summary>
        ///     删除拆下件
        /// </summary>
        public DelegateCommand<object> RemoveRemovalCommand { get; private set; }

        private void OnRemoveRemoval(object obj)
        {
            //SelRemoval.RemoveRecordId = null;
            //SelRemoval.RemoveReason = null;
            //SelRemoval.RemoveReason = null;
            //var snReg = SnRegs.FirstOrDefault(p => p.Id == SelRemoval.SnRegId);
            //if (snReg != null) snReg.AircraftId = SelRemoval.AircraftId;
            //Removals.Remove(SelRemoval);
            //SelRemoval = Removals.FirstOrDefault();
            //RefreshCommandState();
        }

        private bool CanRemoveRemoval(object obj)
        {
            return _selRemoval != null;
        }

        #endregion

        #region 增加装上件

        /// <summary>
        ///     增加装上件
        /// </summary>
        public DelegateCommand<object> AddInstallationCommand { get; private set; }

        private void OnAddInstallation(object obj)
        {
            if (SelSnRemInstRecord == null)
                MessageAlert("提示", "请先选中拆换记录！");
            else if (SelSnRemInstRecord.AircraftId == Guid.Empty)
                MessageAlert("提示", "请先选择飞机！");
            else
            {
                //初始化子窗体
                _childViewHeader = "在库附件";
                InStoreSnRegs = new ObservableCollection<SnRegDTO>();
                InStoreSnRegs.AddRange(SnRegs.Where(p => p.AircraftId == null || p.AircraftId == Guid.Empty));
                if (SelSnRemInstRecord != null && SelSnRemInstRecord.AircraftId != Guid.Empty)
                {
                    ViewSnRegs = InStoreSnRegs;
                }
                _addChildView = true;
                _removeChildView = false;
                SnRegsChildView.ShowDialog();
            }
            RefreshCommandState();
        }

        private bool CanAddInstallation(object obj)
        {
            if (SelSnRemInstRecord == null)
                return false;
            if (SelSnRemInstRecord.AircraftId == Guid.Empty)
                return false;
            if (SelSnRemInstRecord.ActionType == (int)ActionType.拆下)
                return false;
            return true;
        }

        #endregion

        #region 删除装上件

        /// <summary>
        ///     删除装上件
        /// </summary>
        public DelegateCommand<object> RemoveInstallationCommand { get; private set; }

        private void OnRemoveInstallation(object obj)
        {
            var snReg = SnRegs.FirstOrDefault(p => p.Id == SelInstallation.SnRegId);
            if (snReg != null) snReg.AircraftId = null;
            var snHistory = SnHistories.FirstOrDefault(p => p.Id == SelInstallation.Id);
            Installations.Remove(SelInstallation);
            if (snHistory != null) SnHistories.Remove(snHistory);
            SelInstallation = Installations.FirstOrDefault();
            RefreshCommandState();
        }

        private bool CanRemoveInstallation(object obj)
        {
            return _selInstallation != null;
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
                GridViewCell cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "Aircraft"))
                {
                    Guid value = SelSnRemInstRecord.AircraftId;
                    //获取用于子窗体展示的件号集合
                    OnBoardSnRegs = new ObservableCollection<SnRegDTO>();
                    InStoreSnRegs = new ObservableCollection<SnRegDTO>();
                    if (value != Guid.Empty)
                    {
                        OnBoardSnRegs.AddRange(SnRegs.Where(p => p.AircraftId == value));
                        InStoreSnRegs.AddRange(SnRegs.Where(p => p.AircraftId == Guid.Empty));
                    }
                    RefreshCommandState();
                }
                else if (string.Equals(cell.Column.UniqueName, "ActionType"))
                {
                    //if (SelSnRemInstRecord.ActionType == (int) ActionType.拆下 && Installations.Count != 0)
                    //{
                    //    MessageConfirm("已有装上记录，是否修改成拆下类型！（如果是，则会自动删除装上记录）", (s, arg) =>
                    //    {
                    //        if (arg.DialogResult != true) SelSnRemInstRecord.ActionType=(int)ActionType.装上;
                    //    });
                    //}
                    RefreshCommandState();
                }
                else if (string.Equals(cell.Column.UniqueName, "PnReg"))
                {
                    int value = SelInstallation.PnRegId;
                    var pnReg = PnRegs.FirstOrDefault(p => p.Id == value);
                    var snReg = SnRegs.FirstOrDefault(p => p.Id == SelInstallation.SnRegId);
                    if (pnReg != null)
                    {
                        SelInstallation.Pn = pnReg.Pn;
                        if (snReg != null && snReg.PnRegId != value)
                        {
                            snReg.Pn = pnReg.Pn;
                            snReg.PnRegId = value;
                        }
                    }
                    RefreshCommandState();
                }
                else if (string.Equals(cell.Column.UniqueName, "Status"))
                {
                    var snReg = SnRegs.FirstOrDefault(p => p.Id == SelRemoval.SnRegId);
                    if (snReg != null)
                    {
                        snReg.Status = SelRemoval.Status;
                    }
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #endregion

        #region 子窗体相关

        [Import]
        public SnRegsChildView SnRegsChildView; //初始化子窗体

        /// <summary>
        /// 子窗体展示集合
        /// </summary>
        public ObservableCollection<SnRegDTO> ViewSnRegs { get; set; }

        private string _childViewHeader;

        /// <summary>
        /// 子窗体Header
        /// </summary>
        public string ChildViewHeader
        {
            get { return _childViewHeader; }
            set
            {
                if (_childViewHeader != value)
                {
                    _childViewHeader = value;
                    RaisePropertyChanged(() => ChildViewHeader);
                }
            }
        }

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            _addChildView = false;
            _removeChildView = false;
            SnRegsChildView.Close();
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancelExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            var radGridView = sender as RadGridView;
            if (radGridView == null) return;
            if (SelSnRemInstRecord != null && SelSnRemInstRecord.AircraftId != Guid.Empty && _removeChildView)
            {
                radGridView.SelectedItems.ToList().ForEach(p =>
                {
                    var snRegDto = p as SnRegDTO;
                    if (snRegDto != null)
                    {
                        //找到SnReg的最后一条装上记录（时间最大），且这条记录无拆下信息
                        var snHis = SnHistories.Where(s => s.SnRegId == snRegDto.Id).OrderBy(l => l.ActionDate).LastOrDefault();
                        if (snHis == null) MessageAlert("序号件：" + snRegDto.Sn + "没有找到拆装历史！请检查！");
                        else if (snHis.ActionType == (int)ActionType.拆下)
                        {
                            MessageAlert("序号件：" + snRegDto.Sn + "最近一次操作为拆下，不能再做拆下操作！请检查！");
                        }
                        else
                        {
                            var newSh = new SnHistoryDTO
                            {
                                Id = RandomHelper.Next(),
                                ActionDate = DateTime.Now,
                                ActionNo = SelSnRemInstRecord.ActionNo,
                                ActionType = (int)ActionType.拆下,
                                AircraftId = SelSnRemInstRecord.AircraftId,
                                RegNumber = SelSnRemInstRecord.RegNumber,
                                Pn = snRegDto.Pn,
                                Sn = snRegDto.Sn,
                                PnRegId = snRegDto.PnRegId,
                                SnRegId = snRegDto.Id,
                                RemInstRecordId = SelSnRemInstRecord.Id,
                                Status = (int)SnStatus.在库,
                            };
                            Removals.Add(newSh);
                            var snReg = SnRegs.FirstOrDefault(sn => sn.Id == snHis.SnRegId);
                            if (snReg != null)
                            {
                                snHis.Status = newSh.Status;
                                snReg.AircraftId = null;
                            }
                        }
                    }
                });
                SelRemoval = Removals.FirstOrDefault();
                _addChildView = false;
                _removeChildView = false;
            }
            else if (SelSnRemInstRecord != null && _addChildView)
            {
                radGridView.SelectedItems.ToList().ForEach(p =>
                {
                    var snRegDto = p as SnRegDTO;
                    if (snRegDto != null)
                    {
                        var snHis = SnHistories.Where(s => s.SnRegId == snRegDto.Id).OrderBy(l => l.ActionDate).LastOrDefault();
                        if (snHis != null && snHis.ActionType == (int)ActionType.装上)
                        {
                            MessageAlert("序号件：" + snRegDto.Sn + "最近一次操作为装上，不能再做装上操作！请检查！");
                        }
                        else
                        {
                            var newSnHis = new SnHistoryDTO
                            {
                                Id = RandomHelper.Next(),
                                ActionDate = DateTime.Now,
                                ActionNo = SelSnRemInstRecord.ActionNo,
                                ActionType = (int)ActionType.装上,
                                AircraftId = SelSnRemInstRecord.AircraftId,
                                RegNumber = SelSnRemInstRecord.RegNumber,
                                Pn = snRegDto.Pn,
                                Sn = snRegDto.Sn,
                                PnRegId = snRegDto.PnRegId,
                                SnRegId = snRegDto.Id,
                                RemInstRecordId = SelSnRemInstRecord.Id,
                                Status = (int)SnStatus.装机,
                            };
                            var snReg = SnRegs.FirstOrDefault(sn => sn.Id == snRegDto.Id);
                            if (snReg != null)
                            {
                                snReg.AircraftId = SelSnRemInstRecord.AircraftId;
                                snReg.Status = (int)SnStatus.装机;
                            }
                            Installations.Add(newSnHis);
                            SnHistories.AddNew(newSnHis);
                        }
                    }
                });
                SelInstallation = Installations.FirstOrDefault();
                _addChildView = false;
                _removeChildView = false;
            }
            RefreshCommandState();
            SnRegsChildView.Close();
        }

        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommitExecute(object sender)
        {
            return true;
        }

        #endregion

        #endregion
        #endregion
    }
}