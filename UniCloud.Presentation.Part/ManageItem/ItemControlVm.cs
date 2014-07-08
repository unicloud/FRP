#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/8 16:54:22
// 文件名：ItemControlVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.ManageItem
{
    [Export(typeof (ItemControlVm))]
    public class ItemControlVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;
        private bool _addToDependency;
        private bool _addToInstallController;


        [ImportingConstructor]
        public ItemControlVm(IPartService service)
            : base(service)
        {
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
            InstallControllers = _service.CreateCollection(_context.InstallControllers, o => o.Dependencies);
            InstallControllers.GroupDescriptors.Add(new GroupDescriptor
            {
                Member = "AircraftTypeName",
                SortDirection = ListSortDirection.Ascending
            });
            InstallControllers.GroupDescriptors.Add(new GroupDescriptor
            {
                Member = "ItemName",
                SortDirection = ListSortDirection.Ascending
            });
            InstallControllers.LoadedData += (o, e) =>
            {
                if (SelInstallController == null)
                    SelInstallController = InstallControllers.FirstOrDefault();
            };
            _service.RegisterCollectionView(InstallControllers);

            PnRegs = _service.CreateCollection(_context.PnRegs);
            _service.RegisterCollectionView(PnRegs);

            Items = new QueryableDataServiceCollectionView<ItemDTO>(_context, _context.Items);
            AircraftTypes = new QueryableDataServiceCollectionView<AircraftTypeDTO>(_context, _context.AircraftTypes);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddDependencyCommand = new DelegateCommand<object>(OnAddDependency, CanAddDependency);
            RemoveDependencyCommand = new DelegateCommand<object>(OnRemoveDependency, CanRemoveDependency);
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 附件集合

        private ObservableCollection<PnRegDTO> _viewPnRegs = new ObservableCollection<PnRegDTO>();

        /// <summary>
        ///     附件集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnRegDTO> PnRegs { get; set; }

        /// <summary>
        ///     选择的机型
        /// </summary>
        public ObservableCollection<PnRegDTO> ViewPnRegs
        {
            get { return _viewPnRegs; }
            private set
            {
                if (_viewPnRegs != value)
                {
                    _viewPnRegs = value;
                    RaisePropertyChanged(() => ViewPnRegs);
                }
            }
        }

        #endregion

        #region 机型集合

        private AircraftTypeDTO _selAircraftType;

        /// <summary>
        ///     机型集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftTypeDTO> AircraftTypes { get; set; }

        /// <summary>
        ///     选择的机型
        /// </summary>
        public AircraftTypeDTO SelAircraftType
        {
            get { return _selAircraftType; }
            set
            {
                if (_selAircraftType != value)
                {
                    _selAircraftType = value;
                    RaisePropertyChanged(() => SelAircraftType);
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 附件项

        private ItemDTO _selItem;

        /// <summary>
        ///     附件项集合
        /// </summary>
        public QueryableDataServiceCollectionView<ItemDTO> Items { get; set; }

        /// <summary>
        ///     选择的附件项
        /// </summary>
        public ItemDTO SelItem
        {
            get { return _selItem; }
            set
            {
                if (_selItem != value)
                {
                    _selItem = value;
                    RaisePropertyChanged(() => SelItem);
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
            AircraftTypes.Load(true);
            Items.Load(true);

            if (!PnRegs.AutoLoad)
                PnRegs.AutoLoad = true;
            PnRegs.Load(true);

            if (!InstallControllers.AutoLoad)
                InstallControllers.AutoLoad = true;
            InstallControllers.Load(true);
        }

        #region 业务

        #region 装机控制集合

        private InstallControllerDTO _selInstallController;

        /// <summary>
        ///     装机控制集合
        /// </summary>
        public QueryableDataServiceCollectionView<InstallControllerDTO> InstallControllers { get; set; }

        /// <summary>
        ///     选择的装机控制
        /// </summary>
        public InstallControllerDTO SelInstallController
        {
            get { return _selInstallController; }
            private set
            {
                if (_selInstallController != value)
                {
                    _selInstallController = value;
                    if (_selInstallController != null)
                        SelDependency = _selInstallController.Dependencies.FirstOrDefault();
                    RaisePropertyChanged(() => SelInstallController);
                }
                RefreshCommandState();
            }
        }

        #endregion

        #region 选择的依赖项

        private DependencyDTO _selDependency;

        /// <summary>
        ///     选择的依赖项
        /// </summary>
        public DependencyDTO SelDependency
        {
            get { return _selDependency; }
            set
            {
                if (_selDependency != value)
                {
                    _selDependency = value;
                    RaisePropertyChanged(() => SelDependency);
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
            AddDependencyCommand.RaiseCanExecuteChanged();
            RemoveDependencyCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 增加互换件

        /// <summary>
        ///     增加互换件
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            if (SelItem == null)
                MessageAlert("提示", "请先选中附件项！");
            else if (SelAircraftType == null)
                MessageAlert("提示", "请先选中机型！");
            else
            {
                ViewPnRegs = new ObservableCollection<PnRegDTO>();
                if (SelItem != null && SelAircraftType != null)
                {
                    ViewPnRegs.AddRange(
                        PnRegs.Where(p => p.IsLife == SelItem.IsLife && (p.ItemId == SelItem.Id || p.ItemId == null)
                                          &&
                                          !InstallControllers.SourceCollection.Cast<InstallControllerDTO>()
                                              .Any(
                                                  q =>
                                                      q.AircraftTypeId == SelAircraftType.Id && q.ItemId == SelItem.Id &&
                                                      q.PnRegId == p.Id)));
                }
                _addToInstallController = true;
                _addToDependency = false;
                pnRegsChildView.ShowDialog();
            }
            RefreshCommandState();
        }

        private bool CanNew(object obj)
        {
            if (SelItem != null && SelAircraftType != null)
                return true;
            return false;
        }

        #endregion

        #region 移除互换件

        /// <summary>
        ///     移除互换件
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            //如果要删除的装机控制中，附件只在这一条记录中维护了与附件项的关系，则在删除的同时，将件与项的关系断开
            if (InstallControllers.SourceCollection.Cast<InstallControllerDTO>()
                .Count(p => p.PnRegId == SelInstallController.PnRegId) == 1)
            {
                var pnReg =
                    PnRegs.SourceCollection.Cast<PnRegDTO>().FirstOrDefault(p => p.Id == SelInstallController.PnRegId);
                if (pnReg != null) pnReg.ItemId = null;
            }
            InstallControllers.Remove(SelInstallController);
            SelInstallController = InstallControllers.FirstOrDefault();
            RefreshCommandState();
        }

        private bool CanRemove(object obj)
        {
            if (SelInstallController != null)
                return true;
            return false;
        }

        #endregion

        #region 增加依赖项

        /// <summary>
        ///     增加依赖项
        /// </summary>
        public DelegateCommand<object> AddDependencyCommand { get; private set; }

        private void OnAddDependency(object obj)
        {
            if (SelInstallController == null)
                MessageAlert("提示", "请先选中装机控制项！");
            else
            {
                ViewPnRegs = new ObservableCollection<PnRegDTO>();
                ViewPnRegs.AddRange(PnRegs);
                _addToDependency = true;
                _addToInstallController = false;
                pnRegsChildView.ShowDialog();
            }
            RefreshCommandState();
        }

        private bool CanAddDependency(object obj)
        {
            if (SelInstallController == null)
                return false;
            return true;
        }

        #endregion

        #region 移除依赖项

        /// <summary>
        ///     移除依赖项
        /// </summary>
        public DelegateCommand<object> RemoveDependencyCommand { get; private set; }

        private void OnRemoveDependency(object obj)
        {
            SelInstallController.Dependencies.Remove(SelDependency);
            SelDependency = SelInstallController.Dependencies.FirstOrDefault();
            RefreshCommandState();
        }

        private bool CanRemoveDependency(object obj)
        {
            if (SelInstallController != null && SelDependency != null)
                return true;
            return false;
        }

        #endregion

        #endregion

        #region 子窗体相关

        [Import] public PnRegsChildView pnRegsChildView; //初始化子窗体

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            _addToInstallController = false;
            _addToDependency = false;
            pnRegsChildView.Close();
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
            if (SelItem != null && SelAircraftType != null && _addToInstallController)
            {
                radGridView.SelectedItems.ToList().ForEach(p =>
                {
                    var pnRegDto = p as PnRegDTO;
                    if (pnRegDto != null)
                    {
                        if (pnRegDto.ItemId == null)
                            pnRegDto.ItemId = SelItem.Id;
                        var installController = new InstallControllerDTO
                        {
                            Id = RandomHelper.Next(),
                            Pn = pnRegDto.Pn,
                            PnRegId = pnRegDto.Id,
                            ItemId = SelItem.Id,
                            ItemNo = SelItem.ItemNo,
                            ItemName = SelItem.Name,
                            Description = pnRegDto.Description,
                            AircraftTypeId = SelAircraftType.Id,
                            AircraftTypeName = SelAircraftType.Name,
                            StartDate = DateTime.Now,
                        };
                        InstallControllers.AddNew(installController);
                    }
                });
                _addToInstallController = false;
            }
            else if (SelInstallController != null && _addToDependency)
            {
                radGridView.SelectedItems.ToList().ForEach(p =>
                {
                    var pnRegDto = p as PnRegDTO;
                    if (pnRegDto != null)
                    {
                        var dependency = new DependencyDTO
                        {
                            Id = RandomHelper.Next(),
                            Pn = pnRegDto.Pn,
                            DependencyPnId = pnRegDto.Id,
                            InstallControllerId = SelInstallController.Id,
                        };
                        SelInstallController.Dependencies.Add(dependency);
                    }
                });
                _addToDependency = false;
            }
            pnRegsChildView.Close();
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