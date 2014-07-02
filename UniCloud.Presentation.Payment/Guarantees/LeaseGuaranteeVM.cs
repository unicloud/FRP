#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/25，18:12
// 文件名：LeaseGuaranteeVM.cs
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
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;
using UniCloud.Presentation.Service.Payment.Payment.Enums;
using UniCloud.Presentation.SessionExtension;

#endregion

namespace UniCloud.Presentation.Payment.Guarantees
{
    [Export(typeof (LeaseGuaranteeVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class LeaseGuaranteeVM : EditViewModelBase
    {
        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public LeaseGuaranteeVM(IPaymentService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialLeaseGuarantee(); //初始化租赁保证金
            InitialCurrency(); //初始币种
            InitialLeaseOrder(); //初始化租赁订单
            InitialCommand(); //初始化命令
        }

        #region 加载租赁保证金

        private LeaseGuaranteeDTO _selectedLeaseGuarantee;

        /// <summary>
        ///     选择租赁保证金。
        /// </summary>
        public LeaseGuaranteeDTO SelectedLeaseGuarantee
        {
            get { return _selectedLeaseGuarantee; }
            set
            {
                if (_selectedLeaseGuarantee != value)
                {
                    _selectedLeaseGuarantee = value;
                    RefreshCommandState();
                    RaisePropertyChanged(() => SelectedLeaseGuarantee);
                }
            }
        }

        /// <summary>
        ///     获取所有租赁保证金信息。
        /// </summary>
        public QueryableDataServiceCollectionView<LeaseGuaranteeDTO> LeaseGuaranteesView { get; set; }

        /// <summary>
        ///     初始化租赁保证金信息。
        /// </summary>
        private void InitialLeaseGuarantee()
        {
            LeaseGuaranteesView = _service.CreateCollection(_context.LeaseGuarantees);
            LeaseGuaranteesView.PageSize = 20;
            LeaseGuaranteesView.LoadedData += (sender, e) =>
            {
                if (SelectedLeaseGuarantee == null)
                    SelectedLeaseGuarantee = e.Entities.Cast<LeaseGuaranteeDTO>().FirstOrDefault();
                RefreshCommandState();
            };
        }

        #endregion

        #region 加载租赁订单

        private LeaseOrderDTO _selectedLeaseOrder;

        /// <summary>
        ///     选择租赁订单。
        /// </summary>
        public LeaseOrderDTO SelectedLeaseOrder
        {
            get { return _selectedLeaseOrder; }
            set
            {
                _selectedLeaseOrder = value;
                if (SelectedLeaseGuarantee != null && value != null)
                {
                    SelectedLeaseGuarantee.SupplierId = value.SupplierId;
                    SelectedLeaseGuarantee.SupplierName = value.SupplierName;
                    SelectedLeaseGuarantee.OrderName = value.Name;
                }
                RaisePropertyChanged(() => SelectedLeaseOrder);
            }
        }

        /// <summary>
        ///     获取所有租赁订单信息。
        /// </summary>
        public QueryableDataServiceCollectionView<LeaseOrderDTO> LeaseOrdersView { get; set; }

        /// <summary>
        ///     初始化租赁订单信息。
        /// </summary>
        private void InitialLeaseOrder()
        {
            LeaseOrdersView = _service.CreateCollection(_context.LeaseOrders);
            LeaseOrdersView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                }
                RefreshCommandState();
            };
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
            CurrencysView = _service.CreateCollection(_context.Currencies);
            CurrencysView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                RefreshCommandState();
            };
        }

        #endregion

        #region 命令

        #region 新增保证金命令

        public DelegateCommand<object> AddGuaranteeCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddGuarantee(object sender)
        {
            SelectedLeaseGuarantee = new LeaseGuaranteeDTO
            {
                GuaranteeId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                OperatorName = SessionUser.UserName,
            };
            var firstOrDefault = LeaseOrdersView.FirstOrDefault();
            if (firstOrDefault != null)
            {
                SelectedLeaseGuarantee.SupplierId = firstOrDefault.SupplierId;
                SelectedLeaseGuarantee.SupplierName = firstOrDefault.SupplierName;
                SelectedLeaseGuarantee.OrderId = firstOrDefault.Id;
                SelectedLeaseGuarantee.OrderName = firstOrDefault.Name;
            }
            var currency = CurrencysView.FirstOrDefault();
            if (currency != null)
            {
                SelectedLeaseGuarantee.CurrencyId = currency.Id;
                SelectedLeaseGuarantee.CurrencyName = currency.Name;
            }
            LeaseGuaranteesView.AddNew(SelectedLeaseGuarantee);
        }

        /// <summary>
        ///     判断新增保证金命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddAddGuarantee(object sender)
        {
            return GetButtonState();
        }

        #endregion

        #region 删除保证金命令

        public DelegateCommand<object> DelGuaranteeCommand { get; private set; }

        /// <summary>
        ///     执行删除保证金命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnDelGuarantee(object sender)
        {
            if (SelectedLeaseGuarantee == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            MessageConfirm("确定删除此记录及相关信息！", (s, arg) =>
            {
                if (arg.DialogResult != true) return;
                LeaseGuaranteesView.Remove(SelectedLeaseGuarantee);
                SelectedLeaseGuarantee = LeaseGuaranteesView.FirstOrDefault();
                RefreshCommandState();
            });
        }

        /// <summary>
        ///     判断删除保证金命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDelGuarantee(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedLeaseGuarantee != null;
        }

        #endregion

        #region 提交审核

        public DelegateCommand<object> SubmitGuaranteeCommand { get; private set; }

        /// <summary>
        ///     提交审核。
        /// </summary>
        /// <param name="sender"></param>
        public void OnSubmitGuarantee(object sender)
        {
            if (SelectedLeaseGuarantee == null)
            {
                MessageAlert("提示", "请选择需要提交审核的记录");
                return;
            }
            SelectedLeaseGuarantee.Status = (int) GuaranteeStatus.待审核;
            RefreshCommandState();
        }

        /// <summary>
        ///     判断提交审核命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>提交审核命令是否可用。</returns>
        public bool CanSubmitGuarantee(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedLeaseGuarantee != null && SelectedLeaseGuarantee.Status < (int) GuaranteeStatus.待审核;
        }

        #endregion

        #region 审核保证金

        public DelegateCommand<object> ReviewGuaranteeCommand { get; private set; }

        /// <summary>
        ///     执行编辑付款计划行命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnReviewGuarantee(object sender)
        {
            if (SelectedLeaseGuarantee == null)
            {
                MessageAlert("提示", "请选择需要审核的记录");
                return;
            }
            SelectedLeaseGuarantee.Status = (int) GuaranteeStatus.已审核;
            SelectedLeaseGuarantee.Reviewer = SessionUser.UserName;
            RefreshCommandState();
        }

        /// <summary>
        ///     判断编辑飞机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanReviewGuarantee(object sender)
        {
            if (!GetButtonState())
            {
                return false;
            }
            return SelectedLeaseGuarantee != null && SelectedLeaseGuarantee.Status < (int) GuaranteeStatus.已审核
                   && SelectedLeaseGuarantee.Status > (int) GuaranteeStatus.草稿;
        }

        #endregion

        /// <summary>
        ///     获取按钮状态
        /// </summary>
        /// <returns></returns>
        private bool GetButtonState()
        {
            //当处于加载中，按钮是不可用的
            return !CurrencysView.IsLoading && !CurrencysView.IsSubmittingChanges
                   && !LeaseGuaranteesView.IsLoading
                   && !LeaseGuaranteesView.IsSubmittingChanges
                   && !LeaseOrdersView.IsLoading
                   && !LeaseOrdersView.IsSubmittingChanges;
        }

        /// <summary>
        ///     初始化命令
        /// </summary>
        private void InitialCommand()
        {
            AddGuaranteeCommand = new DelegateCommand<object>(OndAddGuarantee, CanAddAddGuarantee);
            DelGuaranteeCommand = new DelegateCommand<object>(OnDelGuarantee,
                CanDelGuarantee);
            SubmitGuaranteeCommand = new DelegateCommand<object>(OnSubmitGuarantee,
                CanSubmitGuarantee);
            ReviewGuaranteeCommand = new DelegateCommand<object>(OnReviewGuarantee,
                CanReviewGuarantee);
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            if (!LeaseGuaranteesView.AutoLoad)
            {
                LeaseGuaranteesView.AutoLoad = true;
            }
            else
            {
                LeaseGuaranteesView.AutoLoad = true;
            }
            CurrencysView.AutoLoad = true;
            LeaseOrdersView.AutoLoad = true;
        }

        protected override void RefreshCommandState()
        {
            SubmitGuaranteeCommand.RaiseCanExecuteChanged();
            DelGuaranteeCommand.RaiseCanExecuteChanged();
            AddGuaranteeCommand.RaiseCanExecuteChanged();
            ReviewGuaranteeCommand.RaiseCanExecuteChanged();
            SaveCommand.RaiseCanExecuteChanged();
            AbortCommand.RaiseCanExecuteChanged();
        }

        protected override bool OnSaveExecuting(object sender)
        {
            var canSave = true;
            foreach (var p in LeaseGuaranteesView.ToList())
            {
                if (p.OrderId == 0)
                {
                    canSave = false;
                    MessageAlert("订单不能为空");
                    break;
                }
                if (p.CurrencyId == 0)
                {
                    canSave = false;
                    MessageAlert("币种不能为空");
                    break;
                }
                if (p.Amount == 0)
                {
                    canSave = false;
                    MessageAlert("付款金额不能为空");
                    break;
                }
            }
            return canSave;
        }

        #endregion
    }
}