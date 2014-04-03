#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(AircraftPurchaseReceptionManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AircraftPurchaseReceptionManagerVM : ReceptionVm
    {
        #region 声明、初始化

        private readonly PurchaseData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;

        [ImportingConstructor]
        public AircraftPurchaseReceptionManagerVM(IRegionManager regionManager, IPurchaseService service)
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
            PurchaseContractAircrafts = new QueryableDataServiceCollectionView<PurchaseContractAircraftDTO>(_context,
                    _context.PurchaseContractAircrafts);

            AircraftPurchaseReceptions = _service.CreateCollection(_context.AircraftPurchaseReceptions.Expand(p => p.RelatedDocs),
                o => o.ReceptionLines, o => o.ReceptionSchedules, o => o.RelatedDocs);
            _service.RegisterCollectionView(AircraftPurchaseReceptions); //注册查询集合。
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
        }

        #endregion

        #region 数据

        #region 公共属性

        #region Rank号是否可编辑

        private bool _canEdit = true;

        /// <summary>
        ///     Rank号是否可编辑
        /// </summary>
        public bool CanEdit
        {
            get { return _canEdit; }
            private set
            {
                if (_canEdit != value)
                {
                    _canEdit = value;
                    RaisePropertyChanged(() => CanEdit);
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
            if (!AircraftPurchaseReceptions.AutoLoad)
                AircraftPurchaseReceptions.AutoLoad = true;
            else
                AircraftPurchaseReceptions.Load(true);

            PurchaseContractAircrafts.Load(true);
            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers));
        }

        #region 业务

        #region 购买飞机接收项目集合

        /// <summary>
        ///     购买飞机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftPurchaseReceptionDTO> AircraftPurchaseReceptions { get; set; }

        #endregion

        #region 购买合同飞机集合

        private ObservableCollection<PurchaseContractAircraftDTO> _viewContractAircrafts =
            new ObservableCollection<PurchaseContractAircraftDTO>();

        /// <summary>
        ///     所有购买合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PurchaseContractAircraftDTO> PurchaseContractAircrafts { get; set; }

        /// <summary>
        ///     购买合同飞机集合,用于界面绑定
        /// </summary>
        public ObservableCollection<PurchaseContractAircraftDTO> ViewPurchaseContractAircrafts
        {
            get { return _viewContractAircrafts; }
            set
            {
                if (_viewContractAircrafts != value)
                {
                    _viewContractAircrafts = value;
                    RaisePropertyChanged(() => ViewPurchaseContractAircrafts);
                }
            }
        }

        #endregion

        #region 选择的接收项目

        private AircraftPurchaseReceptionDTO _selAircraftPurchaseReception;

        /// <summary>
        ///     选择的接收项目
        /// </summary>
        public AircraftPurchaseReceptionDTO SelAircraftPurchaseReception
        {
            get { return _selAircraftPurchaseReception; }
            set
            {
                if (_selAircraftPurchaseReception != value)
                {
                    _selAircraftPurchaseReception = value;
                    RaisePropertyChanged(() => SelAircraftPurchaseReception);

                    var viewPurchaseContractAircrafts =
                        PurchaseContractAircrafts.Where(
                            p => p.SupplierId == SelAircraftPurchaseReception.SupplierId && p.SerialNumber != null)
                            .ToList();
                    ViewPurchaseContractAircrafts.Clear();
                    foreach (var lca in viewPurchaseContractAircrafts)
                    {
                        ViewPurchaseContractAircrafts.Add(lca);
                    }
                    //刷新界面日程控件绑定到的集合
                    _appointments.Clear();
                    foreach (var schedule in value.ReceptionSchedules)
                    {
                        Appointment appointment = ScheduleExtension.ConvertToAppointment(schedule);
                        _appointments.Add(appointment);
                    }
                    RaisePropertyChanged(() => Appointments);
                    //刷新界面按钮
                    RemoveCommand.RaiseCanExecuteChanged();
                    AddAttachCommand.RaiseCanExecuteChanged();
                    AddEntityCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region 选择的接收行

        private AircraftPurchaseReceptionLineDTO _selAircraftPurchaseReceptionLine;

        /// <summary>
        ///     选择的接收行
        /// </summary>
        public AircraftPurchaseReceptionLineDTO SelAircraftPurchaseReceptionLine
        {
            get { return _selAircraftPurchaseReceptionLine; }
            set
            {
                if (_selAircraftPurchaseReceptionLine != value)
                {
                    _selAircraftPurchaseReceptionLine = value;
                    RaisePropertyChanged(() => SelAircraftPurchaseReceptionLine);
                    // 刷新按钮状态
                    RemoveEntityCommand.RaiseCanExecuteChanged();
                }
            }
        }

        #endregion

        #region 交付日程

        private ObservableCollection<Appointment> _appointments = new ObservableCollection<Appointment>();

        /// <summary>
        ///     交付日程
        /// </summary>
        public ObservableCollection<Appointment> Appointments
        {
            get { return _appointments; }
            set
            {
                if (_appointments != value)
                {
                    _appointments = value;
                    RaisePropertyChanged(() => Appointments);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 重载操作

        #region 添加附件
        protected override bool CanAddAttach(object obj)
        {
            return _selAircraftPurchaseReception != null;
        }

        /// <summary>
        ///     子窗口关闭后执行的操作
        /// </summary>
        /// <param name="doc">添加的交付文件</param>
        /// <param name="sender">添加附件命令的参数</param>
        protected override void WindowClosed(DocumentDTO doc, object sender)
        {
            base.WindowClosed(doc, sender);

            var relatedDoc = new RelatedDocDTO
            {
                Id = RandomHelper.Next(),
                DocumentId = doc.DocumentId,
                DocumentName = doc.Name,
                SourceId = SelAircraftPurchaseReception.SourceId
            };
            SelAircraftPurchaseReception.RelatedDocs.Add(relatedDoc);
        }

        #endregion

        #region 移除附件

        /// <summary>
        ///     移除附件
        /// </summary>
        /// <param name="sender"></param>
        protected override void OnRemoveAttach(object sender)
        {
            var doc = sender as RelatedDocDTO;
            if (doc != null)
            {
                MessageConfirm("确认移除", "是否移除交机文档：" + doc.DocumentName + "？", (o, e) =>
                {
                    if (e.DialogResult == true)
                    {
                        SelAircraftPurchaseReception.RelatedDocs.Remove(doc);
                    }
                });
            }
        }

        #endregion

        #endregion

        #region 操作

        #region 新建接收项目

        /// <summary>
        ///     新建接收项目
        /// </summary>
        protected override void OnNew(object obj)
        {
            var recepiton = new AircraftPurchaseReceptionDTO
            {
                AircraftPurchaseReceptionId = RandomHelper.Next(),
                SourceId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
            };
            AircraftPurchaseReceptions.AddNew(recepiton);
        }

        protected override bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除接收项目

        /// <summary>
        ///     删除接收项目
        /// </summary>
        protected override void OnRemove(object obj)
        {
            if (SelAircraftPurchaseReception != null)
            {
                AircraftPurchaseReceptions.Remove(SelAircraftPurchaseReception);
            }
            var currentAircraftPurchaseReception = AircraftPurchaseReceptions.FirstOrDefault();
            if (currentAircraftPurchaseReception == null)
            {
                //删除完，若没有记录了，则也要删除界面明细
                Appointments.Clear();
            }
        }

        protected override bool CanRemove(object obj)
        {
            return _selAircraftPurchaseReception != null;
        }

        #endregion

        #region 新增接收行

        /// <summary>
        ///     新增接收行
        /// </summary>
        protected override void OnAddEntity(object obj)
        {
            var receptionLine = new AircraftPurchaseReceptionLineDTO
            {
                AircraftPurchaseReceptionLineId = RandomHelper.Next(),
                ReceivedAmount = 1,
                AcceptedAmount = 1,
                DeliverDate = DateTime.Now,
                ReceptionId = SelAircraftPurchaseReception.AircraftPurchaseReceptionId
            };
            SelAircraftPurchaseReception.ReceptionLines.Add(receptionLine);
        }

        protected override bool CanAddEntity(object obj)
        {
            return SelAircraftPurchaseReception != null;
        }

        #endregion

        #region 删除接收行

        /// <summary>
        ///     删除接收行
        /// </summary>
        protected override void OnRemoveEntity(object obj)
        {
            if (_selAircraftPurchaseReceptionLine != null)
            {
                SelAircraftPurchaseReception.ReceptionLines.Remove(SelAircraftPurchaseReceptionLine);
            }
        }

        protected override bool CanRemoveEntity(object obj)
        {
            return _selAircraftPurchaseReceptionLine != null;
        }

        #endregion

        #region GridView单元格变更处理

        /// <summary>
        ///     GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        protected override void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "Supplier"))
                {
                    var viewPurchaseContractAircrafts =
                        PurchaseContractAircrafts.Where(
                            p => p.SupplierId == SelAircraftPurchaseReception.SupplierId && p.SerialNumber != null)
                            .ToList();
                    ViewPurchaseContractAircrafts.Clear();
                    foreach (var lca in viewPurchaseContractAircrafts)
                    {
                        ViewPurchaseContractAircrafts.Add(lca);
                    }
                }
                else if (string.Equals(cell.Column.UniqueName, "ContractAircraft"))
                {
                    var value = SelAircraftPurchaseReceptionLine.ContractAircraftId;
                    var contractAircraft =
                        ViewPurchaseContractAircrafts.FirstOrDefault(p => p.PurchaseContractAircraftId == value);
                    if (contractAircraft != null)
                    {
                        SelAircraftPurchaseReceptionLine.ContractName = contractAircraft.ContractName;
                        SelAircraftPurchaseReceptionLine.ContractNumber = contractAircraft.ContractNumber;
                        SelAircraftPurchaseReceptionLine.RankNumber = contractAircraft.RankNumber;
                        SelAircraftPurchaseReceptionLine.MSN = contractAircraft.SerialNumber;
                        SelAircraftPurchaseReceptionLine.AircraftType = contractAircraft.AircraftTypeName;
                    }
                }
            }
        }

        #endregion

        #region ScheduleView新增处理

        protected override void OnCreated(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = scheduleView.EditedAppointment as Appointment;
                var schedule = ScheduleExtension.ConvertToReceptionSchedule(appointment);
                schedule.ReceptionScheduleId = RandomHelper.Next();
                schedule.ReceptionId = SelAircraftPurchaseReception.AircraftPurchaseReceptionId;
                SelAircraftPurchaseReception.ReceptionSchedules.Add(schedule);
            }
        }

        #endregion

        #region ScheduleView删除处理

        protected override void OnDeleted(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = scheduleView.EditedAppointment as Appointment;
                if (appointment != null)
                {
                    var schedule =
                        SelAircraftPurchaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.ReceptionScheduleId == int.Parse(appointment.UniqueId));
                    SelAircraftPurchaseReception.ReceptionSchedules.Remove(schedule);
                }
            }
        }

        #endregion

        #region ScheduleView编辑处理

        protected override void OnEdited(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = scheduleView.EditedAppointment as Appointment;
                if (appointment != null)
                {
                    var schedule =
                        SelAircraftPurchaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.ReceptionScheduleId == int.Parse(appointment.UniqueId));
                    SelAircraftPurchaseReception.ReceptionSchedules.Remove(schedule);
                    if (schedule != null)
                    {
                        schedule = ScheduleExtension.ConvertToReceptionSchedule(appointment);
                        SelAircraftPurchaseReception.ReceptionSchedules.Add(schedule);
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}