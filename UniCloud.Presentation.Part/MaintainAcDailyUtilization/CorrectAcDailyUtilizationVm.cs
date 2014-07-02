#region Version Info

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：linxw 时间：2014/3/4 9:53:09
// 文件名：CorrectAcDailyUtilizationVm
// 版本：V1.0.0
//
// 修改者：linxw 时间：2014/3/4 9:53:09
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Telerik.Windows.Data;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.MaintainAcDailyUtilization
{
    [Export(typeof (CorrectAcDailyUtilizationVm))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    public class CorrectAcDailyUtilizationVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IPartService _service;


        [ImportingConstructor]
        public CorrectAcDailyUtilizationVm(IPartService service)
            : base(service)
        {
            _service = service;
            _context = _service.Context;
            InitializeVm();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVm()
        {
            // 创建并注册CollectionView
            Aircrafts = _service.CreateCollection(_context.Aircrafts);
            var sort = new SortDescriptor {Member = "RegNumber", SortDirection = ListSortDirection.Ascending};
            Aircrafts.SortDescriptors.Add(sort);
            AcDailyUtilizations = _service.CreateCollection(_context.AcDailyUtilizations);
            _service.RegisterCollectionView(AcDailyUtilizations);
        }

        #endregion

        #region 数据

        #region 公共属性

        #region Aircraft

        private AircraftDTO _aircraft;

        /// <summary>
        ///     Aircraft集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftDTO> Aircrafts { get; set; }

        public AircraftDTO Aircraft
        {
            get { return _aircraft; }
            set
            {
                _aircraft = value;
                AcDailyUtilizationDtos = AcDailyUtilizations.Where(p => p.AircraftId == _aircraft.Id);
                AcDailyUtilization =
                    AcDailyUtilizationDtos.FirstOrDefault(
                        p => p.Year == DateTime.Now.Year && p.Month == DateTime.Now.Month);
                RaisePropertyChanged(() => Aircraft);
            }
        }

        #endregion

        #region 飞机日利用率

        private AcDailyUtilizationDTO _acDailyUtilization;

        private IEnumerable<AcDailyUtilizationDTO> _acDailyUtilizationDtos;
        public QueryableDataServiceCollectionView<AcDailyUtilizationDTO> AcDailyUtilizations { get; set; }

        public AcDailyUtilizationDTO AcDailyUtilization
        {
            get { return _acDailyUtilization; }
            set
            {
                _acDailyUtilization = value;
                RaisePropertyChanged(() => AcDailyUtilization);
            }
        }

        public IEnumerable<AcDailyUtilizationDTO> AcDailyUtilizationDtos
        {
            get { return _acDailyUtilizationDtos; }
            set
            {
                _acDailyUtilizationDtos = value;
                RaisePropertyChanged(() => AcDailyUtilizationDtos);
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
            // 将CollectionView的AutoLoad属性设为True
            Aircrafts.Load();
            AcDailyUtilizations.Load();
        }

        #endregion

        #endregion

        #region 操作

        #endregion
    }
}