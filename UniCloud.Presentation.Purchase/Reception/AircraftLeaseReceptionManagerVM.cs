#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ChartView;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public class AircraftLeaseReceptionManagerVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;
        private CategoryCollection _categories;
        private TimeMarkerCollection _timeMarkers;
        private ResourceTypeCollection workGroups;
        private Service.Purchase.SchdeuleExtension.ControlExtension scheduleExtension;
        private Document.Document _document = new Document.Document();

        public WordViewer WordView;
        public PDFViewer PdfView;

        [ImportingConstructor]
        public AircraftLeaseReceptionManagerVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            InitializeVM();
            InitializerCommand();
            scheduleExtension = new Service.Purchase.SchdeuleExtension.ControlExtension();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            AircraftContracts = Service.CreateCollection<APUMaintainContractDTO>(_purchaseData.APUMaintainContracts);
            Service.RegisterCollectionView(AircraftContracts); //注册查询集合。
            AircraftContracts.PropertyChanged += OnViewPropertyChanged;

            LeaseContractAircrafts = Service.CreateCollection<LeaseContractAircraftDTO>(_purchaseData.LeaseContractAircrafts);
            Service.RegisterCollectionView(LeaseContractAircrafts); //注册查询集合。
            LeaseContractAircrafts.PropertyChanged += OnViewPropertyChanged;

            AircraftLeaseReceptions = Service.CreateCollection<AircraftLeaseReceptionDTO>(_purchaseData.AircraftLeaseReceptions);
            Service.RegisterCollectionView(AircraftLeaseReceptions); //注册查询集合。
            AircraftLeaseReceptions.PropertyChanged += OnViewPropertyChanged;

            Suppliers = Service.CreateCollection<SupplierDTO>(_purchaseData.Suppliers);
            Service.RegisterCollectionView(Suppliers); //注册查询集合。
            Suppliers.PropertyChanged += OnViewPropertyChanged;

            RelatedDocs = Service.CreateCollection<RelatedDocDTO>(_purchaseData.RelatedDocs);
            Service.RegisterCollectionView(RelatedDocs); //注册查询集合。
            RelatedDocs.PropertyChanged += OnViewPropertyChanged;
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
            //GridView单元格值变更
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
            //移除文档
            RemoveAttachCommand = new DelegateCommand<object>(OnRemoveAttach);
            //ScheduleView
            CreateCommand = new DelegateCommand<object>(OnCreated);
            EditCommand = new DelegateCommand<object>(OnEdited);
            DelCommand = new DelegateCommand<object>(OnDeleted);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _purchaseData = new PurchaseData(AgentHelper.PurchaseUri);
            return new PurchaseService(_purchaseData);
        }

        #endregion

        #region 数据

        #region 公共属性

        public CategoryCollection Categories
        {
            get
            {
                if (this._categories == null)
                {
                    this._categories = new CategoryCollection
                    {
                        new Category("已完成", new SolidColorBrush(Colors.Green)),
                        new Category("正在进行中…", new SolidColorBrush(Colors.Brown)),
                        new Category("未启动", new SolidColorBrush(Colors.Gray))
                    };
                }
                return this._categories;
            }
        }

        public ResourceTypeCollection WorkGroups
        {
            get
            {
                var resourceType = new ResourceTypeCollection();
                if (this.workGroups == null)
                {
                    var reType = new ResourceType();
                    reType.Resources.Add(new Resource("机务组", "工作组"));
                    reType.Resources.Add(new Resource("机队管理组", "工作组"));
                    reType.Resources.Add(new Resource("后勤组", "工作组"));
                    reType.Resources.Add(new Resource("其他", "工作组"));
                    resourceType.Add(reType);
                }
                return resourceType;
            }
        }

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
            RelatedDocs.AutoLoad = true;
            AircraftContracts.AutoLoad = true;
            LeaseContractAircrafts.AutoLoad = true;
            AircraftLeaseReceptions.AutoLoad = true;
            Suppliers.AutoLoad = true;
        }

        #region 业务

        #region 租赁飞机接收项目集合
        /// <summary>
        ///     租赁飞机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftLeaseReceptionDTO> AircraftLeaseReceptions { get; set; }

        #endregion

        #region 关联文档集合
        /// <summary>
        ///     关联文档集合
        /// </summary>
        public QueryableDataServiceCollectionView<RelatedDocDTO> RelatedDocs { get; set; }

        #endregion

        #region 供应商

        /// <summary>
        ///     供应商
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }
        #endregion

        #region 飞机租赁交易集合
        /// <summary>
        ///     飞机租赁合同集合
        /// </summary>
        public QueryableDataServiceCollectionView<APUMaintainContractDTO> AircraftContracts { get; set; }

        private List<APUMaintainContractDTO> _viewAircraftContracts;
        public List<APUMaintainContractDTO> ViewAircraftContracts
        {
            get { return _viewAircraftContracts; }
            set
            {
                if (_viewAircraftContracts != value)
                {
                    _viewAircraftContracts = value;
                    RaisePropertyChanged(() => ViewAircraftContracts);
                }
            }
        }

        /// <summary>
        ///     飞机租赁交易集合
        /// </summary>
        public QueryableDataServiceCollectionView<TradeDTO> Trades { get; set; }

        private List<TradeDTO> _viewTrades;
        public List<TradeDTO> ViewTrades
        {
            get { return _viewTrades; }
            set
            {
                if (_viewTrades != value)
                {
                    _viewTrades = value;
                    RaisePropertyChanged(() => ViewTrades);
                }
            }
        }

        public QueryableDataServiceCollectionView<AircraftLeaseOrderDTO> AircraftLeaseOrders { get; set; }

        private List<AircraftLeaseOrderDTO> _viewAircraftLeaseOrders;
        public List<AircraftLeaseOrderDTO> ViewAircraftLeaseOrders
        {
            get { return _viewAircraftLeaseOrders; }
            set
            {
                if (_viewAircraftLeaseOrders != value)
                {
                    _viewAircraftLeaseOrders = value;
                    RaisePropertyChanged(() => ViewAircraftLeaseOrders);
                }
            }
        }
        #endregion

        #region 租赁合同飞机集合
        /// <summary>
        ///     飞机租赁合同集合
        /// </summary>
        public QueryableDataServiceCollectionView<LeaseContractAircraftDTO> LeaseContractAircrafts { get; set; }

        #endregion

        #region 选择的接收项目

        private AircraftLeaseReceptionDTO _selAircraftLeaseReception;

        /// <summary>
        ///     选择的接收项目
        /// </summary>
        public AircraftLeaseReceptionDTO SelAircraftLeaseReception
        {
            get { return _selAircraftLeaseReception; }
            set
            {
                if (_selAircraftLeaseReception != value)
                {
                    _selAircraftLeaseReception = value;
                    ViewAircraftContracts = AircraftContracts.Where(p => p.SignatoryId == SelAircraftLeaseReception.SupplierId).ToList();
                    _appointments.Clear();
                    foreach (var schedule in value.ReceptionSchedules)
                    {
                        Appointment appointment = scheduleExtension.ConvertToAppointment(schedule);
                        _appointments.Add(appointment);
                    }
                    ViewDocuments = RelatedDocs.Where(l => l.SourceId == SelAircraftLeaseReception.SourceId).ToList();
                    RaisePropertyChanged(()=>Appointments);
                    RaisePropertyChanged(() => SelAircraftLeaseReception);
                }
            }
        }

        #endregion

        #region 租赁飞机接收行

        private ObservableCollection<AircraftLeaseReceptionLineDTO> _aircraftLeaseReceptionLines;

        /// <summary>
        ///     租赁飞机接收行
        /// </summary>
        public ObservableCollection<AircraftLeaseReceptionLineDTO> AircraftReceptionLines
        {
            get { return _aircraftLeaseReceptionLines; }
            private set
            {
                if (_aircraftLeaseReceptionLines != value)
                {
                    _aircraftLeaseReceptionLines = value;
                    RaisePropertyChanged(() => AircraftReceptionLines);
                }
            }
        }

        #endregion

        #region 选择的接收行

        private AircraftLeaseReceptionLineDTO _selAircraftLeaseReceptionLine;

        /// <summary>
        ///     选择的接收行
        /// </summary>
        public AircraftLeaseReceptionLineDTO SelAircraftLeaseReceptionLine
        {
            get { return _selAircraftLeaseReceptionLine; }
            set
            {
                if (_selAircraftLeaseReceptionLine != value)
                {
                    _selAircraftLeaseReceptionLine = value;
                    RaisePropertyChanged(() => SelAircraftLeaseReceptionLine);
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

        #region 交机文件

        private List<RelatedDocDTO> _viewDocuments;

        /// <summary>
        /// 交机文件
        /// </summary>
        public List<RelatedDocDTO> ViewDocuments
        {
            get { return this._viewDocuments; }
            private set
            {
                if (this._viewDocuments != value)
                {
                    _viewDocuments = value;
                    this.RaisePropertyChanged(() => this.ViewDocuments);
                }
            }
        }

        #endregion

        #region 选择的交机文件

        private RelatedDocDTO _selDocument;

        /// <summary>
        /// 选择的交机文件
        /// </summary>
        public RelatedDocDTO SelDocument
        {
            get { return this._selDocument; }
            private set
            {
                if (this._selDocument != value)
                {
                    this._selDocument = value;
                    this.RaisePropertyChanged(() => this.SelDocument);
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
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var recepiton = new AircraftLeaseReceptionDTO
            {
                AircraftLeaseReceptionId = RandomHelper.Next(),
                SourceId = Guid.NewGuid(),
                CreateDate = DateTime.Now
            };
            AircraftLeaseReceptions.AddNew(recepiton);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除接收项目

        /// <summary>
        ///     删除接收项目
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            AircraftLeaseReceptions.Remove(SelAircraftLeaseReception);
        }

        private bool CanRemove(object obj)
        {
            bool canRemove;
            if (SelAircraftLeaseReception != null)
                canRemove = true;
            else if (AircraftLeaseReceptions != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion

        #region 新增接收行
        /// <summary>
        ///     新增接收行
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            var receptionLine = new AircraftLeaseReceptionLineDTO()
            {
                AircraftLeaseReceptionLineId = RandomHelper.Next(),
                ReceptionId = SelAircraftLeaseReception.AircraftLeaseReceptionId
            };
            SelAircraftLeaseReception.ReceptionLines.Add(receptionLine);
        }

        private bool CanAddEntity(object obj)
        {
            return true;
        }
        #endregion

        #region 删除接收行

        /// <summary>
        ///     删除接收行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            SelAircraftLeaseReception.ReceptionLines.Remove(SelAircraftLeaseReceptionLine);
        }

        private bool CanRemoveEntity(object obj)
        {
            bool canRemove;
            if (SelAircraftLeaseReception != null && SelAircraftLeaseReceptionLine != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            var relatedDoc = new RelatedDocDTO()
            {
                SourceId = SelAircraftLeaseReception.SourceId,
                DocumentId = Guid.NewGuid(),
                DocumentName = "测试文档名称",
            };
            RelatedDocs.AddNew(relatedDoc);
            ViewDocuments.Add(relatedDoc);
            //var radRadioButton = sender as RadRadioButton;
            //if (true)
            //{
            //    WordView.Tag = null;
            //    WordView.ViewModel.InitData(false, _document, WordViewerClosed);
            //    WordView.ShowDialog();
            //}
            //else
            //{
            //    PdfView.Tag = null;
            //    PdfView.ViewModel.InitData(false, _document, PdfViewerClosed);
            //    PdfView.ShowDialog();
            //}
        }

        private void WordViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (WordView.Tag != null && WordView.Tag is Document.Document)
            {
                var document = WordView.Tag as Document.Document;
                //ApuMaintainContract.DocumentId = document.Id;
                //ApuMaintainContract.DocumentName = document.Name;
            }
        }

        private void PdfViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (PdfView.Tag != null && PdfView.Tag is Document.Document)
            {
                var document = PdfView.Tag as Document.Document;
                //ApuMaintainContract.DocumentId = document.Id;
                //ApuMaintainContract.DocumentName = document.Name;
            }
        }
        #endregion

        #region 移除附件

        public DelegateCommand<object> RemoveAttachCommand { get; set; }

        /// <summary>
        ///     移除附件
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnRemoveAttach(object sender)
        {
            if (SelDocument == null)
            {
                MessageBox.Show("没有选中的文档!");
            }
            else
            {
                RelatedDocs.Remove(SelDocument);
                ViewDocuments.Remove(SelDocument);
            }
        }

        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            if (string.IsNullOrEmpty(_document.Name))
            {
                return;
            }
            if (_document.Name.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
            {
                PdfView.Tag = null;
                PdfView.ViewModel.InitData(true, _document, PdfViewerClosed);
                PdfView.ShowDialog();
            }
            else
            {
                WordView.Tag = null;
                WordView.ViewModel.InitData(true, _document, WordViewerClosed);
                WordView.ShowDialog();
            }
        }
        #endregion

        #region GridView单元格变更处理
        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        /// GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        public void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "Supplier"))
                {
                    ViewAircraftContracts = AircraftContracts.Where(p => p.SignatoryId == SelAircraftLeaseReception.SupplierId).ToList();
                }
                else if (string.Equals(cell.Column.UniqueName, "ContractNumber"))
                {
                    if (SelAircraftLeaseReceptionLine.ContractNumber != null)
                    {
                        CanEdit = false;
                    }
                  
                }
                else if (string.Equals(cell.Column.UniqueName, "ContractAircraft"))
                {
                    var value = SelAircraftLeaseReceptionLine.ContractAircraftId;
                    //var contractAircraft =
                    //    SelAircraftLeaseReceptionLine.ViewLeaseContractAircrafts.FirstOrDefault(p => p.LeaseContractAircraftId == value);
                    //if (contractAircraft != null)
                    //{
                    //    SelAircraftLeaseReceptionLine.ContractNumber = contractAircraft.ContractNumber;
                    //    SelAircraftLeaseReceptionLine.MSN = contractAircraft.SerialNumber;
                    //    SelAircraftLeaseReceptionLine.AircraftType = contractAircraft.AircraftTypeName;
                    //}
                }
            }
        }


        #endregion

        #region ScheduleView新增处理
        public DelegateCommand<object> CreateCommand { set; get; }

        public void OnCreated(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
        {
                var appointment = scheduleView.EditedAppointment as Appointment;
                var schedule = scheduleExtension.ConvertToReceptionSchedule(appointment);
                schedule.ReceptionId = SelAircraftLeaseReception.AircraftLeaseReceptionId;
                SelAircraftLeaseReception.ReceptionSchedules.Add(schedule);
            }


        }

        #endregion

        #region ScheduleView删除处理
        public DelegateCommand<object> DelCommand { set; get; }

        public void OnDeleted(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = scheduleView.EditedAppointment as Appointment;
                if (appointment != null)
        {
                    var schedule =
                        SelAircraftLeaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.UniqueId == appointment.UniqueId);
                    SelAircraftLeaseReception.ReceptionSchedules.Remove(schedule);
                }
            }
        }

        #endregion

        #region ScheduleView编辑处理
        public DelegateCommand<object> EditCommand { set; get; }

        public void OnEdited(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = scheduleView.EditedAppointment as Appointment;
                if (appointment != null)
                {
                    var schedule =
                        SelAircraftLeaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.UniqueId == appointment.UniqueId);
                    SelAircraftLeaseReception.ReceptionSchedules.Remove(schedule);
                    if (schedule != null)
        {
                        schedule = scheduleExtension.ConvertToReceptionSchedule(appointment);
                        SelAircraftLeaseReception.ReceptionSchedules.Add(schedule);
                    }
                }
            }

        }

        #endregion
        #endregion
    }
}