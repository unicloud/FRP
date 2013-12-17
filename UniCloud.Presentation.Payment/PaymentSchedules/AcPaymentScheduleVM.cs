#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2013/12/17，10:12
// 文件名：AcPaymentScheduleVM.cs
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
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.Payment;
using UniCloud.Presentation.Service.Payment.Payment;

#endregion

namespace UniCloud.Presentation.Payment.PaymentSchedules
{
    [Export(typeof (AcPaymentScheduleVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AcPaymentScheduleVM : EditViewModelBase
    {
        private PaymentData _context;

        /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public AcPaymentScheduleVM()
        {
            InitialContractAircraft(); // 初始化合同飞机信息。
            InitialAcPaymentSchedule(); // 初始化飞机付款计划信息。
        }

        #region 加载合同飞机

        private ContractAircraftDTO _selectedContractAircraft;

        /// <summary>
        ///     选择合同飞机。
        /// </summary>
        public ContractAircraftDTO SelectedContractAircraft
        {
            get { return _selectedContractAircraft; }
            set
            {
                if (_selectedContractAircraft != value)
                {
                    _selectedContractAircraft = value;
                    if (value != null)
                        LoadAcPaymentScheduleByContractAcId(); //mo根据合同飞机的Id获取飞机付款计划

                    RaisePropertyChanged(() => SelectedContractAircraft);
                }
            }
        }


        /// <summary>
        ///     获取所有合同飞机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ContractAircraftDTO> ContractAircraftsView { get; set; }

        /// <summary>
        ///     初始化合同飞机信息。
        /// </summary>
        private void InitialContractAircraft()
        {
            ContractAircraftsView = Service.CreateCollection(_context.ContractAircrafts);
            ContractAircraftsView.PageSize = 20;
            ContractAircraftsView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    if (SelectedContractAircraft == null)
                    {
                        SelectedContractAircraft = e.Entities.Cast<ContractAircraftDTO>().FirstOrDefault();
                    }
                };
        }

        #endregion

        #region 加载合同飞机下的付款计划

        private FilterDescriptor _acPaymentScheduleFilter; //查找合同飞机下的付款计划。

        private AcPaymentScheduleDTO _selectedAcPaymentSchedule;

        /// <summary>
        ///     选择付款计划
        /// </summary>
        public AcPaymentScheduleDTO SelectedAcPaymentSchedule
        {
            get { return _selectedAcPaymentSchedule; }
            set
            {
                if (_selectedAcPaymentSchedule != value)
                {
                    _selectedAcPaymentSchedule = value;
                    RaisePropertyChanged(() => SelectedAcPaymentSchedule);
                }
            }
        }

        /// <summary>
        ///     获取所有合同飞机的付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<AcPaymentScheduleDTO> AcPaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化飞机付款计划信息。
        /// </summary>
        private void InitialAcPaymentSchedule()
        {
            AcPaymentSchedulesView = Service.CreateCollection(_context.AcPaymentSchedules);
            _acPaymentScheduleFilter = new FilterDescriptor("ContractAcId", FilterOperator.IsEqualTo, 0);
            AcPaymentSchedulesView.FilterDescriptors.Add(_acPaymentScheduleFilter);
            AcPaymentSchedulesView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    if (SelectedAcPaymentSchedule == null)
                    {
                        SelectedAcPaymentSchedule = e.Entities.Cast<AcPaymentScheduleDTO>().FirstOrDefault();
                    }
                };
        }

        /// <summary>
        ///     根据合同飞机的Id获取飞机付款计划
        /// </summary>
        private void LoadAcPaymentScheduleByContractAcId()
        {
            _acPaymentScheduleFilter.Value = SelectedContractAircraft.ContractAircrafId;
            if (!AcPaymentSchedulesView.AutoLoad)
            {
                AcPaymentSchedulesView.AutoLoad = true;
            }
            else
                AcPaymentSchedulesView.Load(true);
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
            ContractAircraftsView.AutoLoad = true;
        }

        #endregion
    }
}