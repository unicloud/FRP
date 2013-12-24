#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/12 9:55:35
// 文件名：FinancingDemandForecastVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using System.ServiceModel.DomainServices.Client;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;

#endregion

namespace UniCloud.Presentation.Payment.QueryAnalyse
{
    [Export(typeof(FinancingDemandForecastVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class FinancingDemandForecastVM : EditViewModelBase
    {
        #region 声明、初始化
        private readonly IRegionManager _regionManager;
        private Point _panOffset;
        private Size _zoom;
        private RadDateTimePicker StartDateTimePicker, EndDateTimePicker;//开始时间控件， 结束时间控件

        [ImportingConstructor]
        public FinancingDemandForecastVM(IRegionManager regionManager)
        {
            _regionManager = regionManager;
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
            Zoom = new Size(1, 1);
            //RelatedDocs = Service.CreateCollection<RelatedDocDTO>(_purchaseData.RelatedDocs);
            //Service.RegisterCollectionView(RelatedDocs); //注册查询集合。
            //RelatedDocs.PropertyChanged += OnViewPropertyChanged;
        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            //NewCommand = new DelegateCommand<object>(OnNew, CanNew);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            //_purchaseData = new PurchaseData(AgentHelper.PurchaseUri);
            //return new PurchaseService(_purchaseData);
            return null;
        }

        #endregion

        #region 数据

        #region 公共属性
        #region ViewModel 属性 EndDate --结束时间

        private DateTime? _endDate = Convert.ToDateTime(DateTime.Now.AddYears(2).ToString("yyyy/M"));
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndDate
        {
            get { return _endDate; }
            set
            {

                if (EndDate != value)
                {
                    if (value == null)
                    {
                        EndDateTimePicker.SelectedValue = _endDate;
                        return;
                    }
                    _endDate = value;
                    this.RaisePropertyChanged(() => this.EndDate);
                    //处理界面需要展示的集合
                    CreateViewFinancingDemandData();
                }
            }
        }
        #endregion

        #region ViewModel 属性 StartDate --开始时间

        private DateTime? _startDate = new DateTime(DateTime.Now.AddYears(-1).Year, 1, 1);
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartDate
        {
            get { return _startDate; }
            set
            {

                if (StartDate != value)
                {
                    if (value == null)
                    {
                        StartDateTimePicker.SelectedValue = _startDate;
                        return;
                    }
                    _startDate = value;
                    this.RaisePropertyChanged(() => this.StartDate);
                    //处理界面需要展示的集合
                    CreateViewFinancingDemandData();
                }
            }
        }
        #endregion

        #region ViewModel 属性 SelectedIndex --时间的统计方式

        private int _selectedIndex = 0;
        /// <summary>
        /// 时间的统计方式
        /// </summary>
        public int SelectedIndex
        {
            get { return _selectedIndex; }
            set
            {

                if (SelectedIndex != value)
                {
                    _selectedIndex = value;
                    //处理界面需要展示的集合
                    CreateViewFinancingDemandData();
                    this.RaisePropertyChanged(() => this.SelectedIndex);

                }
            }
        }
        #endregion

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
            //RelatedDocs.AutoLoad = true;
            LoadFinancingDemandData();
            CreateViewFinancingDemandData();
        }

        #region 业务

        public void LoadFinancingDemandData()
        {
            _financingDemands.Clear();
            var ro = new Random((int)DateTime.Now.Ticks);
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 12; j++)
                {
                    decimal amount = ro.Next(10000, 500000);
                    decimal paidAmount = ro.Next(10000, 500000);
                    var dataItem = new FinancingDemand
                           {
                               Id = ro.Next(),
                               TimeStamp = new DateTime(2001+i,1+j,1),
                               Year = 2001 + i,
                               Month = 1+j,
                               Amount = amount,
                           };
                    if (i < 13)
                    {
                        dataItem.PaidAmount = paidAmount;
                        dataItem.RemainAmount = amount - paidAmount;
                    }
                    FinancingDemands.Add(dataItem);
                }
            }
        }

        public void CreateViewFinancingDemandData()
        {
            if (FinancingDemands != null)
            {
                _viewFinancingDemands.Clear();
                foreach (var dataItem in FinancingDemands)
                {
                    if (SelectedIndex == 1)//按半年统计
                    {
                        if (dataItem.Month != 6 && dataItem.Month != 12)
                        {
                            continue;
                        }
                    }
                    else if (SelectedIndex == 2)//按年份统计
                    {
                        if (dataItem.Month != 12)
                        {
                            continue;
                        }
                    }

                    if (dataItem.TimeStamp<StartDate|| dataItem.TimeStamp>EndDate) continue;
                    dataItem.PaidAmount /= 10000;
                    dataItem.RemainAmount /= 10000;
                    dataItem.Amount /= 10000;
                    ViewFinancingDemands.Add(dataItem);
                }
            }
        }

        #region 资金需求

        private ObservableCollection<FinancingDemand> _financingDemands = new ObservableCollection<FinancingDemand>();

        /// <summary>
        /// 资金需求
        /// </summary>
        public ObservableCollection<FinancingDemand> FinancingDemands
        {
            get { return this._financingDemands; }
            private set
            {
                if (this._financingDemands != value)
                {
                    _financingDemands = value;
                    this.RaisePropertyChanged(() => this.FinancingDemands);
                }
            }
        }



        private ObservableCollection<FinancingDemand> _viewFinancingDemands = new ObservableCollection<FinancingDemand>();

        /// <summary>
        /// 用于界面展示的集合
        /// </summary>
        public ObservableCollection<FinancingDemand> ViewFinancingDemands
        {
            get { return this._viewFinancingDemands; }
            private set
            {
                if (this._viewFinancingDemands != value)
                {
                    _viewFinancingDemands = value;
                    this.RaisePropertyChanged(() => this.ViewFinancingDemands);
                }
            }
        }
        #endregion

        #endregion

        public Point PanOffset
        {
            get { return _panOffset; }
            set
            {
                if (_panOffset != value)
                {
                    _panOffset = value;
                    RaisePropertyChanged("PanOffset");
                }
            }
        }

        public Size Zoom
        {
            get { return _zoom; }
            set
            {
                if (_zoom != value)
                {
                    _zoom = value;
                    RaisePropertyChanged("Zoom");
                }
            }
        }
        #endregion

        #endregion

        #region 操作

        /// <summary>
        /// 控制趋势图中折线（饼状）的显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void checkbox_Checked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox != null)
            {
               var grid = (((checkbox.Parent as StackPanel).Parent as StackPanel).Parent as ScrollViewer).Parent as Grid;
               (grid.Children[0] as RadCartesianChart).Series.FirstOrDefault(p => p.DisplayName == checkbox.Content.ToString()).Visibility = Visibility.Visible;                
            }
        }

        /// <summary>
        /// 控制趋势图中折线（饼状）的隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void checkbox_Unchecked(object sender, RoutedEventArgs e)
        {
            var checkbox = sender as CheckBox;
            if (checkbox != null)
            {
                var grid = (((checkbox.Parent as StackPanel).Parent as StackPanel).Parent as ScrollViewer).Parent as Grid;
                (grid.Children[0] as RadCartesianChart).Series.FirstOrDefault(p => p.DisplayName == checkbox.Content.ToString()).Visibility = Visibility.Collapsed;                
            }
        }
        #endregion
    }

    public class FinancingDemand
    {
        /// <summary>
        /// 主键
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime TimeStamp { get; set; }

        /// <summary>
        /// 年度
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月份
        /// </summary>
        public int Month { get; set; }

        /// <summary>
        /// 累计金额
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// 已付金额
        /// </summary>
        public decimal PaidAmount { get; set; }

        /// <summary>
        /// 剩余金额（资金需求量）
        /// </summary>
        public decimal RemainAmount { get; set; }
    }
}
