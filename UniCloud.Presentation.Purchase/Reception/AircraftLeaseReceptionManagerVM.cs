﻿#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Controls;
using Telerik.Windows.Controls.ScheduleView;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof (AircraftLeaseReceptionManagerVM))]
    public class AircraftLeaseReceptionManagerVM : ReceptionVm
    {
        #region 声明、初始化

        private readonly PurchaseData _context;
        private readonly IPurchaseService _service;

        [ImportingConstructor]
        public AircraftLeaseReceptionManagerVM(IPurchaseService service)
            : base(service)
        {
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
            LeaseContractAircrafts = new QueryableDataServiceCollectionView<LeaseContractAircraftDTO>(_context,
                _context.LeaseContractAircrafts);

            AircraftLeaseReceptions =
                _service.CreateCollection(_context.AircraftLeaseReceptions.Expand(p => p.RelatedDocs),
                    o => o.ReceptionLines, o => o.ReceptionSchedules, o => o.RelatedDocs);
            AircraftLeaseReceptions.LoadedData += (o, e) =>
            {
                if (SelAircraftLeaseReception == null)
                    SelAircraftLeaseReception = AircraftLeaseReceptions.FirstOrDefault();
            };
            _service.RegisterCollectionView(AircraftLeaseReceptions); //注册查询集合。

            Suppliers = new QueryableDataServiceCollectionView<SupplierDTO>(_context, _context.Suppliers);
            var supplierFilter = new FilterDescriptor("AircraftLeaseSupplier", FilterOperator.IsEqualTo, true);
            Suppliers.FilterDescriptors.Add(supplierFilter);
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
            set
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
            if (!AircraftLeaseReceptions.AutoLoad)
                AircraftLeaseReceptions.AutoLoad = true;
            else
                AircraftLeaseReceptions.Load(true);

            LeaseContractAircrafts.Load(true);
            Suppliers.Load(true);
        }

        #region 业务

        #region 租赁飞机接收项目集合

        /// <summary>
        ///     租赁飞机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftLeaseReceptionDTO> AircraftLeaseReceptions { get; set; }

        #endregion

        #region 租赁合同飞机集合

        private ObservableCollection<LeaseContractAircraftDTO> _viewContractAircrafts =
            new ObservableCollection<LeaseContractAircraftDTO>();

        /// <summary>
        ///     所有租赁合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<LeaseContractAircraftDTO> LeaseContractAircrafts { get; set; }

        /// <summary>
        ///     租赁合同飞机集合,用于界面绑定
        /// </summary>
        public ObservableCollection<LeaseContractAircraftDTO> ViewLeaseContractAircrafts
        {
            get { return _viewContractAircrafts; }
            set
            {
                if (_viewContractAircrafts != value)
                {
                    _viewContractAircrafts = value;
                    RaisePropertyChanged(() => ViewLeaseContractAircrafts);
                }
            }
        }

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
                    RaisePropertyChanged(() => SelAircraftLeaseReception);
                    ViewLeaseContractAircrafts.Clear();
                    _appointments.Clear();

                    if (_selAircraftLeaseReception != null)
                    {
                        SelAircraftLeaseReceptionLine = _selAircraftLeaseReception.ReceptionLines.FirstOrDefault();
                        var viewLeaseContractAircrafts = LeaseContractAircrafts.Where(
                            p => p.SupplierId == SelAircraftLeaseReception.SupplierId && p.SerialNumber != null)
                            .ToList();
                        foreach (var lca in viewLeaseContractAircrafts)
                        {
                            ViewLeaseContractAircrafts.Add(lca);
                        }
                        //刷新界面日程控件绑定到的集合
                        foreach (var schedule in value.ReceptionSchedules)
                        {
                            var appointment = ScheduleExtension.ConvertToAppointment(schedule);
                            _appointments.Add(appointment);
                        }
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
            return _selAircraftLeaseReception != null;
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
                SourceId = SelAircraftLeaseReception.SourceId
            };
            SelAircraftLeaseReception.RelatedDocs.Add(relatedDoc);
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
                        SelAircraftLeaseReception.RelatedDocs.Remove(doc);
                    }
                });
            }
        }

        #endregion

        #endregion

        #region 操作

        #region 新建接收项目

        protected override void OnNew(object obj)
        {
            var acLr = new AircraftLeaseReceptionDTO
            {
                AircraftLeaseReceptionId = RandomHelper.Next(),
                SourceId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
            };
            var supplier = Suppliers.FirstOrDefault();
            if (supplier != null)
            {
                acLr.SupplierId = supplier.SupplierId;
                acLr.SupplierName = supplier.Name;
            }
            SelAircraftLeaseReception = acLr;
            AircraftLeaseReceptions.AddNew(SelAircraftLeaseReception);
        }

        protected override bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除接收项目

        protected override void OnRemove(object obj)
        {
            if (SelAircraftLeaseReception == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                AircraftLeaseReceptions.Remove(SelAircraftLeaseReception);
                SelAircraftLeaseReception = AircraftLeaseReceptions.FirstOrDefault();
                if (SelAircraftLeaseReception == null)
                {
                    //删除完，若没有记录了，则也要删除界面明细
                    Appointments.Clear();
                }
            });
        }

        protected override bool CanRemove(object obj)
        {
            return _selAircraftLeaseReception != null;
        }

        #endregion

        #region 新增接收行

        protected override void OnAddEntity(object obj)
        {
            SelAircraftLeaseReceptionLine = new AircraftLeaseReceptionLineDTO
            {
                AircraftLeaseReceptionLineId = RandomHelper.Next(),
                ReceivedAmount = 1,
                AcceptedAmount = 1,
                DeliverDate = DateTime.Now,
                ReceptionId = SelAircraftLeaseReception.AircraftLeaseReceptionId
            };
            var contractAircraft = ViewLeaseContractAircrafts.FirstOrDefault();
            if (contractAircraft != null)
            {
                SelAircraftLeaseReceptionLine.ContractAircraftId = contractAircraft.LeaseContractAircraftId;
                SelAircraftLeaseReceptionLine.ContractName = contractAircraft.ContractName;
                SelAircraftLeaseReceptionLine.ContractNumber = contractAircraft.ContractNumber;
                SelAircraftLeaseReceptionLine.RankNumber = contractAircraft.RankNumber;
                SelAircraftLeaseReceptionLine.MSN = contractAircraft.SerialNumber;
                SelAircraftLeaseReceptionLine.AircraftType = contractAircraft.AircraftTypeName;
            }
            SelAircraftLeaseReception.ReceptionLines.Add(SelAircraftLeaseReceptionLine);
        }

        protected override bool CanAddEntity(object obj)
        {
            return SelAircraftLeaseReception != null;
        }

        #endregion

        #region 删除接收行

        protected override void OnRemoveEntity(object obj)
        {
            if (SelAircraftLeaseReceptionLine == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                SelAircraftLeaseReception.ReceptionLines.Remove(SelAircraftLeaseReceptionLine);
                SelAircraftLeaseReceptionLine = SelAircraftLeaseReception.ReceptionLines.FirstOrDefault();
            });
        }

        protected override bool CanRemoveEntity(object obj)
        {
            return _selAircraftLeaseReceptionLine != null;
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
                    var viewLeaseContractAircrafts =
                        LeaseContractAircrafts.Where(
                            p => p.SupplierId == SelAircraftLeaseReception.SupplierId && p.SerialNumber != null)
                            .ToList();
                    ViewLeaseContractAircrafts.Clear();
                    foreach (var lca in viewLeaseContractAircrafts)
                    {
                        ViewLeaseContractAircrafts.Add(lca);
                    }
                }
                else if (string.Equals(cell.Column.UniqueName, "ContractAircraft"))
                {
                    var value = SelAircraftLeaseReceptionLine.ContractAircraftId;
                    var contractAircraft =
                        ViewLeaseContractAircrafts.FirstOrDefault(p => p.LeaseContractAircraftId == value);
                    if (contractAircraft != null)
                    {
                        SelAircraftLeaseReceptionLine.ContractName = contractAircraft.ContractName;
                        SelAircraftLeaseReceptionLine.ContractNumber = contractAircraft.ContractNumber;
                        SelAircraftLeaseReceptionLine.RankNumber = contractAircraft.RankNumber;
                        SelAircraftLeaseReceptionLine.MSN = contractAircraft.SerialNumber;
                        SelAircraftLeaseReceptionLine.AircraftType = contractAircraft.AircraftTypeName;
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
                schedule.ReceptionId = SelAircraftLeaseReception.AircraftLeaseReceptionId;
                SelAircraftLeaseReception.ReceptionSchedules.Add(schedule);
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
                        SelAircraftLeaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.ReceptionScheduleId == int.Parse(appointment.UniqueId));
                    SelAircraftLeaseReception.ReceptionSchedules.Remove(schedule);
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
                        SelAircraftLeaseReception.ReceptionSchedules.FirstOrDefault(
                            p => p.ReceptionScheduleId == int.Parse(appointment.UniqueId));
                    SelAircraftLeaseReception.ReceptionSchedules.Remove(schedule);
                    if (schedule != null)
                    {
                        schedule = ScheduleExtension.ConvertToReceptionSchedule(appointment);
                        SelAircraftLeaseReception.ReceptionSchedules.Add(schedule);
                    }
                }
            }
        }

        #endregion

        #endregion
    }
}