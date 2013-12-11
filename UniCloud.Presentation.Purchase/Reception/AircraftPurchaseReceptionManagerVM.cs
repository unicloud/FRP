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
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof(AircraftPurchaseReceptionManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AircraftPurchaseReceptionManagerVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;
        private CategoryCollection _categories;
        private TimeMarkerCollection _timeMarkers;
        private ResourceTypeCollection workGroups;
        private Service.Purchase.SchdeuleExtension.ControlExtension scheduleExtension;
        private Document.Document _document = new Document.Document();

        [Import]
        public WordViewer WordView;
        [Import]
        public PDFViewer PdfView;

        [ImportingConstructor]
        public AircraftPurchaseReceptionManagerVM(IRegionManager regionManager)
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
            RelatedDocs = Service.CreateCollection<RelatedDocDTO>(_purchaseData.RelatedDocs);
            Service.RegisterCollectionView(RelatedDocs); //注册查询集合。
            RelatedDocs.PropertyChanged += OnViewPropertyChanged;

            PurchaseContractAircrafts = Service.CreateCollection<PurchaseContractAircraftDTO>(_purchaseData.PurchaseContractAircrafts);
            Service.RegisterCollectionView(PurchaseContractAircrafts); //注册查询集合。
            PurchaseContractAircrafts.PropertyChanged += OnViewPropertyChanged;

            Suppliers = Service.CreateCollection<SupplierDTO>(_purchaseData.Suppliers);
            Service.RegisterCollectionView(Suppliers); //注册查询集合。
            Suppliers.PropertyChanged += OnViewPropertyChanged;

            AircraftPurchaseReceptions = Service.CreateCollection<AircraftPurchaseReceptionDTO>(_purchaseData.AircraftPurchaseReceptions);
            Service.RegisterCollectionView(AircraftPurchaseReceptions); //注册查询集合。
            AircraftPurchaseReceptions.PropertyChanged += OnViewPropertyChanged;
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
            //文档
            AddWordAttachCommand = new DelegateCommand<object>(OnAddWordAttach);
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
            PurchaseContractAircrafts.AutoLoad = true;
            Suppliers.AutoLoad = true;
            AircraftPurchaseReceptions.AutoLoad = true;
        }

        #region 业务

        #region 购买飞机接收项目集合
        /// <summary>
        ///     购买飞机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftPurchaseReceptionDTO> AircraftPurchaseReceptions { get; set; }

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

                    var viewPurchaseContractAircrafts = PurchaseContractAircrafts.Where(p => p.SupplierId == SelAircraftPurchaseReception.SupplierId && p.SerialNumber != null).ToList();
                    ViewPurchaseContractAircrafts.Clear();
                    foreach (var lca in viewPurchaseContractAircrafts)
                    { 
                        ViewPurchaseContractAircrafts.Add(lca);
                    }
                    _appointments.Clear();
                    foreach (var schedule in value.ReceptionSchedules)
                    {
                        Appointment appointment = scheduleExtension.ConvertToAppointment(schedule);
                        _appointments.Add(appointment);
                    }
                    var viewDocuments = RelatedDocs.Where(l => l.SourceId == SelAircraftPurchaseReception.SourceId).ToList();
                    ViewDocuments.Clear();
                    foreach (var doc in viewDocuments)
                    {
                        ViewDocuments.Add(doc);
                    }
                    RaisePropertyChanged(()=>Appointments);
                    RaisePropertyChanged(() => SelAircraftPurchaseReception);
                }
            }
        }

        #endregion

        #region 购买飞机接收行

        private ObservableCollection<AircraftPurchaseReceptionLineDTO> _aircraftPurchaseReceptionLines;

        /// <summary>
        ///     购买飞机接收行
        /// </summary>
        public ObservableCollection<AircraftPurchaseReceptionLineDTO> AircraftReceptionLines
        {
            get { return _aircraftPurchaseReceptionLines; }
            private set
            {
                if (_aircraftPurchaseReceptionLines != value)
                {
                    _aircraftPurchaseReceptionLines = value;
                    RaisePropertyChanged(() => AircraftReceptionLines);
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

        private ObservableCollection<RelatedDocDTO> _viewDocuments=new ObservableCollection<RelatedDocDTO>();

        /// <summary>
        /// 交机文件
        /// </summary>
        public ObservableCollection<RelatedDocDTO> ViewDocuments
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
            var recepiton = new AircraftPurchaseReceptionDTO
            {
                AircraftPurchaseReceptionId = RandomHelper.Next(),
                SourceId = Guid.NewGuid(),
                CreateDate = DateTime.Now
            };
            AircraftPurchaseReceptions.AddNew(recepiton);
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
            AircraftPurchaseReceptions.Remove(SelAircraftPurchaseReception);
        }

        private bool CanRemove(object obj)
        {
            bool canRemove;
            if (SelAircraftPurchaseReception != null)
                canRemove = true;
            else if (AircraftPurchaseReceptions != null)
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
            var receptionLine = new AircraftPurchaseReceptionLineDTO()
            {
                AircraftPurchaseReceptionLineId = RandomHelper.Next(),
                ReceptionId = SelAircraftPurchaseReception.AircraftPurchaseReceptionId
            };
            SelAircraftPurchaseReception.ReceptionLines.Add(receptionLine);
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
            SelAircraftPurchaseReception.ReceptionLines.Remove(SelAircraftPurchaseReceptionLine);
        }

        private bool CanRemoveEntity(object obj)
        {
            bool canRemove;
            if (SelAircraftPurchaseReception != null && SelAircraftPurchaseReceptionLine != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
        }
        #endregion

        #region 添加附件
        /// <summary>
        ///     添加Pdf附件
        /// </summary>
        protected override void OnAddAttach(object sender)
        {
            PdfView.Tag = null;
            PdfView.ViewModel.InitData(false, _document, PdfViewerClosed);
            PdfView.ShowDialog();
        }

        /// <summary>
        ///     添加Word附件
        /// </summary>
        public DelegateCommand<object> AddWordAttachCommand { get; private set; }

        private void OnAddWordAttach(object sender)
        {
            WordView.Tag = null;
            WordView.ViewModel.InitData(false, _document, PdfViewerClosed);
            WordView.ShowDialog();
        }
        private void WordViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (WordView.Tag != null && WordView.Tag is Document.Document)
            {
                var relatedDoc = new RelatedDocDTO()
                {
                    SourceId = SelAircraftPurchaseReception.SourceId,
                };
                var document = WordView.Tag as Document.Document;
                relatedDoc.DocumentId = document.Id;
                relatedDoc.DocumentName = document.Name;
                RelatedDocs.AddNew(relatedDoc);
                ViewDocuments.Add(relatedDoc);
            }
        }

        private void PdfViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (PdfView.Tag != null && PdfView.Tag is Document.Document)
            {
                var relatedDoc = new RelatedDocDTO()
                {
                    SourceId = SelAircraftPurchaseReception.SourceId,
                };
                var document = PdfView.Tag as Document.Document;
                relatedDoc.DocumentId = document.Id;
                relatedDoc.DocumentName = document.Name;
                RelatedDocs.AddNew(relatedDoc);
                ViewDocuments.Add(relatedDoc);
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
                    var viewPurchaseContractAircrafts = PurchaseContractAircrafts.Where(p => p.SupplierId == SelAircraftPurchaseReception.SupplierId && p.SerialNumber!=null).ToList();
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
        public DelegateCommand<object> CreateCommand { set; get; }

        public void OnCreated(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
        {
                var appointment = scheduleView.EditedAppointment as Appointment;
                var schedule = scheduleExtension.ConvertToReceptionSchedule(appointment);
                schedule.ReceptionId = SelAircraftPurchaseReception.AircraftPurchaseReceptionId;
                SelAircraftPurchaseReception.ReceptionSchedules.Add(schedule);
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
                        SelAircraftPurchaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.UniqueId == appointment.UniqueId);
                    SelAircraftPurchaseReception.ReceptionSchedules.Remove(schedule);
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
                        SelAircraftPurchaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.UniqueId == appointment.UniqueId);
                    SelAircraftPurchaseReception.ReceptionSchedules.Remove(schedule);
                    if (schedule != null)
        {
                        schedule = scheduleExtension.ConvertToReceptionSchedule(appointment);
                        SelAircraftPurchaseReception.ReceptionSchedules.Add(schedule);
                    }
                }
            }

        }

        #endregion
        #endregion
    }
}