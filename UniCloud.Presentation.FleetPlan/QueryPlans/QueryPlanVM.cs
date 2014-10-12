#region 版本信息

// ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：陈春勇 时间：2014/01/09，20:01
// 文件名：QueryPlanVM.cs
// 程序集：UniCloud.Presentation.FleetPlan
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================

#endregion

using System;
using System.Collections.Generic;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.ComponentModel.Composition;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;
using Telerik.Windows.Data;
using System.Linq;
namespace UniCloud.Presentation.FleetPlan.QueryPlans
{
    [Export(typeof(QueryPlanVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class QueryPlanVM: UniCloud.Presentation.MVVM.ViewModelBase
    {
        private readonly FleetPlanData _context;
        private readonly IFleetPlanService _service;

            /// <summary>
        ///     构造函数。
        /// </summary>
        [ImportingConstructor]
        public QueryPlanVM(IFleetPlanService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;   
            InitialPlan();//加载计划
            InitialComparePlan();//加载比较计划
        }

        #region 加载计划

        private PlanDTO _selectedPlan;

        /// <summary>
        ///     选择计划。
        /// </summary>
        public PlanDTO SelectedPlan
        {
            get { return _selectedPlan; }
            set
            {
                if (_selectedPlan != value)
                {
                    _selectedPlan = value;
                    if (value != null)
                    {
                        PlanHistories = value.PlanHistories;
                    }
                    RaisePropertyChanged(() => SelectedPlan);
                }
            }
        }

        /// <summary>
        ///     获取所有计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> PlansView { get; set; }

        private IEnumerable<PlanHistoryDTO> _planHistories;
        /// <summary>
        /// 原始计划明细
        /// </summary>
        public IEnumerable<PlanHistoryDTO> PlanHistories
        {
            get
            {
                return _planHistories;
            }
            set
            {
                if (_planHistories!=value)
                {
                    _planHistories = value;
                    RaisePropertyChanged(()=>PlanHistories);
                }
            }
        }

        /// <summary>
        ///     初始化计划信息。
        /// </summary>
        private void InitialPlan()
        {
            PlansView = _service.CreateCollection(_context.Plans);
            _service.RegisterCollectionView(PlansView);
            PlansView.PageSize = 20;
            PlansView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedPlan == null)
                {
                    SelectedPlan = e.Entities.Cast<PlanDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 加载比较的计划

        private PlanDTO _selectedComparePlan;

        /// <summary>
        ///     选择比较计划。
        /// </summary>
        public PlanDTO SelectedComparePlan
        {
            get { return _selectedComparePlan; }
            set
            {
                if (_selectedComparePlan != value)
                {
                    _selectedComparePlan = value;
                    if (value!=null)
                    {
                        ComparePlanHistories = value.PlanHistories;
                    }
                    RaisePropertyChanged(() => SelectedComparePlan);
                }
            }
        }

        private IEnumerable<PlanHistoryDTO> _comparePlanHistories;
        /// <summary>
        /// 比较计划明细
        /// </summary>
        public IEnumerable<PlanHistoryDTO> ComparePlanHistories
        {
            get
            {
                return _comparePlanHistories;
            }
            set
            {
                if (_comparePlanHistories != value)
                {
                    _comparePlanHistories = value;
                    RaisePropertyChanged(() => ComparePlanHistories);
                }
            }
        }


        /// <summary>
        ///     获取所有比较计划信息。
        /// </summary>
        public QueryableDataServiceCollectionView<PlanDTO> ComparePlansView { get; set; }

        /// <summary>
        ///     初始化比较计划信息。
        /// </summary>
        private void InitialComparePlan()
        {
            ComparePlansView = _service.CreateCollection(_context.Plans);
            ComparePlansView.PageSize = 20;
            ComparePlansView.LoadedData += (sender, e) =>
            {
                if (e.HasError)
                {
                    e.MarkErrorAsHandled();
                    return;
                }
                if (SelectedComparePlan == null)
                {
                    SelectedComparePlan = e.Entities.Cast<PlanDTO>().FirstOrDefault();
                }
            };
        }

        #endregion

        #region 重载基类服务

        public override void LoadData()
        {
            //加载比较计划
            if (!ComparePlansView.AutoLoad)
            {
                ComparePlansView.AutoLoad = true;
            }
            else
            {
                ComparePlansView.Load(true);
            }

            //加载计划
            if (!PlansView.AutoLoad)
            {
                PlansView.AutoLoad = true;
            }
            else
            {
                PlansView.Load(true);
            }

       
        }
        #endregion
    }
}
