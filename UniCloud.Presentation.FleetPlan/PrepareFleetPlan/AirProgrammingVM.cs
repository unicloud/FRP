﻿#region 版本信息

/* ========================================================================
// 版权所有 (C) 2013 UniCloud 
//【本类功能概述】
// 
// 作者：HuangQiBin 时间：2013/12/30 19:08:33
// 文件名：AirProgrammingVM
// 版本：V1.0.0
//
// 修改者： 时间： 
// 修改说明：
// ========================================================================*/

#endregion

#region 命名空间

using System;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.Linq;
using Microsoft.Practices.Prism.Commands;
using Microsoft.Practices.Prism.Regions;
using Telerik.Windows.Controls;
using Telerik.Windows.Data;
using Telerik.Windows.Controls.DataServices;
using UniCloud.Presentation.Document;
using UniCloud.Presentation.MVVM;
using UniCloud.Presentation.Service.CommonService.Common;
using UniCloud.Presentation.Service.FleetPlan;
using UniCloud.Presentation.Service.FleetPlan.FleetPlan;

#endregion

namespace UniCloud.Presentation.FleetPlan.PrepareFleetPlan
{
    [Export(typeof(AirProgrammingVM))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class AirProgrammingVM : EditViewModelBase
    {
        #region 声明、初始化

        private readonly FleetPlanData _context;
        private readonly IRegionManager _regionManager;
        private readonly IFleetPlanService _service;

        [ImportingConstructor]
        public AirProgrammingVM(IRegionManager regionManager, IFleetPlanService service)
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
            var sort = new SortDescriptor { Member = "CreateDate", SortDirection = ListSortDirection.Ascending };
            var group = new GroupDescriptor { Member = "ProgrammingName", SortDirection = ListSortDirection.Ascending };
            AirProgrammings = _service.CreateCollection(_context.AirProgrammings, o => o.AirProgrammingLines);
            //AirProgrammings.SortDescriptors.Add(sort);
            //AirProgrammings.GroupDescriptors.Add(group);
            _service.RegisterCollectionView(AirProgrammings);//注册查询集合

            ProgrammingFiles = _service.CreateCollection(_context.ProgrammingFiles);
            ProgrammingFiles.FilterDescriptors.Add(new FilterDescriptor("Type", FilterOperator.IsEqualTo, 2));
            //ProgrammingFiles.SortDescriptors.Add(sort);
            //ProgrammingFiles.GroupDescriptors.Add(group);
            _service.RegisterCollectionView(ProgrammingFiles);//注册查询集合

            Programmings = new QueryableDataServiceCollectionView<ProgrammingDTO>(_context, _context.Programmings);

            AircraftSeries = new QueryableDataServiceCollectionView<AircraftSeriesDTO>(_context, _context.AircraftSeries);

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
            AddDocCommand = new DelegateCommand<object>(OnAddAttach);
            RemoveDocCommand = new DelegateCommand<object>(OnRemoveDoc, CanRemoveDoc);
            CellEditEndCommand = new DelegateCommand<object>(OnCellEditEnd);
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
            if (!AirProgrammings.AutoLoad)
                AirProgrammings.AutoLoad = true;
            else
                AirProgrammings.Load(true);

            if (!ProgrammingFiles.AutoLoad)
                ProgrammingFiles.AutoLoad = true;
            else
                ProgrammingFiles.Load(true);

            Programmings.AutoLoad = true;
            AircraftSeries.AutoLoad = true;
            Managers.AutoLoad = true;
        }

        #region 业务

        #region 航空公司五年规划集合

        /// <summary>
        ///     航空公司五年规划集合
        /// </summary>
        public QueryableDataServiceCollectionView<AirProgrammingDTO> AirProgrammings { get; set; }

        #endregion

        #region 规划期间集合

        /// <summary>
        ///     规划期间集合
        /// </summary>
        public QueryableDataServiceCollectionView<ProgrammingDTO> Programmings { get; set; }

        #endregion

        #region 规划文档集合

        /// <summary>
        ///     规划文档集合
        /// </summary>
        public QueryableDataServiceCollectionView<ProgrammingFileDTO> ProgrammingFiles { get; set; }

        #endregion

        #region 飞机系列集合

        /// <summary>
        ///     飞机系列集合
        /// </summary>
        public QueryableDataServiceCollectionView<AircraftSeriesDTO> AircraftSeries { get; set; }

        #endregion

        #region 发文单位集合

        /// <summary>
        ///     发文单位集合
        /// </summary>
        public QueryableDataServiceCollectionView<ManagerDTO> Managers { get; set; }

        #endregion

        #region 选择的规划

        private AirProgrammingDTO _selAirProgramming;

        /// <summary>
        ///     选择的规划
        /// </summary>
        public AirProgrammingDTO SelAirProgramming
        {
            get { return _selAirProgramming; }
            private set
            {
                if (_selAirProgramming != value)
                {
                    _selAirProgramming = value;
                    RaisePropertyChanged(() => SelAirProgramming);
                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的规划明细

        private AirProgrammingLineDTO _selAirProgrammingLine;

        /// <summary>
        ///     选择的规划明细
        /// </summary>
        public AirProgrammingLineDTO SelAirProgrammingLine
        {
            get { return _selAirProgrammingLine; }
            private set
            {
                if (_selAirProgrammingLine != value)
                {
                    _selAirProgrammingLine = value;
                    RaisePropertyChanged(() => SelAirProgrammingLine);

                    // 刷新按钮状态
                    RefreshCommandState();
                }
            }
        }

        #endregion

        #region 选择的规划文档

        private ProgrammingFileDTO _selProgrammingFile;

        /// <summary>
        ///     选择的规划文档
        /// </summary>
        public ProgrammingFileDTO SelProgrammingFile
        {
            get { return _selProgrammingFile; }
            private set
            {
                if (_selProgrammingFile != value)
                {
                    _selProgrammingFile = value;
                    RaisePropertyChanged(() => SelProgrammingFile);
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
            AddDocCommand.RaiseCanExecuteChanged();
            RemoveDocCommand.RaiseCanExecuteChanged();
        }

        #endregion

        #region 创建新规划

        /// <summary>
        ///     创建新规划
        /// </summary>
        public DelegateCommand<object> NewCommand { get; private set; }

        private void OnNew(object obj)
        {
            var airProg = new AirProgrammingDTO
            {
                Id = Guid.NewGuid(),
                CreateDate = DateTime.Now,
            };
            AirProgrammings.AddNew(airProg);
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
            if (_selAirProgramming != null)
            {
                AirProgrammings.Remove(_selAirProgramming);
            }
        }

        private bool CanRemove(object obj)
        {
            return _selAirProgramming != null;
        }

        #endregion

        #region 增加规划行

        /// <summary>
        ///     增加规划行
        /// </summary>
        public DelegateCommand<object> AddEntityCommand { get; private set; }

        private void OnAddEntity(object obj)
        {
            var airProgLine = new AirProgrammingLineDTO
            {
                Id = Guid.NewGuid(),
                BuyNum = 1,
                ExportNum = 1,
                LeaseNum = 1,
            };

            SelAirProgramming.AirProgrammingLines.Add(airProgLine);
        }

        private bool CanAddEntity(object obj)
        {
            return _selAirProgramming != null;
        }

        #endregion

        #region 移除规划行

        /// <summary>
        ///     移除规划行
        /// </summary>
        public DelegateCommand<object> RemoveEntityCommand { get; private set; }

        private void OnRemoveEntity(object obj)
        {
            if (_selAirProgrammingLine != null)
            {
                SelAirProgramming.AirProgrammingLines.Remove(_selAirProgrammingLine);
            }
        }

        private bool CanRemoveEntity(object obj)
        {
            return _selAirProgrammingLine != null;
        }

        #endregion

        #region 添加附件

        protected override bool CanAddAttach(object obj)
        {
            return _selAirProgramming != null;
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
                SelAirProgramming.DocumentId = doc.DocumentId;
                SelAirProgramming.DocName = doc.Name;
            }
            else
            {
                var programmingFile = new ProgrammingFileDTO
                {
                    DocName = doc.Name,
                    DocumentId = doc.DocumentId,
                    CreateDate = DateTime.Now,
                    IssuedDate = DateTime.Now,
                    Type = 2,//1-表示民航规划文档，2--表示川航规划文档
                };
                if (SelAirProgramming != null)
                    programmingFile.ProgrammingId = SelAirProgramming.ProgrammingId;

                ProgrammingFiles.AddNew(programmingFile);
            }

        }
        #endregion

        #region 添加规划关联文档

        /// <summary>
        ///     添加规划关联文档
        /// </summary>
        public DelegateCommand<object> AddDocCommand { get; private set; }

        #endregion

        #region 删除规划关联文档

        /// <summary>
        ///     删除规划关联文档
        /// </summary>
        public DelegateCommand<object> RemoveDocCommand { get; private set; }

        private void OnRemoveDoc(object obj)
        {
            if (_selProgrammingFile != null)
            {
                ProgrammingFiles.Remove(SelProgrammingFile);
            }
        }

        private bool CanRemoveDoc(object obj)
        {
            return _selProgrammingFile != null;
        }

        #endregion

        #region GridView单元格变更处理

        public DelegateCommand<object> CellEditEndCommand { set; get; }

        /// <summary>
        ///     GridView单元格变更处理
        /// </summary>
        /// <param name="sender"></param>
        protected virtual void OnCellEditEnd(object sender)
        {
            var gridView = sender as RadGridView;
            if (gridView != null)
            {
                var cell = gridView.CurrentCell;
                if (string.Equals(cell.Column.UniqueName, "ProgrammingNameForAir"))
                {
                    var value = SelAirProgramming.ProgrammingId;
                    var programming = Programmings.FirstOrDefault(p => p.Id == value);
                    if (programming != null)
                        SelAirProgramming.ProgrammingName = programming.Name;
                }
                else if (string.Equals(cell.Column.UniqueName, "ProgrammingNameForFile"))
                {
                    var value = SelProgrammingFile.ProgrammingId;
                    var programming = Programmings.FirstOrDefault(p => p.Id == value);
                    if (programming != null)
                        SelProgrammingFile.ProgrammingName = programming.Name;
                }
            }
        }

        #endregion

        #endregion
    }
}