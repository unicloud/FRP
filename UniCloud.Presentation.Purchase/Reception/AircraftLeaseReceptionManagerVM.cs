#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.Windows.Input;
using System.Windows.Media;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export]
    public class AircraftLeaseReceptionManagerVM : EditMasterSlaveViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private PurchaseData _purchaseData;

        [ImportingConstructor]
        public AircraftLeaseReceptionManagerVM(IRegionManager regionManager)
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
            AircraftLeaseReceptions = Service.CreateCollection<AircraftLeaseReceptionDTO>(_purchaseData.AircraftLeaseReceptions);
            Service.RegisterCollectionView(AircraftLeaseReceptions);
            Suppliers = Service.CreateCollection<SupplierDTO>(_purchaseData.Suppliers);
            Service.RegisterCollectionView(Suppliers);
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {

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
            Suppliers.AutoLoad = true;
            //AircraftLeaseReceptions.AutoLoad = true;
            ////var LeaseReceptions = new ObservableCollection<AircraftLeaseReceptionDTO>()
            ////    {
            //var reception1 = new AircraftLeaseReceptionDTO()
            //    {
            //        CloseDate = DateTime.Now,
            //        CreateDate = DateTime.Now,
            //        StartDate = DateTime.Now,
            //        ReceptionLines = new ObservableCollection<AircraftLeaseReceptionLineDTO>()
            //            {
            //                new AircraftLeaseReceptionLineDTO()
            //                    {
            //                        AcceptedAmount = 123,
            //                        ReceivedAmount = 123,
            //                    },
            //                new AircraftLeaseReceptionLineDTO()
            //                    {
            //                        AcceptedAmount = 124,
            //                        ReceivedAmount = 125,
            //                    },
            //            },
            //    };
            //var reception2 = new AircraftLeaseReceptionDTO()
            //    {
            //        CloseDate = DateTime.Now,
            //        CreateDate = DateTime.Now,
            //        StartDate = DateTime.Now
            //    };
            //AircraftLeaseReceptions.AddNewItem(reception1);
            //AircraftLeaseReceptions.AddNewItem(reception2);
        }

        #region 业务

        #region 租赁飞机接收项目集合
        /// <summary>
        ///     租赁飞机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftLeaseReceptionDTO> AircraftLeaseReceptions { get; set; }

        /// <summary>
        ///     租赁飞机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> Suppliers { get; set; }
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
                    AircraftReceptionLines = value.ReceptionLines;
                    RaisePropertyChanged(() => AircraftReceptionLines);
                    if (value != null && value.ReceptionSchedules != null)
                    {
                        foreach (var schedule in value.ReceptionSchedules)
                        {
                            var appointment = new Appointment();

                            Appointments.Add(appointment);
                        }
                    }
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

        #region 交付日程

        private List<Appointment> _appointments;

        /// <summary>
        ///     交付日程
        /// </summary>
        public List<Appointment> Appointments
        {
            get { return _appointments; }
            private set
            {
                if (_appointments != value)
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
        /// 交机文件
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
        ///     重载保存方法
        /// </summary>
        /// <param name="sender">参数</param>
        public void OnSave(object sender)
        {

        }

        /// <summary>
        ///     重载是否保存按钮可用方法
        /// </summary>
        /// <param name="sender">参数</param>
        public override bool CanSaveExecute(object sender)
        {
            return true;
        }
        /// <summary>
        ///     新建接收项目
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
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
        public DelegateCommand<object> AbortCommand { get; private set; }

        private void OnRemove(object obj)
        {
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
        public DelegateCommand<object> SaveCommand { get; private set; }


        private bool CanSave(object obj)
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

        }

        private bool CanRejecte(object obj)
        {
            return true;
        }
        #endregion

        #region 新增接收行

        #endregion

        #region 删除接收行

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