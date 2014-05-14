#region 版本信息
/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/4/8 14:53:32
// 文件名：SnRegVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;
using UniCloud.Presentation.Service.Part.Part.Enums;

#endregion

namespace UniCloud.Presentation.Part.PnRegAndSnReg
{
    [Export(typeof(SnRegVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class SnRegVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly PartData _context;
        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;

        [ImportingConstructor]
        public SnRegVm(IRegionManager regionManager, IPartService service)
            : base(service)
        {
            _regionManager = regionManager;
            _service = service;
            _context = _service.Context;
            InitializeVM();
        }

        /// <summary>
        ///     初始化ViewModel
        ///     <remarks>
        ///         统一在此处创建并注册CollectionView集合。
        ///     </remarks>
        /// </summary>
        private void InitializeVM()
        {
            SnRegs = new QueryableDataServiceCollectionView<SnRegDTO>(_context, _context.SnRegs);
            SnRegs.PageSize = 20;
            SnRegs.LoadedData += (o, e) =>
                                 {
                                     if (SelSnReg == null)
                                         SelSnReg = SnRegs.FirstOrDefault();
                                 };
            SnHistories = _service.CreateCollection(_context.SnHistories);
            SnHistories.LoadedData += (s, e) =>
            {
                if (SelSnReg != null)
                {
                    ViewSnHistories=new ObservableCollection<SnHistoryDTO>();
                    var snHistories =
                        SnHistories.SourceCollection.Cast<SnHistoryDTO>()
                            .Where(p => p.SnRegId == SelSnReg.Id)
                            .ToList();
                    ViewSnHistories.AddRange(snHistories);
                }
            };
        }

        #endregion

        #region 数据

        #region 公共属性

        #region 序号件集合
        private SnRegDTO _selSnReg;

        /// <summary>
        ///     序号件集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnRegDTO> SnRegs { get; set; }

        /// <summary>
        ///     选择的序号件
        /// </summary>
        public SnRegDTO SelSnReg
        {
            get { return this._selSnReg; }
            private set
            {
                if (this._selSnReg != value)
                {
                    this._selSnReg = value;
                    if (value != null)
                    {
                        ViewSnHistories = new ObservableCollection<SnHistoryDTO>();
                        var snHistories =
                            SnHistories.SourceCollection.Cast<SnHistoryDTO>()
                                .Where(p => p.SnRegId == SelSnReg.Id)
                                .ToList();
                        ViewSnHistories.AddRange(snHistories);
                    }
                    RaisePropertyChanged(() => this.SelSnReg);
                    RefreshCommandState();
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
            SnRegs.Load(true);

            if (!SnHistories.AutoLoad)
                SnHistories.AutoLoad = true;
        }

        #region 业务

        #region 序号件拆换记录集合

        private SnHistoryDTO _selSnHistory;
        private ObservableCollection<SnHistoryDTO> _viewSnHistories=new ObservableCollection<SnHistoryDTO>(); 

        /// <summary>
        ///     件装机历史集合
        /// </summary>
        public QueryableDataServiceCollectionView<SnHistoryDTO> SnHistories { get; set; }

        /// <summary>
        ///     选择的序号件拆换记录
        /// </summary>
        public SnHistoryDTO SelSnHistory
        {
            get { return this._selSnHistory; }
            private set
            {
                if (this._selSnHistory != value)
                {
                    this._selSnHistory = value;
                    RaisePropertyChanged(() => this.SelSnHistory);
                    RefreshCommandState();
                }
            }
        }

        /// <summary>
        ///     序号件装机历史
        /// </summary>
        public ObservableCollection<SnHistoryDTO> ViewSnHistories
        {
            get { return this._viewSnHistories; }
            private set
            {
                if (this._viewSnHistories != value)
                {
                    this._viewSnHistories = value;
                    RaisePropertyChanged(() => this.ViewSnHistories);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region 操作

        #region 刷新按钮状态

        protected override void RefreshCommandState()
        {
        }

        #endregion
        #endregion
    }
}
