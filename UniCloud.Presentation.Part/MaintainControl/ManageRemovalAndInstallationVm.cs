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
            _service.RegisterCollectionView(SnRemInstRecords);

            SnRegs = _service.CreateCollection(_context.SnRegs);
            _service.RegisterCollectionView(SnRegs);

            SnHistories = _service.CreateCollection(_context.SnHistories);
            _service.RegisterCollectionView(SnHistories);

            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_context, _context.Aircrafts);


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
            CellEditEndCommand=new DelegateCommand<object>(OnCellEditEnd);
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
            get { return this._onBoardSnRegs; }
            private set
            {
                if (this._onBoardSnRegs != value)
                {
                    this._onBoardSnRegs = value;
                    RaisePropertyChanged(() => this.OnBoardSnRegs);
                }
            }
        }

        /// <summary>
        /// 在库件（可用于装上的件集合）
        /// </summary>
        public ObservableCollection<SnRegDTO> InStoreSnRegs
        {
            get { return this._inStoreSnRegs; }
            private set
            {
                if (this._inStoreSnRegs != value)
                {
                    this._inStoreSnRegs = value;
                    RaisePropertyChanged(() => this.InStoreSnRegs);
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
            get { return this._selSnRemInstRecord; }
            private set
            {
                if (this._selSnRemInstRecord != value)
                {
                    this._selSnRemInstRecord = value;
                    //获取界面拆换历史的数据集合
                    Installations = new ObservableCollection<SnHistoryDTO>();
                    Removals = new ObservableCollection<SnHistoryDTO>();
                    if (value != null && SnHistories.SourceCollection.Cast<SnHistoryDTO>().Count() != 0)
                    {
                        foreach (var snHistory in SnHistories.SourceCollection.Cast<SnHistoryDTO>())
                        {
                            if (snHistory.InstallRecordId == value.Id) Installations.Add(snHistory);
                            else if (snHistory.RemoveRecordId == value.Id) Removals.Add(snHistory);
                        }
                    }
                    //获取用于子窗体展示的件号集合
                    OnBoardSnRegs = new ObservableCollection<SnRegDTO>();
                    InStoreSnRegs = new ObservableCollection<SnRegDTO>();
                    if (value != null && value.AircraftId != Guid.Empty)
                    {
                        OnBoardSnRegs.AddRange(SnRegs.Where(p => p.AircraftId == value.AircraftId));
                        InStoreSnRegs.AddRange(SnRegs.Where(p => p.AircraftId == Guid.Empty));
                    }
                    RaisePropertyChanged(() => this.SelSnRemInstRecord);
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
            get { return this._selRemoval; }
            private set
            {
                if (this._selRemoval != value)
                {
                    this._selRemoval = value;
                    RaisePropertyChanged(() => this.SelRemoval);
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
            get { return this._selInstallation; }
            private set
            {
                if (this._selInstallation != value)
                {
                    this._selInstallation = value;
                    RaisePropertyChanged(() => this.SelInstallation);
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
            var newSnRemInst = new SnRemInstRecordDTO
            {
                Id = RandomHelper.Next(),
                ActionDate = DateTime.Now,
            };
            SnRemInstRecords.AddNew(newSnRemInst);
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除拆换记录

        /// <summary>
        ///     删除拆换记录
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            RefreshCommandState();
        }

        private bool CanRemove(object obj)
        {
            return false;
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
            //if (SelSnRemInstRecord.Position == null)
            //    return false;
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
            //if (SelSnRemInstRecord.Position == null)
            //    return false;
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
            get { return this._childViewHeader; }
            private set
            {
                if (this._childViewHeader != value)
                {
                    this._childViewHeader = value;
                    this.RaisePropertyChanged(() => this.ChildViewHeader);
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
                    }
                });
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
                    }
                });
                _addChildView = false;
                _removeChildView = false;
            }
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