﻿#region 版本信息
/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 19:09:27
// 文件名：CaacProgrammingVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/
#endregion

#region 命名空间

using System;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(CaacProgrammingVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class CaacProgrammingVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private FleetPlanData _context;
        private DocumentDTO _document = new DocumentDTO();

        [Import]
        public DocumentViewer DocumentView;

        [ImportingConstructor]
        public CaacProgrammingVM(IRegionManager regionManager)
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
            CaacProgrammings = Service.CreateCollection(_context.CaacProgrammings,
                (o, p, c) =>
                {
                    foreach (var caacProgramming in from object item in o select item as CaacProgrammingDTO)
                    {
                        caacProgramming.CaacProgrammingLines.CollectionChanged += c;
                        caacProgramming.CaacProgrammingLines.ToList().ForEach(ol => ol.PropertyChanged += p);
                    }
                });
            Service.RegisterCollectionView(CaacProgrammings);

            Programmings = new QueryableDataServiceCollectionView<ProgrammingDTO>(_context, _context.Programmings);

            AircraftCategories = new QueryableDataServiceCollectionView<AircraftCategoryDTO>(_context, _context.AircraftCategories);
            
            Managers = new QueryableDataServiceCollectionView<ManagerDTO>(_context, _context.Managers);

        }

        /// <summary>
        ///     初始化命令。
        /// </summary>
        private void InitializerCommand()
        {
            NewCommand = new DelegateCommand<object>(OnNew, CanNew);
            RemoveCommand = new DelegateCommand<object>(OnRemove, CanRemove);
            AddEntityCommand = new DelegateCommand<object>(OnAddEntity, CanAddEntity);
            RemoveEntityCommand = new DelegateCommand<object>(OnRemoveEntity, CanRemoveEntity);
        }

        /// <summary>
        ///     创建服务实例
        /// </summary>
        protected override IService CreateService()
        {
            _context = new FleetPlanData(AgentHelper.FleetPlanServiceUri);
            return new FleetPlanService(_context);
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
            CaacProgrammings.Load(true);
            Programmings.Load(true);
            AircraftCategories.Load(true);
            Managers.Load(true);
        }

        #region 业务

        #region 民航局公司五年规划集合

        /// <summary>
        ///     民航局公司五年规划集合
        /// </summary>
        public QueryableDataServiceCollectionView<CaacProgrammingDTO> CaacProgrammings { get; set; }

        #endregion

        #region 规划期间集合

        /// <summary>
        ///     规划期间集合
        /// </summary>
        public QueryableDataServiceCollectionView<ProgrammingDTO> Programmings { get; set; }

        #endregion

        #region 座级集合

        /// <summary>
        ///     座级集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftCategoryDTO> AircraftCategories { get; set; }

        #endregion

        #region 发文单位集合

        /// <summary>
        ///     发文单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<ManagerDTO> Managers { get; set; }

        #endregion

        #region 选择的规划

        private CaacProgrammingDTO _selCaacProgramming;

        /// <summary>
        /// 选择的规划
        /// </summary>
        public CaacProgrammingDTO SelCaacProgramming
        {
            get { return this._selCaacProgramming; }
            private set
            {
                if (this._selCaacProgramming != value)
                {
                    _selCaacProgramming = value;
                    this.RaisePropertyChanged(() => this.SelCaacProgramming);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的规划明细

        private CaacProgrammingLineDTO _selCaacProgrammingLine;

        /// <summary>
        /// 选择的规划明细
        /// </summary>
        public CaacProgrammingLineDTO SelCaacProgrammingLine
        {
            get { return this._selCaacProgrammingLine; }
            private set
            {
                if (this._selCaacProgrammingLine != value)
                {
                    _selCaacProgrammingLine = value;
                    this.RaisePropertyChanged(() => this.SelCaacProgrammingLine);

                    // 刷新按钮状态
                    RefreshCommandState();
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
            AddAttachCommand.RaiseCanExecuteChanged();
            NewCommand.RaiseCanExecuteChanged();
            RemoveCommand.RaiseCanExecuteChanged();
            AddEntityCommand.RaiseCanExecuteChanged();
            RemoveEntityCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建新规划

        /// <summary>
        ///     创建新规划
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var caacProg = new CaacProgrammingDTO
            {
                Id = new Guid(),
                CreateDate = DateTime.Now,
            };
            CaacProgrammings.AddNew(caacProg);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除规划

        /// <summary>
        ///     删除规划
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (_selCaacProgramming != null)
            {
                CaacProgrammings.Remove(_selCaacProgramming);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selCaacProgramming != null;
        }

        #endregion

        #region 增加规划行

        /// <summary>
        ///     增加规划行
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            var caacProgLine = new CaacProgrammingLineDTO
            {
                Id = new Guid(),
                Number = 1,
            };

            SelCaacProgramming.CaacProgrammingLines.Add(caacProgLine);
        }

        private bool CanAddEntity(object obj)
        {
            return _selCaacProgramming != null;
        }

        #endregion

        #region 移除规划行

        /// <summary>
        ///     移除规划行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selCaacProgrammingLine != null)
            {
                SelCaacProgramming.CaacProgrammingLines.Remove(_selCaacProgrammingLine);
            }
        }

        private bool CanRemoveEntity(object obj)
        {
            return _selCaacProgrammingLine != null;
        }

        #endregion

        #region 添加附件
        protected override void OnAddAttach(object sender)
        {
            DocumentView.ViewModel.InitData(false, _selCaacProgramming.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();
        }

        protected override bool CanAddAttach(object obj)
        {
            return _selCaacProgramming != null;
        }

        private void DocumentViewerClosed(object sender, WindowClosedEventArgs e)
        {
            if (DocumentView.Tag is DocumentDTO)
            {
                _document = DocumentView.Tag as DocumentDTO;
                SelCaacProgramming.DocumentId = _document.DocumentId;
                SelCaacProgramming.DocName = _document.Name;
            }
        }
        #endregion

        #region 查看附件
        protected override void OnViewAttach(object sender)
        {
            if (SelCaacProgramming == null)
            {
                MessageAlert("请选择一条记录！");
                return;
            }
            DocumentView.ViewModel.InitData(true, _selCaacProgramming.DocumentId, DocumentViewerClosed);
            DocumentView.ShowDialog();

        }
        #endregion

        #endregion
    }

}