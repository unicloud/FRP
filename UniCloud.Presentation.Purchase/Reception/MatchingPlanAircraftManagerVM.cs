#region 命名空间

using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Purchase;
using UniCloud.Presentation.Service.Purchase.Purchase;

#endregion

namespace UniCloud.Presentation.Purchase.Reception
{
    [Export(typeof (MatchingPlanAircraftManagerVM))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class MatchingPlanAircraftManagerVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PurchaseData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPurchaseService _service;

        [ImportingConstructor]
        public MatchingPlanAircraftManagerVM(IRegionManager regionManager, IPurchaseService service) : base(service)
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
            ContractAircrafts = _service.CreateCollection(_context.ContractAircrafts);
            _service.RegisterCollectionView(ContractAircrafts); //注册查询集合。

            PlanAircrafts = _service.CreateCollection(_context.PlanAircrafts);
            _service.RegisterCollectionView(PlanAircrafts); //注册查询集合。
        }

        private void InitializerCommand()
        {
            CancelCommand = new DelegateCommand<object>(OnCancelExecute, CanCancelExecute);
            CommitCommand = new DelegateCommand<object>(OnCommitExecute, CanCommitExecute);
            RejectCommand = new DelegateCommand<object>(OnRejectExecute);
            RepickCommand = new DelegateCommand<object>(OnRepickExecute);
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
            if (!ContractAircrafts.AutoLoad)
                ContractAircrafts.AutoLoad = true;
            else
                ContractAircrafts.Load(true);

            if (!PlanAircrafts.AutoLoad)
                PlanAircrafts.AutoLoad = true;
            else
                PlanAircrafts.Load(true);
        }

        #region 业务

        #region 合同飞机集合

        /// <summary>
        ///     合同飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircrafts { get; set; }

        #region 未匹配的合同飞机集合

        /// <summary>
        ///     未匹配的合同飞机集合
        /// </summary>
        public ObservableCollection<ContractAircraftDTO> NotMatchContractAircrafts
        {
            get
            {
                var contractAircrafts = ContractAircrafts.Where(p => p.PlanAircraftID == null).ToList();
                var notMatchContAcs = new ObservableCollection<ContractAircraftDTO>();
                foreach (var contractAircraft in contractAircrafts)
                {
                    notMatchContAcs.Add(contractAircraft);
                }
                return notMatchContAcs;
            }
        }

        #endregion

        #region 已匹配的合同飞机集合

        /// <summary>
        ///     已匹配的合同飞机集合
        /// </summary>
        public ObservableCollection<ContractAircraftDTO> MatchedContractAircrafts
        {
            get
            {
                var contractAircrafts = ContractAircrafts.Where(p => p.PlanAircraftID != null).ToList();
                var matchedContAcs = new ObservableCollection<ContractAircraftDTO>();
                foreach (var contractAircraft in contractAircrafts)
                {
                    matchedContAcs.Add(contractAircraft);
                }
                return matchedContAcs;
            }
        }

        #endregion

        #endregion

        #region 选择的合同飞机

        private ContractAircraftDTO _selContractAircraft;

        /// <summary>
        ///     选择的合同飞机
        /// </summary>
        public ContractAircraftDTO SelContractAircraft
        {
            get { return _selContractAircraft; }
            private set
            {
                if (_selContractAircraft != value)
                {
                    _selContractAircraft = value;
                    RaisePropertyChanged(() => SelContractAircraft);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 命令

        #region 取消匹配命令

        public DelegateCommand<object> RejectCommand { get; private set; }

        /// <summary>
        ///     执行取消匹配命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnRejectExecute(object sender)
        {
            var currentItem = sender as ContractAircraftDTO;
            if (currentItem != null && currentItem.PlanAircraftID != null)
            {
                var contractAircraft = ContractAircrafts.FirstOrDefault(p =>
                    p.ContractNumber == currentItem.ContractNumber &&
                    p.RankNumber == currentItem.RankNumber);
                if (contractAircraft != null)
                {
                    contractAircraft.PlanAircraftID = null;
                    contractAircraft.PlanAircraft = null;
                }
                ContractAircrafts.SubmitChanges();
                RaisePropertyChanged(() => NotMatchContractAircrafts);
                RaisePropertyChanged(() => MatchedContractAircrafts);
            }
        }

        #endregion

        #region 重新匹配命令

        public DelegateCommand<object> RepickCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnRepickExecute(object sender)
        {
            var currentItem = sender as ContractAircraftDTO;
            if (currentItem != null)
                SelContractAircraft =
                    MatchedContractAircrafts.FirstOrDefault(
                        p => p.ContractNumber == currentItem.ContractNumber && p.RankNumber == currentItem.RankNumber);
            PlanAircraftChildView.ShowDialog();
        }

        #endregion

        #endregion

        #endregion

        #region 子窗体相关操作

        [Import] public PlanAircraftChildView PlanAircraftChildView; //初始化子窗体

        #region 计划飞机集合

        /// <summary>
        ///     计划飞机集合
        /// </summary>
        public QueryableDataServiceCollectionView<PlanAircraftDTO> PlanAircrafts { get; set; }

        #endregion

        #region 选择的计划飞机

        private PlanAircraftDTO _selPlanAircraft;

        /// <summary>
        ///     合同飞机
        /// </summary>
        public PlanAircraftDTO SelPlanAircraft
        {
            get { return _selPlanAircraft; }
            private set
            {
                if (_selPlanAircraft != value)
                {
                    _selPlanAircraft = value;
                    RaisePropertyChanged(() => SelPlanAircraft);
                }
            }
        }

        #endregion

        #region 命令

        #region 取消命令

        public DelegateCommand<object> CancelCommand { get; private set; }

        /// <summary>
        ///     执行取消命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCancelExecute(object sender)
        {
            PlanAircraftChildView.Close();
        }

        /// <summary>
        ///     判断取消命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>取消命令是否可用。</returns>
        public bool CanCancelExecute(object sender)
        {
            return true;
        }

        #endregion

        #region 确定命令

        public DelegateCommand<object> CommitCommand { get; private set; }

        /// <summary>
        ///     执行确定命令。
        /// </summary>
        /// <param name="sender"></param>
        public void OnCommitExecute(object sender)
        {
            if (SelContractAircraft != null && SelPlanAircraft != null)
            {
                var contractAircraft = ContractAircrafts.FirstOrDefault(p =>
                    p.ContractNumber == SelContractAircraft.ContractNumber &&
                    p.RankNumber == SelContractAircraft.RankNumber);
                if (contractAircraft != null)
                    contractAircraft.PlanAircraftID = SelPlanAircraft.Id;
                ContractAircrafts.SubmitChanges();
                RaisePropertyChanged(() => NotMatchContractAircrafts);
                RaisePropertyChanged(() => MatchedContractAircrafts);
                PlanAircraftChildView.Close();
            }
        }


        /// <summary>
        ///     判断确定命令是否可用。
        /// </summary>
        /// <param name="sender"></param>
        /// <returns>确定命令是否可用。</returns>
        public bool CanCommitExecute(object sender)
        {
            return true;
        }

        #endregion

        #endregion

        #endregion
    }
}