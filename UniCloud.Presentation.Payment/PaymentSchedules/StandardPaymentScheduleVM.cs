#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/18，16:12
// 文件名：StandardPaymentScheduleVM.cs
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
    [Export(typeof(StandardPaymentScheduleVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class StandardPaymentScheduleVM : EditViewModelBase
    {
          private PaymentData _context;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
          public StandardPaymentScheduleVM()
        {
            InitialStandardOrder(); // 初始化标准订单信息。
            InitialStandardPaymentSchedule();//初始化付款计划
            InitialCommand();//初始化命令
            InitialCurrency();//初始化币种
        }

        #region 加载标准订单

        private StandardOrderDTO _selectedStandardOrder;

        /// <summary>
        ///     选择标准订单。
        /// </summary>
        public StandardOrderDTO SelectedStandardOrder
        {
            get { return _selectedStandardOrder; }
            set
            {
                if (_selectedStandardOrder != value)
                {
                    _selectedStandardOrder = value;
                    if (value != null)
                    {
                        LoadStandardPaymentScheduleByOrderId();
                    }
                    RaisePropertyChanged(() => SelectedStandardOrder);
                }
            }
        }

        /// <summary>
        ///     获取所有标准订单信息。
        /// </summary>
        public QueryableDataServiceCollectionView<StandardOrderDTO> StandardOrdersView { get; set; }

        /// <summary>
        ///     初始化标准订单信息。
        /// </summary>
        private void InitialStandardOrder()
        {
            StandardOrdersView = Service.CreateCollection(_context.StandardOrders);
            StandardOrdersView.PageSize = 20;
            StandardOrdersView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    if (SelectedStandardOrder == null)
                    {
                        SelectedStandardOrder = e.Entities.Cast<StandardOrderDTO>().FirstOrDefault();
                    }
                };
        }

        #endregion

        #region 加载标准订单下的付款计划

        private FilterDescriptor _paymnetFilterOperator;//付款计划查询

        private StandardPaymentScheduleDTO _selectedStandardPaymentSchedule;

        /// <summary>
        ///     选择标准付款计划
        /// </summary>
        public StandardPaymentScheduleDTO SelectedStandardPaymentSchedule
        {
            get { return _selectedStandardPaymentSchedule; }
            set
            {

                _selectedStandardPaymentSchedule = value;
                RaisePropertyChanged(() => SelectedStandardPaymentSchedule);
            }
        }

        /// <summary>
        ///     获取所有付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<StandardPaymentScheduleDTO> StandardPaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化付款计划信息。
        /// </summary>
        private void InitialStandardPaymentSchedule()
        {
            StandardPaymentSchedulesView = Service.CreateCollection(_context.StandardPaymentSchedules);
            _paymnetFilterOperator = new FilterDescriptor("OrderId", FilterOperator.IsEqualTo, 0);
            StandardPaymentSchedulesView.FilterDescriptors.Add(_paymnetFilterOperator);
            StandardPaymentSchedulesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelectedStandardPaymentSchedule = e.Entities.Cast<StandardPaymentScheduleDTO>().FirstOrDefault();
                RefreshCommandState();//刷新按钮状态
            };
        }
        /// <summary>
        ///根据标准订单Id查询付款计划
        /// </summary>
        private void LoadStandardPaymentScheduleByOrderId()
        {
            _paymnetFilterOperator.Value = SelectedStandardOrder.StandardOrderId;
            if (!StandardPaymentSchedulesView.AutoLoad)
            {
                StandardPaymentSchedulesView.AutoLoad = true;
            }
            else
            {
                StandardPaymentSchedulesView.Load(true);
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

        #region 新增标准付款计划命令

        public DelegateCommand<object> AddPaymentScheduleCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddPaymentSchedule(object sender)
        {
            if (SelectedStandardOrder == null)
            {
                MessageAlert("提示", "标准订单不能为空");
                return;
            }
            if (StandardPaymentSchedulesView.Count >= 1)
            {
                MessageAlert("提示", "已存在付款计划");
                return;
            }
            //新增标准付款计划
            SelectedStandardPaymentSchedule = new StandardPaymentScheduleDTO
             {
                 StandardPaymentScheduleId = RandomHelper.Next(),
                 CreateDate = DateTime.Now,
                 OrderId = SelectedStandardOrder.StandardOrderId,
                 SupplierId = SelectedStandardOrder
                     .SupplierId,
                 SupplierName = SelectedStandardOrder.SupplierName,
             };
            //美元作为默认选中货币
            var currencyDto = CurrencysView.FirstOrDefault(p => p.Name.Equals("美元"));
            if (currencyDto != null)
                SelectedStandardPaymentSchedule.CurrencyId = currencyDto.Id;
            StandardPaymentSchedulesView.AddNewItem(SelectedStandardPaymentSchedule);
            RefreshCommandState();//刷新按钮状态
        }

        /// <summary>
        ///     判断新增标准付款计划命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentSchedule(object sender)
        {
            if (StandardPaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedStandardOrder != null && StandardPaymentSchedulesView.Count <= 0;
        }

        #endregion

        #region 新增标准付款计划行命令

        public DelegateCommand<object> AddPaymentScheduleLineCommand { get; private set; }

        /// <summary>
        ///     执行新增付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddPaymentScheduleLine(object sender)
        {
            if (SelectedStandardPaymentSchedule == null)
            {
                MessageAlert("提示", "付款计划不能为空");
                return;
            }
            //新增标准付款计划行
            SelectPaymentScheduleLine = new PaymentScheduleLineDTO
            {
                PaymentScheduleLineId = RandomHelper.Next(),
                PaymentScheduleId = SelectedStandardPaymentSchedule.StandardPaymentScheduleId,
                ScheduleDate = DateTime.Now,
            };
            SelectedStandardPaymentSchedule.PaymentScheduleLines.Add(SelectPaymentScheduleLine);
        }

        /// <summary>
        ///     判断新增标准付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddPaymentScheduleLine(object sender)
        {
            if (StandardPaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            return SelectedStandardOrder != null && SelectedStandardPaymentSchedule != null
                   && !SelectedStandardPaymentSchedule.IsCompleted;
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
            SelectedStandardPaymentSchedule.PaymentScheduleLines.Remove(SelectPaymentScheduleLine);
            RefreshCommandState();//刷新按钮状态
        }

        /// <summary>
        ///     判断删除标准付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanDelPaymentScheduleLine(object sender)
        {
            if (StandardPaymentSchedulesView.IsSubmittingChanges)
            {
                return false;
            }
            //付款计划跟发票建立关联，则不能删除
            return SelectedStandardOrder != null && SelectedStandardPaymentSchedule != null
                   && !SelectedStandardPaymentSchedule.IsCompleted && SelectPaymentScheduleLine != null;
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
            if (SelectedStandardPaymentSchedule != null)
            {
                if (SelectedStandardPaymentSchedule.PaymentScheduleLines.Count > 0) return true;
                MessageAlert("提示", "付款计划行不能为空");
                return false;
            }
            MessageAlert("提示","标准付款计划不能为空");
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
            if (!StandardOrdersView.AutoLoad)
            {
                StandardOrdersView.AutoLoad = true;
            }
            else
            {
                StandardOrdersView.AutoLoad = true;
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
