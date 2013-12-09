#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/11/20 15:19:04
// 文件名：EngineLeaseReceptionManagerVM
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
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public class EngineLeaseReceptionManagerVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;
        private CategoryCollection categories;
        private TimeMarkerCollection timeMarkers;
        private ResourceTypeCollection workGroups;
        private Service.Purchase.SchdeuleExtension.ControlExtension scheduleExtension;
        private Document.Document _document = new Document.Document();

        public WordViewer WordView;
        public PDFViewer PdfView;

        [ImportingConstructor]
        public EngineLeaseReceptionManagerVM(IRegionManager regionManager)
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
            EngineLeaseReceptions = Service.CreateCollection<EngineLeaseReceptionDTO>(_purchaseData.EngineLeaseReceptions);
            Service.RegisterCollectionView(EngineLeaseReceptions); //注册查询集合。
            EngineLeaseReceptions.PropertyChanged += OnViewPropertyChanged;
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            SubmitCommand = new DelegateCommand<object>(OnSubmit, CanSubmit);
            RejectCommand = new DelegateCommand<object>(OnReject, CanRejecte);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
            AddDocCommand = new DelegateCommand<object>(OnAddDoc, CanAddDoc);
            RemoveDocCommand = new DelegateCommand<object>(OnRemoveDoc, CanRemoveDoc);
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
                if (this.categories == null)
                {
                    this.categories = new CategoryCollection();
                    this.categories.Add(new Category("已完成", new SolidColorBrush(Colors.Green)));
                    this.categories.Add(new Category("正在进行中…", new SolidColorBrush(Colors.Brown)));
                    this.categories.Add(new Category("未启动", new SolidColorBrush(Colors.Gray)));
                }
                return this.categories;
            }
        }

        public TimeMarkerCollection TimeMarkers
        {
            get
            {
                if (this.timeMarkers == null)
                {
                    this.timeMarkers = new TimeMarkerCollection();
                    this.timeMarkers.Add(new TimeMarker("高级别", new SolidColorBrush(Colors.Red)));
                    this.timeMarkers.Add(new TimeMarker("中级别", new SolidColorBrush(Colors.Green)));
                    this.timeMarkers.Add(new TimeMarker("低级别", new SolidColorBrush(Colors.Gray)));
                }
                return this.timeMarkers;
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
                    resourceType.Add(reType);
                }
                return resourceType;
            }
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
            EngineLeaseReceptions.AutoLoad = true;
        }

        #region 业务

        #region 租赁发动机接收项目集合
        /// <summary>
        ///     租赁发动机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineLeaseReceptionDTO> EngineLeaseReceptions { get; set; }
        #endregion

        #region 选择的接收项目

        private EngineLeaseReceptionDTO _selEngineLeaseReception;

        /// <summary>
        ///     选择的接收项目
        /// </summary>
        public EngineLeaseReceptionDTO SelEngineLeaseReception
        {
            get { return _selEngineLeaseReception; }
            set
            {
                if (_selEngineLeaseReception != value)
                {
                    _selEngineLeaseReception = value;
                    Appointments.Clear();
                    foreach (var schedule in value.ReceptionSchedules)
                    {
                        var appointment = scheduleExtension.ConvertToAppointment(schedule);
                        Appointments.ToList().Add(appointment);
                    }
                    RaisePropertyChanged(() => SelEngineLeaseReception);
                }
            }
        }

        #endregion

        #region 租赁发动机接收行

        private ObservableCollection<EngineLeaseReceptionLineDTO> _engineLeaseReceptionLines;

        /// <summary>
        ///     租赁发动机接收行
        /// </summary>
        public ObservableCollection<EngineLeaseReceptionLineDTO> EngineReceptionLines
        {
            get { return _engineLeaseReceptionLines; }
            private set
            {
                if (_engineLeaseReceptionLines != value)
                {
                    _engineLeaseReceptionLines = value;
                    RaisePropertyChanged(() => EngineReceptionLines);
                }
            }
        }

        #endregion

        #region 交付日程

        private List<Appointment> _appointments = new List<Appointment>();

        /// <summary>
        ///     交付日程
        /// </summary>
        public List<Appointment> Appointments
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

        private List<string> _documents;

        /// <summary>
        /// 交机文件
        /// </summary>
        public List<string> Documents
        {
            get { return this._documents; }
            private set
            {
                if (this._documents != value)
                {
                    this._documents = value;
                    this.RaisePropertyChanged(() => this.Documents);
                }
            }
        }

        #endregion

        #region 选择的交机文件

        private List<string> _selDocument;

        /// <summary>
        /// 选择的交机文件
        /// </summary>
        public List<string> SelDocument
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
            var recepiton = new EngineLeaseReceptionDTO();
            this.EngineLeaseReceptions.AddNew(recepiton);
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
            EngineLeaseReceptions.Remove(SelEngineLeaseReception);
        }

        private bool CanRemove(object obj)
        {
            return true;
        }
        #endregion

        #region 保存接收项目

        /// <summary>
        ///     保存接收项目
        /// </summary>
        public DelegateCommand<object> SubmitCommand { get; private set; }

        private void OnSubmit(object sender)
        {
        }

        private bool CanSubmit(object obj)
        {
            return true;
        }
        #endregion

        #region 撤销更改

        /// <summary>
        ///     撤销更改
        /// </summary>
        public DelegateCommand<object> RejectCommand { get; private set; }

        private void OnReject(object obj)
        {
            EngineLeaseReceptions.RejectChanges();
        }

        private bool CanRejecte(object obj)
        {
            return true;
        }
        #endregion

        #region 新增接收行
        /// <summary>
        ///     新增接收行
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            var receptionLine = new EngineLeaseReceptionLineDTO()
            {
                ReceptionId = SelEngineLeaseReception.EngineLeaseReceptionId
            };
            SelEngineLeaseReception.ReceptionLines.Add(receptionLine);
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
            var receptionLine = new EngineLeaseReceptionLineDTO()
            {
                ReceptionId = SelEngineLeaseReception.EngineLeaseReceptionId
            };
            SelEngineLeaseReception.ReceptionLines.Add(receptionLine);
        }

        private bool CanRemoveEntity(object obj)
        {
            return true;
        }
        #endregion

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            var radRadioButton = sender as RadRadioButton;
            if (true)
            {
                WordView.Tag = null;
                WordView.ViewModel.InitData(false, _document, WordViewerClosed);
                WordView.ShowDialog();
            }
            else
            {
                PdfView.Tag = null;
                PdfView.ViewModel.InitData(false, _document, PdfViewerClosed);
                PdfView.ShowDialog();
            }
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

        #region  新增交机文件
        /// <summary>
        ///     新增交机文件
        /// </summary>
        public DelegateCommand<object> AddDocCommand { get; private set; }

        private void OnAddDoc(object obj)
        {

        }

        private bool CanAddDoc(object obj)
        {
            return true;
        }
        #endregion

        #region 删除交机文件
        /// <summary>
        ///     删除交机文件
        /// </summary>
        public DelegateCommand<object> RemoveDocCommand { get; private set; }

        private void OnRemoveDoc(object obj)
        {

        }

        private bool CanRemoveDoc(object obj)
        {
            return true;
        }
        #endregion

        #endregion
    }
}
