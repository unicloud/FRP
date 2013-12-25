#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
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

        private readonly IRegionManager _regionManager;
        private readonly DocumentDTO _document = new DocumentDTO();

        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public AircraftPurchaseReceptionManagerVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
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

            PurchaseContractAircrafts = new QueryableDataServiceCollectionView<PurchaseContractAircraftDTO>(PurchaseDataService, PurchaseDataService.PurchaseContractAircrafts);

            AircraftPurchaseReceptions = Service.CreateCollection<AircraftPurchaseReceptionDTO>(PurchaseDataService.AircraftPurchaseReceptions.Expand(p => p.RelatedDocs));
            Service.RegisterCollectionView(AircraftPurchaseReceptions); //注册查询集合。
            AircraftPurchaseReceptions.PropertyChanged += OnViewPropertyChanged;
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
        /// Rank号是否可编辑
        /// </summary>
        public bool CanEdit
        {
            get { return this._canEdit; }
            private set
            {
                if (this._canEdit != value)
                {
                    _canEdit = value;
                    this.RaisePropertyChanged(() => this.CanEdit);
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
            PurchaseContractAircrafts.Load(true);
            Suppliers.Load(true);
            AircraftPurchaseReceptions.Load(true);
        }

        #region 业务

        #region 购买飞机接收项目集合
        /// <summary>
        ///     购买飞机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftPurchaseReceptionDTO> AircraftPurchaseReceptions { get; set; }

        #endregion

        #region 购买合同飞机集合
        /// <summary>
        ///     所有购买合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PurchaseContractAircraftDTO> PurchaseContractAircrafts { get; set; }


        private ObservableCollection<PurchaseContractAircraftDTO> _viewContractAircrafts = new ObservableCollection<PurchaseContractAircraftDTO>();

        /// <summary>
        /// 购买合同飞机集合,用于界面绑定
        /// </summary>
        public ObservableCollection<PurchaseContractAircraftDTO> ViewPurchaseContractAircrafts
        {
            get { return this._viewContractAircrafts; }
            set
            {
                if (this._viewContractAircrafts != value)
                {
                    _viewContractAircrafts = value;
                    this.RaisePropertyChanged(() => this.ViewPurchaseContractAircrafts);
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

                    var viewPurchaseContractAircrafts = PurchaseContractAircrafts.Where(p => p.SupplierId == SelAircraftPurchaseReception.SupplierId && p.SerialNumber != null).ToList();
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
                if (this._appointments != value)
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
            var receptionLine = new AircraftPurchaseReceptionLineDTO()
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

        #region 添加附件
        /// <summary>
        ///     添加附件
        /// </summary>
        protected override void OnAddAttach(object sender)
        {
            DocumentView.ViewModel.InitData(false, _document.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                var relatedDoc = new RelatedDocDTO()
                {
                    Id = RandomHelper.Next(),
                    SourceId = SelAircraftPurchaseReception.SourceId,
                };
                var document = DocumentView.Tag as DocumentDTO;
                relatedDoc.DocumentId = document.DocumentId;
                relatedDoc.DocumentName = document.Name;
                SelAircraftPurchaseReception.RelatedDocs.Add(relatedDoc);
            }
        }
        #endregion

        #region 移除附件
        /// <summary>
        ///     移除附件
        /// </summary>
        /// <param name="sender"></param>
        protected override void OnRemoveAttach(object sender)
        {
            var currentItem = sender as RelatedDocDTO;
            if (currentItem == null)
            {
                MessageBox.Show("没有选中的文档!");
                return;
            }
            SelAircraftPurchaseReception.RelatedDocs.Remove(currentItem);
        }

        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            var currentItem = sender as RelatedDocDTO;
            if (currentItem == null)
            {
                MessageBox.Show("没有选中的文档!");
                return;
            }
            DocumentView.ViewModel.InitData(true, currentItem.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }
        #endregion

        #region GridView单元格变更处理
        /// <summary>
        /// GridView单元格变更处理
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
                    var viewPurchaseContractAircrafts = PurchaseContractAircrafts.Where(p => p.SupplierId == SelAircraftPurchaseReception.SupplierId && p.SerialNumber != null).ToList();
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