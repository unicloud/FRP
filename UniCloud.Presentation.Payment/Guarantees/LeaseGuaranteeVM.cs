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

using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Telerik.Windows.Data;
using UniCloud.Application.PaymentBC.DTO.GuaranteeDTO;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.Guarantees
{
    [Export(typeof (LeaseGuaranteeVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class LeaseGuaranteeVM : EditViewModelBase
    {
        private PaymentData _context;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public LeaseGuaranteeVM()
        {
            InitialLeaseGuarantee(); //初始化租赁保证金
            InitialCurrency(); //初始币种
            InitialSupplier(); //初始化供应商
            InitialCommand();
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
            LeaseGuaranteesView = Service.CreateCollection(_context.LeaseGuarantees);
            LeaseGuaranteesView.PageSize = 20;
            LeaseGuaranteesView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedLeaseGuarantee == null)
                {
                    SelectedLeaseGuarantee = e.Entities.Cast<LeaseGuaranteeDTO>().FirstOrDefault();
                }
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

        #region 命令

        #region 新增保证金命令

        public DelegateCommand<object> AddGuaranteeCommand { get; private set; }

        /// <summary>
        ///     执行新增命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OndAddGuarantee(object sender)
        {
        }

        /// <summary>
        ///     判断新增保证金命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanAddAddGuarantee(object sender)
        {
            return true;
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
        }

        /// <summary>
        ///     判断删除保证金命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>删除命令是否可用。</returns>
        public bool CanDelGuarantee(object sender)
        {
            return true;
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
        }

        /// <summary>
        ///     判断提交审核命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>提交审核命令是否可用。</returns>
        public bool CanSubmitGuarantee(object sender)
        {
            return true;
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
        }

        /// <summary>
        ///     判断编辑飞机付款计划行命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>新增命令是否可用。</returns>
        public bool CanReviewGuarantee(object sender)
        {
            return true;
        }

        #endregion

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

        #region Supplier相关信息

        private SupplierDTO _selectedSupplier;

        /// <summary>
        ///     选择供应商。
        /// </summary>
        public SupplierDTO SelSupplier
        {
            get { return _selectedSupplier; }
            set
            {
                if (_selectedSupplier != value)
                {
                    _selectedSupplier = value;
                    RaisePropertyChanged(() => SelSupplier);
                }
            }
        }


        /// <summary>
        ///     获取所有供应商公司信息。
        /// </summary>
        public QueryableDataServiceCollectionView<SupplierDTO> SuppliersView { get; set; }

        /// <summary>
        ///     初始化供应商。
        /// </summary>
        private void InitialSupplier()
        {
            SuppliersView = Service.CreateCollection(_context.Suppliers);
            Service.RegisterCollectionView(SuppliersView); //注册查询集合。
            SuppliersView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                SelSupplier = e.Entities.Cast<SupplierDTO>().FirstOrDefault();
            };
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
            if (!LeaseGuaranteesView.AutoLoad)
            {
                LeaseGuaranteesView.AutoLoad = true;
            }
            else
            {
                LeaseGuaranteesView.AutoLoad = true;
            }
            CurrencysView.AutoLoad = true;
            SuppliersView.AutoLoad = true;
        }

        #endregion
    }
}