#region NameSpace

using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.BaseConfigurations
{
    /// <summary>
    /// 基础配置，包括维修工作单元、控制单位的维护
    /// </summary>
    [Export(typeof (BaseConfigurationVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class BaseConfigurationVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;

        [ImportingConstructor]
        public BaseConfigurationVm(IRegionManager regionManager, IPartService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            //创建并注册CollectionView
            MaintainWorks = _service.CreateCollection(_context.MaintainWorks);
            _service.RegisterCollectionView(MaintainWorks);
            MaintainWorks.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = MaintainWorks.CurrentAddItem as MaintainWorkDTO;
                    if (newItem != null)
                    {
                        newItem.Id = RandomHelper.Next();
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectMaintainWork = !MaintainWorks.HasChanges;
                }
            };

            //创建并注册CollectionView
            CtrlUnits = _service.CreateCollection(_context.CtrlUnits);
            _service.RegisterCollectionView(CtrlUnits);
            CtrlUnits.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName == "IsAddingNew")
                {
                    var newItem = CtrlUnits.CurrentAddItem as CtrlUnitDTO;
                    if (newItem != null)
                    {
                        newItem.Id = RandomHelper.Next();
                    }
                }
                else if (e.PropertyName == "HasChanges")
                {
                    CanSelectCtrlUnit = !CtrlUnits.HasChanges;
                }
            };
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
            //// 将CollectionView的AutoLoad属性设为True
            if (!MaintainWorks.AutoLoad)
                MaintainWorks.AutoLoad = true;
            MaintainWorks.Load(true);

            //// 将CollectionView的AutoLoad属性设为True
            if (!CtrlUnits.AutoLoad)
                CtrlUnits.AutoLoad = true;
            CtrlUnits.Load(true);
        }

        #region 工作代码

        private bool _canSelectMaintainWork = true;
        private MaintainWorkDTO _maintainWork;

        /// <summary>
        ///     工作代码集合
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainWorkDTO> MaintainWorks { get; set; }

        /// <summary>
        ///     选中的工作代码
        /// </summary>
        public MaintainWorkDTO MaintainWork
        {
            get { return _maintainWork; }
            set
            {
                if (value != null && _maintainWork != value)
                {
                    _maintainWork = value;
                    RaisePropertyChanged(() => MaintainWork);
                }
            }
        }

        //用户能否选择
        public bool CanSelectMaintainWork
        {
            get { return _canSelectMaintainWork; }
            set
            {
                if (_canSelectMaintainWork != value)
                {
                    _canSelectMaintainWork = value;
                    RaisePropertyChanged(() => CanSelectMaintainWork);
                }
            }
        }

        #endregion

        #region 控制单位

        private bool _canSelectCtrlUnit = true;
        private CtrlUnitDTO _ctrlUnit;

        /// <summary>
        ///     控制单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<CtrlUnitDTO> CtrlUnits { get; set; }

        /// <summary>
        ///     选中的控制单位
        /// </summary>
        public CtrlUnitDTO CtrlUnit
        {
            get { return _ctrlUnit; }
            set
            {
                if (value != null && _ctrlUnit != value)
                {
                    _ctrlUnit = value;
                    RaisePropertyChanged(() => CtrlUnit);
                }
            }
        }

        //用户能否选择
        public bool CanSelectCtrlUnit
        {
            get { return _canSelectCtrlUnit; }
            set
            {
                if (_canSelectCtrlUnit != value)
                {
                    _canSelectCtrlUnit = value;
                    RaisePropertyChanged(() => CanSelectCtrlUnit);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 重载操作

        #endregion

        #endregion
    }
}