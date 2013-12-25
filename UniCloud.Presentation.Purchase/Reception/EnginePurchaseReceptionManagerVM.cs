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
    public class EnginePurchaseReceptionManagerVM : ReceptionVm
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private readonly DocumentDTO _document = new DocumentDTO();

        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public EnginePurchaseReceptionManagerVM(IRegionManager regionManager)
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
            PurchaseContractEngines = new QueryableDataServiceCollectionView<PurchaseContractEngineDTO>(PurchaseDataService, PurchaseDataService.PurchaseContractEngines);

            EnginePurchaseReceptions = Service.CreateCollection<EnginePurchaseReceptionDTO>(PurchaseDataService.EnginePurchaseReceptions.Expand(p => p.RelatedDocs));
            Service.RegisterCollectionView(EnginePurchaseReceptions); //注册查询集合。
            EnginePurchaseReceptions.PropertyChanged += OnViewPropertyChanged;
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
            PurchaseContractEngines.Load(true);
            Suppliers.Load(true);
            EnginePurchaseReceptions.Load(true);
        }

        #region 业务

        #region 购买发动机接收项目集合
        /// <summary>
        ///     购买发动机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePurchaseReceptionDTO> EnginePurchaseReceptions { get; set; }

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
                    RaisePropertyChanged(() => SelEnginePurchaseReception);

                    var viewPurchaseContractEngines = PurchaseContractEngines.Where(p => p.SupplierId == SelEnginePurchaseReception.SupplierId && p.SerialNumber != null).ToList();
                    ViewPurchaseContractEngines.Clear();
                    foreach (var lca in viewPurchaseContractEngines)
                    {
                        ViewPurchaseContractEngines.Add(lca);
                    }
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
            var recepiton = new EnginePurchaseReceptionDTO
            {
                EnginePurchaseReceptionId = RandomHelper.Next(),
                SourceId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
            };
            EnginePurchaseReceptions.AddNew(recepiton);
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
            if (_selEnginePurchaseReception != null)
            {
                EnginePurchaseReceptions.Remove(SelEnginePurchaseReception);
            }
            var currentEnginePurchaseReception = EnginePurchaseReceptions.FirstOrDefault();
            if (currentEnginePurchaseReception == null)
            {
                //删除完，若没有记录了，则也要删除界面明细
                Appointments.Clear();
            }
        }

        protected override bool CanRemove(object obj)
        {
            return _selEnginePurchaseReception != null;
        }
        #endregion

        #region 新增接收行
        /// <summary>
        ///     新增接收行
        /// </summary>
        protected override void OnAddEntity(object obj)
        {
            var receptionLine = new EnginePurchaseReceptionLineDTO()
            {
                EnginePurchaseReceptionLineId = RandomHelper.Next(),
                ReceivedAmount = 1,
                AcceptedAmount = 1,
                DeliverDate = DateTime.Now,
                ReceptionId = SelEnginePurchaseReception.EnginePurchaseReceptionId
            };
            SelEnginePurchaseReception.ReceptionLines.Add(receptionLine);
        }

        protected override bool CanAddEntity(object obj)
        {
            return _selEnginePurchaseReception != null;
        }
        #endregion

        #region 删除接收行

        /// <summary>
        ///     删除接收行
        /// </summary>
        protected override void OnRemoveEntity(object obj)
        {
            if (_selEnginePurchaseReceptionLine != null)
            {
                SelEnginePurchaseReception.ReceptionLines.Remove(SelEnginePurchaseReceptionLine);
            }
        }

        protected override bool CanRemoveEntity(object obj)
        {
            return _selEnginePurchaseReceptionLine != null;
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
                SelEnginePurchaseReception.RelatedDocs.Add(relatedDoc);
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
            SelEnginePurchaseReception.RelatedDocs.Remove(currentItem);
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

        protected override void OnCreated(object sender)
        {
            var scheduleView = sender as RadScheduleView;
            if (scheduleView != null)
            {
                var appointment = scheduleView.EditedAppointment as Appointment;
                var schedule = ScheduleExtension.ConvertToReceptionSchedule(appointment);
                schedule.ReceptionScheduleId = RandomHelper.Next();
                schedule.ReceptionId = SelEnginePurchaseReception.EnginePurchaseReceptionId;
                SelEnginePurchaseReception.ReceptionSchedules.Add(schedule);
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
                        SelEnginePurchaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.ReceptionScheduleId == int.Parse(appointment.UniqueId));
                    SelEnginePurchaseReception.ReceptionSchedules.Remove(schedule);
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
                        SelEnginePurchaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.ReceptionScheduleId == int.Parse(appointment.UniqueId));
                    SelEnginePurchaseReception.ReceptionSchedules.Remove(schedule);
                    if (schedule != null)
                    {
                        schedule = ScheduleExtension.ConvertToReceptionSchedule(appointment);
                        SelEnginePurchaseReception.ReceptionSchedules.Add(schedule);
                    }
                }
            }

        }

        #endregion

        #endregion
    }
}
