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
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Regions;
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
    [Export(typeof(EnginePurchaseReceptionManagerVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EnginePurchaseReceptionManagerVM : ReceptionVm
    {
        #region 声明、初始化

        private readonly PurchaseData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;

        [ImportingConstructor]
        public EnginePurchaseReceptionManagerVM(IRegionManager regionManager, IPurchaseService service)
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
            PurchaseContractEngines =
                new QueryableDataServiceCollectionView<PurchaseContractEngineDTO>(_context,
                    _context.PurchaseContractEngines);

            EnginePurchaseReceptions = _service.CreateCollection(_context.EnginePurchaseReceptions.Expand(p => p.RelatedDocs),
                o => o.ReceptionLines, o => o.ReceptionSchedules, o => o.RelatedDocs);
            EnginePurchaseReceptions.LoadedData += (o, e) =>
                                                   {
                                                       if (SelEnginePurchaseReception == null)
                                                           SelEnginePurchaseReception = EnginePurchaseReceptions.FirstOrDefault();
                                                   };
            _service.RegisterCollectionView(EnginePurchaseReceptions); //注册查询集合。
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
            if (!EnginePurchaseReceptions.AutoLoad)
                EnginePurchaseReceptions.AutoLoad = true;
            else
                EnginePurchaseReceptions.Load(true);

            PurchaseContractEngines.Load(true);
            Suppliers = _service.GetSupplier(() => RaisePropertyChanged(() => Suppliers));
        }

        #region 业务

        #region 购买发动机接收项目集合

        /// <summary>
        ///     购买发动机接收项目集合
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePurchaseReceptionDTO> EnginePurchaseReceptions { get; set; }

        #endregion

        #region 购买合同发动机集合

        private ObservableCollection<PurchaseContractEngineDTO> _viewContractEngines =
            new ObservableCollection<PurchaseContractEngineDTO>();

        /// <summary>
        ///     所有购买合同发动机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PurchaseContractEngineDTO> PurchaseContractEngines { get; set; }

        /// <summary>
        ///     购买合同发动机集合,用于界面绑定
        /// </summary>
        public ObservableCollection<PurchaseContractEngineDTO> ViewPurchaseContractEngines
        {
            get { return _viewContractEngines; }
            set
            {
                if (_viewContractEngines != value)
                {
                    _viewContractEngines = value;
                    RaisePropertyChanged(() => ViewPurchaseContractEngines);
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
                    ViewPurchaseContractEngines.Clear();
                    _appointments.Clear();

                    if (_selEnginePurchaseReception != null)
                    {
                        SelEnginePurchaseReceptionLine = _selEnginePurchaseReception.ReceptionLines.FirstOrDefault();
                        var viewPurchaseContractEngines = PurchaseContractEngines.Where(
                                p => p.SupplierId == SelEnginePurchaseReception.SupplierId && p.SerialNumber != null)
                                .ToList();
                        foreach (var lca in viewPurchaseContractEngines)
                        {
                            ViewPurchaseContractEngines.Add(lca);
                        }
                        foreach (var schedule in value.ReceptionSchedules)
                        {
                            Appointment appointment = ScheduleExtension.ConvertToAppointment(schedule);
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
            return _selEnginePurchaseReception != null;
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
                SourceId = SelEnginePurchaseReception.SourceId
            };
            SelEnginePurchaseReception.RelatedDocs.Add(relatedDoc);
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
                        SelEnginePurchaseReception.RelatedDocs.Remove(doc);
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
            SelEnginePurchaseReception = new EnginePurchaseReceptionDTO
            {
                EnginePurchaseReceptionId = RandomHelper.Next(),
                SourceId = Guid.NewGuid(),
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
            };
            var supplier = Suppliers.FirstOrDefault();
            if (supplier != null)
            {
                SelEnginePurchaseReception.SupplierId = supplier.SupplierId;
                SelEnginePurchaseReception.SupplierName = supplier.Name;
            }
            EnginePurchaseReceptions.AddNew(SelEnginePurchaseReception);
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
            if (SelEnginePurchaseReception == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                EnginePurchaseReceptions.Remove(SelEnginePurchaseReception);

                                                SelEnginePurchaseReception = EnginePurchaseReceptions.FirstOrDefault();
                                                if (SelEnginePurchaseReception == null)
                                                {
                                                    //删除完，若没有记录了，则也要删除界面明细
                                                    Appointments.Clear();
                                                }
                                            });
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
            SelEnginePurchaseReceptionLine = new EnginePurchaseReceptionLineDTO
            {
                EnginePurchaseReceptionLineId = RandomHelper.Next(),
                ReceivedAmount = 1,
                AcceptedAmount = 1,
                DeliverDate = DateTime.Now,
                ReceptionId = SelEnginePurchaseReception.EnginePurchaseReceptionId
            };
            var contractEngine = ViewPurchaseContractEngines.FirstOrDefault();
            if (contractEngine != null)
            {
                SelEnginePurchaseReceptionLine.ContractName = contractEngine.ContractName;
                SelEnginePurchaseReceptionLine.ContractNumber = contractEngine.ContractNumber;
                SelEnginePurchaseReceptionLine.RankNumber = contractEngine.RankNumber;
                SelEnginePurchaseReceptionLine.SerialNumber = contractEngine.SerialNumber;
            }
            SelEnginePurchaseReception.ReceptionLines.Add(SelEnginePurchaseReceptionLine);
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
            if (SelEnginePurchaseReceptionLine == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
                                            {
                                                if (arg.DialogResult != true) return;
                                                SelEnginePurchaseReception.ReceptionLines.Remove(SelEnginePurchaseReceptionLine);
                                                SelEnginePurchaseReceptionLine = SelEnginePurchaseReception.ReceptionLines.FirstOrDefault();
                                            });
        }

        protected override bool CanRemoveEntity(object obj)
        {
            return _selEnginePurchaseReceptionLine != null;
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
                    var viewPurchaseContractEngines =
                        PurchaseContractEngines.Where(
                            p => p.SupplierId == SelEnginePurchaseReception.SupplierId && p.SerialNumber != null)
                            .ToList();
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