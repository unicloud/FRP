#region 版本信息

/* ========================================================================
// 版权所有 (C) 2014 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2014/2/23 17:42:22
// 文件名：TechnicalSolutionVm
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Data;
using UniCloud.Presentation.CommonExtension;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.Part;
using UniCloud.Presentation.Service.Part.Part;

#endregion

namespace UniCloud.Presentation.Part.ManageTS
{
    [Export(typeof(TechnicalSolutionVm))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class TechnicalSolutionVm : EditViewModelBase
    {
        #region 声明、初始化

        private readonly IRegionManager _regionManager;
        private readonly IPartService _service;
        private PartData _context;

        [ImportingConstructor]
        public TechnicalSolutionVm(IRegionManager regionManager, IPartService service)
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
            TechnicalSolutions = _service.CreateCollection(_context.TechnicalSolutions, o => o.TsLines);
            _service.RegisterCollectionView(TechnicalSolutions);//注册查询集合

            PnRegs = new QueryableDataServiceCollectionView<PnRegDTO>(_context, _context.PnRegs);

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
            if (!TechnicalSolutions.AutoLoad)
                TechnicalSolutions.AutoLoad = true;
            else
                TechnicalSolutions.Load(true);

            PnRegs.AutoLoad = true;
        }

        #region 业务

        #region 技术解决方案集合

        /// <summary>
        ///     技术解决方案集合
        /// </summary>
        public QueryableDataServiceCollectionView<TechnicalSolutionDTO> TechnicalSolutions { get; set; }

        #endregion

        #region 件号集合

        /// <summary>
        ///     件号集合
        /// </summary>
        public QueryableDataServiceCollectionView<PnRegDTO> PnRegs { get; set; }

        #endregion

        #region 选择的技术解决方案

        private TechnicalSolutionDTO _selTechnicalSolution;

        /// <summary>
        ///     选择的技术解决方案
        /// </summary>
        public TechnicalSolutionDTO SelTechnicalSolution
        {
            get { return _selTechnicalSolution; }
            private set
            {
                if (_selTechnicalSolution != value)
                {
                    _selTechnicalSolution = value;
                    RaisePropertyChanged(() => SelTechnicalSolution);

                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的方案明细

        private TsLineDTO _selTsLine;

        /// <summary>
        ///     选择的方案明细
        /// </summary>
        public TsLineDTO SeTsLine
        {
            get { return _selTsLine; }
            private set
            {
                if (_selTsLine != value)
                {
                    _selTsLine = value;
                    RaisePropertyChanged(() => SeTsLine);
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

        #region 创建技术解决方案

        /// <summary>
        ///     创建技术解决方案
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var ts = new TechnicalSolutionDTO()
            {
                Id = RandomHelper.Next(),
            };
            TechnicalSolutions.AddNew(ts);
        }

        private bool CanNew(object obj)
        {
            return true;
        }

        #endregion

        #region 删除技术解决方案

        /// <summary>
        ///     删除技术解决方案
        /// </summary>
        public DelegateCommand<object> RemoveCommand { get; private set; }

        private void OnRemove(object obj)
        {
            if (_selTechnicalSolution != null)
            {
                TechnicalSolutions.Remove(_selTechnicalSolution);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selTechnicalSolution != null;
        }

        #endregion

        #region 增加技术解决方案明细

        /// <summary>
        ///     增加技术解决方案明细
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            var newTsLine = new TsLineDTO
            {
                Id = RandomHelper.Next(),
                TsNumber = SelTechnicalSolution.TsNumber,
            };
            SelTechnicalSolution.TsLines.Add(newTsLine);
        }

        private bool CanAddEntity(object obj)
        {
            return _selTechnicalSolution != null;
        }

        #endregion

        #region 移除技术解决方案明细

        /// <summary>
        ///     移除技术解决方案明细
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selTsLine != null)
            {
                SelTechnicalSolution.TsLines.Remove(_selTsLine);
            }
        }

        private bool CanRemoveEntity(object obj)
        {
            return _selTsLine != null;
        }

        #endregion

        #endregion
    }
}