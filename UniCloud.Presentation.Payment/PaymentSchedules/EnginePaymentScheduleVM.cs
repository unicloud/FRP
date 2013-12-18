#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/17，10:12
// 文件名：EnginePaymentScheduleVM.cs
// 程序集：UniCloud.Presentation.Payment
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Controls.DataServices;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof(EnginePaymentScheduleVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class EnginePaymentScheduleVM : EditViewModelBase
    {
        private PaymentData _context;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public EnginePaymentScheduleVM()
        {
            InitialContractEngine(); // 初始化合同发动机信息。
            InitialEnginePaymentSchedule();//初始化付款计划
            InitialCommand();//初始化命令
            InitialCurrency();//初始化币种
        }

        #region 加载合同发动机

        private ContractEngineDTO _selectedContractEngine;

        /// <summary>
        ///     选择合同发动机。
        /// </summary>
        public ContractEngineDTO SelectedContractEngine
        {
            get { return _selectedContractEngine; }
            set
            {
                if (_selectedContractEngine != value)
                {
                    _selectedContractEngine = value;
                    if (value != null)
                    {
                        LoadEnginePaymentScheduleByContractEngineId();
                    }
                    RaisePropertyChanged(() => SelectedContractEngine);
                }
            }
        }

        /// <summary>
        ///     获取所有合同发动机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ContractEngineDTO> ContractEnginesView { get; set; }

        /// <summary>
        ///     初始化合同发动机信息。
        /// </summary>
        private void InitialContractEngine()
        {
            ContractEnginesView = Service.CreateCollection(_context.ContractEngines);
            ContractEnginesView.PageSize = 20;
            ContractEnginesView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    if (SelectedContractEngine == null)
                    {
                        SelectedContractEngine = e.Entities.Cast<ContractEngineDTO>().FirstOrDefault();
                    }
                };
        }

        #endregion

        #region 加载合同发动机下的付款计划

        private FilterDescriptor _paymnetFilterOperator;//付款计划查询

        private EnginePaymentScheduleDTO _selectedEnginePaymentSchedule;

        /// <summary>
        ///     选择发动机付款计划
        /// </summary>
        public EnginePaymentScheduleDTO SelectedEnginePaymentSchedule
        {
            get { return _selectedEnginePaymentSchedule; }
            set
            {

                _selectedEnginePaymentSchedule = value;
                RaisePropertyChanged(() => SelectedEnginePaymentSchedule);
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePaymentScheduleDTO> EnginePaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialEnginePaymentSchedule()
        {
            EnginePaymentSchedulesView = Service.CreateCollection(_context.EnginePaymentSchedules);
            _paymnetFilterOperator = new FilterDescriptor("ContractEngineId", FilterOperator.IsEqualTo, 0);
            EnginePaymentSchedulesView.FilterDescriptors.Add(_paymnetFilterOperator);
            EnginePaymentSchedulesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedEnginePaymentSchedule = e.Entities.Cast<EnginePaymentScheduleDTO>().FirstOrDefault();
                RefreshCommandState();//刷新按钮状态
            };
        }
        /// <summary>
        ///根据合同发动机Id查询付款计划
        /// </summary>
        private void LoadEnginePaymentScheduleByContractEngineId()
        {
            _paymnetFilterOperator.Value = SelectedContractEngine.ContractEngineId;
            if (!EnginePaymentSchedulesView.AutoLoad)
            {
                EnginePaymentSchedulesView.AutoLoad = true;
            }
            else
            {
                EnginePaymentSchedulesView.Load(true);
            }
        }
        #endregion

        #region 加载币种

        private CurrencyDTO _selectedCurrency;

        /// <summary>
        ///     选择币种
        /// </summary>
        public CurrencyDTO SelectedCurrency
        {
            get { return _selectedCurrency; }
            set
            {
                _selectedCurrency = value;
                RaisePropertyChanged(() => SelectedCurrency);
            }
        }

        /// <summary>
        ///     获取所有币种。
        /// </summary>
        public QueryableDataServiceCollectionView<CurrencyDTO> CurrencysView { get; set; }

        /// <summary>
        ///     初始化币种信息。
        /// </summary>
        private void InitialCurrency()
        {
            CurrencysView = Service.CreateCollection(_context.Currencies);
            CurrencysView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedCurrency = e.Entities.Cast<CurrencyDTO>().FirstOrDefault();
            };
        }
        #endregion

        #region 属性

        private PaymentScheduleLineDTO _selectPaymentScheduleLine;
        /// <summary>
        /// 选中付款计划明细
        /// </summary>
        public PaymentScheduleLineDTO SelectPaymentScheduleLine
        {
            get
            {
                return _selectPaymentScheduleLine;
            }
            set
            {
                _selectPaymentScheduleLine = value;
                RaisePropertyChanged(() => SelectPaymentScheduleLine);
                DelPaymentScheduleLineCommand.RaiseCanExecuteChanged();//刷新删除按钮状态
            }
        }
        #endregion

        #region 命令

        #region 新增发动机付款计划命令

        public DelegateCommand<object> AddPaymentScheduleCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddPaymentSchedule(object sender)
        {
            if (SelectedContractEngine == null)
            {
                MessageAlert("提示", "合同发动机不能为空");
                return;
            }
            if (ContractEnginesView.Count >= 1)
            {
                MessageAlert("提示", "已存在付款计划");
                return;
            }
            //新增发动机付款计划
            SelectedEnginePaymentSchedule = new EnginePaymentScheduleDTO
             {
                 EnginePaymentScheduleId = RandomHelper.Next(),
                 CreateDate = DateTime.Now,
                 ContractEngineId = SelectedContractEngine.ContractEngineId,
                 SupplierId = SelectedContractEngine.SupplierId == null ? 0 : SelectedContractEngine
                                                             .SupplierId.Value,
                 SupplierName = SelectedContractEngine.SupplierName,
             };
            //美元作为默认选中货币
            var currencyDto = CurrencysView.FirstOrDefault(p => p.Name.Equals("美元"));
            if (currencyDto != null)
                SelectedEnginePaymentSchedule.CurrencyId = currencyDto.Id;
            ContractEnginesView.AddNewItem(SelectedEnginePaymentSchedule);
            RefreshCommandState();//刷新按钮状态
        }

        /// <summary>
        ///     判断新增发动机付款计划命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentSchedule(object sender)
        {
            if (EnginePaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedContractEngine != null && EnginePaymentSchedulesView.Count <= 0;
        }

        #endregion

        #region 新增发动机付款计划行命令

        public DelegateCommand<object> AddPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行新增付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddPaymentScheduleLine(object sender)
        {
            if (SelectedEnginePaymentSchedule == null)
            {
                MessageAlert("提示", "付款计划不能为空");
                return;
            }
            //新增发动机付款计划行
            SelectPaymentScheduleLine = new PaymentScheduleLineDTO
            {
                PaymentScheduleLineId = RandomHelper.Next(),
                PaymentScheduleId = SelectedEnginePaymentSchedule.EnginePaymentScheduleId,
                ScheduleDate = DateTime.Now,
            };
            SelectedEnginePaymentSchedule.PaymentScheduleLines.Add(SelectPaymentScheduleLine);
        }

        /// <summary>
        ///     判断新增发动机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentScheduleLine(object sender)
        {
            if (EnginePaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedContractEngine != null && SelectedEnginePaymentSchedule != null
                   && !SelectedEnginePaymentSchedule.IsCompleted;
        }

        #endregion

        #region 删除机付款计划行命令

        public DelegateCommand<object> DelPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行删除付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndDelPaymentScheduleLine(object sender)
        {
            if (SelectPaymentScheduleLine == null)
            {
                MessageAlert("提示", "请选择需要删除的付款计划明细");
                return;
            }
            SelectedEnginePaymentSchedule.PaymentScheduleLines.Remove(SelectPaymentScheduleLine);
            RefreshCommandState();//刷新按钮状态
        }

        /// <summary>
        ///     判断删除发动机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanDelPaymentScheduleLine(object sender)
        {
            if (EnginePaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            //付款计划跟发票建立关联，则不能删除
            return SelectedContractEngine != null && SelectedEnginePaymentSchedule != null
                   && !SelectedEnginePaymentSchedule.IsCompleted && SelectPaymentScheduleLine != null;
        }

        #endregion

        /// <summary>
        /// 初始化命令
        /// </summary>
        private void InitialCommand()
        {
            AddPaymentScheduleCommand = new DelegateCommand<object>(OndAddPaymentSchedule, CanAddPaymentSchedule);
            AddPaymentScheduleLineCommand = new DelegateCommand<object>(OndAddPaymentScheduleLine, CanAddPaymentScheduleLine);
            DelPaymentScheduleLineCommand = new DelegateCommand<object>(OndDelPaymentScheduleLine, CanDelPaymentScheduleLine);
        }

        #endregion

        #region 重载保存命令操作
        /// <summary>
        /// 保存前操作
        /// </summary>
        /// <param name="sender"></param>
        protected override bool OnSaveExecuting(QueryableDataServiceCollectionViewBase sender)
        {
            if (SelectedEnginePaymentSchedule != null)
            {
                if (SelectedEnginePaymentSchedule.PaymentScheduleLines.Count > 0) return true;
                MessageAlert("提示", "付款计划行不能为空");
                return false;
            }
            MessageAlert("提示", "发动机付款计划不能为空");
            return false;
        }

        #endregion

        #region 重载基类服务

        protected override IService CreateService()
        {
            _context = new PaymentData(AgentHelper.PaymentUri);
            return new PaymentService(_context);
        }

        public override void LoadData()
        {
            if (!ContractEnginesView.AutoLoad)
            {
                ContractEnginesView.AutoLoad = true;
            }
            else
            {
                ContractEnginesView.AutoLoad = true;
            }
            CurrencysView.AutoLoad = true;
        }

        /// <summary>
        /// 刷新按钮状态
        /// </summary>
        public override void RefreshCommandState()
        {
            SaveCommand.RaiseCanExecuteChanged();
            AbortCommand.RaiseCanExecuteChanged();
            AddPaymentScheduleCommand.RaiseCanExecuteChanged();
            AddPaymentScheduleLineCommand.RaiseCanExecuteChanged();
            DelPaymentScheduleLineCommand.RaiseCanExecuteChanged();
        }

        #endregion
    }
}