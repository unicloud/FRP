#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/11/20 15:19:47
// 文件名：EnginePurchaseReceptionManagerVM
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
    [Export(typeof(EnginePurchaseReceptionManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EnginePurchaseReceptionManagerVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;
        private CategoryCollection _categories;
        private TimeMarkerCollection _timeMarkers;
        private ResourceTypeCollection workGroups;
        private Service.Purchase.SchdeuleExtension.ControlExtension scheduleExtension;
        private DocumentDTO _document = new DocumentDTO();

        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public EnginePurchaseReceptionManagerVM(IRegionManager regionManager)
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

            PurchaseContractEngines = Service.CreateCollection<PurchaseContractEngineDTO>(_purchaseData.PurchaseContractEngines);
            Service.RegisterCollectionView(PurchaseContractEngines); //注册查询集合。
            PurchaseContractEngines.PropertyChanged += OnViewPropertyChanged;

            Suppliers = Service.CreateCollection<SupplierDTO>(_purchaseData.Suppliers);
            Service.RegisterCollectionView(Suppliers); //注册查询集合。
            Suppliers.PropertyChanged += OnViewPropertyChanged;

            EnginePurchaseReceptions = Service.CreateCollection<EnginePurchaseReceptionDTO>(_purchaseData.EnginePurchaseReceptions);
            Service.RegisterCollectionView(EnginePurchaseReceptions); //注册查询集合。
            EnginePurchaseReceptions.PropertyChanged += OnViewPropertyChanged;
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
                        new Category("未启动", new SolidColorBrush(Colors.Gray)),
                        new Category("正在进行中…", new SolidColorBrush(Colors.Brown)),
                        new Category("已完成", new SolidColorBrush(Colors.Green)),
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
            PurchaseContractEngines.AutoLoad = true;
            Suppliers.AutoLoad = true;
            EnginePurchaseReceptions.AutoLoad = true;
        }

        #region 业务

        #region 购买发动机接收项目集合
        /// <summary>
        ///     购买发动机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePurchaseReceptionDTO> EnginePurchaseReceptions { get; set; }

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

        #region 购买合同发动机集合
        /// <summary>
        ///     所有购买合同发动机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PurchaseContractEngineDTO> PurchaseContractEngines { get; set; }


        private ObservableCollection<PurchaseContractEngineDTO> _viewContractEngines = new ObservableCollection<PurchaseContractEngineDTO>();

        /// <summary>
        /// 购买合同发动机集合,用于界面绑定
        /// </summary>
        public ObservableCollection<PurchaseContractEngineDTO> ViewPurchaseContractEngines
        {
            get { return this._viewContractEngines; }
            set
            {
                if (this._viewContractEngines != value)
                {
                    _viewContractEngines = value;
                    this.RaisePropertyChanged(() => this.ViewPurchaseContractEngines);
                }
            }
        }
        #endregion

        #region 选择的接收项目

        private EnginePurchaseReceptionDTO _selEnginePurchaseReception;

        /// <summary>
        ///     选择的接收项目
        /// </summary>
        public EnginePurchaseReceptionDTO SelEnginePurchaseReception
        {
            get { return _selEnginePurchaseReception; }
            set
            {
                if (_selEnginePurchaseReception != value)
                {
                    _selEnginePurchaseReception = value;

                    var viewPurchaseContractEngines = PurchaseContractEngines.Where(p => p.SupplierId == SelEnginePurchaseReception.SupplierId && p.SerialNumber != null).ToList();
                    ViewPurchaseContractEngines.Clear();
                    foreach (var lca in viewPurchaseContractEngines)
                    {
                        ViewPurchaseContractEngines.Add(lca);
                    }
                    _appointments.Clear();
                    foreach (var schedule in value.ReceptionSchedules)
                    {
                        Appointment appointment = scheduleExtension.ConvertToAppointment(schedule);
                        _appointments.Add(appointment);
                    }
                    var viewDocuments = RelatedDocs.Where(l => l.SourceId == SelEnginePurchaseReception.SourceId).ToList();
                    ViewDocuments.Clear();
                    foreach (var doc in viewDocuments)
                    {
                        ViewDocuments.Add(doc);
                    }
                    RaisePropertyChanged(() => Appointments);
                    RaisePropertyChanged(() => SelEnginePurchaseReception);
                }
            }
        }

        #endregion

        #region 购买发动机接收行

        private ObservableCollection<EnginePurchaseReceptionLineDTO> _enginePurchaseReceptionLines;

        /// <summary>
        ///     购买发动机接收行
        /// </summary>
        public ObservableCollection<EnginePurchaseReceptionLineDTO> EngineReceptionLines
        {
            get { return _enginePurchaseReceptionLines; }
            private set
            {
                if (_enginePurchaseReceptionLines != value)
                {
                    _enginePurchaseReceptionLines = value;
                    RaisePropertyChanged(() => EngineReceptionLines);
                }
            }
        }

        #endregion

        #region 选择的接收行

        private EnginePurchaseReceptionLineDTO _selEnginePurchaseReceptionLine;

        /// <summary>
        ///     选择的接收行
        /// </summary>
        public EnginePurchaseReceptionLineDTO SelEnginePurchaseReceptionLine
        {
            get { return _selEnginePurchaseReceptionLine; }
            set
            {
                if (_selEnginePurchaseReceptionLine != value)
                {
                    _selEnginePurchaseReceptionLine = value;
                    RaisePropertyChanged(() => SelEnginePurchaseReceptionLine);
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

        private ObservableCollection<RelatedDocDTO> _viewDocuments = new ObservableCollection<RelatedDocDTO>();

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
            var recepiton = new EnginePurchaseReceptionDTO
            {
                EnginePurchaseReceptionId = RandomHelper.Next(),
                SourceId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
            };
            EnginePurchaseReceptions.AddNew(recepiton);
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
            var delDocs = RelatedDocs.Where(p => p.SourceId == SelEnginePurchaseReception.SourceId).ToList();
            foreach (var reltedDoc in delDocs)
            {
                RelatedDocs.Remove(delDocs);
            }
            EnginePurchaseReceptions.Remove(SelEnginePurchaseReception);
        }

        private bool CanRemove(object obj)
        {
            bool canRemove;
            if (SelEnginePurchaseReception != null)
                canRemove = true;
            else if (EnginePurchaseReceptions != null)
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
            var receptionLine = new EnginePurchaseReceptionLineDTO()
            {
                EnginePurchaseReceptionLineId = RandomHelper.Next(),
                ReceivedAmount = 1,
                AcceptedAmount = 1,
                ReceptionId = SelEnginePurchaseReception.EnginePurchaseReceptionId
            };
            SelEnginePurchaseReception.ReceptionLines.Add(receptionLine);
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
            SelEnginePurchaseReception.ReceptionLines.Remove(SelEnginePurchaseReceptionLine);
        }

        private bool CanRemoveEntity(object obj)
        {
            bool canRemove;
            if (SelEnginePurchaseReception != null && SelEnginePurchaseReceptionLine != null)
                canRemove = true;
            else canRemove = false;
            return canRemove;
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
                    SourceId = SelEnginePurchaseReception.SourceId,
                };
                var document = DocumentView.Tag as DocumentDTO;
                relatedDoc.DocumentId = document.DocumentId;
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
            DocumentView.ViewModel.InitData(true, _document.DocumentId, null);
            DocumentView.ShowDialog();
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
                    var viewPurchaseContractEngines = PurchaseContractEngines.Where(p => p.SupplierId == SelEnginePurchaseReception.SupplierId && p.SerialNumber != null).ToList();
                    ViewPurchaseContractEngines.Clear();
                    foreach (var lca in viewPurchaseContractEngines)
                    {
                        ViewPurchaseContractEngines.Add(lca);
                    }
                }
                else if (string.Equals(cell.Column.UniqueName, "ContractEngine"))
                {
                    var value = SelEnginePurchaseReceptionLine.ContractEngineId;
                    var contractEngine =
                        ViewPurchaseContractEngines.FirstOrDefault(p => p.PurchaseContractEngineId == value);
                    if (contractEngine != null)
                    {
                        SelEnginePurchaseReceptionLine.ContractName = contractEngine.ContractName;
                        SelEnginePurchaseReceptionLine.ContractNumber = contractEngine.ContractNumber;
                        SelEnginePurchaseReceptionLine.RankNumber = contractEngine.RankNumber;
                        SelEnginePurchaseReceptionLine.SerialNumber = contractEngine.SerialNumber;
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
                schedule.ReceptionScheduleId = RandomHelper.Next();
                schedule.ReceptionId = SelEnginePurchaseReception.EnginePurchaseReceptionId;
                SelEnginePurchaseReception.ReceptionSchedules.Add(schedule);
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
                        SelEnginePurchaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.UniqueId == appointment.UniqueId);
                    SelEnginePurchaseReception.ReceptionSchedules.Remove(schedule);
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
                        SelEnginePurchaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.UniqueId == appointment.UniqueId);
                    SelEnginePurchaseReception.ReceptionSchedules.Remove(schedule);
                    if (schedule != null)
                    {
                        schedule = scheduleExtension.ConvertToReceptionSchedule(appointment);
                        SelEnginePurchaseReception.ReceptionSchedules.Add(schedule);
                    }
                }
            }

        }

        #endregion
        #endregion
    }
}
