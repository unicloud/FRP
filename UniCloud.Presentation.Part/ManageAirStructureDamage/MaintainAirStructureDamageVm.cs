#region Version Info
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/2/28 14:44:24
// 文件名：MaintainAirStructureDamageVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/2/28 14:44:24
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.ManageAirStructureDamage
{
    [Export(typeof(MaintainAirStructureDamageVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MaintainAirStructureDamageVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private readonly DocumentDTO _document = new DocumentDTO();

        [ImportingConstructor]
        public MaintainAirStructureDamageVm(IRegionManager regionManager, IPartService service)
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
            // 创建并注册CollectionView
            AirStructureDamages = _service.CreateCollection(_context.AirStructureDamages);
            AirStructureDamages.PageSize = 20;
            _service.RegisterCollectionView(AirStructureDamages);
            AirStructureDamages.PropertyChanged += (sender, e) =>
            {
                if (e.PropertyName.Equals("IsAddingNew", StringComparison.OrdinalIgnoreCase))
                {
                    AirStructureDamage = AirStructureDamages.CurrentAddItem as AirStructureDamageDTO;
                    if (AirStructureDamage != null)
                    {
                        AirStructureDamage.Id = RandomHelper.Next();
                        var firstOrDefault = Aircrafts.FirstOrDefault();
                        if (firstOrDefault != null) AirStructureDamage.AircraftId = firstOrDefault.Id;
                        DocumentName = "添加附件";
                        _document.DocumentId = new Guid();
                        _document.Name = string.Empty;
                        AircraftDamageLevel = AircraftDamageLevel.低;
                        AirStructureReportType = AirStructureReportType.损伤;
                        AirStructureDamageStatus = AirStructureDamageStatus.草稿;
                    }
                }
                else if (e.PropertyName.Equals("HasChanges", StringComparison.OrdinalIgnoreCase))
                {
                    CanSelectAirStructureDamage = !AirStructureDamages.HasChanges;
                }
            };
            Aircrafts = new QueryableDataServiceCollectionView<AircraftDTO>(_context, _context.Aircrafts);
            var sort = new SortDescriptor { Member = "RegNumber", SortDirection = ListSortDirection.Ascending };
            Aircrafts.SortDescriptors.Add(sort);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 文档名称
        private string _documentName;
        public string DocumentName
        {
            get { return _documentName; }
            set
            {
                _documentName = value;
                RaisePropertyChanged("DocumentName");
            }
        }
        #endregion

        #region 报告种类
        private AirStructureReportType _airStructureReportType;
        public AirStructureReportType AirStructureReportType
        {
            get { return _airStructureReportType; }
            set
            {
                _airStructureReportType = value;
                AirStructureDamage.ReportType = (int)_airStructureReportType;
                RaisePropertyChanged(() => AirStructureReportType);
            }
        }
        public Array AirStructureReportTypes
        {
            get { return Enum.GetValues(typeof(AirStructureReportType)); }
        }
        #endregion

        #region 腐蚀级别
        private AircraftDamageLevel _aircraftDamageLevel;
        public AircraftDamageLevel AircraftDamageLevel
        {
            get { return _aircraftDamageLevel; }
            set
            {
                _aircraftDamageLevel = value;
                AirStructureDamage.Level = (int)_aircraftDamageLevel;
                RaisePropertyChanged(() => AircraftDamageLevel);
            }
        }
        public Array AircraftDamageLevels
        {
            get { return Enum.GetValues(typeof(AircraftDamageLevel)); }
        }
        #endregion

        #region 状态
        private AirStructureDamageStatus _airStructureDamageStatus;
        public AirStructureDamageStatus AirStructureDamageStatus
        {
            get { return _airStructureDamageStatus; }
            set
            {
                _airStructureDamageStatus = value;
                AirStructureDamage.Status = (int)_airStructureDamageStatus;
                RaisePropertyChanged(() => AirStructureDamageStatus);
            }
        }
        public Array AirStructureDamageStatuses
        {
            get { return Enum.GetValues(typeof(AirStructureDamageStatus)); }
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
            // 将CollectionView的AutoLoad属性设为True
            if (!AirStructureDamages.AutoLoad)
                AirStructureDamages.AutoLoad = true;
            AirStructureDamages.Load(true);
            Aircrafts.Load();
        }

        #region 结构损伤
        /// <summary>
        ///     结构损伤集合
        /// </summary>
        public QueryableDataServiceCollectionView<AirStructureDamageDTO> AirStructureDamages { get; set; }

        /// <summary>
        ///     选中的结构损伤
        /// </summary>
        private AirStructureDamageDTO _airStructureDamage;
        public AirStructureDamageDTO AirStructureDamage
        {
            get { return _airStructureDamage; }
            set
            {
                if (_airStructureDamage != value)
                {
                    _airStructureDamage = value;
                    if (_airStructureDamage != null)
                    {
                        _document.DocumentId = _airStructureDamage.DocumentId;
                        _document.Name = _airStructureDamage.DocumentName;
                        DocumentName = _airStructureDamage.DocumentName;
                        if (string.IsNullOrEmpty(DocumentName))
                        {
                            DocumentName = "添加附件";
                        }
                    }
                    RaisePropertyChanged(() => AirStructureDamage);
                }
            }
        }

        //用户能否选择
        private bool _canSelectAirStructureDamage = true;
        public bool CanSelectAirStructureDamage
        {
            get { return _canSelectAirStructureDamage; }
            set
            {
                if (_canSelectAirStructureDamage != value)
                {
                    _canSelectAirStructureDamage = value;
                    RaisePropertyChanged(() => CanSelectAirStructureDamage);
                }
            }
        }

        #endregion

        #region 飞机
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }
        private AircraftDTO _aircraft;
        /// <summary>
        ///     选中的飞机
        /// </summary>
        public AircraftDTO Aircraft
        {
            get { return _aircraft; }
            set
            {
                if (value != null && _aircraft != value)
                {
                    _aircraft = value;
                    AirStructureDamage.AircraftReg = _aircraft.RegNumber;
                    AirStructureDamage.AircraftType = _aircraft.AircraftType;
                    AirStructureDamage.AircraftSeries = _aircraft.AircraftSeries;
                    RaisePropertyChanged(() => Aircraft);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #region 操作

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
                AirStructureDamage.DocumentId = doc.DocumentId;
                AirStructureDamage.DocumentName = doc.Name;
                DocumentName = doc.Name;
            }
        }

        #endregion

        #region 重载操作

        #endregion

        #endregion
    }
}
