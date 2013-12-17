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
    [Export(typeof (EnginePaymentScheduleVM))]
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
            InitialContractEngine(); // 初始化合同飞机信息。
            InitialEnginePaymentSchedule(); // 初始化飞机付款计划信息。
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
                        LoadEnginePaymentScheduleByContractAcId(); //根据合同发动机的Id获取飞机付款计划

                    RaisePropertyChanged(() => SelectedContractEngine);
                }
            }
        }


        /// <summary>
        ///     获取所有合同发动机信息。
        /// </summary>
        public QueryableDataServiceCollectionView<ContractEngineDTO> ContractEnginesView { get; set; }

        /// <summary>
        ///     初始化合同飞机信息。
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

        private FilterDescriptor _enginePaymentScheduleFilter; //查找合同发动机下的付款计划。

        private EnginePaymentScheduleDTO _selectedEnginePaymentSchedule;

        /// <summary>
        ///     选择付款计划
        /// </summary>
        public EnginePaymentScheduleDTO SelectedEnginePaymentSchedule
        {
            get { return _selectedEnginePaymentSchedule; }
            set
            {
                if (_selectedEnginePaymentSchedule != value)
                {
                    _selectedEnginePaymentSchedule = value;
                    RaisePropertyChanged(() => SelectedEnginePaymentSchedule);
                }
            }
        }

        /// <summary>
        ///     获取所有合同飞机的付款计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<EnginePaymentScheduleDTO> EnginePaymentSchedulesView { get; set; }

        /// <summary>
        ///     初始化飞机付款计划信息。
        /// </summary>
        private void InitialEnginePaymentSchedule()
        {
            EnginePaymentSchedulesView = Service.CreateCollection(_context.EnginePaymentSchedules);
            _enginePaymentScheduleFilter = new FilterDescriptor("ContractEngineId", FilterOperator.IsEqualTo, 0);
            EnginePaymentSchedulesView.FilterDescriptors.Add(_enginePaymentScheduleFilter);
            EnginePaymentSchedulesView.LoadedData += (sender, e) =>
                {
                    if (e.HasError)
                    {
                        e.MarkErrorAsHandled();
                        return;
                    }
                    if (SelectedEnginePaymentSchedule == null)
                    {
                        SelectedEnginePaymentSchedule = e.Entities.Cast<EnginePaymentScheduleDTO>().FirstOrDefault();
                    }
                };
        }

        /// <summary>
        ///     根据合同发动机的Id获取付款计划
        /// </summary>
        private void LoadEnginePaymentScheduleByContractAcId()
        {
            _enginePaymentScheduleFilter.Value = SelectedContractEngine.ContractEngineId;
            if (!EnginePaymentSchedulesView.AutoLoad)
            {
                EnginePaymentSchedulesView.AutoLoad = true;
            }
            else
                EnginePaymentSchedulesView.Load(true);
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
            ContractEnginesView.AutoLoad = true;
        }

        #endregion
    }
}