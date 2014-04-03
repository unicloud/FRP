#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/25，18:12
// 文件名：MaintainGuaranteeVM.cs
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
    [Export(typeof (MaintainGuaranteeVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class MaintainGuaranteeVM : EditViewModelBase
    {
        private readonly PaymentData _context;
        private readonly IPaymentService _service;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public MaintainGuaranteeVM(IPaymentService service) : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitialMaintainGuarantee(); //初始化大修保证金
            InitialCurrency(); //初始币种
            InitialMaintainOrder(); //初始化大修订单
            InitialCommand(); //初始化命令
        }

        #region 加载大修保证金

        private MaintainGuaranteeDTO _selectedMaintainGuarantee;

        /// <summary>
        ///     选择大修保证金。
        /// </summary>
        public MaintainGuaranteeDTO SelectedMaintainGuarantee
        {
            get { return _selectedMaintainGuarantee; }
            set
            {
                if (_selectedMaintainGuarantee != value)
                {
                    _selectedMaintainGuarantee = value;
                    RefreshCommandState();
                    RaisePropertyChanged(() => SelectedMaintainGuarantee);
                }
            }
        }

        /// <summary>
        ///     获取所有大修保证金信息。
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainGuaranteeDTO> MaintainGuaranteesView { get; set; }

        /// <summary>
        ///     初始化大修保证金信息。
        /// </summary>
        private void InitialMaintainGuarantee()
        {
            MaintainGuaranteesView = _service.CreateCollection(_context.MaintainGuarantees);
            MaintainGuaranteesView.PageSize = 20;
            MaintainGuaranteesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedMaintainGuarantee == null)
                {
                    SelectedMaintainGuarantee = e.Entities.Cast<MaintainGuaranteeDTO>().FirstOrDefault();
                }
                RefreshCommandState();
            };
        }

        #endregion

        #region 加载维修合同

        private MaintainContractDTO _selectedMaintainContract;

        /// <summary>
        ///     选择维修合同。
        /// </summary>
        public MaintainContractDTO SelectedMaintainOrder
        {
            get { return _selectedMaintainContract; }
            set
            {
                _selectedMaintainContract = value;
                if (SelectedMaintainGuarantee != null && value != null)
                {
                    SelectedMaintainGuarantee.SupplierId = value.SignatoryId;
                    SelectedMaintainGuarantee.SupplierName = value.Signatory;
                    SelectedMaintainGuarantee.MaintainContractName = value.Name;
                }
                RaisePropertyChanged(() => SelectedMaintainOrder);
            }
        }

        /// <summary>
        ///     获取所有维修合同信息。
        /// </summary>
        public QueryableDataServiceCollectionView<MaintainContractDTO> MaintainContractView { get; set; }

        /// <summary>
        ///     初始化维修合同信息。
        /// </summary>
        private void InitialMaintainOrder()
        {
            MaintainContractView = _service.CreateCollection(_context.MaintainContracts);
            MaintainContractView.LoadedData += (sender, e) =>
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
            var guarantee = new MaintainGuaranteeDTO
            {
                GuaranteeId = RandomHelper.Next(),
                CreateDate = DateTime.Now,
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7),
                OperatorName = SessionUser.UserName,
            };
            MaintainGuaranteesView.AddNew(guarantee);
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
            if (SelectedMaintainGuarantee == null)
            {
                MessageAlert("提示", "请选择需要删除的记录");
                return;
            }
            MaintainGuaranteesView.Remove(SelectedMaintainGuarantee);
            RefreshCommandState();
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
            return SelectedMaintainGuarantee != null;
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
            if (SelectedMaintainGuarantee == null)
            {
                MessageAlert("提示", "请选择需要提交审核的记录");
                return;
            }
            SelectedMaintainGuarantee.Status = (int) GuaranteeStatus.待审核;
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
            return SelectedMaintainGuarantee != null && SelectedMaintainGuarantee.Status < (int) GuaranteeStatus.待审核;
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
            if (SelectedMaintainGuarantee == null)
            {
                MessageAlert("提示", "请选择需要审核的记录");
                return;
            }
            SelectedMaintainGuarantee.Status = (int) GuaranteeStatus.已审核;
            SelectedMaintainGuarantee.Reviewer = SessionUser.UserName;
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
            return SelectedMaintainGuarantee != null && SelectedMaintainGuarantee.Status < (int) GuaranteeStatus.已审核
                   && SelectedMaintainGuarantee.Status > (int) GuaranteeStatus.草稿;
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
                   && !MaintainGuaranteesView.IsLoading
                   && !MaintainGuaranteesView.IsSubmittingChanges
                   && !MaintainContractView.IsLoading
                   && !MaintainContractView.IsSubmittingChanges;
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
            if (!MaintainGuaranteesView.AutoLoad)
            {
                MaintainGuaranteesView.AutoLoad = true;
            }
            else
            {
                MaintainGuaranteesView.AutoLoad = true;
            }
            CurrencysView.AutoLoad = true;
            MaintainContractView.AutoLoad = true;
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
            foreach (var p in MaintainGuaranteesView.ToList())
            {
                if (p.MaintainContractId == 0)
                {
                    canSave = false;
                    MessageAlert("维修合同不能为空");
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