#region 版本信息

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
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
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

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;

        [ImportingConstructor]
        public CaacProgrammingVM(IRegionManager regionManager, IFleetPlanService service)
            : base(service)
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
            CaacProgrammings = _service.CreateCollection(_context.CaacProgrammings, o => o.CaacProgrammingLines);
            _service.RegisterCollectionView(CaacProgrammings);//注册查询集合

            Programmings = new QueryableDataServiceCollectionView<ProgrammingDTO>(_context, _context.Programmings);

            AircraftCategories = new QueryableDataServiceCollectionView<AircraftCategoryDTO>(_context,
                _context.AircraftCategories);

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
            if (!CaacProgrammings.AutoLoad)
                CaacProgrammings.AutoLoad = true;
            else
                CaacProgrammings.Load(true);

            Programmings.AutoLoad = true;
            AircraftCategories.AutoLoad = true;
            Managers.AutoLoad = true;
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
        ///     选择的规划
        /// </summary>
        public CaacProgrammingDTO SelCaacProgramming
        {
            get { return _selCaacProgramming; }
            private set
            {
                if (_selCaacProgramming != value)
                {
                    _selCaacProgramming = value;
                    RaisePropertyChanged(() => SelCaacProgramming);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的规划明细

        private CaacProgrammingLineDTO _selCaacProgrammingLine;

        /// <summary>
        ///     选择的规划明细
        /// </summary>
        public CaacProgrammingLineDTO SelCaacProgrammingLine
        {
            get { return _selCaacProgrammingLine; }
            private set
            {
                if (_selCaacProgrammingLine != value)
                {
                    _selCaacProgrammingLine = value;
                    RaisePropertyChanged(() => SelCaacProgrammingLine);

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
                CaacProgrammingId = SelCaacProgramming.Id,
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

        protected override bool CanAddAttach(object obj)
        {
            return _selCaacProgramming != null;
        }

        /// <summary>
        ///     子窗口关闭后执行的操作
        /// </summary>
        /// <param name="doc">添加的附件</param>
        /// <param name="sender">添加附件命令的参数</param>
        protected override void WindowClosed(DocumentDTO doc, object sender)
        {
            base.WindowClosed(doc, sender);
            if (sender is Guid)
            {
                SelCaacProgramming.DocumentId = doc.DocumentId;
                SelCaacProgramming.DocName = doc.Name;
            }
        }
        #endregion

        #endregion
    }
}