#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Data.Services.Client;
using System.Windows;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PerformFleetPlan
{
    [Export(typeof (OperationChildVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class OperationChildVM : ViewModelBase
    {
        private readonly FleetPlanData _context;

        [ImportingConstructor]
        public OperationChildVM(IFleetPlanService service)
            : base(service)
        {
            _context = service.Context;
        }

        #region 查询申请、运营历史等相关信息

        private bool _operationIsBusy;

        /// <summary>
        ///     批文历史集合
        /// </summary>
        public IEnumerable<ApprovalHistoryDTO> ApprovalHistories
        {
            get
            {
                if (_selPerformPlan == null || _selPerformPlan.ApprovalHistory == null)
                {
                    return null;
                }
                var ls = new List<ApprovalHistoryDTO> {_selPerformPlan.ApprovalHistory};
                return ls;
            }
        }

        /// <summary>
        ///     运营权历史
        /// </summary>
        public IEnumerable<OperationHistoryDTO> OperationHistories
        {
            get
            {
                if (_selPerformPlan == null || _selPerformPlan.OperationHistory == null)
                {
                    return null;
                }
                var ls = new List<OperationHistoryDTO> {_selPerformPlan.OperationHistory};
                return ls;
            }
        }

        /// <summary>
        ///     商业数据
        /// </summary>
        public IEnumerable<AircraftBusinessDTO> AircraftBusiness
        {
            get
            {
                if (_selPerformPlan == null || _selPerformPlan.AircraftBusiness == null)
                {
                    return null;
                }
                var ls = new List<AircraftBusinessDTO> {_selPerformPlan.AircraftBusiness};
                return ls;
            }
        }

        /// <summary>
        ///     运营是否处于忙中
        /// </summary>
        public bool OperationIsBusy
        {
            get { return _operationIsBusy; }
            set
            {
                _operationIsBusy = value;
                RaisePropertyChanged(() => OperationIsBusy);
            }
        }

        private void GetPerformPlan()
        {
            RaisePropertyChanged(() => ApprovalHistories);
            RaisePropertyChanged(() => OperationHistories);
            RaisePropertyChanged(() => AircraftBusiness);
        }

        #region 查看申请、运营历史

        private PerformPlan _selPerformPlan; //选中的执行计划情况

        /// <summary>
        ///     加载的运营情况
        /// </summary>
        public void OnLoadPerformPlan(Uri path)
        {
            OperationIsBusy = true;
            //查询
            _context.BeginExecute<PerformPlan>(path,
                result => Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    var context = result.AsyncState as FleetPlanData;
                    OperationIsBusy = false;
                    try
                    {
                        if (context != null)
                        {
                            foreach (var performPlan in context.EndExecute<PerformPlan>(result))
                            {
                                _selPerformPlan = performPlan;
                                GetPerformPlan(); //获取查询结果，其中包括申请，运营历史、商业数据
                            }
                        }
                    }
                    catch (DataServiceQueryException ex)
                    {
                        QueryOperationResponse response = ex.Response;

                        Console.WriteLine(response.Error.Message);
                    }
                }), _context);
        }

        #endregion

        #endregion

        public override void LoadData()
        {
        }
    }
}