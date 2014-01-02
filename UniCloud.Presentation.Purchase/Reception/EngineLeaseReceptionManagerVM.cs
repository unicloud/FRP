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
    [Export(typeof (EngineLeaseReceptionManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EngineLeaseReceptionManagerVM : ReceptionVm
    {
        #region 声明、初始化

        private readonly PurchaseData _context;
        private readonly DocumentDTO _document = new DocumentDTO();
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;
        [Import] public DocumentViewer DocumentView;

        [ImportingConstructor]
        public EngineLeaseReceptionManagerVM(IRegionManager regionManager, IPurchaseService service) : base(service)
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
            LeaseContractEngines = new QueryableDataServiceCollectionView<LeaseContractEngineDTO>(_context,
                _context.LeaseContractEngines);

            EngineLeaseReceptions =
                _service.CreateCollection<EngineLeaseReceptionDTO>(
                    _context.EngineLeaseReceptions.Expand(p => p.RelatedDocs));
            _service.RegisterCollectionView(EngineLeaseReceptions); //注册查询集合。
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
            LeaseContractEngines.Load(true);
            Suppliers.Load(true);
            EngineLeaseReceptions.Load(true);
        }

        #region 业务

        #region 租赁发动机接收项目集合

        /// <summary>
        ///     租赁发动机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<EngineLeaseReceptionDTO> EngineLeaseReceptions { get; set; }

        #endregion

        #region 租赁合同发动机集合

        private ObservableCollection<LeaseContractEngineDTO> _viewContractEngines =
            new ObservableCollection<LeaseContractEngineDTO>();

        /// <summary>
        ///     所有租赁合同发动机集合
        /// </summary>
        public QueryableDataServiceCollectionView<LeaseContractEngineDTO> LeaseContractEngines { get; set; }

        /// <summary>
        ///     租赁合同发动机集合,用于界面绑定
        /// </summary>
        public ObservableCollection<LeaseContractEngineDTO> ViewLeaseContractEngines
        {
            get { return _viewContractEngines; }
            set
            {
                if (_viewContractEngines != value)
                {
                    _viewContractEngines = value;
                    RaisePropertyChanged(() => ViewLeaseContractEngines);
                }
            }
        }

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
                    RaisePropertyChanged(() => SelEngineLeaseReception);

                    var viewLeaseContractEngines =
                        LeaseContractEngines.Where(
                            p => p.SupplierId == SelEngineLeaseReception.SupplierId && p.SerialNumber != null).ToList();
                    ViewLeaseContractEngines.Clear();
                    foreach (var lca in viewLeaseContractEngines)
                    {
                        ViewLeaseContractEngines.Add(lca);
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

        private EngineLeaseReceptionLineDTO _selEngineLeaseReceptionLine;

        /// <summary>
        ///     选择的接收行
        /// </summary>
        public EngineLeaseReceptionLineDTO SelEngineLeaseReceptionLine
        {
            get { return _selEngineLeaseReceptionLine; }
            set
            {
                if (_selEngineLeaseReceptionLine != value)
                {
                    _selEngineLeaseReceptionLine = value;
                    RaisePropertyChanged(() => SelEngineLeaseReceptionLine);
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

        #region 新建接收项目

        /// <summary>
        ///     新建接收项目
        /// </summary>
        protected override void OnNew(object obj)
        {
            var recepiton = new EngineLeaseReceptionDTO
            {
                EngineLeaseReceptionId = RandomHelper.Next(),
                SourceId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
            };
            EngineLeaseReceptions.AddNew(recepiton);
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
            if (SelEngineLeaseReception != null)
            {
                EngineLeaseReceptions.Remove(SelEngineLeaseReception);
            }
            var currentEngineLeaseReception = EngineLeaseReceptions.FirstOrDefault();
            if (currentEngineLeaseReception == null)
            {
                //删除完，若没有记录了，则也要删除界面明细
                Appointments.Clear();
            }
        }

        protected override bool CanRemove(object obj)
        {
            return _selEngineLeaseReception != null;
        }

        #endregion

        #region 新增接收行

        /// <summary>
        ///     新增接收行
        /// </summary>
        protected override void OnAddEntity(object obj)
        {
            var receptionLine = new EngineLeaseReceptionLineDTO
            {
                EngineLeaseReceptionLineId = RandomHelper.Next(),
                ReceivedAmount = 1,
                AcceptedAmount = 1,
                DeliverDate = DateTime.Now,
                ReceptionId = SelEngineLeaseReception.EngineLeaseReceptionId
            };
            SelEngineLeaseReception.ReceptionLines.Add(receptionLine);
        }

        protected override bool CanAddEntity(object obj)
        {
            return SelEngineLeaseReception != null;
        }

        #endregion

        #region 删除接收行

        /// <summary>
        ///     删除接收行
        /// </summary>
        protected override void OnRemoveEntity(object obj)
        {
            if (_selEngineLeaseReceptionLine != null)
            {
                SelEngineLeaseReception.ReceptionLines.Remove(SelEngineLeaseReceptionLine);
            }
        }

        protected override bool CanRemoveEntity(object obj)
        {
            return _selEngineLeaseReceptionLine != null;
        }

        #endregion

        #region 添加附件

        /// <summary>
        ///     添加附件
        /// </summary>
        protected override void OnAddAttach(object sender)
        {
            if (SelEngineLeaseReception != null)
            {
                DocumentView.ViewModel.InitData(false, _document.DocumentId, DocumentViewerClosed);
                DocumentView.ShowDialog();
            }
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                var relatedDoc = new RelatedDocDTO
                {
                    Id = RandomHelper.Next(),
                    SourceId = SelEngineLeaseReception.SourceId,
                };
                var document = DocumentView.Tag as DocumentDTO;
                relatedDoc.DocumentId = document.DocumentId;
                relatedDoc.DocumentName = document.Name;
                SelEngineLeaseReception.RelatedDocs.Add(relatedDoc);
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
            SelEngineLeaseReception.RelatedDocs.Remove(currentItem);
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
                    var viewLeaseContractEngines =
                        LeaseContractEngines.Where(
                            p => p.SupplierId == SelEngineLeaseReception.SupplierId && p.SerialNumber != null).ToList();
                    ViewLeaseContractEngines.Clear();
                    foreach (var lca in viewLeaseContractEngines)
                    {
                        ViewLeaseContractEngines.Add(lca);
                    }
                }
                else if (string.Equals(cell.Column.UniqueName, "ContractEngine"))
                {
                    var value = SelEngineLeaseReceptionLine.ContractEngineId;
                    var contractEngine =
                        ViewLeaseContractEngines.FirstOrDefault(p => p.LeaseContractEngineId == value);
                    if (contractEngine != null)
                    {
                        SelEngineLeaseReceptionLine.ContractName = contractEngine.ContractName;
                        SelEngineLeaseReceptionLine.ContractNumber = contractEngine.ContractNumber;
                        SelEngineLeaseReceptionLine.RankNumber = contractEngine.RankNumber;
                        SelEngineLeaseReceptionLine.SerialNumber = contractEngine.SerialNumber;
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
                schedule.ReceptionId = SelEngineLeaseReception.EngineLeaseReceptionId;
                SelEngineLeaseReception.ReceptionSchedules.Add(schedule);
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
                        SelEngineLeaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.ReceptionScheduleId == int.Parse(appointment.UniqueId));
                    SelEngineLeaseReception.ReceptionSchedules.Remove(schedule);
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
                        SelEngineLeaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.ReceptionScheduleId == int.Parse(appointment.UniqueId));
                    SelEngineLeaseReception.ReceptionSchedules.Remove(schedule);
                    if (schedule != null)
                    {
                        schedule = ScheduleExtension.ConvertToReceptionSchedule(appointment);
                        SelEngineLeaseReception.ReceptionSchedules.Add(schedule);
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}